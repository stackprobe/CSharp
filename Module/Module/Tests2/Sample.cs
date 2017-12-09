using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests2
{
	public class Sample
	{
		public int PublicInt = 1;
		private int PrivateInt = 2;
		public static int PublicStaticInt = 3;
		private static int PrivateStaticInt = 4;

		public string Str = "Hogein";
		public string Str_null = null;

		public int[] IntArray = new int[] { 5, 6, 7 };
		public int[] IntArray_null = null;

		public string[] StrArray = new string[] { "Senda", "32o", null, null };
		public string[] StrArray_null = null;

		public List<int> IntList = ArrayTools.toList<int>(new int[] { 8, 9, 10 });
		public List<int> IntList_null = null;

		public List<string> StrList = ArrayTools.toList<string>(new string[] { "AAA", null, null, "DDD", "EEE" });
		public List<string> StrList_null = null;

		public string StrProp
		{
			get
			{
				return "StrProp_123";
			}
			set
			{
				// noop
			}
		}

		public string StrPropGetOnly
		{
			get
			{
				return "StrPropGetOnly_123";
			}
		}

		public string StrPropSetOnly
		{
			set
			{
				// noop
			}
		}

		public Sample Self;
		public Sample Self_null = null;

		public Sample[] SelfArray;
		public Sample[] SelfArray_null = null;

		public List<Sample> SelfList;
		public List<Sample> SelfList_null = null;

		public class InnerClass
		{
			public int InnerInt = 123;
		}

		public InnerClass InnerCls = new InnerClass();
		public InnerClass InnerCls_null = null;

		public Sample SubClass;
		public Sample SubClass_null = null;

		public Sample2 SubClass2;
		public Sample2 SubClass2_null = null;

		public Sample()
		{
			this.Self = this;
			this.SelfArray = new Sample[] { this, null };
			this.SelfList = ArrayTools.toList<Sample>(new Sample[] { this, null });
		}

		public void Dummy_0001()
		{
			Console.WriteLine("" + this.PrivateInt);
			Console.WriteLine("" + PrivateStaticInt);
		}
	}
}
