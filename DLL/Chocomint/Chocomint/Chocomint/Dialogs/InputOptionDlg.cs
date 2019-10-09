using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Chocomint.Dialogs.Resource;
using System.Media;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class InputOptionDlg : Form
	{
		public enum Mode_e
		{
			Default = 1,
			Warning,
		}

		public Mode_e Mode = Mode_e.Default;
		public string[] Options = new string[] { "OK", "Cancel" };
		public int SelectedIndex = -1;
		public string Message = "メッセージを準備しています。";
		public Action PostShown = () => { };

		// <---- prm

		private Rectangle BaseButtonsArea;
		private Size BaseMainWinSize;
		private List<Button> Buttons = new List<Button>();

		public InputOptionDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputOptionDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputOptionDlg_Shown(object sender, EventArgs e)
		{
			switch (this.Mode)
			{
				case Mode_e.Default:
					this.MessageIcon.Image = new Resource0001().QuestionIcon.Image;
					break;

				case Mode_e.Warning:
					this.MessageIcon.Image = new Resource0001().WarningIcon.Image;
					SystemSounds.Exclamation.Play();
					break;

				default:
					throw null; // never
			}
			this.TextMessage.Text = this.Message;
			this.TextMessage.Top += (this.MessageIcon.Height - this.TextMessage.Height) / 2;

			if (this.Options.Length == 0)
				this.Options = new string[] { "OK" };

			this.BaseButtonsArea = this.FirstButton.Bounds;
			this.BaseMainWinSize = this.Size;

			for (int index = 0; index < this.Options.Length; index++)
				this.Buttons.Add(this.CreateButton(this.Options[index], index));

			for (int index = 1; index < this.Buttons.Count; index++)
				this.Controls.Add(this.Buttons[index]);

			this.InputOptionDlg_Resize(null, null);

			this.PostShown();
			ChocomintDialogsCommon.DlgCommonPostShown(this);
		}

		private Button CreateButton(string text, int index)
		{
			Button button = index == 0 ? this.FirstButton : new Button();

			button.Text = text;
			button.Click += (sender, e) =>
			{
				this.SelectedIndex = index;
				this.Close();
			};

			button.Visible = true;

			return button;
		}

		private void InputOptionDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputOptionDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void FirstButton_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void InputOptionDlg_Resize(object sender, EventArgs e)
		{
			Rectangle buttonsArea = new Rectangle(
				this.BaseButtonsArea.Left,
				this.BaseButtonsArea.Top + this.Height - this.BaseMainWinSize.Height,
				this.BaseButtonsArea.Width + this.Width - this.BaseMainWinSize.Width,
				this.BaseButtonsArea.Height
				);

			const int MARGIN = 10;
			int w = Math.Max(100, (buttonsArea.Width + MARGIN) / this.Options.Length - MARGIN);
			int l = buttonsArea.Left;

			for (int index = 0; index < this.Buttons.Count; index++)
			{
				this.Buttons[index].Left = l;
				this.Buttons[index].Top = buttonsArea.Top;
				this.Buttons[index].Width = w;
				this.Buttons[index].Height = buttonsArea.Height;

				l += w + MARGIN;
			}
		}
	}
}
