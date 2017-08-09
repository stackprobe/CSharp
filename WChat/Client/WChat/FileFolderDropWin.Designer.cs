namespace Charlotte
{
	partial class FileFolderDropWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileFolderDropWin));
			this.MainLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// MainLabel
			// 
			this.MainLabel.AutoSize = true;
			this.MainLabel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.MainLabel.ForeColor = System.Drawing.Color.Gray;
			this.MainLabel.Location = new System.Drawing.Point(12, 9);
			this.MainLabel.Name = "MainLabel";
			this.MainLabel.Size = new System.Drawing.Size(256, 80);
			this.MainLabel.TabIndex = 0;
			this.MainLabel.Text = "ここにオブジェクトをドロップして下さい\r\n複数のオブジェクトを貼り付けられます\r\n10件毎にリマークします\r\n最大100件まで";
			// 
			// FileFolderDropWin
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(431, 345);
			this.Controls.Add(this.MainLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FileFolderDropWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Chat - ファイル又はフォルダをドロップ";
			this.Load += new System.EventHandler(this.FileFolderDropWin_Load);
			this.Shown += new System.EventHandler(this.FileFolderDropWin_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileFolderDropWin_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileFolderDropWin_DragEnter);
			this.Resize += new System.EventHandler(this.FileFolderDropWin_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label MainLabel;
	}
}
