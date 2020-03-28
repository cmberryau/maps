namespace Maps.Rendering
{
    /// <summary>
    /// Represents a camera frustum
    /// </summary>
    public class CameraFrustum
    {
        /// <summary>
        /// The view point
        /// </summary>
        public Vector3d ViewPoint
        {
            get;
        }

        /// <summary>
        /// The top left point of the far plane
        /// </summary>
        public Vector3d FarTopLeft
        {
            get;
        }

        /// <summary>
        /// The top right point of the far plane
        /// </summary>
        public Vector3d FarTopRight
        {
            get;
        }

        /// <summary>
        /// The bottom right point of the far plane
        /// </summary>
        public Vector3d FarBottomRight
        {
            get;
        }

        /// <summary>
        /// The bottom left point of the far plane
        /// </summary>
        public Vector3d FarBottomLeft
        {
            get;
        }

        /// <summary>
        /// Initialises a new instawnce of CameraFrustum
        /// </summary>
        /// <param name="viewPoint">The view point</param>
        /// <param name="farTopLeft">The top left point of the far plane</param>
        /// <param name="farTopRight"></param>
        /// <param name="farBottomRight"></param>
        /// <param name="farBottomLeft"></param>
        public CameraFrustum(Vector3d viewPoint, Vector3d farTopLeft, 
            Vector3d farTopRight, Vector3d farBottomRight, Vector3d farBottomLeft)
        {
            ViewPoint = viewPoint;
            FarTopLeft = farTopLeft;
            FarTopRight = farTopRight;
            FarBottomRight = farBottomRight;
            FarBottomLeft = farBottomLeft;
        }
    }
}