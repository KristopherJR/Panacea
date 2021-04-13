﻿using Panacea.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Panacea.Managers
{
    class SceneManager : ISceneManager
    {
        #region FIELDS
        // DECLARE a new 'List' storing 'IEntity' objects, call it 'sceneGraph':
        private List<IEntity> sceneGraph;
        #endregion

        #region PROPERTIES
        public List<IEntity> SceneGraph // read-only property
        {
            get { return sceneGraph; } // get method
        }
        #endregion

        /// <summary>
        /// Constructor for objects of class SceneManager.
        /// </summary>
        public SceneManager()
        {
            // INITIALIZE fields:
            sceneGraph = new List<IEntity>();
        }

        #region IMPLEMENTATION OF ISceneManager
        /// <summary>
        /// Add an object of type 'IEntity' to the 'sceneGraph'. The entity should be provided by the Kernel.
        /// </summary>
        /// <param name="e">An object of type IEntity to be added to the Scene Graph.</param>
        public void spawn(IEntity e)
        {
            // ADD the provided IEntity to the scene graph:
            sceneGraph.Add(e);
        }

        /// <summary>
        /// Removes an object from the Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="UName">The Unique Name of the entity to be removed from the Scene Graph.</param>
        /// <param name="UID">The Unique ID of the entity to be removed from the Scene Graph.</param>
        public void despawn(string UName, int UID)
        {
            // DECLARE a temporary int to store the index of the object to despawn:
            int temp = 0;
            // ITERATE through the 'sceneGraph':
            for (int i = 0; i < sceneGraph.Count; i++)
            {
                // CHECK if the entity UName matches the provided String or if the entity UID matches the provided int:
                if (sceneGraph[i].UName == UName || (sceneGraph[i].UID == UID))
                {
                    // STORE the index of the item to remove in a temporary int:
                    temp = i;
                }
            }
            // REMOVE the entity from the 'sceneGraph':
            sceneGraph.RemoveAt(temp);
        }

        /// <summary>
        /// METHOD: Calls the update method of each entity in the sceneGraph to make them move.
        /// </summary>
        private void moveEntities()
        {  
            // ITERATE through the 'sceneGraphCopy':
            foreach (IEntity entity in sceneGraph)
            {
                // IF the object in the List is a 'Sam':
                if (entity is Sam)
                {
                    // MOVE the ball by it's X and Y speed:
                    (entity as Sam).update();
                }

                // IF the current Object is a Paddle:
                if (entity is Paddle)
                {
                    // MOVE the paddle:
                    (entity as Paddle).Update();
                }
            }
        }

        /// <summary>
        /// Default update method for objects implementing the ISceneManager interface.
        /// </summary>
        public void update()
        {
            // CALL the moveEntities() method:
            this.moveEntities();
        }
        #endregion
    }
}
