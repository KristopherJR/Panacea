using Microsoft.Xna.Framework.Input;

namespace Panacea.Interfaces
{
    interface IInputManager : IInputPublisher
    {
        /// <summary>
        /// Called when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="keyInput">Key that was just pressed.</param>
        void OnNewInput(Keys keyInput);

        /// <summary>
        /// Called when a key is released.
        /// </summary>
        /// <param name="keyReleased">Key that was just released.</param>
        void OnKeyReleased(Keys keyReleased);

        /// <summary>
        /// Called when a mouse input occurs.
        /// </summary>
        /// <param name="mouseState">A Snapshot of the mouse state.</param>
        /// <param name="scrollValue">An int representing which direction the scroll wheel is moving.</param>
        void OnNewMouseInput(MouseState mouseState, int scrollValue);

        /// <summary>
        /// Default Update method for classes implementing IInputManager Interface.
        /// </summary>
        void update();
    }
}
