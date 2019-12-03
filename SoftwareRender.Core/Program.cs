using SimpleECS.ConsoleLayer.Systems;
using SimpleECS.Core.State;
using SimpleECS.Core.Systems;
using SoftwareRender.Core.Systems;

namespace SoftwareRender.Core {
	static class Program {
		static void Main() {
			var graphics = new Graphics(new Screen(32, 32));
			var state = new GameState(graphics, null, null);
			var systems = new SystemSet();
			systems.Add(new DirectRenderSystem());
			systems.Add(new ConsoleRenderSystem());
			systems.Add(new RenderToFileSystem("frame_{0}.png"));
			systems.UpdateOnce(state);
		}
	}
}