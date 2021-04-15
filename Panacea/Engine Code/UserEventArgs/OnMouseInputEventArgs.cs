using Microsoft.Xna.Framework.Input;
using System;

namespace Panacea.Engine_Code.UserEventArgs
{
    public class OnMouseInputEventArgs : EventArgs
    {
        #region FIELDS
        // DECLARE an instance of ButtonState called _mouseButtonInput to store the mouse button that was just pressed by a user:
        private MouseState _mouseState;
        private int _scrollValue;
        #endregion

        #region PROPERTIES
        public MouseState MouseState // read-only property
        {
            get { return _mouseState; } // get method
        }

        public int ScrollValue // read-only property
        {
            get { return _scrollValue; } // get method
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class OnMouseInputEventArgs.
        /// </summary>
        /// <param name="mouseButtonInput">The Mouse Button that was just pressed.</param>
        public OnMouseInputEventArgs(MouseState mouseState, int scrollValue)
        {
            // STORE incoming parameters:
            _mouseState = mouseState;
            _scrollValue = scrollValue;
        }
    }
}
