using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class BgService : IDisposable
	{
		private SockClient _sockClient = new SockClient();

		public void Perform()
		{
			// TODO
		}

		/// <summary>
		/// このオブジェクトを破棄(Dispose 呼び出し)可能か？
		/// 状態は Perform の実行によって変化する。
		/// </summary>
		/// <returns>可能である</returns>
		public bool IsDisposable()
		{
			return _sockClient.IsFinished();
		}

		public void Dispose()
		{
			// noop
		}
	}
}
