namespace Charlotte.Chocomint.Dialogs
{
	partial class MessageDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageDlg));
			this.BtnOk = new System.Windows.Forms.Button();
			this.MessageIcon = new System.Windows.Forms.PictureBox();
			this.TextMessage = new System.Windows.Forms.Label();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.TextDetailMessage = new System.Windows.Forms.TextBox();
			this.DetailLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).BeginInit();
			this.MainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(372, 149);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(100, 50);
			this.BtnOk.TabIndex = 1;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// MessageIcon
			// 
			this.MessageIcon.Location = new System.Drawing.Point(30, 30);
			this.MessageIcon.Name = "MessageIcon";
			this.MessageIcon.Size = new System.Drawing.Size(64, 64);
			this.MessageIcon.TabIndex = 6;
			this.MessageIcon.TabStop = false;
			// 
			// TextMessage
			// 
			this.TextMessage.AutoSize = true;
			this.TextMessage.Location = new System.Drawing.Point(100, 30);
			this.TextMessage.Name = "TextMessage";
			this.TextMessage.Size = new System.Drawing.Size(191, 20);
			this.TextMessage.TabIndex = 0;
			this.TextMessage.Text = "メッセージを準備しています。";
			// 
			// MainPanel
			// 
			this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainPanel.BackColor = System.Drawing.Color.White;
			this.MainPanel.Controls.Add(this.TextDetailMessage);
			this.MainPanel.Controls.Add(this.DetailLabel);
			this.MainPanel.Controls.Add(this.MessageIcon);
			this.MainPanel.Controls.Add(this.TextMessage);
			this.MainPanel.Location = new System.Drawing.Point(0, 0);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(484, 130);
			this.MainPanel.TabIndex = 0;
			// 
			// TextDetailMessage
			// 
			this.TextDetailMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.TextDetailMessage.Location = new System.Drawing.Point(372, 12);
			this.TextDetailMessage.Multiline = true;
			this.TextDetailMessage.Name = "TextDetailMessage";
			this.TextDetailMessage.ReadOnly = true;
			this.TextDetailMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextDetailMessage.Size = new System.Drawing.Size(100, 82);
			this.TextDetailMessage.TabIndex = 1;
			this.TextDetailMessage.Visible = false;
			this.TextDetailMessage.TextChanged += new System.EventHandler(this.TextDetailMessage_TextChanged);
			this.TextDetailMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextDetailMessage_KeyPress);
			// 
			// DetailLabel
			// 
			this.DetailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DetailLabel.AutoSize = true;
			this.DetailLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DetailLabel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.DetailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.DetailLabel.Location = new System.Drawing.Point(398, 100);
			this.DetailLabel.Name = "DetailLabel";
			this.DetailLabel.Size = new System.Drawing.Size(74, 20);
			this.DetailLabel.TabIndex = 2;
			this.DetailLabel.Text = "詳細を表示";
			this.DetailLabel.Visible = false;
			this.DetailLabel.Click += new System.EventHandler(this.DetailLabel_Click);
			// 
			// MessageDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 211);
			this.Controls.Add(this.MainPanel);
			this.Controls.Add(this.BtnOk);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MessageDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "メッセージ";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MessageDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MessageDlg_FormClosed);
			this.Load += new System.EventHandler(this.MessageDlg_Load);
			this.Shown += new System.EventHandler(this.MessageDlg_Shown);
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).EndInit();
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.PictureBox MessageIcon;
		private System.Windows.Forms.Label TextMessage;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.Label DetailLabel;
		private System.Windows.Forms.TextBox TextDetailMessage;
	}
}
