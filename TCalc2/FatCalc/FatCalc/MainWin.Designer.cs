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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.アプリAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.基数RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.精度小数点以下x桁までBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.角括弧表記する数字MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.リセットRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.SouthStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.SouthEastStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainTab = new System.Windows.Forms.TabControl();
			this.TabFourOperation = new System.Windows.Forms.TabPage();
			this.Ans = new System.Windows.Forms.TextBox();
			this.Op2 = new System.Windows.Forms.TextBox();
			this.Op1 = new System.Windows.Forms.TextBox();
			this.SwapOp12 = new System.Windows.Forms.Button();
			this.AnsOp2 = new System.Windows.Forms.Button();
			this.AnsOp1 = new System.Windows.Forms.Button();
			this.Op2Lb = new System.Windows.Forms.Label();
			this.Op1Lb = new System.Windows.Forms.Label();
			this.AnsCB = new System.Windows.Forms.Button();
			this.Op2CB = new System.Windows.Forms.Button();
			this.Op1CB = new System.Windows.Forms.Button();
			this.Execute = new System.Windows.Forms.Button();
			this.Operation = new System.Windows.Forms.ComboBox();
			this.TabPowerRoot = new System.Windows.Forms.TabPage();
			this.PrAnsOp = new System.Windows.Forms.Button();
			this.PrExponent = new System.Windows.Forms.TextBox();
			this.PrAnsCB = new System.Windows.Forms.Button();
			this.PrAns = new System.Windows.Forms.TextBox();
			this.PrOp = new System.Windows.Forms.TextBox();
			this.PrOpLb = new System.Windows.Forms.Label();
			this.PrOpCB = new System.Windows.Forms.Button();
			this.PrExecute = new System.Windows.Forms.Button();
			this.PrOperation = new System.Windows.Forms.ComboBox();
			this.TabChangeRadix = new System.Windows.Forms.TabPage();
			this.CxAnsRadixLb = new System.Windows.Forms.Label();
			this.CxOpRadixLb = new System.Windows.Forms.Label();
			this.CxAnsRadix = new System.Windows.Forms.TextBox();
			this.CxAnsOp = new System.Windows.Forms.Button();
			this.CxOpRadix = new System.Windows.Forms.TextBox();
			this.CxAnsCB = new System.Windows.Forms.Button();
			this.CxAns = new System.Windows.Forms.TextBox();
			this.CxOp = new System.Windows.Forms.TextBox();
			this.CxOpLb = new System.Windows.Forms.Label();
			this.CxOpCB = new System.Windows.Forms.Button();
			this.CxExecute = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.MainTab.SuspendLayout();
			this.TabFourOperation.SuspendLayout();
			this.TabPowerRoot.SuspendLayout();
			this.TabChangeRadix.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリAToolStripMenuItem,
            this.設定SToolStripMenuItem,
            this.表示VToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(555, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
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
            this.基数RToolStripMenuItem,
            this.精度小数点以下x桁までBToolStripMenuItem,
            this.角括弧表記する数字MToolStripMenuItem});
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.設定SToolStripMenuItem.Text = "設定(&S)";
			// 
			// 基数RToolStripMenuItem
			// 
			this.基数RToolStripMenuItem.Name = "基数RToolStripMenuItem";
			this.基数RToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.基数RToolStripMenuItem.Text = "基数(&R)";
			this.基数RToolStripMenuItem.Click += new System.EventHandler(this.基数RToolStripMenuItem_Click);
			// 
			// 精度小数点以下x桁までBToolStripMenuItem
			// 
			this.精度小数点以下x桁までBToolStripMenuItem.Name = "精度小数点以下x桁までBToolStripMenuItem";
			this.精度小数点以下x桁までBToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.精度小数点以下x桁までBToolStripMenuItem.Text = "精度 - 小数点以下？桁まで(&B)";
			this.精度小数点以下x桁までBToolStripMenuItem.Click += new System.EventHandler(this.精度小数点以下x桁までBToolStripMenuItem_Click);
			// 
			// 角括弧表記する数字MToolStripMenuItem
			// 
			this.角括弧表記する数字MToolStripMenuItem.Name = "角括弧表記する数字MToolStripMenuItem";
			this.角括弧表記する数字MToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.角括弧表記する数字MToolStripMenuItem.Text = "角括弧表記する最小の数字(M)";
			this.角括弧表記する数字MToolStripMenuItem.Click += new System.EventHandler(this.角括弧表記する数字MToolStripMenuItem_Click);
			// 
			// 表示VToolStripMenuItem
			// 
			this.表示VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.リセットRToolStripMenuItem});
			this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
			this.表示VToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
			this.表示VToolStripMenuItem.Text = "表示(&V)";
			// 
			// リセットRToolStripMenuItem
			// 
			this.リセットRToolStripMenuItem.Name = "リセットRToolStripMenuItem";
			this.リセットRToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
			this.リセットRToolStripMenuItem.Text = "リセット(&R)";
			this.リセットRToolStripMenuItem.Click += new System.EventHandler(this.リセットRToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SouthStatus,
            this.SouthEastStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 509);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(555, 23);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// SouthStatus
			// 
			this.SouthStatus.Name = "SouthStatus";
			this.SouthStatus.Size = new System.Drawing.Size(436, 18);
			this.SouthStatus.Spring = true;
			this.SouthStatus.Text = "準備しています...";
			this.SouthStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SouthEastStatus
			// 
			this.SouthEastStatus.Name = "SouthEastStatus";
			this.SouthEastStatus.Size = new System.Drawing.Size(104, 18);
			this.SouthEastStatus.Text = "準備しています...";
			// 
			// MainTab
			// 
			this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTab.Controls.Add(this.TabFourOperation);
			this.MainTab.Controls.Add(this.TabPowerRoot);
			this.MainTab.Controls.Add(this.TabChangeRadix);
			this.MainTab.Location = new System.Drawing.Point(12, 29);
			this.MainTab.Name = "MainTab";
			this.MainTab.SelectedIndex = 0;
			this.MainTab.Size = new System.Drawing.Size(531, 477);
			this.MainTab.TabIndex = 1;
			this.MainTab.SelectedIndexChanged += new System.EventHandler(this.MainTab_SelectedIndexChanged);
			// 
			// TabFourOperation
			// 
			this.TabFourOperation.Controls.Add(this.Ans);
			this.TabFourOperation.Controls.Add(this.Op2);
			this.TabFourOperation.Controls.Add(this.Op1);
			this.TabFourOperation.Controls.Add(this.SwapOp12);
			this.TabFourOperation.Controls.Add(this.AnsOp2);
			this.TabFourOperation.Controls.Add(this.AnsOp1);
			this.TabFourOperation.Controls.Add(this.Op2Lb);
			this.TabFourOperation.Controls.Add(this.Op1Lb);
			this.TabFourOperation.Controls.Add(this.AnsCB);
			this.TabFourOperation.Controls.Add(this.Op2CB);
			this.TabFourOperation.Controls.Add(this.Op1CB);
			this.TabFourOperation.Controls.Add(this.Execute);
			this.TabFourOperation.Controls.Add(this.Operation);
			this.TabFourOperation.Location = new System.Drawing.Point(4, 29);
			this.TabFourOperation.Name = "TabFourOperation";
			this.TabFourOperation.Padding = new System.Windows.Forms.Padding(3);
			this.TabFourOperation.Size = new System.Drawing.Size(523, 444);
			this.TabFourOperation.TabIndex = 0;
			this.TabFourOperation.Text = "加減乗除 - 剰余";
			this.TabFourOperation.UseVisualStyleBackColor = true;
			// 
			// Ans
			// 
			this.Ans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Ans.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Ans.Location = new System.Drawing.Point(6, 252);
			this.Ans.MaxLength = 0;
			this.Ans.Multiline = true;
			this.Ans.Name = "Ans";
			this.Ans.ReadOnly = true;
			this.Ans.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Ans.Size = new System.Drawing.Size(511, 186);
			this.Ans.TabIndex = 9;
			this.Ans.Text = "1行目\r\n2行目\r\n3行目\r\n4行目\r\n5行目\r\n6行目\r\n7行目\r\n8行目\r\n9行目";
			this.Ans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ans_KeyPress);
			// 
			// Op2
			// 
			this.Op2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Op2.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Op2.Location = new System.Drawing.Point(6, 146);
			this.Op2.MaxLength = 100000;
			this.Op2.Multiline = true;
			this.Op2.Name = "Op2";
			this.Op2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Op2.Size = new System.Drawing.Size(511, 66);
			this.Op2.TabIndex = 4;
			this.Op2.Text = "1行目\r\n2行目\r\n3行目";
			this.Op2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Op2_KeyPress);
			// 
			// Op1
			// 
			this.Op1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Op1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Op1.Location = new System.Drawing.Point(6, 40);
			this.Op1.MaxLength = 100000;
			this.Op1.Multiline = true;
			this.Op1.Name = "Op1";
			this.Op1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Op1.Size = new System.Drawing.Size(511, 66);
			this.Op1.TabIndex = 1;
			this.Op1.Text = "123456789112345678921234567893123456789412345678951234567896\r\n2行目\r\n3行目";
			this.Op1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Op1_KeyPress);
			// 
			// SwapOp12
			// 
			this.SwapOp12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SwapOp12.Location = new System.Drawing.Point(289, 112);
			this.SwapOp12.Name = "SwapOp12";
			this.SwapOp12.Size = new System.Drawing.Size(133, 28);
			this.SwapOp12.TabIndex = 5;
			this.SwapOp12.Text = "op1-2入れ替え";
			this.SwapOp12.UseVisualStyleBackColor = true;
			this.SwapOp12.Click += new System.EventHandler(this.SwapOp12_Click);
			// 
			// AnsOp2
			// 
			this.AnsOp2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AnsOp2.Location = new System.Drawing.Point(333, 218);
			this.AnsOp2.Name = "AnsOp2";
			this.AnsOp2.Size = new System.Drawing.Size(89, 28);
			this.AnsOp2.TabIndex = 11;
			this.AnsOp2.Text = "op2へ";
			this.AnsOp2.UseVisualStyleBackColor = true;
			this.AnsOp2.Click += new System.EventHandler(this.AnsOp2_Click);
			// 
			// AnsOp1
			// 
			this.AnsOp1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AnsOp1.Location = new System.Drawing.Point(238, 218);
			this.AnsOp1.Name = "AnsOp1";
			this.AnsOp1.Size = new System.Drawing.Size(89, 28);
			this.AnsOp1.TabIndex = 10;
			this.AnsOp1.Text = "op1へ";
			this.AnsOp1.UseVisualStyleBackColor = true;
			this.AnsOp1.Click += new System.EventHandler(this.AnsOp1_Click);
			// 
			// Op2Lb
			// 
			this.Op2Lb.AutoSize = true;
			this.Op2Lb.Location = new System.Drawing.Point(6, 123);
			this.Op2Lb.Name = "Op2Lb";
			this.Op2Lb.Size = new System.Drawing.Size(39, 20);
			this.Op2Lb.TabIndex = 3;
			this.Op2Lb.Text = "op2:";
			// 
			// Op1Lb
			// 
			this.Op1Lb.AutoSize = true;
			this.Op1Lb.Location = new System.Drawing.Point(6, 17);
			this.Op1Lb.Name = "Op1Lb";
			this.Op1Lb.Size = new System.Drawing.Size(39, 20);
			this.Op1Lb.TabIndex = 0;
			this.Op1Lb.Text = "op1:";
			// 
			// AnsCB
			// 
			this.AnsCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AnsCB.Location = new System.Drawing.Point(428, 218);
			this.AnsCB.Name = "AnsCB";
			this.AnsCB.Size = new System.Drawing.Size(89, 28);
			this.AnsCB.TabIndex = 12;
			this.AnsCB.Text = "CBへ";
			this.AnsCB.UseVisualStyleBackColor = true;
			this.AnsCB.Click += new System.EventHandler(this.AnsCB_Click);
			// 
			// Op2CB
			// 
			this.Op2CB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Op2CB.Location = new System.Drawing.Point(428, 112);
			this.Op2CB.Name = "Op2CB";
			this.Op2CB.Size = new System.Drawing.Size(89, 28);
			this.Op2CB.TabIndex = 6;
			this.Op2CB.Text = "CBから";
			this.Op2CB.UseVisualStyleBackColor = true;
			this.Op2CB.Click += new System.EventHandler(this.Op2CB_Click);
			// 
			// Op1CB
			// 
			this.Op1CB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Op1CB.Location = new System.Drawing.Point(428, 6);
			this.Op1CB.Name = "Op1CB";
			this.Op1CB.Size = new System.Drawing.Size(89, 28);
			this.Op1CB.TabIndex = 2;
			this.Op1CB.Text = "CBから";
			this.Op1CB.UseVisualStyleBackColor = true;
			this.Op1CB.Click += new System.EventHandler(this.Op1CB_Click);
			// 
			// Execute
			// 
			this.Execute.Location = new System.Drawing.Point(101, 218);
			this.Execute.Name = "Execute";
			this.Execute.Size = new System.Drawing.Size(89, 28);
			this.Execute.TabIndex = 8;
			this.Execute.Text = "=";
			this.Execute.UseVisualStyleBackColor = true;
			this.Execute.Click += new System.EventHandler(this.Execute_Click);
			// 
			// Operation
			// 
			this.Operation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Operation.FormattingEnabled = true;
			this.Operation.Items.AddRange(new object[] {
            "＋",
            "－",
            "×",
            "÷",
            "剰余",
            "剰余(整数)"});
			this.Operation.Location = new System.Drawing.Point(6, 218);
			this.Operation.Name = "Operation";
			this.Operation.Size = new System.Drawing.Size(89, 28);
			this.Operation.TabIndex = 7;
			this.Operation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Operation_KeyPress);
			// 
			// TabPowerRoot
			// 
			this.TabPowerRoot.Controls.Add(this.PrAnsOp);
			this.TabPowerRoot.Controls.Add(this.PrExponent);
			this.TabPowerRoot.Controls.Add(this.PrAnsCB);
			this.TabPowerRoot.Controls.Add(this.PrAns);
			this.TabPowerRoot.Controls.Add(this.PrOp);
			this.TabPowerRoot.Controls.Add(this.PrOpLb);
			this.TabPowerRoot.Controls.Add(this.PrOpCB);
			this.TabPowerRoot.Controls.Add(this.PrExecute);
			this.TabPowerRoot.Controls.Add(this.PrOperation);
			this.TabPowerRoot.Location = new System.Drawing.Point(4, 29);
			this.TabPowerRoot.Name = "TabPowerRoot";
			this.TabPowerRoot.Padding = new System.Windows.Forms.Padding(3);
			this.TabPowerRoot.Size = new System.Drawing.Size(523, 444);
			this.TabPowerRoot.TabIndex = 1;
			this.TabPowerRoot.Text = "べき乗 - べき根";
			this.TabPowerRoot.UseVisualStyleBackColor = true;
			// 
			// PrAnsOp
			// 
			this.PrAnsOp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PrAnsOp.Location = new System.Drawing.Point(333, 112);
			this.PrAnsOp.Name = "PrAnsOp";
			this.PrAnsOp.Size = new System.Drawing.Size(89, 28);
			this.PrAnsOp.TabIndex = 7;
			this.PrAnsOp.Text = "opへ";
			this.PrAnsOp.UseVisualStyleBackColor = true;
			this.PrAnsOp.Click += new System.EventHandler(this.PrAnsOp_Click);
			// 
			// PrExponent
			// 
			this.PrExponent.Location = new System.Drawing.Point(117, 112);
			this.PrExponent.MaxLength = 10;
			this.PrExponent.Name = "PrExponent";
			this.PrExponent.Size = new System.Drawing.Size(105, 27);
			this.PrExponent.TabIndex = 4;
			this.PrExponent.Text = "1234567890";
			this.PrExponent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PrExponent.TextChanged += new System.EventHandler(this.PrExponent_TextChanged);
			this.PrExponent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrExponent_KeyPress);
			// 
			// PrAnsCB
			// 
			this.PrAnsCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PrAnsCB.Location = new System.Drawing.Point(428, 112);
			this.PrAnsCB.Name = "PrAnsCB";
			this.PrAnsCB.Size = new System.Drawing.Size(89, 28);
			this.PrAnsCB.TabIndex = 8;
			this.PrAnsCB.Text = "CBへ";
			this.PrAnsCB.UseVisualStyleBackColor = true;
			this.PrAnsCB.Click += new System.EventHandler(this.PrAnsCB_Click);
			// 
			// PrAns
			// 
			this.PrAns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PrAns.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.PrAns.Location = new System.Drawing.Point(6, 145);
			this.PrAns.MaxLength = 0;
			this.PrAns.Multiline = true;
			this.PrAns.Name = "PrAns";
			this.PrAns.ReadOnly = true;
			this.PrAns.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.PrAns.Size = new System.Drawing.Size(511, 293);
			this.PrAns.TabIndex = 6;
			this.PrAns.Text = "準備しています...";
			this.PrAns.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrAns_KeyPress);
			// 
			// PrOp
			// 
			this.PrOp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PrOp.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.PrOp.Location = new System.Drawing.Point(6, 40);
			this.PrOp.MaxLength = 100000;
			this.PrOp.Multiline = true;
			this.PrOp.Name = "PrOp";
			this.PrOp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.PrOp.Size = new System.Drawing.Size(511, 66);
			this.PrOp.TabIndex = 1;
			this.PrOp.Text = "1行目\r\n2行目\r\n3行目";
			this.PrOp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrOp_KeyPress);
			// 
			// PrOpLb
			// 
			this.PrOpLb.AutoSize = true;
			this.PrOpLb.Location = new System.Drawing.Point(6, 17);
			this.PrOpLb.Name = "PrOpLb";
			this.PrOpLb.Size = new System.Drawing.Size(31, 20);
			this.PrOpLb.TabIndex = 0;
			this.PrOpLb.Text = "op:";
			// 
			// PrOpCB
			// 
			this.PrOpCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PrOpCB.Location = new System.Drawing.Point(428, 6);
			this.PrOpCB.Name = "PrOpCB";
			this.PrOpCB.Size = new System.Drawing.Size(89, 28);
			this.PrOpCB.TabIndex = 2;
			this.PrOpCB.Text = "CBから";
			this.PrOpCB.UseVisualStyleBackColor = true;
			this.PrOpCB.Click += new System.EventHandler(this.PrOpCB_Click);
			// 
			// PrExecute
			// 
			this.PrExecute.Location = new System.Drawing.Point(228, 112);
			this.PrExecute.Name = "PrExecute";
			this.PrExecute.Size = new System.Drawing.Size(89, 28);
			this.PrExecute.TabIndex = 5;
			this.PrExecute.Text = "=";
			this.PrExecute.UseVisualStyleBackColor = true;
			this.PrExecute.Click += new System.EventHandler(this.PrExecute_Click);
			// 
			// PrOperation
			// 
			this.PrOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PrOperation.FormattingEnabled = true;
			this.PrOperation.Items.AddRange(new object[] {
            "べき乗(^n)",
            "べき根(n√)"});
			this.PrOperation.Location = new System.Drawing.Point(6, 112);
			this.PrOperation.Name = "PrOperation";
			this.PrOperation.Size = new System.Drawing.Size(105, 28);
			this.PrOperation.TabIndex = 3;
			this.PrOperation.SelectedIndexChanged += new System.EventHandler(this.PrOperation_SelectedIndexChanged);
			this.PrOperation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrOperation_KeyPress);
			// 
			// TabChangeRadix
			// 
			this.TabChangeRadix.Controls.Add(this.CxAnsRadixLb);
			this.TabChangeRadix.Controls.Add(this.CxOpRadixLb);
			this.TabChangeRadix.Controls.Add(this.CxAnsRadix);
			this.TabChangeRadix.Controls.Add(this.CxAnsOp);
			this.TabChangeRadix.Controls.Add(this.CxOpRadix);
			this.TabChangeRadix.Controls.Add(this.CxAnsCB);
			this.TabChangeRadix.Controls.Add(this.CxAns);
			this.TabChangeRadix.Controls.Add(this.CxOp);
			this.TabChangeRadix.Controls.Add(this.CxOpLb);
			this.TabChangeRadix.Controls.Add(this.CxOpCB);
			this.TabChangeRadix.Controls.Add(this.CxExecute);
			this.TabChangeRadix.Location = new System.Drawing.Point(4, 29);
			this.TabChangeRadix.Name = "TabChangeRadix";
			this.TabChangeRadix.Size = new System.Drawing.Size(523, 444);
			this.TabChangeRadix.TabIndex = 2;
			this.TabChangeRadix.Text = "基数変換";
			this.TabChangeRadix.UseVisualStyleBackColor = true;
			// 
			// CxAnsRadixLb
			// 
			this.CxAnsRadixLb.AutoSize = true;
			this.CxAnsRadixLb.Location = new System.Drawing.Point(6, 245);
			this.CxAnsRadixLb.Name = "CxAnsRadixLb";
			this.CxAnsRadixLb.Size = new System.Drawing.Size(93, 20);
			this.CxAnsRadixLb.TabIndex = 5;
			this.CxAnsRadixLb.Text = "変換後の基数:";
			// 
			// CxOpRadixLb
			// 
			this.CxOpRadixLb.AutoSize = true;
			this.CxOpRadixLb.Location = new System.Drawing.Point(6, 212);
			this.CxOpRadixLb.Name = "CxOpRadixLb";
			this.CxOpRadixLb.Size = new System.Drawing.Size(93, 20);
			this.CxOpRadixLb.TabIndex = 3;
			this.CxOpRadixLb.Text = "変換前の基数:";
			// 
			// CxAnsRadix
			// 
			this.CxAnsRadix.Location = new System.Drawing.Point(105, 242);
			this.CxAnsRadix.MaxLength = 20;
			this.CxAnsRadix.Name = "CxAnsRadix";
			this.CxAnsRadix.Size = new System.Drawing.Size(185, 27);
			this.CxAnsRadix.TabIndex = 6;
			this.CxAnsRadix.Text = "12345678901234567890";
			this.CxAnsRadix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CxAnsRadix.TextChanged += new System.EventHandler(this.CxAnsRadix_TextChanged);
			this.CxAnsRadix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CxAnsRadix_KeyPress);
			// 
			// CxAnsOp
			// 
			this.CxAnsOp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CxAnsOp.Location = new System.Drawing.Point(375, 241);
			this.CxAnsOp.Name = "CxAnsOp";
			this.CxAnsOp.Size = new System.Drawing.Size(68, 28);
			this.CxAnsOp.TabIndex = 9;
			this.CxAnsOp.Text = "opへ";
			this.CxAnsOp.UseVisualStyleBackColor = true;
			this.CxAnsOp.Click += new System.EventHandler(this.CxAnsOp_Click);
			// 
			// CxOpRadix
			// 
			this.CxOpRadix.Location = new System.Drawing.Point(105, 209);
			this.CxOpRadix.MaxLength = 20;
			this.CxOpRadix.Name = "CxOpRadix";
			this.CxOpRadix.Size = new System.Drawing.Size(185, 27);
			this.CxOpRadix.TabIndex = 4;
			this.CxOpRadix.Text = "12345678901234567890";
			this.CxOpRadix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CxOpRadix.TextChanged += new System.EventHandler(this.CxOpRadix_TextChanged);
			this.CxOpRadix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CxOpRadix_KeyPress);
			// 
			// CxAnsCB
			// 
			this.CxAnsCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CxAnsCB.Location = new System.Drawing.Point(449, 241);
			this.CxAnsCB.Name = "CxAnsCB";
			this.CxAnsCB.Size = new System.Drawing.Size(68, 28);
			this.CxAnsCB.TabIndex = 10;
			this.CxAnsCB.Text = "CBへ";
			this.CxAnsCB.UseVisualStyleBackColor = true;
			this.CxAnsCB.Click += new System.EventHandler(this.CxAnsCB_Click);
			// 
			// CxAns
			// 
			this.CxAns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CxAns.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.CxAns.Location = new System.Drawing.Point(6, 275);
			this.CxAns.MaxLength = 0;
			this.CxAns.Multiline = true;
			this.CxAns.Name = "CxAns";
			this.CxAns.ReadOnly = true;
			this.CxAns.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.CxAns.Size = new System.Drawing.Size(511, 163);
			this.CxAns.TabIndex = 8;
			this.CxAns.Text = "準備しています...";
			this.CxAns.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CxAns_KeyPress);
			// 
			// CxOp
			// 
			this.CxOp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CxOp.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.CxOp.Location = new System.Drawing.Point(6, 40);
			this.CxOp.MaxLength = 100000;
			this.CxOp.Multiline = true;
			this.CxOp.Name = "CxOp";
			this.CxOp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.CxOp.Size = new System.Drawing.Size(511, 163);
			this.CxOp.TabIndex = 1;
			this.CxOp.Text = "準備しています...";
			this.CxOp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CxOp_KeyPress);
			// 
			// CxOpLb
			// 
			this.CxOpLb.AutoSize = true;
			this.CxOpLb.Location = new System.Drawing.Point(6, 17);
			this.CxOpLb.Name = "CxOpLb";
			this.CxOpLb.Size = new System.Drawing.Size(31, 20);
			this.CxOpLb.TabIndex = 0;
			this.CxOpLb.Text = "op:";
			// 
			// CxOpCB
			// 
			this.CxOpCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CxOpCB.Location = new System.Drawing.Point(428, 6);
			this.CxOpCB.Name = "CxOpCB";
			this.CxOpCB.Size = new System.Drawing.Size(89, 28);
			this.CxOpCB.TabIndex = 2;
			this.CxOpCB.Text = "CBから";
			this.CxOpCB.UseVisualStyleBackColor = true;
			this.CxOpCB.Click += new System.EventHandler(this.CxOpCB_Click);
			// 
			// CxExecute
			// 
			this.CxExecute.Location = new System.Drawing.Point(296, 209);
			this.CxExecute.Name = "CxExecute";
			this.CxExecute.Size = new System.Drawing.Size(73, 60);
			this.CxExecute.TabIndex = 7;
			this.CxExecute.Text = "変換";
			this.CxExecute.UseVisualStyleBackColor = true;
			this.CxExecute.Click += new System.EventHandler(this.CxExecute_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(555, 532);
			this.Controls.Add(this.MainTab);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "FatCalc";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.MainTab.ResumeLayout(false);
			this.TabFourOperation.ResumeLayout(false);
			this.TabFourOperation.PerformLayout();
			this.TabPowerRoot.ResumeLayout(false);
			this.TabPowerRoot.PerformLayout();
			this.TabChangeRadix.ResumeLayout(false);
			this.TabChangeRadix.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem アプリAToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel SouthStatus;
		private System.Windows.Forms.ToolStripStatusLabel SouthEastStatus;
		private System.Windows.Forms.TabControl MainTab;
		private System.Windows.Forms.TabPage TabFourOperation;
		private System.Windows.Forms.TabPage TabPowerRoot;
		private System.Windows.Forms.TabPage TabChangeRadix;
		private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 基数RToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 精度小数点以下x桁までBToolStripMenuItem;
		private System.Windows.Forms.Button AnsCB;
		private System.Windows.Forms.Button Op2CB;
		private System.Windows.Forms.Button Op1CB;
		private System.Windows.Forms.Button Execute;
		private System.Windows.Forms.ComboBox Operation;
		private System.Windows.Forms.Label Op2Lb;
		private System.Windows.Forms.Label Op1Lb;
		private System.Windows.Forms.Button SwapOp12;
		private System.Windows.Forms.Button AnsOp2;
		private System.Windows.Forms.Button AnsOp1;
		private System.Windows.Forms.TextBox Ans;
		private System.Windows.Forms.TextBox Op2;
		private System.Windows.Forms.TextBox Op1;
		private System.Windows.Forms.ToolStripMenuItem 角括弧表記する数字MToolStripMenuItem;
		private System.Windows.Forms.TextBox PrExponent;
		private System.Windows.Forms.Button PrAnsCB;
		private System.Windows.Forms.TextBox PrAns;
		private System.Windows.Forms.TextBox PrOp;
		private System.Windows.Forms.Label PrOpLb;
		private System.Windows.Forms.Button PrOpCB;
		private System.Windows.Forms.Button PrExecute;
		private System.Windows.Forms.ComboBox PrOperation;
		private System.Windows.Forms.Button PrAnsOp;
		private System.Windows.Forms.TextBox CxAnsRadix;
		private System.Windows.Forms.Button CxAnsOp;
		private System.Windows.Forms.TextBox CxOpRadix;
		private System.Windows.Forms.Button CxAnsCB;
		private System.Windows.Forms.TextBox CxAns;
		private System.Windows.Forms.TextBox CxOp;
		private System.Windows.Forms.Label CxOpLb;
		private System.Windows.Forms.Button CxOpCB;
		private System.Windows.Forms.Button CxExecute;
		private System.Windows.Forms.Label CxOpRadixLb;
		private System.Windows.Forms.Label CxAnsRadixLb;
		private System.Windows.Forms.ToolStripMenuItem 表示VToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem リセットRToolStripMenuItem;
	}
}

