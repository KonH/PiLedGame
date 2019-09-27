using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SimpleECS.Core.State {
	public sealed class InputRecord {
		public readonly List<InputFrame> Frames;

		public InputRecord():this(new List<InputFrame>()) {}

		InputRecord(List<InputFrame> frameKeys) {
			Frames = frameKeys;
		}

		public void Save(string path) {
			File.WriteAllLines(path, Frames.Select(f => f.ToString()));
		}

		public static InputRecord Load(string path) {
			var lines = File.ReadAllLines(path);
			var frames = lines.Select(InputFrame.Parse).ToList();
			return new InputRecord(frames);
		}
	}
}