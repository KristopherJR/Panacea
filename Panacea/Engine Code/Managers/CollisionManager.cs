using Panacea.Interfaces;
using Panacea.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Managers
{
    class CollisionManager : ICollisionManager
    {
        #region FIELDS
        // DECLARE a new 'List' storing 'ICollidable' objects, call it 'collidables'. This will hold a reference to all objects in the SceneGraph implementing ICollidable (Balls and Paddles):
        private List<ICollidable> collidables;
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
        }

        #region IMPLEMENTATION OF ICollisionManager
        /// <summary>
        /// Adds all ICollidables in the Scene Graph to the collidables List on start-up.
        /// </summary>
        public void populateCollidables(List<IEntity> sceneGraphCopy)
        {
            // ITERATE through the sceneGraphCopy:
            foreach(ICollidable c in sceneGraphCopy)
            {
                // ADD each ICollidable in sceneGraphCopy to collidables:
                collidables.Add(c);
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
        public void checkEntityCollisions()
        {
            // CHECK for collisions in pairs:
            for(int i=0; i < collidables.Count() -1; i++)
            {
                for (int j = i + 1; j < collidables.Count(); j++)
                {
                    // CHECK that the collider is an ICollisionResponder:
                    if (collidables[i] is ICollisionResponder)
                    {
                        // CALL CollisionResponse to check if 'i' collided with 'j'. If they did, respond appropriately:
                        (collidables[i] as ICollisionResponder).CheckAndRespond(collidables[j]);
                        
                    }
                    // NOTE: Not as redundant as it may seem. Functionality is broken without this second check:
                    if (collidables[j] is ICollisionResponder)
                    {
                        // CALL CollisionResponse to check if 'j' collided with 'i'. If they did, respond appropriately:
                        (collidables[j] as ICollisionResponder).CheckAndRespond(collidables[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Default update method for objects implementing the ICollisionManager interface.
        /// </summary>
        public void update()
        {
            // CALL 'checkEntityCollisions()':
            this.checkEntityCollisions();
        }
        #endregion
    }
}
