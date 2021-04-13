using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.UserEventArgs
{
    public class OnEntityTerminationEventArgs : EventArgs
    {
        #region FIELDS
        // DECLARE a String to contain the Unique Name of the Entity to be terminated:
        private String entityUName;
        // DECLARE an int to contain the Unique ID of the Entity to be terminated:
        private int entityUID;
        #endregion

        #region PROPERTIES
        public String EntityUName // property
        {
            get { return entityUName; } // get method
            set { entityUName = value; } // set method
        }
        public int EntityUID // property
        {
            get { return entityUID; } // get method
            set { entityUID = value; } // set method
        }
        #endregion

        /// <summary>
        /// Constructor for OnEntityTerminationEventArgs objects.
        /// </summary>
        /// <param name="eName">The Unique Name of the Entity to be terminated.</param>
        /// <param name="eID">The Unique ID of the Entity to be terminated.</param>
        public OnEntityTerminationEventArgs(String eName, int eID)
        {
            // SET 'entityUName' to the provided String:
            entityUName = eName;
            // SET 'entityUID' to the provided int:
            entityUID = eID;
        }   
    }
}
