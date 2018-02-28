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
			this.BtnAbandon = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// BtnAbandon
			// 
			this.BtnAbandon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnAbandon.Enabled = false;
			this.BtnAbandon.Location = new System.Drawing.Point(162, 89);
			this.BtnAbandon.Name = "BtnAbandon";
			this.BtnAbandon.Size = new System.Drawing.Size(150, 40);
			this.BtnAbandon.TabIndex = 1;
			this.BtnAbandon.Text = "強制終了(&A)";
			this.BtnAbandon.UseVisualStyleBackColor = true;
			this.BtnAbandon.Click += new System.EventHandler(this.BtnAbandon_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(206, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "サーバーの応答を待っています...";
			// 
			// MainTimer
			// 
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// StopServerDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 141);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BtnAbandon);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StopServerDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SSRBServer2";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StopServerDlg_FormClosed);
			this.Load += new System.EventHandler(this.StopServerDlg_Load);
			this.Shown += new System.EventHandler(this.StopServerDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnAbandon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Timer MainTimer;
	}
}
