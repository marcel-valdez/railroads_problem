namespace RouteCalculator.IntegrationTest
{
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;
    using RouteCalculator.Testing;

    /// <summary>
    /// This class does integration tests between each IRouteSpecification implementation and the Route class
    /// </summary>
    /// <typeparam name="T">Type of the IRouteSpecification implementation</typeparam>
    [TestFixture(typeof(StopsCountSpecification), new string[] { "AB1" }, new object[] { 1, 1 }, true)]
    [TestFixture(typeof(StopsCountSpecification), new string[] { "AB1", "BC1" }, new object[] { 1, 2 }, true)]
    [TestFixture(typeof(StopsCountSpecification), new string[] { "AB1", "BC1" }, new object[] { 1, 3 }, true)]
    [TestFixture(typeof(StopsCountSpecification), new string[] { "AB1", "BC1" }, new object[] { 3, 4 }, false)]
    [TestFixture(typeof(StopsCountSpecification), new string[] { "AB4", "BC1" }, new object[] { 3, 4 }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB" }, new object[] { "A", "B" }, true)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB", "BC" }, new object[] { "A", "B", "C" }, true)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB", "BA" }, new object[] { "A", "B", "A" }, true)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB" }, new object[] { "A", "C" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB" }, new object[] { "C", "B" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB" }, new object[] { "A", "B", "C" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB", "BC" }, new object[] { "A", "B" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB", "BD" }, new object[] { "A", "B", "C" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "DB", "BC" }, new object[] { "A", "B", "C" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB", "BC" }, new object[] { "A", "B", "D" }, false)]
    [TestFixture(typeof(PathSpecification), new string[] { "AB", "BC", "CD" }, new object[] { "A", "B", "C" }, false)]
    public class Specification_Route_IntegrationTest<T>
        where T : class, IRouteSpecification
    {
        /// <summary>
        /// Test arguments to be used for the test case
        /// </summary>
        private object[][] testArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="Specification_Route_IntegrationTest&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="path">The graph path.</param>
        /// <param name="constructorArguments">The constructor arguments for the specification.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        public Specification_Route_IntegrationTest(string[] path, object[] constructorArguments, bool expectedResult)
        {
            this.testArguments = new object[][] { new object[] { path, expectedResult, constructorArguments } };
        }

        /// <summary>
        /// Tests if specification knows if route satisfies
        /// </summary>
        /// <param name="routePath">The route path.</param>
        /// <param name="expectedResult">if set to <c>true</c> [expected result].</param>
        /// <param name="constructorArguments">The constructor arguments.</param>
        [Test]
        [TestCaseSource("testArguments")]
        public void TestIfSpecificationKnowsIfRouteSatisfies(string[] routePath, bool expectedResult, object[] constructorArguments)
        {
            // Arrange
            IRouteSpecification spec = Substitute.For<T>(constructorArguments);
            IRoute route = Substitute.For<Route>();
            IList<IRailroad> railroads = TestHelper.GenerateLegs(routePath);
            foreach (IRailroad railroad in railroads)
            {
                route.AddLeg(railroad);
            }

            // Act
            bool actualResult = spec.IsSatisfiedBy(route);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
