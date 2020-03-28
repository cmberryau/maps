using System;
using System.Collections.Generic;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Tessellates input lines into triangle meshes
    /// </summary>
    public class TriangleLineTessellator2d : ILineTessellator
    {
        private readonly double _width;

        /// <summary>
        /// Initializes a new instance of TriangleTessellator2d
        /// </summary>
        /// <param name="width">The width of lines to output</param>
        public TriangleLineTessellator2d(double width)
        {
            _width = width;
        }

        /// <inheritdoc />
        public Mesh Tessellate(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 2)
            {
                throw new ArgumentException("Must have at least two points",
                    nameof(points));
            }

            var pointsxy = points.xy();
            var degreesPerDivision = 15d;
            var capDivisions = 6;
            var capRotation = 180d / capDivisions;
            var norm = Vector3d.Back;

            var vertices = new List<Vector2d>();
            var normals = new List<Vector3d>();
            var uvs = new List<IList<Vector4d>>
            {
                new List<Vector4d>()
            };
            var uvs0 = uvs[0];
            var indices = new List<int>();

            var point = pointsxy[0];
            var along = pointsxy[1] - point;
            var lastAlongMag = along.Magnitude;
            var perp = along.Perpendicular.Normalised * _width;

            var u = 0d;
            var v = 1d;

            AddCap(perp, point, 0, false, u, capRotation, capDivisions, norm, indices,
                vertices, normals, uvs0);

            vertices.Add(point - perp);
            vertices.Add(point + perp);
            normals.Add(norm);
            normals.Add(norm);
            var edgeNorm = (vertices[vertices.Count - 2] - point).Normalised;
            uvs0.Add(new Vector4d(u, 0f + v, edgeNorm.x, edgeNorm.y));
            edgeNorm = (vertices[vertices.Count - 1] - point).Normalised;
            uvs0.Add(new Vector4d(u, 1f - v, edgeNorm.x, edgeNorm.y));

            var lastPerp = perp;
            var pointsCountT1 = pointsxy.Count - 1;
            var strip = new LineStrip2d(pointsxy);
            var distance = strip.Distance;
            var lastv1 = vertices.Count - 1;

            for (var i = 1; i < pointsCountT1; ++i)
            {
                along = pointsxy[i + 1] - pointsxy[i];
                var alongMag = along.Magnitude;
                // hack filtering
                if (alongMag < Mathd.Epsilon)
                {
                    continue;
                }

                u = strip.DistanceAt(i) / distance;
                perp = along.Perpendicular.Normalised * _width;

                // determine the angle between the line segments
                var theta = Vector2d.Angle(lastPerp, perp);
                if (theta > degreesPerDivision)
                {
                    // calculate determinant, filtering out tiny determinants
                    var det = Vector2d.Cross(lastPerp, perp);
                    if (Math.Abs(det) < Mathd.Epsilon)
                    {
                        det = 1d;
                    }

                    // determine the division angle and count
                    var sign = Math.Sign(det);
                    var divisions = (int)Math.Max(Math.Ceiling(theta / degreesPerDivision), 1);
                    var divisionAngle = theta / Math.Max(divisions - 1, 1) * sign;

                    // the internal angle
                    var gamma = 180d - theta;
                    // get the adjacent magnitude
                    var aMag = _width / Math.Tan(gamma * 0.5d * Mathd.Deg2Rad);
                    // clamp limits to current and last size
                    aMag = Math.Min(aMag, Math.Min(alongMag, lastAlongMag));

                    Vector2d p;
                    if (lastAlongMag > alongMag)
                    {
                        // apply that magnitude to the adjacent direction
                        var a = pointsxy[i] - (pointsxy[i] - pointsxy[i - 1]).Normalised * aMag;
                        // resolve the point
                        p = a + lastPerp * sign;
                    }
                    else
                    {
                        // apply that magnitude to the adjacent direction
                        var a = pointsxy[i] - (pointsxy[i] - pointsxy[i + 1]).Normalised * aMag;
                        // resolve the point
                        p = a + perp * sign;
                    }

                    var ccw = det > 0d;
                    for (var j = 0; j < divisions; ++j)
                    {
                        var rotated = lastPerp.Rotate(j * divisionAngle) * -sign;
                        point = pointsxy[i];

                        // ccw turn
                        if (ccw)
                        {
                            vertices.Add(point + rotated);
                            vertices.Add(p);
                        }
                        // cw turn
                        else
                        {
                            vertices.Add(p);
                            vertices.Add(point + rotated);
                        }

                        normals.Add(norm);
                        normals.Add(norm);
                        edgeNorm = (vertices[vertices.Count - 2] - point).Normalised;
                        uvs0.Add(new Vector4d(u, 0f + v, edgeNorm.x, edgeNorm.y));
                        edgeNorm = (vertices[vertices.Count - 1] - point).Normalised;
                        uvs0.Add(new Vector4d(u, 1f - v, edgeNorm.x, edgeNorm.y));

                        if (j > 0)
                        {
                            indices.Add(vertices.Count - 1);
                            indices.Add(vertices.Count - 2);
                            indices.Add(vertices.Count - 3);
                            indices.Add(vertices.Count - 2);
                            indices.Add(vertices.Count - 4);
                            indices.Add(vertices.Count - 3);
                        }
                    }
                }
                else
                {
                    point = pointsxy[i];
                    vertices.Add(point - perp);
                    vertices.Add(point + perp);
                    normals.Add(norm);
                    normals.Add(norm);
                    edgeNorm = (vertices[vertices.Count - 2] - point).Normalised;
                    uvs0.Add(new Vector4d(u, 0f + v, edgeNorm.x, edgeNorm.y));
                    edgeNorm = (vertices[vertices.Count - 1] - point).Normalised;
                    uvs0.Add(new Vector4d(u, 1f - v, edgeNorm.x, edgeNorm.y));
                }

                indices.Add(lastv1 - 1);
                indices.Add(lastv1);
                indices.Add(lastv1 + 1);
                indices.Add(lastv1);
                indices.Add(lastv1 + 2);
                indices.Add(lastv1 + 1);

                lastv1 = vertices.Count - 1;
                lastPerp = perp;
                lastAlongMag = alongMag;
            }

            point = pointsxy[pointsCountT1];
            along = point - pointsxy[pointsCountT1 - 1];
            perp = along.Perpendicular.Normalised * _width;

            vertices.Add(point - perp);
            vertices.Add(point + perp);
            normals.Add(norm);
            normals.Add(norm);
            edgeNorm = (vertices[vertices.Count - 2] - point).Normalised;
            uvs0.Add(new Vector4d(u, 0f + v, edgeNorm.x, edgeNorm.y));
            edgeNorm = (vertices[vertices.Count - 1] - point).Normalised;
            uvs0.Add(new Vector4d(u, 1f - v, edgeNorm.x, edgeNorm.y));

            lastv1 = vertices.Count - 1;

            indices.Add(lastv1 - 3);
            indices.Add(lastv1);
            indices.Add(lastv1 - 1);
            indices.Add(lastv1 - 3);
            indices.Add(lastv1 - 2);
            indices.Add(lastv1);

            if (!strip.Closed)
            {
                AddCap(perp, point, lastv1, true, u, capRotation, capDivisions, norm, 
                    indices, vertices, normals, uvs0);
            }

            var mesh = new Mesh(Topology.Triangles);

            mesh.SetVertices(vertices);
            mesh.SetNormals(normals);
            mesh.SetUVs(uvs);
            mesh.SetIndices(indices);

            return mesh;
        }

        private static void AddCap(Vector2d perp, Vector2d centre, int last, bool end,
                                   double u, double rotation, int divisions, Vector3d norm,
                                   IList<int> indices, IList<Vector2d> vertices,
                                   IList<Vector3d> normals, IList<Vector4d> uvs)
        {
            var angleSign = 1d;

            // if its an end cap, sign is flipped
            if (end)
            {
                angleSign = -1d;
            }

            var d = 1;
            var angle = angleSign * rotation;
            var rotated = perp.Rotate(angle);

            vertices.Add(centre + rotated);
            normals.Add(norm);
            var edgeNorm = (vertices[vertices.Count - 1] - centre).Normalised;
            uvs.Add(new Vector4d(u, 1d, edgeNorm.x, edgeNorm.y));

            if (end)
            {
                indices.Add(last + d);
                indices.Add(last - 1);
                indices.Add(last);
            }
            else
            {
                indices.Add(d - 1);
                indices.Add(divisions);
                indices.Add(divisions - 1);
            }

            for (d = 2; d < divisions; ++d)
            {
                angle = angleSign * rotation * d;
                rotated = perp.Rotate(angle);

                vertices.Add(centre + rotated);
                normals.Add(norm);
                edgeNorm = (vertices[vertices.Count - 1] - centre).Normalised;
                uvs.Add(new Vector4d(u, 1d, edgeNorm.x, edgeNorm.y));

                if (end)
                {
                    indices.Add(last + d);
                    indices.Add(last - 1);
                    indices.Add(last + d - 1);
                }
                else
                {
                    indices.Add(d - 1);
                    indices.Add(d - 2);
                    indices.Add(divisions - 1);
                }
            }
        }
    }
}