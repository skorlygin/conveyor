using Conveyor.Core;
using System.Collections.Generic;
using System.Linq;

namespace Conveyor.List
{
	public sealed class ListConveyor : IConveyor
	{
		private readonly List<(int A, int B)> _indexes;
		private readonly List<double> _a;
		private readonly List<double> _b;

		internal ListConveyor(IEnumerable<(byte LengthA, byte LengthB)> segments, IEnumerable<(double? A, double? B)> input)
		{
			_indexes = new List<(int, int)>(segments.Count());
			(int A, int B) last = (-1, -1);
			foreach (var segment in segments)
			{
				_indexes.Add((last.A + 1 + segment.LengthA, last.B + 1 + segment.LengthB));
				last = _indexes.Last();
			}

			_a = Enumerable.Repeat(0d, segments.Sum(__segment => __segment.LengthA) + segments.Count()).ToList();
			_b = Enumerable.Repeat(0d, segments.Sum(__segment => __segment.LengthB) + segments.Count()).ToList();

			foreach (var item in input)
			{
				if (item.A.HasValue)
				{
					PushA(item.A.Value);
				}
				if (item.B.HasValue)
				{
					PushB(item.B.Value);
				}
			}
		}

		public double PushA(double value)
		{
			_a.Insert(0, value);

			var result = _a.Last();
			_a.RemoveAt(_a.Count - 1);

			foreach (var index in _indexes)
			{
				_b[index.B] = _a[index.A];
			}

			return result;
		}

		public double PushB(double value)
		{
			_b.Insert(0, value);

			var result = _b.Last();
			_b.RemoveAt(_b.Count - 1);

			foreach (var index in _indexes)
			{
				_a[index.A] = _b[index.B];
			}

			return result;
		}
	}
}