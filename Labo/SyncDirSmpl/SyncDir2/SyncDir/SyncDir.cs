using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
			int taskIndex = -1;

			try
			{
				for (taskIndex = 0; taskIndex < this.Tasks.Count; taskIndex++)
				{
					Task task = this.Tasks[taskIndex];

					this.Advance(task);
				}
			}
			catch (Exception e)
			{
				Logger.WriteLine(e);

				try
				{
					while (0 <= --taskIndex)
					{
						Task task = this.Tasks[taskIndex];

						this.Retreat(task);
					}
					throw new Exception("ロールバックしました。", e);
				}
				catch (Exception ex)
				{
					Logger.WriteLine(ex);

					throw new Exception("ロールバックに失敗しました。", ex);
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
					Directory.Delete(task.Path);
					break;

				case Task.Command_e.DELETE_FILE:
					File.Delete(task.Path);
					break;

				case Task.Command_e.UPDATE_FILE:
					this.CopyFile(task.SrcFile, task.Path, true);
					break;

				case Task.Command_e.CREATE_DIR:
					Directory.CreateDirectory(task.Path);
					break;

				case Task.Command_e.CREATE_FILE:
					this.CopyFile(task.SrcFile, task.Path);
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
					Directory.CreateDirectory(task.Path);
					break;

				case Task.Command_e.DELETE_FILE:
					this.CopyFile(task.BackupFile, task.Path);
					break;

				case Task.Command_e.UPDATE_FILE:
					this.CopyFile(task.BackupFile, task.Path, true);
					break;

				case Task.Command_e.CREATE_DIR:
					Directory.Delete(task.Path);
					break;

				case Task.Command_e.CREATE_FILE:
					File.Delete(task.Path);
					break;

				default:
					throw null; // never
			}
		}

		private void CopyFile(string rFile, string wFile, bool overwrite = false)
		{
			File.Copy(rFile, wFile, overwrite);

			{
				FileInfo rInfo = new FileInfo(rFile);
				FileInfo wInfo = new FileInfo(wFile);

				wInfo.CreationTime = rInfo.CreationTime;
				wInfo.LastWriteTime = rInfo.LastWriteTime;
				wInfo.LastAccessTime = rInfo.LastAccessTime;
			}
		}

		private bool IsFileChanged(string rFile, string wFile)
		{
			return new FileInfo(wFile).LastWriteTime < new FileInfo(rFile).LastWriteTime;
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

			// memo: queue ???

			public List<PathInfo> ROPaths = new List<PathInfo>(); // コピー元にのみ存在するパスリスト
			public List<PathInfo> BEPaths = new List<PathInfo>(); // 両方に存在するパスリスト
			public List<PathInfo> WOPaths = new List<PathInfo>(); // コピー先にのみ存在するパスリスト

			public void Main()
			{
				// TODO
			}

			private void CollectPathInfos(string rootDir, string relation, List<PathInfo> dest)
			{
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
				int ret = this.CompIgnoreCase(a.Path, b.Path);

				if (ret != 0)
					return ret;

				return (int)a.Kind - (int)b.Kind;
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
				Directory.Delete(dir, true);

			foreach (string file in Directory.EnumerateFiles(rootDir))
				File.Delete(file);
		}
	}
}
