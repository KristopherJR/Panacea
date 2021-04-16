using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code
{
    public class TileMap
    {
        private Tile[,] tileMap;
        private String tileMapFilePath;
        private bool isLayerCollidable;

        public TileMap(String tileMapFilePath, bool isLayerCollidable)
        {
            this.tileMapFilePath = tileMapFilePath;
            this.isLayerCollidable = isLayerCollidable;
            this.LoadTileMap();
        }

        private List<String> ParseFile(String filePath)
        {
            List<String> lines = new List<String>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                String line;

                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        private void LoadTileMap()
        {
            List<String> rows = this.ParseFile(tileMapFilePath);

            int width = rows[0].Split(',').Count();
            int height = rows.Count;

            tileMap = new Tile[width, height];

            for (int y = 0; y < height; y++)
            {
                String[] tileStrings = rows[y].Split(',');

                for(int x = 0; x < tileStrings.Length; x++)
                {
                    int tileIdParse = int.Parse(tileStrings[x]);
                    bool isValidTile = true;
                    if (tileIdParse < 0)
                    {
                        tileIdParse = (GameContent.NUMBER_OF_TILES) - 1;
                        isValidTile = false;
                    }
                    
                    Tile newTile = new Tile(tileIdParse);
                  
                    newTile.IsCollidable = isLayerCollidable && isValidTile;
                  
                    newTile.SetTilePosition(new Vector2(x * GameContent.DEFAULT_TILE_WIDTH,
                                                        y * GameContent.DEFAULT_TILE_HEIGHT));
                    tileMap[x,y] = newTile;
                }     
            }
        }

        public void DrawTileMap(SpriteBatch spriteBatch)
        {
            foreach (Tile t in tileMap)
            {
                t.Draw(spriteBatch);
            }
        }

        public Tile[,] GetTileMap()
        {
            return tileMap;
        }
    }
}
