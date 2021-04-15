using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.UserEventArgs;
using Panacea.Game_Code.Game_Entities;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using System;

namespace Panacea
{
    public class Sam : AnimatedEntity, ICollidable, ICollisionResponder, IInputListener
    {
        #region FIELDS
        // DECLARE a float, call it 'moveSpeed':
        private float moveSpeed;
        // DECLARE an event, call it 'OnEntityTermination':
        public event EventHandler<OnEntityTerminationEventArgs> OnEntityTermination;
        // DECLARE an array of Keys[] called keysOfInterest. This will contain only the keys that we need to know about being pressed:
        private Keys[] keysOfInterest = { Keys.W, Keys.A, Keys.S, Keys.D };
        #endregion

        #region PROPERTIES
        
        #endregion

        /// <summary>
        /// Constructor for objects of class Sam.
        /// </summary>
        public Sam() : base(GameContent.GetAnimation(AnimationGroup.SamWalkDown))
        {
            //SET the Sam Object calling the method to the centre of the screen:
            this.EntityLocn = new Vector2((Kernel.SCREEN_WIDTH / 2 - this.EntitySprite.TextureWidth / 2),
                                          (Kernel.SCREEN_HEIGHT / 2 - this.EntitySprite.TextureHeight / 2));
            // INITIALIZE moveSpeed to '5':
            this.moveSpeed = 5;
        }

        /// <summary>
        /// ADVANCES the ball one frame by its X and Y speed.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // MOVE the ball by it's X and Y speed:
            this.EntityLocn += entityVelocity;
        }


        #region IMPLEMENTATION OF ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
          
               
         //OnEntityTermination?.Invoke(this, new OnEntityTerminationEventArgs(this.UName, this.UID));

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
            // RESPOND to new input, checking which key was pressed by the user:
            switch (eventInformation.KeyInput)
            {
                case Keys.W:
                    // MOVE player UP by movespeed:
                    this.EntityVelocity = new Vector2(0,-moveSpeed);
                    // SET Sams entityAnimation to walking UP:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkUp);
                    break;
                case Keys.A:
                    // MOVE player LEFT by movespeed:
                    this.EntityVelocity = new Vector2(-moveSpeed, 0);
                    // SET Sams entityAnimation to walking LEFT:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkLeft);
                    break;
                case Keys.S:
                    // MOVE player RIGHT by movespeed:
                    this.EntityVelocity = new Vector2(0, moveSpeed);
                    // SET Sams entityAnimation to walking RIGHT:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkDown);
                    break;
                case Keys.D:
                    // MOVE player DOWN by movespeed:
                    this.EntityVelocity = new Vector2(moveSpeed, 0);
                    // SET Sams entityAnimation to walking DOWN:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkRight);
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
            // RESPOND to new input, checking which key was released by the user:
            switch (eventInformation.KeyReleased)
            {
                case Keys.W:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0,0);
                    break;
                case Keys.A:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0,0);
                    break;
                case Keys.S:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0,0);
                    break;
                case Keys.D:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0,0);
                    break;
            }
        }

        /// <summary>
        /// Event Handler for the even OnNewMouseInput, fired from the InputManager. This will be triggered when a new mouse input occurs.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        public virtual void OnNewMouseInput(object sender, OnMouseInputEventArgs eventInformation)
        {
            //Respond to the new mouse input:
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
