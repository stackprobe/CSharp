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
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.制御CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サーバーの再起動RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サーバーの起動SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サーバーの停止TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ポート番号PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.子プロセスの強制終了AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.現在実行中のバッチファイルを強制終了するAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tSRTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainOutput = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status = new System.Windows.Forms.ToolStripStatusLabel();
			this.EastStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.制御CToolStripMenuItem,
            this.設定SToolStripMenuItem,
            this.子プロセスの強制終了AToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(584, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 制御CToolStripMenuItem
			// 
			this.制御CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.サーバーの再起動RToolStripMenuItem,
            this.サーバーの起動SToolStripMenuItem,
            this.サーバーの停止TToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了XToolStripMenuItem});
			this.制御CToolStripMenuItem.Name = "制御CToolStripMenuItem";
			this.制御CToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.制御CToolStripMenuItem.Text = "制御(&C)";
			// 
			// サーバーの再起動RToolStripMenuItem
			// 
			this.サーバーの再起動RToolStripMenuItem.Name = "サーバーの再起動RToolStripMenuItem";
			this.サーバーの再起動RToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.サーバーの再起動RToolStripMenuItem.Text = "サーバーの再起動(&R)";
			this.サーバーの再起動RToolStripMenuItem.Click += new System.EventHandler(this.サーバーの再起動RToolStripMenuItem_Click);
			// 
			// サーバーの起動SToolStripMenuItem
			// 
			this.サーバーの起動SToolStripMenuItem.Name = "サーバーの起動SToolStripMenuItem";
			this.サーバーの起動SToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.サーバーの起動SToolStripMenuItem.Text = "サーバーの起動(&S)";
			this.サーバーの起動SToolStripMenuItem.Click += new System.EventHandler(this.サーバーの起動SToolStripMenuItem_Click);
			// 
			// サーバーの停止TToolStripMenuItem
			// 
			this.サーバーの停止TToolStripMenuItem.Name = "サーバーの停止TToolStripMenuItem";
			this.サーバーの停止TToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			this.サーバーの停止TToolStripMenuItem.Text = "サーバーの停止(&T)";
			this.サーバーの停止TToolStripMenuItem.Click += new System.EventHandler(this.サーバーの停止TToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 6);
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
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
            this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem});
			this.tSRTToolStripMenuItem.Name = "tSRTToolStripMenuItem";
			this.tSRTToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
			this.tSRTToolStripMenuItem.Text = "TSR(&T)";
			// 
			// 現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem
			// 
			this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem.Name = "現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem";
			this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem.Size = new System.Drawing.Size(390, 22);
			this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem.Text = "現在実行中の TSR バッチファイルを全て強制終了する(&A)";
			this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem.Click += new System.EventHandler(this.現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem_Click);
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
			this.MainOutput.Size = new System.Drawing.Size(560, 308);
			this.MainOutput.TabIndex = 1;
			this.MainOutput.TextChanged += new System.EventHandler(this.MainOutput_TextChanged);
			this.MainOutput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainOutput_KeyPress);
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
			this.Status.ForeColor = System.Drawing.Color.Blue;
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(467, 18);
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
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
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

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 制御CToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サーバーの再起動RToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サーバーの起動SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サーバーの停止TToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ポート番号PToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 子プロセスの強制終了AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 現在実行中のバッチファイルを強制終了するAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tSRTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem;
		private System.Windows.Forms.TextBox MainOutput;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel Status;
		private System.Windows.Forms.ToolStripStatusLabel EastStatus;
	}
}

