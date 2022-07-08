using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Bubbles.Surface;

namespace Bubbles.Core
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "Just a game",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new DisplayWindow(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}