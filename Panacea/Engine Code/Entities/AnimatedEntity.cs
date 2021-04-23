using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Panacea.Game_Code.Game_Entities
{
    public class AnimatedEntity : GameEntity
    {
        #region FIELDS
        // DECLARE an instance of Animation, call it entityAnimation:
        protected Animation entityAnimation;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class AnimatedEntity.
        /// </summary>
        /// <param name="animation">An instance of an Animation. Passed from a character.</param>
        public AnimatedEntity(Animation animation) : base()
        {
            // STORE the entityAnimation parameter:
            this.entityAnimation = animation;
            // SET the entities texture to the animations frame counter:
            entitySprite = animation.AnimationFrames[animation.FrameCounter];
        }

        /// <summary>
        /// Draws an entity onto the SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch">A reference to the SpriteBatch to draw something onto.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // SET the entities texture to the animations frame counter:
            entitySprite = entityAnimation.AnimationFrames[entityAnimation.FrameCounter];
            // CALL the parents Draw method and pass up the SpriteBatch reference:
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Overrides GameEntity's default Update method.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // CALL the parents update method and pass in gameTime:
            base.Update(gameTime);
            // UPDATE the entityAnimation by gameTime:
            entityAnimation.Update(gameTime);
        }
    }
}
