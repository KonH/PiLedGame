using SimpleECS.Core.Entities;

namespace SimpleECS.Core.Systems {
	public interface ISystem {
		void Update(EntitySet entities);
	}
}