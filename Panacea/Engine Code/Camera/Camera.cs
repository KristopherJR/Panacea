using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.Interfaces;
using Panacea.Engine_Code.UserEventArgs;
using Panacea.Interfaces;
using Panacea.UserEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Engine_Code.Camera
{
    public class Camera : IUpdatable, IInputListener
    {
        // DECLARE a Matrix, call it 'transform':
        private Matrix transform;
        // DECLARE a float, call it 'zoomAspect':
        private float zoomAspect;
        // DECLARE a float, call it 'scrollSpeed':
        private float scrollSpeed;
        // DECLARE a Viewport, call it 'viewport':
        private Viewport viewport;
        // DECLARE a GameEntity, call it 'focusedEntity':
        private GameEntity focusedEntity;
        #region PROPERTIES
        public Matrix Transform
        {
            get { return transform; } // read-only
        }
        #endregion

        /// <summary>
        /// Camera Constructor
        /// </summary>
        /// <param name="viewport">A reference to the viewport.</param>
        public Camera(Viewport viewport)
        {
            // INITIALIZE fields:
            transform = new Matrix();
            zoomAspect = 2.0f;
            scrollSpeed = 0.1f;
            this.viewport = viewport;
        }

        /// <summary>
        /// Sets the focus of the camera onto a specific GameEntity.
        /// </summary>
        /// <param name="entity">The entity to focus the camera on.</param>
        public void SetFocus(GameEntity entity)
        {
            // SET focusedEntity to the parameter:
            this.focusedEntity = entity;
        }

        #region IMPLEMENTATION OF IUpdatable
        /// <summary>
        /// Default update loop for an IUpdatable.
        /// </summary>
        /// <param name="gameTime">A reference to the GameTime.</param>
        public void Update(GameTime gameTime)
        {
            transform = Matrix.CreateTranslation(-focusedEntity.EntityLocn.X, -focusedEntity.EntityLocn.Y, 0) * // Main Translation Matrix
                //Matrix.CreateTranslation(-ClampPosition.X + ClampSize.X, -ClampPosition.Y + ClampSize.Y, 0) *
                Matrix.CreateScale(new Vector3(zoomAspect, zoomAspect, 1)) * // Scale Matrix
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0)); // Origin Offset Matrix
        }

        public void OnNewInput(object sender, OnInputEventArgs eventInformation)
        {
            // nothing
        }

        public void OnKeyReleased(object sender, OnKeyReleasedEventArgs eventInformation)
        {
            // nothing
        }

        public void OnNewMouseInput(object sender, OnMouseInputEventArgs eventInformation)
        {
            zoomAspect += eventInformation.ScrollValue * scrollSpeed;
            Console.WriteLine(zoomAspect);
        }

        public Keys[] getKOI()
        {
            Keys[] fake = new Keys[2];
            return fake;      
        }
        #endregion
    }
}
