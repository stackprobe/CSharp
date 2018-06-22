using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Test01.Modules
{
	public class RandomDataFileTools
	{
		public static void Make(string file, long size, ulong seed, bool includeHash)
		{
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
				for (long index = 0; index < size; index++)
				{
					writer.WriteByte(s.NextByte());
				}
			}
			if (includeHash)
			{
				using (SHA512 sha512 = SHA512.Create())
				{
					byte[] hash;

					using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
					{
						hash = sha512.ComputeHash(reader);
					}
					using (FileStream writer = new FileStream(file, FileMode.Append, FileAccess.Write))
					{
						writer.Write(hash, 0, 16);
					}
				}
			}
		}

		public static bool CheckHash(string file)
		{
			throw null;
		}

		public class XorShift128
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

			public int NB_Count = 0;
			public uint NB_Value;

			public byte NextByte()
			{
				if (this.NB_Count == 0)
				{
					this.NB_Count = 3;
					this.NB_Value = this.Next();
				}
				else
				{
					this.NB_Count--;
					this.NB_Value >>= 8;
				}
				return (byte)(this.NB_Value & 0xff);
			}
		}
	}
}
