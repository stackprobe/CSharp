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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリケーションXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ポート番号PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.子プロセスの強制終了AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tSRTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status = new System.Windows.Forms.ToolStripStatusLabel();
			this.EastStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainOutput = new System.Windows.Forms.TextBox();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サーバーを起動するSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サーバーを停止するTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サーバーを再起動するRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリケーションXToolStripMenuItem,
            this.設定SToolStripMenuItem,
            this.子プロセスの強制終了AToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(584, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリケーションXToolStripMenuItem
			// 
			this.アプリケーションXToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サーバーを再起動するRToolStripMenuItem,
            this.サーバーを起動するSToolStripMenuItem,
            this.サーバーを停止するTToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了XToolStripMenuItem});
			this.アプリケーションXToolStripMenuItem.Name = "アプリケーションXToolStripMenuItem";
			this.アプリケーションXToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.アプリケーションXToolStripMenuItem.Text = "アプリケーション(&X)";
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ポート番号PToolStripMenuItem});
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			// 
			// ポート番号PToolStripMenuItem
			// 
			this.ポート番号PToolStripMenuItem.Name = "ポート番号PToolStripMenuItem";
			this.ポート番号PToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
			this.ポート番号PToolStripMenuItem.Text = "ポート番号(&P)";
			this.ポート番号PToolStripMenuItem.Click += new System.EventHandler(this.ポート番号PToolStripMenuItem_Click);
			// 
			// 子プロセスの強制終了AToolStripMenuItem
			// 
			this.子プロセスの強制終了AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem,
            this.tSRTToolStripMenuItem});
			this.子プロセスの強制終了AToolStripMenuItem.Name = "子プロセスの強制終了AToolStripMenuItem";
			this.子プロセスの強制終了AToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.子プロセスの強制終了AToolStripMenuItem.Text = "子プロセスの強制終了(&A)";
			// 
			// 現在実行中のバッチファイルを強制終了するAToolStripMenuItem
			// 
			this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem.Name = "現在実行中のバッチファイルを強制終了するAToolStripMenuItem";
			this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
			this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem.Text = "現在実行中のバッチファイルを強制終了する(&A)";
			this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem.Click += new System.EventHandler(this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem_Click);
			// 
			// tSRTToolStripMenuItem
			// 
			this.tSRTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem});
			this.tSRTToolStripMenuItem.Name = "tSRTToolStripMenuItem";
			this.tSRTToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
			this.tSRTToolStripMenuItem.Text = "TSR(&T)";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.EastStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 339);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(584, 23);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// Status
			// 
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(498, 18);
			this.Status.Spring = true;
			this.Status.Text = "Status";
			this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// EastStatus
			// 
			this.EastStatus.Name = "EastStatus";
			this.EastStatus.Size = new System.Drawing.Size(71, 18);
			this.EastStatus.Text = "EastStatus";
			// 
			// MainOutput
			// 
			this.MainOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainOutput.Location = new System.Drawing.Point(12, 29);
			this.MainOutput.Multiline = true;
			this.MainOutput.Name = "MainOutput";
			this.MainOutput.ReadOnly = true;
			this.MainOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.MainOutput.Size = new System.Drawing.Size(560, 307);
			this.MainOutput.TabIndex = 1;
			this.MainOutput.TextChanged += new System.EventHandler(this.MainOutput_TextChanged);
			this.MainOutput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainOutput_KeyPress);
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// 現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem
			// 
			this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem.Name = "現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem";
			this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem.Size = new System.Drawing.Size(390, 22);
			this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem.Text = "現在実行中の TSR バッチファイルを全て強制終了する(&T)";
			this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem.Click += new System.EventHandler(this.現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem_Click);
			// 
			// サーバーを起動するSToolStripMenuItem
			// 
			this.サーバーを起動するSToolStripMenuItem.Name = "サーバーを起動するSToolStripMenuItem";
			this.サーバーを起動するSToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.サーバーを起動するSToolStripMenuItem.Text = "サーバーを起動する(&S)";
			// 
			// サーバーを停止するTToolStripMenuItem
			// 
			this.サーバーを停止するTToolStripMenuItem.Name = "サーバーを停止するTToolStripMenuItem";
			this.サーバーを停止するTToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.サーバーを停止するTToolStripMenuItem.Text = "サーバーを停止する(&T)";
			// 
			// サーバーを再起動するRToolStripMenuItem
			// 
			this.サーバーを再起動するRToolStripMenuItem.Name = "サーバーを再起動するRToolStripMenuItem";
			this.サーバーを再起動するRToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
			this.サーバーを再起動するRToolStripMenuItem.Text = "サーバーを再起動する(&R)";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 6);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 362);
			this.Controls.Add(this.MainOutput);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SSRBServer3";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリケーションXToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel Status;
		private System.Windows.Forms.ToolStripStatusLabel EastStatus;
		private System.Windows.Forms.TextBox MainOutput;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ポート番号PToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 子プロセスの強制終了AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 現在実行中のバッチファイルを強制終了するAToolStripMenuItem;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ToolStripMenuItem tSRTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サーバーを起動するSToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サーバーを停止するTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サーバーを再起動するRToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
	}
}

