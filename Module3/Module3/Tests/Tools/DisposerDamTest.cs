using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class DisposerDamTest
	{
		public void Test01()
		{
			using (DisposerDam dd = new DisposerDam())
			{
				dd.Using(new FileStream(@"C:\temp\1.tmp", FileMode.Create, FileAccess.Write));
				dd.Using(new FileStream(@"C:\temp\2.tmp", FileMode.Create, FileAccess.Write));
				dd.Using(new FileStream(@"C:\temp\3.tmp", FileMode.Create, FileAccess.Write));
			}

			using (DisposerDam dd = new DisposerDam())
			{
				dd.Using(new TestDisposable("TD1"));
				dd.Using(new TestDisposable("TD2"));
				dd.Using(new TestDisposable("TD3"));
			}

			try
			{
				using (DisposerDam dd = new DisposerDam())
				{
					dd.Using(new TestDisposable("TD1"));
					dd.Using(new TestDisposable("TD2"));
					dd.Using(new TestDisposable("TD3"));

					throw new Exception("Throw_0003");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("e " + e);
			}

			try
			{
				using (DisposerDam dd = new DisposerDam())
				{
					dd.Using(new TestDisposable("TD1"));
					dd.Using(new TestDisposable("TD2"));
					dd.Using(new TestDisposable("TD3_Throw"));
					dd.Using(new TestDisposable("TD4"));
					dd.Using(new TestDisposable("TD5"));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("e " + e);
			}

			// ----

			Console.WriteLine("OK!");
		}

		private class TestDisposable : IDisposable
		{
			private string Title;

			public TestDisposable(string title)
			{
				this.Title = title;
				Console.WriteLine("C " + this.Title);
			}

			public void Dispose()
			{
				Console.WriteLine("D " + this.Title);

				if (this.Title.Contains("Throw"))
				{
					Console.WriteLine("T " + this.Title);

					throw new Exception("Throw_" + this.Title);
				}
			}
		}
	}
}
