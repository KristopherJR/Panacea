using Microsoft.Xna.Framework;
using Panacea.Engine_Code.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Interfaces
{
    interface IEntity : IUpdatable
    {
        #region PROPERTIES
        int UID { get; set; }
        string UName { get; set; }
        #endregion
    }
}