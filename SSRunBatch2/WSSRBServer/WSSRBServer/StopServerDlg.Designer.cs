namespace Charlotte
{
	partial class StopServerDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StopServerDlg));
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.XLabel = new System.Windows.Forms.Label();
			this.BtnForceStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(206, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "サーバーの応答を待っています...";
			// 
			// XLabel
			// 
			this.XLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.XLabel.AutoSize = true;
			this.XLabel.BackColor = System.Drawing.Color.Maroon;
			this.XLabel.ForeColor = System.Drawing.Color.White;
			this.XLabel.Location = new System.Drawing.Point(405, 9);
			this.XLabel.Name = "XLabel";
			this.XLabel.Size = new System.Drawing.Size(107, 20);
			this.XLabel.TabIndex = 1;
			this.XLabel.Text = "[x] not working";
			// 
			// BtnForceStop
			// 
			this.BtnForceStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnForceStop.Enabled = false;
			this.BtnForceStop.Location = new System.Drawing.Point(372, 50);
			this.BtnForceStop.Name = "BtnForceStop";
			this.BtnForceStop.Size = new System.Drawing.Size(140, 40);
			this.BtnForceStop.TabIndex = 2;
			this.BtnForceStop.Text = "強制終了(&A)";
			this.BtnForceStop.UseVisualStyleBackColor = true;
			this.BtnForceStop.Click += new System.EventHandler(this.BtnForceStop_Click);
			// 
			// StopServerDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 102);
			this.Controls.Add(this.BtnForceStop);
			this.Controls.Add(this.XLabel);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StopServerDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WSSRBServer";
			this.Load += new System.EventHandler(this.SockServerWaitToStopDlg_Load);
			this.Shown += new System.EventHandler(this.SockServerWaitToStopDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label XLabel;
		private System.Windows.Forms.Button BtnForceStop;
	}
}
