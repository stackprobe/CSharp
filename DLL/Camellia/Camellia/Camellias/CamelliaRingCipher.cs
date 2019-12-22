using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using System.Security.Cryptography;

namespace Charlotte.Camellias
{
	public class CamelliaRingCipher
	{
		private CamelliaRingCBC _crcbc;

		public CamelliaRingCipher(string passphrase)
			: this(CamelliaRingCipherUtils.GenerateRawKey(passphrase))
		{ }

		public CamelliaRingCipher(byte[] rawKey)
		{
			_crcbc = new CamelliaRingCBC(rawKey);
		}

		public byte[] Encrypt(byte[] src, int offset = 0)
		{
			return this.Encrypt(src, offset, src.Length - offset);
		}

		public byte[] Encrypt(byte[] src, int offset, int size)
		{
			if (
				offset < 0 ||
				src.Length < offset ||
				src.Length - offset < size
				)
				throw new ArgumentException();

			byte[] dest;

			using (MemoryStream mem = new MemoryStream())
			{
				mem.Write(src, offset, size);
				AddPadding(mem);
				AddRandPart(mem);
				AddHash(mem);
				AddRandPart(mem);
				dest = mem.ToArray();
			}
			_crcbc.Encrypt(dest);
			return dest;
		}

		public byte[] Decrypt(byte[] src, int offset = 0)
		{
			return this.Decrypt(src, offset, src.Length - offset);
		}

		public byte[] Decrypt(byte[] src, int offset, int size)
		{
			if (
				offset < 0 ||
				src.Length < offset ||
				src.Length - offset < size ||
				size < 16 + 64 + 64 + 64 ||
				//size < 256 + 64 + 64 + 64 || // while (size + padSize < 0xff); してなかった時の暗号文はこれより短い (最小padding 旧->新: 16->256)
				size % 16 != 0
				)
				throw new ArgumentException();

			byte[] buff = BinTools.GetSubBytes(src, offset, size);

			_crcbc.Decrypt(buff);

			int buffSize = buff.Length;

			UnaddRandPart(buff, ref buffSize);
			UnaddHash(buff, ref buffSize);
			UnaddRandPart(buff, ref buffSize);
			UnaddPadding(buff, ref buffSize);

			return BinTools.GetSubBytes(buff, 0, buffSize);
		}

		private static void AddPadding(MemoryStream mem)
		{
			int size = (int)mem.Length;
			int padSzLow = ~size & 0x0f;
			int padSize;

			do
			{
				padSize = padSzLow | ((int)SecurityTools.CRandom.GetByte() & 0xf0);
			}
			while (size + padSize < 0xff);

			for (int index = 0; index < padSize; index++)
			{
				mem.WriteByte(SecurityTools.CRandom.GetByte());
			}
			mem.WriteByte((byte)padSize);
		}

		private static void UnaddPadding(byte[] data, ref int size)
		{
			if (size < 1)
				throw new Exception("不正な長さ");

			size--;
			int padSize = (int)data[size];

			if (size < padSize)
				throw new Exception("不正な長さ");

			size -= padSize;
		}

		private const int RAND_PART_SIZE = 64;

		private static void AddRandPart(MemoryStream mem)
		{
			mem.Write(SecurityTools.CRandom.GetBytes(RAND_PART_SIZE), 0, RAND_PART_SIZE);
		}

		private static void UnaddRandPart(byte[] data, ref int size)
		{
			if (size < RAND_PART_SIZE)
				throw new Exception("不正な長さ");

			size -= RAND_PART_SIZE;
		}

		private const int HASH_SIZE = 64;

		private static void AddHash(MemoryStream mem)
		{
			mem.Position = 0;

			using (SHA512 sha512 = SHA512.Create())
			{
				mem.Write(sha512.ComputeHash(mem), 0, HASH_SIZE);
			}
		}

		private static void UnaddHash(byte[] data, ref int size)
		{
			if (size < HASH_SIZE)
				throw new Exception("不正な長さ");

			size -= HASH_SIZE;

			using (SHA512 sha512 = SHA512.Create())
			{
				byte[] hash1 = BinTools.GetSubBytes(data, size, HASH_SIZE);
				byte[] hash2 = sha512.ComputeHash(data, 0, size);

				if (BinTools.Comp(hash1, hash2) != 0)
					throw new Exception("ハッシュ不一致");
			}
		}
	}
}
