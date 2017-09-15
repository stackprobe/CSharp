using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class FailedOperationTest
	{
		public void test01()
		{
			try
			{
				throw new Exception("Exception_01", new Exception("InnerException_01", new FailedOperation()));
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}
		}

		public void test02()
		{
			try
			{
				throw new Completed("処理「あああああ」が完了しました。");
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}

			try
			{
				throw new Ended();
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}

			try
			{
				throw new Cancelled("処理「あああああ」はキャンセルされました。");
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}

			try
			{
				throw new FailedOperation("処理「あああああ」に失敗しました。");
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}

			try
			{
				throw new Exception("処理「あああああ」はエラー終了しました。");
			}
			catch (Exception e)
			{
				FailedOperation.caught(e);
			}
		}
	}
}
