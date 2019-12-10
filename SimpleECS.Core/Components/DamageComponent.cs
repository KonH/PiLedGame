using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class DamageComponent : IComponent {
		public int         Damage     { get; private set; }
		public DamageLayer Layer      { get; private set; }
		public bool        Persistent { get; private set; }

		public DamageComponent Init(int damage = 1, DamageLayer layer = default, bool persistent = false) {
			Damage     = damage;
			Layer      = layer;
			Persistent = persistent;
			return this;
		}
	}
}