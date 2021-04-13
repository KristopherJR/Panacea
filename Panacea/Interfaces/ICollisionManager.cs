using COMP2451Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2451Project.Interfaces
{
    interface ICollisionManager
    {
        /// <summary>
        /// Adds all ICollidables in the Scene Graph to the collidables List on start-up.
        /// </summary>
        void populateCollidables(List<IEntity> sceneGraphCopy);

        /// <summary>
        /// Remove the specified item matching the provided uName and uID from the collidables List. Usually called after an item has been
        /// terminated from the game.
        /// </summary>
        /// <param name="uName">The unique name of the object to be removed from collidables.</param>
        /// <param name="uID">The unique ID of the object to be removed from collidables.</param>
        void removeCollidable(String uName, int uID);

        /// <summary>
        /// Add a new Collidable to the collidables List to check it for collisions.
        /// </summary>
        /// <param name="newCollidable">A new ICollidable object to add to the collidables List.</param>
        void addCollidable(ICollidable newCollidable);

        /// <summary>
        /// Iterate through the stored entities and check if a Collision has occured. React appropriately if a collision has occured.
        /// </summary>
        void checkEntityCollisions();

        /// <summary>
        /// Default update method for objects implementing the ICollisionManager interface.
        /// </summary>
        void update();
    }
}
