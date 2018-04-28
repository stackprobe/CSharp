namespace Charlotte
{
	partial class MemberFontDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberFontDlg));
			this.IdentMidPtn = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.StampBtn = new System.Windows.Forms.Button();
			this.OKBtn = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.LStamp = new System.Windows.Forms.Label();
			this.IdentBtn = new System.Windows.Forms.Button();
			this.MessageBtn = new System.Windows.Forms.Button();
			this.LIdent = new System.Windows.Forms.Label();
			this.LMessage = new System.Windows.Forms.Label();
			this.T最近のIdents = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.CorrectBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// IdentMidPtn
			// 
			this.IdentMidPtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.IdentMidPtn.Location = new System.Drawing.Point(12, 32);
			this.IdentMidPtn.MaxLength = 100;
			this.IdentMidPtn.Name = "IdentMidPtn";
			this.IdentMidPtn.Size = new System.Drawing.Size(460, 27);
			this.IdentMidPtn.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(217, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "名前・トリップに含まれる文字列：";
			// 
			// StampBtn
			// 
			this.StampBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StampBtn.Location = new System.Drawing.Point(12, 271);
			this.StampBtn.Name = "StampBtn";
			this.StampBtn.Size = new System.Drawing.Size(114, 40);
			this.StampBtn.TabIndex = 4;
			this.StampBtn.Text = "日時";
			this.StampBtn.UseVisualStyleBackColor = true;
			this.StampBtn.Click += new System.EventHandler(this.StampBtn_Click);
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
			// LStamp
			// 
			this.LStamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LStamp.AutoSize = true;
			this.LStamp.Location = new System.Drawing.Point(132, 281);
			this.LStamp.Name = "LStamp";
			this.LStamp.Size = new System.Drawing.Size(243, 20);
			this.LStamp.TabIndex = 5;
			this.LStamp.Text = "フォント情報ああああいいいいうううう";
			// 
			// IdentBtn
			// 
			this.IdentBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.IdentBtn.Location = new System.Drawing.Point(12, 317);
			this.IdentBtn.Name = "IdentBtn";
			this.IdentBtn.Size = new System.Drawing.Size(114, 40);
			this.IdentBtn.TabIndex = 6;
			this.IdentBtn.Text = "ident";
			this.IdentBtn.UseVisualStyleBackColor = true;
			this.IdentBtn.Click += new System.EventHandler(this.IdentBtn_Click);
			// 
			// MessageBtn
			// 
			this.MessageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MessageBtn.Location = new System.Drawing.Point(12, 363);
			this.MessageBtn.Name = "MessageBtn";
			this.MessageBtn.Size = new System.Drawing.Size(114, 40);
			this.MessageBtn.TabIndex = 8;
			this.MessageBtn.Text = "メッセージ";
			this.MessageBtn.UseVisualStyleBackColor = true;
			this.MessageBtn.Click += new System.EventHandler(this.MessageBtn_Click);
			// 
			// LIdent
			// 
			this.LIdent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LIdent.AutoSize = true;
			this.LIdent.Location = new System.Drawing.Point(132, 327);
			this.LIdent.Name = "LIdent";
			this.LIdent.Size = new System.Drawing.Size(243, 20);
			this.LIdent.TabIndex = 7;
			this.LIdent.Text = "フォント情報ああああいいいいうううう";
			// 
			// LMessage
			// 
			this.LMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LMessage.AutoSize = true;
			this.LMessage.Location = new System.Drawing.Point(132, 373);
			this.LMessage.Name = "LMessage";
			this.LMessage.Size = new System.Drawing.Size(243, 20);
			this.LMessage.TabIndex = 9;
			this.LMessage.Text = "フォント情報ああああいいいいうううう";
			// 
			// T最近のIdents
			// 
			this.T最近のIdents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.T最近のIdents.Location = new System.Drawing.Point(12, 85);
			this.T最近のIdents.Multiline = true;
			this.T最近のIdents.Name = "T最近のIdents";
			this.T最近のIdents.ReadOnly = true;
			this.T最近のIdents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.T最近のIdents.Size = new System.Drawing.Size(460, 180);
			this.T最近のIdents.TabIndex = 3;
			this.T最近のIdents.WordWrap = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(152, 20);
			this.label5.TabIndex = 2;
			this.label5.Text = "最近の名前・トリップ：";
			// 
			// CorrectBtn
			// 
			this.CorrectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CorrectBtn.Location = new System.Drawing.Point(12, 409);
			this.CorrectBtn.Name = "CorrectBtn";
			this.CorrectBtn.Size = new System.Drawing.Size(114, 40);
			this.CorrectBtn.TabIndex = 10;
			this.CorrectBtn.Text = "補正";
			this.CorrectBtn.UseVisualStyleBackColor = true;
			this.CorrectBtn.Click += new System.EventHandler(this.CorrectBtn_Click);
			// 
			// MemberFontDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.CorrectBtn);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.T最近のIdents);
			this.Controls.Add(this.LMessage);
			this.Controls.Add(this.LIdent);
			this.Controls.Add(this.MessageBtn);
			this.Controls.Add(this.IdentBtn);
			this.Controls.Add(this.LStamp);
			this.Controls.Add(this.OKBtn);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.StampBtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.IdentMidPtn);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MemberFontDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MemberFontDlg";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemberFontDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MemberFontDlg_FormClosed);
			this.Load += new System.EventHandler(this.MemberFontDlg_Load);
			this.Shown += new System.EventHandler(this.MemberFontDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox IdentMidPtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button StampBtn;
		private System.Windows.Forms.Button OKBtn;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Label LStamp;
		private System.Windows.Forms.Button IdentBtn;
		private System.Windows.Forms.Button MessageBtn;
		private System.Windows.Forms.Label LIdent;
		private System.Windows.Forms.Label LMessage;
		private System.Windows.Forms.TextBox T最近のIdents;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button CorrectBtn;
	}
}
