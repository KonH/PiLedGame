using SimpleECS.Core.Events;

namespace ShootGame.Logic.Events {
	public sealed class AddScoreEvent : IEvent {
		public int Score { get; private set; }

		public void Init(int score) {
			Score = score;
		}
	}
}