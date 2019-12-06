using SimpleECS.Core.Configs;
using SimpleECS.Core.Entities;
using SimpleECS.Core.Systems;
using SoftwareRender.Core.Configs;
using SoftwareRender.Core.Systems;

namespace SoftwareRender.Core {
	static class Program {
		static void Main() {
			var entities = new EntitySet();
			var screen = new ScreenConfig(32, 32);
			var systems = new SystemSet();
			systems.Add(new DirectRenderSystem());
			systems.Add(new RenderToFileSystem(screen, new RenderToFileConfig("frame_{0}.png")));
			systems.UpdateOnce(entities);
		}
	}
}