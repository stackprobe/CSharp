using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			new Question(new string[]
			{
				// $_git:secretBegin

				/    //// //
				////// // //
				//   // / //
				// //// / //
				////////////
				/ / //// ///
				/ / //   ///
				/ /  ///////
				/ ////    //

				// $_git:secretEnd
			})
			.Solve();
		}

		public void Test02()
		{
			new Question(new string[]
			{
				// $_git:secretBegin

				/   //// //
				///// // //
				//   / / //
				// ////////
				//// // ///
				/ /  /  ///
				/ /  //////
				/ ////   //

				// $_git:secretEnd
			})
			.Solve();
		}
	}
}
