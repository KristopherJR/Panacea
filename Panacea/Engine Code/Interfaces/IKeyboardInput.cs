using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Interfaces
{
    interface IKeyboardInput
    {
        /// <summary>
        /// gets the current state of the Keyboard and returns it.
        /// </summary>
        /// <returns>The current state of the keyboard.</returns>
        KeyboardState GetCurrentState();
    }
}
