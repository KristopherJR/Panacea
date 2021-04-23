using Panacea.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panacea.Managers
{
    class CollisionManager : ICollisionManager
    {
        #region FIELDS
        // DECLARE a new 'List' storing 'ICollidable' objects, call it 'collidables'. This will hold a reference to all objects in the SceneGraph implementing ICollidable (Balls and Paddles):
        private List<ICollidable> collidables;
        // DECLARE a new List storing ICollisionResponders, call it collisionGraph:
        private List<ICollisionResponder> collisionGraph;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class CollisionManager.
        /// </summary>
        public CollisionManager()
        {
            // INITALISE 'collidables':
            collidables = new List<ICollidable>();
            // INITALISE 'collisionGraph':
            collisionGraph = new List<ICollisionResponder>();
        }

        #region IMPLEMENTATION OF ICollisionManager
        /// <summary>
        /// Adds all ICollidables in the Scene Graph to the collidables List on start-up.
        /// </summary>
        public void PopulateCollidables(List<IEntity> sceneGraphCopy)
        {
            // ITERATE through the sceneGraphCopy:
            foreach (ICollidable c in sceneGraphCopy)
            {
                if (c.IsCollidable)
                {
                    // ADD each ICollidable in sceneGraphCopy to collidables:
                    collidables.Add(c);
                }
            }
            foreach (IEntity c in sceneGraphCopy)
            {
                if (c is ICollisionResponder)
                {
                    collisionGraph.Add(c as ICollisionResponder);
                }
            }
        }

        /// <summary>
        /// Remove the specified item matching the provided uName and uID from the collidables List. Usually called after an item has been
        /// terminated from the game.
        /// </summary>
        /// <param name="uName">The unique name of the object to be removed from collidables.</param>
        /// <param name="uID">The unique ID of the object to be removed from collidables.</param>
        public void removeCollidable(String uName, int uID)
        {
            // ITERATE through the collidables list:
            for (int i = 0; i < collidables.Count(); i++)
            {
                // CHECK the unique name and ID matches the provided parameters:
                if (((collidables[i] as Entity).UName == uName) & ((collidables[i] as Entity).UID == uID))
                {
                    // REMOVE the object from collidables List:
                    collidables.Remove(collidables[i]);
                    // BREAK out of the loop once the object has been found and removed:
                    break;
                }
            }
        }

        /// <summary>
        /// Add a new Collidable to the collidables List to check it for collisions.
        /// </summary>
        /// <param name="newCollidable">A new ICollidable object to add to the collidables List.</param>
        public void addCollidable(ICollidable newCollidable)
        {
            collidables.Add(newCollidable);
        }

        /// <summary>
        /// Iterate through the stored entities and check if a Collision has occured. React appropriately if a collision has occured.
        /// Method based on code by Marc Price.
        /// </summary>
        public void CheckEntityCollisions()
        {
            // CHECK for collisions in pairs:
            for (int i = 0; i < collidables.Count; i++)
            {
                for (int j = 0; j < collisionGraph.Count; j++)
                {
                    if (collisionGraph[j] != collidables[i])
                    {
                        // CALL CollisionResponse to check if 'i' collided with 'j'. If they did, respond appropriately:
                        collisionGraph[j].CheckAndRespond(collidables[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Default Update method for objects implementing the ICollisionManager interface.
        /// </summary>
        public void update()
        {
            // CALL 'CheckEntityCollisions()':
            this.CheckEntityCollisions();
        }
        #endregion
    }
}
