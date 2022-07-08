using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using Bubbles.Core;
using Bubbles.Utils;
using Bubbles.VOS;
using Screen = Bubbles.Surface.Screen;
using Bubbles.UserInput;
using Bubbles.Vectors;

namespace Bubbles.Render
{
    internal class Renderer
    {
        private static Shader cursorShader;
        private static Shader waveShader;
        private static FSQ fsq;
        private static readonly Vec2 tmpMP = new Vec2();

        public static void Start()
        {
            cursorShader = new Shader(Resource.Read("Shaders.Cursor.vertex"), Resource.Read("Shaders.Cursor.fragment"));
            waveShader = new Shader(Resource.Read("Shaders.Wave.vertex"), Resource.Read("Shaders.Wave.fragment"));
            fsq = new FSQ();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
        }
        public static void Render()
        {
            GL.ClearColor(0, 0, 0, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (cursorShader.BeginRender())
            {
                Binder.uniform("screenSize", Screen.size                  , cursorShader);
                Binder.uniform("cursor"    , Input.GetMousePosition(tmpMP), cursorShader);
                Binder.uniform("color"     , Game.cursorColor             , cursorShader);
                fsq.Draw();
                cursorShader.StopRender();
            }
            if (waveShader.BeginRender())
            {
                /**
                 * Optimization idea
                 * Implement a batching renderer.
                 * Calling a draw call for every wave won't run fast.
                 * Calculate how many uniform vectors the device support
                 * then you send a array of waves every render pass
                 * this way you can render a big quantity of waves per draw call.
                 */
                for (int i = 0; i < Game.waves.Count; i++)
                {
                    Wave wave = Game.waves[i];
                    Binder.uniform("screenSize" , Screen.size     , waveShader);
                    Binder.uniform("loc"        , wave.loc        , waveShader);
                    Binder.uniform("start"      , wave.start      , waveShader);
                    Binder.uniform("end"        , wave.end        , waveShader);
                    Binder.uniform("color"      , wave.color      , waveShader);
                    Binder.uniform("lifetime"   , wave.lifetime   , waveShader);
                    Binder.uniform("maxLifetime", wave.maxLifeTime, waveShader);
                    fsq.Draw();
                }
                waveShader.StopRender();
            }
        }

        public static void OnResize(float sizeX, float sizeY)
        {
            GL.Viewport(0, 0, (int)sizeX, (int)sizeY);
        }
    }
}
