namespace RouteCalculator.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;
    using Testing;

    /// <summary>
    /// This class contains the UNIT tests for the static Program class
    /// </summary>
    [TestFixture]
    public class ProgramTest
    {
        /// <summary>
        /// Tests if executes first conforming correctly
        /// </summary>
        /// <param name="targetMethod">The target method.</param>
        /// <param name="specResults">The spec results.</param>
        [TestCase("FindFirstConformingRouteDistance", new int[] { -1 })]
        [TestCase("FindFirstConformingRouteDistance", new int[] { 1 })]
        [TestCase("FindFirstConformingRouteDistance", new int[] { -1, -1 })]
        [TestCase("FindFirstConformingRouteDistance", new int[] { 1, -1 })]
        [TestCase("FindFirstConformingRouteDistance", new int[] { -1, 1 })]
        [TestCase("FindFirstConformingRouteDistance", new int[] { 1, 1 })]
        [TestCase("FindConformingRouteCount", new int[] { 2 })]
        [TestCase("FindConformingRouteCount", new int[] { 1 })]
        [TestCase("FindConformingRouteCount", new int[] { 0 })]
        [TestCase("FindConformingRouteCount", new int[] { 1, 1 })]
        [TestCase("FindConformingRouteCount", new int[] { 0, 1 })]
        [TestCase("FindConformingRouteCount", new int[] { 1, 0 })]

        /// <summary>
        /// Tests if executes first conforming correctly
        /// </summary>
        /// <param name="targetMethod">The target method to test.</param>
        /// <param name="specResults">The specifications results.</param>
        [Test]
        public void TestIfExecutesFirstConformingCorrectly(string targetMethod, int[] specResults)
        {
            // Arrange
            MethodCall method = TestHelper.GetPublicStaticMethod<RouteCalculator.Program>(targetMethod);
            IList<IRouteSpecification> specs = new List<IRouteSpecification>();
            IRouteFinder finder = Substitute.For<IRouteFinder>();
            FillTestData(specResults, specs, finder);

            // Act
            var results = (IEnumerable<int>)method(finder, specs);

            // Assert
            CollectionAssert.AreEquivalent(specResults, results);
        }        

        /// <summary>
        /// Fills the finder and specifications with test data.
        /// </summary>
        /// <param name="specificationResults">The specification results.</param>
        /// <param name="specifications">The specifications.</param>
        /// <param name="routeFinder">The route finder.</param>
        private static void FillTestData(int[] specificationResults, IList<IRouteSpecification> specifications, IRouteFinder routeFinder)
        {
            foreach (int specResult in specificationResults)
            {
                IRouteSpecification spec = Substitute.For<IRouteSpecification>();
                IRoute route = default(IRoute);
                IEnumerable<IRoute> results = new IRoute[] { };
                if (specResult != -1)
                {
                    route = Substitute.For<IRoute>();
                    route.Distance.ReturnsForAnyArgs(specResult);
                    results = Enumerable.Range(0, specResult).Select(n => Substitute.For<IRoute>());
                }

                routeFinder.FindFirstSatisfyingRoute(spec).Returns(route);                
                routeFinder.FindRoutes(spec).Returns(results);
                specifications.Add(spec);
            }
        }
    }
}
