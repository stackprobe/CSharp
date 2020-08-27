namespace t0001
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
			this.Str = new System.Windows.Forms.TextBox();
			this.Exp = new System.Windows.Forms.TextBox();
			this.Res = new System.Windows.Forms.TextBox();
			this.IgnCase = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// Str
			// 
			this.Str.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Str.Location = new System.Drawing.Point(12, 12);
			this.Str.Name = "Str";
			this.Str.Size = new System.Drawing.Size(360, 27);
			this.Str.TabIndex = 0;
			this.Str.Text = "C:\\temp\\Regex_TEST20180515.dat";
			this.Str.TextChanged += new System.EventHandler(this.Str_TextChanged);
			// 
			// Exp
			// 
			this.Exp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Exp.Location = new System.Drawing.Point(12, 45);
			this.Exp.Name = "Exp";
			this.Exp.Size = new System.Drawing.Size(360, 27);
			this.Exp.TabIndex = 1;
			this.Exp.Text = "\\\\Regex_TEST[0-9]{8}.dat$";
			this.Exp.TextChanged += new System.EventHandler(this.Exp_TextChanged);
			// 
			// Res
			// 
			this.Res.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Res.Location = new System.Drawing.Point(12, 108);
			this.Res.Name = "Res";
			this.Res.Size = new System.Drawing.Size(360, 27);
			this.Res.TabIndex = 3;
			// 
			// IgnCase
			// 
			this.IgnCase.AutoSize = true;
			this.IgnCase.Checked = true;
			this.IgnCase.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IgnCase.Location = new System.Drawing.Point(12, 78);
			this.IgnCase.Name = "IgnCase";
			this.IgnCase.Size = new System.Drawing.Size(101, 24);
			this.IgnCase.TabIndex = 2;
			this.IgnCase.Text = "IgnoreCase";
			this.IgnCase.UseVisualStyleBackColor = true;
			this.IgnCase.CheckedChanged += new System.EventHandler(this.IgnCase_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 161);
			this.Controls.Add(this.IgnCase);
			this.Controls.Add(this.Res);
			this.Controls.Add(this.Exp);
			this.Controls.Add(this.Str);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "t1001";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Str;
		private System.Windows.Forms.TextBox Exp;
		private System.Windows.Forms.TextBox Res;
		private System.Windows.Forms.CheckBox IgnCase;
	}
}

