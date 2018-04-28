namespace Charlotte
{
	partial class FontInfoDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontInfoDlg));
			this.Families = new System.Windows.Forms.ComboBox();
			this.CB太字 = new System.Windows.Forms.CheckBox();
			this.CB斜体 = new System.Windows.Forms.CheckBox();
			this.CB取り消し線 = new System.Windows.Forms.CheckBox();
			this.CB下線 = new System.Windows.Forms.CheckBox();
			this.SizeTxt = new System.Windows.Forms.TextBox();
			this.文字色Btn = new System.Windows.Forms.Button();
			this.L文字色 = new System.Windows.Forms.Label();
			this.SampleTxt = new System.Windows.Forms.TextBox();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.OKBtn = new System.Windows.Forms.Button();
			this.LErrorMessage = new System.Windows.Forms.Label();
			this.CB背景を暗く = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// Families
			// 
			this.Families.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Families.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Families.FormattingEnabled = true;
			this.Families.Location = new System.Drawing.Point(12, 12);
			this.Families.Name = "Families";
			this.Families.Size = new System.Drawing.Size(460, 28);
			this.Families.TabIndex = 0;
			this.Families.SelectedIndexChanged += new System.EventHandler(this.Families_SelectedIndexChanged);
			// 
			// CB太字
			// 
			this.CB太字.AutoSize = true;
			this.CB太字.Location = new System.Drawing.Point(12, 79);
			this.CB太字.Name = "CB太字";
			this.CB太字.Size = new System.Drawing.Size(54, 24);
			this.CB太字.TabIndex = 2;
			this.CB太字.Text = "太字";
			this.CB太字.UseVisualStyleBackColor = true;
			this.CB太字.CheckedChanged += new System.EventHandler(this.CB太字_CheckedChanged);
			// 
			// CB斜体
			// 
			this.CB斜体.AutoSize = true;
			this.CB斜体.Location = new System.Drawing.Point(72, 79);
			this.CB斜体.Name = "CB斜体";
			this.CB斜体.Size = new System.Drawing.Size(54, 24);
			this.CB斜体.TabIndex = 3;
			this.CB斜体.Text = "斜体";
			this.CB斜体.UseVisualStyleBackColor = true;
			this.CB斜体.CheckedChanged += new System.EventHandler(this.CB斜体_CheckedChanged);
			// 
			// CB取り消し線
			// 
			this.CB取り消し線.AutoSize = true;
			this.CB取り消し線.Location = new System.Drawing.Point(132, 79);
			this.CB取り消し線.Name = "CB取り消し線";
			this.CB取り消し線.Size = new System.Drawing.Size(93, 24);
			this.CB取り消し線.TabIndex = 4;
			this.CB取り消し線.Text = "取り消し線";
			this.CB取り消し線.UseVisualStyleBackColor = true;
			this.CB取り消し線.CheckedChanged += new System.EventHandler(this.CB取り消し線_CheckedChanged);
			// 
			// CB下線
			// 
			this.CB下線.AutoSize = true;
			this.CB下線.Location = new System.Drawing.Point(231, 79);
			this.CB下線.Name = "CB下線";
			this.CB下線.Size = new System.Drawing.Size(54, 24);
			this.CB下線.TabIndex = 5;
			this.CB下線.Text = "下線";
			this.CB下線.UseVisualStyleBackColor = true;
			this.CB下線.CheckedChanged += new System.EventHandler(this.CB下線_CheckedChanged);
			// 
			// SizeTxt
			// 
			this.SizeTxt.Location = new System.Drawing.Point(12, 46);
			this.SizeTxt.MaxLength = 2;
			this.SizeTxt.Name = "SizeTxt";
			this.SizeTxt.Size = new System.Drawing.Size(114, 27);
			this.SizeTxt.TabIndex = 1;
			this.SizeTxt.Text = "99";
			this.SizeTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.SizeTxt.TextChanged += new System.EventHandler(this.SizeTxt_TextChanged);
			// 
			// 文字色Btn
			// 
			this.文字色Btn.Location = new System.Drawing.Point(12, 109);
			this.文字色Btn.Name = "文字色Btn";
			this.文字色Btn.Size = new System.Drawing.Size(114, 40);
			this.文字色Btn.TabIndex = 6;
			this.文字色Btn.Text = "文字色";
			this.文字色Btn.UseVisualStyleBackColor = true;
			this.文字色Btn.Click += new System.EventHandler(this.文字色Btn_Click);
			// 
			// L文字色
			// 
			this.L文字色.AutoSize = true;
			this.L文字色.Location = new System.Drawing.Point(132, 119);
			this.L文字色.Name = "L文字色";
			this.L文字色.Size = new System.Drawing.Size(57, 20);
			this.L文字色.TabIndex = 7;
			this.L文字色.Text = "000000";
			// 
			// SampleTxt
			// 
			this.SampleTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SampleTxt.Location = new System.Drawing.Point(12, 155);
			this.SampleTxt.MaxLength = 1000;
			this.SampleTxt.Multiline = true;
			this.SampleTxt.Name = "SampleTxt";
			this.SampleTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.SampleTxt.Size = new System.Drawing.Size(460, 228);
			this.SampleTxt.TabIndex = 9;
			this.SampleTxt.Text = "いろはにほへと\r\nチリヌルヲ\r\n大三元\r\nドラ40\r\nABCDEFGHIJKLMNOPQRSTUVWXYZ\r\nabcdefghijklmnopqrstuvwxyz" +
    "\r\n0123456789\r\nほげインの森\r\nForest of hoge-in\r\n_(:3 」∠ )_(:3 」∠ )_";
			// 
			// CancelBtn
			// 
			this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBtn.Location = new System.Drawing.Point(358, 409);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(114, 40);
			this.CancelBtn.TabIndex = 12;
			this.CancelBtn.Text = "キャンセル";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// OKBtn
			// 
			this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OKBtn.Location = new System.Drawing.Point(238, 409);
			this.OKBtn.Name = "OKBtn";
			this.OKBtn.Size = new System.Drawing.Size(114, 40);
			this.OKBtn.TabIndex = 11;
			this.OKBtn.Text = "OK";
			this.OKBtn.UseVisualStyleBackColor = true;
			this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
			// 
			// LErrorMessage
			// 
			this.LErrorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LErrorMessage.AutoSize = true;
			this.LErrorMessage.BackColor = System.Drawing.Color.Yellow;
			this.LErrorMessage.ForeColor = System.Drawing.Color.Red;
			this.LErrorMessage.Location = new System.Drawing.Point(12, 386);
			this.LErrorMessage.Name = "LErrorMessage";
			this.LErrorMessage.Size = new System.Drawing.Size(438, 20);
			this.LErrorMessage.TabIndex = 10;
			this.LErrorMessage.Text = "ここにエラーメッセージを表示します。あああああいいいいいううううう";
			// 
			// CB背景を暗く
			// 
			this.CB背景を暗く.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CB背景を暗く.AutoSize = true;
			this.CB背景を暗く.Location = new System.Drawing.Point(379, 125);
			this.CB背景を暗く.Name = "CB背景を暗く";
			this.CB背景を暗く.Size = new System.Drawing.Size(93, 24);
			this.CB背景を暗く.TabIndex = 8;
			this.CB背景を暗く.Text = "背景を暗く";
			this.CB背景を暗く.UseVisualStyleBackColor = true;
			this.CB背景を暗く.CheckedChanged += new System.EventHandler(this.CB背景を暗く_CheckedChanged);
			// 
			// FontInfoDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.CB背景を暗く);
			this.Controls.Add(this.LErrorMessage);
			this.Controls.Add(this.OKBtn);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.SampleTxt);
			this.Controls.Add(this.L文字色);
			this.Controls.Add(this.文字色Btn);
			this.Controls.Add(this.SizeTxt);
			this.Controls.Add(this.CB下線);
			this.Controls.Add(this.CB取り消し線);
			this.Controls.Add(this.CB斜体);
			this.Controls.Add(this.CB太字);
			this.Controls.Add(this.Families);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FontInfoDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "へちま改のフォント設定";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FontInfoDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FontInfoDlg_FormClosed);
			this.Load += new System.EventHandler(this.FontInfoDlg_Load);
			this.Shown += new System.EventHandler(this.FontInfoDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox Families;
		private System.Windows.Forms.CheckBox CB太字;
		private System.Windows.Forms.CheckBox CB斜体;
		private System.Windows.Forms.CheckBox CB取り消し線;
		private System.Windows.Forms.CheckBox CB下線;
		private System.Windows.Forms.TextBox SizeTxt;
		private System.Windows.Forms.Button 文字色Btn;
		private System.Windows.Forms.Label L文字色;
		private System.Windows.Forms.TextBox SampleTxt;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button OKBtn;
		private System.Windows.Forms.Label LErrorMessage;
		private System.Windows.Forms.CheckBox CB背景を暗く;
	}
}
