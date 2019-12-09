using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleECS.Core.Common;

namespace SimpleECS.Core.Configs {
	public sealed class InputRecordConfig {
		public readonly List<FrameRecord> Frames;

		public InputRecordConfig(string path): this(File.ReadAllLines(path)) {}

		public InputRecordConfig(string[] lines) {
			Frames = lines.Select(FrameRecord.Parse).ToList();
		}
	}
}