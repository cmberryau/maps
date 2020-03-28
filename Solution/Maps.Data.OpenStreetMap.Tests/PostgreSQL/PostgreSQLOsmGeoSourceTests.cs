using System;
using System.Collections.Generic;
using Maps.Data.OpenStreetMap.PostgreSQL;
using Maps.Geographical;
using Maps.Tests;
using Npgsql;
using NUnit.Framework;

namespace Maps.Data.OpenStreetMap.Tests.PostgreSQL
{
    /// <summary>
    /// Series of tests for the PostgreSQLOsmGeoSource class
    /// </summary>
    [TestFixture]
    public class PostgreSQLOsmGeoSourceTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            Assert.IsNotNull(geoSource);
        }

        /// <summary>
        /// Tests the constructor when given a null client
        /// </summary>
        [Test]
        public void TestConstructorNullClient()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get nodes
        /// </summary>
        [Test]
        public void TestGetNodesBox()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var nodes = geoSource.GetNodes(TestUtilities.BigIngolstadtBox);

            Assert.IsNotNull(nodes);
            Assert.IsNotEmpty(nodes);
        }

        /// <summary>
        /// Tests the reaction to getting nodes when given an box which covers no geometries
        /// </summary>
        [Test]
        public void TestGetNodesBoxEmptyArea()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get nodes
        /// </summary>
        [Test]
        public void TestGetNodesSingleId()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var nodes = geoSource.GetNodes(new List<long>
            {
                // see http://www.openstreetmap.org/node/3630753431
                3630753431L
            });

            Assert.IsNotNull(nodes);
            Assert.IsNotEmpty(nodes);
            Assert.IsTrue(nodes.Count == 1);

            var node = nodes[0];
            var expected = new Geodetic2d(48.76411d, 11.4209873d);
            TestUtilities.AssertThatGeodetic2dsAreEqual(expected, node.Coordinate);
            var tags = node.Tags;
            Assert.IsTrue(tags.Count == 1);
            Assert.IsTrue(tags.ContainsKey("natural"));
            Assert.IsTrue(tags["natural"] == "tree");
        }

        /// <summary>
        /// Tests the reaction to getting nodes when given an invalid id
        /// </summary>
        [Test]
        public void TestGetNodesInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get multiple nodes
        /// </summary>
        [Test]
        public void TestGetNodesMulitpleIds()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var nodes = geoSource.GetNodes(new List<long>
            {
                // see http://www.openstreetmap.org/node/3630753431
                3630753431L,
                // see http://www.openstreetmap.org/node/443317092
                443317092L
            });

            Assert.IsNotNull(nodes);
            Assert.IsNotEmpty(nodes);
            Assert.IsTrue(nodes.Count == 2);

            var node = nodes[0];
            var expected = new Geodetic2d(48.76411d, 11.4209873d);
            TestUtilities.AssertThatGeodetic2dsAreEqual(expected, node.Coordinate);
            var tags = node.Tags;
            Assert.IsTrue(tags.Count == 1);
            Assert.IsTrue(tags.ContainsKey("natural"));
            Assert.IsTrue(tags["natural"] == "tree");

            node = nodes[1];
            expected = new Geodetic2d(48.7640513d, 11.4211742d);
            TestUtilities.AssertThatGeodetic2dsAreEqual(expected, node.Coordinate, 
                Mathd.EpsilonE14);
            tags = node.Tags;
            Assert.IsTrue(tags.Count == 3);
            Assert.IsTrue(tags.ContainsKey("barrier"));
            Assert.IsTrue(tags["barrier"] == "bollard");
            Assert.IsTrue(tags.ContainsKey("bicycle"));
            Assert.IsTrue(tags["bicycle"] == "yes");
            Assert.IsTrue(tags.ContainsKey("foot"));
            Assert.IsTrue(tags["foot"] == "yes");
        }

        /// <summary>
        /// Tests the reaction when fetching multiple nodes and given an invalid id
        /// </summary>
        [Test]
        public void TestGetNodeMultipleIdsInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get multiple nodes, with duplicate ids
        /// </summary>
        [Test]
        public void TestGetNodesDuplicateIds()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var nodes = geoSource.GetNodes(new List<long>
            {
                // see http://www.openstreetmap.org/node/3630753431
                3630753431L,
                3630753431L,
                // see http://www.openstreetmap.org/node/443317092
                443317092L
            });

            Assert.IsNotNull(nodes);
            Assert.IsNotEmpty(nodes);
            Assert.IsTrue(nodes.Count == 3);

            var node = nodes[0];
            var expected = new Geodetic2d(48.76411d, 11.4209873d);
            TestUtilities.AssertThatGeodetic2dsAreEqual(expected, node.Coordinate);
            var tags = node.Tags;
            Assert.IsTrue(tags.Count == 1);
            Assert.IsTrue(tags.ContainsKey("natural"));
            Assert.IsTrue(tags["natural"] == "tree");

            // check for reference equivalence
            Assert.AreSame(node, nodes[1]);

            node = nodes[1];
            expected = new Geodetic2d(48.76411d, 11.4209873d);
            TestUtilities.AssertThatGeodetic2dsAreEqual(expected, node.Coordinate);
            tags = node.Tags;
            Assert.IsTrue(tags.Count == 1);
            Assert.IsTrue(tags.ContainsKey("natural"));
            Assert.IsTrue(tags["natural"] == "tree");

            node = nodes[2];
            expected = new Geodetic2d(48.7640513d, 11.4211742d);
            TestUtilities.AssertThatGeodetic2dsAreEqual(expected, node.Coordinate,
                Mathd.EpsilonE14);
            tags = node.Tags;
            Assert.IsTrue(tags.Count == 3);
            Assert.IsTrue(tags.ContainsKey("barrier"));
            Assert.IsTrue(tags["barrier"] == "bollard");
            Assert.IsTrue(tags.ContainsKey("bicycle"));
            Assert.IsTrue(tags["bicycle"] == "yes");
            Assert.IsTrue(tags.ContainsKey("foot"));
            Assert.IsTrue(tags["foot"] == "yes");
        }

        /// <summary>
        /// Tests the ability to get ways
        /// </summary>
        [Test]
        public void TestGetWaysBox()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var ways = geoSource.GetWays(TestUtilities.BigIngolstadtBox);

            Assert.IsNotNull(ways);
            Assert.IsNotEmpty(ways);
        }

        /// <summary>
        /// Tests the reaction to getting ways when given a box which covers no geometries
        /// </summary>
        [Test]
        public void TestGetWaysBoxEmptyArea()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get ways
        /// </summary>
        [Test]
        public void TestGetWaysSingleId()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var ways = geoSource.GetWays(new List<long>
            {
                // see http://www.openstreetmap.org/way/103512667
                103512667L
            });

            Assert.IsNotNull(ways);
            Assert.IsNotEmpty(ways);
            Assert.IsTrue(ways.Count == 1);

            var way = ways[0];
            var nodes = way.Nodes;
            Assert.IsTrue(nodes.Count == 9);
            Assert.IsTrue(nodes[0].Id == 267380165L);
            Assert.IsTrue(nodes[1].Id == 4346910378L);
            Assert.IsTrue(nodes[2].Id == 4346910377L);
            Assert.IsTrue(nodes[3].Id == 89129121L);
            Assert.IsTrue(nodes[4].Id == 4346910375L);
            Assert.IsTrue(nodes[5].Id == 491562776L);
            Assert.IsTrue(nodes[6].Id == 89129122L);
            Assert.IsTrue(nodes[7].Id == 1168659785L);
            Assert.IsTrue(nodes[8].Id == 267408878L);
            var tags = way.Tags;
            Assert.IsTrue(tags.Count == 4);
            Assert.IsTrue(tags.ContainsKey("highway"));
            Assert.IsTrue(tags["highway"] == "residential");
            Assert.IsTrue(tags.ContainsKey("maxspeed"));
            Assert.IsTrue(tags["maxspeed"] == "30");
            Assert.IsTrue(tags.ContainsKey("name"));
            Assert.IsTrue(tags["name"] == "Kreuzstraße");
            Assert.IsTrue(tags.ContainsKey("source"));
            Assert.IsTrue(tags["source"] == "HiRes aerial imagery");
        }

        /// <summary>
        /// Tests the reaction to getting nodes when given an invalid id
        /// </summary>
        [Test]
        public void TestGetWaysInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get multiple ways
        /// </summary>
        [Test]
        public void TestGetWaysMultipleIds()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var ways = geoSource.GetWays(new List<long>
            {
                // see http://www.openstreetmap.org/way/103512667
                103512667L,
                // see http://www.openstreetmap.org/way/145069287
                145069287L
            });

            Assert.IsNotNull(ways);
            Assert.IsNotEmpty(ways);
            Assert.IsTrue(ways.Count == 2);

            var way = ways[0];
            var nodes = way.Nodes;
            Assert.IsTrue(nodes.Count == 9);
            Assert.IsTrue(nodes[0].Id == 267380165L);
            Assert.IsTrue(nodes[1].Id == 4346910378L);
            Assert.IsTrue(nodes[2].Id == 4346910377L);
            Assert.IsTrue(nodes[3].Id == 89129121L);
            Assert.IsTrue(nodes[4].Id == 4346910375L);
            Assert.IsTrue(nodes[5].Id == 491562776L);
            Assert.IsTrue(nodes[6].Id == 89129122L);
            Assert.IsTrue(nodes[7].Id == 1168659785L);
            Assert.IsTrue(nodes[8].Id == 267408878L);
            var tags = way.Tags;
            Assert.IsTrue(tags.Count == 4);
            Assert.IsTrue(tags.ContainsKey("highway"));
            Assert.IsTrue(tags["highway"] == "residential");
            Assert.IsTrue(tags.ContainsKey("maxspeed"));
            Assert.IsTrue(tags["maxspeed"] == "30");
            Assert.IsTrue(tags.ContainsKey("name"));
            Assert.IsTrue(tags["name"] == "Kreuzstraße");
            Assert.IsTrue(tags.ContainsKey("source"));
            Assert.IsTrue(tags["source"] == "HiRes aerial imagery");

            way = ways[1];
            nodes = way.Nodes;
            Assert.IsTrue(nodes.Count == 11);
            Assert.IsTrue(nodes[0].Id == 267380161L);
            Assert.IsTrue(nodes[1].Id == 442981345L);
            Assert.IsTrue(nodes[2].Id == 491553217L);
            Assert.IsTrue(nodes[3].Id == 308982533L);
            Assert.IsTrue(nodes[4].Id == 267380164L);
            Assert.IsTrue(nodes[5].Id == 1795306001L);
            Assert.IsTrue(nodes[6].Id == 427991051L);
            Assert.IsTrue(nodes[7].Id == 491542376L);
            Assert.IsTrue(nodes[8].Id == 491553151L);
            Assert.IsTrue(nodes[9].Id == 491562777L);
            Assert.IsTrue(nodes[10].Id == 89129121L);
            tags = way.Tags;
            Assert.IsTrue(tags.Count == 6);
            Assert.IsTrue(tags.ContainsKey("highway"));
            Assert.IsTrue(tags["highway"] == "unclassified");
            Assert.IsTrue(tags.ContainsKey("maxspeed"));
            Assert.IsTrue(tags["maxspeed"] == "30");
            Assert.IsTrue(tags.ContainsKey("name"));
            Assert.IsTrue(tags["name"] == "Bergbräustraße");
            Assert.IsTrue(tags.ContainsKey("oneway"));
            Assert.IsTrue(tags["oneway"] == "no");
            Assert.IsTrue(tags.ContainsKey("source"));
            Assert.IsTrue(tags["source"] == "HiRes aerial imagery");
            Assert.IsTrue(tags.ContainsKey("surface"));
            Assert.IsTrue(tags["surface"] == "sett");
        }

        /// <summary>
        /// Tests the reaction to getting ways when given multiple ids and an invalid id
        /// </summary>
        [Test]
        public void TestGetWaysMultipleIdsInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get multiple ways, with duplicate ids
        /// </summary>
        [Test]
        public void TestGetWaysDuplicateIds()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var ways = geoSource.GetWays(new List<long>
            {
                // see http://www.openstreetmap.org/way/103512667
                103512667L,
                103512667L,
                // see http://www.openstreetmap.org/way/145069287
                145069287L
            });

            Assert.IsNotNull(ways);
            Assert.IsNotEmpty(ways);
            Assert.IsTrue(ways.Count == 3);

            var way = ways[0];
            var nodes = way.Nodes;
            Assert.IsTrue(nodes.Count == 9);
            Assert.IsTrue(nodes[0].Id == 267380165L);
            Assert.IsTrue(nodes[1].Id == 4346910378L);
            Assert.IsTrue(nodes[2].Id == 4346910377L);
            Assert.IsTrue(nodes[3].Id == 89129121L);
            Assert.IsTrue(nodes[4].Id == 4346910375L);
            Assert.IsTrue(nodes[5].Id == 491562776L);
            Assert.IsTrue(nodes[6].Id == 89129122L);
            Assert.IsTrue(nodes[7].Id == 1168659785L);
            Assert.IsTrue(nodes[8].Id == 267408878L);
            var tags = way.Tags;
            Assert.IsTrue(tags.Count == 4);
            Assert.IsTrue(tags.ContainsKey("highway"));
            Assert.IsTrue(tags["highway"] == "residential");
            Assert.IsTrue(tags.ContainsKey("maxspeed"));
            Assert.IsTrue(tags["maxspeed"] == "30");
            Assert.IsTrue(tags.ContainsKey("name"));
            Assert.IsTrue(tags["name"] == "Kreuzstraße");
            Assert.IsTrue(tags.ContainsKey("source"));
            Assert.IsTrue(tags["source"] == "HiRes aerial imagery");

            // check for reference equivalence
            Assert.AreSame(way, ways[1]);

            way = ways[1];
            nodes = way.Nodes;
            Assert.IsTrue(nodes.Count == 9);
            Assert.IsTrue(nodes[0].Id == 267380165L);
            Assert.IsTrue(nodes[1].Id == 4346910378L);
            Assert.IsTrue(nodes[2].Id == 4346910377L);
            Assert.IsTrue(nodes[3].Id == 89129121L);
            Assert.IsTrue(nodes[4].Id == 4346910375L);
            Assert.IsTrue(nodes[5].Id == 491562776L);
            Assert.IsTrue(nodes[6].Id == 89129122L);
            Assert.IsTrue(nodes[7].Id == 1168659785L);
            Assert.IsTrue(nodes[8].Id == 267408878L);
            tags = way.Tags;
            Assert.IsTrue(tags.Count == 4);
            Assert.IsTrue(tags.ContainsKey("highway"));
            Assert.IsTrue(tags["highway"] == "residential");
            Assert.IsTrue(tags.ContainsKey("maxspeed"));
            Assert.IsTrue(tags["maxspeed"] == "30");
            Assert.IsTrue(tags.ContainsKey("name"));
            Assert.IsTrue(tags["name"] == "Kreuzstraße");
            Assert.IsTrue(tags.ContainsKey("source"));
            Assert.IsTrue(tags["source"] == "HiRes aerial imagery");

            way = ways[2];
            nodes = way.Nodes;
            Assert.IsTrue(nodes.Count == 11);
            Assert.IsTrue(nodes[0].Id == 267380161L);
            Assert.IsTrue(nodes[1].Id == 442981345L);
            Assert.IsTrue(nodes[2].Id == 491553217L);
            Assert.IsTrue(nodes[3].Id == 308982533L);
            Assert.IsTrue(nodes[4].Id == 267380164L);
            Assert.IsTrue(nodes[5].Id == 1795306001L);
            Assert.IsTrue(nodes[6].Id == 427991051L);
            Assert.IsTrue(nodes[7].Id == 491542376L);
            Assert.IsTrue(nodes[8].Id == 491553151L);
            Assert.IsTrue(nodes[9].Id == 491562777L);
            Assert.IsTrue(nodes[10].Id == 89129121L);
            tags = way.Tags;
            Assert.IsTrue(tags.Count == 6);
            Assert.IsTrue(tags.ContainsKey("highway"));
            Assert.IsTrue(tags["highway"] == "unclassified");
            Assert.IsTrue(tags.ContainsKey("maxspeed"));
            Assert.IsTrue(tags["maxspeed"] == "30");
            Assert.IsTrue(tags.ContainsKey("name"));
            Assert.IsTrue(tags["name"] == "Bergbräustraße");
            Assert.IsTrue(tags.ContainsKey("oneway"));
            Assert.IsTrue(tags["oneway"] == "no");
            Assert.IsTrue(tags.ContainsKey("source"));
            Assert.IsTrue(tags["source"] == "HiRes aerial imagery");
            Assert.IsTrue(tags.ContainsKey("surface"));
            Assert.IsTrue(tags["surface"] == "sett");
        }

        /// <summary>
        /// Tests the ability to get relations
        /// </summary>
        [Test]
        public void TestGetRelationsBox()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(TestUtilities.BigIngolstadtBox);

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
        }

        /// <summary>
        /// Tests the reaction to getting relations when given a box that covers no geometries
        /// </summary>
        [Test]
        public void TestGetRelationsEmptyBox()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get a single relation using an id
        /// </summary>
        [Test]
        public void TestGetRelationsSingleId()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5623484
                5623484L
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.IsTrue(relations.Count == 1);

            var relation = relations[0];
            Assert.IsNotNull(relation);

            var members = relation.Members;
            var roles = relation.MemberRoles;

            Assert.IsTrue(members.Count == 8);
            Assert.IsTrue(roles.Count == 8);

            Assert.IsTrue(members[0].Id == 3810007336L);
            Assert.IsTrue(members[0] is Node);
            Assert.IsTrue(roles[0] == "label");

            Assert.IsTrue(members[1].Id == 377469004L);
            Assert.IsTrue(members[1] is Way);
            Assert.IsTrue(roles[1] == "outer");

            Assert.IsTrue(members[2].Id == 377468980L);
            Assert.IsTrue(members[2] is Way);
            Assert.IsTrue(roles[2] == "outer");

            Assert.IsTrue(members[3].Id == 377468985L);
            Assert.IsTrue(members[3] is Way);
            Assert.IsTrue(roles[3] == "outer");

            Assert.IsTrue(members[4].Id == 377578435L);
            Assert.IsTrue(members[4] is Way);
            Assert.IsTrue(roles[4] == "outer");

            Assert.IsTrue(members[5].Id == 377578436L);
            Assert.IsTrue(members[5] is Way);
            Assert.IsTrue(roles[5] == "outer");

            Assert.IsTrue(members[6].Id == 377468993L);
            Assert.IsTrue(members[6] is Way);
            Assert.IsTrue(roles[6] == "outer");

            Assert.IsTrue(members[7].Id == 377469003L);
            Assert.IsTrue(members[7] is Way);
            Assert.IsTrue(roles[7] == "outer");
        }

        /// <summary>
        /// Tests the reaction when getting relations and given an invalid id
        /// </summary>
        [Test]
        public void TestGetRelationsInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get multiple relations
        /// </summary>
        [Test]
        public void TestGetRelationsMultipleIds()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5623484
                5623484L,
                // see http://www.openstreetmap.org/relation/5623483
                5623483L
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.IsTrue(relations.Count == 2);

            var relation = relations[0];
            Assert.IsNotNull(relation);

            var members = relation.Members;
            var roles = relation.MemberRoles;

            Assert.IsTrue(members.Count == 8);
            Assert.IsTrue(roles.Count == 8);

            Assert.IsTrue(members[0].Id == 3810007336L);
            Assert.IsTrue(members[0] is Node);
            Assert.IsTrue(roles[0] == "label");

            Assert.IsTrue(members[1].Id == 377469004L);
            Assert.IsTrue(members[1] is Way);
            Assert.IsTrue(roles[1] == "outer");

            Assert.IsTrue(members[2].Id == 377468980L);
            Assert.IsTrue(members[2] is Way);
            Assert.IsTrue(roles[2] == "outer");

            Assert.IsTrue(members[3].Id == 377468985L);
            Assert.IsTrue(members[3] is Way);
            Assert.IsTrue(roles[3] == "outer");

            Assert.IsTrue(members[4].Id == 377578435L);
            Assert.IsTrue(members[4] is Way);
            Assert.IsTrue(roles[4] == "outer");

            Assert.IsTrue(members[5].Id == 377578436L);
            Assert.IsTrue(members[5] is Way);
            Assert.IsTrue(roles[5] == "outer");

            Assert.IsTrue(members[6].Id == 377468993L);
            Assert.IsTrue(members[6] is Way);
            Assert.IsTrue(roles[6] == "outer");

            Assert.IsTrue(members[7].Id == 377469003L);
            Assert.IsTrue(members[7] is Way);
            Assert.IsTrue(roles[7] == "outer");

            relation = relations[1];
            Assert.IsNotNull(relation);

            members = relation.Members;
            roles = relation.MemberRoles;

            Assert.IsTrue(members.Count == 6);
            Assert.IsTrue(roles.Count == 6);

            Assert.IsTrue(members[0].Id == 3810007336L);
            Assert.IsTrue(members[0] is Node);
            Assert.IsTrue(roles[0] == "label");

            Assert.IsTrue(members[1].Id == 377468989L);
            Assert.IsTrue(members[1] is Way);
            Assert.IsTrue(roles[1] == "outer");

            Assert.IsTrue(members[2].Id == 377459452L);
            Assert.IsTrue(members[2] is Way);
            Assert.IsTrue(roles[2] == "outer");

            Assert.IsTrue(members[3].Id == 377459473L);
            Assert.IsTrue(members[3] is Way);
            Assert.IsTrue(roles[3] == "outer");

            Assert.IsTrue(members[4].Id == 377469012L);
            Assert.IsTrue(members[4] is Way);
            Assert.IsTrue(roles[4] == "outer");

            Assert.IsTrue(members[5].Id == 377469003L);
            Assert.IsTrue(members[5] is Way);
            Assert.IsTrue(roles[5] == "outer");
        }

        /// <summary>
        /// Tests the reaction to getting multiple relations while given an invalid id
        /// </summary>
        [Test]
        public void TestGetRelationsMultipleIdsInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get multiple relations, with duplicates
        /// </summary>
        [Test]
        public void TestGetRelationsDuplicateIds()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5623484
                5623484L,
                5623484L,
                // see http://www.openstreetmap.org/relation/5623483
                5623483L
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.IsTrue(relations.Count == 3);

            var relation = relations[0];
            Assert.IsNotNull(relation);
            var members = relation.Members;
            var roles = relation.MemberRoles;
            Assert.IsTrue(members.Count == 8);
            Assert.IsTrue(roles.Count == 8);

            Assert.IsTrue(members[0].Id == 3810007336L);
            Assert.IsTrue(members[0] is Node);
            Assert.IsTrue(roles[0] == "label");
            Assert.IsTrue(members[1].Id == 377469004L);
            Assert.IsTrue(members[1] is Way);
            Assert.IsTrue(roles[1] == "outer");
            Assert.IsTrue(members[2].Id == 377468980L);
            Assert.IsTrue(members[2] is Way);
            Assert.IsTrue(roles[2] == "outer");
            Assert.IsTrue(members[3].Id == 377468985L);
            Assert.IsTrue(members[3] is Way);
            Assert.IsTrue(roles[3] == "outer");
            Assert.IsTrue(members[4].Id == 377578435L);
            Assert.IsTrue(members[4] is Way);
            Assert.IsTrue(roles[4] == "outer");
            Assert.IsTrue(members[5].Id == 377578436L);
            Assert.IsTrue(members[5] is Way);
            Assert.IsTrue(roles[5] == "outer");
            Assert.IsTrue(members[6].Id == 377468993L);
            Assert.IsTrue(members[6] is Way);
            Assert.IsTrue(roles[6] == "outer");
            Assert.IsTrue(members[7].Id == 377469003L);
            Assert.IsTrue(members[7] is Way);
            Assert.IsTrue(roles[7] == "outer");

            Assert.AreSame(relation, relations[1]);

            relation = relations[1];
            Assert.IsNotNull(relation);
            members = relation.Members;
            roles = relation.MemberRoles;
            Assert.IsTrue(members.Count == 8);
            Assert.IsTrue(roles.Count == 8);

            Assert.IsTrue(members[0].Id == 3810007336L);
            Assert.IsTrue(members[0] is Node);
            Assert.IsTrue(roles[0] == "label");
            Assert.IsTrue(members[1].Id == 377469004L);
            Assert.IsTrue(members[1] is Way);
            Assert.IsTrue(roles[1] == "outer");
            Assert.IsTrue(members[2].Id == 377468980L);
            Assert.IsTrue(members[2] is Way);
            Assert.IsTrue(roles[2] == "outer");
            Assert.IsTrue(members[3].Id == 377468985L);
            Assert.IsTrue(members[3] is Way);
            Assert.IsTrue(roles[3] == "outer");
            Assert.IsTrue(members[4].Id == 377578435L);
            Assert.IsTrue(members[4] is Way);
            Assert.IsTrue(roles[4] == "outer");
            Assert.IsTrue(members[5].Id == 377578436L);
            Assert.IsTrue(members[5] is Way);
            Assert.IsTrue(roles[5] == "outer");
            Assert.IsTrue(members[6].Id == 377468993L);
            Assert.IsTrue(members[6] is Way);
            Assert.IsTrue(roles[6] == "outer");
            Assert.IsTrue(members[7].Id == 377469003L);
            Assert.IsTrue(members[7] is Way);
            Assert.IsTrue(roles[7] == "outer");

            relation = relations[2];
            Assert.IsNotNull(relation);
            members = relation.Members;
            roles = relation.MemberRoles;
            Assert.IsTrue(members.Count == 6);
            Assert.IsTrue(roles.Count == 6);

            Assert.IsTrue(members[0].Id == 3810007336L);
            Assert.IsTrue(members[0] is Node);
            Assert.IsTrue(roles[0] == "label");
            Assert.IsTrue(members[1].Id == 377468989L);
            Assert.IsTrue(members[1] is Way);
            Assert.IsTrue(roles[1] == "outer");
            Assert.IsTrue(members[2].Id == 377459452L);
            Assert.IsTrue(members[2] is Way);
            Assert.IsTrue(roles[2] == "outer");
            Assert.IsTrue(members[3].Id == 377459473L);
            Assert.IsTrue(members[3] is Way);
            Assert.IsTrue(roles[3] == "outer");
            Assert.IsTrue(members[4].Id == 377469012L);
            Assert.IsTrue(members[4] is Way);
            Assert.IsTrue(roles[4] == "outer");
            Assert.IsTrue(members[5].Id == 377469003L);
            Assert.IsTrue(members[5] is Way);
            Assert.IsTrue(roles[5] == "outer");
        }

        /// <summary>
        /// Tests the ability to fetch a relation with a relation dependency
        /// </summary>
        [Test]
        public void TestGetRelationWithRelationDependency()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5873329
                5873329L,
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.IsTrue(relations.Count == 1);

            var relation = relations[0];
            var members = relation.Members;
            var roles = relation.MemberRoles;
            var tags = relation.Tags;

            Assert.IsTrue(members.Count == 3);
            Assert.IsTrue(members[0].Id == 2701746L);
            Assert.IsTrue(members[0] is Relation);
            Assert.IsTrue(roles[0] == "");
            Assert.IsTrue(members[1].Id == 5873328L);
            Assert.IsTrue(members[1] is Relation);
            Assert.IsTrue(roles[1] == "");
            Assert.IsTrue(members[2].Id == 5873327);
            Assert.IsTrue(members[2] is Relation);
            Assert.IsTrue(roles[2] == "");

            Assert.IsTrue(tags.Count == 7);
            Assert.IsTrue(tags["colour"] == "#BEB405");
            Assert.IsTrue(tags["name"] == "Bus 53");
            Assert.IsTrue(tags["network"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["operator"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["ref"] == "53");
            Assert.IsTrue(tags["route_master"] == "bus");
            Assert.IsTrue(tags["type"] == "route_master");
        }

        /// <summary>
        /// Tests the ability to fetch multiple relations with relation dependencies
        /// </summary>
        [Test]
        public void TestGetRelationsMultipleIdsWithRelationDependencies()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5873329
                5873329L,
                // see http://www.openstreetmap.org/relation/5831016
                5831016L,
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.IsTrue(relations.Count == 2);

            var relation = relations[0];
            var members = relation.Members;
            var roles = relation.MemberRoles;
            var tags = relation.Tags;

            Assert.IsTrue(members.Count == 3);
            Assert.IsTrue(members[0].Id == 2701746L);
            Assert.IsTrue(members[0] is Relation);
            Assert.IsTrue(roles[0] == "");
            Assert.IsTrue(members[1].Id == 5873328L);
            Assert.IsTrue(members[1] is Relation);
            Assert.IsTrue(roles[1] == "");
            Assert.IsTrue(members[2].Id == 5873327);
            Assert.IsTrue(members[2] is Relation);
            Assert.IsTrue(roles[2] == "");

            Assert.IsTrue(tags.Count == 7);
            Assert.IsTrue(tags["colour"] == "#BEB405");
            Assert.IsTrue(tags["name"] == "Bus 53");
            Assert.IsTrue(tags["network"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["operator"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["ref"] == "53");
            Assert.IsTrue(tags["route_master"] == "bus");
            Assert.IsTrue(tags["type"] == "route_master");

            relation = relations[1];
            members = relation.Members;
            roles = relation.MemberRoles;
            tags = relation.Tags;

            Assert.IsTrue(members.Count == 2);
            Assert.IsTrue(members[0].Id == 2701744L);
            Assert.IsTrue(members[0] is Relation);
            Assert.IsTrue(roles[0] == "");
            Assert.IsTrue(members[1].Id == 5831015L);
            Assert.IsTrue(members[1] is Relation);
            Assert.IsTrue(roles[1] == "");

            Assert.IsTrue(tags.Count == 7);
            Assert.IsTrue(tags["colour"] == "#31B2EA");
            Assert.IsTrue(tags["name"] == "Bus 51");
            Assert.IsTrue(tags["network"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["operator"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["ref"] == "51");
            Assert.IsTrue(tags["route_master"] == "bus");
            Assert.IsTrue(tags["type"] == "route_master");
        }

        /// <summary>
        /// Tests the reaction when getting multiple relations with relation 
        /// dependencies whenm given anb invalid id
        /// </summary>
        [Test]
        public void TestGetRelationsMultipleIdsWithRelationDependencyInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to fetch multiple relations with relation dependencies
        /// with duplicate ids
        /// </summary>
        [Test]
        public void TestGetDuplicateRelationsWithRelationDependencies()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5873329
                5873329L,
                5873329L,
                // see http://www.openstreetmap.org/relation/5831016
                5831016L,
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.IsTrue(relations.Count == 3);

            var relation = relations[0];
            var members = relation.Members;
            var roles = relation.MemberRoles;
            var tags = relation.Tags;

            Assert.IsTrue(members.Count == 3);
            Assert.IsTrue(members[0].Id == 2701746L);
            Assert.IsTrue(members[0] is Relation);
            Assert.IsTrue(roles[0] == "");
            Assert.IsTrue(members[1].Id == 5873328L);
            Assert.IsTrue(members[1] is Relation);
            Assert.IsTrue(roles[1] == "");
            Assert.IsTrue(members[2].Id == 5873327);
            Assert.IsTrue(members[2] is Relation);
            Assert.IsTrue(roles[2] == "");

            Assert.IsTrue(tags.Count == 7);
            Assert.IsTrue(tags["colour"] == "#BEB405");
            Assert.IsTrue(tags["name"] == "Bus 53");
            Assert.IsTrue(tags["network"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["operator"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["ref"] == "53");
            Assert.IsTrue(tags["route_master"] == "bus");
            Assert.IsTrue(tags["type"] == "route_master");

            Assert.AreSame(relation, relations[1]);

            relation = relations[1];
            members = relation.Members;
            roles = relation.MemberRoles;
            tags = relation.Tags;

            Assert.IsTrue(members.Count == 3);
            Assert.IsTrue(members[0].Id == 2701746L);
            Assert.IsTrue(members[0] is Relation);
            Assert.IsTrue(roles[0] == "");
            Assert.IsTrue(members[1].Id == 5873328L);
            Assert.IsTrue(members[1] is Relation);
            Assert.IsTrue(roles[1] == "");
            Assert.IsTrue(members[2].Id == 5873327);
            Assert.IsTrue(members[2] is Relation);
            Assert.IsTrue(roles[2] == "");

            Assert.IsTrue(tags.Count == 7);
            Assert.IsTrue(tags["colour"] == "#BEB405");
            Assert.IsTrue(tags["name"] == "Bus 53");
            Assert.IsTrue(tags["network"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["operator"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["ref"] == "53");
            Assert.IsTrue(tags["route_master"] == "bus");
            Assert.IsTrue(tags["type"] == "route_master");

            relation = relations[2];
            members = relation.Members;
            roles = relation.MemberRoles;
            tags = relation.Tags;

            Assert.IsTrue(members.Count == 2);
            Assert.IsTrue(members[0].Id == 2701744L);
            Assert.IsTrue(members[0] is Relation);
            Assert.IsTrue(roles[0] == "");
            Assert.IsTrue(members[1].Id == 5831015L);
            Assert.IsTrue(members[1] is Relation);
            Assert.IsTrue(roles[1] == "");

            Assert.IsTrue(tags.Count == 7);
            Assert.IsTrue(tags["colour"] == "#31B2EA");
            Assert.IsTrue(tags["name"] == "Bus 51");
            Assert.IsTrue(tags["network"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["operator"] == "Ingolstädter Verkehrsgesellschaft mbH");
            Assert.IsTrue(tags["ref"] == "51");
            Assert.IsTrue(tags["route_master"] == "bus");
            Assert.IsTrue(tags["type"] == "route_master");
        }

        /// <summary>
        /// Tests the ability to get relations dependent on the given input
        /// </summary>
        [Test]
        public void TestGetDependentRelationsSingleId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the reaction when getting relations when given an invalid id
        /// </summary>
        [Test]
        public void TestGetDependentRelationsInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get dependent relations when given multiple ids
        /// </summary>
        [Test]
        public void TestGetDependentRelationsMultipleIds()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the reaction to getting multiple dependent relations when given some
        /// valid and one invalid id
        /// </summary>
        [Test]
        public void TestGetDependentRelationsMultipleIdsInvalidId()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the ability to get anonymous geometries using a box
        /// </summary>
        [Test]
        public void TestGetBox()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the reaction when getting anonymous geometries using an area covering
        /// no geometries
        /// </summary>
        [Test]
        public void TestGetBoxEmptyArea()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void TestGetRelationSelfDependencyCase()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/3018045
                3018045L,
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
        }

        [Test]
        public void TestGetRelationCyclicDependencyCase()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var relations = geoSource.GetRelations(new List<long>
            {
                // see http://www.openstreetmap.org/relation/5338846
                5338846L,
            });

            Assert.IsNotNull(relations);
            Assert.IsNotEmpty(relations);
            Assert.AreEqual(1, relations.Count);

            var relation = relations[0];
            Assert.AreEqual(2, relation.Members.Count);

            // the member relation of the member is actually the original relation
            var member = relation.Members[0] as Relation;
            Assert.IsNotNull(member);
            Assert.AreSame(member.Members[1], relation);
        }

        [Test]
        public void TestGetNodesBoxWithTags()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var tags = new List<Tuple<string, IList<string>>>
            {
                new Tuple<string, IList<string>>("place", new List<string>
                {
                    "city"
                })
            };

            var nodes = geoSource.GetNodes(TestUtilities.BigIngolstadtBox, tags);

            Assert.IsNotNull(nodes);
            Assert.IsNotEmpty(nodes);
        }

        [Test]
        public void TestGetWaysBoxWithTags()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);

            var tags = new List<Tuple<string, IList<string>>>
            {
                new Tuple<string, IList<string>>("highway", new List<string>
                {
                    "service",
                    "road"
                })
            };

            var ways = geoSource.GetWays(TestUtilities.BigIngolstadtBox, tags);

            Assert.IsNotNull(ways);
            Assert.IsNotEmpty(ways);
        }

        [Test]
        public void TestCloneMethod()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var geoSource = new PostgreSQLOsmGeoSource(client);
            var clonedGeoSource = geoSource.Clone() as PostgreSQLOsmGeoSource;

            Assert.IsNotNull(clonedGeoSource);
        }
    }
}