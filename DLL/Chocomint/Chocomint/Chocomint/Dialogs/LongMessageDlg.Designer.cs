namespace Charlotte.Chocomint.Dialogs
{
	partial class LongMessageDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LongMessageDlg));
			this.MainPanel = new System.Windows.Forms.Panel();
			this.TextMessage = new System.Windows.Forms.TextBox();
			this.MessageIcon = new System.Windows.Forms.PictureBox();
			this.BtnOk = new System.Windows.Forms.Button();
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
			this.MainPanel.Controls.Add(this.TextMessage);
			this.MainPanel.Controls.Add(this.MessageIcon);
			this.MainPanel.Location = new System.Drawing.Point(0, 0);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(484, 130);
			this.MainPanel.TabIndex = 0;
			// 
			// TextMessage
			// 
			this.TextMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TextMessage.Location = new System.Drawing.Point(100, 12);
			this.TextMessage.Multiline = true;
			this.TextMessage.Name = "TextMessage";
			this.TextMessage.ReadOnly = true;
			this.TextMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TextMessage.Size = new System.Drawing.Size(372, 105);
			this.TextMessage.TabIndex = 0;
			this.TextMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextMessage_KeyPress);
			// 
			// MessageIcon
			// 
			this.MessageIcon.Location = new System.Drawing.Point(30, 30);
			this.MessageIcon.Name = "MessageIcon";
			this.MessageIcon.Size = new System.Drawing.Size(64, 64);
			this.MessageIcon.TabIndex = 6;
			this.MessageIcon.TabStop = false;
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(372, 149);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(100, 50);
			this.BtnOk.TabIndex = 2;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// LongMessageDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 211);
			this.Controls.Add(this.BtnOk);
			this.Controls.Add(this.MainPanel);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LongMessageDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "メッセージ";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LongMessageDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LongMessageDlg_FormClosed);
			this.Load += new System.EventHandler(this.LongMessageDlg_Load);
			this.Shown += new System.EventHandler(this.LongMessageDlg_Shown);
			this.Resize += new System.EventHandler(this.LongMessageDlg_Resize);
			this.MainPanel.ResumeLayout(false);
			this.MainPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.PictureBox MessageIcon;
		private System.Windows.Forms.Button BtnOk;
		public System.Windows.Forms.TextBox TextMessage;
	}
}
