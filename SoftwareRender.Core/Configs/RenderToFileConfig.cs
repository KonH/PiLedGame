namespace SoftwareRender.Core.Configs {
	public sealed class RenderToFileConfig {
		public readonly string FileNameFormat;

		public RenderToFileConfig(string fileNameFormat) {
			FileNameFormat = fileNameFormat;
		}
	}
}