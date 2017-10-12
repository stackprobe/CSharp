using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class BgService
	{
		public void Perform()
		{
			// TODO
		}

		/// <summary>
		/// このオブジェクトを破棄(Destroy 呼び出し)可能か？
		/// 状態は Perform の実行によって変化する。
		/// </summary>
		/// <returns>可能である</returns>
		public bool IsEndable()
		{
			return true; // TODO
		}

		public void Destroy()
		{
			// noop
		}
	}
}
