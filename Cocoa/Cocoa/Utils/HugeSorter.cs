using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Utils
{
	public abstract class HugeSorter<T>
	{
		protected abstract void BeforeFirstRead();
		protected abstract T Read();
		protected abstract void AfterLastRead();
		protected abstract void BeforeFirstWrite();
		protected abstract void Write(T value);
		protected abstract void AfterLastWrite();
		protected abstract void Copy(IPart part);

		protected abstract int GetWeight(T value);
		protected abstract int Capacity();

		protected abstract IPart CreatePart();

		protected interface IPart : IDisposable
		{
			void BeforeFirstWrite();
			void Write(T value);
			void AfterLastWrite();
			void BeforeFirstRead();
			T Read();
			void AfterLastRead();
		}

		public void Sort(Comparison<T> comp)
		{
			Queue<IPart> parts = new Queue<IPart>();
			try
			{
				List<T> values = new List<T>();
				int loaded = 0;

				this.BeforeFirstRead();

				for (; ; )
				{
					T value = this.Read();

					if (value == null)
						break;

					values.Add(value);
					loaded += this.GetWeight(value);

					if (this.Capacity() < loaded)
					{
						this.WriteToPart(values, comp, parts);
						values.Clear();
						loaded = 0;
					}
				}
				if (1 <= values.Count)
					this.WriteToPart(values, comp, parts);

				this.AfterLastRead();

				values = null;
				loaded = -1;

				if (parts.Count == 0)
				{
					this.BeforeFirstWrite();
					this.AfterLastWrite();
				}
				else if (parts.Count == 1)
				{
					using (IPart part = parts.Dequeue())
					{
						this.Copy(part);
					}
				}
				else
				{
					while (2 <= parts.Count)
					{
						using (IPart part = parts.Dequeue())
						using (IPart part2 = parts.Dequeue())
						{
							if (parts.Count == 0)
							{
								this.BeforeFirstWrite();
								this.MergePart(part, part2, value => Write(value), comp);
								this.AfterLastWrite();
							}
							else
							{
								IPart partNew = CreatePart();

								parts.Enqueue(partNew);
								partNew.BeforeFirstWrite();

								this.MergePart(part, part2, value => partNew.Write(value), comp);

								partNew.AfterLastWrite();
							}
						}
					}
				}
			}
			finally
			{
				ExceptionDam.Section(eDam =>
				{
					foreach (IPart part in parts)
						eDam.Invoke(() => part.Dispose());
				});
			}
		}

		private void WriteToPart(List<T> values, Comparison<T> comp, Queue<IPart> parts)
		{
			values.Sort(comp);

			IPart part = CreatePart();

			parts.Enqueue(part);
			part.BeforeFirstWrite();

			foreach (T value in values)
				part.Write(value);

			part.AfterLastWrite();
		}

		private void MergePart(IPart part, IPart part2, Action<T> writer, Comparison<T> comp)
		{
			part.BeforeFirstRead();
			part2.BeforeFirstRead();

			T value = part.Read();
			T value2 = part2.Read();

			while (value != null && value2 != null)
			{
				int ret = comp(value, value2);

				if (ret <= 0)
				{
					writer(value);
					value = part.Read();
				}
				if (0 <= ret)
				{
					writer(value2);
					value2 = part2.Read();
				}
			}
			while (value != null)
			{
				writer(value);
				value = part.Read();
			}
			while (value2 != null)
			{
				writer(value2);
				value2 = part2.Read();
			}
			part.AfterLastRead();
			part2.AfterLastRead();
		}
	}
}
