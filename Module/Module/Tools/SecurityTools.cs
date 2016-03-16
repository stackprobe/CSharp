using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	public class SecurityTools
	{
		public static string GetSHA512_128String(byte[] block)
		{
			return GetSHA512String(block).Substring(0, 32);
		}

		public static string GetSHA512String(byte[] block)
		{
			return StringTools.ToHex(SHA512.Create().ComputeHash(block));
		}
	}
}
