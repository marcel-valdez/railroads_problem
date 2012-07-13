namespace RouteCalculator.IntegrationTest
{
    using System;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;
    using RouteCalculator.Testing;

    /// <summary>
    /// This class contains integration tests for the railroad map + route + shortest route finder + specifications implementations
    /// </summary>
    [TestFixture]
    public class RailroadMap_Route_ShortestRouteFinder_Specifications_IntegrationTest
    {
        // Route found
        // Simplest possible that passes
        [TestCase(
            "AB1",
            "AB1",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "A", "B" },
            new object[] { 0, 1 })]

        // Simplest possible that passes with an extra leg
        [TestCase(
            "AB1, BC1",
            "BC1",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "B", "C" },
            new object[] { 0, 1 })]

        // Simplest possible that passes that includes two legs
        [TestCase(
            "AB1, BC1",
            "AB1, BC1",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "A", "B", "C" },
            new object[] { 0, 2 })]

        // Route not found
        // Simplest possible that does not pass because of first spec
        [TestCase(
            "AB1",
            "",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "A", "C" },
            new object[] { 0, 1 })]

        // Simplest possible that does not pass because of second spec
        [TestCase(
            "AB2",
            "",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "A", "B" },
            new object[] { 0, 1 })]

        // Simplest possible that does not pass because of first that considers two legs
        [TestCase(
            "AB1, BC1",
            "",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "A", "B", "D" },
            new object[] { 0, 2 })]

        // Simplest possible that does not pass because of second that considers two legs
        [TestCase(
            "AB1, BC1",
            "",
            new Type[] { typeof(PathSpecification), typeof(DistanceSpecification) },
            new object[] { "A", "B", "C" },
            new object[] { 0, 1 })]

        // Route found
        // Simplest possible that passes
        [TestCase(
            "AB1",
            "AB1",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "A", "B" },
            new object[] { 1, 1 })]

        // Simplest possible that passes with an extra leg
        [TestCase(
            "AB1, BC1",
            "BC1",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "B", "C" },
            new object[] { 1, 1 })]

        // Simplest possible that passes that includes two legs
        [TestCase(
            "AB1, BC1",
            "AB1, BC1",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "A", "C" },
            new object[] { 2, 2 })]

        // Route not found
        // Simplest possible that does not pass because of first spec
        [TestCase(
            "AB1",
            "",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "A", "C" },
            new object[] { 1, 1 })]

        // Simplest possible that does not pass because of second spec
        [TestCase(
            "AB1",
            "",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "A", "B" },
            new object[] { 2, 2 })]

        // Simplest possible that does not pass because of first that considers two legs
        [TestCase(
            "AB1, BC1",
            "",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "A", "D" },
            new object[] { 0, 2 })]

        // Simplest possible that does not pass because of second that considers two legs
        [TestCase(
            "AB1, BC1",
            "",
            new Type[] { typeof(OriginAndDestinationSpecification), typeof(StopsCountSpecification) },
            new object[] { "A", "C" },
            new object[] { 0, 1 })]

        // Use case #8
        [TestCase(
            "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7",
            "AB5 BC4",
            new Type[] { typeof(OriginAndDestinationSpecification) },
            new object[] { "A", "C" })]

        // Use case #9
        [TestCase(
            "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7",
            "BC4 CE2 EB3",
            new Type[] { typeof(OriginAndDestinationSpecification) },
            new object[] { "B", "B" })]

        // Fully connected triangle
        [TestCase(
            "AB1, BA4, BC2, CB3, AC3, CA2",
            "CB3 BC2",
            new Type[] { typeof(OriginAndDestinationSpecification) },
            new object[] { "C", "C" })]
        [TestCase(
            "AB1, BC2, CB3, AC3",
            "",
            new Type[] { typeof(OriginAndDestinationSpecification) },
            new object[] { "C", "A" })]

        // Fully connected square
        [TestCase(
            "AB1, BA4, BC2, CB3, CD3, DC2, DA4, AD1, AC3, CA2, BD1, DB4",
            "BA4 AB1",
            new Type[] { typeof(OriginAndDestinationSpecification) },
            new object[] { "B", "B" })]

        // Partially connected square (1 route only for answer)
        [TestCase(
            "BA4, CB3, CD3, DA4, AD1, CA2, AC3, BD1",
            "BA4 AC3 CB3",
            new Type[] { typeof(OriginAndDestinationSpecification) },
            new object[] { "B", "B" })]

        /// <summary>
        /// Tests if the integration of classes can find the best route
        /// </summary>
        /// <param name="graph">The railroad map graph.</param>
        /// <param name="expectedRouteGraph">The expected route graph.</param>
        /// <param name="specificationTypes">The specification types.</param>
        /// <param name="specsCtorsArgs">The specifications constructors arguments.</param>
        [Test]
        public void TestIfIntegrationsCanFindTheBestRoute(
            string graph, 
            string expectedRouteGraph, 
            Type[] specificationTypes, 
            params object[][] specsCtorsArgs)
        {
            // Arrange
            var map = new RailroadMap();
            map.BuildMap(graph);
            var comparer = new ShortestRouteComparer();
            ShortestRouteFinder routeFinder = new ShortestRouteFinder(map, comparer);
            var specs = new IRouteSpecification[specificationTypes.Length];
            for (int i = 0; i < specificationTypes.Length; i++)
            {
                IRouteSpecification specification = Substitute.For(new Type[] { specificationTypes[i] }, specsCtorsArgs[i]) as IRouteSpecification;
                specs[i] = specification;
            }

            var compositeSpecification = new AndSpecification(specs);
            IRoute expectedRoute = TestHelper.BuildRouteFromString(expectedRouteGraph);
            IRoute actualRoute = default(IRoute);

            // Act            
            actualRoute = routeFinder.FindFirstSatisfyingRoute(compositeSpecification);

            // Assert
            Assert.AreEqual(expectedRoute.ToString(), actualRoute == null ? new Route().ToString() : actualRoute.ToString());
        }
    }
}
