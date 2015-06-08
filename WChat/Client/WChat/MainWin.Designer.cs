namespace Charlotte
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.TopMenu = new System.Windows.Forms.MenuStrip();
			this.アプリAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ユーザーリストを表示しないUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ユーザーリストの幅を固定するLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.入力エリアの高さを固定するTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.送信ボタンを表示しないHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.送信ボタンの幅を固定するBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.その他の設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ファイル又はフォルダの貼り付けDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.BottomStatus = new System.Windows.Forms.StatusStrip();
			this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.LeftStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainSplit = new System.Windows.Forms.SplitContainer();
			this.MemberList = new System.Windows.Forms.ListBox();
			this.MemberListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ホームディレクトリを開くHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TimeLineSplit = new System.Windows.Forms.SplitContainer();
			this.TimeLineText = new System.Windows.Forms.RichTextBox();
			this.RemarkSplit = new System.Windows.Forms.SplitContainer();
			this.RemarkText = new System.Windows.Forms.TextBox();
			this.BtnSend = new System.Windows.Forms.Button();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.TopMenu.SuspendLayout();
			this.BottomStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
			this.MainSplit.Panel1.SuspendLayout();
			this.MainSplit.Panel2.SuspendLayout();
			this.MainSplit.SuspendLayout();
			this.MemberListMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TimeLineSplit)).BeginInit();
			this.TimeLineSplit.Panel1.SuspendLayout();
			this.TimeLineSplit.Panel2.SuspendLayout();
			this.TimeLineSplit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RemarkSplit)).BeginInit();
			this.RemarkSplit.Panel1.SuspendLayout();
			this.RemarkSplit.Panel2.SuspendLayout();
			this.RemarkSplit.SuspendLayout();
			this.SuspendLayout();
			// 
			// TopMenu
			// 
			this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリAToolStripMenuItem,
            this.設定SToolStripMenuItem,
            this.ツールTToolStripMenuItem});
			this.TopMenu.Location = new System.Drawing.Point(0, 0);
			this.TopMenu.Name = "TopMenu";
			this.TopMenu.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
			this.TopMenu.Size = new System.Drawing.Size(442, 28);
			this.TopMenu.TabIndex = 0;
			this.TopMenu.Text = "menuStrip1";
			// 
			// アプリAToolStripMenuItem
			// 
			this.アプリAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了XToolStripMenuItem});
			this.アプリAToolStripMenuItem.Name = "アプリAToolStripMenuItem";
			this.アプリAToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
			this.アプリAToolStripMenuItem.Text = "アプリ(&A)";
			// 
			// 終了XToolStripMenuItem
			// 
			this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
			this.終了XToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.終了XToolStripMenuItem.Text = "終了(&X)";
			this.終了XToolStripMenuItem.Click += new System.EventHandler(this.終了XToolStripMenuItem_Click);
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ユーザーリストを表示しないUToolStripMenuItem,
            this.ユーザーリストの幅を固定するLToolStripMenuItem,
            this.入力エリアの高さを固定するTToolStripMenuItem,
            this.送信ボタンを表示しないHToolStripMenuItem,
            this.送信ボタンの幅を固定するBToolStripMenuItem,
            this.toolStripMenuItem1,
            this.その他の設定SToolStripMenuItem});
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			// 
			// ユーザーリストを表示しないUToolStripMenuItem
			// 
			this.ユーザーリストを表示しないUToolStripMenuItem.Name = "ユーザーリストを表示しないUToolStripMenuItem";
			this.ユーザーリストを表示しないUToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.ユーザーリストを表示しないUToolStripMenuItem.Text = "ユーザーリストを表示しない(&U)";
			this.ユーザーリストを表示しないUToolStripMenuItem.Click += new System.EventHandler(this.ユーザーリストを表示しないUToolStripMenuItem_Click);
			// 
			// ユーザーリストの幅を固定するLToolStripMenuItem
			// 
			this.ユーザーリストの幅を固定するLToolStripMenuItem.Name = "ユーザーリストの幅を固定するLToolStripMenuItem";
			this.ユーザーリストの幅を固定するLToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.ユーザーリストの幅を固定するLToolStripMenuItem.Text = "ユーザーリストの幅を固定する(&L)";
			this.ユーザーリストの幅を固定するLToolStripMenuItem.Click += new System.EventHandler(this.ユーザーリストの幅を固定するLToolStripMenuItem_Click);
			// 
			// 入力エリアの高さを固定するTToolStripMenuItem
			// 
			this.入力エリアの高さを固定するTToolStripMenuItem.Name = "入力エリアの高さを固定するTToolStripMenuItem";
			this.入力エリアの高さを固定するTToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.入力エリアの高さを固定するTToolStripMenuItem.Text = "入力エリアの高さを固定する(&T)";
			this.入力エリアの高さを固定するTToolStripMenuItem.Click += new System.EventHandler(this.入力エリアの高さを固定するTToolStripMenuItem_Click);
			// 
			// 送信ボタンを表示しないHToolStripMenuItem
			// 
			this.送信ボタンを表示しないHToolStripMenuItem.Name = "送信ボタンを表示しないHToolStripMenuItem";
			this.送信ボタンを表示しないHToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.送信ボタンを表示しないHToolStripMenuItem.Text = "送信ボタンを表示しない(&H)";
			this.送信ボタンを表示しないHToolStripMenuItem.Click += new System.EventHandler(this.送信ボタンを表示しないHToolStripMenuItem_Click);
			// 
			// 送信ボタンの幅を固定するBToolStripMenuItem
			// 
			this.送信ボタンの幅を固定するBToolStripMenuItem.Name = "送信ボタンの幅を固定するBToolStripMenuItem";
			this.送信ボタンの幅を固定するBToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.送信ボタンの幅を固定するBToolStripMenuItem.Text = "送信ボタンの幅を固定する(&B)";
			this.送信ボタンの幅を固定するBToolStripMenuItem.Click += new System.EventHandler(this.送信ボタンの幅を固定するBToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(258, 6);
			// 
			// その他の設定SToolStripMenuItem
			// 
			this.その他の設定SToolStripMenuItem.Name = "その他の設定SToolStripMenuItem";
			this.その他の設定SToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.その他の設定SToolStripMenuItem.Text = "その他の設定(&S)";
			this.その他の設定SToolStripMenuItem.Click += new System.EventHandler(this.その他の設定SToolStripMenuItem_Click);
			// 
			// ツールTToolStripMenuItem
			// 
			this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイル又はフォルダの貼り付けDToolStripMenuItem});
			this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
			this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
			this.ツールTToolStripMenuItem.Text = "ツール(&T)";
			// 
			// ファイル又はフォルダの貼り付けDToolStripMenuItem
			// 
			this.ファイル又はフォルダの貼り付けDToolStripMenuItem.Name = "ファイル又はフォルダの貼り付けDToolStripMenuItem";
			this.ファイル又はフォルダの貼り付けDToolStripMenuItem.Size = new System.Drawing.Size(383, 22);
			this.ファイル又はフォルダの貼り付けDToolStripMenuItem.Text = "ファイル又はフォルダへのハイパーリンクの貼り付け(&D)";
			this.ファイル又はフォルダの貼り付けDToolStripMenuItem.Click += new System.EventHandler(this.ファイル又はフォルダの貼り付けDToolStripMenuItem_Click);
			// 
			// BottomStatus
			// 
			this.BottomStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMessage,
            this.LeftStatusMessage});
			this.BottomStatus.Location = new System.Drawing.Point(0, 537);
			this.BottomStatus.Name = "BottomStatus";
			this.BottomStatus.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.BottomStatus.Size = new System.Drawing.Size(442, 23);
			this.BottomStatus.TabIndex = 2;
			this.BottomStatus.Text = "statusStrip1";
			// 
			// StatusMessage
			// 
			this.StatusMessage.Name = "StatusMessage";
			this.StatusMessage.Size = new System.Drawing.Size(318, 18);
			this.StatusMessage.Spring = true;
			this.StatusMessage.Text = "準備しています...";
			this.StatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LeftStatusMessage
			// 
			this.LeftStatusMessage.Name = "LeftStatusMessage";
			this.LeftStatusMessage.Size = new System.Drawing.Size(104, 18);
			this.LeftStatusMessage.Text = "準備しています...";
			// 
			// MainSplit
			// 
			this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainSplit.Location = new System.Drawing.Point(0, 28);
			this.MainSplit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MainSplit.Name = "MainSplit";
			// 
			// MainSplit.Panel1
			// 
			this.MainSplit.Panel1.Controls.Add(this.MemberList);
			// 
			// MainSplit.Panel2
			// 
			this.MainSplit.Panel2.Controls.Add(this.TimeLineSplit);
			this.MainSplit.Size = new System.Drawing.Size(442, 509);
			this.MainSplit.SplitterDistance = 147;
			this.MainSplit.SplitterWidth = 5;
			this.MainSplit.TabIndex = 1;
			this.MainSplit.TabStop = false;
			this.MainSplit.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MainSplit_SplitterMoved);
			// 
			// MemberList
			// 
			this.MemberList.ContextMenuStrip = this.MemberListMenu;
			this.MemberList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MemberList.FormattingEnabled = true;
			this.MemberList.ItemHeight = 20;
			this.MemberList.Location = new System.Drawing.Point(0, 0);
			this.MemberList.Name = "MemberList";
			this.MemberList.Size = new System.Drawing.Size(147, 509);
			this.MemberList.TabIndex = 0;
			this.MemberList.SelectedIndexChanged += new System.EventHandler(this.MemberList_SelectedIndexChanged);
			// 
			// MemberListMenu
			// 
			this.MemberListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ホームディレクトリを開くHToolStripMenuItem});
			this.MemberListMenu.Name = "MemberListMenu";
			this.MemberListMenu.Size = new System.Drawing.Size(240, 26);
			this.MemberListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MemberListMenu_Opening);
			// 
			// ホームディレクトリを開くHToolStripMenuItem
			// 
			this.ホームディレクトリを開くHToolStripMenuItem.Name = "ホームディレクトリを開くHToolStripMenuItem";
			this.ホームディレクトリを開くHToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			this.ホームディレクトリを開くHToolStripMenuItem.Text = "ホームディレクトリを開く(&H)";
			this.ホームディレクトリを開くHToolStripMenuItem.Click += new System.EventHandler(this.ホームディレクトリを開くHToolStripMenuItem_Click);
			// 
			// TimeLineSplit
			// 
			this.TimeLineSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TimeLineSplit.Location = new System.Drawing.Point(0, 0);
			this.TimeLineSplit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TimeLineSplit.Name = "TimeLineSplit";
			this.TimeLineSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// TimeLineSplit.Panel1
			// 
			this.TimeLineSplit.Panel1.Controls.Add(this.TimeLineText);
			// 
			// TimeLineSplit.Panel2
			// 
			this.TimeLineSplit.Panel2.Controls.Add(this.RemarkSplit);
			this.TimeLineSplit.Size = new System.Drawing.Size(290, 509);
			this.TimeLineSplit.SplitterDistance = 411;
			this.TimeLineSplit.SplitterWidth = 7;
			this.TimeLineSplit.TabIndex = 0;
			this.TimeLineSplit.TabStop = false;
			this.TimeLineSplit.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.TimeLineSplit_SplitterMoved);
			// 
			// TimeLineText
			// 
			this.TimeLineText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TimeLineText.Location = new System.Drawing.Point(0, 0);
			this.TimeLineText.Name = "TimeLineText";
			this.TimeLineText.Size = new System.Drawing.Size(290, 411);
			this.TimeLineText.TabIndex = 0;
			this.TimeLineText.Text = "";
			this.TimeLineText.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.TimeLineText_LinkClicked);
			this.TimeLineText.TextChanged += new System.EventHandler(this.TimeLineText_TextChanged);
			// 
			// RemarkSplit
			// 
			this.RemarkSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RemarkSplit.Location = new System.Drawing.Point(0, 0);
			this.RemarkSplit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RemarkSplit.Name = "RemarkSplit";
			// 
			// RemarkSplit.Panel1
			// 
			this.RemarkSplit.Panel1.Controls.Add(this.RemarkText);
			// 
			// RemarkSplit.Panel2
			// 
			this.RemarkSplit.Panel2.Controls.Add(this.BtnSend);
			this.RemarkSplit.Size = new System.Drawing.Size(290, 91);
			this.RemarkSplit.SplitterDistance = 212;
			this.RemarkSplit.SplitterWidth = 5;
			this.RemarkSplit.TabIndex = 0;
			this.RemarkSplit.TabStop = false;
			this.RemarkSplit.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.RemarkSplit_SplitterMoved);
			// 
			// RemarkText
			// 
			this.RemarkText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RemarkText.Location = new System.Drawing.Point(0, 0);
			this.RemarkText.MaxLength = 30000;
			this.RemarkText.Multiline = true;
			this.RemarkText.Name = "RemarkText";
			this.RemarkText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.RemarkText.Size = new System.Drawing.Size(212, 91);
			this.RemarkText.TabIndex = 0;
			this.RemarkText.TextChanged += new System.EventHandler(this.RemarkText_TextChanged);
			this.RemarkText.Enter += new System.EventHandler(this.RemarkText_Enter);
			this.RemarkText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RemarkText_KeyPress);
			// 
			// BtnSend
			// 
			this.BtnSend.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnSend.Location = new System.Drawing.Point(0, 0);
			this.BtnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BtnSend.Name = "BtnSend";
			this.BtnSend.Size = new System.Drawing.Size(73, 91);
			this.BtnSend.TabIndex = 0;
			this.BtnSend.Text = "送信";
			this.BtnSend.UseVisualStyleBackColor = true;
			this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 560);
			this.Controls.Add(this.MainSplit);
			this.Controls.Add(this.BottomStatus);
			this.Controls.Add(this.TopMenu);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.TopMenu;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "Chat";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.Resize += new System.EventHandler(this.MainWin_Resize);
			this.TopMenu.ResumeLayout(false);
			this.TopMenu.PerformLayout();
			this.BottomStatus.ResumeLayout(false);
			this.BottomStatus.PerformLayout();
			this.MainSplit.Panel1.ResumeLayout(false);
			this.MainSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
			this.MainSplit.ResumeLayout(false);
			this.MemberListMenu.ResumeLayout(false);
			this.TimeLineSplit.Panel1.ResumeLayout(false);
			this.TimeLineSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TimeLineSplit)).EndInit();
			this.TimeLineSplit.ResumeLayout(false);
			this.RemarkSplit.Panel1.ResumeLayout(false);
			this.RemarkSplit.Panel1.PerformLayout();
			this.RemarkSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.RemarkSplit)).EndInit();
			this.RemarkSplit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip TopMenu;
		private System.Windows.Forms.StatusStrip BottomStatus;
		private System.Windows.Forms.SplitContainer MainSplit;
		private System.Windows.Forms.SplitContainer TimeLineSplit;
		private System.Windows.Forms.RichTextBox TimeLineText;
		private System.Windows.Forms.SplitContainer RemarkSplit;
		private System.Windows.Forms.TextBox RemarkText;
		private System.Windows.Forms.Button BtnSend;
		private System.Windows.Forms.ListBox MemberList;
		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ToolStripMenuItem アプリAToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ユーザーリストの幅を固定するLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 入力エリアの高さを固定するTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 送信ボタンの幅を固定するBToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem その他の設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ユーザーリストを表示しないUToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 送信ボタンを表示しないHToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel StatusMessage;
		private System.Windows.Forms.ContextMenuStrip MemberListMenu;
		private System.Windows.Forms.ToolStripMenuItem ホームディレクトリを開くHToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel LeftStatusMessage;
		private System.Windows.Forms.ToolStripMenuItem ツールTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ファイル又はフォルダの貼り付けDToolStripMenuItem;
	}
}

