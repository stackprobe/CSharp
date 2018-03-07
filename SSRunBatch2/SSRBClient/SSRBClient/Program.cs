using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				OnBoot();

				Gnd.I = new Gnd();

				WorkingDir.Root = WorkingDir.CreateProcessRoot();

				try
				{
					if (1 <= args.Length && args[0].ToUpper() == "//R")
					{
						Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
					}
					else
					{
						Main2(args);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				WorkingDir.Root.Dispose();
				WorkingDir.Root = null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public static void PostMessage(object message)
		{
			Console.WriteLine("[TRACE] " + message);
		}

		public const string APP_IDENT = "{4240335b-267a-4dad-aa92-7d81bfd1369a}";
		public const string APP_TITLE = "SSRBClient";

		public static string SelfFile;
		public static string SelfDir;

		public static void OnBoot()
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);
		}

		private static void Main2(string[] args)
		{
			ArgsReader ar = new ArgsReader(args);

			ar.Add("/S", () => Gnd.I.SendFiles.Add(ar.NextArg()));
			ar.Add("/R", () => Gnd.I.RecvFiles.Add(ar.NextArg()));
			ar.Add("/C", () => Gnd.I.Commands.Add(ar.NextArg()));
			ar.Add("/B", () => Gnd.I.Commands.AddRange(File.ReadAllLines(ar.NextArg(), StringTools.ENCODING_SJIS)));
			ar.Add("/O", () => Gnd.I.OutLinesFile = ar.NextArg());

			ar.Perform();

			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				BatchClient client = new BatchClient()
				{
					Domain = ar.GetArg(0),
					PortNo = ar.HasArgs(2) ? int.Parse(ar.GetArg(1)) : Consts.DEF_PORT_NO,
					SendFiles = Gnd.I.SendFiles.ToArray(),
					RecvFiles = Gnd.I.RecvFiles.ToArray(),
					Commands = Gnd.I.Commands.ToArray(),
					OutLinesFile = wd.MakePath(),
				};

				client.Perform();

				if (Gnd.I.OutLinesFile == null)
				{
					using (StreamReader reader = new StreamReader(client.OutLinesFile, StringTools.ENCODING_SJIS))
					{
						for (; ; )
						{
							string line = reader.ReadLine();

							if (line == null)
								break;

							Console.WriteLine(line);
						}
					}
				}
				else
				{
					File.Delete(Gnd.I.OutLinesFile);
					File.Move(client.OutLinesFile, Gnd.I.OutLinesFile);
				}
			}
		}
	}
}
