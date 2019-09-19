using PiLedGame.State;

namespace PiLedGame.System {
	public interface ISystem {
		void Update(GameState state);
	}
}