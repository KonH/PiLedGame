using System.IO;
using UnityEditor;
using UnityEngine;

namespace ExternalCompiler {
	public class ExternalCompilerWindow : EditorWindow {
		Compiler _compiler = new Compiler();

		public void OnGUI() {
			if ( GUILayout.Button("Add") ) {
				var currentDirectory = Directory.GetCurrentDirectory();
				var selectedDirectory = EditorUtility.OpenFolderPanel("Select project folder", currentDirectory, null);
				_compiler.Compile(currentDirectory, selectedDirectory);
			}
		}

		[MenuItem("ExternalCompiler/Setup")]
		public static void ShowWindow() {
			CreateWindow<ExternalCompilerWindow>("ExternalCompiler").Show();
		}
	}
}
