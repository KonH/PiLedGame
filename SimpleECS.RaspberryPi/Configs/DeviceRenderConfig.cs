namespace SimpleECS.RaspberryPi.Configs {
	public sealed class DeviceRenderConfig {
		public readonly byte Brightness;

		public DeviceRenderConfig(byte brightness) {
			Brightness = brightness;
		}
	}
}