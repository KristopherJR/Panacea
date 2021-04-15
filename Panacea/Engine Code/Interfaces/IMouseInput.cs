using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Interfaces
{
    interface IMouseInput
    {
        /// <summary>
        /// gets the current state of the Mouse and returns it.
        /// </summary>
        /// <returns>The current state of the mouse.</returns>
        MouseState GetCurrentState();
    }
}
