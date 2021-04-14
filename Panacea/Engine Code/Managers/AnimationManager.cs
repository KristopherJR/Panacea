using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Panacea.Game_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Engine_Code.Managers
{
    class AnimationManager
    {
        private AnimationModel animationModel;

        private float timer;

        public Vector2 Position { get; set; }

        public AnimationManager(AnimationModel am)
        {
            animationModel = am;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationModel.Texture,
                             Position,
                             new Rectangle(animationModel.CurrentFrame * animationModel.FrameWidth,
                                           0,
                                           animationModel.FrameWidth,
                                           animationModel.FrameHeight),
                             Color.White);
        }

        public void Play(AnimationModel am)
        {
            if (animationModel == am)
            {
                return;
            }

            animationModel = am;
            animationModel.CurrentFrame = 0;
            timer = 0;
        }

        public void Stop()
        {
            timer = 0f;
            animationModel.CurrentFrame = 0;
        }

        public void update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(timer > animationModel.FrameSpeed)
            {
                timer = 0f;
                animationModel.CurrentFrame++;

                if(animationModel.CurrentFrame > animationModel.FrameCount)
                {
                    animationModel.CurrentFrame = 0;
                }
            }
        }
    }
}
