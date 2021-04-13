using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2451Project.Interfaces
{
    interface IEntity
    {
        #region PROPERTIES
        int UID { get; set; }
        string UName { get; set; }
        #endregion

        /// <summary>
        /// Update method for objects implementing the IEntity interface.
        /// </summary>
        void update();
    }
}