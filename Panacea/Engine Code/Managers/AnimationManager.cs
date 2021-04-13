using Microsoft.Xna.Framework;
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
    }
}
