using PiLedGame.State;

namespace PiLedGame.Systems {
	public interface ISystem {
		void Update(GameState state);
	}
}