using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace SyncDir
{
	public class SyncDir
	{
		public void Main(string rRootDir, string wRootDir, string backupDir)
		{
			Logger.WriteLine("< " + rRootDir);
			Logger.WriteLine("> " + wRootDir);
			Logger.WriteLine("* " + backupDir);

			Directory.CreateDirectory(wRootDir);
			Directory.CreateDirectory(backupDir);

			if (Directory.Exists(rRootDir) == false)
				throw new Exception("コピー元ディレクトリが存在しません。");

			if (Directory.Exists(wRootDir) == false)
				throw new Exception("コピー先ディレクトリが存在しません。");

			if (Directory.Exists(backupDir) == false)
				throw new Exception("バックアップ先ディレクトリが存在しません。");

			this.CleanupDir(backupDir);
			this.Main2(rRootDir, wRootDir, backupDir);
			this.CleanupDir(backupDir);

			Logger.WriteLine("同期しました。");
		}

		private void Main2(string rRootDir, string wRootDir, string backupDir)
		{
			CollectPathInfo cpi = new CollectPathInfo()
			{
				RRootDir = rRootDir,
				WRootDir = wRootDir,
			};

			cpi.Main();

			#region MakeTasks

			this.Tasks.Clear(); // reset

			for (int index = cpi.WOPaths.Count - 1; 0 <= index; index--)
			{
				PathInfo info = cpi.WOPaths[index];

				if (info.Kind == PathInfo.Kind_e.DIR)
				{
					this.Tasks.Add(new Task()
					{
						Command = Task.Command_e.DELETE_DIR,
						Path = Path.Combine(wRootDir, info.Path),
					});
				}
				else // FILE
				{
					this.Tasks.Add(new Task()
					{
						Command = Task.Command_e.DELETE_FILE,
						Path = Path.Combine(wRootDir, info.Path),
						BackupFile = Path.Combine(backupDir, Path.GetFileName(info.Path) + "~D" + index),
					});
				}
			}
			for (int index = 0; index < cpi.BEPaths.Count; index++)
			{
				PathInfo info = cpi.BEPaths[index];

				if (info.Kind == PathInfo.Kind_e.FILE)
				{
					string rFile = Path.Combine(rRootDir, info.Path);
					string wFile = Path.Combine(wRootDir, info.Path);

					if (this.IsFileChanged(rFile, wFile))
					{
						this.Tasks.Add(new Task()
						{
							Command = Task.Command_e.UPDATE_FILE,
							Path = Path.Combine(wRootDir, info.Path),
							SrcFile = Path.Combine(rRootDir, info.Path),
							BackupFile = Path.Combine(backupDir, Path.GetFileName(info.Path) + "~E" + index),
						});
					}
				}
			}
			foreach (PathInfo info in cpi.ROPaths)
			{
				if (info.Kind == PathInfo.Kind_e.DIR)
				{
					this.Tasks.Add(new Task()
					{
						Command = Task.Command_e.CREATE_DIR,
						Path = Path.Combine(wRootDir, info.Path),
					});
				}
				else // FILE
				{
					this.Tasks.Add(new Task()
					{
						Command = Task.Command_e.CREATE_FILE,
						Path = Path.Combine(wRootDir, info.Path),
						SrcFile = Path.Combine(rRootDir, info.Path),
					});
				}
			}

			#endregion

			this.Backup();
			this.Fire();
		}

		private void Fire()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				try
				{
					this.Advance(this.Tasks[index]);
				}
				catch (Exception e)
				{
					Logger.WriteLine("処理に失敗したためロールバックを開始します。" + e);

					try
					{
						this.RetreatFailedTask(this.Tasks[index]);

						while (0 <= --index)
						{
							this.Retreat(this.Tasks[index]);
						}
					}
					catch (Exception ex)
					{
						throw new Exception("ロールバックに失敗しました。", ex);
					}
					throw new Exception("ロールバックしました。", e);
				}
			}
		}

		private void Backup()
		{
			foreach (Task task in this.Tasks)
			{
				this.Backup(task);
			}
		}

		private void Backup(Task task)
		{
			switch (task.Command)
			{
				case Task.Command_e.DELETE_FILE:
				case Task.Command_e.UPDATE_FILE:
					this.CopyFile(task.Path, task.BackupFile);
					break;

				default:
					break;
			}
		}

		private void Advance(Task task)
		{
			switch (task.Command)
			{
				case Task.Command_e.DELETE_DIR:
					this.DeleteDir(task.Path);
					break;

				case Task.Command_e.DELETE_FILE:
					this.DeleteFile(task.Path);
					break;

				case Task.Command_e.UPDATE_FILE:
					this.CopyFile(task.SrcFile, task.Path, true);
					break;

				case Task.Command_e.CREATE_DIR:
					this.CreateDir(task.Path);
					break;

				case Task.Command_e.CREATE_FILE:
					this.CopyFile(task.SrcFile, task.Path);
					break;

				default:
					throw null; // never
			}
		}

		private void RetreatFailedTask(Task task)
		{
			switch (task.Command)
			{
				case Task.Command_e.DELETE_DIR:
					if (Directory.Exists(task.Path) == false)
					{
						this.CreateDir(task.Path);
					}
					break;

				case Task.Command_e.DELETE_FILE:
				case Task.Command_e.UPDATE_FILE:
					if (File.Exists(task.Path) == false)
					{
						this.CopyFile(task.BackupFile, task.Path);
					}
					else if (this.IsFileChanged(task.BackupFile, task.Path))
					{
						this.CopyFile(task.BackupFile, task.Path, true);
					}
					break;

				case Task.Command_e.CREATE_DIR:
					if (Directory.Exists(task.Path))
					{
						this.DeleteDir(task.Path);
					}
					break;

				case Task.Command_e.CREATE_FILE:
					if (File.Exists(task.Path))
					{
						this.DeleteFile(task.Path);
					}
					break;

				default:
					throw null; // never
			}
		}

		private void Retreat(Task task)
		{
			switch (task.Command)
			{
				case Task.Command_e.DELETE_DIR:
					this.CreateDir(task.Path);
					break;

				case Task.Command_e.DELETE_FILE:
					this.CopyFile(task.BackupFile, task.Path);
					break;

				case Task.Command_e.UPDATE_FILE:
					this.CopyFile(task.BackupFile, task.Path, true);
					break;

				case Task.Command_e.CREATE_DIR:
					this.DeleteDir(task.Path);
					break;

				case Task.Command_e.CREATE_FILE:
					this.DeleteFile(task.Path);
					break;

				default:
					throw null; // never
			}
		}

		private void CopyFile(string rFile, string wFile, bool overwrite = false)
		{
			Logger.WriteLine("ファイル \"" + rFile + "\" を \"" + wFile + "\" にコピーします。上書き=" + overwrite);

			File.Copy(rFile, wFile, overwrite);

			{
				FileInfo rInfo = new FileInfo(rFile);
				FileInfo wInfo = new FileInfo(wFile);
				FileAttributes attr = wInfo.Attributes;

				wInfo.Attributes &= ~FileAttributes.ReadOnly;

				wInfo.CreationTime = rInfo.CreationTime;
				wInfo.LastWriteTime = rInfo.LastWriteTime;
				wInfo.LastAccessTime = rInfo.LastAccessTime;

				wInfo.Attributes = attr;
			}

			Logger.WriteLine("ファイルをコピーしました。");
		}

		private bool IsFileChanged(string rFile, string wFile)
		{
			return this.GetFileHash(rFile) != this.GetFileHash(wFile);
		}

		private string GetFileHash(string file)
		{
			using (MD5 md5 = MD5.Create())
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return BitConverter.ToString(md5.ComputeHash(reader)).Replace("-", "").ToLower();
			}
		}

		private class Task
		{
			public enum Command_e
			{
				DELETE_DIR = 1,
				DELETE_FILE,
				UPDATE_FILE,
				CREATE_DIR,
				CREATE_FILE,
			}

			public Command_e Command;
			public string Path;
			public string SrcFile;
			public string BackupFile;
		}

		private List<Task> Tasks = new List<Task>();

		private class PathInfo
		{
			public enum Kind_e
			{
				DIR = 1,
				FILE,
			}

			public Kind_e Kind;
			public string Path;
		}

		private class CollectPathInfo
		{
			public string RRootDir;
			public string WRootDir;

			public List<PathInfo> ROPaths = new List<PathInfo>(); // コピー元にのみ存在するパスリスト
			public List<PathInfo> BEPaths = new List<PathInfo>(); // 両方に存在するパスリスト
			public List<PathInfo> WOPaths = new List<PathInfo>(); // コピー先にのみ存在するパスリスト

			private const string DIRECTORY_SEPARATOR = "\\";

			public void Main()
			{
				this.Merge(this.GetPathInfos(this.RRootDir, ""), this.GetPathInfos(this.WRootDir, ""));

				for (int index = 0; index < this.BEPaths.Count; index++)
				{
					PathInfo info = this.BEPaths[index];

					// 固有の処理 ----->

					// 両方に存在する data というディレクトリは無視する。(同期しない)
					if (info.Kind == PathInfo.Kind_e.DIR && Path.GetFileName(info.Path).ToLower() == "data")
					{
						this.BEPaths[index] = null;
						continue;
					}

					// <----- 固有の処理

					if (info.Kind == PathInfo.Kind_e.DIR)
					{
						this.Merge(
							this.GetPathInfos(Path.Combine(this.RRootDir, info.Path), info.Path + DIRECTORY_SEPARATOR),
							this.GetPathInfos(Path.Combine(this.WRootDir, info.Path), info.Path + DIRECTORY_SEPARATOR)
							);
					}
				}
				for (int index = 0; index < this.ROPaths.Count; index++)
				{
					PathInfo info = this.ROPaths[index];

					// 固有の処理 ----->

					// コピー元にのみ存在する data というディレクトリは無視する。(コピーしない)
					if (info.Kind == PathInfo.Kind_e.DIR && Path.GetFileName(info.Path).ToLower() == "data")
					{
						this.ROPaths[index] = null;
						continue;
					}

					// <----- 固有の処理

					if (info.Kind == PathInfo.Kind_e.DIR)
						this.ROPaths.AddRange(this.GetPathInfos(Path.Combine(this.RRootDir, info.Path), info.Path + DIRECTORY_SEPARATOR));
				}
				for (int index = 0; index < this.WOPaths.Count; index++)
				{
					PathInfo info = this.WOPaths[index];

					// 固有の処理 ----->

					// コピー先にのみ存在する data というディレクトリは無視する。(削除しない)
					if (info.Kind == PathInfo.Kind_e.DIR && Path.GetFileName(info.Path).ToLower() == "data")
					{
						this.WOPaths[index] = null;
						this.AllParent_SetNull(this.WOPaths, index, info.Path);
						continue;
					}

					// <----- 固有の処理

					if (info.Kind == PathInfo.Kind_e.DIR)
						this.WOPaths.AddRange(this.GetPathInfos(Path.Combine(this.WRootDir, info.Path), info.Path + DIRECTORY_SEPARATOR));
				}

				this.ROPaths.RemoveAll(info => info == null);
				this.BEPaths.RemoveAll(info => info == null);
				this.WOPaths.RemoveAll(info => info == null);
			}

			private void AllParent_SetNull(List<PathInfo> infos, int index, string path)
			{
				while ((path = Path.GetDirectoryName(path)) != "")
				{
					for (; ; )
					{
						if (index <= 0)
							return;

						index--;

						if (infos[index] != null && infos[index].Kind == PathInfo.Kind_e.DIR && this.Path_Comp(infos[index].Path, path) == 0)
						{
							infos[index] = null;
							break;
						}
					}
				}
			}

			private List<PathInfo> GetPathInfos(string rootDir, string relation)
			{
				List<PathInfo> dest = new List<PathInfo>();

				foreach (string dir in Directory.EnumerateDirectories(rootDir))
				{
					dest.Add(new PathInfo()
					{
						Kind = PathInfo.Kind_e.DIR,
						Path = relation + Path.GetFileName(dir),
					});
				}
				foreach (string file in Directory.EnumerateFiles(rootDir))
				{
					dest.Add(new PathInfo()
					{
						Kind = PathInfo.Kind_e.FILE,
						Path = relation + Path.GetFileName(file),
					});
				}
				dest.Sort(this.Comp);
				return dest;
			}

			private void Merge(List<PathInfo> rPaths, List<PathInfo> wPaths)
			{
				int rIndex = 0;
				int wIndex = 0;

				while (rIndex < rPaths.Count || wIndex < wPaths.Count)
				{
					int ret;

					if (rPaths.Count <= rIndex)
					{
						ret = 1;
					}
					else if (wPaths.Count <= wIndex)
					{
						ret = -1;
					}
					else
					{
						ret = this.Comp(rPaths[rIndex], wPaths[wIndex]);
					}

					if (ret < 0)
					{
						this.ROPaths.Add(rPaths[rIndex]);
						rIndex++;
					}
					else if (0 < ret)
					{
						this.WOPaths.Add(wPaths[wIndex]);
						wIndex++;
					}
					else
					{
						this.BEPaths.Add(rPaths[rIndex]);
						//this.BEPaths.Add(wPaths[wIndex]);
						rIndex++;
						wIndex++;
					}
				}
			}

			private int Comp(PathInfo a, PathInfo b)
			{
				int ret = this.Path_Comp(a.Path, b.Path);

				if (ret != 0)
					return ret;

				return (int)a.Kind - (int)b.Kind;
			}

			private int Path_Comp(string a, string b)
			{
				return this.CompIgnoreCase(a, b);
			}

			private int CompIgnoreCase(string a, string b)
			{
				return this.Comp(a.ToLower(), b.ToLower());
			}

			private int Comp(string a, string b)
			{
				return this.Comp(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
			}

			private int Comp(byte[] a, byte[] b)
			{
				int minlen = Math.Min(a.Length, b.Length);

				for (int index = 0; index < minlen; index++)
					if (a[index] != b[index])
						return (int)a[index] - (int)b[index];

				return a.Length - b.Length;
			}
		}

		private void CleanupDir(string rootDir)
		{
			foreach (string dir in Directory.EnumerateDirectories(rootDir))
			{
				this.CleanupDir(dir);
				this.DeleteDir(dir);
			}
			foreach (string file in Directory.EnumerateFiles(rootDir))
			{
				this.DeleteFile(file);
			}
		}

		private void DeleteFile(string file)
		{
			Logger.WriteLine("ファイル \"" + file + "\" を削除します。");

			{
				FileInfo info = new FileInfo(file);

				info.Attributes &= ~FileAttributes.ReadOnly;
			}

			File.Delete(file);

			Logger.WriteLine("ファイルを削除しました。");
		}

		private void CreateDir(string dir)
		{
			Logger.WriteLine("ディレクトリ \"" + dir + "\" を作成します。");

			Directory.CreateDirectory(dir);

			Logger.WriteLine("ディレクトリを作成しました。");
		}

		private void DeleteDir(string dir)
		{
			Logger.WriteLine("ディレクトリ \"" + dir + "\" を削除します。");

			Directory.Delete(dir);

			Logger.WriteLine("ディレクトリを削除しました。");
		}
	}
}
