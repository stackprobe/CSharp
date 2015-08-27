using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Flowertact.Tools;
using Charlotte.Satellite.Tools;
using System.Threading;

namespace Charlotte.Flowertact
{
	public class Fortewave
	{
		private object SYNCROOT = new object();
		private PostOfficeBox _rPob;
		private PostOfficeBox _wPob;

		public Fortewave(string ident)
			: this(ident, ident)
		{ }

		public Fortewave(string rIdent, string wIdent)
		{
			if (rIdent == null)
				throw new ArgumentNullException("rIdent");

			if (wIdent == null)
				throw new ArgumentNullException("wIdent");

			_rPob = new PostOfficeBox(rIdent);
			_wPob = new PostOfficeBox(wIdent);
		}

		public void Clear()
		{
			lock (SYNCROOT)
			{
				_rPob.Clear();
				_wPob.Clear();
			}
		}

		public void Send(object sendObj)
		{
			if (sendObj == null)
				throw new ArgumentNullException("sendObj");

			lock (SYNCROOT)
			{
				QueueData<SubBlock> sendData = new Serializer(sendObj).GetBuff();
				_wPob.Send(sendData);
			}
		}

		public object Recv(int millis)
		{
			if (millis < Timeout.Infinite)
				throw new ArgumentException("millis lt min");

			lock (SYNCROOT)
			{
				byte[] recvData = _rPob.Recv(millis);

				if (recvData == null)
					return null;

				object recvObj = new Deserializer(recvData).Next();
				return recvObj;
			}
		}
	}
}
