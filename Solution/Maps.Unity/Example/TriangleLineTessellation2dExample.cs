using System;
using System.Collections.Generic;
using Maps.Geometry.Tessellation;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Example
{
    public class TriangleLineTessellation2dExample : MonoBehaviour
    {
        public Vector2[] Points;
        public Material Material;

        private MeshFilter _filter;
        private ILineTessellator _tessellator;
        private float _width;

        private void Start()
        {
            if (Points == null || Points.Length == 0)
            {
                return;
            }

            _width = 0.1f;
            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = Material;
            _filter = gameObject.AddComponent<MeshFilter>();
            _tessellator = new TriangleLineTessellator2d(_width);
        }

        private IList<Vector3d> Vector3ds(IList<Vector2> points)
        {
            var vectors = new Vector3d[points.Count];

            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];
                vectors[i] = new Vector3d(point.x, point.y, 0d);
            }

            return vectors;
        }

        private void LateUpdate()
        {
            var vecs = Vector3ds(Points);
            var meshd = _tessellator.Tessellate(vecs);
            _filter.mesh = meshd.UnityMesh();

            var along = Points[1] - Points[0];
            var lastAlongMag = along.magnitude;
            var perp = new Vector2(-along.y, along.x).normalized * _width;

            Debug.DrawLine(Points[0], Points[0] + perp, Color.red);

            var lastPerp = perp;

            for (var i = 1; i < Points.Length - 1; ++i)
            {
                along = Points[i + 1] - Points[i];
                var alongMag = along.magnitude;
                perp = new Vector2(-along.y, along.x).normalized * _width;

                var theta = Vector2.Angle(lastPerp, perp);
                if (theta > 15d)
                {
                    var det = Cross(lastPerp, perp);
                    if (Mathf.Abs(det) < Mathf.Epsilon)
                    {
                        det = 1f;
                    }

                    var sign = Math.Sign(det);
                    var gamma = 180f - theta;
                    // get the adjacent magnitude
                    var aMag = _width / Mathf.Tan(gamma * 0.5f * Mathf.Deg2Rad);
                    // clamp limits to current and last size
                    aMag = Mathf.Min(aMag, Mathf.Min(alongMag, lastAlongMag));
                    // apply that magnitude to the adjacent direction
                    var a = Points[i] - (Points[i] - Points[i + 1]).normalized * aMag;
                    // resolve the point
                    var p = a + perp * sign;

                    Debug.DrawLine(Points[i], p, Color.magenta);
                }

                Debug.DrawLine(Points[i], Points[i] + perp, Color.red);
                lastPerp = perp;
                lastAlongMag = alongMag;
            }
        }

        private static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }
    }
}