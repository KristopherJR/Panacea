﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Panacea.Game_Code;
using Panacea.Game_Code.Game_Entities;
using System.Collections.Generic;

namespace Panacea
{
    /// <summary>
    /// Enum used to access specific Animations in the animations Dictionary.
    /// </summary>
    public enum AnimationGroup
    {
        // DEFINE Sams Enums for his animations:
        SamWalkDown, SamWalkUp, SamWalkLeft, SamWalkRight,
        SamSprintDown, SamSprintUp, SamSprintRight, SamSprintLeft
    }
    /// <summary>
    /// Static class GameContent. Used to store all of the games images, sounds and font in one contained place.
    /// </summary>
    public static class GameContent
    {
        #region FIELDS
        // DECLARE a static Texture2D, call it SamSpriteSheet:
        public static Texture2D SamSpriteSheet;
        // DECLARE a static Texture2D, call it WorldTileSheet:
        public static Texture2D WorldTileSheet;
        // DECLARE a const int, call it DEFAULT_FRAMERATE and set it to 6fps:
        public const int DEFAULT_FRAMERATE = 6;
        // DECLARE a const int, call it DEFAULT_TILE_WIDTH and set it to 16:
        public const int DEFAULT_TILE_WIDTH = 16;
        // DECLARE a const int, call it DEFAULT_TILE_HEIGHT and set it to 16:
        public const int DEFAULT_TILE_HEIGHT = 16;
        // DECLARE a const int, call it TILE_SHEET_WIDTH:
        public const int TILE_SHEET_WIDTH = 352 / DEFAULT_TILE_WIDTH;
        // DECLARE a const int, call it NUMBER_OF_TILES:
        public const int NUMBER_OF_TILES = 397;

        // DECLARE a static Dictionary to store all of the games animations. Reference each element via the AnimationGroup enum:
        private static Dictionary<AnimationGroup, Animation> animations;
        // DECLARE a stiatc Dictionary to store all Tile Sprites. Reference each one by an int id:
        private static Dictionary<int, Sprite> tileSprites;
        #endregion

        /// <summary>
        /// Loads all of the games Content. Called from the Kernel.
        /// </summary>
        /// <param name="cm">A Reference to the ContentManager used to load assets.</param>
        public static void LoadContent(ContentManager cm)
        {
            // INITALIZE Fields:
            animations = new Dictionary<AnimationGroup, Animation>();
            tileSprites = new Dictionary<int, Sprite>();

            // LOAD Sams spritesheet:
            SamSpriteSheet = cm.Load<Texture2D>("sam_spritesheet");
            // LOAD the World Tile Sheet:
            WorldTileSheet = cm.Load<Texture2D>("inner_tilesheet");

            #region LOADING ANIMATIONS
            // LOAD Sam Walking Down:
            LoadAnimation(4, DEFAULT_FRAMERATE, 1, 6, 15, 22, 16, AnimationGroup.SamWalkDown);
            // LOAD Sam Walking Right:
            LoadAnimation(4, DEFAULT_FRAMERATE, 2, 38, 13, 22, 16, AnimationGroup.SamWalkRight);
            // LOAD Sam Walking Up:
            LoadAnimation(4, DEFAULT_FRAMERATE, 0, 69, 15, 23, 16, AnimationGroup.SamWalkUp);
            // LOAD Sam Walking Left:
            LoadAnimation(4, DEFAULT_FRAMERATE, 1, 102, 13, 22, 16, AnimationGroup.SamWalkLeft);

            // LOAD Sam Sprinting Down:
            LoadAnimation(4, DEFAULT_FRAMERATE, 144, 6, 16, 22, 16, AnimationGroup.SamSprintDown);
            // LOAD Sam Sprinting Right:
            LoadAnimation(4, DEFAULT_FRAMERATE, 146, 38, 14, 22, 16, AnimationGroup.SamSprintRight);
            // LOAD Sam Sprinting Up:
            LoadAnimation(4, DEFAULT_FRAMERATE, 144, 69, 16, 23, 16, AnimationGroup.SamSprintUp);
            // LOAD Sam Sprinting Left:
            LoadAnimation(4, DEFAULT_FRAMERATE, 145, 102, 13, 22, 16, AnimationGroup.SamSprintLeft);
            #endregion

            #region LOADING TILESPRITES
            // LOAD the Tile Sprites:
            for (int i=0; i < NUMBER_OF_TILES; i++)
            {
                ExtractTile(i);
            }

            
            #endregion
        }
        /// <summary>
        /// Called from inside this classes constructor. Creates and loads animations.
        /// </summary>
        /// <param name="numberOfFrames">The amount of individual frames in the entityAnimation.</param>
        /// <param name="frameRate">The rate at which the entityAnimation plays in Frames per Second.</param>
        /// <param name="x">The top left pixel origin of the texture on the spritesheet about the X axis.</param>
        /// <param name="y">The top left pixel origin of the texture on the spritesheet about the Y axis.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="spacer">The amount of pixels on the X axis between each frame on the spritesheet.</param>
        /// <param name="animationGroup">A reference to the enum storing the names of each entityAnimation. Used when retrieving animations from the Dictionary.</param>
        private static void LoadAnimation(int numberOfFrames, int frameRate, int x, int y, int width, int height, int spacer, AnimationGroup animationGroup)
        {
            // CREATE a new entityAnimation, call it tempAnimation and pass in the frameRate:
            Animation tempAnimation = new Animation(frameRate);

            // LOOP for the total number of frames:
            for (int i = 0; i < numberOfFrames; i++)
            {
                // CREATE a new sprite and call it tempFrame. Pass in the spritesheet and other parameters:
                Sprite tempFrame = new Sprite(SamSpriteSheet, ((i * spacer) + x), y, width, height);
                // ADD the tempFrame to the tempAnimation:
                tempAnimation.AddFrame(tempFrame);
            }

            // STORE the new tempAnimation in the animations Dictionary, using the name provided from the enum:
            animations.Add(animationGroup, tempAnimation);
        }

        private static void ExtractTile(int tileID)
        {
            int x = (tileID % TILE_SHEET_WIDTH) * DEFAULT_TILE_WIDTH;
            int y = (tileID / TILE_SHEET_WIDTH) * DEFAULT_TILE_WIDTH;

            LoadTileSprite(x, y, DEFAULT_TILE_WIDTH, DEFAULT_TILE_HEIGHT, tileID);
        }

        private static void LoadTileSprite(int x, int y, int width, int height, int tileID)
        {
            // CREATE a new Sprite, call it tempTileSprite and pass in the parameters:
            Sprite tempTileSprite = new Sprite(WorldTileSheet, x, y, width, height);
            // ADD the tempTileSprite to the tileSprites, saving them with a tileID:
            tileSprites.Add(tileID, tempTileSprite);
        }

        /// <summary>
        /// Returns a specfic entityAnimation from the collective entityAnimation Dictionary. Uses a name from the enum to locate and return a specific entityAnimation.
        /// </summary>
        /// <param name="animationGroup">The entityAnimation enum tags.</param>
        /// <returns></returns>
        public static Animation GetAnimation(AnimationGroup animationGroup)
        {
            // RETURN the specific entityAnimation from animations based on the name specificed from the enum:
            return animations[animationGroup];
        }

        public static Sprite GetTileSprite(int tileID)
        {
            return tileSprites[tileID];
        }
    }
}
