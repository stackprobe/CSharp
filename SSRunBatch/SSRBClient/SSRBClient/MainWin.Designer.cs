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
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.送信ファイルSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SendFile_追加AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SendFile_削除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.応答ファイルRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RecvFile_追加AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RecvFile_削除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ServerPortNo = new System.Windows.Forms.TextBox();
			this.ServerDomain = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Batch = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.Response = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status = new System.Windows.Forms.ToolStripStatusLabel();
			this.EastStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.BtnRun = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリAToolStripMenuItem,
            this.送信ファイルSToolStripMenuItem,
            this.応答ファイルRToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(584, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリAToolStripMenuItem
			// 
			this.アプリAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
			this.アプリAToolStripMenuItem.Name = "アプリAToolStripMenuItem";
			this.アプリAToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
			this.アプリAToolStripMenuItem.Text = "アプリ(&A)";
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// 送信ファイルSToolStripMenuItem
			// 
			this.送信ファイルSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendFile_追加AToolStripMenuItem,
            this.SendFile_削除DToolStripMenuItem});
			this.送信ファイルSToolStripMenuItem.Name = "送信ファイルSToolStripMenuItem";
			this.送信ファイルSToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.送信ファイルSToolStripMenuItem.Text = "送信ファイル(&S)";
			// 
			// SendFile_追加AToolStripMenuItem
			// 
			this.SendFile_追加AToolStripMenuItem.Name = "SendFile_追加AToolStripMenuItem";
			this.SendFile_追加AToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.SendFile_追加AToolStripMenuItem.Text = "追加(&A)";
			this.SendFile_追加AToolStripMenuItem.Click += new System.EventHandler(this.SendFile_追加AToolStripMenuItem_Click);
			// 
			// SendFile_削除DToolStripMenuItem
			// 
			this.SendFile_削除DToolStripMenuItem.Name = "SendFile_削除DToolStripMenuItem";
			this.SendFile_削除DToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.SendFile_削除DToolStripMenuItem.Text = "削除(&D)";
			// 
			// 応答ファイルRToolStripMenuItem
			// 
			this.応答ファイルRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RecvFile_追加AToolStripMenuItem,
            this.RecvFile_削除DToolStripMenuItem});
			this.応答ファイルRToolStripMenuItem.Name = "応答ファイルRToolStripMenuItem";
			this.応答ファイルRToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.応答ファイルRToolStripMenuItem.Text = "応答ファイル(&R)";
			// 
			// RecvFile_追加AToolStripMenuItem
			// 
			this.RecvFile_追加AToolStripMenuItem.Name = "RecvFile_追加AToolStripMenuItem";
			this.RecvFile_追加AToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.RecvFile_追加AToolStripMenuItem.Text = "追加(&A)";
			this.RecvFile_追加AToolStripMenuItem.Click += new System.EventHandler(this.RecvFile_追加AToolStripMenuItem_Click);
			// 
			// RecvFile_削除DToolStripMenuItem
			// 
			this.RecvFile_削除DToolStripMenuItem.Name = "RecvFile_削除DToolStripMenuItem";
			this.RecvFile_削除DToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.RecvFile_削除DToolStripMenuItem.Text = "削除(&D)";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.ServerPortNo);
			this.groupBox1.Controls.Add(this.ServerDomain);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 70);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "接続先";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(364, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "/ ポート番号";
			// 
			// ServerPortNo
			// 
			this.ServerPortNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ServerPortNo.Location = new System.Drawing.Point(454, 26);
			this.ServerPortNo.MaxLength = 5;
			this.ServerPortNo.Name = "ServerPortNo";
			this.ServerPortNo.Size = new System.Drawing.Size(100, 27);
			this.ServerPortNo.TabIndex = 3;
			this.ServerPortNo.Text = "65535";
			this.ServerPortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ServerDomain
			// 
			this.ServerDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ServerDomain.Location = new System.Drawing.Point(73, 26);
			this.ServerDomain.MaxLength = 300;
			this.ServerDomain.Name = "ServerDomain";
			this.ServerDomain.Size = new System.Drawing.Size(285, 27);
			this.ServerDomain.TabIndex = 1;
			this.ServerDomain.Text = "localhost";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "ホスト名";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Batch:";
			// 
			// Batch
			// 
			this.Batch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Batch.Location = new System.Drawing.Point(12, 125);
			this.Batch.MaxLength = 10000;
			this.Batch.Multiline = true;
			this.Batch.Name = "Batch";
			this.Batch.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.Batch.Size = new System.Drawing.Size(560, 143);
			this.Batch.TabIndex = 3;
			this.Batch.Text = "コマンド1\r\nコマンド2\r\nコマンド3\r\nコマンド4\r\nコマンド5\r\nコマンド6";
			this.Batch.WordWrap = false;
			this.Batch.TextChanged += new System.EventHandler(this.Batch_TextChanged);
			this.Batch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Batch_KeyPress);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 307);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 20);
			this.label4.TabIndex = 5;
			this.label4.Text = "Response:";
			// 
			// Response
			// 
			this.Response.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Response.Location = new System.Drawing.Point(12, 330);
			this.Response.Multiline = true;
			this.Response.Name = "Response";
			this.Response.ReadOnly = true;
			this.Response.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.Response.Size = new System.Drawing.Size(560, 143);
			this.Response.TabIndex = 6;
			this.Response.Text = "応答1\r\n応答2\r\n応答3\r\n応答4\r\n応答5\r\n応答6";
			this.Response.WordWrap = false;
			this.Response.TextChanged += new System.EventHandler(this.Response_TextChanged);
			this.Response.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Response_KeyPress);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.EastStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 476);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(584, 23);
			this.statusStrip1.TabIndex = 7;
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
			// BtnRun
			// 
			this.BtnRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnRun.Location = new System.Drawing.Point(12, 274);
			this.BtnRun.Name = "BtnRun";
			this.BtnRun.Size = new System.Drawing.Size(560, 30);
			this.BtnRun.TabIndex = 4;
			this.BtnRun.Text = "実行";
			this.BtnRun.UseVisualStyleBackColor = true;
			this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 499);
			this.Controls.Add(this.BtnRun);
			this.Controls.Add(this.Response);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Batch);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SSRBClient";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ServerPortNo;
		private System.Windows.Forms.TextBox ServerDomain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox Batch;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox Response;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel Status;
		private System.Windows.Forms.ToolStripStatusLabel EastStatus;
		private System.Windows.Forms.ToolStripMenuItem 送信ファイルSToolStripMenuItem;
		private System.Windows.Forms.Button BtnRun;
		private System.Windows.Forms.ToolStripMenuItem SendFile_追加AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SendFile_削除DToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 応答ファイルRToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RecvFile_追加AToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RecvFile_削除DToolStripMenuItem;
	}
}

