using Conveyor.Core;
using System.Collections.Generic;
using System.Linq;

namespace Conveyor.LinkedList
{
	public sealed class LinkedListConveyorBuilder : IConveyorBuilder<LinkedListConveyor>
	{
		private readonly List<(double? A, double? B)> _input = new List<(double?, double?)>();
		private readonly List<(byte LengthA, byte LengthB)> _segments = new List<(byte, byte)>();

		public LinkedListConveyor Build()
		{
			_segments.Reverse();

			var last = new LinkedListConveyor.Item(0, null, null);
			foreach (var segment in _segments)
			{
				last = new LinkedListConveyor.Item(0,
					Enumerable.Range(0, segment.LengthA)
						.Aggregate(last, (__last, __) => new LinkedListConveyor.Item(0, __last, null)),
					Enumerable.Range(0, segment.LengthB)
						.Aggregate(last, (__last, __) => new LinkedListConveyor.Item(0, null, __last)));
			}

			var result = new LinkedListConveyor(last.NextA, last.NextB);

			foreach (var item in _input)
			{
				if (item.A.HasValue)
				{
					result.PushA(item.A.Value);
				}
				if (item.B.HasValue)
				{
					result.PushB(item.B.Value);
				}
			}

			return result;
		}

		public IConveyorBuilder<LinkedListConveyor> WithInput(params (double?, double?)[] input)
		{
			_input.Clear();
			_input.AddRange(input);

			return this;
		}

		public IConveyorBuilder<LinkedListConveyor> WithSegments(params (byte, byte)[] segments)
		{
			_segments.Clear();
			_segments.AddRange(segments);

			return this;
		}

		IConveyor IConveyorBuilder.Build() => Build();
		IConveyorBuilder IConveyorBuilder.WithInput(params (double?, double?)[] input) => WithInput(input);
		IConveyorBuilder IConveyorBuilder.WithSegments(params (byte, byte)[] segments) => WithSegments(segments);
	}
}