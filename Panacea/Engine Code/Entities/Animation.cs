using Microsoft.Xna.Framework;
using Panacea.Game_Code.Game_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Game_Code
{
    public class Animation
    {
        #region FIELDS
        // DECLARE a List of Sprites, call it animationFrames:
        protected List<Sprite> animationFrames;
        // DECLARE an int, call it frameCounter:
        private int frameCounter;
        // DECLARE a float, call it timer:
        private float timer;
        // DECLARE a double, call it frameRate:
        private double frameRate;
        // DECLARE a bool, call it isPlaying:
        private bool isPlaying;
        #endregion

        #region PROPERTIES

        public List<Sprite> AnimationFrames // read-only property
        {
            get { return animationFrames; }
        }
        public int FrameCounter // read-only property
        {
            get { return frameCounter; }
        }
        public Boolean IsPlaying // read-only property
        {
            get { return isPlaying; }
        }
        #endregion

        /// <summary>
        /// Constructor for class Animation.
        /// </summary>
        /// <param name="frameRate">How fast you would like the entityAnimation to play in Frames per Second.</param>
        public Animation(double frameRate)
        {
            // INITIALIZE instance variables:
            this.animationFrames = new List<Sprite>();
            this.frameCounter = 0;
            this.timer = 0;
            this.frameRate = frameRate;
            // START the entityAnimation:
            this.Start();
        }

        /// <summary>
        /// Starts the entityAnimation loop.
        /// </summary>
        public void Start()
        {
            // SET isPlaying to true:
            isPlaying = true;
            // SET the timer to 0, so the entityAnimation plays from the start:
            timer = 0;
        }

        /// <summary>
        /// Stops the entityAnimation loop.
        /// </summary>
        public void Stop()
        {
            // SET isPlaying to true:
            isPlaying = false;
            // SET the timer to 0, so the entityAnimation plays from the start:
            timer = 0;

        }
        public void AddFrame(Sprite sprite)
        {
            animationFrames.Add(sprite);
        }
        public void Update(GameTime gameTime)
        {
            // IF the program is running:
            if(isPlaying)
            {
                // ADD the time elapsed in milliseconds to the timer:
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                // IF the timer is greater than 1 / frameRate*1000:
                if(timer > 1/frameRate*1000)
                {
                    // SET the current frame to the next frame using the remainder:
                    frameCounter = (frameCounter + 1) % animationFrames.Count;
                    // RESET the timer:
                    timer = 0;
                }
            }

        }
    }
}
