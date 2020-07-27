namespace Charlotte.Chocomint.Dialogs
{
	partial class WaitDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitDlg));
			this.Message = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.DetailMessage = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// Message
			// 
			this.Message.AutoSize = true;
			this.Message.Location = new System.Drawing.Point(40, 40);
			this.Message.Name = "Message";
			this.Message.Size = new System.Drawing.Size(113, 20);
			this.Message.TabIndex = 0;
			this.Message.Text = "処理しています。";
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressBar.Location = new System.Drawing.Point(12, 95);
			this.ProgressBar.Maximum = 1000;
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(560, 30);
			this.ProgressBar.TabIndex = 1;
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(366, 300);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(206, 50);
			this.BtnCancel.TabIndex = 3;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// DetailMessage
			// 
			this.DetailMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DetailMessage.Location = new System.Drawing.Point(12, 131);
			this.DetailMessage.Multiline = true;
			this.DetailMessage.Name = "DetailMessage";
			this.DetailMessage.ReadOnly = true;
			this.DetailMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.DetailMessage.Size = new System.Drawing.Size(560, 163);
			this.DetailMessage.TabIndex = 2;
			this.DetailMessage.Text = "1行目\r\n2行目\r\n3行目\r\n4行目\r\n5行目\r\n6行目\r\n7行目";
			this.DetailMessage.WordWrap = false;
			// 
			// WaitDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 362);
			this.Controls.Add(this.DetailMessage);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.Message);
			this.Controls.Add(this.ProgressBar);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WaitDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "処理中";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WaitDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaitDlg_FormClosed);
			this.Load += new System.EventHandler(this.WaitDlg_Load);
			this.Shown += new System.EventHandler(this.WaitDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label Message;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.TextBox DetailMessage;
	}
}
