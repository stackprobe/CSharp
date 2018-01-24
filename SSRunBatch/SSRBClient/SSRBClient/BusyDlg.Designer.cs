namespace Charlotte
{
	partial class BusyDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusyDlg));
			this.label1 = new System.Windows.Forms.Label();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.XLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(259, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "SSRBServer からの応答を待っています...";
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// XLabel
			// 
			this.XLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.XLabel.AutoSize = true;
			this.XLabel.BackColor = System.Drawing.Color.Maroon;
			this.XLabel.ForeColor = System.Drawing.Color.White;
			this.XLabel.Location = new System.Drawing.Point(375, 9);
			this.XLabel.Name = "XLabel";
			this.XLabel.Size = new System.Drawing.Size(107, 20);
			this.XLabel.TabIndex = 1;
			this.XLabel.Text = "[x] not working";
			// 
			// BusyDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 82);
			this.Controls.Add(this.XLabel);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BusyDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SSRBClient / 通信中";
			this.Load += new System.EventHandler(this.BusyDlg_Load);
			this.Shown += new System.EventHandler(this.BusyDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.Label XLabel;
	}
}
