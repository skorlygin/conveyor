using Conveyor.Core;
using Conveyor.LinkedList;
using Conveyor.List;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Conveyor.Tests
{
	public class TimeTests
	{
		private readonly ITestOutputHelper _output;

		public TimeTests(ITestOutputHelper output)
		{
			_output = output;
		}

		public static IEnumerable<object[]> ConveyorBuilders => new List<object[]>
		{
			new object[] { new ListConveyorBuilder() },
			new object[] { new LinkedListConveyorBuilder() }
		};

		[Theory]
		[MemberData(nameof(ConveyorBuilders))]
		public void Conveyor_500_to_1500__by_5_to_10__PushA_1000(IConveyorBuilder builder)
		{
			var random = new Random(616);
			var timer = Stopwatch.StartNew();

			for (var count = 500; count <= 1500; count += 500)
			{
				var segments = Enumerable
					.Repeat(0, count)
					.Select(__ => ((byte)random.Next(5, 10), (byte)random.Next(5, 10)))
					.ToArray();

				timer.Reset();

				var sut = builder
					.WithSegments(segments)
					.Build();

				_output.WriteLine($"{sut.GetType().Name}@{count} initialize: {timer.ElapsedMilliseconds} ms");

				timer.Reset();

				var result = Enumerable
					.Repeat(0, 1000)
					.Select(__value => sut.PushA(__value))
					.ToArray();

				_output.WriteLine($"{sut.GetType().Name}@{count} PushA {1000} times: {timer.ElapsedMilliseconds} ms");
			}
		}
	}
}