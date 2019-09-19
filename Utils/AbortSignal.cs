namespace PiLedGame.Utils {
	public sealed class AbortSignal : IAbortWatcher {
		public bool IsAbortRequested { get; set; }
	}
}