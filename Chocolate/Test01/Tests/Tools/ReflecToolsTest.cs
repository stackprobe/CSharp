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
			foreach (ReflecTools.MethodBox method in ReflecTools.GetMethods(new Sample3()))
			{
				//Console.WriteLine(JsonTools.Encode(DebugTools.ToListOrMap(method.Value, 1)));
				Console.WriteLine(method.Value.Name);
				Console.WriteLine("IsStatic: " + method.Value.IsStatic);

				Console.WriteLine("Parameters:");
				foreach (ReflecTools.ParameterBox param in method.GetParameters())
				{
					Console.WriteLine("\t" + param.Value.ParameterType.Name + " " + param.Value.Name);
				}
			}
		}

		public void Test02()
		{
			foreach (ReflecTools.MethodBox constructor in ReflecTools.GetConstructors(new Sample3()))
			{
				//Console.WriteLine(JsonTools.Encode(DebugTools.ToListOrMap(constructor.Value, 1)));

				Console.WriteLine("Parameters:");
				foreach (ReflecTools.ParameterBox param in constructor.GetParameters())
				{
					Console.WriteLine("\t" + param.Value.ParameterType.Name + " " + param.Value.Name);
				}
			}
		}

		public void Test02b()
		{
			foreach (ReflecTools.MethodBox constructor in ReflecTools.GetConstructors(new Sample4(0.0))) // Sample3のコンストラクタは見えない。
			{
				//Console.WriteLine(JsonTools.Encode(DebugTools.ToListOrMap(constructor.Value, 1)));

				Console.WriteLine("Parameters:");
				foreach (ReflecTools.ParameterBox param in constructor.GetParameters())
				{
					Console.WriteLine("\t" + param.Value.ParameterType.Name + " " + param.Value.Name);
				}
			}
		}

		public void Test03()
		{
			ReflecTools.MethodBox ctor = ReflecTools.GetConstructorsByType(Type.GetType("Charlotte.Tests2.Sample4"))[0];
			Sample4 s;

			//s = (Sample4)ctor.Value.Invoke(null, new object[] { 0.0 }); // 例外
			s = (Sample4)((ConstructorInfo)ctor.Value).Invoke(new object[] { 0.0 });

			s.PrintOK();
		}

		public void Test03b()
		{
			ReflecTools.MethodBox ctor = ReflecTools.GetConstructorsByType(Type.GetType("Charlotte.Tests2.Sample4"))[0];
			Sample4 s;

			s = (Sample4)ctor.Construct(new object[] { -1.0 });

			s.PrintOK();

			// ----

			{
				ReflecTools.MethodBox method = ReflecTools.GetMethodsByType(s.GetType()).Where(m => m.Value.Name == "PrintOK").ToArray()[0];

				method.Value.Invoke(s, new object[] { }); // == s.PrintOK();
			}

			// ----

			foreach (ReflecTools.MethodBox method in ReflecTools.GetMethodsByType(s.GetType()))
			{
				Console.WriteLine("\t" + method.Value.Name);
			}

			// ----

			{
				ReflecTools.MethodBox method = ReflecTools.GetMethodsByType(s.GetType()).Where(m => m.Value.Name == "SPrintString").ToArray()[0];

				method.Value.Invoke(null, new object[] { "ABC" }); // == Sample3.SPrintString("ABC");
			}
		}
	}
}
