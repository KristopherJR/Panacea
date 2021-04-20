using Microsoft.Xna.Framework;
using Panacea.Engine_Code.Interfaces;
using Panacea.Game_Code;
using System;
using System.Collections.Generic;

namespace Panacea.Engine_Code.Managers
{
    public class NavigationManager : INavigationManager
    {
        #region FIELDS
        // DECLARE a TileMap, call it navigationGrid. This will hold a map of all the tiles the entity can navigate to:
        private TileMap navigationGrid;
        // DECLARE a List<IPathFinder>, call it pathFinders. This will hold all A.I entities that navigate the world by themselves:
        private List<IPathFinder> pathFinders;
        // DECLARE a float, call it idleTimer. Used to make the entity wait before moving to the next tile:
        private float idleTimer;
        #endregion

        #region PROPERTIES
        public TileMap NavigationGrid
        {
            get { return navigationGrid; }
            set { navigationGrid = value; }
        }

        public List<IPathFinder> PathFinders // read-only property
        {
            get { return pathFinders; }
        }
        #endregion

        public NavigationManager(TileMap navigationGrid)
        {
            // INITIALIZE fields:
            this.pathFinders = new List<IPathFinder>();
            this.idleTimer = 5.0f;
        }

        #region IMPLEMENTATION OF INavigationManager
        /// <summary>
        /// Adds a new IPathFinder to the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to add to the NavigationManager.</param>
        public void AddPathFinder(IPathFinder pathFinder)
        {
            // ADD the new pathFinder to the List:
            pathFinders.Add(pathFinder);
        }
        /// <summary>
        /// Adds a new IPathFinder to the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to add to the NavigationManager.</param>
        public void RemovePathFinder(IPathFinder pathFinder)
        {
            // REMOVE the pathFinder from the List:
            pathFinders.Remove(pathFinder);
        }
        /// <summary>
        /// Calculates a destination tile from the NavigationGrid and returns the x,y location as a Vector2.
        /// </summary>
        /// <returns>A Vector2 with x,y values of the location to navigate to.</returns>
        public Vector2 FindDestinationTile()
        {
            // DECLARE a new instance of Random, call it random:
            Random random = new Random();
            // DECLARE a new Vector2, call it destinationTile:
            Vector2 destinationTile;
            // DECLARE a 2D array, call it navGrid. Set it to the TileMap in the navgiationGrid:
            Tile[,] navGrid = navigationGrid.GetTileMap();
            // DECLARE an int, call it rows. Set it to the length of the navGrids rows:
            int rows = navGrid.GetLength(0);
            // DECLARE an int, call it columns. Set it to the length of the navGrids columns:
            int columns = navGrid.GetLength(1);
            // DECLARE an int, call it destinationX. Set it to a random number between 0 - rows:
            int destinationX = random.Next(0, rows);
            // DECLARE an int, call it destinationY. Set it to a random number between 0 - columns:
            int destinationY = random.Next(0, columns);
            // SET destinationTile to navGrid at destinationX/Ys Location:
            destinationTile = navGrid[destinationX, destinationY].EntityLocn;
            // RETURN the new destinationTile:
            return destinationTile;
        }

        /// <summary>
        /// Calculates the navigation path for the IPathFinder.
        /// </summary>
        /// <param name="destinationTile">The location to find a path to.</param>
        /// <returns>The navigation path for the IPathFinder.</returns>
        public Vector2 CalculateNavigationPath(Vector2 destinationTile)
        {
            
        }
        /// <summary>
        /// Default Update loop.
        /// </summary>
        /// <param name="gameTime">A Snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
