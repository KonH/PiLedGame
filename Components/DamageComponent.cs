using PiLedGame.Common;

namespace PiLedGame.Components {
	public sealed class DamageComponent : IComponent {
		public readonly int         Damage;
		public readonly DamageLayer Layer;
		public readonly bool        Persistent;

		public DamageComponent(int damage = 1, DamageLayer layer = default, bool persistent = false) {
			Damage     = damage;
			Layer      = layer;
			Persistent = persistent;
		}
	}
}