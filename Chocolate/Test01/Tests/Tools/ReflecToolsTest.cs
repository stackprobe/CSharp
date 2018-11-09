using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests2;
using System.Reflection;

namespace Charlotte.Tests.Tools
{
	public class ReflecToolsTest
	{
		public void Test01()
		{
			foreach (ReflectTools.MethodBox method in ReflectTools.GetMethodsByInstance(new Sample3()))
			{
				//Console.WriteLine(JsonTools.Encode(DebugTools.ToListOrMap(method.Value, 1)));
				Console.WriteLine(method.Value.Name);
				Console.WriteLine("IsStatic: " + method.Value.IsStatic);

				Console.WriteLine("Parameters:");
				foreach (ReflectTools.ParameterBox param in method.GetParameters())
				{
					Console.WriteLine("\t" + param.Value.ParameterType.Name + " " + param.Value.Name);
				}
			}
		}

		public void Test02()
		{
			foreach (ReflectTools.MethodBox constructor in ReflectTools.GetConstructorsByInstance(new Sample3()))
			{
				//Console.WriteLine(JsonTools.Encode(DebugTools.ToListOrMap(constructor.Value, 1)));

				Console.WriteLine("Parameters:");
				foreach (ReflectTools.ParameterBox param in constructor.GetParameters())
				{
					Console.WriteLine("\t" + param.Value.ParameterType.Name + " " + param.Value.Name);
				}
			}
		}

		public void Test02b()
		{
			foreach (ReflectTools.MethodBox constructor in ReflectTools.GetConstructorsByInstance(new Sample4(0.0))) // Sample3のコンストラクタは見えない。
			{
				//Console.WriteLine(JsonTools.Encode(DebugTools.ToListOrMap(constructor.Value, 1)));

				Console.WriteLine("Parameters:");
				foreach (ReflectTools.ParameterBox param in constructor.GetParameters())
				{
					Console.WriteLine("\t" + param.Value.ParameterType.Name + " " + param.Value.Name);
				}
			}
		}

		public void Test03()
		{
			ReflectTools.MethodBox ctor = ReflectTools.GetConstructors(Type.GetType("Charlotte.Tests2.Sample4"))[0];
			Sample4 s;

			//s = (Sample4)ctor.Value.Invoke(null, new object[] { 0.0 }); // 例外
			s = (Sample4)((ConstructorInfo)ctor.Value).Invoke(new object[] { 0.0 });

			s.PrintOK();
		}

		public void Test03b()
		{
			ReflectTools.MethodBox ctor = ReflectTools.GetConstructors(Type.GetType("Charlotte.Tests2.Sample4"))[0];
			Sample4 s;

			s = (Sample4)ctor.Construct(new object[] { -1.0 });

			s.PrintOK();

			// ----

			{
				ReflectTools.MethodBox method = ReflectTools.GetMethods(s.GetType()).Where(m => m.Value.Name == "PrintOK").ToArray()[0];

				method.Invoke(s, new object[] { }); // == s.PrintOK();
			}

			// ----

			foreach (ReflectTools.MethodBox method in ReflectTools.GetMethods(s.GetType()))
			{
				Console.WriteLine("\t" + method.Value.Name);
			}

			// ----

			{
				ReflectTools.MethodBox method = ReflectTools.GetMethods(s.GetType()).Where(m => m.Value.Name == "SPrintString").ToArray()[0];

				method.Invoke(new object[] { "ABC" }); // == Sample3.SPrintString("ABC");
			}

			// ----

			Console.WriteLine("" + typeof(Sample4));
			Console.WriteLine("" + typeof(Sample4.InnerClass1));

			Console.WriteLine(Type.GetType("Charlotte.Tests2.Sample4"));
			Console.WriteLine(Type.GetType("Charlotte.Tests2.Sample4+InnerClass1"));

			ReflectTools.GetMethods(Type.GetType("Charlotte.Tests2.Sample4+InnerClass1")).Where(m => m.Value.Name == "Test01").ToArray()[0].Invoke(new object[0]);

			// ----

			Console.WriteLine(Type.GetType("Charlotte.Tools.XmlNode")); // <-- 見つからない。
			Console.WriteLine(Type.GetType("Charlotte.Tools.XmlNode,Chocolate")); // <-- 見つかる！

			// Chocolate.dll ...
			//Console.WriteLine(Type.GetType("Charlotte.Tests.Tools.ReflecToolsTest")); // <-- 見つからない。
			//Console.WriteLine(Type.GetType("Charlotte.Tests.Tools.ReflecToolsTest,Test01")); // <-- 見つかる！

			// ----

			{
				Hub hub = new Hub();

				// "Charlotte." は省略できる。
				hub.Perform(new ArgsReader("Tests2.Sample4+InnerClass1 Test01".Split(' ')));

#if false
				{
					string sArgs = @"
a = new Tools.XmlNode Root RootValue ;
a . WriteToFile C:\temp\ReflecToolsTest_Test03b_0001.xml
";
					// ファイル作成注意！！！

					hub.Perform(new ArgsReader(StringTools.Tokenize(sArgs, "\r\n ", false, true)));
				}
#endif

				{
					string sArgs = @"
a = new Tests2.Sample4+InnerClass1 ;
a . Test02 ;
b = a . GetSelf ;
b . Test02 ;
b = a . CreateInner2 ;
b . Test02 ;
b = b . ToString ;
Tests2.Sample3 SPrintString b ;
Tests2.Sample3 SPrintString *b ;
Tests2.Sample3 SPrintString **b ;
Tests2.Sample3 SPrintString **; ;
Tests2.Sample3 SPrintString *** ;
";
					hub.Perform(new ArgsReader(StringTools.Tokenize(sArgs, "\r\n ", false, true)));
				}
			}
		}

		public void Test04()
		{
			{
				ReflectTools.MethodBox method = ReflectTools.GetMethods(this.GetType()).Where(m => m.Value.Name == "Test04_Func01").ToArray()[0];

				foreach (ReflectTools.ParameterBox prm in method.GetParameters())
				{
					Console.WriteLine("" + prm.Value);
				}
			}

			{
				ReflectTools.MethodBox method = ReflectTools.GetMethods(this.GetType()).Where(m => m.Value.Name == "Test04_Func01b").ToArray()[0];

				foreach (ReflectTools.ParameterBox prm in method.GetParameters())
				{
					Console.WriteLine("" + prm.Value);
				}
			}

			{
				ReflectTools.MethodBox method = ReflectTools.GetMethods(this.GetType()).Where(m => m.Value.Name == "Test04_Func02").ToArray()[0];

				foreach (ReflectTools.ParameterBox prm in method.GetParameters())
				{
					Console.WriteLine("" + prm.Value);
				}
			}
		}

		public void Test04_Func01(string[] prms)
		{ }

		public void Test04_Func01b(params string[] prms)
		{ }

		public void Test04_Func02(string str, char chr, int i, long l, float f, double d)
		{ }
	}
}
