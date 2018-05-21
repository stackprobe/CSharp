using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class Hub
	{
		private Func<string, Type> TypeGetter;

		/// <summary>
		/// Hub hub = new Hub(typeName => Type.GetType(typeName));
		/// </summary>
		/// <param name="typeGetter"></param>
		public Hub(Func<string, Type> typeGetter)
		{
			// test
			//Console.WriteLine(Type.GetType("Charlotte.Tests2.Sample4")); // 見えない。
			//Console.WriteLine(Type.GetType("Charlotte.Tests2.Sample4,Test01")); // 見えるじゃん... orz

#if true
			this.TypeGetter = typeName =>
			{
				Type type = typeGetter(typeName);

				if (type == null)
				{
					type = typeGetter("Charlotte." + typeName);

					if (type == null)
						throw new Exception("指定されたタイプが見つかりません。" + typeName);
				}
				return type;
			};
#elif true
			this.TypeGetter = typeName =>
			{
				Type type = typeGetter(typeName);

				if (type == null)
					throw new Exception("指定されたタイプが見つかりません。" + typeName);

				return type;
			};
#else
			this.TypeGetter = typeGetter;
#endif
		}

		private ObjectMap Vars = ObjectMap.CreateIgnoreCase();

		public void Perform(ArgsReader ar)
		{
			do
			{
				string option1 = ar.NextArg();

				if (ar.ArgIs("="))
				{
					string destVarName = option1;

					if (ar.ArgIs("new")) // <変数> = new <タイプ> <引数>... ;
					{
						string typeName = ar.NextArg();
						object[] prms = ReadParams(ar);

						this.Vars[destVarName] = ReflecTools.GetConstructors(this.TypeGetter(typeName)).Where(ctor => IsMatchParams(ctor, prms)).ToArray()[0].Construct(prms);
					}
					else
					{
						string option2 = ar.NextArg();

						if (ar.ArgIs(".")) // <変数> = <変数> . <メソッド> <引数>... ;
						{
							string varName = option2;
							string methodName = ar.NextArg();
							object[] prms = ReadParams(ar);

							object instance = this.Vars[varName];

							this.Vars[destVarName] = ReflecTools.GetMethodsByInstance(instance).Where(method => method.Value.IsStatic == false && IsMatchParams(method, prms)).ToArray()[0].Invoke(instance, prms);
						}
						else // <変数> = <タイプ> <staticメソッド> <引数>... ;
						{
							string typeName = option2;
							string methodName = ar.NextArg();
							object[] prms = ReadParams(ar);

							this.Vars[destVarName] = ReflecTools.GetMethods(this.TypeGetter(typeName)).Where(method => method.Value.IsStatic && IsMatchParams(method, prms)).ToArray()[0].Invoke(prms);
						}
					}
				}
				else if (ar.ArgIs(".")) // <変数> . <メソッド> <引数>... ;
				{
					string varName = option1;
					string methodName = ar.NextArg();
					object[] prms = ReadParams(ar);

					object instance = this.Vars[varName];

					ReflecTools.GetMethodsByInstance(instance).Where(method => method.Value.IsStatic == false && IsMatchParams(method, prms)).ToArray()[0].Invoke(instance, prms);
				}
				else // <タイプ> <staticメソッド> <引数>... ;
				{
					string typeName = option1;
					string methodName = ar.NextArg();
					object[] prms = ReadParams(ar);

					ReflecTools.GetMethods(this.TypeGetter(typeName)).Where(method => method.Value.IsStatic && IsMatchParams(method, prms)).ToArray()[0].Invoke(prms);
				}
			}
			while (ar.HasArgs());
		}

		private static object[] ReadParams(ArgsReader ar)
		{
			List<object> prms = new List<object>();

			while (ar.HasArgs() && ar.ArgIs(";") == false)
			{
				string prm = ar.NextArg();

				if (prm.StartsWith(";"))
					prm = prm.Substring(1);

				prms.Add(prm);
			}
			return prms.ToArray();
		}

		private bool IsMatchParams(ReflecTools.MethodBox method, object[] prms)
		{
			return method.GetParameters().Length == prms.Length;
		}
	}
}
