using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace Bubbles.UserInput
{
    internal class Key
    {
        private Keys keyName;
        private bool lastPressed;
        private bool down;
        private bool pressed;
        private bool up;

        public Key(Keys keyName)
        {
            this.keyName = keyName;
        }

        public void Update(KeyboardState keyStates)
        {
            if (down)
            {
                down = false;
            }
            if (up)
            {
                up = false;
            }
            
            pressed = keyStates.IsKeyPressed(keyName);
            if(!lastPressed && pressed)
            {
                down = true;
            }
            if(lastPressed && !pressed)
            {
                up = true;
            }
        }

        public bool IsDown()
        {
            return down;
        }
        public bool IsPressed()
        {
            return pressed;
        }
        public bool IsUp()
        {
            return up;
        }

        public bool IsKey(Keys key)
        {
            return keyName == key;
        }
    }
}
