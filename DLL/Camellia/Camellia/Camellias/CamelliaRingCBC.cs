using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Camellias.openCrypto;
using Charlotte.Tools;

namespace Charlotte.Camellias
{
	public class CamelliaRingCBC
	{
		private CamelliaTransformLE[] Transforms;

		public CamelliaRingCBC(byte[] rawKey)
		{
			this.Transforms = CreateTransforms(rawKey);
		}

		private static CamelliaTransformLE[] CreateTransforms(byte[] rawKey)
		{
			if (rawKey.Length < 16)
				throw new ArgumentException("短すぎる鍵");

			if (rawKey.Length == 32)
				return CreateTransforms(rawKey, 16);

			if (rawKey.Length % 32 == 0)
				return CreateTransforms(rawKey, 32);

			if (rawKey.Length == 24)
				throw new ArgumentException("鍵が1つしかない。");

			if (rawKey.Length % 24 == 0)
				return CreateTransforms(rawKey, 24);

			if (rawKey.Length == 16)
				throw new ArgumentException("鍵が1つしかない。");

			if (rawKey.Length % 16 == 0)
				return CreateTransforms(rawKey, 16);

			throw new ArgumentException("鍵の長さに問題があります。");
		}

		private static CamelliaTransformLE[] CreateTransforms(byte[] rawKey, int keyWidth)
		{
			int deep = rawKey.Length / keyWidth;
			CamelliaTransformLE[] transforms = new CamelliaTransformLE[deep];

			for (int index = 0; index < deep; index++)
				transforms[index] = new CamelliaTransformLE(new CamelliaManaged(), BinTools.GetSubBytes(rawKey, index * keyWidth, keyWidth), new byte[0], true);

			return transforms;
		}

		public void Encrypt(byte[] data, int offset, int size)
		{
			if (
				offset < 0 ||
				data.Length < offset ||
				data.Length - offset < size ||
				size < 32 ||
				size % 16 != 0
				)
				throw new ArgumentException();

			foreach (CamelliaTransformLE transform in this.Transforms)
				transform.EncryptRingCBC(data, offset, size / 16);
		}

		public void Decrypt(byte[] data, int offset, int size)
		{
			if (
				offset < 0 ||
				data.Length < offset ||
				data.Length - offset < size ||
				size < 32 ||
				size % 16 != 0
				)
				throw new ArgumentException();

			for (int index = this.Transforms.Length - 1; 0 <= index; index--)
				this.Transforms[index].DecryptRingCBC(data, offset, size / 16);
		}
	}
}
