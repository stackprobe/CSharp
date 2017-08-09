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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.Elapsed = new System.Windows.Forms.Label();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.BtnCancel = new System.Windows.Forms.Button();
			this.ChkBoxCancel = new System.Windows.Forms.CheckBox();
			this.Status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(12, 77);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(563, 27);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 1;
			// 
			// Elapsed
			// 
			this.Elapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Elapsed.AutoSize = true;
			this.Elapsed.Location = new System.Drawing.Point(12, 127);
			this.Elapsed.Name = "Elapsed";
			this.Elapsed.Size = new System.Drawing.Size(317, 20);
			this.Elapsed.TabIndex = 0;
			this.Elapsed.Text = "経過時間 ... だいたい 1234567890 分 59 秒 くらい";
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Enabled = false;
			this.BtnCancel.Location = new System.Drawing.Point(482, 119);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(93, 37);
			this.BtnCancel.TabIndex = 3;
			this.BtnCancel.Text = "中止";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// ChkBoxCancel
			// 
			this.ChkBoxCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ChkBoxCancel.AutoSize = true;
			this.ChkBoxCancel.Location = new System.Drawing.Point(383, 126);
			this.ChkBoxCancel.Name = "ChkBoxCancel";
			this.ChkBoxCancel.Size = new System.Drawing.Size(93, 24);
			this.ChkBoxCancel.TabIndex = 2;
			this.ChkBoxCancel.Text = "中止ボタン";
			this.ChkBoxCancel.UseVisualStyleBackColor = true;
			this.ChkBoxCancel.CheckedChanged += new System.EventHandler(this.ChkBoxCancel_CheckedChanged);
			// 
			// Status
			// 
			this.Status.AutoSize = true;
			this.Status.Location = new System.Drawing.Point(25, 31);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(115, 20);
			this.Status.TabIndex = 4;
			this.Status.Text = "準備しています...";
			// 
			// BusyDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(587, 168);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.ChkBoxCancel);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.Elapsed);
			this.Controls.Add(this.progressBar1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "BusyDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FatCalc - 計算しています...";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BusyDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BusyDlg_FormClosed);
			this.Load += new System.EventHandler(this.BusyDlg_Load);
			this.Shown += new System.EventHandler(this.BusyDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label Elapsed;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.CheckBox ChkBoxCancel;
		private System.Windows.Forms.Label Status;
	}
}
