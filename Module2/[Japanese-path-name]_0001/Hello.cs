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
		return ReadConsTokens().Select(v => int.Parse(v)).ToArray();
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
