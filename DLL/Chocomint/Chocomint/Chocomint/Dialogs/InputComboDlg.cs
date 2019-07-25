using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class InputComboDlg : Form
	{
		public bool OkPressed = false;
		public object Value = null;
		public Action PostShown = () => { };
		public Func<object, object> Validator = ret => ret;
		public Func<object, object, bool> MatchValue = object.Equals;

		// <---- prm

		public InputComboDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		public class Item
		{
			public object Value;
			public string ValueForUI;

			public Item(object value, string valueForUI)
			{
				this.Value = value;
				this.ValueForUI = valueForUI;
			}
		}

		private List<Item> Items = new List<Item>();

		public void AddItem(Item item)
		{
			this.Items.Add(item);
		}

		public void AddItems(IEnumerable<Item> items)
		{
			foreach (Item item in items)
				this.Items.Add(item);
		}

		public void AddItem(object value, string valueForUI)
		{
			this.AddItem(new Item(value, valueForUI));
		}

		private void InputComboDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputComboDlg_Shown(object sender, EventArgs e)
		{
			if (this.Items.Count == 0)
				this.Items.Add(new Item(null, ""));

			this.ComboValue.Items.Clear();

			foreach (Item item in this.Items)
				this.ComboValue.Items.Add(item.ValueForUI);

			{
				int index = this.IndexOf(this.Value);

				if (index == -1)
					index = 0;

				this.ComboValue.SelectedIndex = index;
			}

			this.PostShown();
			ChocomintCommon.DlgCommonPostShown(this);
		}

		private int IndexOf(object value)
		{
			for (int index = 0; index < this.Items.Count; index++)
				if (this.MatchValue(this.Items[index].Value, value))
					return index;

			return -1;
		}

		private void InputComboDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputComboDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void ComboValue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.BtnOk.Focus();
				e.Handled = true;
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				object ret = this.Items[this.ComboValue.SelectedIndex].Value;

				ret = this.Validator(ret);

				this.OkPressed = true;
				this.Value = ret;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("入力エラー", ex, this);

				this.ComboValue.Focus();
			}
		}
	}
}
