using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectTree
	{
		public static object Conv(object root)
		{
			if (root == null)
				return null;

			if (root.GetType().IsPrimitive) // for JsonTools.Encode()
			{
				return new JsonTools.Word()
				{
					Value = root.ToString(),
				};
			}

			if (root is string) // string も IEnumerable
				return root;

			if (root is IDictionary) // IDictionary は IEnumerable を継承しているので、先に。
			{
				ObjectMap om = ObjectMap.Create();

				foreach (DictionaryEntry pair in (IDictionary)root)
				{
					om.Add(pair.Key, Conv(pair.Value));
				}
				return om;
			}
			if (root is IEnumerable)
			{
				ObjectList ol = new ObjectList();

				foreach (object element in (IEnumerable)root)
				{
					ol.Add(Conv(element));
				}
				return ol;
			}
			return root;
		}

		public static ObjectTree Convert(object root)
		{
			return new ObjectTree(Conv(root));
		}

		private object Root;

		public ObjectTree(object root)
		{
			this.Root = root;
		}

		public ObjectTree this[int index]
		{
			get
			{
				return new ObjectTree(((ObjectList)this.Root)[index]);
			}
		}

		public ObjectTree this[string path]
		{
			get
			{
				return this[StringTools.Tokenize(path, "/")];
			}
		}

		public ObjectTree this[string[] pTkns]
		{
			get
			{
				object node = this.Root;

				foreach (string pTkn in pTkns)
				{
					if (node is ObjectList)
					{
						node = ((ObjectList)node)[int.Parse(pTkn)];
					}
					else if (node is ObjectMap)
					{
						node = ((ObjectMap)node)[pTkn];
					}
					else
					{
						throw new Exception("不明なパストークンです。" + pTkn);
					}
				}
				return new ObjectTree(node);
			}
		}

		public int Count
		{
			get
			{
				if (this.Root is ObjectList)
				{
					return ((ObjectList)this.Root).Count;
				}
				if (this.Root is ObjectMap)
				{
					return ((ObjectMap)this.Root).Count;
				}
				throw new Exception("リスト又はマップではありません。");
			}
		}

		public string[] GetKeys()
		{
			if (this.Root is ObjectList)
			{
				return IntTools.Sequence(((ObjectList)this.Root).Count).Select(index => index.ToString()).ToArray();
			}
			if (this.Root is ObjectMap)
			{
				return ((ObjectMap)this.Root).GetKeys().ToArray();
			}
			throw new Exception("リスト又はマップではありません。");
		}

		public string StringValue
		{
			get
			{
				return this.Root.ToString();
			}
		}

		public override string ToString()
		{
			if (this.Root == null)
			{
				return "[DEBUG] null";
			}
			return "[DEBUG] JSON " + JsonTools.Encode(this.Root);
		}

		public ObjectTree[] ToArray()
		{
			return ArrayTools.ToArray(this.Iterate());
		}

		public IEnumerable<ObjectTree> Iterate()
		{
			if (this.Root is ObjectList)
			{
				foreach (object element in ((ObjectList)this.Root).Direct())
				{
					yield return new ObjectTree(element);
				}
			}
			else if (this.Root is ObjectMap)
			{
				foreach (KeyValuePair<string, object> pair in ((ObjectMap)this.Root).GetEntries())
				{
					yield return new ObjectTree(new ObjectList(pair.Key, pair.Value));
				}
			}
			else
			{
				throw new Exception("リスト又はマップではありません。");
			}
		}

		public bool IsList()
		{
			return this.Root is ObjectList;
		}

		public bool IsMap()
		{
			return this.Root is ObjectMap;
		}

		public object Direct()
		{
			return this.Root;
		}
	}
}
