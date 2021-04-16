using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Panacea.Game_Code.Game_Entities;
using Panacea.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code
{
    public class Tile : GameEntity, ICollidable
    {
        #region PROPERTIES
        public bool IsCollidable
        {
            get { return isCollidable; }
            set { isCollidable = value; }
        }
        #endregion
        public Tile(int tileID) : base()
        {
            // INITIALIZE fields:
            this.entitySprite = this.LoadTileSprite(tileID);
            this.entityLocn = new Vector2(0,0);
        }
        public void SetTilePosition(Vector2 tilePosition)
        {
            this.entityLocn = new Vector2(tilePosition.X, tilePosition.Y);
        }

        private Sprite LoadTileSprite(int tileID)
        {
            Sprite retrievedTile = GameContent.GetTileSprite(tileID);
            return retrievedTile;
        }
    }
}
