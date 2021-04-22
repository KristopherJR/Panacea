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

        public NavigationManager()
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
        /// Selects a random walkable tile from the Navigation Grid and stores it as destionationTile.
        /// </summary>
        private Tile FindDestinationTile()
        {
            Tile destinationTile = null;
            bool tileFound = false;
            // DECLARE a 2D array, call it navGrid. Set it to the TileMap in the navgiationGrid:
            Tile[,] navGrid = navigationGrid.GetTileMap();
            // DECLARE an int, call it rows. Set it to the length of the navGrids rows:
            int rows = navGrid.GetLength(0);
            // DECLARE an int, call it columns. Set it to the length of the navGrids columns:
            int columns = navGrid.GetLength(1);

            while(!tileFound)
            {
                // DECLARE a new instance of Random, call it random:
                Random random = new Random();
            
                // DECLARE an int, call it destinationX. Set it to a random number between 0 - rows:
                int destinationX = random.Next(0, rows);
                // DECLARE an int, call it destinationY. Set it to a random number between 0 - columns:
                int destinationY = random.Next(0, columns);

                if(navGrid[destinationX,destinationY].IsCollidable == false) // Walkable tile
                {
                    tileFound = true;
                    // SET destinationTile to navGrid at destinationX/Ys Location:
                    return destinationTile = navGrid[destinationX, destinationY];
                }
            }

            return destinationTile;
        }

        private List<Tile> CalculateAdjacentTiles(Tile currentTile)
        {
            List<Tile> adjacentTiles = new List<Tile>();

            int tileWidth = GameContent.DEFAULT_TILE_WIDTH;
            int tileHeight = GameContent.DEFAULT_TILE_HEIGHT;

            Tile tileUp = navigationGrid.GetTileAtIndex((int)currentTile.EntityLocn.X / tileWidth,
                                                       ((int)currentTile.EntityLocn.Y - tileHeight) / tileHeight);

            Tile tileRight = navigationGrid.GetTileAtIndex(((int)currentTile.EntityLocn.X + tileWidth) / tileWidth,
                                                           ((int)currentTile.EntityLocn.Y / tileHeight));

            Tile tileDown = navigationGrid.GetTileAtIndex((int)currentTile.EntityLocn.X / tileWidth,
                                                         ((int)currentTile.EntityLocn.Y + tileHeight) / tileHeight);

            Tile tileLeft = navigationGrid.GetTileAtIndex(((int)currentTile.EntityLocn.X - tileWidth) / tileWidth,
                                                          ((int)currentTile.EntityLocn.Y / tileHeight));

            if(tileUp != null && !tileUp.IsCollidable)
            {
                adjacentTiles.Add(tileUp);
            }
            if (tileRight != null && !tileRight.IsCollidable)
            {
                adjacentTiles.Add(tileRight);
            }
            if (tileDown != null && !tileDown.IsCollidable)
            {
                adjacentTiles.Add(tileDown);
            }
            if (tileLeft != null && !tileLeft.IsCollidable)
            {
                adjacentTiles.Add(tileLeft);
            }

            return adjacentTiles;
        }

        private List<Vector2> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
        {
            List<Vector2> path = new List<Vector2>();
            path.Add(current.EntityLocn);

            while(cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current.EntityLocn);

            }
            path.Reverse();
            return path;
        }

        private float CalculateEuclideanDistance(Tile startTile, Tile destinationTile)
        {

            float euclideanDistance = Vector2.Distance(startTile.EntityLocn, destinationTile.EntityLocn);

            return euclideanDistance;
        }

        /// <summary>
        /// Based on Pseudocode from: https://en.wikipedia.org/wiki/A*_search_algorithm 
        /// </summary>
        /// <param name="startTile"></param>
        /// <param name="goalTile"></param>
        /// <returns></returns>
        private List<Vector2> AStar(Tile startTile, Tile goalTile)
        {
            // DECLARE a HashSet of type Tile. Contains all discovered Tiles that may need to be re-expanded.
            // Initially only startTile is known.
            HashSet<Tile> openSet = new HashSet<Tile>();

            openSet.Add(startTile);

            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
            Dictionary<Tile, float> gScore = new Dictionary<Tile, float>();

            gScore.Add(startTile, 0);

            Dictionary<Tile, float> fScore = new Dictionary<Tile, float>();

            fScore.Add(startTile, CalculateEuclideanDistance(startTile, goalTile));

            while(openSet.Count != 0)
            {
                Tile currentTile = null;

                foreach(Tile t in openSet)
                {
                    if(currentTile == null || fScore[currentTile] > fScore[t])
                    {
                        currentTile = t;
                    }
                }

                if(currentTile == goalTile)
                {
                    return this.ReconstructPath(cameFrom, currentTile);
                }
                openSet.Remove(currentTile);

                List<Tile> neighbours = this.CalculateAdjacentTiles(currentTile);

                foreach(Tile t in neighbours)
                {
                    float tentativeGScore = gScore[currentTile];

                    if(!gScore.ContainsKey(t) || tentativeGScore < gScore[t])
                    {
                        cameFrom[t] = currentTile;
                        gScore[t] = tentativeGScore;
                        fScore[t] = gScore[t] + this.CalculateEuclideanDistance(t, goalTile);

                        if(!openSet.Contains(t))
                        {
                            openSet.Add(t);
                        }
                    }
                }
            }

            throw new Exception("PathFinder could not find a route to the destination Tile!");
        }

        private Tile CalculateCurrentTileAtPosition(Vector2 currentPosition)
        {
            Tile newTile = navigationGrid.GetTileAtIndex((int)currentPosition.X / GameContent.DEFAULT_TILE_WIDTH,
                                                         (int)currentPosition.Y / GameContent.DEFAULT_TILE_HEIGHT);
            return newTile;
        }


        /// <summary>
        /// Default Update loop.
        /// </summary>
        /// <param name="gameTime">A Snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            foreach(IPathFinder pathFinder in pathFinders)
            {
                if(pathFinder.Path == null || pathFinder.Path.Count == 0)
                {
                    Tile newDestinationTile = this.FindDestinationTile();
                    if(pathFinder is GameEntity)
                    {
                        Tile currentTile = this.CalculateCurrentTileAtPosition((pathFinder as GameEntity).EntityLocn);

                        List<Vector2> newPath = this.AStar(currentTile, newDestinationTile);
                        pathFinder.Path = newPath;
                    }
                }
            }
        }
        #endregion
    }
}
