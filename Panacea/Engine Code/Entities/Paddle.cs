using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panacea.InputClasses;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Panacea
{
    public class Paddle : PongEntity, ICollidable, ICollisionResponder, IInputListener
    {
        #region FIELDS
        // DECLARE a Texture2D, call it 'paddleTexture':
        private Texture2D paddleTexture;
        // DECLARE an array of Keys[] called keysOfInterest. This will contain only the keys that we need to know about being pressed:
        private Keys[] keysOfInterest = { Keys.W, Keys.S, Keys.Up, Keys.Down };
        #endregion

        #region PROPERTIES
        #endregion
        /// <summary>
        /// Constructor for objects of class Paddle.
        /// </summary>
        public Paddle()
        {
            // SET the 'paddleTexture' of 'Paddle' to the static texture contained in Kernel:
            paddleTexture = Kernel.paddleTexture;
            // SET 'EntityTexture' to 'paddleTexture':
            this.EntityTexture = paddleTexture;
        }

        /// <summary>
        /// CHECKS if the paddle has hit the roof or floor and stop it from moving.
        /// </summary>
        /// <returns>True if the Paddle has hit the roof or floor, else false.</returns>
        public Boolean HitRoofOrFloor()
        {
            // CHECK if the paddle has hit the floor:
            if (this.EntityLocn.Y > Kernel.SCREEN_HEIGHT - this.EntityTexture.Height)
            {
                // RESET the paddle to just above the floor so the paddle doesn't get stuck:
                this.EntityLocn = new Vector2(this.EntityLocn.X, (Kernel.SCREEN_HEIGHT - this.EntityTexture.Height) - 1);
                // RETURN true:
                return true;
            }
            // CHECK if the paddle has hit the roof:
            else if(this.EntityLocn.Y < 0)
            {
                // RESET the paddle to just below the roof so the paddle doesn't get stuck:
                this.EntityLocn = new Vector2(this.EntityLocn.X, 1);
                // RETURN true:
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The default update method for a 'Paddle':
        /// </summary>
        public void Update()
        {
            // MOVE the Paddle by its velocity:
            base.EntityLocn += velocity;
            // RESET velocity after each pass of the simulation:
            this.Velocity = new Vector2(0, 0);
        }

        #region IMPLEMENTATION OF ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            if (this.HitRoofOrFloor())
            {
                this.Velocity = new Vector2(0, 0);
            }    
        }
        #endregion

        #region IMPLEMENTATION OF IInputListener
        /// <summary>
        /// Event Handler for the event OnNewInput, fired from the InputManager. This will be triggered when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        public virtual void OnNewInput(object sender, OnInputEventArgs eventInformation)
        {
            //Respond to new input:
            switch (eventInformation.KeyInput)
            {
                case Keys.W:
                    if (this.UName == "Paddle1")
                    {
                        this.Velocity = new Vector2(0, -5);
                    }
                    break;
                case Keys.S:
                    if (this.UName == "Paddle1")
                    {
                        this.Velocity = new Vector2(0, 5);
                    }
                    break;
                case Keys.Up:
                    if (this.UName == "Paddle2")
                    {
                        this.Velocity = new Vector2(0, -5);
                    }
                    break;
                case Keys.Down:
                    if (this.UName == "Paddle2")
                    {
                        this.Velocity = new Vector2(0, 5);
                    }
                    break;
            }
        }

        /// <summary>
        /// Event Handler for the event OnKeyReleased, fired from the InputManager. This will be triggered when a key is released.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        public virtual void OnKeyReleased(object sender, OnKeyReleasedEventArgs eventInformation)
        {
            //Respond to new input:
            switch (eventInformation.KeyReleased)
            {
                case Keys.W:
                    if (this.UName == "Paddle1")
                    {
                        this.Velocity = new Vector2(0, 0);
                    }
                    break;
                case Keys.S:
                    if (this.UName == "Paddle1")
                    {
                        this.Velocity = new Vector2(0, 0);
                    }
                    break;
                case Keys.Up:
                    if (this.UName == "Paddle2")
                    {
                        this.Velocity = new Vector2(0, 0);
                    }
                    break;
                case Keys.Down:
                    if (this.UName == "Paddle2")
                    {
                        this.Velocity = new Vector2(0, 0);
                    }
                    break;
            }
        }

        /// <summary>
        /// Used to return the KeysOfInterst contained in the Listener.
        /// </summary>
        /// <returns>The array of KeysOfInterest.</returns>
        public Keys[] getKOI()
        {
            // return keysOfInterest:
            return keysOfInterest;
        }
        #endregion
    }
}
