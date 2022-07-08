using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bubbles.Timing
{
    internal class Time
    {
        private static double secondframe;
        private static Stopwatch stopWatch = new Stopwatch();
        private static float deltaTime;
        public static float timeScale = 1f;

        public static void Start()
        {
            stopWatch.Start();
        }
        public static void Update()
        {
            TimeSpan ts = stopWatch.Elapsed;
            double FirstFrame = ts.TotalMilliseconds / 1000d;
            deltaTime = (float)(FirstFrame - secondframe);
            secondframe = ts.TotalMilliseconds / 1000d;
        }

        public static float GetDeltaTime()
        {
            return deltaTime * timeScale;
        }
    }
}
