using Microsoft.Xna.Framework.Input;
using Panacea.Engine_Code.UserEventArgs;
using Panacea.UserEventArgs;

namespace Panacea.Interfaces
{
    interface IInputListener
    {
        /// <summary>
        /// Event Handler for the event OnNewInput, fired from the InputManager. This will be triggered when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        void OnNewInput(object sender, OnInputEventArgs eventInformation);

        /// <summary>
        /// Event Handler for the event OnKeyReleased, fired from the InputManager. This will be triggered when a key is released.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        void OnKeyReleased(object sender, OnKeyReleasedEventArgs eventInformation);

        /// <summary>
        /// Event Handler for the even OnNewMouseInput, fired from the InputManager. This will be triggered when a new mouse input occurs.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        void OnNewMouseInput(object sender, OnMouseInputEventArgs eventInformation);

        /// <summary>
        /// Used to return the KeysOfInterst contained in the Listener.
        /// </summary>
        /// <returns>The array of KeysOfInterest.</returns>
        Keys[] getKOI();
    }
}
