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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.コントロールCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.開始SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.停止TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.ファイル転送サーバーFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.ChatSvStatusLabel = new System.Windows.Forms.Label();
			this.FileSvStatusLabel = new System.Windows.Forms.Label();
			this.ChatSvStatus = new System.Windows.Forms.Label();
			this.FileSvStatus = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリAToolStripMenuItem,
            this.コントロールCToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(296, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリAToolStripMenuItem
			// 
			this.アプリAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定SToolStripMenuItem,
            this.toolStripMenuItem1,
            this.終了XToolStripMenuItem});
			this.アプリAToolStripMenuItem.Name = "アプリAToolStripMenuItem";
			this.アプリAToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
			this.アプリAToolStripMenuItem.Text = "アプリ(&A)";
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			this.設定SToolStripMenuItem.Click += new System.EventHandler(this.設定SToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 6);
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// コントロールCToolStripMenuItem
			// 
			this.コントロールCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開始SToolStripMenuItem,
            this.停止TToolStripMenuItem,
            this.toolStripMenuItem2,
            this.ファイル転送サーバーFToolStripMenuItem});
			this.コントロールCToolStripMenuItem.Name = "コントロールCToolStripMenuItem";
			this.コントロールCToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.コントロールCToolStripMenuItem.Text = "コントロール(&C)";
			// 
			// 開始SToolStripMenuItem
			// 
			this.開始SToolStripMenuItem.Name = "開始SToolStripMenuItem";
			this.開始SToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.開始SToolStripMenuItem.Text = "開始(&S)";
			this.開始SToolStripMenuItem.Click += new System.EventHandler(this.開始SToolStripMenuItem_Click);
			// 
			// 停止TToolStripMenuItem
			// 
			this.停止TToolStripMenuItem.Name = "停止TToolStripMenuItem";
			this.停止TToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.停止TToolStripMenuItem.Text = "停止(&T)";
			this.停止TToolStripMenuItem.Click += new System.EventHandler(this.停止TToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(210, 6);
			// 
			// ファイル転送サーバーFToolStripMenuItem
			// 
			this.ファイル転送サーバーFToolStripMenuItem.Name = "ファイル転送サーバーFToolStripMenuItem";
			this.ファイル転送サーバーFToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
			this.ファイル転送サーバーFToolStripMenuItem.Text = "ファイル転送サーバー(&F)";
			this.ファイル転送サーバーFToolStripMenuItem.Click += new System.EventHandler(this.ファイル転送サーバーFToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 129);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(296, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 5;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// ChatSvStatusLabel
			// 
			this.ChatSvStatusLabel.AutoSize = true;
			this.ChatSvStatusLabel.Location = new System.Drawing.Point(21, 46);
			this.ChatSvStatusLabel.Name = "ChatSvStatusLabel";
			this.ChatSvStatusLabel.Size = new System.Drawing.Size(100, 20);
			this.ChatSvStatusLabel.TabIndex = 1;
			this.ChatSvStatusLabel.Text = "サーバーの状態";
			// 
			// FileSvStatusLabel
			// 
			this.FileSvStatusLabel.AutoSize = true;
			this.FileSvStatusLabel.Location = new System.Drawing.Point(21, 85);
			this.FileSvStatusLabel.Name = "FileSvStatusLabel";
			this.FileSvStatusLabel.Size = new System.Drawing.Size(178, 20);
			this.FileSvStatusLabel.TabIndex = 3;
			this.FileSvStatusLabel.Text = "ファイル転送サーバーの状態";
			// 
			// ChatSvStatus
			// 
			this.ChatSvStatus.AutoSize = true;
			this.ChatSvStatus.Location = new System.Drawing.Point(221, 46);
			this.ChatSvStatus.Name = "ChatSvStatus";
			this.ChatSvStatus.Size = new System.Drawing.Size(63, 20);
			this.ChatSvStatus.TabIndex = 2;
			this.ChatSvStatus.Text = "取得中...";
			// 
			// FileSvStatus
			// 
			this.FileSvStatus.AutoSize = true;
			this.FileSvStatus.Location = new System.Drawing.Point(221, 85);
			this.FileSvStatus.Name = "FileSvStatus";
			this.FileSvStatus.Size = new System.Drawing.Size(63, 20);
			this.FileSvStatus.TabIndex = 4;
			this.FileSvStatus.Text = "取得中...";
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(296, 151);
			this.Controls.Add(this.FileSvStatus);
			this.Controls.Add(this.ChatSvStatus);
			this.Controls.Add(this.FileSvStatusLabel);
			this.Controls.Add(this.ChatSvStatusLabel);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "MainWin";
			this.Text = "Chat Server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem コントロールCToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 開始SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 停止TToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem ファイル転送サーバーFToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Label ChatSvStatusLabel;
		private System.Windows.Forms.Label FileSvStatusLabel;
		private System.Windows.Forms.Label ChatSvStatus;
		private System.Windows.Forms.Label FileSvStatus;
	}
}

