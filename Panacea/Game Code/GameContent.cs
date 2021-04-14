using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Panacea
{
    class GameContent
    {
        #region FIELDS
        public static Texture2D ImgSam;
        private SpriteFont font;
        #endregion
        #region PROPERTIES
        #endregion
        public GameContent(ContentManager cm)
        {
            //load images
            ImgSam = cm.Load<Texture2D>("assets/sam/walking/down/1");

            //load fonts
            font = cm.Load<SpriteFont>("AdobeMingStd-Light20");
        }
    }
}
