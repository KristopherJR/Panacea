using Microsoft.Xna.Framework;
using Panacea.Game_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Engine_Code.Interfaces
{
    public interface INavigationManager
    {
        TileMap NavigationGrid {get; set; }
        List<IPathFinder> PathFinders { get; } // read-only property
        /// <summary>
        /// Adds a new IPathFinder to the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to add to the NavigationManager.</param>
        void AddPathFinder(IPathFinder pathFinder);
        /// <summary>
        /// Adds a new IPathFinder to the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to add to the NavigationManager.</param>
        void RemovePathFinder(IPathFinder pathFinder);
        /// <summary>
        /// Default Update loop.
        /// </summary>
        /// <param name="gameTime">A Snapshot of the GameTime.</param>
        void Update(GameTime gameTime);
    }
}
