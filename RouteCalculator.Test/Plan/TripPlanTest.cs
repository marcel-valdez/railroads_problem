namespace RouteCalculator.Test.Plan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;

    /// <summary>
    /// This class contains all of the unit tests for the TripPlan class
    /// </summary>
    [TestFixture]
    public class TripPlanTest
    {
        /// <summary>
        /// This field contains the path configurations
        /// </summary>
        private static object[] pathConfigurationsData =
        {
            // Simple route A->B
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "B", 1 },
                },

                // Routes
                new object[] 
                {
                    new int[] { 0 }
                },
            },

            // Simple two length: A->B->C
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "B", 1 },
                    new object[] { "B", "C", 1 }
                },

                // Routes
                new object[] 
                {
                    new int[] { 0 },
                    new int[] { 1 },
                    new int[] { 0, 1 },
                }
            },

            // Simple doubly connected: A<->B
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "B", 1 },
                    new object[] { "B", "A", 1 }
                },

                // Routes
                new object[] 
                {
                    new int[] { 0 },
                    new int[] { 1 },
                    new int[] { 0, 1 },
                    new int[] { 1, 0 }
                }
            },

            // Simple B is the center: A->B<-C
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "B", 1 },
                    new object[] { "C", "B", 1 }
                },

                // Routes
                new object[] 
                {
                    new int[] { 0 },
                    new int[] { 1 }
                }
            },
            
            // Routes not completely connected
            // A->B  D->E
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "B", 1 },
                    new object[] { "D", "E", 1 }
                },

                // Routes
                new object[] 
                {
                    new int[] { 0 },
                    new int[] { 1 }
                }
            },

            // Simple triangle: C->A->B->C
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "B", 1 },
                    new object[] { "B", "C", 1 },
                    new object[] { "C", "A", 1 }
                },

                // Routes (sin repetir railroad)
                new object[] 
                {
                    new int[] { 0 },
                    new int[] { 1 },
                    new int[] { 2 },
                    new int[] { 0, 1 },
                    new int[] { 1, 2 },
                    new int[] { 2, 0 },
                    new int[] { 0, 1, 2 },
                    new int[] { 2, 1, 0 }
                }
            },

            // Simple C is the center: A->C<-B
            //                            ^
            //                            |
            //                            D
            new object[] 
            {
                // Graph
                new object[][] 
                {
                    new object[] { "A", "C", 1 },
                    new object[] { "B", "C", 1 },
                    new object[] { "D", "C", 1 }
                },

                // Routes
                new object[][] { }
            },
        };

        /// <summary>
        /// Tests if it can calculate possible routes
        /// </summary>
        [Test]
        public void TestIfItCanCalculatePossibleRoutes()
        {
            // Arrange
            // Create the mock Map using a received Railroad and City configuration (create each railroad and city manually?)

            // Note: Integration test would be to setup the Graph using a string (and depend on RailroadMap.BuildMap(string))
            // Initialize the TripPlan with a mock Map

            // Act
            // Calculate the possible routes

            // Assert
            // Assert the calculated routes coincide with those specified
        }

        /// <summary>
        /// Tests if the helper method BuildSimpleMap works correctly
        /// </summary>
        [Test]
        public void TestIfBuildSimpleMapWorks()
        {
            // Arrange
            object[][] paths = new object[][] 
            {
                new object[] { "A", "B", 1 },
                new object[] { "A", "C", 2 },
                new object[] { "B", "C", 3 },
                new object[] { "C", "A", 1 },
            };

            string[] expectedCityNames = { "A", "B", "C" };
            int[] expectedRailroadLengths = { 1, 2, 3, 1 };
            string[][] expectedRailroadCities = new string[][] 
            { 
                new string[] { "A", "B" }, 
                new string[] { "A", "C" }, 
                new string[] { "B", "C" },
                new string[] { "C", "A" } 
            };

            // Act
            var map = BuildSimpleMap(paths);

            // Assert
            CollectionAssert.AreEquivalent(expectedCityNames, map.Cities.Select(city => city.Name));
            CollectionAssert.AreEquivalent(expectedRailroadLengths, map.Railroads.Select(rr => rr.Length));
            for (int i = 0; i < expectedRailroadCities.Length; i++)
            {
                Railroad railroad = map.Railroads.ElementAt(i);
                City origin = railroad.Origin;
                City destination = railroad.Destination;

                // Assert they connect correctly
                Assert.AreEqual(expectedRailroadCities[i][0], origin.Name);
                Assert.AreEqual(expectedRailroadCities[i][1], destination.Name);
                CollectionAssert.Contains(origin.Outgoing, railroad);
            }
        }

        /// <summary>
        /// Builds the railroad map.
        /// </summary>
        /// <param name="paths">The railroad paths (no length needed).</param>
        /// <returns>An IRailroadMap with the specified paths</returns>
        private static IRailroadMap BuildSimpleMap(object[][] paths)
        {
            IRailroadMap map = Substitute.For<IRailroadMap>();
            IList<City> cities = new List<City>();
            IList<Railroad> railroads = new List<Railroad>();
            foreach (object[] pathConfig in paths)
            {
                string originCityName = (string)pathConfig[0];
                string destinationCityName = (string)pathConfig[1];
                int railroadLength = (int)pathConfig[2];

                City originCity = GetOrCreateCity(cities, originCityName);
                City destinationCity = GetOrCreateCity(cities, destinationCityName);
                Railroad newRailroad = new Railroad();
                newRailroad.Origin = originCity;
                newRailroad.Destination = destinationCity;
                newRailroad.Length = railroadLength;
                railroads.Add(newRailroad);
                originCity.Outgoing.Add(newRailroad);
            }

            map.Cities.Returns(cities);
            map.Railroads.Returns(railroads);

            return map;
        }

        /// <summary>
        /// Gets or creates a city.
        /// </summary>
        /// <param name="cities">The cities.</param>
        /// <param name="originCityName">Name of the origin city.</param>
        /// <returns>
        /// The city with the name <paramref name="originCityName"/>
        /// </returns>
        private static City GetOrCreateCity(IList<City> cities, string originCityName)
        {
            City city = cities.FirstOrDefault(item => item.Name == originCityName);
            if (city == null)
            {
                city = new City();
                city.Name = originCityName;
                cities.Add(city);
            }

            return city;
        }
    }
}
