namespace SimpleECS.Core.Components {
	public sealed class FadeRenderComponent : IComponent {
		public double Decrease { get; private set; }
		public double Accum    { get; set; }

		public void Init(double decrease) {
			Decrease = decrease;
			Accum    = 0;
		}
	}
}