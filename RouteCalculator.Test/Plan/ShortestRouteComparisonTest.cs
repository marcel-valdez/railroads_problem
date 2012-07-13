namespace RouteCalculator.Test.Plan
{
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;

    /// <summary>
    /// Represents the comparison result to expect.
    /// </summary>
    public enum ComparisonResult
    {
        /// <summary>
        /// Route A is better
        /// </summary>
        RouteAIsBetter,

        /// <summary>
        /// Route B is better
        /// </summary>
        RouteBIsBetter,

        /// <summary>
        /// Routes are equal
        /// </summary>
        RoutesAreEqual
    }

    /// <summary>
    /// This class contains the unit tests for the ShortestRouteComparison class
    /// </summary>
    [TestFixture]
    public class ShortestRouteComparisonTest
    {
        /// <summary>
        /// Tests if it compares correctly
        /// </summary>
        /// <param name="routeADistance">The route A distance.</param>
        /// <param name="routeBDistance">The route B distance.</param>
        /// <param name="expectedResult">The expected result.</param>
        [TestCase(2, 3, ComparisonResult.RouteAIsBetter)]
        [TestCase(3, 2, ComparisonResult.RouteBIsBetter)]
        [TestCase(2, 2, ComparisonResult.RoutesAreEqual)]
        [TestCase(0, 1, ComparisonResult.RouteAIsBetter)]
        [TestCase(1, 0, ComparisonResult.RouteBIsBetter)]
        [TestCase(1, 1, ComparisonResult.RoutesAreEqual)]
        [TestCase(0, 0, ComparisonResult.RoutesAreEqual)]
        [Test]
        public void TestIfItComparesCorrectly(int routeADistance, int routeBDistance, ComparisonResult expectedResult)
        {
            // Arrange
            IRoute routeA = Substitute.For<IRoute>();
            routeA.Distance.ReturnsForAnyArgs(routeADistance);
            IRoute routeB = Substitute.For<IRoute>();
            routeB.Distance.ReturnsForAnyArgs(routeBDistance);

            ShortestRouteComparison comparison = new ShortestRouteComparison(routeA);

            // Act
            bool routeAIsBetter = comparison.BetterThan(routeB);
            bool routeBIsBetter = comparison.WorseThan(routeB);
            bool betterThanWorse = comparison.BetterThan(Worst.Route);
            bool worseThanWorst = comparison.WorseThan(Worst.Route);
            bool equivalentToWorst = comparison.WorseThan(Worst.Route);

            // Assert
            Assert.IsTrue(betterThanWorse);
            Assert.IsFalse(worseThanWorst);
            Assert.IsFalse(equivalentToWorst);

            switch (expectedResult)
            {
                case ComparisonResult.RouteAIsBetter:
                    Assert.IsTrue(routeAIsBetter);
                    Assert.IsFalse(routeBIsBetter);
                    break;
                case ComparisonResult.RouteBIsBetter:
                    Assert.IsFalse(routeAIsBetter, string.Format("{0} is more than {1} so A should be worse.", routeADistance, routeBDistance));
                    Assert.IsTrue(routeBIsBetter);
                    break;
                case ComparisonResult.RoutesAreEqual:
                    Assert.IsFalse(routeAIsBetter);
                    Assert.IsFalse(routeBIsBetter);
                    break;
            }
        }
    }
}
