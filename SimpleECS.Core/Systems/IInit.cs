using SimpleECS.Core.State;

namespace SimpleECS.Core.Systems {
	public interface IInit {
		void Init(GameState state);
	}
}