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
        /// Calculates a destination tile from the NavigationGrid and returns the x,y location as a Vector2.
        /// </summary>
        /// <returns>A Vector2 with x,y values of the location to navigate to.</returns>
        Vector2 FindDestinationTile();
        /// <summary>
        /// Calculates the navigation path for the IPathFinder.
        /// </summary>
        /// <returns>The navigation path for the IPathFinder.</returns>
        Vector2 CalculateNavigationPath();
        /// <summary>
        /// Default Update loop.
        /// </summary>
        /// <param name="gameTime">A Snapshot of the GameTime.</param>
        void Update(GameTime gameTime);
    }
}
