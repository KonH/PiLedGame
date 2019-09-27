using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class HealthComponent : IComponent {
		public int Health;
		public readonly DamageLayer Layer;

		public HealthComponent(int health = 1, DamageLayer layer = default) {
			Health = health;
			Layer  = layer;
		}
	}
}