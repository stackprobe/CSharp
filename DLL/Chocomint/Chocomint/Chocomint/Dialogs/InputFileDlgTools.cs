using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public static class InputFileDlgTools
	{
		/// <summary>
		/// 読み込み用ファイルの選択ダイアログ
		/// </summary>
		/// <param name="title">タイトル文字列</param>
		/// <param name="prompt">プロンプト文字列</param>
		/// <param name="parent">親フォームを持つ</param>
		/// <param name="file">初期ファイル</param>
		/// <param name="defval">デフォルトの戻り値</param>
		/// <param name="filterString">書式 = [総称:]拡張子1.拡張子2.拡張子3 ...</param>
		/// <returns>存在するファイル名</returns>
		public static string Load(string title, string prompt, bool hasParent = false, string file = "", string defval = null, string filterString = null)
		{
			return Existing(InputFileDlg.Mode_e.LOAD, title, prompt, hasParent, file, defval, filterString);
		}

		/// <summary>
		/// 書き出し用ファイルの選択・入力ダイアログ
		/// </summary>
		/// <param name="title">タイトル文字列</param>
		/// <param name="prompt">プロンプト文字列</param>
		/// <param name="parent">親フォームを持つ</param>
		/// <param name="file">初期ファイル</param>
		/// <param name="defval">デフォルトの戻り値</param>
		/// <param name="filterString">書式 = 拡張子1.拡張子2.拡張子3 ...</param>
		/// <returns>存在しないかもしれないファイル名</returns>
		public static string Save(string title, string prompt, bool hasParent = false, string file = "", string defval = null, string filterString = null)
		{
			return Show(InputFileDlg.Mode_e.SAVE, title, prompt, hasParent, file, defval, filterString, null);
		}

		public static string Existing(InputFileDlg.Mode_e mode, string title, string prompt, bool hasParent = false, string file = "", string defval = null, string filterString = null)
		{
			Func<string, string> validator = v =>
			{
				if (File.Exists(v) == false)
					throw new Exception("指定されたファイルは存在しません。");

				return v;
			};

			return Show(mode, title, prompt, hasParent, file, defval, filterString, validator);
		}

		public static string Show(InputFileDlg.Mode_e mode, string title, string prompt, bool hasParent = false, string file = "", string defval = null, string filterString = null, Func<string, string> validator = null)
		{
			using (InputFileDlg f = new InputFileDlg())
			{
				f.Mode = mode;
				f.Value = file;

				if (filterString != null)
					f.FilterString = filterString;

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
					f.Prompt.Text = prompt;

					if (validator != null)
						f.Validator = validator;
				};

				f.ShowDialog();

				if (f.OkPressed)
					return f.Value;

				return defval;
			}
		}
	}
}
