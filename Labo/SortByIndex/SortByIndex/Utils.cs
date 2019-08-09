using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Utils
	{
		public static void Transfer<T>(T[] buff, long startPosition, long endPosition, Action<long, int> routine)
		{
			for (long position = startPosition; position < endPosition; )
			{
				int readWriteSize = (int)Math.Min((long)buff.Length, endPosition - position);

				routine(position, readWriteSize);

				position += readWriteSize;
			}
		}
	}
}
