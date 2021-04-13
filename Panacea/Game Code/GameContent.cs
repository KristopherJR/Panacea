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
        #region PROPERTIES
        public Texture2D imgSamWalkUp1 { get; set; }
        public Texture2D imgSamWalkUp2 { get; set; }
        public Texture2D imgSamWalkUp3 { get; set; }
        public Texture2D imgSamWalkUp4 { get; set; }
        public Texture2D imgSamWalkDown1 { get; set; }
        public Texture2D imgSamWalkDown2 { get; set; }
        public Texture2D imgSamWalkDown3 { get; set; }
        public Texture2D imgSamWalkDown4 { get; set; }

        public Texture2D imgPaddle { get; set; }
        public Texture2D imgBall { get; set; }
        public Texture2D imgPixel { get; set; }
        public SpriteFont labelFont { get; set; }
        #endregion


        public GameContent(ContentManager Content)
        {
            //load images
            imgBall = Content.Load<Texture2D>("Ball");
            imgPixel = Content.Load<Texture2D>("Pixel");
            imgPaddle = Content.Load<Texture2D>("Paddle");
            imgBrick = Content.Load<Texture2D>("Brick");

            //load sounds
            startSound = Content.Load<SoundEffect>("StartSound");
            brickSound = Content.Load<SoundEffect>("BrickSound");
            paddleBounceSound = Content.Load<SoundEffect>("PaddleBounceSound");
            wallBounceSound = Content.Load<SoundEffect>("WallBounceSound");
            missSound = Content.Load<SoundEffect>("MissSound");
            //load fonts
            labelFont = Content.Load<SpriteFont>("Arial20");
        }
    }
}
