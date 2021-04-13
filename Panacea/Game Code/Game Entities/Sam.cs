using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Panacea
{
    public class Sam : GameEntity, ICollidable, ICollisionResponder
    {
        #region FIELDS
        // DECLARE a Texture2D, call it 'ballTexture':
        private Texture2D ballTexture;
        // DECLARE a float, call it 'mSpeed':
        private float mSpeed;
        // DECLARE a Random, call it 'random':
        private Random random;
        // DECLARE an event, call it 'OnEntityTermination':
        public event EventHandler<OnEntityTerminationEventArgs> OnEntityTermination;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class Sam.
        /// </summary>
        public Sam()
        {
            // SET the 'ballTexture' of 'Sam' to the static texture contained in Kernel:
            ballTexture = Kernel.ballTexture;
            // SET the EntityTexture to 'ballTexture':
            this.EntityTexture = ballTexture;
            // INITIALIZE the RNG:
            this.random = new Random();
            // INITIALIZE mSpeed to '8':
            this.mSpeed = 8;
        }

        /// <summary>
        /// CHECKS if a Sam has hit the left or right wall. If it has, return true - else false.
        /// </summary>
        /// <returns>True if out of play.</returns>
        public Boolean GoneOutOfPlay()
        {
            // CHECK if the 'Sam' has hit the Right or Left wall:
            if ((this.EntityLocn.X > Kernel.SCREEN_WIDTH - this.EntityTexture.Width) || (this.EntityLocn.X < 0))
            {
                // RETURN True:
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// CHECKS if a Sam has hit the roof or floor. If it has, return true - else false.
        /// </summary>
        /// <returns>True if the Sam hit the Roof or Floor.</returns>
        public Boolean HitRoofOrFloor()
        {
            // CHECK if the 'Sam' has hit the floor:
            if ((this.EntityLocn.Y > Kernel.SCREEN_HEIGHT - this.EntityTexture.Height))
            {
                // SET it to above the floor so that it doesn't get stuck in a loop:
                this.EntityLocn = new Vector2(this.EntityLocn.X, Kernel.SCREEN_HEIGHT - this.EntityTexture.Height);
                return true;
            }
            // CHECK if the 'Sam' has hit the roof:
            else if ((this.EntityLocn.Y < 0))
            {
                // SET it to the edge of the roof so it doesn't get stuck in a collision check loop:
                this.EntityLocn = new Vector2(this.EntityLocn.X, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ADVANCES the ball one frame by its X and Y speed.
        /// </summary>
        public override void update()
        {
            // MOVE the ball by it's X and Y speed:
            this.EntityLocn += velocity;
        }

        /// <summary>
        /// IMPLEMENTS functionality to 'serve' the ball. Will set the ball to the centre of the screen when called.
        /// </summary>
        public void Serve()
        {   
            { 
                //SET the Sam Object calling the method to the centre of the screen:
                this.EntityLocn = new Vector2((Kernel.SCREEN_WIDTH / 2 - this.EntityTexture.Width / 2), (Kernel.SCREEN_HEIGHT / 2 - this.EntityTexture.Height / 2));

                //GENERATE a random number used to send the ball in a random direction:
                float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

                //SET the XSpeed and YSpeed to a random number using 'rotation':
                this.velocity.X = (float)Math.Sin(rotation);
                this.velocity.Y = (float)Math.Cos(rotation);

                //MAKE the ball have an equal (50/50) chance to launch left or right:
                if (random.Next(1, 3) == 2) //random number which will always be 1 or 2.
                {
                    //INVERT the velocity:
                    this.velocity = -this.velocity;
                }

                //MULTIPLY the velocity by the mSpeed:
                velocity *= mSpeed;
            }
        }

        #region IMPLEMENTATION OF ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            if (collidee is Paddle)
            {
                // CHECK if this ball has hit a Paddle:
                if (GameEntity.hasCollided(this, collidee))
                {
                    // BOUNCE the ball off the Paddle:
                    this.Velocity = new Vector2((-this.Velocity.X) * SPIN, (this.Velocity.Y) * SPIN);
                }
            }
            // IF this ball has gone out of play:
            if(this.GoneOutOfPlay())
            {
                // BROADCAST the event OnEntityTermination to its subscribers, signifying a Sam has gone out of play and should be removed from the game.
                OnEntityTermination?.Invoke(this, new OnEntityTerminationEventArgs(this.UName, this.UID));
            } 
            // IF this ball has hit the roof or floor:
            if(this.HitRoofOrFloor())
            {
                // SEND the 'Sam' the other way on its Y Axis:
                this.velocity.Y = -this.velocity.Y;
            }
        }
        #endregion
    }
}
