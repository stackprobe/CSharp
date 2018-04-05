using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Tools
{
	public class FileToolsTest
	{
		public void Test01()
		{
			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				string dir = wd.MakePath();

				DebugPrintTree(wd.GetPath("."));

				MakeRandDir(dir, 3);

				DebugPrintTree(wd.GetPath("."));

				FileTools.CleanupDir(dir);

				DebugPrintTree(wd.GetPath("."));
			}
		}

		private static void MakeRandDir(string dir, int depth)
		{
			FileTools.CreateDir(dir);

			for (int c = (int)SecurityTools.CRandom.GetRandom(20); 0 < c; c--)
			{
				string lPath = SecurityTools.MakePassword();
				string path = Path.Combine(dir, lPath);

				if (SecurityTools.CRandom.GetRandom(2) == 1u && 1 <= depth)
				{
					MakeRandDir(path, depth - 1);
				}
				else
				{
					File.WriteAllBytes(path, SecurityTools.CRandom.GetBytes((int)SecurityTools.CRandom.GetRandom(1000)));
				}
			}
		}

		private static void DebugPrintTree(string dir)
		{
			foreach (string line in ProcessTools.Batch(new string[] { "TREE /F " + dir }))
			{
				Console.WriteLine(line);
			}
		}

		public void Test02()
		{
			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				string dir = wd.MakePath();

				MakeRandDir(dir, 3);

				string[] tree1 = DebugGetTree(dir);

				FileTools.CleanupDir(dir);

				string[] tree2 = DebugGetTree(dir);

				if (tree1.Length < 2)
					throw null;

				if (tree2.Length != 2)
					throw null;
			}
			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				string dir1 = wd.MakePath();
				string dir2 = wd.MakePath();

				MakeRandDir(dir1, 3);

				string[] tree1_1 = DebugGetTree(dir1);
				string[] tree1_2 = DebugGetTree(dir2);

				FileTools.CopyDir(dir1, dir2);

				string[] tree2_1 = DebugGetTree(dir1);
				string[] tree2_2 = DebugGetTree(dir2);

				if (ArrayTools.Comp<string>(tree1_1, tree2_1, StringTools.CompIgnoreCase) != 0)
					throw null;

				if (ArrayTools.Comp<string>(tree1_1, tree2_2, StringTools.CompIgnoreCase) != 0)
					throw null;

				if (tree1_2.Length != 0)
					throw null;
			}
			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				string dir1 = wd.MakePath();
				string dir2 = wd.MakePath();

				MakeRandDir(dir1, 3);

				string[] tree1_1 = DebugGetTree(dir1);
				string[] tree1_2 = DebugGetTree(dir2);

				FileTools.MoveDir(dir1, dir2);

				string[] tree2_1 = DebugGetTree(dir1);
				string[] tree2_2 = DebugGetTree(dir2);

				if (ArrayTools.Comp<string>(tree1_1, tree2_2, StringTools.CompIgnoreCase) != 0)
					throw null;

				if (tree1_2.Length != 0)
					throw null;

				if (tree2_1.Length != 0)
					throw null;
			}
		}

		private static string[] DebugGetTree(string dir)
		{
			List<string> dest = new List<string>();

			if (Directory.Exists(dir))
			{
				DebugGetTree_Main(dir, dest);
			}
			return dest.ToArray();
		}

		private static void DebugGetTree_Main(string dir, List<string> dest)
		{
			dest.Add("B"); // Begin

			foreach (string subDir in Directory.GetDirectories(dir))
			{
				dest.Add("D " + Path.GetFileName(subDir));

				DebugGetTree_Main(subDir, dest);
			}
			foreach (string subFile in Directory.GetFiles(dir))
			{
				dest.Add("F " + Path.GetFileName(subFile));
			}
			dest.Add("E"); // End
		}
	}
}
