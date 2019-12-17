using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Camellias.openCrypto;

namespace Charlotte.Camellias
{
	public class Camellia : Charlotte.Tools.CipherTools.IBlockCipher
	{
		private CamelliaTransformLE Transform;

		public Camellia(byte[] rawKey)
		{
			if (
				rawKey.Length != 16 &&
				rawKey.Length != 24 &&
				rawKey.Length != 32
				)
				throw new ArgumentException();

			this.Transform = new CamelliaTransformLE(new CamelliaManaged(), rawKey, new byte[0], true);
		}

		public void EncryptBlock(byte[] src, byte[] dest)
		{
			if (
				src.Length != 16 ||
				dest.Length != 16
				)
				throw new ArgumentException();

			this.Transform.EncryptECB(src, 0, dest, 0);
		}

		public void DecryptBlock(byte[] src, byte[] dest)
		{
			if (
				src.Length != 16 ||
				dest.Length != 16
				)
				throw new ArgumentException();

			this.Transform.DecryptECB(src, 0, dest, 0);
		}

		public void Dispose()
		{
			if (this.Transform != null)
			{
				this.Transform.Dispose();
				this.Transform = null;
			}
		}
	}
}
