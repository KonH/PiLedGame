namespace PiLedGame.Components {
	public sealed class HealthComponent : IComponent {
		public int Health;
		public readonly string Layer;

		public HealthComponent(int health = 1, string layer = null) {
			Health = health;
			Layer  = layer;
		}
	}
}