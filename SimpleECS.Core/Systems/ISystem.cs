using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public interface ISystem {
		void Update(GameState state);
	}
}