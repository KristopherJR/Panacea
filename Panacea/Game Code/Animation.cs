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
        protected List<Sprite> animationFrames;
        private int frameCounter;
        private float timer;
        private double frameRate;

        private Boolean isPlaying;

        #region PROPERTIES

        public List<Sprite> AnimationFrames
        {
            get { return animationFrames; }
        }
        public int FrameCounter
        {
            get { return frameCounter; }
        }
        public Boolean IsPlaying
        {
            get { return isPlaying; }
        }
        #endregion

        public Animation(double frameRate)
        {
            this.animationFrames = new List<Sprite>();
            this.frameCounter = 0;
            this.timer = 0;
            this.frameRate = frameRate;
            this.Start();
        }

        public void Start()
        {
            isPlaying = true;

        }
        public void Stop()
        {
            isPlaying = false;
            timer = 0;

        }
        public void AddFrame(Sprite sprite)
        {
            animationFrames.Add(sprite);
        }
        public void Update(GameTime gameTime)
        {
            if(isPlaying)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(timer > 1/frameRate*1000)
                {
                    frameCounter = (frameCounter + 1) % animationFrames.Count;
                    timer = 0;
                }
            }

        }
    }
}
