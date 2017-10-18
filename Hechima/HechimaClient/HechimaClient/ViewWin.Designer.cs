namespace Charlotte
{
	partial class ViewWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewWin));
			this.RemarksText = new System.Windows.Forms.TextBox();
			this.ViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.閉じるCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// RemarksText
			// 
			this.RemarksText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RemarksText.ContextMenuStrip = this.ViewContextMenu;
			this.RemarksText.Location = new System.Drawing.Point(12, 12);
			this.RemarksText.Multiline = true;
			this.RemarksText.Name = "RemarksText";
			this.RemarksText.ReadOnly = true;
			this.RemarksText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.RemarksText.Size = new System.Drawing.Size(460, 438);
			this.RemarksText.TabIndex = 0;
			this.RemarksText.TextChanged += new System.EventHandler(this.RemarksText_TextChanged);
			this.RemarksText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RemarksText_KeyPress);
			// 
			// ViewContextMenu
			// 
			this.ViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.閉じるCToolStripMenuItem});
			this.ViewContextMenu.Name = "ViewContextMenu";
			this.ViewContextMenu.Size = new System.Drawing.Size(131, 26);
			// 
			// 閉じるCToolStripMenuItem
			// 
			this.閉じるCToolStripMenuItem.Name = "閉じるCToolStripMenuItem";
			this.閉じるCToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.閉じるCToolStripMenuItem.Text = "閉じる(&C)";
			this.閉じるCToolStripMenuItem.Click += new System.EventHandler(this.閉じるCToolStripMenuItem_Click);
			// 
			// ViewWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 462);
			this.Controls.Add(this.RemarksText);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "ViewWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "へちまビュー";
			this.Load += new System.EventHandler(this.WiewWin_Load);
			this.Shown += new System.EventHandler(this.WiewWin_Shown);
			this.ViewContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox RemarksText;
		private System.Windows.Forms.ContextMenuStrip ViewContextMenu;
		private System.Windows.Forms.ToolStripMenuItem 閉じるCToolStripMenuItem;
	}
}
