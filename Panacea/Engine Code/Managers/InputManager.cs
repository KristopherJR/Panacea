using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.UserEventArgs;
using Panacea.InputClasses;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using System;
using System.Collections.Generic;

namespace Panacea.Managers
{
    class InputManager : IInputManager, IInputPublisher
    {
        #region FIELDS
        // DECLARE a new event to publish when a new input occurs:
        public event EventHandler<OnInputEventArgs> NewInput;
        // DECLARE a new event that signifies a Key has been released:
        public event EventHandler<OnKeyReleasedEventArgs> KeyReleased;

        // DECLARE a new event that signifies a Mouse input has occured:
        public event EventHandler<OnMouseInputEventArgs> NewMouseInput;

        // DECLARE a reference to IInput, call it input:
        private IInput input;

        // DECLARE a List of IInputListener, call it subscribers:
        private List<IInputListener> subscribers;

        // DECLARE a KeyboardState, call it newKeyboardState:
        private KeyboardState newKeyboardState;
        // DECLARE a KeyboardState, call it oldKeyboardState:
        private KeyboardState oldKeyboardState;

        // DECLARE a MouseState, call it newMouseState:
        private MouseState newMouseState;
        // DECLARE a Mouse, call it oldMouseState:
        private MouseState oldMouseState;

        // DECLARE an int, call it lastScrollState:
        private int lastScrollState;
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
            // INITIALISE lastScrollState:
            lastScrollState = 0;
        }

        /// <summary>
        /// Check for new mouse inputs from the user.
        /// </summary>
        private void CheckNewMouseInput()
        {
            // GET the current Mouse State:
            newMouseState = ((input as Input).MouseInput).GetCurrentState();
            
            foreach (IInputListener sub in subscribers)
            {
                // IF there are subscribers to mouse input events:
                if(NewMouseInput != null)
                {
                    // IF the mouse button has been pressed and the current mouse state is different to the previous:
                    if(newMouseState.LeftButton == ButtonState.Pressed && newMouseState.LeftButton != oldMouseState.LeftButton)
                    {
                        // FIRE the event passing in the mouse state:
                        this.OnNewMouseInput(newMouseState, 0);
                    }
                    // USER has scrolled up:
                    if (newMouseState.ScrollWheelValue > lastScrollState)
                    {
                        // FIRE the event passing in the mouse state:
                        this.OnNewMouseInput(newMouseState, 1);
                        // STORE the previous scroll wheel state:
                        lastScrollState = newMouseState.ScrollWheelValue;
                    }
                    // USER has scrolled down:
                    if (newMouseState.ScrollWheelValue < lastScrollState)
                    {          
                        this.OnNewMouseInput(newMouseState, -1);
                        lastScrollState = newMouseState.ScrollWheelValue;
                    }
                    
                }
            }
            // STORE the current state as oldMouseState:
            oldMouseState = newMouseState;

        }

        /// <summary>
        /// Check for any new inputs that have occured from the user.
        /// </summary>
        private void CheckNewInput()
        {
            // GET the current Keyboard State:
            newKeyboardState = ((input as Input).KeyboardInput).GetCurrentState();

            // STORE any keys that are pressed into a new variable, 'keysPressed':
            Keys[] keysPressed = newKeyboardState.GetPressedKeys();
            // LOOP for the amount of keys that have been pressed:
            foreach (Keys key in keysPressed)
            {
                // LOOP through all subscribers:
                foreach (IInputListener sub in subscribers)
                {
                    // TEMPORARILY store the subscribers KOI as its own variable:
                    Keys[] tempKOI = sub.getKOI();
                    // CHECK if the pressed 'key' matches a key in the subscribers KeysOfInterest:
                    for (int i = 0; i < tempKOI.Length; i++)
                    {
                        if (tempKOI[i] == key)
                        {
                            // AND if there are subscribers to the Input Event:
                            if (NewInput != null)
                            {
                                // FIRE the event and pass in the key that was pressed:
                                this.OnNewInput(key);
                                // SET oldKeyboardState to newKeyboardState:
                                oldKeyboardState = newKeyboardState;
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
                    if (newKeyboardState.IsKeyUp(tempKOI[i]) && oldKeyboardState.IsKeyDown(tempKOI[i]))
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
            // STORE some information about the event and the keyReleased:
            OnKeyReleasedEventArgs args = new OnKeyReleasedEventArgs(keyReleased);
            // FIRE the event to listeners:
            KeyReleased(this, args);
        }

        /// <summary>
        /// Called when a mouse input occurs.
        /// </summary>
        /// <param name="mouseState">A Snapshot of the mouse state.</param>
        public virtual void OnNewMouseInput(MouseState mouseState, int scrollValue)
        {
            // STORE some information about the event and the mouse state:
            OnMouseInputEventArgs args = new OnMouseInputEventArgs(mouseState, scrollValue);
            // FIRE the event to listeners:
            NewMouseInput(this, args);
        }
        /// <summary>
        /// Default Update method for classes implementing IInputManager Interface.
        /// </summary>
        public void update()
        {
            // look for changes in input data
            this.CheckNewInput();
            this.CheckKeyReleased();
            this.CheckNewMouseInput();
        }
        #endregion

        #region IMPLEMENTATION OF IInputPublisher
        /// <summary>
        /// Allows a listener to Subscribe to the InputManager to watch for changes in input. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="subscriber">A reference to the object subscribing to events from InputManager.</param>
        /// <param name="inputHandler">The input handler to Subscribe to the event.</param>
        /// <param name="releaseHandler">The key release handler to Subscribe to the event.</param>
        /// <param name="mouseInputHandler">The mouse input handler to Subscribe to the event.</param>
        public void Subscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler, EventHandler<OnMouseInputEventArgs> mouseInputHandler)
        {
            // ADD the new subscriber to the subscribers List:
            subscribers.Add(subscriber);
            // ADD input event handler:
            NewInput += inputHandler;
            // ADD key released handler:
            KeyReleased += releaseHandler;
            // ADD mouse input handler:
            NewMouseInput += mouseInputHandler;
        }

        /// <summary>
        /// Allows a listener to unsubscribe from the InputManagers events.
        /// </summary>
        /// <param name="subscriber">A reference to the unsubscribing object of InputManager events.</param>
        /// <param name="inputHandler">The input handler to unsubscribe from the event.</param>
        /// <param name="releaseHandler">The key release handler to unsubscribe from the event.</param>
        /// <param name="mouseInputHandler">The mouse input handler to unsubscribe from the event."</param>
        /// 
        public void Unsubscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler, EventHandler<OnMouseInputEventArgs> mouseInputHandler)
        {
            // REMOVE the subscriber from the subscribers List:
            subscribers.Remove(subscriber);
            // UNSUBSCRIBE from the input event:
            NewInput -= inputHandler;
            // UNSUBSCRIBE from the key release event:
            KeyReleased -= releaseHandler;
            // // UNSUBSCRIBE from the mouse input event:
            NewMouseInput -= mouseInputHandler;
        }
        #endregion
    }
}
