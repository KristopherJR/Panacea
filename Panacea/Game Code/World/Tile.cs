using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Panacea.Game_Code.Game_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code
{
    public class Tile : GameEntity
    {

        public Tile(TileSpriteGroup tileSpriteGroupName) : base()
        {
            // INITIALIZE fields:
            this.entitySprite = this.LoadTileSprite(tileSpriteGroupName);
            this.entityLocn = new Vector2(0,0);
        }

        public void setTilePosition(Vector2 tilePosition)
        {
            this.entityLocn = new Vector2(tilePosition.X, tilePosition.Y);
        }

        private Sprite LoadTileSprite(TileSpriteGroup tileSpriteGroupName)
        {
            Sprite retrievedTile = GameContent.GetTileSprite(tileSpriteGroupName);
            return retrievedTile;
        }
    }
}
