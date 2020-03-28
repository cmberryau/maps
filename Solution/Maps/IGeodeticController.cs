namespace Maps
{
    /// <summary>
    /// Interface for objects with geodetic control
    /// </summary>
    public interface IGeodeticController : IGeodeticallyControllable
    {
        /// <summary>
        /// Moves up
        /// </summary>
        /// <param name="meters"></param>
        void MoveUp(double meters);

        /// <summary>
        /// Moves down
        /// </summary>
        /// <param name="meters"></param>
        void MoveDown(double meters);

        /// <summary>
        /// Performs a north move
        /// </summary>
        /// <param name="meters"></param>
        void MoveNorth(double meters);

        /// <summary>
        /// Performs a south move
        /// </summary>
        /// <param name="meters"></param>
        void MoveSouth(double meters);

        /// <summary>
        /// Performs a east move
        /// </summary>
        /// <param name="meters"></param>
        void MoveEast(double meters);

        /// <summary>
        /// Performs a west move
        /// </summary>
        /// <param name="meters"></param>
        void MoveWest(double meters);

        /// <summary>
        /// Performs a forward move
        /// </summary>
        /// <param name="meters"></param>
        void MoveForward(double meters);

        /// <summary>
        /// Performs a backwards move
        /// </summary>
        /// <param name="meters"></param>
        void MoveBackward(double meters);

        /// <summary>
        /// Performs a right move
        /// </summary>
        /// <param name="meters"></param>
        void MoveRight(double meters);

        /// <summary>
        /// Performs a left move
        /// </summary>
        /// <param name="meters"></param>
        void MoveLeft(double meters);

        /// <summary>
        /// Faces north
        /// </summary>
        void FaceNorth();

        /// <summary>
        /// Faces south
        /// </summary>
        void FaceSouth();

        /// <summary>
        /// Faces east
        /// </summary>
        void FaceEast();

        /// <summary>
        /// Faces west
        /// </summary>
        void FaceWest();

        /// <summary>
        /// Rotates clockwise
        /// </summary>
        /// <param name="degrees"></param>
        void TurnClockwise(double degrees);

        /// <summary>
        /// Rotates counterclockwise
        /// </summary>
        /// <param name="degrees"></param>
        void TurnCounterclockwise(double degrees);
    }
}