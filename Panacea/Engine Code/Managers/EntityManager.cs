using Panacea.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Managers
{
    class EntityManager : IEntityManager
    {
        #region FIELDS
        // DECLARE a 'List' to contain all objects of type 'IEntity', this 'List' contains a reference to ALL entityPool in the program:
        private List<IEntity> entityPool;
        // DECLARE an int, call it 'idCounter':
        private int idCounter;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class EntityManager.
        /// </summary>
        public EntityManager()
        {
            // INITIALIZE the fields:
            entityPool = new List<IEntity>();
            idCounter = 1;
        }

        #region IMPLEMENTATION OF IEntityManager
        /// <summary>
        /// Create an entity of the provided generic type (must be an IEntity).
        /// </summary>
        /// <typeparam name="T">The Generic type to be created.</typeparam>
        /// <returns></returns>
        public IEntity createEntity<T>() where T : IEntity, new()
        {
            // CREATE a new object of the generic type provided:
            IEntity newEntity = new T();
            // SET a unique id:
            newEntity.UID = idCounter;
            // INCREMENT the idCounter:
            idCounter++;
            // STORE that new 'Ball' in the entityPool reference List:
            entityPool.Add(newEntity);
            // SET a unique name:
            this.setUniqueName(newEntity);
            // RETURN the new 'Ball' object to the caller as an 'IEntity':
            return newEntity;
        }

        /// <summary>
        /// Destroy an entity by removing it from the Entity Pool.
        /// </summary>
        /// <param name="UName">The Unique Name of the entity to be destroyed.</param>
        /// <param name="UID">The Unique ID of the entity to be destroyed.</param>
        public void destroyEntity(String UName, int UID)
        {
            // DECLARE a temporary int to store the index of the object to destroy:
            int temp = 0;
            // ITERATE through the 'entityPool':
            for (int i = 0; i < entityPool.Count; i++)
            {
                // CHECK if the entity UName matches the provided String or if the entity UID matches the provided int:
                if (entityPool[i].UName == UName || (entityPool[i].UID == UID))
                {
                    // STORE the index of the item to remove in a temporary int:
                    temp = i;
                }
            }
            // REMOVE the entity from the 'sceneGraph':
            entityPool.RemoveAt(temp);
        }
        #endregion

        private void setUniqueName(IEntity e)
        {
            // DECLARE an int called 'tempCounter' and set it to '0':
            int tempCounter = 0;

            // CHECK if the IEntity passed in is a 'Ball' object:
            if (e is Ball)
            {
                // STEP-THROUGH the 'entityPool' List for each 'Ball':
                for(int i=0;i<entityPool.Count;i++)
                {
                    if (entityPool[i] is Ball)
                    {
                        // INCREASE the 'tempCounter' for each 'Ball':
                        tempCounter++;
                    }
                }
                // STEP-THROUGH the 'entityPool' List again for each 'Ball':
                for(int i= 0; i < entityPool.Count; i++)
                {
                    if(entityPool[i] is Ball)
                    {
                        // CHECK that the name isn't already being used:
                        if (entityPool[i].UName == ("Ball" + tempCounter))
                        {
                            // IF the name is being used, increase the counter again:
                            tempCounter++;
                        }
                    }  
                }
                // SET the entityPool name to "Ball" plus the 'tempCounter':
                e.UName = ("Ball" + tempCounter);
            }

            // CHECK if the IEntity passed in is a 'Paddle' object:
            if (e is Paddle)
            {
                // STEP-THROUGH the 'entityPool' List for each 'Paddle':
                for (int i = 0; i < entityPool.Count; i++)
                {
                    if (entityPool[i] is Paddle)
                    {
                        // INCREASE the 'tempCounter' for each 'Paddle':
                        tempCounter++;
                    }
                }
                // STEP-THROUGH the 'entityPool' List again for each 'Paddle':
                for (int i = 0; i < entityPool.Count; i++)
                {
                    if (entityPool[i] is Paddle)
                    {
                        // CHECK that the name isn't already being used:
                        if (entityPool[i].UName == ("Paddle" + tempCounter))
                        {
                            // IF the name is being used, increase the counter again:
                            tempCounter++;
                        }
                    }
                }
                // SET the entityPool name to "Paddle" plus the 'tempCounter':
                e.UName = ("Paddle" + tempCounter);
            }
        }
    }
}
