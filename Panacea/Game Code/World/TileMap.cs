using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Panacea.Game_Code
{
    public class TileMap
    {
        // DECLARE a 2-Dimensional Array, call it tileMap:
        private Tile[,] tileMap;
        // DECLARE a String, call it tileMapFilePath. This will contain the system path to the .csv file containing the TileMap data:
        private String tileMapFilePath;
        // DECLARE a bool, call it isLayerCollidable:
        private bool isLayerCollidable;

        /// <summary>
        /// Constructor for objects of class TileMap.
        /// </summary>
        /// <param name="tileMapFilePath">The System File Path to the .csv file containing the TileMap data.</param>
        /// <param name="isLayerCollidable">A boolean specifying if objects can collide with this Layer.</param>
        public TileMap(String tileMapFilePath, bool isLayerCollidable)
        {
            // SET fields to the incoming parameters:
            this.tileMapFilePath = tileMapFilePath;
            this.isLayerCollidable = isLayerCollidable;
            // LOAD the TileMap:
            this.LoadTileMap();
        }

        /// <summary>
        /// Loads the TileMap from the data in the .csv File.
        /// </summary>
        private void LoadTileMap()
        {
            // DECLARE a List<String>, call it rows. Set it to the return of ParseFile(tileMapFilePath):
            List<String> rows = this.ParseFile(tileMapFilePath);
            // DECLARE an int, call it width. Set it to the Count of values in the text file on the X axis:
            int width = rows[0].Split(',').Count();
            // DECLARE an int, call it height. Set it to the Count of values in the text file on the Y axis:
            int height = rows.Count;
            // INITALISE the tileMap and pass in the width and height:
            tileMap = new Tile[width, height];
            // LOOP for height:
            for (int y = 0; y < height; y++)
            {
                // DECLARE a String[], call it tileStrings. Set it to the rows[y] split by commas:
                String[] tileStrings = rows[y].Split(',');
                // LOOP for tileStrings.Length (I.E the entire text row):
                for (int x = 0; x < tileStrings.Length; x++)
                {
                    // DECLARE an int, call it tileIDParse and set it to the value at tileStrings[x]. Convert it from a String to an int:
                    int tileIdParse = int.Parse(tileStrings[x]);
                    // DECLARE a bool, call it isValidTile and set it to true:
                    bool isValidTile = true;
                    // IF tileIdParse < 0:
                    if (tileIdParse < 0)
                    {
                        // This section is used to stop invisible tiles being loaded into the TileMap:

                        // SET tileIdParse to the total number of Tiles - 1:
                        tileIdParse = (GameContent.NUMBER_OF_TILES) - 1;
                        // SET isValidTile to false:
                        isValidTile = false;
                    }
                    // DECLARE a Tile, call it newTile and pass in tileIdParse:
                    Tile newTile = new Tile(tileIdParse);
                    // SET newTile.IsCollidable to true if the layer is collidable and it's a valid tile, else false:
                    newTile.IsCollidable = isLayerCollidable && isValidTile;
                    // SET the tiles position to the next space in the TileMap:
                    newTile.SetTilePosition(new Vector2(x * GameContent.DEFAULT_TILE_WIDTH,
                                                        y * GameContent.DEFAULT_TILE_HEIGHT));
                    // STORE the newly created Tile in the TileMap:
                    tileMap[x, y] = newTile;
                }
            }
        }
        /// <summary>
        /// Reads a File and returns it as a List<String>.
        /// </summary>
        /// <param name="filePath">The System File Path to the File.</param>
        /// <returns>A List<String> containing all of the data in the File.</returns>
        private List<String> ParseFile(String filePath)
        {
            // DECLARE a List<String>, call it lines and initalise it:
            List<String> lines = new List<String>();
            // USE a StreamReader to read in the File at the filePath:
            using (StreamReader reader = new StreamReader(filePath))
            {
                // DECLARE a String, call it line:
                String line;
                // WHILST there is a new line in the File:
                while ((line = reader.ReadLine()) != null)
                {
                    // ADD the line to the lines List:
                    lines.Add(line);
                }
            }
            // RETURN lines:
            return lines;
        }

        /// <summary>
        /// Returns a Tile from the TileMap at the given index.
        /// </summary>
        /// <param name="xIndex">The X index of the Tile to retrieve.</param>
        /// <param name="yIndex">The Y index of the Tile to retrieve.</param>
        /// <returns>The Tile at the specified index from the TileMap.</returns>
        public Tile GetTileAtIndex(int xIndex, int yIndex)
        {
            // RETURN the Tile from the TileMap at the specified index:
            return tileMap[xIndex, yIndex];
        }
        /// <summary>
        /// Draws the TileMap onto the SpriteBatch provided as a parameter.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw the TileMap onto.</param>
        public void DrawTileMap(SpriteBatch spriteBatch)
        {
            // FOREACH Tile in TileMap:
            foreach (Tile t in tileMap)
            {
                // DRAW the Tile:
                t.Draw(spriteBatch);
            }
        }
        /// <summary>
        /// Returns the Whole TileMap.
        /// </summary>
        /// <returns>Returns the Whole TileMap.</returns>
        public Tile[,] GetTileMap()
        {
            // RETURN tileMap:
            return tileMap;
        }
    }
}
