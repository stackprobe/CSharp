using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class NRecver
	{
		// ---- ここから

		public delegate void NRecved_d(string message);

		public bool NRecvEnd;

		public void NRecv(string ident, NRecved_d recved)
		{
			// TODO
		}

		// ---- ここまで
	}
}
