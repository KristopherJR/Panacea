using Microsoft.Xna.Framework;
using Panacea.Engine_Code.Interfaces;
using Panacea.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code.Game_Entities.Characters
{
    class Mary : AnimatedEntity, ICollidable, ICollisionResponder, IPathFinder
    {
        #region FIELDS
        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of Mary's position and reset her if she collides with something:
        private Vector2 lastPosition;
        #endregion
        #region PROPERTIES
        public bool IsCollidable // property
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }
        #endregion

        public Mary() : base(GameContent.GetAnimation(AnimationGroup.MaryWalkDown))
        {
            // SET Mary's location in the world:
            this.EntityLocn = new Vector2(150,150);
            // SET her Velocity:
            this.entityVelocity = new Vector2(1, 1);
        }

        /// <summary>
        /// Update loop for Mary, overrides the parent Update() method. Stores her lastPosition before moving her on each update loop.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // UPDATE the parent class:
            base.Update(gameTime);
            // STORE Mary's last position as her current one before she moves:
            this.lastPosition = this.EntityLocn;
            // MOVE Mary by her velocity:
            this.EntityLocn += entityVelocity;
        }

        #region IMPLEMENTATION of ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            if (GameEntity.hasCollided(this, collidee))
            {
                entityLocn = lastPosition;
            }
        }
        #endregion
    }
}
