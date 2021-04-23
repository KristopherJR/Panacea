using Microsoft.Xna.Framework;
using Panacea.Game_Code;
using System.Collections.Generic;

namespace Panacea.Engine_Code.Interfaces
{
    public interface INavigationManager
    {
        TileMap NavigationGrid { get; set; } // property
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
