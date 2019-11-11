using SimpleECS.Core.Common;

namespace SimpleECS.Core.Systems {
	public interface IReadInputSystem : ISystem {
		(bool, KeyCode) TryReadKey();
	}
}