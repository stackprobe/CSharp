using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public static class InputComboDlgTools
	{
		public class Item<T>
		{
			public T Value;
			public string ValueForUI;

			public Item(T value, string valueForUI)
			{
				this.Value = value;
				this.ValueForUI = valueForUI;
			}
		}

		public static T Show<T>(string title, string prompt, IEnumerable<Item<T>> items, bool hasParent = false, T value = default(T), T defval = default(T), Func<T, T> validator = null)
		{
			using (InputComboDlg f = new InputComboDlg())
			{
				f.Value = value;

				if (items != null)
					f.AddItems(items.Select(item => new InputComboDlg.Item(item.Value, item.ValueForUI)));

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
					f.Prompt.Text = prompt;

					if (validator != null)
						f.Validator = v => validator((T)v);
				};

				f.ShowDialog();

				if (f.OkPressed)
					return (T)f.Value;

				return defval;
			}
		}
	}
}
