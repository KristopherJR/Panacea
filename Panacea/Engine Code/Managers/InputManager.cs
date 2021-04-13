using Panacea.InputClasses;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Managers
{
    class InputManager : IInputManager, IInputPublisher
    { 
        #region FIELDS
        // DECLARE a new event to publish when a new input occurs:
        public event EventHandler<OnInputEventArgs> NewInput;
        // DECLARE a new event that signifies a Key has been released:
        public event EventHandler<OnKeyReleasedEventArgs> KeyReleased;

        // DECLARE a reference to IInput, call it input:
        private IInput input;
        // DECLARE a List of IInputListener, call it subscribers:
        private List<IInputListener> subscribers;
        
        // DECLARE a KeyboardState, call it newState:
        private KeyboardState newState;
        // DECLARE a KeyboardState, call it oldState:
        private KeyboardState oldState;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class InputManager.
        /// </summary>
        public InputManager()
        {
            // INITIALISE input:
            input = new Input();
            // INITIALISE subscribers:
            subscribers = new List<IInputListener>();
        }

        /// <summary>
        /// Check for any new inputs that have occured from the user.
        /// </summary>
        private void CheckNewInput()
        {
            // GET the current Keyboard State:
            newState = ((input as Input).KeyboardInput).GetCurrentState();
            // STORE any keys that are pressed into a new variable, 'keysPressed':
            Keys[] keysPressed = newState.GetPressedKeys();
            // LOOP for the amount of keys that have been pressed:
            foreach (Keys key in keysPressed)
            {
                // LOOP through all subscribers:
                foreach (IInputListener sub in subscribers)
                {
                    // TEMPORARILY store the subscribers KOI as its own variable:
                    Keys[] tempKOI = sub.getKOI();
                    // CHECK if the pressed 'key' matches a key in the subscribers KeysOfInterest:
                    for(int i=0; i < tempKOI.Length; i++)
                    {
                        if (tempKOI[i] == key)
                        {
                            // AND if there are subscribers to the Input Event:
                            if (NewInput != null)
                            {
                                // FIRE the event and pass in the key that was pressed:
                                this.OnNewInput(key);
                                // SET oldState to newState:
                                oldState = newState;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method checks if a previously pressed key has been released by the User and fires an event if it has.
        /// </summary>
        private void CheckKeyReleased()
        {
            // LOOP through all subscribers:
            foreach (IInputListener sub in subscribers)
            {
                // TEMPORARILY store the subscribers KOI as its own variable:
                Keys[] tempKOI = sub.getKOI();
                // CHECK if the pressed 'key' matches a key in the subscribers KeysOfInterest:
                for (int i = 0; i < tempKOI.Length; i++)
                {
                    // IF a previously pressed key of interest has been released:
                    if (newState.IsKeyUp(tempKOI[i]) && oldState.IsKeyDown(tempKOI[i]))
                    {
                        // FIRE the key release event, passing in the current Key of interest:
                        this.OnKeyReleased(tempKOI[i]);
                    }
                }
            }
        }

        #region IMPLEMENTATION OF IInputManager
        /// <summary>
        /// Called when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="keyInput">Key that was just pressed.</param>
        public virtual void OnNewInput(Keys keyInput)
        {
            // STORE some information about the event and the new keyInput:
            OnInputEventArgs args = new OnInputEventArgs(keyInput);
            // FIRE the event to listeners:
            NewInput(this, args);
        }
        /// <summary>
        /// Called when a key is released.
        /// </summary>
        /// <param name="keyReleased">Key that was just released.</param>
        public virtual void OnKeyReleased(Keys keyReleased)
        {
            // STORE some information aabout the event and the keyReleased:
            OnKeyReleasedEventArgs args = new OnKeyReleasedEventArgs(keyReleased);
            // FIRE the event to listeners:
            KeyReleased(this, args);
        }

        /// <summary>
        /// Default update method for classes implementing IInputManager Interface.
        /// </summary>
        public void update()
        {
            // look for changes in input data
            this.CheckNewInput();
            this.CheckKeyReleased();
        }
        #endregion

        #region IMPLEMENTATION OF IInputPublisher
        /// <summary>
        /// Allows a listener to subscribe to the InputManager to watch for changes in input. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="subscriber">A reference to the object subscribing to events from InputManager.</param>
        /// <param name="inputHandler">The input handler to subscribe to the event.</param>
        /// <param name="releaseHandler">The key release handler to subscribe to the event.</param>
        public void subscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler)
        {
            // ADD the new subscriber to the subscribers List:
            subscribers.Add(subscriber);
            // ADD input event handler:
            NewInput += inputHandler;
            // ADD key released handler:
            KeyReleased += releaseHandler;
        }

        /// <summary>
        /// Allows a listener to unsubscribe from the InputManagers events.
        /// </summary>
        /// <param name="subscriber">A reference to the unsubscribing object of InputManager events.</param>
        /// <param name="inputHandler">The input handler to unsubscribe from the event.</param>
        /// <param name="releaseHandler">The key release handler to unsubscribe from the event.</param>
        public void unsubscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler)
        {
            // REMOVE the subscriber from the subscribers List:
            subscribers.Remove(subscriber);
            // UNSUBSCRIBE from the input event:
            NewInput -= inputHandler;
            // UNSUBSCRIBE from the key release event:
            KeyReleased -= releaseHandler;
        }
        #endregion
    }
}
