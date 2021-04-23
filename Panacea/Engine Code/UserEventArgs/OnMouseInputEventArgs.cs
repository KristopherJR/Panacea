using Microsoft.Xna.Framework.Input;
using System;

namespace Panacea.Engine_Code.UserEventArgs
{
    public class OnMouseInputEventArgs : EventArgs
    {
        #region FIELDS
        // DECLARE an instance of MouseState called _mouseState:
        private MouseState _mouseState;
        // DECLARE an int, call it _scrollValue:
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
        /// <param name="mouseState">The current MouseState.</param>
        /// <param name="scrollValue">The scroll wheel value.</param>
        public OnMouseInputEventArgs(MouseState mouseState, int scrollValue)
        {
            // STORE incoming parameters:
            _mouseState = mouseState;
            _scrollValue = scrollValue;
        }
    }
}
