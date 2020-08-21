namespace Charlotte
{
	partial class OnlineDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineDlg));
			this.OnlineText = new System.Windows.Forms.TextBox();
			this.MainContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.直ちに更新RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.MainContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// OnlineText
			// 
			this.OnlineText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OnlineText.ContextMenuStrip = this.MainContextMenu;
			this.OnlineText.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.OnlineText.Location = new System.Drawing.Point(12, 12);
			this.OnlineText.MaxLength = 0;
			this.OnlineText.Multiline = true;
			this.OnlineText.Name = "OnlineText";
			this.OnlineText.ReadOnly = true;
			this.OnlineText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.OnlineText.Size = new System.Drawing.Size(260, 238);
			this.OnlineText.TabIndex = 0;
			this.OnlineText.WordWrap = false;
			this.OnlineText.TextChanged += new System.EventHandler(this.OnlineText_TextChanged);
			this.OnlineText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlineText_KeyPress);
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直ちに更新RToolStripMenuItem});
			this.MainContextMenu.Name = "MainContextMenu";
			this.MainContextMenu.Size = new System.Drawing.Size(155, 26);
			// 
			// 直ちに更新RToolStripMenuItem
			// 
			this.直ちに更新RToolStripMenuItem.Name = "直ちに更新RToolStripMenuItem";
			this.直ちに更新RToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.直ちに更新RToolStripMenuItem.Text = "直ちに更新(&R)";
			this.直ちに更新RToolStripMenuItem.Click += new System.EventHandler(this.直ちに更新RToolStripMenuItem_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// OnlineDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.OnlineText);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "OnlineDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "へちまオンライン";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnlineDlg_FormClosed);
			this.Load += new System.EventHandler(this.OnlineDlg_Load);
			this.Shown += new System.EventHandler(this.OnlineDlg_Shown);
			this.MainContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox OnlineText;
		private System.Windows.Forms.ContextMenuStrip MainContextMenu;
		private System.Windows.Forms.ToolStripMenuItem 直ちに更新RToolStripMenuItem;
		private System.Windows.Forms.Timer MainTimer;
	}
}
