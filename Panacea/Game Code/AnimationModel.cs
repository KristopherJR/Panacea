using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code
{
    class AnimationModel
    {
        // This class was programmed based on Oyyou's video lesson on YouTube, found here: https://www.youtube.com/watch?v=OLsiWxgONeM
        // This is predominantly his code, with a few tweaks here and there
        #region FIELDS
        #endregion
        #region PROPERTIES
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public float FrameSpeed { get; set; }
        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }
        #endregion

        public AnimationModel(Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.2f;
        }
    }
}
