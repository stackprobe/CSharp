using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class CrossDictionaryTest
	{
		public void Test01()
		{
			CrossDictionary<string, string> cm = DictionaryTools.CreateCross();

			cm.Add("A", "123");
			cm.Add("B", "456");
			cm.Add("C", "789");

			DebugTools.MustThrow(() => cm.Add("B", "999"));
			DebugTools.MustThrow(() => cm.Add("Z", "456"));

			foreach (string k in cm.Keys())
				Console.WriteLine("k: " + k);

			foreach (string v in cm.Values())
				Console.WriteLine("v: " + v);

			foreach (string k in cm.Keys())
				Console.WriteLine("kv: " + k + " ---> " + cm[k]);

			foreach (string v in cm.Values())
				Console.WriteLine("vk: " + v + " ---> " + cm.GetKey(v));
		}
	}
}
