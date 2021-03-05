using Conveyor.Core;
using Conveyor.List;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Conveyor.Tests
{
	public class UnitTests
	{
		public static IEnumerable<object[]> ConveyorBuilders => new List<object[]>
		{
			new object[] { new ListConveyorBuilder() }
		};

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_11_11_PushA(IConveyorBuilder builder)
		{
			var sut = builder
				.WithSegments((1, 1), (1, 1))
				.WithInput((1, null), (0, null), (0, null), (0, null))
				.Build();

			var result = sut.PushA(0);

			result.Should().Be(1);
		}

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_11_11_PushB(IConveyorBuilder builder)
		{
			var sut = builder
				.WithSegments((1, 1), (1, 1))
				.WithInput((1, null), (0, null), (0, null), (0, null))
				.Build();

			var result = sut.PushB(0);

			result.Should().Be(1);
		}

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_11_11_PushA_Full(IConveyorBuilder builder)
		{
			var sut = builder
				.WithSegments((1, 1), (1, 1))
				.WithInput((1, null), (2, null), (3, null), (4, null))
				.Build();

			var result = Enumerable.Range(5, 4).Select(__value => sut.PushA(__value)).ToArray();

			result.Should().HaveCount(4);
			result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4 });
		}

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_11_11_PushB_Full(IConveyorBuilder builder)
		{
			var sut = builder
				.WithSegments((1, 1), (1, 1))
				.WithInput((1, null), (2, null), (3, null), (4, null))
				.Build();

			var result = Enumerable.Range(5, 4).Select(__value => sut.PushB(__value)).ToArray();

			result.Should().HaveCount(4);
			result.Should().BeEquivalentTo(new[] { 1, 0, 3, 0 });
		}

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_21_41_PushA_Full(IConveyorBuilder builder)
		{
			var sut = builder
				.WithSegments((2, 1), (3, 1))
				.WithInput((1, null), (2, null), (3, null), (4, null), (5, null), (6, null), (7, null))
				.Build();

			var result = Enumerable.Range(8, 7).Select(__value => sut.PushA(__value)).ToArray();

			result.Should().HaveCount(7);
			result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7 });
		}

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_21_41_PushB_Full(IConveyorBuilder builder)
		{
			var sut = builder
				.WithSegments((2, 1), (3, 1))
				.WithInput((1, null), (2, null), (3, null), (4, null), (5, null), (6, null), (7, null))
				.Build();

			var result = Enumerable.Range(8, 4).Select(__value => sut.PushB(__value)).ToArray();

			result.Should().HaveCount(4);
			result.Should().BeEquivalentTo(new[] { 1, 0, 5, 0 });
		}
	}
}