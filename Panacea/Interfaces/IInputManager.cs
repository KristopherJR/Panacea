using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2451Project.Interfaces
{
    interface IInputManager
    {
        /// <summary>
        /// Called when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="keyInput">Key that was just pressed.</param>
        void OnNewInput(Keys keyInput);

        /// <summary>
        /// Called when a key is released.
        /// </summary>
        /// <param name="keyReleased">Key that was just released.</param>
        void OnKeyReleased(Keys keyReleased);

        /// <summary>
        /// Default update method for classes implementing IInputManager Interface.
        /// </summary>
        void update();
    }
}
