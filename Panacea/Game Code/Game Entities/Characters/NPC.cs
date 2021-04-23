using Microsoft.Xna.Framework;
using Panacea.Engine_Code.Interfaces;
using Panacea.Interfaces;
using System.Collections.Generic;

namespace Panacea.Game_Code.Game_Entities.Characters
{
    class NPC : AnimatedEntity, ICollidable, ICollisionResponder, IPathFinder
    {
        #region FIELDS
        // DECLARE a List<Vector2>, call it path. This will contain the NPCs randomly calculate Path:
        private List<Vector2> path;
        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of the NPC's position and reset it if it collides with something:
        private Vector2 lastPosition;
        // DECLARE a int, call it i:
        private int i;
        // DECLARE a float, call it speed:
        private float speed;
        // DECLARE a float, call it idleTimer:
        private float idleTimer;
        // DECLARE a float, call it waitDuration:
        private float waitDuration;
        // DECLARE a bool, call it isWaiting:
        private bool isWaiting;
        #endregion

        #region PROPERTIES
        public List<Vector2> Path // property
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

        /// <summary>
        /// Constructor for objects of class NPC.
        /// </summary>
        public NPC() : base(GameContent.GetAnimation(AnimationGroup.MaryWalkDown))
        {
            // INITIALIZE fields:
            this.path = new List<Vector2>();
            this.speed = 1;
            this.i = 0;
            this.idleTimer = 0.0f;
            this.waitDuration = 5.0f;
            this.isWaiting = false;

            // SET NPC's location in the world:
            this.EntityLocn = new Vector2(150, 150);
        }

        /// <summary>
        /// Method that allows the NPC to move along their generated Path.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        private void FollowPath(GameTime gameTime)
        {
            // IF i < path.Count:
            if (i < path.Count)
            {
                // DECLARE a Vector2, call it location and set it to the Entities current location:
                Vector2 location = this.EntityLocn;
                // DECLARE a Vector2, call it direction and set it to the Entities current location - path[i]:
                Vector2 direction = path[i] - this.EntityLocn;
                // IF the direction is greater than 0
                if (direction.Length() > 0)
                {
                    // NORMALIZE the Vector or else it will be set to infinity!!!:
                    direction.Normalize();
                }
                // DECLARE a Vector2, call it newVelocity and set it to speed * direction:
                Vector2 newVelocity = direction * speed;
                // ADD the velocity to the entity location:
                this.EntityLocn += newVelocity;
                // IF the Distance between the entity location and the current tile in the path is less than 0.5 (I.E the entity is close enough to its goal tile):
                if (Vector2.Distance(this.entityLocn, path[i]) < 0.5)
                {
                    // INCREMENT i:
                    i++;
                }
                // IF the player has moved through the entire path:
                if (i == path.Count)
                {
                    // SET the entity velocity to 0:
                    entityVelocity = Vector2.Zero;
                    // SET their path to null, this will prompt the NavigationManager to give them a new one:
                    path = null;
                    // RESET i back to -:
                    i = 0;
                    // SET isWaiting to true so the entity waits before moving again:
                    isWaiting = true;
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
            // IF the Entity IS NOT waiting:
            if (isWaiting == false)
            {
                // FOLLOW its path:
                this.FollowPath(gameTime);
            }
            // IF the Entity IS waiting:
            if (isWaiting)
            {
                // INCREMENT the idleTimer by the elapsed GameTime in seconds:
                idleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // IF the idleTimer reaches the waitDuration:
                if (idleTimer >= waitDuration)
                {
                    // SET isWaiting back to false:
                    isWaiting = false;
                    // RESET the idleTimer:
                    idleTimer = 0.0f;
                }
            }

        }

        #region IMPLEMENTATION of ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            // IF the GameEntitys have collided:
            if (GameEntity.hasCollided(this, collidee))
            {
                // SET this location to its last position:
                this.entityLocn = lastPosition;
            }
        }
        #endregion
    }
}
