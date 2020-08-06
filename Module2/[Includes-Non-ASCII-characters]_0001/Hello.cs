using System;
using System.Collections.Generic;
using System.Linq;

class Hello
{
	static void Main()
	{
		try
		{
			new Hello().Perform();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	private void Perform()
	{
		// TODO
	}

	private int[] ReadConsInts()
	{
		return ReadConsVals(v => int.Parse(v));
	}

	private T[] ReadConsVals<T>(Func<string, T> conv)
	{
		return ReadConsTokens().Select(conv).ToArray();
	}

	private string[] ReadConsTokens()
	{
		return ReadConsLine().Split(' ');
	}

	private string ReadConsLine()
	{
		return Console.ReadLine().Trim();
	}
}
