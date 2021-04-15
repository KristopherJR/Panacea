using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Engine_Code.Interfaces
{
    interface IUpdatable
    {
        /// <summary>
        /// Default update loop for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        void Update(GameTime gameTime);
    }
}
