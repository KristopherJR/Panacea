using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP2451Project.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace COMP2451Project
{
    public abstract class PongEntity : Entity
    {
        #region FIELDS
        // DECLARE a public static float called "SPIN", set it to "1.05f". This is a value added to the velocity of a 'Ball' when it collides with a 'Paddle':
        public static float SPIN = 1.05f;
        // DECLARE a reference to a Texture2D object, call it "entityTexture". This will contain the .PNG graphic file:
        private Texture2D entityTexture;
        // DECLARE a reference to a Vector2 object, call it "entityLocn". This is used to store the PongEntities Location:
        private Vector2 entityLocn;
        // DECLARE a reference to a Vector2 object, call it "velocity". This is used to represent velocity values of PongEntities:
        protected Vector2 velocity;
        #endregion

        #region PROPERTIES
        public Rectangle HitBox // property
        {
            get { return new Rectangle((int)this.entityLocn.X, (int)this.EntityLocn.Y, this.entityTexture.Width, this.entityTexture.Height); } //HitBox returns an appropriately sized hit box for the entity calling it, based on the entityPool location and size at the time of calling.
        }

        public Texture2D EntityTexture // property
        {
            get { return entityTexture; } // get method
            set { entityTexture = value; } // set method
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
        /// Constructor for objects of class PongEntity.
        /// </summary>
        public PongEntity()
        {
            // INITALIZE entityLocn to default 0,0:
            this.entityLocn = new Vector2(0,0);
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
            if ((collider as PongEntity).HitBox.Intersects((colidee as PongEntity).HitBox))
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
