﻿namespace Charlotte
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
			this.TaskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TTIMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.EndProcMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.TTIMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// TaskTrayIcon
			// 
			this.TaskTrayIcon.ContextMenuStrip = this.TTIMenu;
			this.TaskTrayIcon.Text = "NoScreenSaver";
			// 
			// TTIMenu
			// 
			this.TTIMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EndProcMenuItem});
			this.TTIMenu.Name = "TTIMenu";
			this.TTIMenu.Size = new System.Drawing.Size(153, 48);
			// 
			// EndProcMenuItem
			// 
			this.EndProcMenuItem.Name = "EndProcMenuItem";
			this.EndProcMenuItem.Size = new System.Drawing.Size(152, 22);
			this.EndProcMenuItem.Text = "Exit";
			this.EndProcMenuItem.Click += new System.EventHandler(this.EndProcMenuItem_Click);
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Interval = 200;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-400, -400);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "NoScreenSaver_HiddenMainWin";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.TTIMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon TaskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip TTIMenu;
		private System.Windows.Forms.ToolStripMenuItem EndProcMenuItem;
		private System.Windows.Forms.Timer MainTimer;
	}
}
