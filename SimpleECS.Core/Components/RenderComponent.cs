using SimpleECS.Core.Common;

namespace SimpleECS.Core.Components {
	public sealed class RenderComponent : IComponent {
		public Color Color;

		public RenderComponent Init(Color color) {
			Color = color;
			return this;
		}
	}
}