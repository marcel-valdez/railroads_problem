namespace RouteCalculator.Test.Specify
{
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;

    /// <summary>
    /// This class contains all of the unit tests for the StopsCountSpecification class
    /// </summary>
    [TestFixture]
    public class StopsCountSpecificationTest
    {
        /// <summary>
        /// Test data used to verify it can count stops correctly
        /// </summary>
        private static object[] testData = 
        {
            // Test cases when minimum and maximum are the same            
            new object[] { 1, 1, 1, true },
            new object[] { 10, 10, 10, true },
            new object[] { 2, 2, 1, false },
            new object[] { 2, 2, 3, false },
            new object[] { 0, 0, 1, false },
            new object[] { 1, 1, 0, false },

            // Test cases when minumum and maximum are different
            // Within range
            new object[] { 0, 1, 1, true },
            new object[] { 0, 1, 0, true },
            new object[] { 1, 2, 2, true },

            // Outside of the range
            new object[] { 0, 1, 2, false },
            new object[] { 1, 2, 0, false },
            new object[] { 2, 3, 1, false }, // Too low
            new object[] { 2, 3, 4, false }, // Too high
        };

        /// <summary>
        /// Tests if stops count specification can validate correctly
        /// </summary>
        /// <param name="minimumStopCount">The route stop count.</param>
        /// <param name="maximumStopCount">The maximum stop count.</param>
        /// <param name="specStopCount">The spec stop count.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        [Test]
        [TestCaseSource("testData")]
        public void TestIfStopsCountSpecificationCanValidateCorrectly(int minimumStopCount, int maximumStopCount, int specStopCount, bool expectedResult)
        {
            // Arrange
            var target = new StopsCountSpecification(minimumStopCount, maximumStopCount);
            var route = Substitute.For<IRoute>();
            var legs = new List<Railroad>();
            for (int i = 0; i < specStopCount; i++)
            {
                legs.Add(new Railroad());
            }

            route.Legs.Returns(legs);

            // Act
            bool actual = target.Validate(route);

            // Assert
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
