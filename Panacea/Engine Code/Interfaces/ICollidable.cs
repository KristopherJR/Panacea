namespace Panacea.Interfaces
{
    public interface ICollidable
    {
        /// <summary>
        /// A Property to determine if the object can be collided with.
        /// </summary>
        bool IsCollidable { get; set; }
    }
}
