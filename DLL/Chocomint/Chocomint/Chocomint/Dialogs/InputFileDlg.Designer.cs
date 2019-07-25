namespace Charlotte.Chocomint.Dialogs
{
	partial class InputFileDlg
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputFileDlg));
			this.BtnBrowse = new System.Windows.Forms.Button();
			this.TextValue = new System.Windows.Forms.TextBox();
			this.TextValueMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.項目なしToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Prompt = new System.Windows.Forms.Label();
			this.BtnCancel = new System.Windows.Forms.Button();
			this.BtnOk = new System.Windows.Forms.Button();
			this.Hint = new System.Windows.Forms.Label();
			this.TextValueMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnBrowse
			// 
			this.BtnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnBrowse.Location = new System.Drawing.Point(522, 53);
			this.BtnBrowse.Name = "BtnBrowse";
			this.BtnBrowse.Size = new System.Drawing.Size(50, 27);
			this.BtnBrowse.TabIndex = 2;
			this.BtnBrowse.Text = "...";
			this.BtnBrowse.UseVisualStyleBackColor = true;
			this.BtnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
			// 
			// TextValue
			// 
			this.TextValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextValue.ContextMenuStrip = this.TextValueMenu;
			this.TextValue.Location = new System.Drawing.Point(34, 53);
			this.TextValue.MaxLength = 300;
			this.TextValue.Name = "TextValue";
			this.TextValue.Size = new System.Drawing.Size(482, 27);
			this.TextValue.TabIndex = 1;
			this.TextValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextValue_KeyPress);
			// 
			// TextValueMenu
			// 
			this.TextValueMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.項目なしToolStripMenuItem});
			this.TextValueMenu.Name = "TextValueMenu";
			this.TextValueMenu.Size = new System.Drawing.Size(117, 26);
			// 
			// 項目なしToolStripMenuItem
			// 
			this.項目なしToolStripMenuItem.Enabled = false;
			this.項目なしToolStripMenuItem.Name = "項目なしToolStripMenuItem";
			this.項目なしToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.項目なしToolStripMenuItem.Text = "項目なし";
			// 
			// Prompt
			// 
			this.Prompt.AutoSize = true;
			this.Prompt.Location = new System.Drawing.Point(30, 30);
			this.Prompt.Name = "Prompt";
			this.Prompt.Size = new System.Drawing.Size(191, 20);
			this.Prompt.TabIndex = 0;
			this.Prompt.Text = "ファイル名を入力して下さい。";
			// 
			// BtnCancel
			// 
			this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCancel.Location = new System.Drawing.Point(472, 129);
			this.BtnCancel.Name = "BtnCancel";
			this.BtnCancel.Size = new System.Drawing.Size(100, 50);
			this.BtnCancel.TabIndex = 5;
			this.BtnCancel.Text = "キャンセル";
			this.BtnCancel.UseVisualStyleBackColor = true;
			this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// BtnOk
			// 
			this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOk.Location = new System.Drawing.Point(366, 129);
			this.BtnOk.Name = "BtnOk";
			this.BtnOk.Size = new System.Drawing.Size(100, 50);
			this.BtnOk.TabIndex = 4;
			this.BtnOk.Text = "OK";
			this.BtnOk.UseVisualStyleBackColor = true;
			this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
			// 
			// Hint
			// 
			this.Hint.AutoSize = true;
			this.Hint.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Hint.ForeColor = System.Drawing.Color.Teal;
			this.Hint.Location = new System.Drawing.Point(31, 83);
			this.Hint.Name = "Hint";
			this.Hint.Size = new System.Drawing.Size(250, 17);
			this.Hint.TabIndex = 3;
			this.Hint.Text = "ファイルのドラッグアンドドロップも可能です。";
			// 
			// InputFileDlg
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 191);
			this.Controls.Add(this.Hint);
			this.Controls.Add(this.BtnBrowse);
			this.Controls.Add(this.TextValue);
			this.Controls.Add(this.Prompt);
			this.Controls.Add(this.BtnCancel);
			this.Controls.Add(this.BtnOk);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputFileDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ファイル入力";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputFileDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InputFileDlg_FormClosed);
			this.Load += new System.EventHandler(this.InputFileDlg_Load);
			this.Shown += new System.EventHandler(this.InputFileDlg_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.InputFileDlg_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.InputFileDlg_DragEnter);
			this.TextValueMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnBrowse;
		public System.Windows.Forms.TextBox TextValue;
		public System.Windows.Forms.Label Prompt;
		private System.Windows.Forms.Button BtnCancel;
		private System.Windows.Forms.Button BtnOk;
		private System.Windows.Forms.ContextMenuStrip TextValueMenu;
		private System.Windows.Forms.ToolStripMenuItem 項目なしToolStripMenuItem;
		private System.Windows.Forms.Label Hint;
	}
}
