namespace Maps.Geometry
{
    /// <summary>
    /// 2 dimensional ray with double precision
    /// </summary>
    public class Ray2d
    {
        /// <summary>
        /// The origin of the ray
        /// </summary>
        public readonly Vector2d Origin;

        /// <summary>
        /// The direction of the ray
        /// </summary>
        public readonly Vector2d Direction;

        /// <summary>
        /// Initializes a new instance of Ray2d
        /// </summary>
        /// <param name="origin">The origin of the ray</param>
        /// <param name="direction">The direction of the ray</param>
        public Ray2d(Vector2d origin, Vector2d direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}