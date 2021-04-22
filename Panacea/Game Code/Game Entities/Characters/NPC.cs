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
    class NPC : AnimatedEntity, ICollidable, ICollisionResponder, IPathFinder
    {
        #region FIELDS
        private List<Vector2> path;
        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of the NPC's position and reset it if it collides with something:
        private Vector2 lastPosition;
        private int i;
        private float speed;
        private float progress;
        #endregion
        #region PROPERTIES
        public List<Vector2> Path
        {
            get { return path; }
            set { path = value; }
        }
        public bool IsCollidable // property
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }
        #endregion

        public NPC() : base(GameContent.GetAnimation(AnimationGroup.MaryWalkDown))
        {
            // INITIALIZE path:
            path = new List<Vector2>();
            speed = 1;
            i = 0;
            // SET NPC's location in the world:
            this.EntityLocn = new Vector2(150,150);
        }

        private void FollowPath(GameTime gameTime)
        {
            if(i < path.Count)
            {
                Vector2 newVelocity = this.entityLocn - path[i];
                newVelocity.Normalize();
                newVelocity *= speed;
                this.entityVelocity = newVelocity;
                this.EntityLocn = path[i];//entityVelocity;

                if (Vector2.Distance(this.entityLocn, path[i]) < 0.5)
                {
                    i++;
                }
                if(i == path.Count)
                {
                    path = null;
                    i = 0;
                }
            } 
        }

        /// <summary>
        /// Update loop for NPC's, overrides the parent Update() method. Stores the lastPosition before moving on each update loop.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // UPDATE the parent class:
            base.Update(gameTime);

            this.FollowPath(gameTime);
            
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
