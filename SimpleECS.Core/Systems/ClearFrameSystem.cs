using SimpleECS.Core.States;

namespace SimpleECS.Core.Systems {
	public sealed class ClearFrameSystem : ComponentSystem<FrameState> {
		public override void Update(FrameState frame) {
			frame.Clear();
		}
	}
}