﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Satellite
{
	public class FileTools
	{
		public static void CreateFile(string file)
		{
			File.WriteAllBytes(file, new byte[0]);
		}

		public static void CreateDir(string dir)
		{
			Directory.CreateDirectory(dir);
		}

		public static bool ExistFile(string file)
		{
			return File.Exists(file);
		}

		public static bool ExistDir(string dir)
		{
			return Directory.Exists(dir);
		}

		public static void DeleteFile(string file)
		{
			File.Delete(file);
		}

		public static void DeleteDir(string dir, bool recursive = false)
		{
			if (recursive == false && IsEmptyDir(dir) == false)
				return;

			Directory.Delete(dir, recursive);
		}

		public static bool IsEmptyDir(string dir)
		{
			return
				Directory.GetDirectories(dir).Length == 0 &&
				Directory.GetFiles(dir).Length == 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dir"></param>
		/// <returns>dir 直下のディレクトリとファイルのローカル名の一覧</returns>
		public static List<string> List(string dir)
		{
			List<string> list = new List<string>();

			foreach (string[] paths in new string[][] { Directory.GetDirectories(dir), Directory.GetFiles(dir) })
				foreach (string path in paths)
					list.Add(Path.GetFileName(path));

			return list;
		}

		public static void WriteAllBytes(string file, QueueData<SubBlock> fileData)
		{
			using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
			{
				for (; ; )
				{
					SubBlock block = fileData.Poll(null);

					if (block == null)
						break;

					fs.Write(block.Block, block.StartPos, block.Length);
				}
			}
		}
	}
}
