namespace PiLedGame.Components {
	public sealed class DamageComponent : IComponent {
		public readonly int    Damage;
		public readonly string Layer;

		public DamageComponent(int damage = 1, string layer = null) {
			Damage = damage;
			Layer  = layer;
		}
	}
}