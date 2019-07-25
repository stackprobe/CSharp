namespace Charlotte.Chocomint.Dialogs.Resource
{
	partial class Resource0001
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resource0001));
			this.ErrorIcon = new System.Windows.Forms.PictureBox();
			this.InformationIcon = new System.Windows.Forms.PictureBox();
			this.WarningIcon = new System.Windows.Forms.PictureBox();
			this.QuestionIcon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.InformationIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WarningIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.QuestionIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// ErrorIcon
			// 
			this.ErrorIcon.Image = ((System.Drawing.Image)(resources.GetObject("ErrorIcon.Image")));
			this.ErrorIcon.Location = new System.Drawing.Point(12, 12);
			this.ErrorIcon.Name = "ErrorIcon";
			this.ErrorIcon.Size = new System.Drawing.Size(64, 64);
			this.ErrorIcon.TabIndex = 0;
			this.ErrorIcon.TabStop = false;
			// 
			// InformationIcon
			// 
			this.InformationIcon.Image = ((System.Drawing.Image)(resources.GetObject("InformationIcon.Image")));
			this.InformationIcon.Location = new System.Drawing.Point(82, 12);
			this.InformationIcon.Name = "InformationIcon";
			this.InformationIcon.Size = new System.Drawing.Size(64, 64);
			this.InformationIcon.TabIndex = 1;
			this.InformationIcon.TabStop = false;
			// 
			// WarningIcon
			// 
			this.WarningIcon.Image = ((System.Drawing.Image)(resources.GetObject("WarningIcon.Image")));
			this.WarningIcon.Location = new System.Drawing.Point(222, 12);
			this.WarningIcon.Name = "WarningIcon";
			this.WarningIcon.Size = new System.Drawing.Size(64, 64);
			this.WarningIcon.TabIndex = 2;
			this.WarningIcon.TabStop = false;
			// 
			// QuestionIcon
			// 
			this.QuestionIcon.Image = ((System.Drawing.Image)(resources.GetObject("QuestionIcon.Image")));
			this.QuestionIcon.Location = new System.Drawing.Point(152, 12);
			this.QuestionIcon.Name = "QuestionIcon";
			this.QuestionIcon.Size = new System.Drawing.Size(64, 64);
			this.QuestionIcon.TabIndex = 3;
			this.QuestionIcon.TabStop = false;
			// 
			// Resource0001
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.QuestionIcon);
			this.Controls.Add(this.WarningIcon);
			this.Controls.Add(this.InformationIcon);
			this.Controls.Add(this.ErrorIcon);
			this.Name = "Resource0001";
			this.Text = "Resource0001";
			this.Load += new System.EventHandler(this.Resource0001_Load);
			((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.InformationIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WarningIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.QuestionIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.PictureBox ErrorIcon;
		public System.Windows.Forms.PictureBox InformationIcon;
		public System.Windows.Forms.PictureBox WarningIcon;
		public System.Windows.Forms.PictureBox QuestionIcon;

	}
}
