namespace RouteCalculator.IntegrationTest
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;
    using RouteCalculator.Testing;

    /// <summary>
    /// This class contains the integration tests for the ISpecification implementations along with the Route class
    /// </summary>
    [TestFixture]
    public class Specifications_And_Route_IntegrationTest
    {
        // TODO: Add test data according to parameters needed for the testcase

        /// <summary>
        /// Tests if AndSpec, DistanceSpec and OriginAndDestinationSpec can specify a Route
        /// </summary>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        /// <param name="actualRoutePath">The actual route path.</param>
        /// <param name="specifiedMinDistance">The specified min distance.</param>
        /// <param name="specifiedMaxDistance">The specified max distance.</param>
        /// <param name="specifiedOrigin">The specified origin.</param>
        /// <param name="specifiedDestination">The specified destination.</param>
        /// <param name="satisfy">if set to <c>true</c> [SatisfiedBy] will be used.</param>
        /// <param name="mightSatisfy">if set to <c>true</c> [MightBeSatisfiedBy] will be used.</param>
        [Test]
        public void TestIfAndSpecDistanceSpecAndOriginSpecCanSpecifyARoute(
            bool expectedResult, 
            string[] actualRoutePath, 
            int specifiedMinDistance, 
            int specifiedMaxDistance, 
            string specifiedOrigin, 
            string specifiedDestination, 
            bool satisfy = true, 
            bool mightSatisfy = false)
        {
            // Arrange
            IRouteSpecification originAndDestinationSpec = Substitute.For<OriginAndDestinationSpecification>(specifiedOrigin, specifiedDestination);
            IRouteSpecification distanceSpec = Substitute.For<DistanceSpecification>(specifiedMinDistance, specifiedMaxDistance);
            IRouteSpecification andSpec = Substitute.For<AndSpecification>(originAndDestinationSpec, distanceSpec);
            IRoute route = Substitute.For<Route>();
            IList<IRailroad> legs = TestHelper.GenerateLegs(actualRoutePath);
            foreach (IRailroad leg in legs)
            {
                route.AddLeg(leg);
            }
            
            bool actualResult = true;

            // Act
            if (satisfy)
            {
                actualResult = GetSatisfyResult(originAndDestinationSpec, distanceSpec, andSpec, route);
            }

            if (mightSatisfy)
            {
                actualResult = actualResult && GetMightSatisfyResult(originAndDestinationSpec, distanceSpec, andSpec, route);
            }

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Gets the actual result for satisfy test.
        /// </summary>
        /// <param name="originAndDestinationSpec">The origin and destination specification to use.</param>
        /// <param name="distanceSpec">The distance specification to use.</param>
        /// <param name="andSpec">The AndSpecification to use.</param>
        /// <param name="route">The route to test.</param>
        /// <returns>The result of the AndSpecification</returns>
        private static bool GetSatisfyResult(IRouteSpecification originAndDestinationSpec, IRouteSpecification distanceSpec, IRouteSpecification andSpec, IRoute route)
        {
            bool actualResult = andSpec.IsSatisfiedBy(route);
            originAndDestinationSpec.ReceivedWithAnyArgs().IsSatisfiedBy(null);
            Assert.Null(route.ReceivedWithAnyArgs().Origin);
            if (originAndDestinationSpec.IsSatisfiedBy(route))
            {
                Assert.Null(route.ReceivedWithAnyArgs().Destination);
                distanceSpec.ReceivedWithAnyArgs().IsSatisfiedBy(null);
                Assert.Null(route.ReceivedWithAnyArgs().Distance);
            }

            return actualResult;
        }

        /// <summary>
        /// Gets the might satisfy result.
        /// </summary>
        /// <param name="originAndDestinationSpec">The origin and destination specification to use.</param>
        /// <param name="distanceSpec">The distance specification to use.</param>
        /// <param name="andSpec">The AndSpecification to use.</param>
        /// <param name="route">The route to test.</param>
        /// <returns>The result of the AndSpecification</returns>
        private static bool GetMightSatisfyResult(IRouteSpecification originAndDestinationSpec, IRouteSpecification distanceSpec, IRouteSpecification andSpec, IRoute route)
        {
            bool mightSatisfyResult = andSpec.MightBeSatisfiedBy(route);
            originAndDestinationSpec.ReceivedWithAnyArgs().MightBeSatisfiedBy(null);
            Assert.Null(route.ReceivedWithAnyArgs().Origin);
            if (originAndDestinationSpec.MightBeSatisfiedBy(route))
            {
                distanceSpec.ReceivedWithAnyArgs().MightBeSatisfiedBy(null);
                Assert.Null(route.ReceivedWithAnyArgs().Distance);
            }

            return mightSatisfyResult;
        }
    }
}
