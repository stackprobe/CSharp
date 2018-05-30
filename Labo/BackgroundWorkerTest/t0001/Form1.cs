using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace t0001
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			{
				int thId = Thread.CurrentThread.ManagedThreadId;

				thId.ToString(); // Main Thread
			}

			BackgroundWorker bw = new BackgroundWorker();

			bw.DoWork += delegate
			{
				int thId = Thread.CurrentThread.ManagedThreadId;

				thId.ToString(); // Worker Thread

				Thread.Sleep(2000);
				bw.ReportProgress(0);
				Thread.Sleep(2000);
				bw.ReportProgress(50);
				Thread.Sleep(2000);
				bw.ReportProgress(100);
				Thread.Sleep(2000);
			};

			bw.RunWorkerCompleted += delegate
			{
				int thId = Thread.CurrentThread.ManagedThreadId;

				thId.ToString(); // Main Thread
			};

			bw.ProgressChanged += delegate
			{
				int thId = Thread.CurrentThread.ManagedThreadId;

				thId.ToString(); // Main Thread
			};

			bw.WorkerReportsProgress = true;
			bw.RunWorkerAsync();
		}
	}
}
