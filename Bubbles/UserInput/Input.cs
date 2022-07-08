using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bubbles.Surface;
using Bubbles.Vectors;
using Cursor = System.Windows.Forms.Cursor;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;
using Screen = Bubbles.Surface.Screen;

namespace Bubbles.UserInput
{
    internal class Input
    {
        private static Mouse mouse = new Mouse();
        private static KeyboardState keyStates;
        private static List<Key> keys = new List<Key>();

        public static void Start(KeyboardState keyStates)
        {
            Input.keyStates = keyStates;
        }

        public static void Update()
        {
            mouse.Update();

            for(int x = 0; x < keys.Count; x++)
            {
                Key k = keys[x];
                k.Update(keyStates);
            }
        }

        public static Vec2 GetMousePosition()
        {
            return GetMousePosition(new Vec2());
        }
        public static Vec2 GetMousePosition(Vec2 outVec)
        {
            outVec.Set(GetMouseX(), GetMouseY());
            return outVec;
        }
        public static float GetMouseX()
        {
            return mouse.GetMouseX();
        }

        public static float GetMouseY()
        {
            return mouse.GetMouseY();
        }

        public static bool IsMouseDown()
        {
            return mouse.IsMouseDown();
        }

        public static bool IsMousePressed()
        {
            return mouse.IsMousePressed();
        }

        public static Vec2 GetMouseSlide()
        {
            return mouse.GetMouseSlide();
        }

        public static float GetMouseSlideMagnitude()
        {
            return mouse.GetMouseSlideMagnitude();
        }

        public static void RegisterKey(Keys key)
        {
            for (int x = 0; x < keys.Count; x++)
            {
                Key k = keys[x];
                if (k.IsKey(key))
                {
                    return;
                }
            }
            keys.Add(new Key(key));
        }

        public static bool GetKeyDown(Keys key)
        {
            for (int x = 0; x < keys.Count; x++)
            {
                Key k = keys[x];
                if (k.IsKey(key))
                {
                    return k.IsDown();
                }
            }
            throw new Exception("Unregistered key " + key);          
        }
    }
}
