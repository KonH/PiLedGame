namespace PiLedGame.Utils {
	public interface IAbortWatcher {
		bool IsAbortRequested { get; }
	}
}