using Microsoft.Xna.Framework.Input;
using Panacea.Interfaces;

namespace Panacea.InputClasses
{
    class MouseInput : IMouseInput
    {
        /// <summary>
        /// Gets the current state of the Mouse and returns it.
        /// </summary>
        /// <returns>The current state of the mouse.</returns>
        public MouseState GetCurrentState()
        {
            // CREATE a new instance of MouseState, called newState. Assigned it to the current mouse state:
            MouseState newState = Mouse.GetState();
            // RETURN newState:
            return newState;
        }
    }
}
