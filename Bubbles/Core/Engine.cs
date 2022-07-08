using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bubbles.Timing;
using Bubbles.Render;
using Bubbles.UserInput;
using Screen = Bubbles.Surface.Screen;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Bubbles.Core
{
    internal class Engine
    {
        private static EventHandler stopEvent;
        public static void Start(float locX, float locY, EventHandler stopEvent, KeyboardState keyStates)
        {
            Screen.loc.Set(locX, locY);
            Engine.stopEvent = stopEvent;
            Time.Start();
            Input.Start(keyStates);
            Game.Start();
            Renderer.Start();
        }

        public static void Update(float locX, float locY, float sizeX, float sizeY)
        {
            Screen.loc.Set(locX, locY);
            Screen.size.Set(sizeX, sizeY);
            Time.Update();
            Input.Update();
            Game.Update();
            Renderer.Render();
            //Debug.WriteLine("Frame time: " + Time.deltaTime);
        }

        public static void OnResize(float sizeX, float sizeY)
        {
            Screen.size.Set(sizeX, sizeY);
            Renderer.OnResize(sizeX, sizeY);
        }

        public static void Stop()
        {
            stopEvent.Invoke(null, EventArgs.Empty);
        }
    }
}
