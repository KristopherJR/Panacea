using COMP2451Project.UserEventArgs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP2451Project.Interfaces
{
    interface IInputPublisher
    {
        /// <summary>
        /// Allows a listener to subscribe to the InputManager to watch for changes in input. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="subscriber">A reference to the object subscribing to events from InputManager.</param>
        /// <param name="inputHandler">The input handler to subscribe to the event.</param>
        /// <param name="releaseHandler">The key release handler to subscribe to the event.</param>
        void subscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler);

        /// <summary>
        /// Allows a listener to unsubscribe from the InputManagers events.
        /// </summary>
        /// <param name="subscriber">A reference to the unsubscribing object of InputManager events.</param>
        /// <param name="inputHandler">The input handler to unsubscribe from the event.</param>
        /// <param name="releaseHandler">The key release handler to unsubscribe from the event.</param>
        void unsubscribe(IInputListener subscriber, EventHandler<OnInputEventArgs> inputHandler, EventHandler<OnKeyReleasedEventArgs> releaseHandler);
    }
}
