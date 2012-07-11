namespace RouteCalculator.Testing.Plan
{
    using System.Linq;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;

    /// <summary>
    /// This class contains all of the unit tests for the Route class
    /// </summary>
    [TestFixture]
    public class RouteTest
    {
        /// <summary>
        /// Test data for the distance validation test
        /// </summary>
        private static object[] distanceData = 
        {
            new object[] { new int[] { 1 } },
            new object[] { new int[] { 0 } },
            new object[] { new int[] { 2 } },
            new object[] { new int[] { 1, 0 } },
            new object[] { new int[] { 1, 1 } },
            new object[] { new int[] { 1, 1, 1 } }
        };

        /// <summary>
        /// Test data for the origin and destination test
        /// </summary>
        private static object[] cityData = 
        {
            new object[] { new string[] { "A", "B" } },
            new object[] { new string[] { "A", "B", "C" } },
            new object[] { new string[] { "A", "B", "A" } },
            new object[] { new string[] { "A", "B", "A", "C" } }
        };

        /// <summary>
        /// Tests if it can calculate distance correctly
        /// </summary>
        /// <param name="legDistances">The leg distances.</param>
        [Test]
        [TestCaseSource("distanceData")]
        public void TestIfItCanCalculateDistanceCorrectly(int[] legDistances)
        {
            // Arrange
            var target = new Route();
            for (int i = 0; i < legDistances.Length; i++)
            {
                IRailroad mockRailRoad = Substitute.For<IRailroad>();
                mockRailRoad.Length = legDistances[i];
                target.AddLeg(mockRailRoad);
            }

            int expectedTotalDistance = legDistances.Sum();

            // Act
            int actualTotalDistance = target.Distance;

            // Assert
            Assert.AreEqual(expectedTotalDistance, actualTotalDistance);
        }

        /// <summary>
        /// Tests if it know its origin and destination
        /// </summary>
        /// <param name="cityNames">The city names.</param>
        [Test]
        [TestCaseSource("cityData")]
        public void TestIfItKnowItsOriginAndDestination(params string[] cityNames)
        {
            // Arrange
            var target = new Route();
            string expectedOrigin = cityNames[0];
            string expectedDestination = cityNames[cityNames.Length - 1];

            // Act
            for (int i = 0; i < cityNames.Length - 1; i++)
            {
                var originCity = Substitute.For<ICity>();
                originCity.Name = cityNames[i];
                var destinationCity = Substitute.For<ICity>();
                destinationCity.Name = cityNames[i + 1];
                IRailroad leg = Substitute.For<IRailroad>();
                leg.Origin = originCity;
                leg.Destination = destinationCity;
                target.AddLeg(leg);
            }

            // Assert
            Assert.AreEqual(expectedOrigin, target.Origin.Name);
            Assert.AreEqual(expectedDestination, target.Destination.Name);
        }
    }
}
