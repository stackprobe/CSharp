namespace Test02
{
	partial class MainWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.BtnInputStringDlg = new System.Windows.Forms.Button();
			this.BtnGo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnInputStringDlg
			// 
			this.BtnInputStringDlg.Location = new System.Drawing.Point(12, 68);
			this.BtnInputStringDlg.Name = "BtnInputStringDlg";
			this.BtnInputStringDlg.Size = new System.Drawing.Size(200, 50);
			this.BtnInputStringDlg.TabIndex = 1;
			this.BtnInputStringDlg.Text = "InputStringDlg";
			this.BtnInputStringDlg.UseVisualStyleBackColor = true;
			this.BtnInputStringDlg.Click += new System.EventHandler(this.BtnInputStringDlg_Click);
			// 
			// BtnGo
			// 
			this.BtnGo.Location = new System.Drawing.Point(12, 12);
			this.BtnGo.Name = "BtnGo";
			this.BtnGo.Size = new System.Drawing.Size(200, 50);
			this.BtnGo.TabIndex = 0;
			this.BtnGo.Text = "Go";
			this.BtnGo.UseVisualStyleBackColor = true;
			this.BtnGo.Click += new System.EventHandler(this.BtnGo_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.BtnGo);
			this.Controls.Add(this.BtnInputStringDlg);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Chocomint / Test02";
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnInputStringDlg;
		private System.Windows.Forms.Button BtnGo;
	}
}

