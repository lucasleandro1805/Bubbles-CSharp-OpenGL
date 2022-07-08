using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bubbles.Utils;
using Random = Bubbles.Utils.Random;

namespace Bubbles.Vectors
{
    internal class Color
    {
        public float r, g, b, a;

        public Color()
        {
            r = g = b = a = 1.0f;
        }
        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public void Set(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static Color RandomRGB()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
