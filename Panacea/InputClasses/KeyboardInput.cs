using COMP2451Project.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2451Project.InputClasses
{
    class KeyboardInput : IKeyboardInput
    {
        #region FIELDS
        #endregion
        #region PROPERTIES
        #endregion
        public KeyboardInput()
        {
            //do nothing
        }

        /// <summary>
        /// gets the current state of the Keyboard and returns it.
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
