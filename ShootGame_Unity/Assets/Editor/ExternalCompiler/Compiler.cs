using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ExternalCompiler {
	public class Compiler {
		ConcurrentDictionary<string, CompileResult> Libraries { get; } = new ConcurrentDictionary<string, CompileResult>();

		public CompileResult TryDequeueNextResult() {
			var finishedResult = Libraries
				.FirstOrDefault(p => p.Value != null);
			if ( string.IsNullOrEmpty(finishedResult.Key) ) {
				return null;
			}
			Libraries.TryRemove(finishedResult.Key, out var result);
			return result;
		}

		public bool IsCompiling(string projectPath) {
			if ( !Libraries.TryGetValue(projectPath, out var value) ) {
				return false;
			}
			return (value == null);
		}

		public void TryCompileLibrary(string currentDirectory, string projectDirectory) {
			if ( !Libraries.TryAdd(projectDirectory, null) ) {
				return;
			}
			Task.Run(() => {
				try {
					Compile(currentDirectory, projectDirectory);
				} catch ( Exception e ) {
					UnityEngine.Debug.LogException(e);
					Libraries.TryRemove(projectDirectory, out _);
				}
			});
		}

		void Compile(string currentDirectory, string projectDirectory) {
			var relativePath = PathUtils.GetRelativeDirectoryPath(projectDirectory, currentDirectory);
			var relativeTargetDirectory = Path.Combine(currentDirectory, "Assets", "Plugins");
			var fileName = "/usr/local/share/dotnet/dotnet";
			var args = $"build -o {relativeTargetDirectory}";
			var (exitCode, output) = ProcessUtils.Start(fileName, args, relativePath);
			Libraries.TryUpdate(projectDirectory, new CompileResult(projectDirectory, exitCode, output), null);
		}
	}
}
