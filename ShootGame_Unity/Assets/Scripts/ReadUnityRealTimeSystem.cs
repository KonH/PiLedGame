using UnityEngine;
using SimpleECS.Core.Systems;

namespace ShootGame.Unity {
	class ReadUnityRealTimeSystem : BaseFinishFrameRealTimeSystem {
		public override double TotalElapsedSeconds => Time.realtimeSinceStartup;
	}
}