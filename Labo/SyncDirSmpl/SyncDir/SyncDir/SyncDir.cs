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

			Logger.WriteLine("同期しました。");
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
					else if (this.File_Comp(task.BackupFile, task.Path) != 0)
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
			return new FileInfo(wFile).LastWriteTime < new FileInfo(rFile).LastWriteTime;
		}

		private int File_Comp(string file1, string file2)
		{
			using (FileStream reader1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
			using (FileStream reader2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
			{
				for (; ; )
				{
					int chr1 = reader1.ReadByte();
					int chr2 = reader2.ReadByte();

					if (chr1 != chr2)
						return (int)chr1 - (int)chr2;

					if (chr1 == -1)
						return 0;
				}
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

		private const string DIRECTORY_SEPARATOR = "\\";

		private void CollectPathInfos(string rootDir, string relation, List<PathInfo> dest)
		{
			foreach (string dir in Directory.EnumerateDirectories(rootDir))
			{
				dest.Add(new PathInfo()
				{
					Kind = PathInfo.Kind_e.DIR,
					Path = relation + Path.GetFileName(dir),
				});

				this.CollectPathInfos(dir, relation + Path.GetFileName(dir) + DIRECTORY_SEPARATOR, dest);
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
