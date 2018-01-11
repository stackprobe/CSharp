using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class WorkingDir : IDisposable
	{
		public static WorkingDir root = new WorkingDir(Path.Combine(FileTools.getTMP(), Program.APP_IDENT)); // zantei

		/// <summary>
		/// ★空白を含まないはず。
		/// </summary>
		private string _dir;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dir">★空白を含まないこと。</param>
		public WorkingDir(string dir)
		{
			_dir = dir;

			FileTools.rm(_dir);
			Directory.CreateDirectory(_dir);
		}

		public WorkingDir create()
		{
			return new WorkingDir(this.makePath());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>★空白を含まない。</returns>
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
				FileTools.rm(_dir);
				_dir = null;
			}
		}
	}
}
