// -----------------------------------------------------------------------
// <copyright file="RouteFinderTest.cs" company="Marcel Valdez">
// Copyright 2012 Marcel Valdez
// </copyright>
// -----------------------------------------------------------------------
namespace RouteCalculator.Test.Plan
{
    using System.Collections.Generic;
    using System.Linq;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;
    using RouteCalculator.Testing;

    /// <summary>
    /// This class contains all of the unit tests for the RouteFinder class
    /// </summary>
    [TestFixture]
    public class RouteFinderTest
    {
        /// <summary>
        /// It represents a city specification to ignore.
        /// </summary>
        private const string IGNORED_CITY = "Z";

        /// <summary>
        /// It represents an ignored railroad length.
        /// </summary>
        private const int IGNORED_LENGTH = 0;

        /// <summary>
        /// It represents the count of ignored route legs specification
        /// </summary>
        private const int IGNORED_LEGS_COUNT = 0;

        //// AB1 = Stops: 1, Length: 1, Path: A-B
        [TestCase(new string[] { "AB1" }, new string[] { "AB1" }, new string[] { "AB1" }, true, new string[] { "AB1" })]
        [TestCase(new string[] { "AB1", "BC1" }, new string[] { "AB1" }, new string[] { "AB1" }, true, new string[] { "AB1" })]
        [TestCase(new string[] { "AB1", "BA1" }, new string[] { "AB1" }, new string[] { "AB1" }, true, new string[] { "AB1" })]
        [TestCase(new string[] { "AB2" }, new string[] { "AB1" }, new string[] { "AB1" }, false, new string[] { "ZZ0" })]
        [TestCase(new string[] { "AB1", "BC1", "CA1" }, new string[] { "AB1" }, new string[] { "AB1" }, true, new string[] { "AB1" })]
        //// AC1 = Stops: 1, Length: 1, Path: A-C
        [TestCase(new string[] { "AB1" }, new string[] { "AC1" }, new string[] { "AC1" }, false, new string[] { "ZZ0" })]
        //// AB1 BC1 = Stops: 2, Length: 2, Path: A-B-C
        [TestCase(new string[] { "AB1", "BC1" }, new string[] { "AB1", "BC1" }, new string[] { "AB1", "BC1" }, true, new string[] { "AB1", "BC1" })]
        //// AB0 BC0 = Stops: 2, Length: Any, Path: A-B-C
        [TestCase(new string[] { "AB1", "BC2" }, new string[] { "AB0", "BC0" }, new string[] { "AB0", "BC0" }, true, new string[] { "AB1", "BC2" })]
        //// AZ0 ZZ0 ZA0 = Stops: 3, Length: Any, Path: A-X-X-A
        [TestCase(new string[] { "AB1", "BC1", "CA1" }, new string[] { "AZ0", "ZZ0", "ZA0" }, new string[] { "AZ0", "ZZ0", "ZA0" }, true, new string[] { "AB1", "BC1", "CA1" })]
        //// AB1 BC1 = Stops: 2, Length: 2, Path: A-B-C
        [TestCase(new string[] { "AB1", "BC2" }, new string[] { "AB1", "BC1" }, new string[] { "AB1", "BC1" }, false, new string[] { "ZZ0" })]
        //// AZ0 ZZ0 ZZ0 ZC0 = Stops: 4, Length: Any, Path: A-X-X-X-C
        [TestCase(
            new string[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" },
            new string[] { "AZ0", "ZZ0", "ZZ0", "ZC0" },
            new string[] { "AZ0", "ZZ0", "ZZ0", "ZC0" }, 
            true,
            new string[] { "AB5", "BC4", "CD8", "DC8" })]
        //// CE0 ZZ0 ZZ0 ZZ0 DC30 = Stops: 5, Length: 30 (or less), Path: C-E-X-X-D-C
        [TestCase(
            new string[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" },
            new string[] { "CE0", "ZZ0", "ZZ0", "ZZ0", "DC30" },
            new string[] { "CE0", "ZZ0", "ZZ0", "ZZ0", "DC30" },
            true,
            new string[] { "CE2", "EB3", "BC4", "CD8", "DC8" })]
        //// AE0 ED0 = Stops: 2, Length: Any, Path: A-E-D
        [TestCase(
            new string[] { "AB5", "BC4", "CD8", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7" },
            new string[] { "AE0", "ED0" },
            new string[] { "AE0", "ED0" },
            false,
            new string[] { "ZZ0" })]

        /// <summary>
        /// Tests if it can find first satisfying route
        /// </summary>
        /// <param name="graph">The graph of the map.</param>
        /// <param name="specifiedRoute">The specified route.</param>
        /// <param name="specifiedMightRoute">The specified route that might be satisfied.</param>
        /// <param name="shouldFindAValidRoute">if set to <c>true</c> [should find a valid route].</param>
        /// <param name="expectedRouteGraph">The expected route graph. Ignored if <paramref name="shouldFindAValidRoute"/> is set to false</param>
        [Test]
        public void TestIfItCanFindFirstSatisfyingRoute(
            string[] graph, 
            string[] specifiedRoute, 
            string[] specifiedMightRoute, 
            bool shouldFindAValidRoute, 
            string[] expectedRouteGraph)
        {
            // Arrange
            // Arrange the expected route
            IRoute expectedRoute = BuildRoute(expectedRouteGraph);

            // Arrange the map
            IRailroadMap map = Substitute.For<IRailroadMap>();
            IList<ICity> cities = TestHelper.GenerateCities(graph);
            IList<IRailroad> railroads = TestHelper.GenerateLegs(graph, cities);
            map.Cities.Returns(cities);
            map.Railroads.Returns(railroads);

            // Arrange the specification
            IRouteSpecification specification = Substitute.For<IRouteSpecification>();
            IRoute routeSpec = BuildRoute(specifiedRoute);
            specification
                .IsSatisfiedBy(null)
                .ReturnsForAnyArgs(method => SatisfiesSpecification(routeSpec, method.Arg<IRoute>()));

            // Arrange the specification that "might" be satisfied
            IRoute routeMightBeSpec = BuildRoute(specifiedMightRoute);
            specification
                .MightBeSatisfiedBy(null)
                .ReturnsForAnyArgs(method => MightSatisfySpecification(routeMightBeSpec, method.Arg<IRoute>()));

            // Arrange the target
            IRouteFinder target = Substitute.For<RouteFinder>(map);

            // Arrange root route
            IRoute rootRoute = Substitute.For<IRoute>();
            rootRoute.Legs.Returns(new List<IRailroad>());

            // Act
            IRoute actualResult = target.FindFirstSatisfyingRoute(specification);

            // Assert
            if (shouldFindAValidRoute)
            {
                Assert.IsNotNull(actualResult);
                Assert.IsTrue(SatisfiesSpecification(expectedRoute, actualResult));
            }
            else
            {
                Assert.IsNull(actualResult);
            }
        }

        /// <summary>
        /// Determines if a route might satisfy a specification
        /// </summary>
        /// <param name="routeSpec">The route spec.</param>
        /// <param name="route">The route.</param>
        /// <returns>true if it might satisfy the specification, false otherwise</returns>
        private static bool MightSatisfySpecification(IRoute routeSpec, IRoute route)
        {
            if (routeSpec.Distance > IGNORED_LENGTH && route.Distance > routeSpec.Distance)
            {
                return false;
            }

            if (routeSpec.Legs.Count() != IGNORED_LEGS_COUNT && route.Legs.Count() > routeSpec.Legs.Count())
            {
                return false;
            }

            if (!SatisfiesSpecification(routeSpec.Origin, route.Origin))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if a route satisfies a specification
        /// </summary>
        /// <param name="routeSpec">The route specification.</param>
        /// <param name="route">The route to compare.</param>
        /// <returns>true if it satisfies the specification, false otherwise.</returns>
        private static bool SatisfiesSpecification(IRoute routeSpec, IRoute route)
        {
            if (!SatisfiesSpecification(routeSpec.Destination, route.Destination)
              || !SatisfiesSpecification(routeSpec.Origin, route.Origin))
            {
                return false;
            }

            if (routeSpec.Legs.Count() != IGNORED_LEGS_COUNT)
            {
                if (routeSpec.Legs.Count() != route.Legs.Count())
                {
                    return false;
                }

                for (int i = 0; i < routeSpec.Legs.Count(); i++)
                {
                    IRailroad specRailroad = routeSpec.Legs.ElementAt(i);
                    IRailroad routeRailroad = route.Legs.ElementAt(i);

                    if (!SatisfiesSpecification(specRailroad, routeRailroad))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Determines if a railroad statisfies a specification
        /// </summary>
        /// <param name="specRailroad">The specified railroad.</param>
        /// <param name="routeRailroad">The actual railroad.</param>
        /// <returns>true if it satisfies the specification, false otherwise.</returns>
        private static bool SatisfiesSpecification(IRailroad specRailroad, IRailroad routeRailroad)
        {
            return SatisfiesSpecification(specRailroad.Origin, routeRailroad.Origin) &&
                   SatisfiesSpecification(specRailroad.Destination, routeRailroad.Destination);
        }

        /// <summary>
        /// Determines if a city satisfies a specification
        /// </summary>
        /// <param name="specifiedCity">The specified city.</param>
        /// <param name="city">The city to check.</param>
        /// <returns>true if it satisfies the specification.</returns>
        private static bool SatisfiesSpecification(ICity specifiedCity, ICity city)
        {
            return specifiedCity.Name == IGNORED_CITY || specifiedCity.Name == city.Name;
        }

        /// <summary>
        /// Builds the route.
        /// </summary>
        /// <param name="specifiedRoute">The specified route.</param>
        /// <returns>The built route.</returns>
        private static IRoute BuildRoute(string[] specifiedRoute)
        {
            IRoute routeSpec = Substitute.For<IRoute>();
            IList<IRailroad> specifiedLegs = TestHelper.GenerateLegs(specifiedRoute);
            routeSpec.Legs.ReturnsForAnyArgs(specifiedLegs);

            // Specify distance
            int distance = specifiedLegs.Sum(leg => leg.Length);
            routeSpec.Distance.ReturnsForAnyArgs(distance);

            // Specify origin
            ICity origin = specifiedLegs.First().Origin;
            routeSpec.Origin.ReturnsForAnyArgs(origin);

            // Specify destination
            ICity destination = specifiedLegs.Last().Destination;
            routeSpec.Destination.ReturnsForAnyArgs(destination);

            return routeSpec;
        }
    }
}
