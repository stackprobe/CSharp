using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class ManyHandleClass0002 : IDisposable
	{
		private RandomizedErrorHandle Handle01;
		private RandomizedErrorHandle Handle02;
		private RandomizedErrorHandle Handle03;
		private RandomizedErrorHandle Handle04;
		private RandomizedErrorHandle Handle05;

		public ManyHandleClass0002()
		{
			HandleDam.Transaction(hDam =>
			{
				this.Handle01 = hDam.Add(new RandomizedErrorHandle());
				this.Handle02 = hDam.Add(new RandomizedErrorHandle());
				this.Handle03 = hDam.Add(new RandomizedErrorHandle());
				this.Handle04 = hDam.Add(new RandomizedErrorHandle());
				this.Handle05 = hDam.Add(new RandomizedErrorHandle());
			});
		}

		private LimitCounter DisposeOnce = LimitCounter.One();

		public void Dispose()
		{
			if (this.DisposeOnce.Issue())
			{
				ExceptionDam.Section(eDam =>
				{
					eDam.Dispose(ref this.Handle01);
					eDam.Dispose(ref this.Handle02);
					eDam.Dispose(ref this.Handle03);
					eDam.Dispose(ref this.Handle04);
					eDam.Dispose(ref this.Handle05);
				});
			}
		}
	}
}
