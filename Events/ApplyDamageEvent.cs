namespace PiLedGame.Events {
	public sealed class ApplyDamageEvent : IEvent {
		public int Value;

		public ApplyDamageEvent(int value) {
			Value = value;
		}
	}
}