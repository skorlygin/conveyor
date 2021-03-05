using Conveyor.Core;
using System.Collections.Generic;

namespace Conveyor.List
{
	public sealed class ListConveyorBuilder : IConveyorBuilder<ListConveyor>
	{
		private readonly List<(double?, double?)> _input = new List<(double?, double?)>();
		private readonly List<(byte, byte)> _segments = new List<(byte, byte)>();

		public ListConveyor Build() => new ListConveyor(_segments, _input);

		public IConveyorBuilder<ListConveyor> WithInput(params (double?, double?)[] input)
		{
			_input.Clear();
			_input.AddRange(input);

			return this;
		}

		public IConveyorBuilder<ListConveyor> WithSegments(params (byte, byte)[] segments)
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