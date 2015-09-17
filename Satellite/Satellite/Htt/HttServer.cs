﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Satellite.Tools;
using Charlotte.Flowertact;
using Charlotte.Satellite;
using System.IO;

namespace Charlotte.Htt
{
	public class HttServer
	{
		private const string COMMON_ID = "{7da01163-efa3-4941-a5a6-be0800720d8e}"; // shared_uuid:3
		private const string MUTEX_ID = COMMON_ID + "_m";
		private const string HTT_ID = COMMON_ID + "_h";
		private const string HTT_SERVICE_ID = COMMON_ID + "_hs";

		private static readonly byte[] EMPTY = new byte[0];
		private static readonly byte[] COMMAND_CLEAR = Encoding.ASCII.GetBytes("C");
		private static readonly byte[] COMMAND_RESPONSE = Encoding.ASCII.GetBytes("R");
		private static readonly byte[] COMMAND_ERROR = Encoding.ASCII.GetBytes("E");

		private static MutexObject _mutex = new MutexObject(MUTEX_ID);
		private static Fortewave _pipeline;

		public static void Perform(HttService service)
		{
			if (service == null)
				throw new ArgumentNullException("service");

			if (_mutex.WaitOne(0))
			{
				try
				{
					_pipeline = new Fortewave(HTT_SERVICE_ID, HTT_ID);

					while (service.Interlude())
					{
						Object recvData = _pipeline.Recv(2000);

						if (recvData != null)
						{
							try
							{
								HttResponse res = service.Service(new HttRequest((ObjectList)recvData, _pipeline));
								ObjectList ol = new ObjectList();

								ol.Add(COMMAND_RESPONSE);
								ol.Add(((ObjectList)recvData).GetList()[0]);
								ol.Add(Encoding.ASCII.GetBytes(res.GetHTTPVersion()));
								ol.Add(Encoding.ASCII.GetBytes("" + res.GetStatusCode()));
								ol.Add(Encoding.ASCII.GetBytes(res.GetReasonPhrase()));

								{
									Dictionary<String, String> headerFields = new Dictionary<String, String>();

									res.WriteHeaderFields(headerFields);

									List<string> lines = new List<string>();

									foreach (string key in headerFields.Keys)
									{
										string value = headerFields[key];

										foreach (string colo_value in StringTools.Tokenize(value, ':'))
										{
											string colo_key = key;

											foreach (string line_value in StringTools.Tokenize(colo_value, '\n'))
											{
												ol.Add(Encoding.ASCII.GetBytes(colo_key));
												ol.Add(Encoding.ASCII.GetBytes(line_value));

												colo_key = "";
											}
										}
									}
									ol.Add(Encoding.ASCII.GetBytes("" + (lines.Count / 2)));

									foreach (string line in lines)
										ol.Add(Encoding.ASCII.GetBytes(line));
								}

								{
									string bodyPartFile = res.GetBodyPartFile();

									if (bodyPartFile != null)
									{
										ol.Add(StringTools.ENCODING_SJIS.GetBytes(Path.GetFullPath(bodyPartFile)));
										ol.Add(EMPTY);
									}
									else
									{
										ol.Add(EMPTY);

										{
											byte[] bodyPart = res.GetBodyPart();

											if (bodyPart == null)
												bodyPart = EMPTY;

											ol.Add(bodyPart);
										}
									}
								}

								_pipeline.Send(ol);
							}
							catch //(Exception e)
							{
								//Console.WriteLine(e);

								_pipeline.Send(new ObjectList(
										COMMAND_ERROR,
										((ObjectList)recvData).GetList()[0]
										));
							}
						}
					}
				}
				finally
				{
					if (_pipeline != null)
					{
						_pipeline.Close();
						_pipeline = null;
					}
					_mutex.Release();
				}
			}
		}
	}
}
