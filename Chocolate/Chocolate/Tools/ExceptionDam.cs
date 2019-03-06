﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ExceptionDam
	{
		public static void Section(Action<ExceptionDam> routine)
		{
			ExceptionDam eDam = new ExceptionDam();
			try
			{
				routine(eDam);
			}
			catch (Exception e)
			{
				eDam.Add(e);
			}
			eDam.Burst();
		}

		private List<Exception> Errors = new List<Exception>();

		public void Add(Exception e)
		{
			this.Errors.Add(e);
		}

		public void Invoke(Action routine)
		{
			try
			{
				routine();
			}
			catch (Exception e)
			{
				this.Add(e);
			}
		}

		public void Burst()
		{
			if (1 <= this.Errors.Count)
			{
				Exception[] errors = this.Errors.ToArray();

				this.Errors.Clear();

				throw new AggregateException("Has some errors.", errors);
			}
		}
	}
}
