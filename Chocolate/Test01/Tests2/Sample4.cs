using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests2
{
	public class Sample4 : Sample3
	{
		private double Value;

		public Sample4(double a)
		{
			this.Value = a;
		}

		public void PrintOK()
		{
			Console.WriteLine("Sample4_OK " + this.Value);
		}

		public class InnerClass1
		{
			public static void Test01()
			{
				Console.WriteLine("Sample4.InnerClass1.Test01(static)_OK");
			}

			public void Test02()
			{
				Console.WriteLine("Sample4.InnerClass1.Test02(not_static)_OK");
			}

			public InnerClass1 GetSelf()
			{
				return this;
			}
		}
	}
}
