using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	public class SecurityTools
	{
		public static RandomUnit CRandom = new RandomUnit(new RNGRandomNumberGenerator());

		public static string MakePassword(string allowChars, int length)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < length; index++)
				buff.Append(allowChars[(int)CRandom.GetRandom((uint)allowChars.Length)]);

			return buff.ToString();
		}

		public static string MakePassword()
		{
			return MakePassword(StringTools.DECIMAL + StringTools.ALPHA + StringTools.alpha, 22);
		}

		public static string MakePassword_9A()
		{
			return MakePassword(StringTools.DECIMAL + StringTools.ALPHA, 25);
		}

		public static string MakePassword_9()
		{
			return MakePassword(StringTools.DECIMAL, 39);
		}

		public class AES : IDisposable
		{
			private AesManaged Aes;
			private ICryptoTransform Encryptor = null;
			private ICryptoTransform Decryptor = null;

			public AES(byte[] rawKey)
			{
				if (
					rawKey.Length != 16 &&
					rawKey.Length != 24 &&
					rawKey.Length != 32
					)
					throw new ArgumentException();

				this.Aes = new AesManaged();
				this.Aes.KeySize = rawKey.Length * 8;
				this.Aes.BlockSize = 128;
				this.Aes.Mode = CipherMode.ECB;
				this.Aes.IV = new byte[16]; // dummy
				this.Aes.Key = rawKey;
				this.Aes.Padding = PaddingMode.None;
			}

			public void EncryptBlock(byte[] src, byte[] dest)
			{
				if (
					src.Length != 16 ||
					dest.Length != 16
					)
					throw new ArgumentException();

				if (this.Encryptor == null)
					this.Encryptor = this.Aes.CreateEncryptor();

				this.Encryptor.TransformBlock(src, 0, 16, dest, 0);
			}

			public void DecryptBlock(byte[] src, byte[] dest)
			{
				if (
					src.Length != 16 ||
					dest.Length != 16
					)
					throw new ArgumentException();

				if (this.Decryptor == null)
					this.Decryptor = this.Aes.CreateDecryptor();

				this.Decryptor.TransformBlock(src, 0, 16, dest, 0);
			}

			public void Dispose()
			{
				if (this.Aes != null)
				{
					if (this.Encryptor != null)
						this.Encryptor.Dispose();

					if (this.Decryptor != null)
						this.Decryptor.Dispose();

					this.Aes.Dispose();
					this.Aes = null;
				}
			}
		}

		public class AESRandomNumberGenerator : RandomUnit.IRandomNumberGenerator
		{
			private AES Aes;
			private byte[] Counter = new byte[16];
			private byte[] Block = new byte[16];

			public AESRandomNumberGenerator(int seed)
				: this(seed.ToString())
			{ }

			public AESRandomNumberGenerator(string seed)
				: this(Encoding.UTF8.GetBytes(seed))
			{ }

			public AESRandomNumberGenerator(byte[] seed)
			{
				using (SHA512 sha512 = SHA512.Create())
				{
					byte[] hash = sha512.ComputeHash(seed);
					byte[] rawKey = new byte[16];
					//byte[] rawKey = new byte[24];
					//byte[] rawKey = new byte[32];

					Array.Copy(hash, 0, rawKey, 0, 16);
					//Array.Copy(hash, 0, rawKey, 0, 24);
					//Array.Copy(hash, 0, rawKey, 0, 32);
					//Array.Copy(hash, 16, this.Counter, 0, 16);
					//Array.Copy(hash, 24, this.Counter, 0, 16);
					//Array.Copy(hash, 32, this.Counter, 0, 16);

					this.Aes = new AES(rawKey);
				}
			}

			public byte[] GetBlock()
			{
				this.Aes.EncryptBlock(this.Counter, this.Block);

				for (int index = 0; index < 16; index++)
				{
					if (this.Counter[index] < 0xff)
					{
						this.Counter[index]++;
						break;
					}
					this.Counter[index] = 0x00;
				}
				return this.Block;
			}

			public void Dispose()
			{
				if (this.Aes != null)
				{
					this.Aes.Dispose();
					this.Aes = null;
				}
			}
		}

		public class RNGRandomNumberGenerator : RandomUnit.IRandomNumberGenerator
		{
			private RandomNumberGenerator Rng = new RNGCryptoServiceProvider();
			private byte[] Cache = new byte[4096];

			public byte[] GetBlock()
			{
				this.Rng.GetBytes(this.Cache);
				return this.Cache;
			}

			public void Dispose()
			{
				if (this.Rng != null)
				{
					this.Rng.Dispose();
					this.Rng = null;
				}
			}
		}

		public static byte[] GetSHA512(byte[] src)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return sha512.ComputeHash(src);
			}
		}

		public static byte[] GetSHA512File(string file)
		{
			using (SHA512 sha512 = SHA512.Create())
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return sha512.ComputeHash(reader);
			}
		}

		public static byte[] GetMD5(byte[] src)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				return md5.ComputeHash(src);
			}
		}

		public static byte[] GetMD5File(string file)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return md5.ComputeHash(reader);
			}
		}

		public static string ToFiarIdent(string ident)
		{
			if (IsFiarIdent(ident) == false)
				ident = BinTools.Hex.ToString(BinTools.GetSubBytes(GetSHA512(Encoding.UTF8.GetBytes(ident)), 0, 16));

			return ident;
		}

		public static bool IsFiarIdent(string ident)
		{
			string fmt = ident;

			fmt = StringTools.ReplaceChars(fmt, StringTools.DECIMAL + StringTools.alpha + "-{}", '9');
			fmt = StringTools.ReplaceLoop(fmt, "99", "9");

			return fmt == "9" && ident.Length <= 38;
		}
	}
}
