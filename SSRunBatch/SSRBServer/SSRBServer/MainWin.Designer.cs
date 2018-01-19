namespace Charlotte
{
	partial class MainWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.TaskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TTIMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.現在実行中のバッチファイルを放棄するAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ポート番号PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.Abandon_実行AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TTIMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TaskTrayIcon
			// 
			this.TaskTrayIcon.ContextMenuStrip = this.TTIMenu;
			this.TaskTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskTrayIcon.Icon")));
			this.TaskTrayIcon.Text = "準備しています...";
			this.TaskTrayIcon.BalloonTipClicked += new System.EventHandler(this.TaskTrayIcon_BalloonTipClicked);
			this.TaskTrayIcon.BalloonTipClosed += new System.EventHandler(this.TaskTrayIcon_BalloonTipClosed);
			// 
			// TTIMenu
			// 
			this.TTIMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.現在実行中のバッチファイルを放棄するAToolStripMenuItem,
            this.ポート番号PToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了XToolStripMenuItem});
			this.TTIMenu.Name = "TTIMenu";
			this.TTIMenu.Size = new System.Drawing.Size(311, 98);
			// 
			// 現在実行中のバッチファイルを放棄するAToolStripMenuItem
			// 
			this.現在実行中のバッチファイルを放棄するAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Abandon_実行AToolStripMenuItem});
			this.現在実行中のバッチファイルを放棄するAToolStripMenuItem.Name = "現在実行中のバッチファイルを放棄するAToolStripMenuItem";
			this.現在実行中のバッチファイルを放棄するAToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
			this.現在実行中のバッチファイルを放棄するAToolStripMenuItem.Text = "現在実行中のバッチファイルを放棄する(&A)";
			// 
			// ポート番号PToolStripMenuItem
			// 
			this.ポート番号PToolStripMenuItem.Name = "ポート番号PToolStripMenuItem";
			this.ポート番号PToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
			this.ポート番号PToolStripMenuItem.Text = "ポート番号(&P)";
			this.ポート番号PToolStripMenuItem.Click += new System.EventHandler(this.ポート番号PToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(307, 6);
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(310, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// Abandon_実行AToolStripMenuItem
			// 
			this.Abandon_実行AToolStripMenuItem.Name = "Abandon_実行AToolStripMenuItem";
			this.Abandon_実行AToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.Abandon_実行AToolStripMenuItem.Text = "実行(&A)";
			this.Abandon_実行AToolStripMenuItem.Click += new System.EventHandler(this.Abandon_実行AToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-400, -400);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "SSRBServer_MainWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.TTIMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon TaskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip TTIMenu;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ポート番号PToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 現在実行中のバッチファイルを放棄するAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem Abandon_実行AToolStripMenuItem;
	}
}

