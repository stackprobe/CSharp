namespace Charlotte
{
	partial class ColorWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorWin));
			this.BtnOk = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.ValText = new System.Windows.Forms.TextBox();
			this.BtnColorDlg = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.サンプル = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(151, 157);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(103, 44);
			this.BtnOk.TabIndex = 3;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(260, 157);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(103, 44);
			this.BtnCancel.TabIndex = 4;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// ValText
			// 
			this.ValText.Location = new System.Drawing.Point(12, 12);
			this.ValText.MaxLength = 6;
			this.ValText.Name = "ValText";
			this.ValText.Size = new System.Drawing.Size(103, 27);
			this.ValText.TabIndex = 0;
			this.ValText.Text = "999999";
			this.ValText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ValText.TextChanged += new System.EventHandler(this.ValText_TextChanged);
			// 
			// BtnColorDlg
			// 
			this.BtnColorDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtnColorDlg.Location = new System.Drawing.Point(12, 157);
			this.BtnColorDlg.Name = "BtnColorDlg";
			this.BtnColorDlg.Size = new System.Drawing.Size(103, 44);
			this.BtnColorDlg.TabIndex = 2;
			this.BtnColorDlg.Text = "選択";
			this.BtnColorDlg.UseVisualStyleBackColor = true;
			this.BtnColorDlg.Click += new System.EventHandler(this.BtnColorDlg_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.サンプル);
			this.groupBox1.Location = new System.Drawing.Point(12, 45);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(351, 106);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "サンプル";
			// 
			// サンプル
			// 
			this.サンプル.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.サンプル.BackColor = System.Drawing.Color.Teal;
			this.サンプル.Location = new System.Drawing.Point(6, 26);
			this.サンプル.Name = "サンプル";
			this.サンプル.Size = new System.Drawing.Size(339, 74);
			this.サンプル.TabIndex = 0;
			// 
			// ColorWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 213);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.BtnColorDlg);
			this.Controls.Add(this.ValText);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.BtnCancel);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "ColorWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "色の設定";
			this.Load += new System.EventHandler(this.ColorWin_Load);
			this.Shown += new System.EventHandler(this.ColorWin_Shown);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.TextBox ValText;
		private System.Windows.Forms.Button BtnColorDlg;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel サンプル;
	}
}
