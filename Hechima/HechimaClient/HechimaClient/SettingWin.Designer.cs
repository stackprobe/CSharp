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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.BouyomiChanEnabled = new System.Windows.Forms.CheckBox();
			this.BouyomiChanPort = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.BouyomiChanDomain = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.Password = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.crypTunnelPort = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.ServerPort = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ServerDomain = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.MessageTextBackColorBtn = new System.Windows.Forms.Button();
			this.MessageTextForeColorBtn = new System.Windows.Forms.Button();
			this.RemarksTextBackColorBtn = new System.Windows.Forms.Button();
			this.RemarksTextForeColorBtn = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.RemarksTextFontSize = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.RemarksTextFontFamily = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.RemarkFormat = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.MessageTextEnterMode = new System.Windows.Forms.ComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label13 = new System.Windows.Forms.Label();
			this.UserTrip = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.UserName = new System.Windows.Forms.TextBox();
			this.UpdateUserTripBtn = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.OkBtn = new System.Windows.Forms.Button();
			this.CorrectBtn = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(560, 487);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.label5);
			this.tabPage1.Controls.Add(this.Password);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 29);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(552, 454);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "接続";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.BouyomiChanEnabled);
			this.groupBox2.Controls.Add(this.BouyomiChanPort);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.BouyomiChanDomain);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(6, 175);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(540, 100);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "棒読みちゃん";
			// 
			// BouyomiChanEnabled
			// 
			this.BouyomiChanEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BouyomiChanEnabled.AutoSize = true;
			this.BouyomiChanEnabled.Location = new System.Drawing.Point(480, 61);
			this.BouyomiChanEnabled.Name = "BouyomiChanEnabled";
			this.BouyomiChanEnabled.Size = new System.Drawing.Size(54, 24);
			this.BouyomiChanEnabled.TabIndex = 4;
			this.BouyomiChanEnabled.Text = "有効";
			this.BouyomiChanEnabled.UseVisualStyleBackColor = true;
			// 
			// BouyomiChanPort
			// 
			this.BouyomiChanPort.Location = new System.Drawing.Point(122, 59);
			this.BouyomiChanPort.MaxLength = 5;
			this.BouyomiChanPort.Name = "BouyomiChanPort";
			this.BouyomiChanPort.Size = new System.Drawing.Size(100, 27);
			this.BouyomiChanPort.TabIndex = 3;
			this.BouyomiChanPort.Text = "50001";
			this.BouyomiChanPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 20);
			this.label6.TabIndex = 2;
			this.label6.Text = "ポート番号 :";
			// 
			// BouyomiChanDomain
			// 
			this.BouyomiChanDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BouyomiChanDomain.Location = new System.Drawing.Point(122, 26);
			this.BouyomiChanDomain.MaxLength = 300;
			this.BouyomiChanDomain.Name = "BouyomiChanDomain";
			this.BouyomiChanDomain.Size = new System.Drawing.Size(412, 27);
			this.BouyomiChanDomain.TabIndex = 1;
			this.BouyomiChanDomain.Text = "localhost";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 29);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(105, 20);
			this.label7.TabIndex = 0;
			this.label7.Text = "IP or ホスト名 :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 145);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 20);
			this.label5.TabIndex = 1;
			this.label5.Text = "パスワード :";
			// 
			// Password
			// 
			this.Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Password.Location = new System.Drawing.Point(128, 142);
			this.Password.MaxLength = 1000;
			this.Password.Name = "Password";
			this.Password.Size = new System.Drawing.Size(418, 27);
			this.Password.TabIndex = 2;
			this.Password.Text = "aa9999[x22]";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.crypTunnelPort);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.ServerPort);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.ServerDomain);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(540, 130);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "鯖";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.Teal;
			this.label4.Location = new System.Drawing.Point(228, 95);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(272, 20);
			this.label4.TabIndex = 6;
			this.label4.Text = "このPCの空いているTCPポートを指定してね";
			// 
			// crypTunnelPort
			// 
			this.crypTunnelPort.Location = new System.Drawing.Point(122, 92);
			this.crypTunnelPort.MaxLength = 5;
			this.crypTunnelPort.Name = "crypTunnelPort";
			this.crypTunnelPort.Size = new System.Drawing.Size(100, 27);
			this.crypTunnelPort.TabIndex = 5;
			this.crypTunnelPort.Text = "52525";
			this.crypTunnelPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "中継ポート番号 :";
			// 
			// ServerPort
			// 
			this.ServerPort.Location = new System.Drawing.Point(122, 59);
			this.ServerPort.MaxLength = 5;
			this.ServerPort.Name = "ServerPort";
			this.ServerPort.Size = new System.Drawing.Size(100, 27);
			this.ServerPort.TabIndex = 3;
			this.ServerPort.Text = "52255";
			this.ServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "ポート番号 :";
			// 
			// ServerDomain
			// 
			this.ServerDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ServerDomain.Location = new System.Drawing.Point(122, 26);
			this.ServerDomain.MaxLength = 300;
			this.ServerDomain.Name = "ServerDomain";
			this.ServerDomain.Size = new System.Drawing.Size(412, 27);
			this.ServerDomain.TabIndex = 1;
			this.ServerDomain.Text = "localhost";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "IP or ホスト名 :";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label14);
			this.tabPage2.Controls.Add(this.MessageTextBackColorBtn);
			this.tabPage2.Controls.Add(this.MessageTextForeColorBtn);
			this.tabPage2.Controls.Add(this.RemarksTextBackColorBtn);
			this.tabPage2.Controls.Add(this.RemarksTextForeColorBtn);
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Controls.Add(this.label9);
			this.tabPage2.Controls.Add(this.RemarkFormat);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.MessageTextEnterMode);
			this.tabPage2.Location = new System.Drawing.Point(4, 29);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(552, 454);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "画面";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// MessageTextBackColorBtn
			// 
			this.MessageTextBackColorBtn.Location = new System.Drawing.Point(262, 325);
			this.MessageTextBackColorBtn.Name = "MessageTextBackColorBtn";
			this.MessageTextBackColorBtn.Size = new System.Drawing.Size(250, 40);
			this.MessageTextBackColorBtn.TabIndex = 9;
			this.MessageTextBackColorBtn.Text = "入力メッセージ背景色 = ffffff";
			this.MessageTextBackColorBtn.UseVisualStyleBackColor = true;
			this.MessageTextBackColorBtn.Click += new System.EventHandler(this.MessageTextBackColorBtn_Click);
			// 
			// MessageTextForeColorBtn
			// 
			this.MessageTextForeColorBtn.Location = new System.Drawing.Point(6, 325);
			this.MessageTextForeColorBtn.Name = "MessageTextForeColorBtn";
			this.MessageTextForeColorBtn.Size = new System.Drawing.Size(250, 40);
			this.MessageTextForeColorBtn.TabIndex = 8;
			this.MessageTextForeColorBtn.Text = "入力メッセージ文字色 = 000000";
			this.MessageTextForeColorBtn.UseVisualStyleBackColor = true;
			this.MessageTextForeColorBtn.Click += new System.EventHandler(this.MessageTextForeColorBtn_Click);
			// 
			// RemarksTextBackColorBtn
			// 
			this.RemarksTextBackColorBtn.Location = new System.Drawing.Point(262, 279);
			this.RemarksTextBackColorBtn.Name = "RemarksTextBackColorBtn";
			this.RemarksTextBackColorBtn.Size = new System.Drawing.Size(250, 40);
			this.RemarksTextBackColorBtn.TabIndex = 7;
			this.RemarksTextBackColorBtn.Text = "発言リスト背景色 = ffffff";
			this.RemarksTextBackColorBtn.UseVisualStyleBackColor = true;
			this.RemarksTextBackColorBtn.Click += new System.EventHandler(this.RemarksTextBackColorBtn_Click);
			// 
			// RemarksTextForeColorBtn
			// 
			this.RemarksTextForeColorBtn.Location = new System.Drawing.Point(6, 279);
			this.RemarksTextForeColorBtn.Name = "RemarksTextForeColorBtn";
			this.RemarksTextForeColorBtn.Size = new System.Drawing.Size(250, 40);
			this.RemarksTextForeColorBtn.TabIndex = 6;
			this.RemarksTextForeColorBtn.Text = "発言リスト文字色 = 000000";
			this.RemarksTextForeColorBtn.UseVisualStyleBackColor = true;
			this.RemarksTextForeColorBtn.Click += new System.EventHandler(this.RemarksTextForeColorBtn_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.RemarksTextFontSize);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.RemarksTextFontFamily);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Location = new System.Drawing.Point(6, 173);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(540, 100);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "発言リストのフォント";
			// 
			// RemarksTextFontSize
			// 
			this.RemarksTextFontSize.Location = new System.Drawing.Point(96, 59);
			this.RemarksTextFontSize.MaxLength = 2;
			this.RemarksTextFontSize.Name = "RemarksTextFontSize";
			this.RemarksTextFontSize.Size = new System.Drawing.Size(50, 27);
			this.RemarksTextFontSize.TabIndex = 3;
			this.RemarksTextFontSize.Text = "10";
			this.RemarksTextFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 62);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(58, 20);
			this.label10.TabIndex = 2;
			this.label10.Text = "サイズ :";
			// 
			// RemarksTextFontFamily
			// 
			this.RemarksTextFontFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RemarksTextFontFamily.Location = new System.Drawing.Point(96, 26);
			this.RemarksTextFontFamily.MaxLength = 300;
			this.RemarksTextFontFamily.Name = "RemarksTextFontFamily";
			this.RemarksTextFontFamily.Size = new System.Drawing.Size(438, 27);
			this.RemarksTextFontFamily.TabIndex = 1;
			this.RemarksTextFontFamily.Text = "メイリオ";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 29);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(84, 20);
			this.label11.TabIndex = 0;
			this.label11.Text = "フォント名 :";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.Teal;
			this.label9.Location = new System.Drawing.Point(235, 70);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(113, 100);
			this.label9.TabIndex = 3;
			this.label9.Text = "R == 改行\r\nS == 日時\r\nB == ブランク\r\nI == ident\r\nM == メッセージ";
			// 
			// RemarkFormat
			// 
			this.RemarkFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RemarkFormat.Location = new System.Drawing.Point(135, 40);
			this.RemarkFormat.MaxLength = 100;
			this.RemarkFormat.Name = "RemarkFormat";
			this.RemarkFormat.Size = new System.Drawing.Size(411, 27);
			this.RemarkFormat.TabIndex = 2;
			this.RemarkFormat.Text = "RSBIRRMR";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 43);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(123, 20);
			this.label8.TabIndex = 1;
			this.label8.Text = "発言フォーマット :";
			// 
			// MessageTextEnterMode
			// 
			this.MessageTextEnterMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessageTextEnterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MessageTextEnterMode.FormattingEnabled = true;
			this.MessageTextEnterMode.Items.AddRange(new object[] {
            "Enter で送信、Ctrl + Enter で改行",
            "Ctrl + Enter で送信、Enter で改行"});
			this.MessageTextEnterMode.Location = new System.Drawing.Point(6, 6);
			this.MessageTextEnterMode.Name = "MessageTextEnterMode";
			this.MessageTextEnterMode.Size = new System.Drawing.Size(540, 28);
			this.MessageTextEnterMode.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox4);
			this.tabPage3.Location = new System.Drawing.Point(4, 29);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(552, 454);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "ユーザー";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.UserTrip);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.UserName);
			this.groupBox4.Controls.Add(this.UpdateUserTripBtn);
			this.groupBox4.Location = new System.Drawing.Point(6, 6);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(540, 150);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "ユーザー名";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(6, 62);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(71, 20);
			this.label13.TabIndex = 2;
			this.label13.Text = "トリップ :";
			// 
			// UserTrip
			// 
			this.UserTrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UserTrip.Location = new System.Drawing.Point(83, 59);
			this.UserTrip.MaxLength = 100;
			this.UserTrip.Name = "UserTrip";
			this.UserTrip.Size = new System.Drawing.Size(451, 27);
			this.UserTrip.TabIndex = 3;
			this.UserTrip.Text = "0123456789abcdef0123456789abcdef";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 29);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(45, 20);
			this.label12.TabIndex = 0;
			this.label12.Text = "名前 :";
			// 
			// UserName
			// 
			this.UserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UserName.Location = new System.Drawing.Point(83, 26);
			this.UserName.MaxLength = 20;
			this.UserName.Name = "UserName";
			this.UserName.Size = new System.Drawing.Size(451, 27);
			this.UserName.TabIndex = 1;
			this.UserName.Text = "名無しさん12345";
			// 
			// UpdateUserTripBtn
			// 
			this.UpdateUserTripBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateUserTripBtn.Location = new System.Drawing.Point(434, 92);
			this.UpdateUserTripBtn.Name = "UpdateUserTripBtn";
			this.UpdateUserTripBtn.Size = new System.Drawing.Size(100, 40);
			this.UpdateUserTripBtn.TabIndex = 4;
			this.UpdateUserTripBtn.Text = "トリップ更新";
			this.UpdateUserTripBtn.UseVisualStyleBackColor = true;
			this.UpdateUserTripBtn.Click += new System.EventHandler(this.UpdateUserTripBtn_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBtn.Location = new System.Drawing.Point(462, 505);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(110, 45);
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "キャンセル";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// OkBtn
			// 
			this.OkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkBtn.Location = new System.Drawing.Point(346, 505);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.Size = new System.Drawing.Size(110, 45);
			this.OkBtn.TabIndex = 1;
			this.OkBtn.Text = "OK";
			this.OkBtn.UseVisualStyleBackColor = true;
			this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
			// 
			// CorrectBtn
			// 
			this.CorrectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CorrectBtn.Location = new System.Drawing.Point(12, 505);
			this.CorrectBtn.Name = "CorrectBtn";
			this.CorrectBtn.Size = new System.Drawing.Size(110, 45);
			this.CorrectBtn.TabIndex = 3;
			this.CorrectBtn.Text = "補正";
			this.CorrectBtn.UseVisualStyleBackColor = true;
			this.CorrectBtn.Click += new System.EventHandler(this.CorrectBtn_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.ForeColor = System.Drawing.Color.Teal;
			this.label14.Location = new System.Drawing.Point(354, 70);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(192, 80);
			this.label14.TabIndex = 4;
			this.label14.Text = "設定例:\r\n標準 == RSBIRRMR\r\n改行少なめ == SBIRMR\r\n改行もっと少なめ == SBIBMR";
			// 
			// SettingWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 562);
			this.Controls.Add(this.CorrectBtn);
			this.Controls.Add(this.OkBtn);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "SettingWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "へちまの設定";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SettingWin_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox BouyomiChanPort;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox BouyomiChanDomain;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox Password;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox crypTunnelPort;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox ServerPort;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ServerDomain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox BouyomiChanEnabled;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox RemarkFormat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox MessageTextEnterMode;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button OkBtn;
		private System.Windows.Forms.Button MessageTextBackColorBtn;
		private System.Windows.Forms.Button MessageTextForeColorBtn;
		private System.Windows.Forms.Button RemarksTextBackColorBtn;
		private System.Windows.Forms.Button RemarksTextForeColorBtn;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox RemarksTextFontSize;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox RemarksTextFontFamily;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox UserTrip;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox UserName;
		private System.Windows.Forms.Button UpdateUserTripBtn;
		private System.Windows.Forms.Button CorrectBtn;
		private System.Windows.Forms.Label label14;
	}
}
