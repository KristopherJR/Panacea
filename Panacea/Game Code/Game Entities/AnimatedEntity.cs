using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code.Game_Entities
{
    public class AnimatedEntity : GameEntity
    {
        protected Animation animation;
        
        public AnimatedEntity(Animation animation) : base()
        {
            this.animation = animation;
            entitySprite = animation.AnimationFrames[animation.FrameCounter];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            entitySprite = animation.AnimationFrames[animation.FrameCounter];
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            animation.Update(gameTime);
        }
    }
}
