using Microsoft.Xna.Framework;
using Panacea.Interfaces;

namespace Panacea
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

        /// <summary>
        /// Default Update loop of Entity.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public virtual void Update(GameTime gameTime)
        {
            // do nothing
        }
    }
}
