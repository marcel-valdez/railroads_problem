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
    /// This class contains the unit tests for the ShortestRouteFinder class
    /// </summary>
    [TestFixture]
    public class ShortestRouteFinderTest
    {
        [TestCase("AB1", "AB1", "AB1")] // Simplest possible test case               
        [TestCase("AB1 BC1", "AB1", "AB1")]
        [TestCase("AB1 BA1", "BZ0 ZB0", "BA1 AB1")] // Simplest possible test case that has a cycle and starts at second railroad
        [TestCase("AB1 BC1 CA1", "AZ0 ZA0", "AB1 BC1 CA1")]
        [TestCase("AB1 BC1", "AZ0 ZC2", "AB1 BC1")] 
        [TestCase("AB1 BC1 CD1 AD1", "AZ0 ZD10", "AD1")] 
        [TestCase("AB1 BC2 CD1 AC1 BD1", "AZ0 ZD0", "AB1 BD1")]
        [TestCase("AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7", "AZ0 ZC0", "AB5 BC4")]
        [TestCase("AB1 BC1 CA1", "BZ0 ZB0", "BC1 CA1 AB1")]

        // No route possible
        [TestCase("AB1", "AC0", "")]
        [TestCase("AB1", "AC0", "")]
        [TestCase("AB1 BD1", "AC0", "")]
        [TestCase("AB1 BC1", "AC1", "")]
        [TestCase("AB1 BC2 CD1 AC1 BD1", "AD1", "")]
        
        // Problematic testcases
        [TestCase("BC4 CD8 DC8 CE2 EB3", "BB0", "BC4 CE2 EB3")]
        [TestCase("AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7", "BB0", "BC4 CE2 EB3")]

        /// <summary>
        /// Tests if it finds the shortest route
        /// </summary>
        /// <param name="mapGraph">The map graph.</param>
        /// <param name="specifiedRoute">The specified route.</param>
        /// <param name="expectedRouteGraph">The expected route graph.</param>
        [Test]
        public void TestIfItFindsTheShortestRoute(string mapGraph, string specifiedRoute, string expectedRouteGraph)
        {
            // Arrange
            // Arrange the map
            IRailroadMap map = Substitute.For<IRailroadMap>();
            IList<ICity> cities = TestHelper.GenerateCities(mapGraph).ToArray();
            IList<IRailroad> railroads = TestHelper.GenerateLegs(mapGraph, cities).ToArray();
            map.Cities.Returns(cities);
            map.Railroads.Returns(railroads);

            // Arrange the specification
            IRouteComparer comparer = BuildComparerMock();
            IRouteSpecification specification = Substitute.For<IRouteSpecification>();
            IRoute routeSpec = TestHelper.BuildMockRoute(specifiedRoute);
            routeSpec
                .Legs
                .ReturnsForAnyArgs(FinderTestHelper.IGNORE_LEGS);

            specification
                .IsSatisfiedBy(null)
                .ReturnsForAnyArgs(
                    call => FinderTestHelper.SatisfiesSpecification(routeSpec, call.Arg<IRoute>()));

            // Arrange the specification that "might" be satisfied
            IRoute routeMightBeSpec = TestHelper.BuildMockRoute(specifiedRoute);
            routeMightBeSpec.Legs
                .ReturnsForAnyArgs(FinderTestHelper.IGNORE_LEGS);

            specification.MightBeSatisfiedBy(null)
                .ReturnsForAnyArgs(call => FinderTestHelper.MightSatisfySpecification(routeMightBeSpec, call.Arg<IRoute>()));

            // Arrange the target
            IRouteFinder target = Substitute.For<ShortestRouteFinder>(map, comparer);

            // Arrange root route
            IRoute rootRoute = Substitute.For<IRoute>();
            rootRoute.Legs.Returns(new List<IRailroad>());

            // Act
            IRoute actualRouteFound = target.FindFirstSatisfyingRoute(specification);

            // Assert
            if (!string.IsNullOrEmpty(expectedRouteGraph))
            {
                Assert.IsNotNull(actualRouteFound);
                Assert.IsTrue(FinderTestHelper.SatisfiesSpecification(routeSpec, actualRouteFound));
                IRoute expectedRoute = TestHelper.BuildMockRoute(expectedRouteGraph);
                Assert.IsTrue(FinderTestHelper.SatisfiesSpecification(expectedRoute, actualRouteFound));
            }
            else
            {
                Assert.IsNull(actualRouteFound);
            }
        }

        /// <summary>
        /// Builds the comparison mock.
        /// </summary>
        /// <param name="routeA">The route to compare.</param>
        /// <returns>The comparison mock.</returns>
        private static IRouteComparison BuildComparisonMock(IRoute routeA)
        {
            IRouteComparison comparison = Substitute.For<IRouteComparison>();
            comparison.BetterThan(Arg.Any<IRoute>())
                .ReturnsForAnyArgs(innerCall =>
                {
                    if (routeA is Worst)
                    {
                        return false;
                    }

                    IRoute routeB = innerCall.Arg<IRoute>();
                    return routeB is Worst || routeA.Distance < routeB.Distance;
                });
            comparison.WorseThan(Arg.Any<IRoute>())
                .ReturnsForAnyArgs(innerCall =>
                {
                    if (routeA is Worst)
                    {
                        return true;
                    }

                    IRoute routeB = innerCall.Arg<IRoute>();
                    return !(routeB is Worst) && !(routeA is Worst) && routeA.Distance > routeB.Distance;
                });

            return comparison;
        }

        /// <summary>
        /// Builds the comparer mock. Using shortest distance as criteria.
        /// </summary>
        /// <returns>The route comparer</returns>
        private static IRouteComparer BuildComparerMock()
        {
            IRouteComparer comparer = Substitute.For<IRouteComparer>();
            comparer.Is(Arg.Any<IRoute>())
                    .ReturnsForAnyArgs(call =>
                    {
                        IRoute routeA = call.Arg<IRoute>();
                        IRouteComparison comparison = BuildComparisonMock(routeA);

                        return comparison;
                    });

            return comparer;
        }
    }
}
