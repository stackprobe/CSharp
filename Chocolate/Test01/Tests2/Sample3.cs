using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests2
{
	public class Sample3
	{
		public Sample3()
		{
			// noop
		}

		public Sample3(string a)
		{
			// noop
		}

		public Sample3(string a, string b, string c)
		{
			// noop
		}

		public Sample3(int a)
		{
			// noop
		}

		public Sample3(int a, int b, int c)
		{
			// noop
		}

		public Sample3(string str, int val)
		{
			// noop
		}

		public Sample3(int val, string str)
		{
			// noop
		}

		public void Test01()
		{
			// noop
		}

		public void Test01(string a)
		{
			// noop
		}

		public int Test01(int val)
		{
			return -1; // noop
		}

		public int Test02(int val)
		{
			return -1; // noop
		}

		public int Test03(int val)
		{
			return -1; // noop
		}

		public static void STest01()
		{
			// noop
		}

		public static void STest01(string a)
		{
			// noop
		}

		public static void STest02(string a)
		{
			// noop
		}

		public static void SPrintString(string str)
		{
			Console.WriteLine("Sample3.SPrintString: " + str);
		}
	}
}
