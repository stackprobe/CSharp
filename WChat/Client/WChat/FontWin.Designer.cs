namespace Charlotte
{
	partial class FontWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontWin));
			this.FontName = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.FontSize = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.FSBold = new System.Windows.Forms.CheckBox();
			this.FSItalic = new System.Windows.Forms.CheckBox();
			this.FSStrikeout = new System.Windows.Forms.CheckBox();
			this.FSUnderline = new System.Windows.Forms.CheckBox();
			this.BtnColor = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.BtnOk = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.サンプル = new System.Windows.Forms.TextBox();
			this.FontColor = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.groupBox1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// FontName
			// 
			this.FontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FontName.FormattingEnabled = true;
			this.FontName.Location = new System.Drawing.Point(114, 26);
			this.FontName.Name = "FontName";
			this.FontName.Size = new System.Drawing.Size(357, 28);
			this.FontName.TabIndex = 1;
			this.FontName.SelectedIndexChanged += new System.EventHandler(this.FontName_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "フォント名";
			// 
			// FontSize
			// 
			this.FontSize.Location = new System.Drawing.Point(114, 69);
			this.FontSize.MaxLength = 3;
			this.FontSize.Name = "FontSize";
			this.FontSize.Size = new System.Drawing.Size(114, 27);
			this.FontSize.TabIndex = 3;
			this.FontSize.Text = "108";
			this.FontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.FontSize.TextChanged += new System.EventHandler(this.FontSize_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "サイズ";
			// 
			// FSBold
			// 
			this.FSBold.AutoSize = true;
			this.FSBold.Location = new System.Drawing.Point(114, 102);
			this.FSBold.Name = "FSBold";
			this.FSBold.Size = new System.Drawing.Size(54, 24);
			this.FSBold.TabIndex = 5;
			this.FSBold.Text = "太字";
			this.FSBold.UseVisualStyleBackColor = true;
			this.FSBold.CheckedChanged += new System.EventHandler(this.FSBold_CheckedChanged);
			// 
			// FSItalic
			// 
			this.FSItalic.AutoSize = true;
			this.FSItalic.Location = new System.Drawing.Point(174, 102);
			this.FSItalic.Name = "FSItalic";
			this.FSItalic.Size = new System.Drawing.Size(54, 24);
			this.FSItalic.TabIndex = 6;
			this.FSItalic.Text = "斜体";
			this.FSItalic.UseVisualStyleBackColor = true;
			this.FSItalic.CheckedChanged += new System.EventHandler(this.FSItalic_CheckedChanged);
			// 
			// FSStrikeout
			// 
			this.FSStrikeout.AutoSize = true;
			this.FSStrikeout.Location = new System.Drawing.Point(234, 102);
			this.FSStrikeout.Name = "FSStrikeout";
			this.FSStrikeout.Size = new System.Drawing.Size(93, 24);
			this.FSStrikeout.TabIndex = 7;
			this.FSStrikeout.Text = "取り消し線";
			this.FSStrikeout.UseVisualStyleBackColor = true;
			this.FSStrikeout.CheckedChanged += new System.EventHandler(this.FSStrikeout_CheckedChanged);
			// 
			// FSUnderline
			// 
			this.FSUnderline.AutoSize = true;
			this.FSUnderline.Location = new System.Drawing.Point(333, 102);
			this.FSUnderline.Name = "FSUnderline";
			this.FSUnderline.Size = new System.Drawing.Size(54, 24);
			this.FSUnderline.TabIndex = 8;
			this.FSUnderline.Text = "下線";
			this.FSUnderline.UseVisualStyleBackColor = true;
			this.FSUnderline.CheckedChanged += new System.EventHandler(this.FSUnderline_CheckedChanged);
			// 
			// BtnColor
			// 
			this.BtnColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnColor.Location = new System.Drawing.Point(114, 132);
			this.BtnColor.Name = "BtnColor";
			this.BtnColor.Size = new System.Drawing.Size(273, 27);
			this.BtnColor.TabIndex = 9;
			this.BtnColor.Text = "色";
			this.BtnColor.UseVisualStyleBackColor = true;
			this.BtnColor.Click += new System.EventHandler(this.BtnColor_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "スタイル";
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(271, 325);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(103, 44);
			this.BtnOk.TabIndex = 12;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(380, 325);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(103, 44);
			this.BtnCancel.TabIndex = 13;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.サンプル);
			this.groupBox1.Location = new System.Drawing.Point(12, 165);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(471, 154);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "サンプル";
			// 
			// サンプル
			// 
			this.サンプル.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.サンプル.Location = new System.Drawing.Point(6, 26);
			this.サンプル.Multiline = true;
			this.サンプル.Name = "サンプル";
			this.サンプル.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.サンプル.Size = new System.Drawing.Size(459, 122);
			this.サンプル.TabIndex = 0;
			this.サンプル.Text = "!#$%&()*+,-./あいう日本語123";
			// 
			// FontColor
			// 
			this.FontColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.FontColor.Location = new System.Drawing.Point(393, 132);
			this.FontColor.Name = "FontColor";
			this.FontColor.ReadOnly = true;
			this.FontColor.Size = new System.Drawing.Size(78, 27);
			this.FontColor.TabIndex = 10;
			this.FontColor.Text = "999999";
			this.FontColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMessage});
			this.statusStrip1.Location = new System.Drawing.Point(0, 380);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(495, 23);
			this.statusStrip1.TabIndex = 14;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusMessage
			// 
			this.StatusMessage.Name = "StatusMessage";
			this.StatusMessage.Size = new System.Drawing.Size(449, 18);
			this.StatusMessage.Spring = true;
			this.StatusMessage.Text = "準備しています...";
			this.StatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FontWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(495, 403);
			this.Controls.Add(this.FontColor);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.BtnColor);
			this.Controls.Add(this.FSUnderline);
			this.Controls.Add(this.FSStrikeout);
			this.Controls.Add(this.FSItalic);
			this.Controls.Add(this.FSBold);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.FontSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FontName);
			this.Controls.Add(this.statusStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "FontWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "フォントの設定";
			this.Load += new System.EventHandler(this.FontWin_Load);
			this.Shown += new System.EventHandler(this.FontWin_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox FontName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox FontSize;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox FSBold;
		private System.Windows.Forms.CheckBox FSItalic;
		private System.Windows.Forms.CheckBox FSStrikeout;
		private System.Windows.Forms.CheckBox FSUnderline;
		private System.Windows.Forms.Button BtnColor;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox サンプル;
		private System.Windows.Forms.TextBox FontColor;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel StatusMessage;

	}
}
