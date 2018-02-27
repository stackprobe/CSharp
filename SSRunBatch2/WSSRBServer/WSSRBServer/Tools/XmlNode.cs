using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Charlotte.Tools
{
	public class XmlNode
	{
		public string Name;
		public string Value;
		public List<XmlNode> Children;

		public XmlNode(string name = "", string value = "")
			: this(name, value, new List<XmlNode>())
		{ }

		public XmlNode(string name, string value, List<XmlNode> children)
		{
			this.Name = name;
			this.Value = value;
			this.Children = children;
		}

		public static XmlNode LoadFile(string xmlFile)
		{
			XmlNode node = new XmlNode();
			Stack<XmlNode> parents = new Stack<XmlNode>();

			using (XmlReader reader = XmlReader.Create(xmlFile))
			{
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							{
								XmlNode child = new XmlNode(reader.LocalName);

								node.Children.Add(child);
								parents.Push(node);
								node = child;

								bool singleTag = reader.IsEmptyElement;

								while (reader.MoveToNextAttribute())
									node.Children.Add(new XmlNode(reader.Name, reader.Value));

								if (singleTag)
									node = parents.Pop();
							}
							break;

						case XmlNodeType.Text:
							node.Value = reader.Value;
							break;

						case XmlNodeType.EndElement:
							node = parents.Pop();
							break;

						default:
							break;
					}
				}
			}
			node = node.Children[0];
			PostLoad(node);
			return node;
		}

		private static void PostLoad(XmlNode node)
		{
			node.Name = "" + node.Name;
			node.Value = "" + node.Value;

			{
				int index = node.Name.IndexOf(':');

				if (index != -1)
					node.Name = node.Name.Substring(index + 1);
			}

			node.Name = node.Name.Trim();
			node.Value = node.Value.Trim();

			foreach (XmlNode child in node.Children)
				PostLoad(child);
		}

		public void WriteToFile(string xmlFile)
		{
			File.WriteAllLines(xmlFile, this.GetLines(), Encoding.UTF8);
		}

		private const string INDENT = "\t";

		public string[] GetLines()
		{
			List<string> dest = new List<string>();
			dest.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			this.AddTo(dest, "");
			return dest.ToArray();
		}

		private void AddTo(List<string> dest, String indent)
		{
			string name = this.Name;

			if (name == "")
				name = "element";

			if (Children.Count != 0)
			{
				dest.Add(indent + "<" + name + ">" + this.Value);

				foreach (XmlNode child in Children)
					child.AddTo(dest, indent + INDENT);

				dest.Add(indent + "</" + name + ">");
			}
			else if (this.Value != "")
			{
				dest.Add(indent + "<" + name + ">" + this.Value + "</" + name + ">");
			}
			else
			{
				dest.Add(indent + "<" + name + "/>");
			}
		}

		public XmlNode Get(string path)
		{
			return this.Collect(path)[0];
		}

		public XmlNode[] Collect(string path)
		{
			List<XmlNode> dest = new List<XmlNode>();
			this.Collect(path.Split('/'), 0, dest);
			return dest.ToArray();
		}

		private void Collect(string[] ptkns, int index, List<XmlNode> dest)
		{
			if (index < ptkns.Length)
			{
				foreach (XmlNode child in this.Children)
					if (child.Name == ptkns[index])
						child.Collect(ptkns, index + 1, dest);
			}
			else
			{
				dest.Add(this);
			}
		}
	}
}
