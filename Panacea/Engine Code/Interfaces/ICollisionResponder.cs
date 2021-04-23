namespace Panacea.Interfaces
{
    interface ICollisionResponder
    {
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        void CheckAndRespond(ICollidable collidee);
    }
}
