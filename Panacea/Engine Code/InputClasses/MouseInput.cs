using Microsoft.Xna.Framework.Input;
using Panacea.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.InputClasses
{
    class MouseInput : IMouseInput
    {
        public MouseState GetCurrentState()
        {
            // CREATE a new instance of MouseState, called newKeyboardState. Assigned it to the current mouse state:
            MouseState newState = Mouse.GetState();
            // RETURN newKeyboardState:
            return newState;
        }
    }
}
