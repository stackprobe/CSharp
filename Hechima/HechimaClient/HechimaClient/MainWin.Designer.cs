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
			this.MessageTextContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.たかざわダブルじゅんすけDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RemarksText = new System.Windows.Forms.TextBox();
			this.RemarksTextContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.見るだけMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.MainContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MessageTextContextMenu.SuspendLayout();
			this.RemarksTextContextMenu.SuspendLayout();
			this.MainContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MessageText
			// 
			this.MessageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessageText.ContextMenuStrip = this.MessageTextContextMenu;
			this.MessageText.Location = new System.Drawing.Point(12, 384);
			this.MessageText.MaxLength = 500;
			this.MessageText.Multiline = true;
			this.MessageText.Name = "MessageText";
			this.MessageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.MessageText.Size = new System.Drawing.Size(460, 66);
			this.MessageText.TabIndex = 2;
			this.MessageText.Text = "1行目\r\n2行目\r\n3行目";
			this.MessageText.TextChanged += new System.EventHandler(this.MessageText_TextChanged);
			this.MessageText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MessageText_KeyPress);
			// 
			// MessageTextContextMenu
			// 
			this.MessageTextContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.たかざわダブルじゅんすけDToolStripMenuItem});
			this.MessageTextContextMenu.Name = "MessageTextContextMenu";
			this.MessageTextContextMenu.Size = new System.Drawing.Size(240, 26);
			// 
			// たかざわダブルじゅんすけDToolStripMenuItem
			// 
			this.たかざわダブルじゅんすけDToolStripMenuItem.Name = "たかざわダブルじゅんすけDToolStripMenuItem";
			this.たかざわダブルじゅんすけDToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.たかざわダブルじゅんすけDToolStripMenuItem.Text = "たかざわダブルじゅんすけ(&D)";
			this.たかざわダブルじゅんすけDToolStripMenuItem.Click += new System.EventHandler(this.たかざわダブルじゅんすけDToolStripMenuItem_Click);
			// 
			// RemarksText
			// 
			this.RemarksText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RemarksText.ContextMenuStrip = this.RemarksTextContextMenu;
			this.RemarksText.Location = new System.Drawing.Point(12, 12);
			this.RemarksText.MaxLength = 0;
			this.RemarksText.Multiline = true;
			this.RemarksText.Name = "RemarksText";
			this.RemarksText.ReadOnly = true;
			this.RemarksText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.RemarksText.Size = new System.Drawing.Size(460, 366);
			this.RemarksText.TabIndex = 1;
			this.RemarksText.TextChanged += new System.EventHandler(this.RemarksText_TextChanged);
			// 
			// RemarksTextContextMenu
			// 
			this.RemarksTextContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.見るだけMToolStripMenuItem});
			this.RemarksTextContextMenu.Name = "RemarksTextContextMenu";
			this.RemarksTextContextMenu.Size = new System.Drawing.Size(153, 48);
			// 
			// 見るだけMToolStripMenuItem
			// 
			this.見るだけMToolStripMenuItem.Name = "見るだけMToolStripMenuItem";
			this.見るだけMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.見るだけMToolStripMenuItem.Text = "ビュー(&V)";
			this.見るだけMToolStripMenuItem.Click += new System.EventHandler(this.見るだけMToolStripMenuItem_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定SToolStripMenuItem});
			this.MainContextMenu.Name = "MainContextMenu";
			this.MainContextMenu.Size = new System.Drawing.Size(119, 26);
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			this.設定SToolStripMenuItem.Click += new System.EventHandler(this.設定SToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 462);
			this.ContextMenuStrip = this.MainContextMenu;
			this.Controls.Add(this.RemarksText);
			this.Controls.Add(this.MessageText);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "へちま";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.MessageTextContextMenu.ResumeLayout(false);
			this.RemarksTextContextMenu.ResumeLayout(false);
			this.MainContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox MessageText;
		private System.Windows.Forms.TextBox RemarksText;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ContextMenuStrip MainContextMenu;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip RemarksTextContextMenu;
		private System.Windows.Forms.ToolStripMenuItem 見るだけMToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip MessageTextContextMenu;
		private System.Windows.Forms.ToolStripMenuItem たかざわダブルじゅんすけDToolStripMenuItem;
	}
}

