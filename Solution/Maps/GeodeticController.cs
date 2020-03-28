using System;
using Maps.Geographical;

namespace Maps
{
    /// <summary>
    /// Responsible for controlling a geodetically controllable object
    /// </summary>
    public class GeodeticController : IGeodeticController
    {
        /// <inheritdoc />
        public Geodetic3d Coordinate
        {
            get => _target.Coordinate;
            set => _target.Coordinate = value;
        }

        /// <inheritdoc />
        public double Heading
        {
            get => _target.Heading;
            set => _target.Heading = value;
        }

        private readonly IGeodeticallyControllable _target;

        /// <summary>
        /// Initializes a new instance of GeodeticController
        /// </summary>
        /// <param name="target">The target to control</param>
        public GeodeticController(IGeodeticallyControllable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            _target = target;
        }

        /// <inheritdoc />
        public void MoveUp(double meters)
        {
            Coordinate = new Geodetic3d(Coordinate.Geodetic2d, Coordinate.Height + meters);
        }

        /// <inheritdoc />
        public void MoveDown(double meters)
        {
            Coordinate = new Geodetic3d(Coordinate.Geodetic2d, Coordinate.Height - meters);
        }

        /// <inheritdoc />
        public void MoveNorth(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, 
                meters, (double)CardinalDirection.North), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveSouth(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, 
                meters, (double)CardinalDirection.South), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveEast(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, 
                meters, (double)CardinalDirection.East), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveWest(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, 
                meters, (double)CardinalDirection.West), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveForward(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, meters, 
                (double)CardinalDirection.North + Heading), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveBackward(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, meters, 
                (double)CardinalDirection.South + Heading), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveRight(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, meters, 
                (double)CardinalDirection.East + Heading), Coordinate.Height);
        }

        /// <inheritdoc />
        public void MoveLeft(double meters)
        {
            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, meters, 
                (double)CardinalDirection.West + Heading), Coordinate.Height);
        }

        /// <inheritdoc />
        public void FaceNorth()
        {
            Heading = (double)CardinalDirection.North;
        }

        /// <inheritdoc />
        public void FaceSouth()
        {
            Heading = (double)CardinalDirection.South;
        }

        /// <inheritdoc />
        public void FaceEast()
        {
            Heading = (double)CardinalDirection.East;
        }

        /// <inheritdoc />
        public void FaceWest()
        {
            Heading = (double)CardinalDirection.West;
        }

        /// <inheritdoc />
        public void TurnClockwise(double degrees)
        {
            Heading += degrees;
        }

        /// <inheritdoc />
        public void TurnCounterclockwise(double degrees)
        {
            Heading -= degrees;
        }
    }
}