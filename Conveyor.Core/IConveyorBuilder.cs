namespace Conveyor.Core
{
	public interface IConveyorBuilder
	{
		IConveyorBuilder WithInput(params (double?, double?)[] input);
		IConveyorBuilder WithSegments(params (byte, byte)[] segments);
		IConveyor Build();
	}

	public interface IConveyorBuilder<T> : IConveyorBuilder where T : IConveyor
	{
		new IConveyorBuilder<T> WithInput(params (double?, double?)[] input);
		new IConveyorBuilder<T> WithSegments(params (byte, byte)[] segments);
		new T Build();
	}
}