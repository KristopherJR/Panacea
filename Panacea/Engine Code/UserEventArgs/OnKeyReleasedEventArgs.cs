using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.UserEventArgs
{
    public class OnKeyReleasedEventArgs : EventArgs
    {
        #region FIELDS
        // DECLARE an instance of Keys called _keyReleased to store the key that was just released by the user:
        private Keys _keyReleased;
        #endregion

        #region PROPERTIES
        public Keys KeyReleased // read-only property
        {
            get { return _keyReleased; } // get method
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class OnKeyReleasedEventArgs.
        /// </summary>
        /// <param name="keyReleased">The key that was just released.</param>
        public OnKeyReleasedEventArgs(Keys keyReleased)
        {
            // SET the incoming parameter to the _keyInput:
            _keyReleased = keyReleased;
        }
    }
}
