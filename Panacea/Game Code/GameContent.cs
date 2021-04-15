using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Panacea.Game_Code;
using Panacea.Game_Code.Game_Entities;
using System.Collections.Generic;

namespace Panacea
{
    public enum AnimationGroup
    {
        SamWalkDown,SamWalkUp,SamWalkLeft,SamWalkRight
    }
    public static class GameContent
    {
        #region FIELDS
        public static Texture2D SamSpriteSheet;
        private const int DEFAULT_FRAMERATE = 6;

        private static Dictionary<AnimationGroup, Animation> animations;
        #endregion

        public static void LoadContent(ContentManager cm)
        {
            // LOAD Sams spritesheet:
            SamSpriteSheet = cm.Load<Texture2D>("sam_spritesheet");
            // INITALIZE Fields:
            animations = new Dictionary<AnimationGroup, Animation>();

            // LOAD Sam Walking Down:
            LoadAnimation(4, DEFAULT_FRAMERATE, 1, 6, 15, 22, 16, AnimationGroup.SamWalkDown);
            // LOAD Sam Walking Right:
            LoadAnimation(4, DEFAULT_FRAMERATE, 2, 38, 13, 22, 16, AnimationGroup.SamWalkRight);
            // LOAD Sam Walking Up:
            LoadAnimation(4, DEFAULT_FRAMERATE, 0, 69, 15, 23, 16, AnimationGroup.SamWalkUp);
            // LOAD Sam Walking Left:
            LoadAnimation(4, DEFAULT_FRAMERATE, 1, 102, 13, 22, 16, AnimationGroup.SamWalkLeft);
        }
        private static void LoadAnimation(int numberOfFrames, int frameRate, int x, int y, int width, int height, int spacer, AnimationGroup animationGroup)
        {
            Animation samWalkingDownAnimation = new Animation(frameRate);

            for (int i = 0; i < numberOfFrames; i++)
            {
                Sprite tempFrame = new Sprite(SamSpriteSheet, ((i * spacer) + x), y, width, height);
                samWalkingDownAnimation.AddFrame(tempFrame);
            }

            animations.Add(animationGroup, samWalkingDownAnimation);
        }

        public static Animation GetAnimation(AnimationGroup animationGroup)
        {
            return animations[animationGroup];
        }
    }
}
