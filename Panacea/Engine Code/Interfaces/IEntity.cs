using Panacea.Engine_Code.Interfaces;

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