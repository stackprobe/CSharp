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
			List<PathInfo> rPaths = this.GetPathInfos(rRootDir); // コピー元パスリスト
			List<PathInfo> wPaths = this.GetPathInfos(wRootDir); // コピー先パスリスト
			List<PathInfo> roPaths = new List<PathInfo>(); // コピー元にのみ存在するパスリスト
			List<PathInfo> bePaths = new List<PathInfo>(); // 両方に存在するパスリスト
			List<PathInfo> woPaths = new List<PathInfo>(); // コピー先にのみ存在するパスリスト

			this.Merge(rPaths, wPaths, roPaths, bePaths, woPaths);

			#region MakeTasks

			this.Tasks.Clear(); // reset

			for (int index = woPaths.Count - 1; 0 <= index; index--)
			{
				PathInfo info = woPaths[index];

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
			for (int index = 0; index < bePaths.Count; index++)
			{
				PathInfo info = bePaths[index];

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
			foreach (PathInfo info in roPaths)
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

		private void Merge(List<PathInfo> rPaths, List<PathInfo> wPaths, List<PathInfo> roPaths, List<PathInfo> bePaths, List<PathInfo> woPaths)
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
					roPaths.Add(rPaths[rIndex]);
					rIndex++;
				}
				else if (0 < ret)
				{
					woPaths.Add(wPaths[wIndex]);
					wIndex++;
				}
				else
				{
					bePaths.Add(rPaths[rIndex]);
					//bePaths.Add(wPaths[wIndex]);
					rIndex++;
					wIndex++;
				}
			}
		}

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

		private List<PathInfo> GetPathInfos(string rootDir)
		{
			List<PathInfo> dest = new List<PathInfo>();

			this.CollectPathInfos(rootDir, "", dest);

			dest.Sort(this.Comp);

			return dest;
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

				this.CollectPathInfos(dir, relation + Path.GetFileName(dir) + "\\", dest);
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

		private void CleanupDir(string rootDir)
		{
			foreach (string dir in Directory.EnumerateDirectories(rootDir))
				Directory.Delete(dir, true);

			foreach (string file in Directory.EnumerateFiles(rootDir))
				File.Delete(file);
		}
	}
}
