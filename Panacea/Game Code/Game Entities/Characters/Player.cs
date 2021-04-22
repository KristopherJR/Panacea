using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.UserEventArgs;
using Panacea.Game_Code.Game_Entities;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using System;

namespace Panacea
{
    public class Player : AnimatedEntity, ICollidable, ICollisionResponder, IInputListener
    {
        #region FIELDS
        // DECLARE a float, call it 'moveSpeed':
        private float moveSpeed;
        // DECLARE an event, call it 'OnEntityTermination':
        public event EventHandler<OnEntityTerminationEventArgs> OnEntityTermination;
        // DECLARE an array of Keys[] called keysOfInterest. This will contain only the keys that we need to know about being pressed:
        private Keys[] keysOfInterest = { Keys.W, Keys.A, Keys.S, Keys.D, Keys.LeftShift };
        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of Sams position and reset him if he collides with something:
        private Vector2 lastPosition;
        // DECLARE a bool, call it 'isSprintEnabled'. Used to toggle between Sprint mode:
        private bool isSprintEnabled;
        // DECLARE a bool, call it 'isSprintReleased'. Used to flag when the user lets go off sprint:
        private bool isSprintReleased;
        #endregion

        #region PROPERTIES
        public bool IsCollidable // property
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class Player.
        /// </summary>
        public Player() : base(GameContent.GetAnimation(AnimationGroup.SamWalkDown))
        {
            // SET Sams location in the world:
            this.EntityLocn = new Vector2(100,100);
            // INITIALIZE moveSpeed to '1.5f':
            this.moveSpeed = 1.5f;
            // SET isSprintEnabled to false as default:
            this.isSprintEnabled = false;
            // SET isSprintReleased to true as default:
            this.isSprintReleased = true;
            // SET isCharacter to true:
            this.isCharacter = true;
        }

        /// <summary>
        /// Update loop for Player, overrides the parent Update() method. Stores his lastPosition before moving him on each update loop.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // UPDATE the parent class:
            base.Update(gameTime);
            // STORE Sams last position as his current one before he moves:
            lastPosition = EntityLocn;
            // MOVE Player by his velocity:
            this.EntityLocn += entityVelocity;
        }

        #region IMPLEMENTATION OF ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            if(GameEntity.hasCollided(this,collidee))
            {
                entityLocn = lastPosition;
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
            // RESPOND to new input, checking which key was pressed by the user:
            switch (eventInformation.KeyInput)
            {
                case Keys.W:
                    // MOVE player UP by movespeed:
                    this.EntityVelocity = new Vector2(0,-moveSpeed);
                    // IF the player is sprinting:
                    if(isSprintEnabled)
                    {
                        // SET Sams animation to sprint UP:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamSprintUp);
                        break;
                    }
                    // SET Sams entityAnimation to walking UP:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkUp);
                    break;
                case Keys.A:
                    // MOVE player LEFT by movespeed:
                    this.EntityVelocity = new Vector2(-moveSpeed, 0);
                    // IF the player is sprinting:
                    if (isSprintEnabled)
                    {
                        // SET Sams animation to sprint LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamSprintLeft);
                        break;
                    }
                    // SET Sams entityAnimation to walking LEFT:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkLeft);
                    break;
                case Keys.S:
                    // MOVE player RIGHT by movespeed:
                    this.EntityVelocity = new Vector2(0, moveSpeed);
                    // IF the player is sprinting:
                    if (isSprintEnabled)
                    {
                        // SET Sams animation to sprint DOWN:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamSprintDown);
                        break;
                    }
                    // SET Sams entityAnimation to walking DOWN:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkDown);
                    break;
                case Keys.D:
                    // MOVE player DOWN by movespeed:
                    this.EntityVelocity = new Vector2(moveSpeed, 0);
                    // IF the player is sprinting:
                    if (isSprintEnabled)
                    {
                        // SET Sams animation to sprint RIGHT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamSprintRight);
                        break;
                    }
                    // SET Sams entityAnimation to walking RIGHT:
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.SamWalkRight);
                    break;
                case Keys.LeftShift:
                    // IF sprint is off, the user is trying to turn it on. Only turn it off once they let go of shift:
                    if(isSprintEnabled == false && isSprintReleased == true)
                    {
                        // INCREASE Sams speed by 50%:
                        this.moveSpeed *= 1.5f;
                        // FLAG that he is now sprinting:
                        this.isSprintEnabled = true;
                        // FLAG that sprint hasn't been released:
                        this.isSprintReleased = false;
                        break;
                    }
                    // IF sprint is on, the user is trying to turn it off. Only turn it off once they let go of shift:
                    if (isSprintEnabled == true && isSprintReleased == true)
                    {
                        // DECREASE Sams speed by 50%:
                        this.moveSpeed /= 1.5f;
                        // FLAG that he is not sprinting anymore:
                        this.isSprintEnabled = false;
                        // FLAG that sprint hasn't been released:
                        this.isSprintReleased = false;
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
                case Keys.LeftShift:
                    // FLAG the player has released sprint key:
                    this.isSprintReleased = true;
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
