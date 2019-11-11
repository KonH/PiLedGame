using UnityEngine;
using SimpleECS.Core.Systems;

namespace ShootGame.Unity {
	class ReadUnityRealTimeSystem : FinishFrameRealTimeSystemBase {
		public override double TotalElapsedSeconds => Time.realtimeSinceStartup;
	}
}