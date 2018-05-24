namespace Charlotte
{
	partial class StopTSRDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StopTSRDlg));
			this.LMessage = new System.Windows.Forms.Label();
			this.BtnAbandon = new System.Windows.Forms.Button();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.Status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// LMessage
			// 
			this.LMessage.AutoSize = true;
			this.LMessage.ForeColor = System.Drawing.Color.White;
			this.LMessage.Location = new System.Drawing.Point(12, 9);
			this.LMessage.Name = "LMessage";
			this.LMessage.Size = new System.Drawing.Size(274, 20);
			this.LMessage.TabIndex = 0;
			this.LMessage.Text = "TSR バッチファイルの終了を待っています...";
			// 
			// BtnAbandon
			// 
			this.BtnAbandon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnAbandon.Location = new System.Drawing.Point(128, 48);
			this.BtnAbandon.Name = "BtnAbandon";
			this.BtnAbandon.Size = new System.Drawing.Size(400, 40);
			this.BtnAbandon.TabIndex = 1;
			this.BtnAbandon.Text = "現在実行中の TSR バッチファイルを全て強制終了する(&A)";
			this.BtnAbandon.UseVisualStyleBackColor = true;
			this.BtnAbandon.Click += new System.EventHandler(this.BtnAbandon_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// Status
			// 
			this.Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Status.AutoSize = true;
			this.Status.ForeColor = System.Drawing.Color.Yellow;
			this.Status.Location = new System.Drawing.Point(452, 9);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(76, 20);
			this.Status.TabIndex = 2;
			this.Status.Text = "TSR = 999";
			// 
			// StopTSRDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ClientSize = new System.Drawing.Size(540, 100);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.BtnAbandon);
			this.Controls.Add(this.LMessage);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StopTSRDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SSRBServer3";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StopTSRDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StopTSRDlg_FormClosed);
			this.Load += new System.EventHandler(this.StopTSRDlg_Load);
			this.Shown += new System.EventHandler(this.StopTSRDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LMessage;
		private System.Windows.Forms.Button BtnAbandon;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.Label Status;
	}
}
