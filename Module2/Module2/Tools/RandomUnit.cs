using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	public class RandomUnit
	{
		public interface IRandomNumberGenerator
		{
			byte[] GetBlock();
		}

		private IRandomNumberGenerator Rng;

		public RandomUnit(IRandomNumberGenerator rng)
		{
			this.Rng = rng;
		}

		private static byte[] EMPTY_BYTES = new byte[0];
		private byte[] Cache = EMPTY_BYTES;
		private int RIndex = 0;

		public byte GetByte()
		{
			if (this.Cache.Length <= this.RIndex)
			{
				this.Cache = this.Rng.GetBlock();
				this.RIndex = 0;
			}
			return this.Cache[this.RIndex++];
		}

		public byte[] GetBytes(int length)
		{
			byte[] dest = new byte[length];

			for (int index = 0; index < length; index++)
				dest[index] = this.GetByte();

			return dest;
		}

		public uint GetUInt16()
		{
			return
				((uint)this.GetByte() << 8) |
				((uint)this.GetByte() << 0);
		}

		public uint GetUInt()
		{
			return
				(this.GetUInt16() << 16) |
				(this.GetUInt16() << 0);
		}

		public ulong GetUInt64()
		{
			return
				((ulong)this.GetUInt() << 32) |
				((ulong)this.GetUInt() << 0);
		}

		public ulong GetRandom64(ulong modulo)
		{
			if (modulo == 0UL)
				throw new ArgumentOutOfRangeException("modulo == 0");

			if (modulo == 1UL)
				return 0UL;

			ulong r_mod = (ulong.MaxValue % modulo + 1UL) % modulo;
			ulong r;

			do
			{
				r = this.GetUInt64();
			}
			while (r < r_mod);

			r %= modulo;

			return r;
		}

		public uint GetRandom(uint modulo)
		{
			return (uint)this.GetRandom64((ulong)modulo);
		}

		public long GetRange64(long minval, long maxval)
		{
			return (long)this.GetRandom64((ulong)(maxval + 1L - minval)) + minval;
		}

		public int GetRange(int minval, int maxval)
		{
			return (int)this.GetRandom((uint)(maxval + 1 - minval)) + minval;
		}

		public long GetInt64(long modulo)
		{
			return (long)this.GetRandom64((ulong)modulo);
		}

		public int GetInt(int modulo)
		{
			return (int)this.GetRandom((uint)modulo);
		}
	}
}
