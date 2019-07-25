namespace Charlotte.Chocomint.Dialogs
{
	partial class InputTrackBarDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputTrackBarDlg));
			this.BarValue = new System.Windows.Forms.TrackBar();
			this.BtnOk = new System.Windows.Forms.Button();
			this.Prompt = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.CurrValue = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.BarValue)).BeginInit();
			this.SuspendLayout();
			// 
			// BarValue
			// 
			this.BarValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BarValue.Location = new System.Drawing.Point(34, 53);
			this.BarValue.Name = "BarValue";
			this.BarValue.Size = new System.Drawing.Size(520, 45);
			this.BarValue.TabIndex = 1;
			this.BarValue.Scroll += new System.EventHandler(this.BarValue_Scroll);
			this.BarValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BarValue_KeyPress);
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(366, 129);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(100, 50);
			this.BtnOk.TabIndex = 3;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// Prompt
			// 
			this.Prompt.AutoSize = true;
			this.Prompt.Location = new System.Drawing.Point(30, 30);
			this.Prompt.Name = "Prompt";
			this.Prompt.Size = new System.Drawing.Size(152, 20);
			this.Prompt.TabIndex = 0;
			this.Prompt.Text = "数値を入力して下さい。";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(472, 129);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(100, 50);
			this.BtnCancel.TabIndex = 4;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// CurrValue
			// 
			this.CurrValue.AutoSize = true;
			this.CurrValue.ForeColor = System.Drawing.Color.Teal;
			this.CurrValue.Location = new System.Drawing.Point(30, 101);
			this.CurrValue.Name = "CurrValue";
			this.CurrValue.Size = new System.Drawing.Size(115, 20);
			this.CurrValue.TabIndex = 2;
			this.CurrValue.Text = "準備しています...";
			// 
			// InputTrackBarDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 191);
			this.Controls.Add(this.CurrValue);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.Prompt);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BarValue);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputTrackBarDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "数値入力";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputTrackBarDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputTrackBarDlg_FormClosed);
			this.Load += new System.EventHandler(this.InputTrackBarDlg_Load);
			this.Shown += new System.EventHandler(this.InputTrackBarDlg_Shown);
			((System.ComponentModel.ISupportInitialize)(this.BarValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnOk;
		public System.Windows.Forms.Label Prompt;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Label CurrValue;
		public System.Windows.Forms.TrackBar BarValue;
	}
}
