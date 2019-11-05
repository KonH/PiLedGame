using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace ExternalCompiler {
	public static class ProcessUtils {
		public static void Start(string fileName, string arguments, string workDir) {
			var startInfo = new ProcessStartInfo {
				FileName = fileName,
				Arguments = arguments,
				WorkingDirectory = workDir,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			};
			var process = Process.Start(startInfo);
			process.WaitForExit();
			var exitCode = process.ExitCode;
			var output = process.StandardOutput.ReadToEnd();
			var error = process.StandardError.ReadToEnd();
			var message = $"Result:\nOutput:\n{output}\nError:\n{error}";
			if ( exitCode != 0 ) {
				Debug.LogErrorFormat(message);
			} else {
				Debug.LogFormat(message);
			}
		}
	}
}