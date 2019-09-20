using System.Collections.Generic;

namespace PiLedGame.State {
	public sealed class Debug {
		public float UpdateTime  { get; }
		public bool  IsTriggered { get; set; }

		readonly int _maxLogSize;

		List<string> _contents = new List<string>();

		public Debug(float updateTime, int maxLogSize) {
			UpdateTime  = updateTime;
			_maxLogSize = maxLogSize;
		}

		public void Log(string line) {
			_contents.Add(line);
			if ( _contents.Count == _maxLogSize ) {
				_contents.RemoveAt(0);
			}
		}

		public List<string> GetContent() {
			return new List<string>(_contents);
		}
	}
}