using System;

namespace Panacea.Interfaces
{
    interface IEntityManager
    {
        /// <summary>
        /// Create an entity of the provided generic type (must be an IEntity).
        /// </summary>
        /// <typeparam name="T">The Generic type to be created.</typeparam>
        /// <returns></returns>
        IEntity createEntity<T>() where T : IEntity, new();

        /// <summary>
        /// Destroy an entity by removing it from the entity list.
        /// </summary>
        /// <param name="UName">The Unique Name of the entity to be destroyed.</param>
        /// <param name="UID">The Unique ID of the entity to be destroyed.</param>
        void destroyEntity(String UName, int UID);
    }
}
