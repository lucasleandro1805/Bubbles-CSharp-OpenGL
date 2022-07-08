using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bubbles.Vectors
{
    internal class Vec2
    {
        public float x;
        public float y;

        public Vec2()
        {

        }
        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(Vec2 vec)
        {
            Set(vec.x, vec.y);
        }
        public void Set(float x, float y)
        {
            this.x = x;
            this.y = y;        
        }

        public float Length()
        {
            return MathF.Sqrt(x * x + y * y);
        }
        public void Sub(Vec2 other, Vec2 outVec)
        {
            outVec.x = this.x - other.x;
            outVec.y = this.y - other.y;
        }
    }
}
