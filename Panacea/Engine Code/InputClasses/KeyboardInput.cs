using Microsoft.Xna.Framework.Input;
using Panacea.Interfaces;

namespace Panacea.InputClasses
{
    class KeyboardInput : IKeyboardInput
    {
        #region FIELDS
        #endregion
        #region PROPERTIES
        #endregion
        /// <summary>
        /// Gets the current state of the Keyboard and returns it.
        /// </summary>
        /// <returns>The current state of the keyboard.</returns>
        public KeyboardState GetCurrentState()
        {
            // CREATE a new instance of KeyboardState, called newState. Assigned it to the current Keyboard state:
            KeyboardState newState = Keyboard.GetState();
            // RETURN newState:
            return newState;
        }
    }
}
