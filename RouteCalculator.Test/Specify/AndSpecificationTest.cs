﻿namespace RouteCalculator.Test.Specify
{
    using NSubstitute;
    using NUnit.Framework;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;

    /// <summary>
    /// Contains the tests for the AndSpecification class
    /// </summary>
    [TestFixture]
    public class AndSpecificationTest
    {
        /// <summary>
        /// Specification data used to validate the and specification
        /// </summary>
        private static object[] specificationData = 
        {
            // Predicate1, Predicate 2, Expected Result
            new bool[] { true, true, true },
            new bool[] { true, false, false },
            new bool[] { false, true, false },
            new bool[] { false, false, false }
        };

        /// <summary>
        /// Tests if it connects two specifications correctly
        /// </summary>
        /// <param name="predicate1Validates">if set to <c>true</c> [predicate1 validates].</param>
        /// <param name="predicate2Validates">if set to <c>true</c> [predicate2 validates].</param>
        /// <param name="expectedResult">Expected result of the validation</param>
        [Test]
        [TestCaseSource("specificationData")]
        public void TestIfItConnectsTwoSpecificationsCorrectly(bool predicate1Validates, bool predicate2Validates, bool expectedResult)
        {
            // Arrange
            var predicate1 = Substitute.For<IRouteSpecification>();
            var predicate2 = Substitute.For<IRouteSpecification>();
            predicate1.IsSatisfiedBy(Arg.Any<IRoute>()).Returns(predicate1Validates);
            predicate2.IsSatisfiedBy(Arg.Any<IRoute>()).Returns(predicate2Validates);
            predicate1.MightBeSatisfiedBy(Arg.Any<IRoute>()).Returns(predicate1Validates);
            predicate2.MightBeSatisfiedBy(Arg.Any<IRoute>()).Returns(predicate2Validates);
            var andSpecification = new AndSpecification(predicate1, predicate2);
            bool satisfiedBy = false;
            bool mightBeSatisfiedBy = false;

            // Act
            satisfiedBy = andSpecification.IsSatisfiedBy(Substitute.For<IRoute>());
            mightBeSatisfiedBy = andSpecification.MightBeSatisfiedBy(Substitute.For<IRoute>());

            // Assert
            Assert.AreEqual(expectedResult, satisfiedBy);
            Assert.AreEqual(expectedResult, mightBeSatisfiedBy);
            predicate1.ReceivedWithAnyArgs().IsSatisfiedBy(null);
            predicate1.ReceivedWithAnyArgs().MightBeSatisfiedBy(null);
            if (predicate1Validates)
            {
                predicate2.ReceivedWithAnyArgs().IsSatisfiedBy(null);
                predicate2.ReceivedWithAnyArgs().MightBeSatisfiedBy(null);
            }
        }
    }
}
