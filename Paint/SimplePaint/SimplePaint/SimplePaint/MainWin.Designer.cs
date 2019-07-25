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
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.ファイル読み込みToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ファイル書き出しToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.クリアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.サイズ変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.アンチエイリアスToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ペン先ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.透明度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.色2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.透明度2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.形2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.矩形選択ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.矩形選択解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlZMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CtrlYMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MPicBoundsBugMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.塗りつぶしToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.テクスチャToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.テクスチャ矩形タイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.グラデーションToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.command2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PuzzleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.South = new System.Windows.Forms.ToolStripStatusLabel();
			this.SouthWest = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainPanel = new System.Windows.Forms.Panel();
			this.MainPicture = new System.Windows.Forms.PictureBox();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.MainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリToolStripMenuItem,
            this.編集ToolStripMenuItem,
            this.設定ToolStripMenuItem,
            this.ペン先ToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.commandToolStripMenuItem,
            this.command2ToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// アプリToolStripMenuItem
			// 
			this.アプリToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ファイル読み込みToolStripMenuItem,
            this.ファイル書き出しToolStripMenuItem});
			this.アプリToolStripMenuItem.Name = "アプリToolStripMenuItem";
			this.アプリToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.アプリToolStripMenuItem.Text = "アプリ";
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
			// 
			// ファイル読み込みToolStripMenuItem
			// 
			this.ファイル読み込みToolStripMenuItem.Name = "ファイル読み込みToolStripMenuItem";
			this.ファイル読み込みToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.ファイル読み込みToolStripMenuItem.Text = "ファイルを開く";
			this.ファイル読み込みToolStripMenuItem.Click += new System.EventHandler(this.ファイル読み込みToolStripMenuItem_Click);
			// 
			// ファイル書き出しToolStripMenuItem
			// 
			this.ファイル書き出しToolStripMenuItem.Name = "ファイル書き出しToolStripMenuItem";
			this.ファイル書き出しToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.ファイル書き出しToolStripMenuItem.Text = "ファイルに保存する";
			this.ファイル書き出しToolStripMenuItem.Click += new System.EventHandler(this.ファイル書き出しToolStripMenuItem_Click);
			// 
			// 編集ToolStripMenuItem
			// 
			this.編集ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.クリアToolStripMenuItem,
            this.サイズ変更ToolStripMenuItem});
			this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
			this.編集ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.編集ToolStripMenuItem.Text = "編集";
			// 
			// クリアToolStripMenuItem
			// 
			this.クリアToolStripMenuItem.Name = "クリアToolStripMenuItem";
			this.クリアToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.クリアToolStripMenuItem.Text = "クリア";
			this.クリアToolStripMenuItem.Click += new System.EventHandler(this.クリアToolStripMenuItem_Click);
			// 
			// サイズ変更ToolStripMenuItem
			// 
			this.サイズ変更ToolStripMenuItem.Name = "サイズ変更ToolStripMenuItem";
			this.サイズ変更ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.サイズ変更ToolStripMenuItem.Text = "サイズ変更";
			this.サイズ変更ToolStripMenuItem.Click += new System.EventHandler(this.サイズ変更ToolStripMenuItem_Click);
			// 
			// 設定ToolStripMenuItem
			// 
			this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アンチエイリアスToolStripMenuItem});
			this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
			this.設定ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.設定ToolStripMenuItem.Text = "設定";
			// 
			// アンチエイリアスToolStripMenuItem
			// 
			this.アンチエイリアスToolStripMenuItem.Name = "アンチエイリアスToolStripMenuItem";
			this.アンチエイリアスToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.アンチエイリアスToolStripMenuItem.Text = "アンチエイリアス";
			this.アンチエイリアスToolStripMenuItem.Click += new System.EventHandler(this.アンチエイリアスToolStripMenuItem_Click);
			// 
			// ペン先ToolStripMenuItem
			// 
			this.ペン先ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.色ToolStripMenuItem,
            this.透明度ToolStripMenuItem,
            this.形ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.色2MenuItem,
            this.透明度2MenuItem,
            this.形2MenuItem});
			this.ペン先ToolStripMenuItem.Name = "ペン先ToolStripMenuItem";
			this.ペン先ToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.ペン先ToolStripMenuItem.Text = "ペン先";
			// 
			// 色ToolStripMenuItem
			// 
			this.色ToolStripMenuItem.Name = "色ToolStripMenuItem";
			this.色ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.色ToolStripMenuItem.Text = "色";
			this.色ToolStripMenuItem.Click += new System.EventHandler(this.色ToolStripMenuItem_Click);
			// 
			// 透明度ToolStripMenuItem
			// 
			this.透明度ToolStripMenuItem.Name = "透明度ToolStripMenuItem";
			this.透明度ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.透明度ToolStripMenuItem.Text = "透明度";
			this.透明度ToolStripMenuItem.Click += new System.EventHandler(this.透明度ToolStripMenuItem_Click);
			// 
			// 形ToolStripMenuItem
			// 
			this.形ToolStripMenuItem.Name = "形ToolStripMenuItem";
			this.形ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.形ToolStripMenuItem.Text = "形";
			this.形ToolStripMenuItem.Click += new System.EventHandler(this.形ToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 6);
			// 
			// 色2MenuItem
			// 
			this.色2MenuItem.Name = "色2MenuItem";
			this.色2MenuItem.Size = new System.Drawing.Size(122, 22);
			this.色2MenuItem.Text = "色２";
			this.色2MenuItem.Click += new System.EventHandler(this.色2MenuItem_Click);
			// 
			// 透明度2MenuItem
			// 
			this.透明度2MenuItem.Name = "透明度2MenuItem";
			this.透明度2MenuItem.Size = new System.Drawing.Size(122, 22);
			this.透明度2MenuItem.Text = "透明度２";
			this.透明度2MenuItem.Click += new System.EventHandler(this.透明度2MenuItem_Click);
			// 
			// 形2MenuItem
			// 
			this.形2MenuItem.Name = "形2MenuItem";
			this.形2MenuItem.Size = new System.Drawing.Size(122, 22);
			this.形2MenuItem.Text = "形２";
			this.形2MenuItem.Click += new System.EventHandler(this.形2MenuItem_Click);
			// 
			// 操作ToolStripMenuItem
			// 
			this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.矩形選択ToolStripMenuItem,
            this.矩形選択解除ToolStripMenuItem,
            this.CtrlZMenuItem,
            this.CtrlYMenuItem,
            this.GCMenuItem,
            this.MPicBoundsBugMenuItem});
			this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
			this.操作ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.操作ToolStripMenuItem.Text = "操作";
			// 
			// 矩形選択ToolStripMenuItem
			// 
			this.矩形選択ToolStripMenuItem.Enabled = false;
			this.矩形選択ToolStripMenuItem.Name = "矩形選択ToolStripMenuItem";
			this.矩形選択ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.矩形選択ToolStripMenuItem.Text = "矩形選択";
			// 
			// 矩形選択解除ToolStripMenuItem
			// 
			this.矩形選択解除ToolStripMenuItem.Enabled = false;
			this.矩形選択解除ToolStripMenuItem.Name = "矩形選択解除ToolStripMenuItem";
			this.矩形選択解除ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.矩形選択解除ToolStripMenuItem.Text = "矩形選択の解除";
			// 
			// CtrlZMenuItem
			// 
			this.CtrlZMenuItem.Name = "CtrlZMenuItem";
			this.CtrlZMenuItem.Size = new System.Drawing.Size(162, 22);
			this.CtrlZMenuItem.Text = "Ctrl_Z";
			this.CtrlZMenuItem.Click += new System.EventHandler(this.CtrlZMenuItem_Click);
			// 
			// CtrlYMenuItem
			// 
			this.CtrlYMenuItem.Name = "CtrlYMenuItem";
			this.CtrlYMenuItem.Size = new System.Drawing.Size(162, 22);
			this.CtrlYMenuItem.Text = "Ctrl_Y";
			this.CtrlYMenuItem.Click += new System.EventHandler(this.CtrlYMenuItem_Click);
			// 
			// GCMenuItem
			// 
			this.GCMenuItem.Name = "GCMenuItem";
			this.GCMenuItem.Size = new System.Drawing.Size(162, 22);
			this.GCMenuItem.Text = "GC";
			this.GCMenuItem.Click += new System.EventHandler(this.GCMenuItem_Click);
			// 
			// MPicBoundsBugMenuItem
			// 
			this.MPicBoundsBugMenuItem.Name = "MPicBoundsBugMenuItem";
			this.MPicBoundsBugMenuItem.Size = new System.Drawing.Size(162, 22);
			this.MPicBoundsBugMenuItem.Text = "MPicBoundsBug";
			this.MPicBoundsBugMenuItem.Click += new System.EventHandler(this.MPicBoundsBugMenuItem_Click);
			// 
			// commandToolStripMenuItem
			// 
			this.commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.塗りつぶしToolStripMenuItem,
            this.テクスチャToolStripMenuItem,
            this.テクスチャ矩形タイルToolStripMenuItem,
            this.グラデーションToolStripMenuItem});
			this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
			this.commandToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
			this.commandToolStripMenuItem.Text = "Command";
			// 
			// 塗りつぶしToolStripMenuItem
			// 
			this.塗りつぶしToolStripMenuItem.Name = "塗りつぶしToolStripMenuItem";
			this.塗りつぶしToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.塗りつぶしToolStripMenuItem.Text = "塗りつぶし";
			this.塗りつぶしToolStripMenuItem.Click += new System.EventHandler(this.塗りつぶしToolStripMenuItem_Click);
			// 
			// テクスチャToolStripMenuItem
			// 
			this.テクスチャToolStripMenuItem.Name = "テクスチャToolStripMenuItem";
			this.テクスチャToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.テクスチャToolStripMenuItem.Text = "テクスチャ";
			this.テクスチャToolStripMenuItem.Click += new System.EventHandler(this.テクスチャToolStripMenuItem_Click);
			// 
			// テクスチャ矩形タイルToolStripMenuItem
			// 
			this.テクスチャ矩形タイルToolStripMenuItem.Name = "テクスチャ矩形タイルToolStripMenuItem";
			this.テクスチャ矩形タイルToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.テクスチャ矩形タイルToolStripMenuItem.Text = "テクスチャ_矩形タイル";
			this.テクスチャ矩形タイルToolStripMenuItem.Click += new System.EventHandler(this.テクスチャ矩形タイルToolStripMenuItem_Click);
			// 
			// グラデーションToolStripMenuItem
			// 
			this.グラデーションToolStripMenuItem.Name = "グラデーションToolStripMenuItem";
			this.グラデーションToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.グラデーションToolStripMenuItem.Text = "グラデーション";
			this.グラデーションToolStripMenuItem.Click += new System.EventHandler(this.グラデーションToolStripMenuItem_Click);
			// 
			// command2ToolStripMenuItem
			// 
			this.command2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PuzzleMenuItem});
			this.command2ToolStripMenuItem.Name = "command2ToolStripMenuItem";
			this.command2ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
			this.command2ToolStripMenuItem.Text = "Command2";
			// 
			// PuzzleMenuItem
			// 
			this.PuzzleMenuItem.Name = "PuzzleMenuItem";
			this.PuzzleMenuItem.Size = new System.Drawing.Size(152, 22);
			this.PuzzleMenuItem.Text = "Puzzle";
			this.PuzzleMenuItem.Click += new System.EventHandler(this.PuzzleMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.South,
            this.SouthWest});
			this.statusStrip1.Location = new System.Drawing.Point(0, 539);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// South
			// 
			this.South.Name = "South";
			this.South.Size = new System.Drawing.Size(722, 17);
			this.South.Spring = true;
			this.South.Text = "Ready...";
			this.South.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.South.Click += new System.EventHandler(this.South_Click);
			// 
			// SouthWest
			// 
			this.SouthWest.Name = "SouthWest";
			this.SouthWest.Size = new System.Drawing.Size(47, 17);
			this.SouthWest.Text = "Ready...";
			// 
			// MainPanel
			// 
			this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainPanel.Controls.Add(this.MainPicture);
			this.MainPanel.Location = new System.Drawing.Point(12, 27);
			this.MainPanel.Name = "MainPanel";
			this.MainPanel.Size = new System.Drawing.Size(760, 509);
			this.MainPanel.TabIndex = 1;
			this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
			// 
			// MainPicture
			// 
			this.MainPicture.Location = new System.Drawing.Point(3, 3);
			this.MainPicture.Name = "MainPicture";
			this.MainPicture.Size = new System.Drawing.Size(300, 300);
			this.MainPicture.TabIndex = 0;
			this.MainPicture.TabStop = false;
			this.MainPicture.Click += new System.EventHandler(this.MainPicture_Click);
			this.MainPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPicture_MouseDown);
			this.MainPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPicture_MouseMove);
			this.MainPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPicture_MouseUp);
			// 
			// MainWin
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.MainPanel);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "Simple Paint";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWin_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWin_DragEnter);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainWin_KeyPress);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.MainPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel South;
		private System.Windows.Forms.ToolStripStatusLabel SouthWest;
		private System.Windows.Forms.ToolStripMenuItem アプリToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.Panel MainPanel;
		private System.Windows.Forms.PictureBox MainPicture;
		private System.Windows.Forms.ToolStripMenuItem 編集ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem クリアToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem サイズ変更ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem ファイル読み込みToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ファイル書き出しToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem アンチエイリアスToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ペン先ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 塗りつぶしToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 矩形選択ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 矩形選択解除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 色ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 形ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 透明度ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlZMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 色2MenuItem;
		private System.Windows.Forms.ToolStripMenuItem 透明度2MenuItem;
		private System.Windows.Forms.ToolStripMenuItem 形2MenuItem;
		private System.Windows.Forms.ToolStripMenuItem テクスチャToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CtrlYMenuItem;
		private System.Windows.Forms.ToolStripMenuItem GCMenuItem;
		private System.Windows.Forms.ToolStripMenuItem command2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem PuzzleMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MPicBoundsBugMenuItem;
		private System.Windows.Forms.ToolStripMenuItem テクスチャ矩形タイルToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem グラデーションToolStripMenuItem;
	}
}

