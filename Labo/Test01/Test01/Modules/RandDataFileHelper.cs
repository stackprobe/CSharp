using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Test01.Modules
{
	public class RandDataFileHelper
	{
		public static void Make(string file, long size, ulong seed, bool includeHash)
		{
			if (includeHash && size < 16L) throw null;

			XorShift128 s = new XorShift128()
			{
				Y = (uint)seed,
				Z = (uint)(seed >> 32),
			};

			s.Skip(100);

			if (includeHash)
				size -= 16;

			using (FileStream writer = new FileStream(file, FileMode.Create, FileAccess.Write))
			{
				uint v = 0u;

				for (long index = 0; index < size; index++)
				{
					int vi = (int)(index % 4L);

					if (vi == 0)
						v = s.Next();

					writer.WriteByte((byte)((v >> (vi * 8)) & 0xff));
				}
			}
			if (includeHash)
			{
				byte[] hash;

				using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
				using (SHA512 sha512 = SHA512.Create())
				{
					hash = sha512.ComputeHash(reader);
				}
				hash = ToSpecialHash(hash);

				using (FileStream writer = new FileStream(file, FileMode.Append, FileAccess.Write))
				{
					writer.Write(hash, 0, 16);
				}
			}
		}

		public static bool CheckHash(string file)
		{
			long size = new FileInfo(file).Length;

			if (size < 16L) throw null;

			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			using (SHA512 sha512 = SHA512.Create())
			{
				ReadWriteBlockLoopToSize(
					(buff, readSize) => reader.Read(buff, 0, readSize),
					(buff, readSize) => sha512.TransformBlock(buff, 0, readSize, null, 0),
					size - 16L
					);

				sha512.TransformFinalBlock(new byte[0], 0, 0);
				byte[] hash = sha512.Hash;
				hash = ToSpecialHash(hash);

				for (int index = 0; index < 16; index++)
					if (reader.ReadByte() != (int)hash[index])
						return false;
			}
			return true;
		}

		private static void ReadWriteBlockLoopToSize(Action<byte[], int> reader, Action<byte[], int> writer, long size)
		{
			byte[] buff = new byte[128 * 1024 * 1024];

			for (long offset = 0L; offset < size; )
			{
				int readSize = (int)Math.Min((long)buff.Length, size - offset);

				reader(buff, readSize);
				writer(buff, readSize);

				offset += readSize;
			}
		}

		private static byte[] ToSpecialHash(byte[] hash)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				byte[] trailer = Encoding.ASCII.GetBytes("{2bff2813-1e61-463d-929e-eeed8771ef49}"); // shared_uuid@ign

				sha512.TransformBlock(hash, 0, hash.Length, null, 0);
				sha512.TransformFinalBlock(trailer, 0, trailer.Length);

				hash = sha512.Hash;
			}
			return hash;
		}

		private class XorShift128
		{
			public uint X = 1u;
			public uint Y;
			public uint Z;
			public uint A;
			public uint T;

			public uint Next()
			{
				this.T = this.X;
				this.T ^= this.X << 11;
				this.T ^= this.T >> 8;
				this.T ^= this.A;
				this.T ^= this.A >> 19;
				this.X = this.Y;
				this.Y = this.Z;
				this.Z = this.A;
				this.A = this.T;

				return this.T;
			}

			public void Skip(int count)
			{
				for (; 0 < count; count--)
				{
					this.Next();
				}
			}
		}
	}
}
