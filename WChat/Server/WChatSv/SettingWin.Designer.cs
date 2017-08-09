namespace Charlotte
{
	partial class SettingWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingWin));
			this.label1 = new System.Windows.Forms.Label();
			this.ChatSvPort = new System.Windows.Forms.TextBox();
			this.RevServerPort = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ProcMode = new System.Windows.Forms.ComboBox();
			this.BtnOk = new System.Windows.Forms.Button();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(192, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "チャットサーバー / ポート番号";
			// 
			// ChatSvPort
			// 
			this.ChatSvPort.Location = new System.Drawing.Point(261, 20);
			this.ChatSvPort.MaxLength = 5;
			this.ChatSvPort.Name = "ChatSvPort";
			this.ChatSvPort.Size = new System.Drawing.Size(74, 27);
			this.ChatSvPort.TabIndex = 1;
			this.ChatSvPort.Text = "65535";
			this.ChatSvPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// RevServerPort
			// 
			this.RevServerPort.Location = new System.Drawing.Point(261, 53);
			this.RevServerPort.MaxLength = 5;
			this.RevServerPort.Name = "RevServerPort";
			this.RevServerPort.Size = new System.Drawing.Size(74, 27);
			this.RevServerPort.TabIndex = 3;
			this.RevServerPort.Text = "65535";
			this.RevServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(218, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "ファイル転送サーバー / ポート番号";
			// 
			// ProcMode
			// 
			this.ProcMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProcMode.FormattingEnabled = true;
			this.ProcMode.Items.AddRange(new object[] {
            "コンソールを表示しない（推奨）",
            "コンソールを表示する・通常",
            "コンソールを表示する・最小化"});
			this.ProcMode.Location = new System.Drawing.Point(28, 98);
			this.ProcMode.Name = "ProcMode";
			this.ProcMode.Size = new System.Drawing.Size(307, 28);
			this.ProcMode.TabIndex = 4;
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(103, 153);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(117, 41);
			this.BtnOk.TabIndex = 5;
			this.BtnOk.Text = "OK(&O)";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(226, 153);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(117, 41);
			this.BtnCancel.TabIndex = 6;
			this.BtnCancel.Text = "キャンセル(&C)";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// SettingWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(355, 206);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.ProcMode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.RevServerPort);
			this.Controls.Add(this.ChatSvPort);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "設定";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SettingWin_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ChatSvPort;
		private System.Windows.Forms.TextBox RevServerPort;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ProcMode;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.Button BtnCancel;
	}
}
