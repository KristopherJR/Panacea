using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Panacea.Game_Code.Game_Entities
{
    public class Sprite
    {
        #region FIELDS
        // DECLARE a Texture2D to store the sprites texture:
        private Texture2D spriteSheetTexture;
        // DECLARE an int called xOrigin to store the x co-ordinate of the texture on the sprite sheet:
        private int xOrigin;
        // DECLARE an int called yOrigin to store the y co-ordinate of the texture on the sprite sheet:
        private int yOrigin;
        // DECLARE an int called textureWidth, stores the width of the texture in pixels:
        private int textureWidth;
        // DECLARE an int called textureHeight, stores the height of the texture in pixels:
        private int textureHeight;
        #endregion

        #region PROPERTIES
        public Texture2D SpriteSheetTexture
        {
            get { return spriteSheetTexture; } // get method
            set { spriteSheetTexture = value; } // set method
        }

        public int TextureWidth
        {
            get { return textureWidth; }
        }

        public int TextureHeight
        {
            get { return textureHeight; }
        }
        #endregion
        /// <summary>
        /// Constructor for a Sprite.
        /// </summary>
        /// <param name="spriteTexture">The image file for the texture</param>
        /// <param name="xOrigin">The X co-ordinate for the texture on the sprite sheet.</param>
        /// <param name="yOrigin">The Y co-ordinate for the texture on the sprite sheet.</param>
        /// <param name="textureWidth">The width of the texture.</param>
        /// <param name="textureHeight">the height of the texture.</param>
        public Sprite(Texture2D spriteTexture, int xOrigin, int yOrigin, int textureWidth, int textureHeight)
        {
            // INITIALIZE fields:
            this.spriteSheetTexture = spriteTexture;
            this.xOrigin = xOrigin;
            this.yOrigin = yOrigin;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
        }

        /// <summary>
        /// Draws the sprite onto the SpriteBatch. Called from GameEntity.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw the Sprite on.</param>
        /// <param name="location">Where to draw the sprite on the SpriteBatch.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            spriteBatch.Draw(spriteSheetTexture, location, new Rectangle(xOrigin, yOrigin, textureWidth, textureHeight), Color.White);
        }
    }
}
