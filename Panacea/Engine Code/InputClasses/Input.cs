using Panacea.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.InputClasses
{
    class Input : IInput
    {
        #region FIELDS
        // DECLARE a reference to IKeyboardInput, call it 'keyboardInput':
        private IKeyboardInput keyboardInput;
        // DECLARE a reference IMouseInput, call it 'mouseInput':
        private IMouseInput mouseInput;
        #endregion

        #region PROPERTIES
        public IKeyboardInput KeyboardInput // read-only property
        {
            get { return keyboardInput; } // get method
        }
        public IMouseInput MouseInput // read-only property
        {
            get { return mouseInput; } // get method
        }
        #endregion

        public Input()
        {
            // INITIALIZE keyboardInput and mouseInput:
            keyboardInput = new KeyboardInput();
            mouseInput = new MouseInput();
        }     
    }
}
