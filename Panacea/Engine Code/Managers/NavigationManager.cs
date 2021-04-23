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
        #endregion

        #region PROPERTIES
        public TileMap NavigationGrid // property
        {
            get { return navigationGrid; }
            set { navigationGrid = value; }
        }

        public List<IPathFinder> PathFinders // read-only property
        {
            get { return pathFinders; }
        }
        #endregion

        /// <summary>
        /// Constructor for class NavigationManager.
        /// </summary>
        public NavigationManager()
        {
            // INITIALIZE fields:
            this.pathFinders = new List<IPathFinder>();
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
        /// Removes an IPathFinder from the NavigationManager.
        /// </summary>
        /// <param name="pathFinder">The IPathFinder to remove from the NavigationManager.</param>
        public void RemovePathFinder(IPathFinder pathFinder)
        {
            // REMOVE the pathFinder from the List:
            pathFinders.Remove(pathFinder);
        }

        /// <summary>
        /// Selects a random walkable tile from the Navigation Grid and stores it as destinationTile.
        /// </summary>
        private Tile FindDestinationTile()
        {
            // DECLARE a tile, call it destinationTile and set it to null:
            Tile destinationTile = null;
            // DECLARE a bool, call it tileFound and set it to false:
            bool tileFound = false;
            // DECLARE a 2D array, call it navGrid. Set it to the TileMap in the navgiationGrid:
            Tile[,] navGrid = navigationGrid.GetTileMap();
            // DECLARE an int, call it rows. Set it to the length of the navGrids rows:
            int rows = navGrid.GetLength(0);
            // DECLARE an int, call it columns. Set it to the length of the navGrids columns:
            int columns = navGrid.GetLength(1);
            // WHILE the pathfinder does not have a destination tile:
            while (!tileFound)
            {
                // DECLARE a new instance of Random, call it random:
                Random random = new Random();

                // DECLARE an int, call it destinationX. Set it to a random number between 0 - rows:
                int destinationX = random.Next(0, rows);
                // DECLARE an int, call it destinationY. Set it to a random number between 0 - columns:
                int destinationY = random.Next(0, columns);
                // IF the randomly selected tile is NOT collidable:
                if (navGrid[destinationX, destinationY].IsCollidable == false) // Walkable tile
                {
                    // SET tileFound to true:
                    tileFound = true;
                    // STORE the newly selected tile as destinationTile. Save it in the navGrid at destinationX/Ys Location:
                    return destinationTile = navGrid[destinationX, destinationY];
                }
            }
            // RETURN the new destinationTile:
            return destinationTile;
        }

        /// <summary>
        /// Calculates adjacent tiles to the provided Tile object parameter. Returns all adjacent Tiles as a List<>.
        /// </summary>
        /// <param name="currentTile">The Tile the PathFinder is currently on.</param>
        /// <returns></returns>
        private List<Tile> CalculateAdjacentTiles(Tile currentTile)
        {
            // DECLARE a List<Tile>, call it adjacentTiles and initalise it:
            List<Tile> adjacentTiles = new List<Tile>();
            // DECLARE an int, call it tileWidth and set it to the default tile width:
            int tileWidth = GameContent.DEFAULT_TILE_WIDTH;
            // DECLARE an int, call it tileHeight and set it to the default tile height:
            int tileHeight = GameContent.DEFAULT_TILE_HEIGHT;
            // CREATE a new Tile called 'tileUp'. Set it to the Tile above the currentTile:
            Tile tileUp = navigationGrid.GetTileAtIndex((int)currentTile.EntityLocn.X / tileWidth,
                                                       ((int)currentTile.EntityLocn.Y - tileHeight) / tileHeight);
            // CREATE a new Tile called 'tileRight'. Set it to the Tile to the right of the currentTile:
            Tile tileRight = navigationGrid.GetTileAtIndex(((int)currentTile.EntityLocn.X + tileWidth) / tileWidth,
                                                           ((int)currentTile.EntityLocn.Y / tileHeight));
            // CREATE a new Tile called 'tileDown'. Set it to the Tile below the currentTile:
            Tile tileDown = navigationGrid.GetTileAtIndex((int)currentTile.EntityLocn.X / tileWidth,
                                                         ((int)currentTile.EntityLocn.Y + tileHeight) / tileHeight);
            // CREATE a new Tile called 'tileLeft'. Set it to the Tile to the left of the currentTile:
            Tile tileLeft = navigationGrid.GetTileAtIndex(((int)currentTile.EntityLocn.X - tileWidth) / tileWidth,
                                                          ((int)currentTile.EntityLocn.Y / tileHeight));
            // IF there is a Tile above the current Tile and it is walkable:
            if (tileUp != null && !tileUp.IsCollidable)
            {
                // ADD it to adjacentTiles:
                adjacentTiles.Add(tileUp);
            }
            // IF there is a Tile to the right of the current Tile and it is walkable:
            if (tileRight != null && !tileRight.IsCollidable)
            {
                // ADD it to adjacentTiles:
                adjacentTiles.Add(tileRight);
            }
            // IF there is a Tile below the current Tile and it is walkable:
            if (tileDown != null && !tileDown.IsCollidable)
            {
                // ADD it to adjacentTiles:
                adjacentTiles.Add(tileDown);
            }
            // IF there is a Tile to the left of the current Tile and it is walkable:
            if (tileLeft != null && !tileLeft.IsCollidable)
            {
                // ADD it to adjacentTiles:
                adjacentTiles.Add(tileLeft);
            }
            // RETURN the adjacentTiles:
            return adjacentTiles;
        }
        /// <summary>
        /// Reconstructs the Path in its current best-state.
        /// </summary>
        /// <param name="cameFrom">A Dictionary containing all Tiles that are currently in the Path.</param>
        /// <param name="current">A Tile that the PathFinder is currently on.</param>
        /// <returns>A List<Vector2> with the reconstructed Path.</Vector2></returns>
        private List<Vector2> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
        {
            // DECLARE a List<Vector2>, call it path and initalise it:
            List<Vector2> path = new List<Vector2>();
            // ADD the location of the current Tile to the Path:
            path.Add(current.EntityLocn);
            // WHILE the current Tile is in the cameFrom Dictionary:
            while (cameFrom.ContainsKey(current))
            {
                // SET current to the current Tile in the cameFrom Dictionary:
                current = cameFrom[current];
                // ADD the current Tile to the Path:
                path.Add(current.EntityLocn);
            }
            // REVERSE the path so it reads from start -> finish rather than finish -> start:
            path.Reverse();
            // RETURN the newly reconstructed path:
            return path;
        }

        /// <summary>
        /// Calculates the Euclidean Distance between 2 Tile objects.
        /// </summary>
        /// <param name="startTile">The starting Tile.</param>
        /// <param name="destinationTile">The destination Tile.</param>
        /// <returns>A float containing the Euclidean Distance between the 2 Tiles.</returns>
        private float CalculateEuclideanDistance(Tile startTile, Tile destinationTile)
        {
            // DECLARE a float, call it euclideanDistance. Set it to the Distance between the start and destination Tile:
            float euclideanDistance = Vector2.Distance(startTile.EntityLocn, destinationTile.EntityLocn);
            // RETURN the euclideanDistance:
            return euclideanDistance;
        }

        /// <summary>
        /// Based on Pseudocode from: https://en.wikipedia.org/wiki/A*_search_algorithm 
        /// </summary>
        /// <param name="startTile">The start tile.</param>
        /// <param name="goalTile">The destination tile.</param>
        /// <returns>A List<Vector2> containing the shortest path from start to goal Tile.</Vector2></returns>
        private List<Vector2> AStar(Tile startTile, Tile goalTile)
        {
            // DECLARE a HashSet of type Tile. Contains all discovered Tiles that may need to be re-expanded.
            // Initially only startTile is known.
            HashSet<Tile> openSet = new HashSet<Tile>();
            // ADD the startTile to the openSet:
            openSet.Add(startTile);
            // DECLARE a Dictionary<Tile, Tile>, call it cameFrom and initialise it:
            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
            // DECLARE a Dictionary<Tile, float>, call it gScore and initialise it:
            Dictionary<Tile, float> gScore = new Dictionary<Tile, float>();
            // ADD the start tile to gScore at value 0:
            gScore.Add(startTile, 0);
            // DECLARE a Dictionary<Tile, float>, call it fScore and initialise it:
            Dictionary<Tile, float> fScore = new Dictionary<Tile, float>();
            // ADD startTile to fScore at value EuclideanDistance(start, goal):
            fScore.Add(startTile, CalculateEuclideanDistance(startTile, goalTile));

            // WHILE there are objects in the openSet:
            while (openSet.Count != 0)
            {
                // CREATE a new Tile, call it currentTile and set it to null:
                Tile currentTile = null;
                // FOREACH Tile in the openSet:
                foreach (Tile t in openSet)
                {
                    // IF the currentTile is Null OR the fScore of the currentTile is greater than the fScore of t:
                    if (currentTile == null || fScore[currentTile] > fScore[t])
                    {
                        // SET currentTile to t:
                        currentTile = t;
                    }
                }
                // IF the currentTile is the same as the goalTile:
                if (currentTile == goalTile)
                {
                    // RETURN the complete Path once it has been reconstructred:
                    return this.ReconstructPath(cameFrom, currentTile);
                }
                // REMOVE the currentTile from the openSet:
                openSet.Remove(currentTile);
                // DECLARE a List<Tile>, call it neighbours and set it to the return of CalculateAdjacentTiles(currentTile):
                List<Tile> neighbours = this.CalculateAdjacentTiles(currentTile);
                // FOREACH Tile in neighbours:
                foreach (Tile t in neighbours)
                {
                    // DECLARE a float, call it tentativeGScore and set it to the gScore value of the currentTile:
                    float tentativeGScore = gScore[currentTile];
                    // IF gScore doesn't contain t OR tentativeGScore is less than gScore[t]:
                    if (!gScore.ContainsKey(t) || tentativeGScore < gScore[t])
                    {
                        // SET cameFrom[t] to the current tile:
                        cameFrom[t] = currentTile;
                        // SET gScore[t] to tentativeGScore:
                        gScore[t] = tentativeGScore;
                        // SET fScore[t] to gScore[t] PLUS the EuclideanDistance of t to the destination Tile:
                        fScore[t] = gScore[t] + this.CalculateEuclideanDistance(t, goalTile);
                        // IF the openSet doesn't contain t:
                        if (!openSet.Contains(t))
                        {
                            // ADD it to the openSet:
                            openSet.Add(t);
                        }
                    }
                }
            }
            // THROW a new exception if the PathFinder couldn't calculate a path to the destination Tile:
            throw new Exception("PathFinder could not find a route to the destination Tile!");
        }
        
        /// <summary>
        /// Calculates what Tile is at the current location and returns it:
        /// </summary>
        /// <param name="currentPosition">the current position as a Vector2</param>
        /// <returns>The Tile at the current position.</returns>
        private Tile CalculateCurrentTileAtPosition(Vector2 currentPosition)
        {
            // DECLARE a new Tile, call it newTile and set it to the Tile at the currentPosition:
            Tile newTile = navigationGrid.GetTileAtIndex((int)currentPosition.X / GameContent.DEFAULT_TILE_WIDTH,
                                                         (int)currentPosition.Y / GameContent.DEFAULT_TILE_HEIGHT);
            // RETURN newTile:
            return newTile;
        }

        /// <summary>
        /// Default Update loop.
        /// </summary>
        /// <param name="gameTime">A Snapshot of the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            // FOREACH PathFinder:
            foreach (IPathFinder pathFinder in pathFinders)
            {
                // IF the PathFinder DOES NOT have a Path:
                if (pathFinder.Path == null || pathFinder.Path.Count == 0)
                {
                    // CALCULATE them a new Desintation Tile:
                    Tile newDestinationTile = this.FindDestinationTile();
                    // IF the pathFinder is a GameEntity:
                    if (pathFinder is GameEntity)
                    {
                        // CREATE a new Tile, call it currentTile and set it to the Tile at the PathFinders current location (Their start tile):
                        Tile currentTile = this.CalculateCurrentTileAtPosition((pathFinder as GameEntity).EntityLocn);
                        // DECLARE a List<Vector2>, call it newPath and set it to the returned path from the AStar Algorithm method:
                        List<Vector2> newPath = this.AStar(currentTile, newDestinationTile);
                        // SET the PathFinders path to the newly generated one:
                        pathFinder.Path = newPath;
                    }
                }
            }
        }
        #endregion
    }
}
