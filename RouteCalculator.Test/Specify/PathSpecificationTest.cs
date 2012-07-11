namespace RouteCalculator.Test.Specify
{
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;

    /// <summary>
    /// This class contains the unit tests for the Trip Specification class
    /// </summary>
    [TestFixture]
    public class PathSpecificationTest
    {
        /// <summary>
        /// It contains the test data used for configuring the routes ofr this PathSpecificationTest
        /// </summary>
        private static object[] routesTestDataConfiguration =
        {
            new object[] 
            {
                new string[] { "AB" }, new string[] { "A", "B" }, true
            },
            new object[] 
            {
                new string[] { "AB", "CD", "BC" }, new string[] { "A", "B" }, false
            },
            new object[] 
            {
                new string[] { "AB", "BC", "CD" }, new string[] { "A", "B", "C", "D" }, true
            },
            new object[] 
            {
                new string[] { "AB", "BC", "CD" }, new string[] { "A", "B", "C", "E" }, false
            },
            new object[] 
            {
                new string[] { "AE", "EB", "BC", "CD" }, new string[] { "A", "E", "B", "C", "D" }, true
            }
        };

        /// <summary>
        /// Tests if trip specification can specify A cities route
        /// </summary>
        /// <param name="routeConfiguration">The route configuration.</param>
        /// <param name="citiesRoute">The cities route.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        [Test]
        [TestCaseSource("routesTestDataConfiguration")]
        public void TestIfTripSpecificationCanSpecifyACitiesRoute(string[] routeConfiguration, string[] citiesRoute, bool expectedResult)
        {
            // Arrange
            PathSpecification pathSpec = new PathSpecification(citiesRoute);
            IRoute route = Substitute.For<IRoute>();
            IList<Railroad> legs = TestHelper.GenerateLegs(routeConfiguration);

            route.Legs.Returns(legs);
            
            // Act
            bool actual = pathSpec.Validate(route);

            // Assert
            Assert.AreEqual(expectedResult, actual);
            Assert.Null(route.ReceivedWithAnyArgs().Legs);
        }
    }
}
