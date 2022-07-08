using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bubbles.Vectors;
using Screen = Bubbles.Surface.Screen;

namespace Bubbles.UserInput
{
    internal class Mouse
    {
        private bool lastMousePress;
        private bool mousePressed;
        private bool mouseDown;
        private bool mouseUp;
        private Vec2 lastMousePos = new Vec2();
        private Vec2 mousePos = new Vec2();
        private Vec2 mouseSlide = new Vec2();

        public void Update()
        {
            mouseDown = false;
            mouseUp = false;
            if (lastMousePress && !(Control.MouseButtons == MouseButtons.Left))
            {
                lastMousePress = false;
                mouseUp = true;
            }

            if (!lastMousePress && Control.MouseButtons == MouseButtons.Left)
            {
                lastMousePress = true;
                mouseDown = true;
            }

            mousePressed = Control.MouseButtons == MouseButtons.Left;

            mousePos.Set(GetMouseX(), GetMouseY());
            mousePos.Sub(lastMousePos, mouseSlide);
            lastMousePos.Set(mousePos);
        }

        public float GetMouseX()
        {
            return Cursor.Position.X - Screen.loc.x;
        }

        public float GetMouseY()
        {
            return Screen.size.y - (Cursor.Position.Y - Screen.loc.y);
        }
        public bool IsMouseDown()
        {
            return mouseDown;
        }
        public bool IsMousePressed()
        {
            return mousePressed;
        }

        public Vec2 GetMouseSlide()
        {
            return mouseSlide;
        }

        public float GetMouseSlideMagnitude()
        {
            return mouseSlide.Length();
        }

    }
}
