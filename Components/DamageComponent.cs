namespace PiLedGame.Components {
	public sealed class DamageComponent : IComponent {
		public readonly int    Damage;
		public readonly string Layer;
		public readonly bool   Persistent;
		public bool            Triggered;

		public DamageComponent(int damage = 1, string layer = null, bool persistent = false) {
			Damage     = damage;
			Layer      = layer;
			Persistent = persistent;
		}
	}
}