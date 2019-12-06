using System;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;

namespace SimpleECS.Core.States {
	public sealed class RandomState : IComponent {
		public readonly Random Generator;

		public RandomState(RandomConfig config) {
			var seed = config.Seed;
			Generator = (seed != null) ? new Random(seed.Value) : new Random();
		}
	}
}