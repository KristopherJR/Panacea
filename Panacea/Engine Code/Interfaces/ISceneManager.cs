using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Interfaces
{
    interface ISceneManager
    {
        /// <summary>
        /// Add an object of type 'IEntity' to the 'sceneGraph'. The entity should be provided by the Kernel.
        /// </summary>
        /// <param name="e">An object of type IEntity to be added to the Scene Graph.</param>
        void spawn(IEntity e);

        /// <summary>
        /// Removes an object from the Scene Graph. The object can be specified by either its unique name or unique id number.
        /// </summary>
        /// <param name="UName">The Unique Name of the entity to be removed from the Scene Graph.</param>
        /// <param name="UID">The Unique ID of the entity to be removed from the Scene Graph.</param>
        void despawn(String UName, int UID);

        /// <summary>
        /// Default update method for objects implementing the ISceneManager interface.
        /// </summary>
        void update();
    }
}
