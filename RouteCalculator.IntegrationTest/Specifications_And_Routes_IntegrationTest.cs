namespace RouteCalculator.IntegrationTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using RouteCalculator.Specify;
    using RouteCalculator.Plan;
    using RouteCalculator.Map;

    [TestFixture]
    public class Specifications_And_Routes_IntegrationTest
    {
        /// <summary>
        /// Tests if AndSpec, DistanceSpec and OriginAndDestinationSpec can specify a Route
        /// </summary>
        [Test]
        public void TestIfAndSpecDistanceSpecAndOriginSpecCanSpecifyARoute()
        {
            // Arrange
            string destination = String.Empty;
            string origin = String.Empty;
            int maxDistance = 0;
            int minDistance = 0;
            IRouteSpecification originAndDestinationSpec = new OriginAndDestinationSpecification(origin, destination);
            IRouteSpecification distanceSpec = new DistanceSpecification(minDistance, maxDistance);
            IRouteSpecification andSpec = new AndSpecification(originAndDestinationSpec, distanceSpec);
            IRoute route = new Route();
            IList<Railroad> legs = TestHelper

            // Act


            // Assert

        }
    }
}
