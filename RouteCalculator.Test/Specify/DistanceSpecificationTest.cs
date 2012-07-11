namespace RouteCalculator.Test.Specify
{
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;

    /// <summary>
    /// This class contains all of the unit tests for the DistanceSpecification class
    /// </summary>
    [TestFixture]
    public class DistanceSpecificationTest
    {
        /// <summary>
        /// Test data to specify route distance, min and max specified distance, and expected validation result.
        /// </summary>
        private static object[] distanceRouteData =
        {
            // Min and max distances are the same
            // Valid distances
            new object[] { 0, 0, 0, true },
            new object[] { 1, 1, 1, true },
            new object[] { 2, 2, 2, true },

            // Invalid distances
            new object[] { 1, 0, 0, false },
            new object[] { 0, 1, 1, false },
            new object[] { 2, 1, 1, false },
            new object[] { 1, 2, 2, false },
            new object[] { 3, 2, 2, false },

            // Min and max distances are different
            // Valid distances
            new object[] { 0, 0, 1, true },
            new object[] { 1, 0, 1, true },
            new object[] { 0, 0, 2, true },
            new object[] { 1, 0, 2, true },
            new object[] { 2, 0, 2, true },
            new object[] { 1, 1, 2, true },
            new object[] { 2, 1, 2, true },
            new object[] { 1, 1, 3, true },
            new object[] { 2, 1, 3, true },
            new object[] { 3, 1, 3, true },

            // Invalid distances
            new object[] { 2, 0, 1, false },            
            new object[] { 3, 0, 2, false },
            new object[] { 0, 1, 2, false },
            new object[] { 3, 1, 2, false },
            new object[] { 1, 2, 4, false },
        };

        /// <summary>
        /// Tests if it can specify correctly the required distance
        /// </summary>
        /// <param name="routeDistance">The route distance.</param>
        /// <param name="minDistance">The min distance.</param>
        /// <param name="maxDistance">The max distance.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        [Test]
        [TestCaseSource("distanceRouteData")]
        public void TestIfItCanSpecifyCorrectlyTheRequiredDistance(int routeDistance, int minDistance, int maxDistance, bool expectedResult)
        {
            // Arrange
            var target = new DistanceSpecification(minDistance, maxDistance);
            IRoute route = Substitute.For<IRoute>();
            route.Distance.Returns(routeDistance);
            
            // Act
            bool actualResult = target.Validate(route);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(0, route.Received().Distance);
        }
    }
}
