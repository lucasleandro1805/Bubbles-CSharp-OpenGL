using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bubbles.Timing;
using Bubbles.Vectors;
using Color = Bubbles.Vectors.Color;

namespace Bubbles.VOS
{
    internal class Wave
    {
        public float start;
        public float end;
        public Vec2 loc;
        public Color color;
        public float lifetime;
        public float maxLifeTime = 5f;
        public float speed = 150;

        public Wave(float x, float y)
        {
            color = new Color();
            loc = new Vec2(x, y);
            start = 1;
            end = 2;
        }
        public Wave(float x, float y, Color color)
        {
            this.color = color;
            loc = new Vec2(x, y);
            start = 1;
            end = 2;
        }

        public void Start()
        {

        }
        public void Update()
        {
            float progress = lifetime / maxLifeTime;
            float invProgress = 1.0f - progress;
            start += speed * 0.5f * invProgress * Time.GetDeltaTime();
            end += speed * invProgress * Time.GetDeltaTime();
            lifetime += Time.GetDeltaTime();
        }

        public bool IsDestroyed()
        {
            return lifetime >= maxLifeTime;
        }
    }
}
