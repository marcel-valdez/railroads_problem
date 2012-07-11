namespace RouteCalculator.Test.Specify
{
    using NSubstitute;
    using NUnit.Framework;
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
            new bool[] { true, true, true, true, true },
            new bool[] { true, true, false, true, true },
            new bool[] { false, true, false, true, true },
            new bool[] { true, true, true, false, false },
            new bool[] { true, false, true, false, false },
            new bool[] { false, false, false, false, true }
        };

        /// <summary>
        /// Tests if it connects two specifications correctly
        /// </summary>
        /// <param name="predicate1Applies">if set to <c>true</c> [predicate1 applies].</param>
        /// <param name="predicate1Validates">if set to <c>true</c> [predicate1 validates].</param>
        /// <param name="predicate2Applies">if set to <c>true</c> [predicate2 applies].</param>
        /// <param name="predicate2Validates">if set to <c>true</c> [predicate2 validates].</param>
        /// <param name="expectedResult">Expected result of the validation</param>
        [Test]
        [TestCaseSource("specificationData")]
        public void TestIfItConnectsTwoSpecificationsCorrectly(bool predicate1Applies, bool predicate1Validates, bool predicate2Applies, bool predicate2Validates, bool expectedResult)
        {
            // Arrange
            var predicate1 = Substitute.For<ISpecification>();
            var predicate2 = Substitute.For<ISpecification>();
            predicate1.AppliesTo(Arg.Any<object>()).Returns(predicate1Applies);
            predicate2.AppliesTo(Arg.Any<object>()).Returns(predicate2Applies);
            predicate1.Validate(Arg.Any<object>()).Returns(predicate1Validates);
            predicate2.Validate(Arg.Any<object>()).Returns(predicate2Validates);
            var andSpecification = new AndSpecification(predicate1, predicate2);
            bool actual = false;

            // Act
            actual = andSpecification.Validate(new object());

            // Assert
            Assert.AreEqual(expectedResult, actual);
            predicate1.ReceivedWithAnyArgs().AppliesTo(Arg.Any<object>());
            if (predicate1Applies)
            {
                predicate1.ReceivedWithAnyArgs().Validate(Arg.Any<object>());
            }

            if (!predicate1Applies || (predicate1Applies && predicate1Validates))
            {
                predicate2.ReceivedWithAnyArgs().AppliesTo(Arg.Any<object>());

                if (predicate2Applies)
                {
                    predicate2.ReceivedWithAnyArgs().Validate(Arg.Any<object>());
                }
            }
        }
    }
}
