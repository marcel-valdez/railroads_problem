namespace RouteCalculator.Test.Specify
{
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;

    /// <summary>
    /// This class contains the tests that are common to all ISpecification implementations
    /// </summary>
    /// <typeparam name="TSpec">The type of the specification.</typeparam>
    /// <typeparam name="TValidated">The type of the validated.</typeparam>
    [TestFixture(typeof(StopsCountSpecification), typeof(IRoute))]
    [TestFixture(typeof(OriginAndEndSpecification), typeof(IRoute))]
    [TestFixture(typeof(PathSpecification), typeof(IRoute))]
    [TestFixture(typeof(DistanceSpecification), typeof(IRoute))]
    public class SpecificationTest<TSpec, TValidated>
        where TSpec : IRouteSpecification, new()
        where TValidated : class
    {
        /// <summary>
        /// Tests if the specification applies to the given type
        /// </summary>
        [Test]
        public void TestIfItApplies()
        {
            // Arrange
            var spec = new TSpec();

            // Act
            bool actual = spec.AppliesTo(Substitute.For<TValidated>());

            // Assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Tests if it doesn't apply to an object
        /// </summary>
        [Test]
        public void TestIfItDoesNotApplyToAnObject()
        {
            // Arrange
            IRouteSpecification spec = new TSpec();

            // Act
            bool actual = spec.AppliesTo(new object());

            // Assert
            Assert.IsFalse(actual);
        }
    }
}
