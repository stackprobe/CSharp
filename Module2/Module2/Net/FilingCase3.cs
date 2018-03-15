using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Net
{
	public class FilingCase3 : IDisposable
	{
		private string Domain;
		private int PortNo;
		private string BasePath;

		private FilingCase3Client Client;

		public FilingCase3(string domain = "localhost", int portNo = 65123, string basePath = "Common")
		{
			this.Domain = domain;
			this.PortNo = portNo;
			this.BasePath = basePath;

			this.Client = new FilingCase3Client(domain, portNo, basePath);
		}

		public byte[] Get(string path)
		{
			return this.Twice(() => this.Client.Get(path));
		}

		public int Post(string path, byte[] data)
		{
			return this.Twice(() => this.Client.Post(path, data));
		}

		public byte[] GetPost(string path, byte[] data)
		{
			return this.Twice(() => this.Client.GetPost(path, data));
		}

		public string[] List(string path)
		{
			return this.Twice(() => this.Client.List(path));
		}

		public int Delete(string path)
		{
			return this.Twice(() => this.Client.Delete(path));
		}

		private T Twice<T>(Func<T> once)
		{
			try
			{
				return once();
			}
			catch
			{
				this.Client.Dispose();
				this.Client = new FilingCase3Client(this.Domain, this.PortNo, this.BasePath);

				return once();
			}
		}

		public void Dispose()
		{
			if (this.Client != null)
			{
				this.Client.Dispose();
				this.Client = null;
			}
		}
	}
}
