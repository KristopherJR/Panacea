using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.Interfaces;
using Panacea.Engine_Code.UserEventArgs;
using Panacea.Interfaces;
using Panacea.UserEventArgs;

namespace Panacea.Engine_Code.Camera
{
    public class Camera : IUpdatable, IInputListener
    {
        #region FIELDS
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
        #endregion
        #region PROPERTIES
        public Matrix Transform
        {
            get { return transform; } // read-only
        }
        #endregion

        /// <summary>
        /// Camera Constructor.
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
            // SET up the transform via Matrix:
            transform = Matrix.CreateTranslation(-focusedEntity.EntityLocn.X, -focusedEntity.EntityLocn.Y, 0) * // Main Translation Matrix
                        Matrix.CreateScale(new Vector3(zoomAspect, zoomAspect, 1)) * // Scale Matrix using zoomAspect
                        Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0)); // Origin Offset Matrix
        }
        /// <summary>
        /// Called whenever a mouse input event is fired from the InputManager.
        /// </summary>
        /// <param name="sender">The object firing the event.</param>
        /// <param name="eventInformation">Information about the event.</param>
        public void OnNewMouseInput(object sender, OnMouseInputEventArgs eventInformation)
        {
            // SET zoomAspect to the scollValue in the eventInformation, * scrollSpeed:
            zoomAspect += eventInformation.ScrollValue * scrollSpeed;
            //Console.WriteLine(zoomAspect);
        }
        #region _
        public void OnNewInput(object sender, OnInputEventArgs eventInformation)
        {
            // nothing
        }

        public void OnKeyReleased(object sender, OnKeyReleasedEventArgs eventInformation)
        {
            // nothing
        }

        public Keys[] getKOI()
        {
            Keys[] fake = new Keys[2];
            return fake;
        }
        #endregion
        #endregion
    }
}
