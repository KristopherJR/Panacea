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
        #endregion
        public GameContent(ContentManager cm)
        {
            ImgSam = cm.Load<Texture2D>("sam_spritesheet");
        }
    }
}
