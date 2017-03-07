using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Threading;
using Charlotte.CalcTools;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			Gnd.MainWin = this;

			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;

			this.Operation.SelectedIndex = 0;
			this.Op1.MaxLength = Gnd.OperandLenMax;
			this.Op2.MaxLength = Gnd.OperandLenMax;
			this.Ans.ForeColor = Consts.AnswerForeColor;
			this.Ans.BackColor = Consts.AnswerBackColor;

			this.PrOp.MaxLength = Gnd.OperandLenMax;
			this.PrOperation.SelectedIndex = 0;
			this.PrExponent.Text = "2";
			this.PrAns.ForeColor = Consts.AnswerForeColor;
			this.PrAns.BackColor = Consts.AnswerBackColor;

			this.CxOp.MaxLength = Gnd.OperandLenMax;
			this.CxOpRadix.Text = "" + Gnd.Radix;
			this.CxAnsRadix.Text = "" + Gnd.Radix;
			this.CxAns.ForeColor = Consts.AnswerForeColor;
			this.CxAns.BackColor = Consts.AnswerBackColor;
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			WordWrapOffTools.WordWrapOff(this.Op1);
			WordWrapOffTools.WordWrapOff(this.Op2);
			WordWrapOffTools.WordWrapOff(this.Ans);

			WordWrapOffTools.WordWrapOff(this.PrOp);
			WordWrapOffTools.WordWrapOff(this.PrAns);

			WordWrapOffTools.WordWrapOff(this.CxOp);
			WordWrapOffTools.WordWrapOff(this.CxAns);

			{
				Control refCtrl = this.TabFourOperation;
				double f13 = 1.0 / 3.0;
				double f23 = 2.0 / 3.0;

				new ControlRectMan(refCtrl, 0.0, 0.0, 0.0, f13, this.Op1);
				new ControlRectMan(refCtrl, 0.0, f13, 0.0, 0.0, this.Op2Lb);
				new ControlRectMan(refCtrl, 0.0, f13, 0.0, 0.0, this.SwapOp12);
				new ControlRectMan(refCtrl, 0.0, f13, 0.0, 0.0, this.Op2CB);
				new ControlRectMan(refCtrl, 0.0, f13, 0.0, f13, this.Op2);
				new ControlRectMan(refCtrl, 0.0, f23, 0.0, 0.0, this.Operation);
				new ControlRectMan(refCtrl, 0.0, f23, 0.0, 0.0, this.Execute);
				new ControlRectMan(refCtrl, 0.0, f23, 0.0, 0.0, this.AnsOp1);
				new ControlRectMan(refCtrl, 0.0, f23, 0.0, 0.0, this.AnsOp2);
				new ControlRectMan(refCtrl, 0.0, f23, 0.0, 0.0, this.AnsCB);
				new ControlRectMan(refCtrl, 0.0, f23, 0.0, f13, this.Ans);
			}

			{
				Control refCtrl = this.TabPowerRoot;

				new ControlRectMan(refCtrl, 0.0, 0.0, 0.0, 0.5, this.PrOp);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.PrOperation);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.PrExponent);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.PrExecute);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.PrAnsOp);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.PrAnsCB);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.5, this.PrAns);
			}

			{
				Control refCtrl = this.TabChangeRadix;

				new ControlRectMan(refCtrl, 0.0, 0.0, 0.0, 0.5, this.CxOp);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxOpRadixLb);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxOpRadix);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxAnsRadixLb);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxAnsRadix);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxExecute);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxAnsOp);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.0, this.CxAnsCB);
				new ControlRectMan(refCtrl, 0.0, 0.5, 0.0, 0.5, this.CxAns);
			}

			if (Gnd.MainWin_L != -1)
			{
				this.Left = Gnd.MainWin_L;
				this.Top = Gnd.MainWin_T;
				this.Width = Gnd.MainWin_W;
				this.Height = Gnd.MainWin_H;
				this.MainTab.SelectedIndex = 1; // op1にフォーカスを..
				this.MainTab.SelectedIndex = Gnd.MainTab_SelectedIndex;
			}

			this.RefreshUi();
		}

		private void RefreshUi()
		{
			{
				string str = Gnd.ErrorMessage;

				str = str.Replace("\r", "");
				str = str.Replace("\n", "");

				Gnd.ErrorMessage = str;
			}

			if (Gnd.ErrorMessage == "")
			{
				this.SouthStatus.ForeColor = Consts.DefForeColor;
				this.SouthStatus.BackColor = Consts.DefBackColor;
			}
			else
			{
				this.SouthStatus.ForeColor = Consts.ErrorForeColor;
				this.SouthStatus.BackColor = Color.Yellow;//Consts.ErrorBackColor;
			}
			this.SouthStatus.Text = Gnd.ErrorMessage;

			{
				string message;

				if (this.MainTab.SelectedIndex == 2)
					message = "小数点以下 " + Gnd.Basement + " 桁まで, [" + Gnd.BracketMin + "]";
				else
					message = Gnd.Radix + "進, 小数点以下 " + Gnd.Basement + " 桁まで, [" + Gnd.BracketMin + "]";

				this.SouthEastStatus.Text = message;
			}

			//this.PrExponent.Text = "" + IntTools.ToInt(this.PrExponent.Text, int.MinValue, int.MaxValue, 0);
			//this.CxOpRadix.Text = "" + IntTools.ToInt(this.CxOpRadix.Text, int.MinValue, int.MaxValue, 0);
			//this.CxAnsRadix.Text = "" + IntTools.ToInt(this.CxAnsRadix.Text, int.MinValue, int.MaxValue, 0);
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			Gnd.SaveDataFile();
			Gnd.MainWin = null;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void 基数RToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (InputDlg dlg = new InputDlg())
			{
				dlg.PrmPrompt = "基数を入力して下さい。\n(値域：2～" + UInt64.MaxValue + ")";
				dlg.MinVal = 2;
				dlg.MaxVal = UInt64.MaxValue;
				dlg.InitVal = Gnd.Radix;

				dlg.ShowDialog();

				if (dlg.OkPressed)
					Gnd.Radix = dlg.RetValue;
			}
			this.RefreshUi();
		}

		private void 精度小数点以下x桁までBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (InputDlg dlg = new InputDlg())
			{
				dlg.PrmPrompt = "精度を入力して下さい。\n割り算・べき根・基数変換時、小数点以下［入力値］桁まで求めます。\n(値域：0～" + IntTools.IMAX + ")";
				dlg.MinVal = 0;
				dlg.MaxVal = UInt64Tools.IMAX;
				dlg.InitVal = (UInt64)Gnd.Basement;

				dlg.ShowDialog();

				if (dlg.OkPressed)
					Gnd.Basement = (int)dlg.RetValue;
			}
			this.RefreshUi();
		}

		private void 角括弧表記する数字MToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (InputDlg dlg = new InputDlg())
			{
				dlg.PrmPrompt = "角括弧表記する最小の数字 (の大きさ) を入力して下さい。\n(値域：0～36)";
				dlg.MinVal = 0;
				dlg.MaxVal = 36;
				dlg.InitVal = (UInt64)Gnd.BracketMin;

				dlg.ShowDialog();

				if (dlg.OkPressed)
					Gnd.BracketMin = (int)dlg.RetValue;
			}
			this.RefreshUi();
		}

		private void PrExponent_TextChanged(object sender, EventArgs e)
		{
			Common.UIntChanged(this.PrExponent, this.PrOperation.SelectedIndex == Consts.PR_OPERATION_POWER ? 0ul : 1ul, IntTools.IMAX);
		}

		private void CxOpRadix_TextChanged(object sender, EventArgs e)
		{
			Common.UIntChanged(this.CxOpRadix, 2, UInt64.MaxValue);
		}

		private void CxAnsRadix_TextChanged(object sender, EventArgs e)
		{
			Common.UIntChanged(this.CxAnsRadix, 2, UInt64.MaxValue);
		}

		private void Op1CB_Click(object sender, EventArgs e)
		{
			string text = Common.GetClipboard();

			if (text != null)
				Common.SetOperand(this.Op1, text);
		}

		private void Op2CB_Click(object sender, EventArgs e)
		{
			string text = Common.GetClipboard();

			if (text != null)
				Common.SetOperand(this.Op2, text);
		}

		private void SwapOp12_Click(object sender, EventArgs e)
		{
			string tmp = this.Op1.Text;
			this.Op1.Text = this.Op2.Text;
			this.Op2.Text = tmp;
		}

		private void AnsCB_Click(object sender, EventArgs e)
		{
			Common.SetClipboard(this.Ans.Text);
		}

		private void AnsOp1_Click(object sender, EventArgs e)
		{
			Common.SetOperand(this.Op1, this.Ans.Text);
		}

		private void AnsOp2_Click(object sender, EventArgs e)
		{
			Common.SetOperand(this.Op2, this.Ans.Text);
		}

		private void PrOpCB_Click(object sender, EventArgs e)
		{
			string text = Common.GetClipboard();

			if (text != null)
				Common.SetOperand(this.PrOp, text);
		}

		private void PrAnsCB_Click(object sender, EventArgs e)
		{
			Common.SetClipboard(this.PrAns.Text);
		}

		private void PrAnsOp_Click(object sender, EventArgs e)
		{
			Common.SetOperand(this.PrOp, this.PrAns.Text);
		}

		private void CxOpCB_Click(object sender, EventArgs e)
		{
			string text = Common.GetClipboard();

			if (text != null)
				Common.SetOperand(this.CxOp, text);
		}

		private void CxAnsCB_Click(object sender, EventArgs e)
		{
			Common.SetClipboard(this.CxAns.Text);
		}

		private void CxAnsOp_Click(object sender, EventArgs e)
		{
			Common.SetOperand(this.CxOp, this.CxAns.Text);
		}

		private void リセットRToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Op1.Text = "";
			this.Op1.ForeColor = Consts.DefForeColor;
			this.Op1.BackColor = Consts.DefBackColor;
			this.Op2.Text = "";
			this.Op2.ForeColor = Consts.DefForeColor;
			this.Op2.BackColor = Consts.DefBackColor;
			this.Ans.Text = "";

			this.PrOp.Text = "";
			this.PrOp.ForeColor = Consts.DefForeColor;
			this.PrOp.BackColor = Consts.DefBackColor;
			this.PrExponent.Text = "2";
			this.PrAns.Text = "";

			this.CxOp.Text = "";
			this.CxOp.ForeColor = Consts.DefForeColor;
			this.CxOp.BackColor = Consts.DefBackColor;
			this.CxOpRadix.Text = "" + Gnd.Radix;
			this.CxAnsRadix.Text = "" + Gnd.Radix;
			this.CxAns.Text = "";

			Gnd.ErrorMessage = "";

			this.RefreshUi();
		}

		private void PrOperation_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.PrExponent_TextChanged(null, null);
		}

		private void Op1_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.Op1, this.Op2);
		}

		private void Op2_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.Op2, this.Operation);
		}

		private void Operation_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.Operation, this.Execute);
		}

		private void Ans_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.Ans, this.Op1);
		}

		private void PrOp_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.PrOp, this.PrOperation);
		}

		private void PrOperation_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.PrOperation, this.PrExponent);
		}

		private void PrExponent_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.PrExponent, this.PrExecute);
		}

		private void PrAns_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.PrAns, this.PrOp);
		}

		private void CxOp_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.CxOp, this.CxOpRadix);
		}

		private void CxOpRadix_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.CxOpRadix, this.CxAnsRadix);
		}

		private void CxAnsRadix_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.CxAnsRadix, this.CxExecute);
		}

		private void CxAns_KeyPress(object sender, KeyPressEventArgs e)
		{
			Common.KeyPressed(e, this.CxAns, this.CxOp);
		}

		private void Execute_Click(object sender, EventArgs e)
		{
			Gnd.SaveDataFile();

			string l = this.Op1.Text;
			string r = this.Op2.Text;
			int operation = this.Operation.SelectedIndex;
			string ans = "";
			int ngOp = -1;

			Gnd.ErrorMessage = "";

			Common.Busy(delegate
			{
				try
				{
					Calc calc = new Calc(Gnd.Radix, Gnd.Basement, Gnd.BracketMin);

					ngOp = 0;
					l = calc.Normalize(l);
					ngOp = 1;
					r = calc.Normalize(r);
					ngOp = -1;

					switch (operation)
					{
						case 0: // add
							ans = calc.Execute(l, '+', r);
							break;

						case 1: // red
							ans = calc.Execute(l, '-', r);
							break;

						case 2: // mul
							ans = calc.Execute(l, '*', r);
							break;

						case 3: // div
							ans = calc.Execute(l, '/', r);
							break;

						case 4: // mod
							ans = calc.Execute(l, '%', r);
							break;

						case 5: // mod_int
							calc.Basement = 0;
							ans = calc.Execute(l, '%', r);
							break;

						default:
							throw new Exception("Unknown operation: " + operation);
					}
				}
				catch (Exception ex)
				{
					Logger.WriteLine(ex);
					Gnd.ErrorMessage = ex.Message;
				}
			});

			Common.SetOperand(this.Op1, l);
			Common.SetOperandStatus(this.Op1, ngOp != 0);
			Common.SetOperand(this.Op2, r);
			Common.SetOperandStatus(this.Op2, ngOp != 1);
			Common.SetAnswer(this.Ans, ans);

			this.RefreshUi();
			this.Ans.Focus();
		}

		private void PrExecute_Click(object sender, EventArgs e)
		{
			Gnd.SaveDataFile();

			string l = this.PrOp.Text;
			int operation = this.PrOperation.SelectedIndex;
			string exponent = this.PrExponent.Text;
			string ans = "";
			int ngOp = -1;

			Gnd.ErrorMessage = "";

			Common.Busy(delegate
			{
				try
				{
					Calc calc = new Calc(Gnd.Radix, Gnd.Basement, Gnd.BracketMin);

					ngOp = 0;
					l = calc.Normalize(l);
					ngOp = -1;

					switch (operation)
					{
						case 0: // power
							ans = calc.Power(l, int.Parse(exponent));
							break;

						case 1: // root
							ans = calc.Root(l, int.Parse(exponent));
							break;

						default:
							throw new Exception("Unknown operation: " + operation);
					}
				}
				catch (Exception ex)
				{
					Logger.WriteLine(ex);
					Gnd.ErrorMessage = ex.Message;
				}
			});

			Common.SetOperand(this.PrOp, l);
			Common.SetOperandStatus(this.PrOp, ngOp != 0);
			Common.SetAnswer(this.PrAns, ans);

			this.RefreshUi();
			this.PrAns.Focus();
		}

		private void CxExecute_Click(object sender, EventArgs e)
		{
			Gnd.SaveDataFile();

			string l = this.CxOp.Text;
			string lRadix = this.CxOpRadix.Text;
			string ansRadix = this.CxAnsRadix.Text;
			string ans = "";
			int ngOp = -1;

			Gnd.ErrorMessage = "";

			Common.Busy(delegate
			{
				try
				{
					Calc calc = new Calc(UInt64.Parse(lRadix), Gnd.Basement, Gnd.BracketMin);

					ngOp = 0;
					l = calc.Normalize(l);
					ngOp = -1;

					ans = calc.ChangeRadix(l, UInt64.Parse(ansRadix));
				}
				catch (Exception ex)
				{
					Logger.WriteLine(ex);
					Gnd.ErrorMessage = ex.Message;
				}
			});

			Common.SetOperand(this.CxOp, l);
			Common.SetOperandStatus(this.CxOp, ngOp != 0);
			Common.SetAnswer(this.CxAns, ans);

			this.RefreshUi();
			this.CxAns.Focus();
		}

		private void MainTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		public void SaveStatus()
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Gnd.MainWin_L = this.Left;
				Gnd.MainWin_T = this.Top;
				Gnd.MainWin_W = this.Width;
				Gnd.MainWin_H = this.Height;
			}
			Gnd.MainTab_SelectedIndex = this.MainTab.SelectedIndex;
		}
	}
}
