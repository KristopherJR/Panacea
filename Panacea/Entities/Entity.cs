using COMP2451Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2451Project
{
    public abstract class Entity : IEntity
    {
        #region FIELDS
        // DECLARE an int, call it 'uID':
        private int uID;
        // DECLARE a string, call it 'uName':
        private string uName;
        #endregion

        #region PROPERTIES
        public int UID
        {
            get { return uID; }
            set { uID = value; }
        }
        public string UName
        {
            get { return uName; }
            set { uName = value; }
        }
        #endregion

        public Entity()
        {
            // do nothing
        }
        public virtual void update()
        {
            // do nothing
        }
    }
}
