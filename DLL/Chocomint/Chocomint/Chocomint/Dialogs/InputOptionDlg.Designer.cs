namespace Charlotte.Chocomint.Dialogs
{
	partial class InputOptionDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputOptionDlg));
			this.MainPanel = new System.Windows.Forms.Panel();
			this.MessageIcon = new System.Windows.Forms.PictureBox();
			this.TextMessage = new System.Windows.Forms.Label();
			this.FirstButton = new System.Windows.Forms.Button();
			this.MainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// MainPanel
			// 
			this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainPanel.BackColor = System.Drawing.Color.White;
			this.MainPanel.Controls.Add(this.MessageIcon);
			this.MainPanel.Controls.Add(this.TextMessage);
			this.MainPanel.Location = new System.Drawing.Point(0, 0);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(664, 165);
			this.MainPanel.TabIndex = 0;
			// 
			// MessageIcon
			// 
			this.MessageIcon.Location = new System.Drawing.Point(50, 50);
			this.MessageIcon.Name = "MessageIcon";
			this.MessageIcon.Size = new System.Drawing.Size(64, 64);
			this.MessageIcon.TabIndex = 8;
			this.MessageIcon.TabStop = false;
			// 
			// TextMessage
			// 
			this.TextMessage.AutoSize = true;
			this.TextMessage.Location = new System.Drawing.Point(120, 50);
			this.TextMessage.Name = "TextMessage";
			this.TextMessage.Size = new System.Drawing.Size(191, 20);
			this.TextMessage.TabIndex = 0;
			this.TextMessage.Text = "メッセージを準備しています。";
			// 
			// FirstButton
			// 
			this.FirstButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FirstButton.Location = new System.Drawing.Point(12, 179);
			this.FirstButton.Name = "FirstButton";
			this.FirstButton.Size = new System.Drawing.Size(640, 50);
			this.FirstButton.TabIndex = 1;
			this.FirstButton.Text = "OK";
			this.FirstButton.UseVisualStyleBackColor = true;
			this.FirstButton.Click += new System.EventHandler(this.FirstButton_Click);
			// 
			// InputOptionDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(664, 241);
			this.Controls.Add(this.FirstButton);
			this.Controls.Add(this.MainPanel);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputOptionDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "質問";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputOptionDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputOptionDlg_FormClosed);
			this.Load += new System.EventHandler(this.InputOptionDlg_Load);
			this.Shown += new System.EventHandler(this.InputOptionDlg_Shown);
			this.Resize += new System.EventHandler(this.InputOptionDlg_Resize);
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Button FirstButton;
		private System.Windows.Forms.Label TextMessage;
		private System.Windows.Forms.PictureBox MessageIcon;
	}
}
