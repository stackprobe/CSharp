using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public static class WaitDlgTools
	{
		private static VisitorCounter WaitDlgVCnt = new VisitorCounter();

		/// <summary>
		/// <para>長時間用のビジーダイアログ</para>
		/// <para>キャンセル可能</para>
		/// <para>実行中のプログレスバーの位置・メッセージの変更可能</para>
		/// </summary>
		/// <param name="title">タイトル</param>
		/// <param name="message">初期メッセージ</param>
		/// <param name="routine">主処理</param>
		/// <param name="interlude">実行中100ミリ秒毎に呼ばれる。進捗(0.0～1.0)を返す。</param>
		/// <param name="interlude_cancelled">キャンセル後100ミリ秒毎に呼ばれる。</param>
		/// <param name="hasParent">? 親ウィンドウ有り</param>
		public static void Show(string title, string message, Action routine, Func<double> interlude, Action interlude_cancelled, bool hasParent = false)
		{
			if (WaitDlgVCnt.HasVisitor())
			{
				routine();
				return;
			}

			using (WaitDlgVCnt.Section())
			using (WaitDlg f = new WaitDlg())
			using (ThreadEx th = new ThreadEx(routine))
			{
				f.Th = th;
				f.Interlude = interlude;
				f.Interlude_Cancelled = interlude_cancelled;

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
					f.Message.Text = message;
				};

				f.ShowDialog();

				th.RelayThrow();
			}
		}
	}
}
