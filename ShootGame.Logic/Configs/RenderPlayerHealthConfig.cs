using System;
using SimpleECS.Core.Common;

namespace ShootGame.Logic.Configs {
	public sealed class RenderPlayerHealthConfig {
		public readonly Point2D          StartPosition;
		public readonly Point2D          Offset;
		public readonly Func<int, Color> HealthToColor;

		public RenderPlayerHealthConfig(Point2D startPosition, Point2D offset, Func<int, Color> healthToColor) {
			StartPosition = startPosition;
			Offset        = offset;
			HealthToColor = healthToColor;
		}
	}
}