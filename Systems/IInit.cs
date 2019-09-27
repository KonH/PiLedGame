using PiLedGame.State;

namespace PiLedGame.Systems {
	public interface IInit {
		void Init(GameState state);
	}
}