﻿namespace RouteCalculator.IntegrationTest
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    /// This class contains the integration tests between the FileParser, RailRoad and City class
    /// </summary>
    [TestFixture]
    [SuppressMessage(
        category: "Microsoft.Performance",
        checkId: "CA1822:MarkMembersAsStatic",
        Justification = "Most methods inside test classes are called by the NUnit framework indirectly.", 
        Scope = "type",
        Target = "RouteCalculator.IntegrationTest.RailroadMap_Railroad_City_IntegrationTest")]
    [SuppressMessage(
        category: "Microsoft.Naming",
        checkId: "CA1707:IdentifiersShouldNotContainUnderscores",
        Justification = "I call Angry Monkeys: Method name is too long, it needs underscore.")]
    public class RailroadMap_Railroad_City_IntegrationTest
    {
        /// <summary>
        /// Tests that the RailroadMap can generate a single rail road.
        /// </summary>
        [Test]
        public void TestCanGenerateASingleRailroad()
        {
            //// Arrange
            RailroadMap map;
            IEnumerable<Railroad> railroads;
            Railroad railroad;
            IEnumerable<City> cities;
            City originCity;
            City destinationCity;
            int expectedLength = 1;
            int expectedRailRoadCount = 1;
            int expectedCityCount = 2;
            string expectedOriginCityName = "A";
            string expectedDesintationCityName = "B";
            string filePath = "test_data/single_railroad_two_cities.txt";
            var testDataFileStream = File.OpenRead(filePath);

            //// Act
            map = new RailroadMap();
            map.Init(testDataFileStream);
            railroads = map.Railroads;
            cities = map.Cities;

            //// Assert
            //// Verify that collections are obtained correctly.
            Assert.AreEqual(expectedRailRoadCount, railroads.Count());
            Assert.AreEqual(expectedCityCount, cities.Count());
            //// Verify that cities and railroads are read correctly
            railroad = railroads.ElementAt(0);
            originCity = cities.First(city => city.Name == expectedOriginCityName);
            destinationCity = cities.First(city => city.Name == expectedDesintationCityName);
            Assert.AreEqual(expectedLength, railroad.Length);
            //// Verify cities and railroads are connected correctly
            Assert.AreEqual(originCity, railroad.Origin);
            Assert.AreEqual(destinationCity, railroad.Destination);
            Assert.AreEqual(originCity.Outgoing.ElementAt(0), railroad);
            Assert.AreEqual(destinationCity.Incoming.ElementAt(0), railroad);
        }
    }
}