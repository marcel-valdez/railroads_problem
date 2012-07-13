namespace RouteCalculator.Test.Plan
{
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;

    /// <summary>
    /// This class contains the unit tests for the shortest route comparer.
    /// </summary>
    [TestFixture]
    public class ShortestRouteComparerTest
    {
        /// <summary>
        /// Tests if it returns the correct type
        /// </summary>
        [Test]
        public void TestIfItReturnsACorrectType()
        {
            // Arrange
            ShortestRouteComparer comparer = new ShortestRouteComparer();            

            // Act
            IRouteComparison actual = comparer.Is(Substitute.For<IRoute>());

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf(typeof(ShortestRouteComparison), actual);
        }
    }
}
