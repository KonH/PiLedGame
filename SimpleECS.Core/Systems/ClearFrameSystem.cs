using SimpleECS.Core.Entities;
using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class ClearFrameSystem : ComponentSystem<FrameState> {
		public override void Update(ComponentCollection<FrameState> components) {
			foreach ( var frame in components ) {
				frame.Clear();
			}
		}
	}
}