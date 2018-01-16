namespace Charlotte
{
	partial class ResourceWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceWin));
			this.IGreen = new System.Windows.Forms.PictureBox();
			this.IYellow = new System.Windows.Forms.PictureBox();
			this.IRed = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.IGreen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.IYellow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.IRed)).BeginInit();
			this.SuspendLayout();
			// 
			// IGreen
			// 
			this.IGreen.Image = ((System.Drawing.Image)(resources.GetObject("IGreen.Image")));
			this.IGreen.Location = new System.Drawing.Point(12, 12);
			this.IGreen.Name = "IGreen";
			this.IGreen.Size = new System.Drawing.Size(100, 100);
			this.IGreen.TabIndex = 0;
			this.IGreen.TabStop = false;
			// 
			// IYellow
			// 
			this.IYellow.Image = ((System.Drawing.Image)(resources.GetObject("IYellow.Image")));
			this.IYellow.Location = new System.Drawing.Point(118, 12);
			this.IYellow.Name = "IYellow";
			this.IYellow.Size = new System.Drawing.Size(100, 100);
			this.IYellow.TabIndex = 1;
			this.IYellow.TabStop = false;
			// 
			// IRed
			// 
			this.IRed.Image = ((System.Drawing.Image)(resources.GetObject("IRed.Image")));
			this.IRed.Location = new System.Drawing.Point(224, 12);
			this.IRed.Name = "IRed";
			this.IRed.Size = new System.Drawing.Size(100, 100);
			this.IRed.TabIndex = 2;
			this.IRed.TabStop = false;
			// 
			// ResourceWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(817, 539);
			this.Controls.Add(this.IRed);
			this.Controls.Add(this.IYellow);
			this.Controls.Add(this.IGreen);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "ResourceWin";
			this.Text = "ResourceWin";
			this.Load += new System.EventHandler(this.ResourceWin_Load);
			((System.ComponentModel.ISupportInitialize)(this.IGreen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.IYellow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.IRed)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox IGreen;
		private System.Windows.Forms.PictureBox IYellow;
		private System.Windows.Forms.PictureBox IRed;
	}
}
