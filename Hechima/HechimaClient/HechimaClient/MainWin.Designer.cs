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
			this.MessageText = new System.Windows.Forms.TextBox();
			this.RemarksText = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.へちまHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MessageText
			// 
			this.MessageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessageText.Location = new System.Drawing.Point(12, 384);
			this.MessageText.Multiline = true;
			this.MessageText.Name = "MessageText";
			this.MessageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.MessageText.Size = new System.Drawing.Size(460, 66);
			this.MessageText.TabIndex = 2;
			this.MessageText.Text = "1行目\r\n2行目\r\n3行目";
			this.MessageText.TextChanged += new System.EventHandler(this.MessageText_TextChanged);
			this.MessageText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MessageText_KeyPress);
			// 
			// RemarksText
			// 
			this.RemarksText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RemarksText.Location = new System.Drawing.Point(12, 29);
			this.RemarksText.Multiline = true;
			this.RemarksText.Name = "RemarksText";
			this.RemarksText.ReadOnly = true;
			this.RemarksText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.RemarksText.Size = new System.Drawing.Size(460, 349);
			this.RemarksText.TabIndex = 1;
			this.RemarksText.TextChanged += new System.EventHandler(this.RemarksText_TextChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.へちまHToolStripMenuItem,
            this.設定SToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(484, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// へちまHToolStripMenuItem
			// 
			this.へちまHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
			this.へちまHToolStripMenuItem.Name = "へちまHToolStripMenuItem";
			this.へちまHToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
			this.へちまHToolStripMenuItem.Text = "へちま(&H)";
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定SSMenuItem});
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			// 
			// 設定SSMenuItem
			// 
			this.設定SSMenuItem.Name = "設定SSMenuItem";
			this.設定SSMenuItem.Size = new System.Drawing.Size(152, 22);
			this.設定SSMenuItem.Text = "設定(&S)";
			this.設定SSMenuItem.Click += new System.EventHandler(this.設定SSMenuItem_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 462);
			this.Controls.Add(this.RemarksText);
			this.Controls.Add(this.MessageText);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "へちま";
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

		private System.Windows.Forms.TextBox MessageText;
		private System.Windows.Forms.TextBox RemarksText;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem へちまHToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定SSMenuItem;
		private System.Windows.Forms.Timer MainTimer;
	}
}

