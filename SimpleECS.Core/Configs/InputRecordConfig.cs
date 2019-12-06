using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class InputRecordConfig {
		public readonly List<FrameRecord> Frames;

		public InputRecordConfig(string path) {
			var lines = File.ReadAllLines(path);
			Frames = lines.Select(FrameRecord.Parse).ToList();
		}
	}
}