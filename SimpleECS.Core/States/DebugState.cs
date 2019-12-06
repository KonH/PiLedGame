using System.Collections.Generic;
using SimpleECS.Core.Components;
using SimpleECS.Core.Configs;

namespace SimpleECS.Core.States {
	public sealed class DebugState : IComponent {
		readonly DebugConfig _config;

		public bool IsTriggered { get; set; }

		List<string> _contents = new List<string>();

		public DebugState(DebugConfig config) {
			_config = config;
		}

		public void Log(string line) {
			_contents.Add(line);
			if ( _contents.Count == _config.MaxLogSize ) {
				_contents.RemoveAt(0);
			}
		}

		public List<string> GetContent() {
			return new List<string>(_contents);
		}
	}
}