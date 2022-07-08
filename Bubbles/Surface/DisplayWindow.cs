using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using Bubbles.Render;
using Bubbles.Core;

namespace Bubbles.Surface
{
    public partial class DisplayWindow : GameWindow
    {
        // A simple constructor to let us set properties like window size, title, FPS, etc. on the window.
        public DisplayWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            EventHandler stopEvent = new EventHandler(delegate (Object o, EventArgs a)
            {
                Close();
            });
            Engine.Start(Location.X, Location.Y, stopEvent, KeyboardState);
        }

        // This function runs on every update frame.
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            Engine.Update(Location.X, Location.Y, Size.X, Size.Y);
            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            Engine.OnResize(Size.X, Size.Y);
        }
    }
}
