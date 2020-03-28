using System;
using System.Collections.Generic;
using System.Reflection;
using Maps.Appearance;
using Maps.Geographical;
using Maps.Geographical.Features;
using UnityEngine;

namespace Maps.Unity.Example
{
    /// <summary>
    /// Provides a quick test UI for the Maps.Unity.Map API
    /// </summary>
    public class MapAPIExample : MonoBehaviour
    {
        /// <summary>
        /// The map this MapAPIExample instance is associated with
        /// </summary>
        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnassignedField.Global
        public Map Map;

        // debug gui stuff
        private Vector2 _buttonSize;
        private Vector2 _centre;

        // movement stuff
        private const double DefaultMeters = 10;
        private double _heightMeters = DefaultMeters;
        private double _offsetMeters = DefaultMeters;

        // appearance
        private TiledMapAppearance _day;
        private TiledMapAppearance _night;

        // dynamic feature
        private IDynamicFeature _dynamic;

        private void Start()
        {
            if (Map == null)
            {
                throw new NullReferenceException($"{nameof(Map)} must be assigned");
            }

            // set up debug gui stuff
            if (Application.isMobilePlatform)
            {
                _buttonSize = new Vector2(300f, 300f);
            }
            else
            {
                _buttonSize = new Vector2(100f, 100f);
            }

            _centre = new Vector2(Screen.width - _buttonSize.x, Screen.height);
        }

        private IDynamicFeature CreateRoute()
        {
            var points = new List<Geodetic2d>
            {
                new Geodetic2d(48.7639493, 11.4193614),
                new Geodetic2d(48.7639668, 11.4196805),
                new Geodetic2d(48.7639755, 11.4200862),
                new Geodetic2d(48.7639692, 11.4204398),
                new Geodetic2d(48.7639509, 11.4207894)
            };

            return new RouteSection("Test Route", points);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 2,
                _centre.y - _buttonSize.y * 2.5f), _buttonSize), "<"))
            {
                if (Map != null)
                {
                    Map.TurnCounterclockwise(1d);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x, _centre.y - _buttonSize.y *
                2.5f), _buttonSize), ">"))
            {
                if (Map != null)
                {
                    Map.TurnClockwise(1d);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x,
                _centre.y - _buttonSize.y * 4), _buttonSize), "^^"))
            {
                if (Map != null)
                {
                    Map.Tilt++;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x,
                _centre.y - _buttonSize.y * 3), _buttonSize), "vv"))
            {
                if (Map != null)
                {
                    Map.Tilt--;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 2,
                _centre.y - _buttonSize.y * 1.5f), _buttonSize), $"<- {_offsetMeters}m"))
            {
                if (Map != null)
                {
                    Map.MoveLeft(_offsetMeters);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x, 
                _centre.y - _buttonSize.y * 2),_buttonSize), $"^ {_offsetMeters}m"))
            {
                if (Map != null)
                {
                    Map.MoveForward(_offsetMeters);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x, _centre.y - 
                _buttonSize.y * 1.5f), _buttonSize), $"{_offsetMeters}m ->"))
            {
                if (Map != null)
                {
                    Map.MoveRight(_offsetMeters);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x,
                _centre.y - _buttonSize.y), _buttonSize), $"v {_offsetMeters}m"))
            {
                if (Map != null)
                {
                    Map.MoveBackward(_offsetMeters);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 3,
                _centre.y - _buttonSize.y * 2), _buttonSize), $"+ {_heightMeters}m"))
            {
                if (Map != null)
                {
                    Map.MoveUp(_heightMeters);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 3,
                _centre.y - _buttonSize.y), _buttonSize), $"- {_heightMeters}m"))
            {
                if (Map != null)
                {
                    Map.MoveDown(_heightMeters);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 4,
                _centre.y - _buttonSize.y * 2), _buttonSize), "*10"))
            {
                _heightMeters *= 10;
                _offsetMeters *= 10;
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 3,
                _centre.y - _buttonSize.y * 3), _buttonSize), "Day"))
            {
                if (_day == null)
                {
                    // get original appearance
                    if (Map != null)
                    {
                        _day = Map.Appearance;
                    }
                    else
                    {
                        _day = TiledMapAppearance.DefaultDay;
                    }

                    _night = TiledMapAppearance.DefaultNight;
                }

                if (Map != null)
                {
                    Map.Appearance = _day;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 4,
                _centre.y - _buttonSize.y * 3), _buttonSize), "Night"))
            {
                if (_day == null)
                {
                    // get original appearance
                    if (Map != null)
                    {
                        _day = Map.Appearance;
                    }
                    else
                    {
                        _day = TiledMapAppearance.DefaultDay;
                    }

                    _night = TiledMapAppearance.DefaultNight;
                }

                if (Map != null)
                {
                    Map.Appearance = _night;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 4,
                _centre.y - _buttonSize.y), _buttonSize), "/10"))
            {
                _heightMeters /= 10;
                _offsetMeters /= 10;
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 7,
                _centre.y - _buttonSize.y * 3f), _buttonSize), "Spawn Route"))
            {
                if (Map != null)
                {
                    _dynamic = CreateRoute();
                    Map.Add(_dynamic);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 7,
                _centre.y - _buttonSize.y * 2f), _buttonSize), "Spawn Dynamic"))
            {
                if (Map != null)
                {
                    _dynamic = new CarIcon("Car")
                    {
                        Coordinate = new Geodetic3d(Map.Coordinate.Geodetic2d, 0d)
                    };
                }

                if (Map != null)
                {
                    Map.Add(_dynamic);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 7f,
                _centre.y - _buttonSize.y), _buttonSize), "Dispose Dynamic"))
            {
                if (_dynamic != null)
                {
                    _dynamic.Dispose();
                    _dynamic = null;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 10f,
                _centre.y - _buttonSize.y * 2.5f), _buttonSize), "<"))
            {
                if (_dynamic != null)
                {
                    _dynamic.Heading -= 5;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 8f,
                _centre.y - _buttonSize.y * 2.5f), _buttonSize), ">"))
            {
                if (_dynamic != null)
                {
                    _dynamic.Heading += 5;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 10f,
                _centre.y - _buttonSize.y * 3.5f), _buttonSize), "Hide"))
            {
                if (_dynamic != null)
                {
                    _dynamic.Active = false;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 8f,
                _centre.y - _buttonSize.y * 3.5f), _buttonSize), "Show"))
            {
                if (_dynamic != null)
                {
                    _dynamic.Active = true;
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 10f,
                _centre.y - _buttonSize.y * 1.5f), _buttonSize), $"<- {_offsetMeters}m"))
            {
                if (_dynamic != null)
                {
                    OffsetCoordinate(_dynamic, _offsetMeters, 
                        (double) CardinalDirection.West + _dynamic.Heading, 1d);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 9f,
                _centre.y - _buttonSize.y * 2), _buttonSize), $"^ {_offsetMeters}m"))
            {
                if (_dynamic != null)
                {
                    OffsetCoordinate(_dynamic, _offsetMeters, 
                        (double) CardinalDirection.North + _dynamic.Heading, 1d);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 8f,
                _centre.y - _buttonSize.y * 1.5f), _buttonSize), $"{_offsetMeters}m ->"))
            {
                if (_dynamic != null)
                {
                    OffsetCoordinate(_dynamic, _offsetMeters, 
                        (double) CardinalDirection.East + _dynamic.Heading, 1d);
                }
            }

            if (GUI.Button(new Rect(new Vector2(_centre.x - _buttonSize.x * 9f,
                _centre.y - _buttonSize.y), _buttonSize), $"v {_offsetMeters}m"))
            {
                if (_dynamic != null)
                {
                    OffsetCoordinate(_dynamic, _offsetMeters, 
                        (double) CardinalDirection.South + _dynamic.Heading, 1d);
                }
            }
        }

        private void OffsetCoordinate(IGeodeticallyControllable target,
            double meters, double heading, double duration = 0d)
        {
            target.Coordinate = new Geodetic3d(Geodetic2d.Offset(
                target.Coordinate.Geodetic2d, meters, heading), 
                target.Coordinate.Height);
        }
    }
}