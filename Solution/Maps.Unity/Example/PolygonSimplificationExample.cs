using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.Geographical.Simplification;
using UnityEngine;

namespace Maps.Unity.Example
{
    /// <summary>
    /// Provides a visual example of polygon simplification
    /// </summary>
    public class PolygonSimplificationExample : MonoBehaviour
    {
        /// <summary>
        /// The epsilon value in meters
        /// </summary>
        [Range(0f, 10f)]
        public float EpsilonMeters = 0;

        private IList<Geodetic2d> _coordinates;
        private float _lastEpsilonMeters;
        private AreaAppearance _areaAppearance;
        private Vector3d _anchor;

        private void Start()
        {
            _coordinates = new []
            {
                new Geodetic2d(48.7609849,11.4176535),
                new Geodetic2d(48.7609818,11.417662),
                new Geodetic2d(48.7609766,11.4176892),
                new Geodetic2d(48.7609788,11.4177251),
                new Geodetic2d(48.7609882,11.4177525),
                new Geodetic2d(48.7610042,11.417774),
                new Geodetic2d(48.7610264,11.4177855),
                new Geodetic2d(48.7610464,11.4177838),
                new Geodetic2d(48.7610688,11.4177674),
                new Geodetic2d(48.7610848,11.4177371),
                new Geodetic2d(48.7610904,11.4177047),
                new Geodetic2d(48.7610876,11.4176724),
                new Geodetic2d(48.7610791,11.4176477),
                new Geodetic2d(48.761067,11.4176295),
                new Geodetic2d(48.7610526,11.4176179),
                new Geodetic2d(48.7610316,11.4176129),
                new Geodetic2d(48.7610091,11.417621),
                new Geodetic2d(48.7609943,11.4176362),
                new Geodetic2d(48.7609677,11.4176078),
                new Geodetic2d(48.7609814,11.4175818),
                new Geodetic2d(48.7609745,11.4175437),
                new Geodetic2d(48.7609626,11.4175271),
                new Geodetic2d(48.7609925,11.4174403),
                new Geodetic2d(48.7609663,11.4174172),
                new Geodetic2d(48.7609715,11.4174038),
                new Geodetic2d(48.7609517,11.4173869),
                new Geodetic2d(48.7616453,11.4154766),
                new Geodetic2d(48.761778,11.4151704),
                new Geodetic2d(48.7618532,11.4150776),
                new Geodetic2d(48.7619129,11.4150425),
                new Geodetic2d(48.7619974,11.4150553),
                new Geodetic2d(48.7624078,11.4151601),
                new Geodetic2d(48.7625246,11.4152027),
                new Geodetic2d(48.762544,11.4152291),
                new Geodetic2d(48.7625923,11.4152633),
                new Geodetic2d(48.7627058,11.4152968),
                new Geodetic2d(48.7627696,11.415254),
                new Geodetic2d(48.7627611,11.415149),
                new Geodetic2d(48.7628022,11.4149489),
                new Geodetic2d(48.762873,11.4144332),
                new Geodetic2d(48.7628708,11.4143214),
                new Geodetic2d(48.7628843,11.4141957),
                new Geodetic2d(48.7629408,11.414085),
                new Geodetic2d(48.7630372,11.4140314),
                new Geodetic2d(48.7631447,11.4140048),
                new Geodetic2d(48.7632024,11.4140135),
                new Geodetic2d(48.7633727,11.4140893),
                new Geodetic2d(48.7634834,11.414109),
                new Geodetic2d(48.7635671,11.4141334),
                new Geodetic2d(48.7635892,11.4141409),
                new Geodetic2d(48.7637588,11.4142074),
                new Geodetic2d(48.7637924,11.4141838),
                new Geodetic2d(48.7638196,11.4140662),
                new Geodetic2d(48.7638742,11.4140805),
                new Geodetic2d(48.7637999,11.4146025),
                new Geodetic2d(48.7634318,11.4144905),
                new Geodetic2d(48.7634354,11.4144357),
                new Geodetic2d(48.763423,11.4143887),
                new Geodetic2d(48.7633564,11.4143536),
                new Geodetic2d(48.7631539,11.4142962),
                new Geodetic2d(48.7630838,11.4143167),
                new Geodetic2d(48.7630503,11.4143749),
                new Geodetic2d(48.763003,11.4147487),
                new Geodetic2d(48.7629395,11.4152159),
                new Geodetic2d(48.7629431,11.4152754),
                new Geodetic2d(48.7629617,11.4152933),
                new Geodetic2d(48.7629108,11.415501),
                new Geodetic2d(48.7628036,11.4156174),
                new Geodetic2d(48.7625995,11.4155383),
                new Geodetic2d(48.7623292,11.4154917),
                new Geodetic2d(48.7620976,11.4154173),
                new Geodetic2d(48.7619413,11.4153409),
                new Geodetic2d(48.7619181,11.4153557),
                new Geodetic2d(48.7612627,11.4171722),
                new Geodetic2d(48.7613125,11.4177126),
                new Geodetic2d(48.7612537,11.4178569),
                new Geodetic2d(48.7611236,11.4182126),
                new Geodetic2d(48.761011,11.4184303),
                new Geodetic2d(48.760945,11.418357),
                new Geodetic2d(48.7609524,11.4183355),
                new Geodetic2d(48.7609523,11.4183085),
                new Geodetic2d(48.7609415,11.4182874),
                new Geodetic2d(48.7610205,11.4181242),
                new Geodetic2d(48.7607405,11.4178864),
                new Geodetic2d(48.7604701,11.4179526),
                new Geodetic2d(48.7605102,11.4183628),
                new Geodetic2d(48.7607257,11.4186957),
                new Geodetic2d(48.7608203,11.4185316),
                new Geodetic2d(48.7608307,11.418541),
                new Geodetic2d(48.7608496,11.4185391),
                new Geodetic2d(48.7608626,11.4185208),
                new Geodetic2d(48.7609143,11.418582),
                new Geodetic2d(48.7608239,11.4187485),
                new Geodetic2d(48.7607168,11.4189652),
                new Geodetic2d(48.7605841,11.4191639),
                new Geodetic2d(48.7604822,11.4192476),
                new Geodetic2d(48.7604067,11.4192669),
                new Geodetic2d(48.7602426,11.419285),
                new Geodetic2d(48.7602057,11.4193114),
                new Geodetic2d(48.7598549,11.4198194),
                new Geodetic2d(48.7595233,11.420327),
                new Geodetic2d(48.7592291,11.4207659),
                new Geodetic2d(48.7594192,11.4217016),
                new Geodetic2d(48.759455,11.4217165),
                new Geodetic2d(48.7595026,11.4216705),
                new Geodetic2d(48.7595983,11.4221464),
                new Geodetic2d(48.7596145,11.4221402),
                new Geodetic2d(48.7596455,11.4222833),
                new Geodetic2d(48.7593547,11.4224179),
                new Geodetic2d(48.7593022,11.4221264),
                new Geodetic2d(48.7589532,11.4222936),
                new Geodetic2d(48.7583714,11.4225623),
                new Geodetic2d(48.7580322,11.4234292),
                new Geodetic2d(48.757846,11.4232558),
                new Geodetic2d(48.7578008,11.4232412),
                new Geodetic2d(48.7577637,11.4232579),
                new Geodetic2d(48.757745,11.4232854),
                new Geodetic2d(48.7577332,11.4233175),
                new Geodetic2d(48.7577302,11.4233643),
                new Geodetic2d(48.7577425,11.4234207),
                new Geodetic2d(48.7577637,11.4234789),
                new Geodetic2d(48.7579136,11.4236081),
                new Geodetic2d(48.757822,11.4238385),
                new Geodetic2d(48.7577139,11.4237186),
                new Geodetic2d(48.7576167,11.4236108),
                new Geodetic2d(48.7575511,11.4233545),
                new Geodetic2d(48.7575743,11.4232375),
                new Geodetic2d(48.757629,11.4230904),
                new Geodetic2d(48.7576807,11.4229956),
                new Geodetic2d(48.7578406,11.4229627),
                new Geodetic2d(48.7579242,11.4230116),
                new Geodetic2d(48.7579553,11.423009),
                new Geodetic2d(48.7579978,11.4229213),
                new Geodetic2d(48.7581135,11.4226259),
                new Geodetic2d(48.7581945,11.4224177),
                new Geodetic2d(48.7582641,11.422286),
                new Geodetic2d(48.7585113,11.4221772),
                new Geodetic2d(48.7589134,11.4219833),
                new Geodetic2d(48.759195,11.4219131),
                new Geodetic2d(48.759227,11.4218579),
                new Geodetic2d(48.7592359,11.4217994),
                new Geodetic2d(48.7591834,11.4216382),
                new Geodetic2d(48.7590692,11.4211759),
                new Geodetic2d(48.7590309,11.4208789),
                new Geodetic2d(48.759022,11.4207874),
                new Geodetic2d(48.7590271,11.4206852),
                new Geodetic2d(48.7590614,11.4205921),
                new Geodetic2d(48.759141,11.4204605),
                new Geodetic2d(48.7593854,11.4201003),
                new Geodetic2d(48.759668,11.4196849),
                new Geodetic2d(48.7598484,11.4194145),
                new Geodetic2d(48.7599988,11.4191913),
                new Geodetic2d(48.7601482,11.4189655),
                new Geodetic2d(48.7602277,11.4188614),
                new Geodetic2d(48.760275,11.4188214),
                new Geodetic2d(48.7603599,11.4186835),
                new Geodetic2d(48.7603729,11.4186184),
                new Geodetic2d(48.7603557,11.4184105),
                new Geodetic2d(48.7603493,11.4183232),
                new Geodetic2d(48.7603258,11.4181106),
                new Geodetic2d(48.7603362,11.4177842),
                new Geodetic2d(48.7603769,11.4177268),
                new Geodetic2d(48.7604165,11.4176987),
                new Geodetic2d(48.7604865,11.4176753),
                new Geodetic2d(48.7604886,11.4177058),
                new Geodetic2d(48.7607992,11.4176396),
                new Geodetic2d(48.760797,11.4176059),
                new Geodetic2d(48.7608758,11.4175873),
                new Geodetic2d(48.7609198,11.4176252),
                new Geodetic2d(48.7609529,11.4176182),
                new Geodetic2d(48.7609849,11.4176535),
            };

            gameObject.AddComponent<MeshRenderer>();
            gameObject.AddComponent<MeshFilter>();

            var polygon = new GeodeticPolygon2d(_coordinates);
            var simplifier = new RamerDouglasPeukerSimplifier(EpsilonMeters);
            polygon = simplifier.Simplify(polygon);
            var area = new Area(Guid.Empty, "", polygon, AreaCategory.Unknown,
                polygon.Area);

            var projection = new WebMercatorProjection(1000000d);
            var appearance = DefaultMapAppearance.Create(16, true);
            _areaAppearance = appearance.AppearanceFor(area);

            throw new NotImplementedException();

            //var mesh = _areaAppearance.Tessellate(area);
            //_anchor = mesh.Bounds.Centre;
            //mesh = mesh.Relative(_anchor, 1f);
            //var unityMesh = mesh.UnityMesh();
            //var filter = gameObject.GetComponent<MeshFilter>();
            //filter.mesh = unityMesh;
        }

        private void Update()
        {
            if (_lastEpsilonMeters != EpsilonMeters)
            {
                var polygon = new GeodeticPolygon2d(_coordinates);
                var simplifier = new RamerDouglasPeukerSimplifier(EpsilonMeters);
                polygon = simplifier.Simplify(polygon);

                throw new NotImplementedException();

                //var area = new Area(Guid.Empty, "", polygon, AreaCategory.Unknown,
                //    polygon.Area);
                //var mesh = _areaAppearance.Tessellate(area);
                //mesh = mesh.Relative(_anchor, 1f);

                //var unityMesh = mesh.UnityMesh();
                //var filter = gameObject.GetComponent<MeshFilter>();
                //filter.mesh = unityMesh;
            }

            _lastEpsilonMeters = EpsilonMeters;
        }
    }
}