using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Features;

namespace Maps.Geographical.Combining
{
    /// <summary>
    /// Responsible for combining features
    /// </summary>
    public class FeatureCombiner
    {
        /// <summary>
        /// Combines the given features
        /// </summary>
        /// <param name="features">The features to combine</param>
        /// <returns>A list of combined features</returns>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null 
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if any element of 
        /// <paramref name="features"/> is null</exception>
        public IList<Feature> Combine(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            features.AssertNoNullEntries();

            // sort features into a concrete type collection
            var collection = new FeatureCollection(features);
            var result = new List<Feature>();

            PassThroughPlaces(collection, result);
            CombineSegments(collection, result);
            CombineAreas(collection, result);

            return result;
        }

        private static void PassThroughPlaces(IFeatureCollection collection, IList<Feature> result)
        {
            var placeCount = collection.PlaceCount;
            for (var i = 0; i < placeCount; ++i)
            {
                result.Add(collection.Places[i]);
            }
        }

        private static void CombineSegments(IFeatureCollection collection, IList<Feature> result)
        {
            var segmentCount = collection.SegmentCount;
            var groupDict = new Dictionary<Feature, IList<Segment>>();
            var groupList = new List<IList<Segment>>();
            var grouped = new bool[segmentCount];
            var groupedCount = 0;

            // arrange combinable segments into groups
            for (var i = 0; i < segmentCount && groupedCount < segmentCount; ++i)
            {
                var outer = collection.Segments[i];

                for (var j = 0; j < segmentCount && groupedCount < segmentCount; ++j)
                {
                    var inner = collection.Segments[j];

                    // don't try to merge onto self, test for possible combination
                    if (i != j && outer.CanCombine(inner))
                    {
                        groupDict.TryGetValue(outer, out IList<Segment> outerGroup);
                        groupDict.TryGetValue(inner, out IList<Segment> innerGroup);

                        // both are grouped already
                        if (outerGroup != null && innerGroup != null)
                        {
                            // seperate groups, merge into the outer group
                            if (!ReferenceEquals(outerGroup, innerGroup))
                            {
                                var innerGroupCount = innerGroup.Count;
                                for (var k = 0; k < innerGroupCount; ++k)
                                {
                                    // set the elements group to the outer group
                                    groupDict[innerGroup[k]] = outerGroup;
                                    // add the element to the outer group
                                    outerGroup.Add(innerGroup[k]);
                                }

                                // clear the outer group
                                innerGroup.Clear();
                            }
                        }
                        else
                        {
                            // inner has no group, outer has one
                            if (innerGroup == null && outerGroup != null)
                            {
                                groupDict[inner] = outerGroup;
                                outerGroup.Add(inner);
                                grouped[j] = true;
                                ++groupedCount;
                            }
                            // outer has no group, inner has one
                            else if (innerGroup != null)
                            {
                                groupDict[outer] = innerGroup;
                                innerGroup.Add(outer);
                                grouped[i] = true;
                                ++groupedCount;
                            }
                            // neither have a group
                            else
                            {
                                // create a new group
                                groupDict[outer] = new List<Segment>
                                {
                                    outer,
                                    inner
                                };

                                groupDict[inner] = groupDict[outer];
                                groupList.Add(groupDict[outer]);

                                grouped[i] = true;
                                grouped[j] = true;

                                groupedCount += 2;
                            }
                        }
                    }
                }
            }

            // combine the groups
            if (groupedCount > 0)
            {
                var groupCount = groupList.Count;
                for (var i = 0; i < groupCount; ++i)
                {
                    if (groupList[i].Count == 1)
                    {
                        result.Add(groupList[i][0]);
                    }
                    else if (groupList[i].Count > 0)
                    {
                        var combined = Segment.Combine(groupList[i]);
                        var combinedCount = combined.Count;
                        for (var j = 0; j < combinedCount; ++j)
                        {
                            result.Add(combined[j]);
                        }
                    }
                }
            }

            // add the ungrouped areas
            if (groupedCount < segmentCount)
            {
                for (var i = 0; i < segmentCount; ++i)
                {
                    if (!grouped[i])
                    {
                        result.Add(collection.Segments[i]);
                    }
                }
            }
        }

        private static void CombineAreas(IFeatureCollection collection, IList<Feature> result)
        {
            var areaCount = collection.AreaCount;
            var groupDict = new Dictionary<Feature, IList<Area>>();
            var groupList = new List<IList<Area>>();
            var grouped = new bool[areaCount];
            var groupedCount = 0;

            // arrange combinable areas into groups
            for (var i = 0; i < areaCount && groupedCount < areaCount; ++i)
            {
                var outer = collection.Areas[i];

                for (var j = 0; j < areaCount && groupedCount < areaCount; ++j)
                {
                    var inner = collection.Areas[j];

                    // don't try to merge onto self, test for possible combination
                    if (i != j && outer.CouldCombine(inner))
                    {
                        groupDict.TryGetValue(outer, out IList<Area> outerGroup);
                        groupDict.TryGetValue(inner, out IList<Area> innerGroup);

                        // both are grouped already
                        if (outerGroup != null && innerGroup != null)
                        {
                            // seperate groups, merge into the outer group
                            if (!ReferenceEquals(outerGroup, innerGroup))
                            {
                                var innerGroupCount = innerGroup.Count;
                                for (var k = 0; k < innerGroupCount; ++k)
                                {
                                    // set the elements group to the outer group
                                    groupDict[innerGroup[k]] = outerGroup;
                                    // add the element to the outer group
                                    outerGroup.Add(innerGroup[k]);
                                }

                                // clear the outer group
                                innerGroup.Clear();
                            }
                        }
                        else
                        {
                            // inner has no group, outer has one
                            if (innerGroup == null && outerGroup != null)
                            {
                                groupDict[inner] = outerGroup;
                                outerGroup.Add(inner);
                                grouped[j] = true;
                                ++groupedCount;
                            } 
                            // outer has no group, inner has one
                            else if (innerGroup != null)
                            {
                                groupDict[outer] = innerGroup;
                                innerGroup.Add(outer);
                                grouped[i] = true;
                                ++groupedCount;
                            }
                            // neither have a group
                            else
                            {
                                // create a new group
                                groupDict[outer] = new List<Area>
                                {
                                    outer,
                                    inner
                                };

                                groupDict[inner] = groupDict[outer];
                                groupList.Add(groupDict[outer]);

                                grouped[i] = true;
                                grouped[j] = true;

                                groupedCount += 2;
                            }
                        }
                    }
                }
            }

            // combine the groups
            if (groupedCount > 0)
            {
                var groupCount = groupList.Count;
                for (var i = 0; i < groupCount; ++i)
                {
                    if (groupList[i].Count == 1)
                    {
                        result.Add(groupList[i][0]);
                    }
                    else if (groupList[i].Count > 0)
                    {
                        var combined = Area.Combine(groupList[i]);
                        var combinedCount = combined.Count;
                        for (var j = 0; j < combinedCount; ++j)
                        {
                            result.Add(combined[j]);
                        }
                    }
                }
            }

            // add the ungrouped areas
            if (groupedCount < areaCount)
            {
                for (var i = 0; i < areaCount; ++i)
                {
                    if (!grouped[i])
                    {
                        result.Add(collection.Areas[i]);
                    }
                }
            }
        }
    }
}