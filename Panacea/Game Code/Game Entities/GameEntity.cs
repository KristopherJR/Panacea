using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panacea.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panacea.Game_Code.Game_Entities;

namespace Panacea
{
    public abstract class GameEntity : Entity
    {
        #region FIELDS
        // DECLARE a reference to a Sprite object, call it "entitySprite". This is used to store graphical information about the entity as a Sprite:
        protected Sprite entitySprite;
        // DECLARE a reference to a Vector2 object, call it "entityLocn". This is used to store the GameEntities Location:
        private Vector2 entityLocn;
        // DECLARE a reference to a Vector2 object, call it "velocity". This is used to represent velocity values of PongEntities:
        protected Vector2 velocity;

        #endregion

        #region PROPERTIES
        public Rectangle HitBox // property
        {
            get { return new Rectangle((int)this.entityLocn.X, (int)this.EntityLocn.Y, entitySprite.SpriteTexture.Width, entitySprite.SpriteTexture.Height); } //HitBox returns an appropriately sized hit box for the entity calling it, based on the entityPool location and size at the time of calling.
        }

        public Sprite EntitySprite // property
        {
            get { return entitySprite; } // get method
            set { entitySprite = value; } // set method
        }

        public Vector2 EntityLocn
        {
            get { return entityLocn; } // get method
            set { entityLocn = value; } // set method
        }

        public Vector2 Velocity
        {
            get { return velocity; } // get method
            set { velocity = value; } // set method
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class GameEntity.
        /// </summary>
        public GameEntity()
        {
            // INITALIZE entityLocn to default 0,0:
            this.entityLocn = new Vector2(0,0);
        }

        /// <summary>
        /// Called from Kernel, tells the GameEntity to draw itself onto the spriteBatch parameter passed in.
        /// </summary>
        /// <param name="spriteBatch">The games SpriteBatch.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            entitySprite.Draw(spriteBatch, entityLocn);
        }

        /// <summary>
        /// Checks if 2 ICollidables have collided. Returns true if they have, else false.
        /// </summary>
        /// <param name="collider">First ICollidable.</param>
        /// <param name="colidee">Second ICollidable.</param>
        /// <returns></returns>
        public static Boolean hasCollided(ICollidable collider, ICollidable colidee)
        {
            // IF the ICollidables HitBox's intersect:
            if ((collider as GameEntity).HitBox.Intersects((colidee as GameEntity).HitBox))
            {
                // RETURN true:
                return true;
            }
            else
            {
                // ELSE return false:
                return false;
            }
        }
    }
}
