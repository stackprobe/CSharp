using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class BgService
	{
		private SockClient _sockClient = new SockClient();

		public void Perform()
		{
			// TODO
		}

		/// <summary>
		/// 今プロセスを終了しても良いか。
		/// 状態は Perform の実行によって変化する。
		/// </summary>
		/// <returns>良い</returns>
		public bool IsEndable()
		{
			return _sockClient.IsFinished();
		}
	}
}
