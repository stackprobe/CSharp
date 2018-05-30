namespace Charlotte
{
	partial class BacklogDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BacklogDlg));
			this.Backlog = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Backlog
			// 
			this.Backlog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Backlog.Location = new System.Drawing.Point(12, 12);
			this.Backlog.MaxLength = 10;
			this.Backlog.Name = "Backlog";
			this.Backlog.Size = new System.Drawing.Size(360, 27);
			this.Backlog.TabIndex = 1;
			this.Backlog.Text = "1234567890";
			this.Backlog.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Backlog.TextChanged += new System.EventHandler(this.Backlog_TextChanged);
			this.Backlog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Backlog_KeyPress);
			// 
			// BacklogDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 51);
			this.Controls.Add(this.Backlog);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BacklogDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SSRBServer2 / Backlog";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BacklogDlg_FormClosed);
			this.Load += new System.EventHandler(this.BacklogDlg_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Backlog;
	}
}
