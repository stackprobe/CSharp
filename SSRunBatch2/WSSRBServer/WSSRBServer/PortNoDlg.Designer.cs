namespace Charlotte
{
	partial class PortNoDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortNoDlg));
			this.PortNo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// PortNo
			// 
			this.PortNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PortNo.Location = new System.Drawing.Point(12, 12);
			this.PortNo.MaxLength = 5;
			this.PortNo.Name = "PortNo";
			this.PortNo.Size = new System.Drawing.Size(360, 27);
			this.PortNo.TabIndex = 0;
			this.PortNo.Text = "65535";
			this.PortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PortNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortNo_KeyPress);
			// 
			// PortNoDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 51);
			this.Controls.Add(this.PortNo);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PortNoDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SSRBServer2 / ポート番号";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PortNoDlg_FormClosed);
			this.Load += new System.EventHandler(this.PortNoDlg_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox PortNo;

	}
}
