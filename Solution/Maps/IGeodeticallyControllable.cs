namespace Maps
{
    /// <summary>
    /// A combined interface for geodetically controllable objects
    /// </summary>
    public interface IGeodeticallyControllable : ICoordinateControllable, 
        IHeadingControllable
    {
        
    }
}