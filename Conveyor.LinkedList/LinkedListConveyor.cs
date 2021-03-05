using Conveyor.Core;

namespace Conveyor.LinkedList
{
	public sealed class LinkedListConveyor : IConveyor
	{
		public sealed class Item : IConveyor
		{
			private double _value;
			public Item NextA { get; private set; }
			public Item NextB { get; private set; }

			internal Item(double value, Item nextA, Item nextB)
			{
				_value = value;
				NextA = nextA;
				NextB = nextB;
			}

			public double PushA(double value)
			{
				var result = NextA == null ? _value : NextA.PushA(_value);
				_value = value;

				return result;
			}

			public double PushB(double value)
			{
				var result = NextB == null ? _value : NextB.PushB(_value);
				_value = value;

				return result;
			}
		}

		private readonly Item _a;
		private readonly Item _b;

		public LinkedListConveyor(Item a, Item b)
		{
			_a = a;
			_b = b;
		}

		public double PushA(double value) => _a.PushA(value);
		public double PushB(double value) => _b.PushB(value);
	}
}