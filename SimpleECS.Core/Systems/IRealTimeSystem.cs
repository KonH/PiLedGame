namespace SimpleECS.Core.Systems {
	public interface IRealTimeSystem : ISystem {
		double TotalElapsedSeconds { get; }
	}
}