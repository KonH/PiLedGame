using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public interface IInit {
		void Init(EntitySet entities);
	}
}