namespace SimpleECS.Core.Events {
	public sealed class ApplyDamageEvent : BaseEvent {
		public int Value { get; private set; }

		public void Init(int value) {
			Value = value;
		}
	}
}