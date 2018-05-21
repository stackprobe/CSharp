﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class Hub
	{
		public static Type GetType(string typeName)
		{
			foreach (string prefix in new string[] { "", "Charlotte." })
			{
				foreach (string suffix in new string[] { "," + System.Reflection.Assembly.GetEntryAssembly().GetName().Name, "" })
				{
					Type type = Type.GetType(prefix + typeName + suffix);

					if (type != null)
						return type;
				}
			}
			throw new Exception("指定されたタイプは見つかりません。" + typeName);
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
						object[] prms = this.ReadParams(ar);

						this.Vars[destVarName] = ReflecTools.GetConstructors(GetType(typeName)).Where(ctor => IsMatchParams(ctor, prms)).ToArray()[0].Construct(prms);
					}
					else
					{
						string option2 = ar.NextArg();

						if (ar.ArgIs(".")) // <変数> = <変数> . <メソッド> <引数>... ;
						{
							string varName = option2;
							string methodName = ar.NextArg();
							object[] prms = this.ReadParams(ar);

							object instance = this.Vars[varName];

							this.Vars[destVarName] = ReflecTools.GetMethodsByInstance(instance).Where(method =>
								method.Value.Name == methodName &&
								method.Value.IsStatic == false &&
								IsMatchParams(method, prms)
								).ToArray()[0].Invoke(instance, prms);
						}
						else // <変数> = <タイプ> <staticメソッド> <引数>... ;
						{
							string typeName = option2;
							string methodName = ar.NextArg();
							object[] prms = this.ReadParams(ar);

							this.Vars[destVarName] = ReflecTools.GetMethods(GetType(typeName)).Where(method =>
								method.Value.Name == methodName &&
								method.Value.IsStatic &&
								IsMatchParams(method, prms)
								).ToArray()[0].Invoke(prms);
						}
					}
				}
				else if (ar.ArgIs(".")) // <変数> . <メソッド> <引数>... ;
				{
					string varName = option1;
					string methodName = ar.NextArg();
					object[] prms = this.ReadParams(ar);

					object instance = this.Vars[varName];

					ReflecTools.GetMethodsByInstance(instance).Where(method =>
						method.Value.Name == methodName &&
						method.Value.IsStatic == false &&
						IsMatchParams(method, prms)
						).ToArray()[0].Invoke(instance, prms);
				}
				else // <タイプ> <staticメソッド> <引数>... ;
				{
					string typeName = option1;
					string methodName = ar.NextArg();
					object[] prms = this.ReadParams(ar);

					ReflecTools.GetMethods(GetType(typeName)).Where(method =>
						method.Value.Name == methodName &&
						method.Value.IsStatic &&
						IsMatchParams(method, prms)
						).ToArray()[0].Invoke(prms);
				}
			}
			while (ar.HasArgs());
		}

		private object[] ReadParams(ArgsReader ar)
		{
			List<object> prms = new List<object>();

			while (ar.HasArgs() && ar.ArgIs(";") == false)
			{
				string prm = ar.NextArg();

				if (prm.StartsWith(";"))
					prm = prm.Substring(1);

				if (this.Vars.ContainsKey(prm))
					prms.Add(this.Vars[prm]);
				else
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
