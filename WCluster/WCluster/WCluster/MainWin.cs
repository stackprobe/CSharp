using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Threading;
using System.IO;

namespace WCluster
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;

			this.BackColor = Color.Black;
			this.MainPanel.BackColor = Color.Black;

			this.ProgressImg.Width = this.ProgressImg.Image.Width;
			this.ProgressImg.Height = this.ProgressImg.Image.Height;

			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (this.MT_Count == 0)
				{
					this.MainProcTh = new Thread(MainProc);
					this.MainProcTh.Start();
				}
				if (10 < this.MT_Count)
				{
					if (this.MainProcTh.IsAlive == false)
					{
						this.MT_Enabled = false;
						this.Close();
						return;
					}
				}

				{
					int l = (this.MainPanel.Width - this.ProgressImg.Width) / 2;
					int t = (this.MainPanel.Height - this.ProgressImg.Height) / 2;

					if (this.ProgressImg.Left != l)
						this.ProgressImg.Left = l;

					if (this.ProgressImg.Top != t)
						this.ProgressImg.Top = t;
				}

				Image img = this.ProgressImg.Image;
				img.RotateFlip(RotateFlipType.Rotate90FlipNone);
				this.ProgressImg.Image = img;
			}
			catch (Exception ex)
			{
				this.MT_Enabled = false;
				throw ex;
			}
			finally
			{
				this.MT_Count++;
				this.MT_Busy = false;
			}
		}

		private Thread MainProcTh;
		public static Exception MainProcEx;

		private const string CLUSTER_EXT = ".wclu";
		private const string DEF_OUT_EXT = ".out";

		private void MainProc()
		{
			try
			{
				string[] args = Environment.GetCommandLineArgs();
				args = ShiftArray(args);

				Clusterizer clusterizer = new Clusterizer();
				bool forceMode = false;

				for (; ; )
				{
					if (args[0].ToUpper() == "/F")
					{
						forceMode = true;
						args = ShiftArray(args);
						continue;
					}
					if (args[0].ToUpper() == "/P")
					{
						clusterizer = new CipherClusterizer(args[1]);
						args = ShiftArray(args);
						args = ShiftArray(args);
						continue;
					}
					if (args[0].ToUpper() == "/-")
					{
						args = ShiftArray(args);
						break;
					}
					break;
				}
				if (args.Length == 1)
				{
					string rPath = args[0];

					rPath = Path.GetFullPath(rPath);

					if (File.Exists(rPath))
					{
						string wPath =
							Path.GetExtension(rPath).ToLower() == CLUSTER_EXT ?
								EraseExtension(rPath) :
								rPath + DEF_OUT_EXT;

						if (forceMode == false && ExistsPath(wPath))
							throw new Exception("出力パスは既に存在します。[1F]");

						clusterizer.FileToDirectory(rPath, wPath);
						return;
					}
					if (Directory.Exists(rPath))
					{
						string wPath = rPath + CLUSTER_EXT;

						if (forceMode == false && ExistsPath(wPath))
							throw new Exception("出力パスは既に存在します。[1D]");

						clusterizer.DirectoryToFile(rPath, wPath);
						return;
					}
					throw new Exception("入力パスは存在しません。[1]");
				}
				if (args.Length == 2)
				{
					string rPath = args[0];
					string wPath = args[1];

					rPath = Path.GetFullPath(rPath);
					wPath = Path.GetFullPath(wPath);

					if (File.Exists(rPath))
					{
						if (Directory.Exists(wPath))
						{
							wPath = Path.Combine(
								wPath,
								Path.GetExtension(rPath).ToLower() == CLUSTER_EXT ?
									Path.GetFileNameWithoutExtension(rPath) :
									Path.GetFileName(rPath) + DEF_OUT_EXT
								);
						}
						if (forceMode == false && ExistsPath(wPath))
							throw new Exception("出力パスは既に存在します。[2F]");

						clusterizer.FileToDirectory(rPath, wPath);
						return;
					}
					if (Directory.Exists(rPath))
					{
						if (Directory.Exists(wPath))
							wPath = Path.Combine(wPath, Path.GetFileName(rPath) + CLUSTER_EXT);

						if (forceMode == false && ExistsPath(wPath))
							throw new Exception("出力パスは既に存在します。[2D]");

						clusterizer.DirectoryToFile(rPath, wPath);
						return;
					}
					throw new Exception("入力パスは存在しません。[2]");
				}
				throw new Exception("コマンド引数エラー");
			}
			catch (Exception e)
			{
				MainProcEx = e;
			}
		}

		public static string[] ShiftArray(string[] src)
		{
			string[] dest = new string[src.Length - 1];

			for (int index = 1; index < src.Length; index++)
				dest[index - 1] = src[index];

			return dest;
		}

		public static string EraseExtension(string path)
		{
			return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
		}

		public static bool ExistsPath(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}
	}
}
