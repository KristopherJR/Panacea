using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code
{
    public class TileMap
    {
        private List<Tile> tileMap; 
        public TileMap()
        {
            tileMap = new List<Tile>();

            this.LoadTileMap();
        }

        private void LoadTileMap()
        {
            Tile newTile = new Tile(TileSpriteGroup.HospitalFloorTile);
            newTile.setTilePosition(new Vector2(100, 100));
            tileMap.Add(newTile);
        }

        public void DrawTileMap(SpriteBatch spriteBatch)
        {
            foreach (Tile t in tileMap)
            {
                t.Draw(spriteBatch);
            }
        }
    }
}
