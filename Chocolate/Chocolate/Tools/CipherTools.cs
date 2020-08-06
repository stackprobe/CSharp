using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	public static class CipherTools
	{
		public interface IBlockCipher : IDisposable
		{
			void EncryptBlock(byte[] src, byte[] dest);
			void DecryptBlock(byte[] src, byte[] dest);
		}

		public class AES : IBlockCipher
		{
			private AesManaged Aes;
			private ICryptoTransform Encryptor = null;
			private ICryptoTransform Decryptor = null;

			public AES(byte[] rawKey)
			{
				HandleDam.Transaction(hDam =>
				{
					if (
						rawKey.Length != 16 &&
						rawKey.Length != 24 &&
						rawKey.Length != 32
						)
						throw new ArgumentException();

					this.Aes = hDam.Add(new AesManaged());
					this.Aes.KeySize = rawKey.Length * 8;
					this.Aes.BlockSize = 128;
					this.Aes.Mode = CipherMode.ECB;
					this.Aes.IV = new byte[16]; // dummy
					this.Aes.Key = rawKey;
					this.Aes.Padding = PaddingMode.None;
				});
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
					ExceptionDam.Section(eDam =>
					{
						eDam.Dispose(ref this.Encryptor);
						eDam.Dispose(ref this.Decryptor);
						eDam.Dispose(ref this.Aes);
					});
				}
			}
		}
	}
}
