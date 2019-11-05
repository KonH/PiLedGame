using System.IO;
using UnityEditor;

namespace ExternalCompiler {
	public class Compiler {
		public void Compile(string currentDirectory, string projectDirectory) {
			var relativePath = PathUtils.GetRelativeDirectoryPath(projectDirectory, currentDirectory);
			var relativeTargetDirectory = Path.Combine(currentDirectory, "Assets", "Plugins");
			ProcessUtils.Start("/usr/local/share/dotnet/dotnet", $"build -o {relativeTargetDirectory}", relativePath);
			AssetDatabase.Refresh();
		}
	}
}
