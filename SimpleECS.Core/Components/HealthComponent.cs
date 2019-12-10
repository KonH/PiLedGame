using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class HealthComponent : IComponent {
		public int Health;
		public DamageLayer Layer { get; private set; }

		public HealthComponent Init(int health = 1, DamageLayer layer = default) {
			Health = health;
			Layer  = layer;
			return this;
		}
	}
}