using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class WorkingDir : IDisposable
	{
		public WorkingDir()
			: this(StringTools.getUUID())
		{ }

		/// <summary>
		/// ★空白を含まないはず。
		/// </summary>
		private string _dir;

		public WorkingDir(string lDir)
		{
			_dir = Path.Combine(FileTools.getTMP(), lDir);

			FileTools.rm(_dir);
			Directory.CreateDirectory(_dir);
		}

		/// <summary>
		/// ★空白を含まないパスを返す。
		/// </summary>
		/// <returns></returns>
		public string getDir()
		{
			return _dir;
		}

		/// <summary>
		/// ★空白を含まないパスを返す。
		/// </summary>
		/// <returns></returns>
		public string makePath()
		{
			return getPath(StringTools.getUUID());
		}

		public string getPath(string relPath)
		{
			return Path.Combine(_dir, relPath);
		}

		public void Dispose()
		{
			if (_dir != null)
			{
				string dir = _dir;
				_dir = null;
				FileTools.rm(dir);
			}
		}
	}
}
