﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Charlotte.Tools
{
	public static class SecurityTools
	{
		private static object GCRB_SYNCROOT = new object();
		private const int GCRB_BUFF_SIZE = 1024;
		private static byte[] _gcrbBuff;
		private static int _gcrbIndex = GCRB_BUFF_SIZE;

		public static byte getCRandByte()
		{
			//lock (GCRB_SYNCROOT)
			{
				if (GCRB_BUFF_SIZE <= _gcrbIndex)
				{
					_gcrbBuff = getCRand(GCRB_BUFF_SIZE);
					_gcrbIndex = 0;
				}
				return _gcrbBuff[_gcrbIndex++];
			}
		}

		private static RandomNumberGenerator _cRandom = RNGCryptoServiceProvider.Create();

		/// <summary>
		/// thread safe
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static byte[] getCRand(int size)
		{
			byte[] dest = new byte[size];
			_cRandom.GetBytes(dest); // thread safe らしい。https://msdn.microsoft.com/ja-jp/library/wb9c8c67(v=vs.110).aspx
			return dest;
		}

		public static byte[] getSHA512(byte[] src)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return sha512.ComputeHash(src);
			}
		}

		public static byte[] getSHA512File(string file)
		{
			using (SHA512 sha512 = SHA512.Create())
			using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return sha512.ComputeHash(fs);
			}
		}

		public static uint getCRandUInt()
		{
			return
				((uint)getCRandByte() << 24) |
				((uint)getCRandByte() << 16) |
				((uint)getCRandByte() << 8) |
				(uint)getCRandByte();
		}

		public static UInt64 getCRandUInt64()
		{
			return
				((UInt64)getCRandByte() << 56) |
				((UInt64)getCRandByte() << 48) |
				((UInt64)getCRandByte() << 40) |
				((UInt64)getCRandByte() << 32) |
				((UInt64)getCRandByte() << 24) |
				((UInt64)getCRandByte() << 16) |
				((UInt64)getCRandByte() << 8) |
				(UInt64)getCRandByte();
		}

		public static uint getCRandUInt(uint modulo)
		{
			if (modulo == 0u)
				throw new ArgumentException("modulo is zero");

			return (uint)(getCRandUInt64() % modulo); // zantei
		}

		public static string makePassword(int len = 22, string chrs = StringTools.DIGIT + StringTools.ALPHA + StringTools.alpha)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < len; index++)
				buff.Append(chrs[(int)getCRandUInt((uint)chrs.Length)]);

			return buff.ToString();
		}
	}
}
