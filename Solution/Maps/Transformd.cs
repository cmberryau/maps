using System;

namespace Maps
{
    /// <summary>
    /// Responsible for representing the position, rotation and scale of an objec with double precision
    /// </summary>
    public sealed class Transformd
    {
        /// <summary>
        /// A delegate for handling transform changes
        /// </summary>
        public delegate void TransformChangedHandler();

        /// <summary>
        /// Called when the transform has changed
        /// </summary>
        public event TransformChangedHandler Changed;

        /// <summary>
        /// The complete transform matrix
        /// </summary>
        public Matrix4d Matrix
        {
            get
            {
                var matrix = _matrix;

                if (_parent != null)
                {
                    return _parent.Matrix * matrix;
                }

                return matrix;
            }
        }

        /// <summary>
        /// The complete local transform matrix
        /// </summary>
        public Matrix4d LocalMatrix => _matrix;

        /// <summary>
        /// The identity transform
        /// </summary>
        public static Transformd Identity => new Transformd();

        /// <summary>
        /// The vector facing forward
        /// </summary>
        public Vector3d Forward => Vector3d.Forward * Rotation;

        /// <summary>
        /// The vector facing backwards
        /// </summary>
        public Vector3d Back => -Forward;

        /// <summary>
        /// The vector facing to the right
        /// </summary>
        public Vector3d Right => Vector3d.Right * Rotation;

        /// <summary>
        /// The vector facing to the left
        /// </summary>
        public Vector3d Left => -Right;

        /// <summary>
        /// The vector facing up
        /// </summary>
        public Vector3d Up => Vector3d.Up * Rotation;

        /// <summary>
        /// The vector facing down
        /// </summary>
        public Vector3d Down => -Up;

        /// <summary>
        /// The world position of the object
        /// </summary>
        public Vector3d Position
        {
            get => Matrix * Vector3d.Zero;
            set
            {
                M = new Matrix4d(_matrix.a, _matrix.b, _matrix.c, value.x,
                                 _matrix.e, _matrix.f, _matrix.g, value.y,
                                 _matrix.i, _matrix.j, _matrix.k, value.z,
                                 _matrix.m, _matrix.n, _matrix.o, _matrix.p);
            }
        }

        /// <summary>
        /// The world rotation of the object
        /// </summary>
        public Quaterniond Rotation
        {
            get => RotationFor(Scale, Matrix);
            set
            {
                var delta = value * Rotation.Inverse;
                Rotate(delta);
            }
        }

        /// <summary>
        /// The world scale of the object
        /// </summary>
        public Vector3d Scale
        {
            get
            {
                var m = Matrix;
                var x = new Vector3d(m.a, m.e, m.i).Magnitude;
                var y = new Vector3d(m.b, m.f, m.j).Magnitude;
                var z = new Vector3d(m.c, m.g, m.k).Magnitude;

                return new Vector3d(x, y, z);
            }
            set
            {
                var scale = Scale;
                var x = value.x / scale.x;
                var y = value.y / scale.y;
                var z = value.z / scale.z;

                var ms = new Matrix4d(x, 0, 0, 0,
                                      0, y, 0, 0,
                                      0, 0, z, 0,
                                      0, 0, 0, 1);
                M *= ms;
            }
        }

        /// <summary>
        /// The local position of the object
        /// </summary>
        public Vector3d LocalPosition
        {
            get => M * Vector3d.Zero;
            set
            {
                M = new Matrix4d(_matrix.a, _matrix.b, _matrix.c, value.x,
                                 _matrix.e, _matrix.f, _matrix.g, value.y,
                                 _matrix.i, _matrix.j, _matrix.k, value.z,
                                 _matrix.m, _matrix.n, _matrix.o, _matrix.p);
            }
        }

        /// <summary>
        /// The local rotation of the object
        /// </summary>
        public Quaterniond LocalRotation
        {
            get => RotationFor(LocalScale, LocalMatrix);
            set
            {
                var delta = value * LocalRotation.Inverse;
                Rotate(delta);
            }
        }

        /// <summary>
        /// The scale of the object
        /// </summary>
        public Vector3d LocalScale
        {
            get
            {
                var x = new Vector3d(_matrix.a, _matrix.e, _matrix.i).Magnitude;
                var y = new Vector3d(_matrix.b, _matrix.f, _matrix.j).Magnitude;
                var z = new Vector3d(_matrix.c, _matrix.g, _matrix.k).Magnitude;

                return new Vector3d(x, y, z);
            }
            set
            {
                var localScale = LocalScale;
                var x = value.x / localScale.x;
                var y = value.y / localScale.y;
                var z = value.z / localScale.z;

                var ms = new Matrix4d(x, 0, 0, 0,
                                      0, y, 0, 0,
                                      0, 0, z, 0,
                                      0, 0, 0, 1);
                M *= ms;
            }
        }

        private Matrix4d M
        {
            get => _matrix;
            set
            {
                _matrix = value;

                if (Changed != null)
                {
                    Changed();
                }
            }
        }

        private Matrix4d _matrix;
        private Transformd _parent;

        /// <summary>
        /// Initializes a new instance of Transformd
        /// </summary>
        public Transformd()
        {
            _matrix = GenerateMatrix(Vector3d.Zero, Quaterniond.Identity, Vector3d.One);
        }

        /// <summary>
        /// Initializes a new instance of Transformd
        /// </summary>
        /// <param name="translation">The local translation of the object</param>
        public Transformd(Vector3d translation)
        {
            _matrix = GenerateMatrix(translation, Quaterniond.Identity, Vector3d.One);
        }

        /// <summary>
        /// Initializes a new instance of Transformd
        /// </summary>
        /// <param name="translation">The local translation of the object</param>
        /// <param name="rotation">The local rotation of the object</param>
        public Transformd(Vector3d translation, Quaterniond rotation)
        {
            _matrix = GenerateMatrix(translation, rotation, Vector3d.One);
        }

        /// <summary>
        /// Initializes a new instance of Transformd
        /// </summary>
        /// <param name="translation">The local translation of the object</param>
        /// <param name="rotation">The local rotation of the object</param>
        /// <param name="scale">The local scale of the object</param>
        public Transformd(Vector3d translation, Quaterniond rotation, Vector3d scale)
        {
            _matrix = GenerateMatrix(translation, rotation, scale);
        }

        /// <summary>
        /// Sets the parent of the transform
        /// </summary>
        /// <param name="parent">The new parent transform</param>
        public void SetParent(Transformd parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (ReferenceEquals(parent, this))
            {
                throw new ArgumentException("Cannot parent a transform to itself",
                    nameof(parent));
            }

            _parent = parent;
            _parent.Changed += OnParentChanged;
        }

        /// <summary>
        /// Translates the transform
        /// </summary>
        /// <param name="translation">The translation vector</param>
        /// <param name="world">Should the translation be interpreted as relative to the world?</param>
        public void Translate(Vector3d translation, bool world = false)
        {
            if (world)
            {
                translation *= -Rotation;
            }

            var mt = new Matrix4d(1, 0, 0, translation.x,
                                  0, 1, 0, translation.y,
                                  0, 0, 1, translation.z,
                                  0, 0, 0, 1);

            M *= mt;
        }

        /// <summary>
        /// Rotates the transform
        /// </summary>
        /// <param name="axis">The axis of rotation</param>
        /// <param name="angle">The angle in degrees</param>
        /// <param name="world">Should the axis be interpreted as relative to the world?</param>
        public void Rotate(Vector3d axis, double angle, bool world = false)
        {
            if (world)
            {
                axis = -Rotation * axis;
            }

            var q = Quaterniond.AxisAngle(axis, angle);
            Rotate(q);
        }

        /// <summary>
        /// Rotates the transform
        /// </summary>
        /// <param name="q">The quaternion to rotate by</param>
        public void Rotate(Quaterniond q)
        {
            var mq = q.RotationMatrix4;
            M *= mq;
        }

        /// <summary>
        /// Rotates around a point
        /// </summary>
        /// <param name="point">The point to rotate around</param>
        /// <param name="axis">The axis to rotate around</param>
        /// <param name="angle">The angle to rotate</param>
        public void RotateAround(Vector3d point, Vector3d axis, double angle)
        {
            RotateAround(point, Quaterniond.AxisAngle(axis, angle));
        }

        /// <summary>
        /// Rotates around a point
        /// </summary>
        /// <param name="point">The point to rotate around</param>
        /// <param name="rotation">The rotation to perform</param>
        public void RotateAround(Vector3d point, Quaterniond rotation)
        {
            var back = Matrix4d.Translate(point);
            var to = Matrix4d.Translate(-point);

            var mat = back * rotation.RotationMatrix4 * to;
            M = mat * _matrix;
        }

        private static Matrix4d GenerateMatrix(Vector3d translation, 
            Quaterniond rotation, Vector3d scale)
        {
            var mt = new Matrix4d(1, 0, 0, translation.x,
                                  0, 1, 0, translation.y,
                                  0, 0, 1, translation.z,
                                  0, 0, 0, 1);
            var ms = new Matrix4d(scale.x, 0, 0, 0,
                                  0, scale.y, 0, 0,
                                  0, 0, scale.z, 0,
                                  0, 0, 0, 1);
            return mt * rotation.RotationMatrix4 * ms;
        }

        private static Quaterniond RotationFor(Vector3d scale, Matrix4d mat)
        {
            var sxi = 1 / scale.x;
            var syi = 1 / scale.y;
            var szi = 1 / scale.z;

            var a = mat.a * sxi;
            var b = mat.b * syi;
            var c = mat.c * szi;

            var d = mat.e * sxi;
            var e = mat.f * syi;
            var f = mat.g * szi;

            var g = mat.i * sxi;
            var h = mat.j * syi;
            var i = mat.k * szi;

            return new Quaterniond(new Matrix3d(a, b, c, d, e, f, g, h, i));
        }

        private void OnParentChanged()
        {
            if (Changed != null)
            {
                Changed();
            }
        }
    }
}