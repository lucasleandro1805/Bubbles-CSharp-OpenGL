using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bubbles.Vectors;

namespace Bubbles.Render
{
    internal class Binder
    {
        public static void uniform(string name, float x, float y, Shader shader)
        {
            int loc = shader.GetUniformlocation(name);
            GL.Uniform2(loc, x, y);
        }
        public static void uniform(string name, float x, Shader shader)
        {
            int loc = shader.GetUniformlocation(name);
            GL.Uniform1(loc, x);
        }
        public static void uniform(string name, Vectors.Color color, Shader shader)
        {
            int loc = shader.GetUniformlocation(name);
            GL.Uniform4(loc, color.r, color.g, color.b, color.a);
        }
        public static void uniform(string name, Vec2 vec, Shader shader)
        {
            int loc = shader.GetUniformlocation(name);
            GL.Uniform2(loc, vec.x, vec.y);
        }
    }
}
