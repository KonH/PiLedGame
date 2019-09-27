using System.Collections.Generic;

namespace SimpleECS.ConsoleLayer.Utils {
	public sealed class ArgumentSet {
		readonly Dictionary<string, string> _values = new Dictionary<string, string>();

		public static ArgumentSet Parse(string[] args) {
			var result = new ArgumentSet();
			foreach ( var rawArg in args ) {
				if ( !rawArg.StartsWith('-') ) {
					continue;
				}
				var arg = rawArg.Substring(1);
				var parts = arg.Split('=');
				if ( parts.Length == 2 ) {
					result._values[parts[0]] = parts[1];
				} else {
					result._values[arg] = true.ToString();
				}
			}
			return result;
		}

		public string Get(string name, string def) => _values.GetValueOrDefault(name, def);
		public bool Get(string name, bool def) => bool.Parse(_values.GetValueOrDefault(name, def.ToString()));
		public int Get(string name, int def) => int.Parse(_values.GetValueOrDefault(name, def.ToString()));
	}
}