using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleECS.Core.Common;
using SimpleECS.Core.Components;

namespace SimpleECS.Core.States {
	public sealed class InputRecordState : IComponent {
		public readonly List<FrameRecord> Frames = new List<FrameRecord>();

		public void Save(string path) {
			File.WriteAllLines(path, GetLines());
		}

		public string[] GetLines() {
			return Frames.Select(f => f.ToString()).ToArray();
		}
	}
}