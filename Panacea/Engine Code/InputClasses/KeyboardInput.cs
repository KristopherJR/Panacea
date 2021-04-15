using Panacea.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.InputClasses
{
    class KeyboardInput : IKeyboardInput
    {
        #region FIELDS
        #endregion
        #region PROPERTIES
        #endregion
        /// <summary>
        /// gets the current state of the Keyboard and returns it.
        /// </summary>
        /// <returns>The current state of the keyboard.</returns>
        public KeyboardState GetCurrentState()
        {
            // CREATE a new instance of KeyboardState, called newKeyboardState. Assigned it to the current Keyboard state:
            KeyboardState newState = Keyboard.GetState();
            // RETURN newKeyboardState:
            return newState;
        }
    }
}
