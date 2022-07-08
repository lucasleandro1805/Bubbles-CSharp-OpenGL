using Bubbles.Timing;
using Bubbles.UserInput;
using Bubbles.VOS;
using Color = Bubbles.Vectors.Color;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace Bubbles.Core
{
    internal class Game
    {
        public static List<Wave> waves = new List<Wave>();
        public static List<Wave> appendWaves = new List<Wave>();
        public static List<Wave> destroyWaves = new List<Wave>();

        private static float mousePressedTime = 0;
        public static Color cursorColor;
        private static Color unPressedCursor = new Color();
        private static Color pressedCursor = new Color(0.2f, 0f, 1f);
        private static float spawnDelay = 0.2f;
        private static float spawnYet = 0f;
        private static bool paused = false;

        public static void Start()
        {
            Input.RegisterKey(Keys.Escape);
            Input.RegisterKey(Keys.Space);
        }
        public static void Update()
        {
            HandleMouseInput();
            HandleControl();
            UpdateWaves();
        }
        private static void HandleMouseInput()
        {
            if (Input.IsMouseDown())
            {
                appendWaves.Add(new Wave(Input.GetMouseX(), Input.GetMouseY(), Color.RandomRGB()));
            }

            if (Input.IsMousePressed())
            {
                cursorColor = pressedCursor;
                mousePressedTime += Time.GetDeltaTime();
                if (mousePressedTime >= 0.5f)
                {
                    spawnYet -= Time.GetDeltaTime() * (Input.GetMouseSlideMagnitude() * 5f + 1f);
                    if (spawnYet <= 0f)
                    {
                        appendWaves.Add(new Wave(Input.GetMouseX(), Input.GetMouseY(), Color.RandomRGB()));
                        spawnYet = spawnDelay;
                    }
                }
            }
            else
            {
                cursorColor = unPressedCursor;
                mousePressedTime = 0;
                spawnYet = 0f;
            }
        }
        private static void HandleControl()
        {
            if (Input.GetKeyDown(Keys.Escape))
            {
                Engine.Stop();
            }

            if (Input.GetKeyDown(Keys.Space))
            {
                paused = !paused;
            }

            if (paused)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1f;
            }
        }
        private static void UpdateWaves()
        {
            for (int i = 0; i < appendWaves.Count; i++)
            {
                Wave wave = appendWaves[i];
                wave.Start();
                waves.Add(wave);
            }
            appendWaves.Clear();

            for (int i = 0; i < waves.Count; i++)
            {
                Wave wave = waves[i];
                wave.Update();
                if (wave.IsDestroyed())
                {
                    destroyWaves.Add(wave);
                }
            }
            waves.RemoveAll(IsGarbage);
            destroyWaves.Clear();
        }

        private static bool IsGarbage(Wave i)
        {
            return i.IsDestroyed();
        }
    }
}
