namespace RouteCalculator.FunctionalTest
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    /// <summary>
    /// Uses the default data given by ThoughtWorks to test the application
    /// </summary>
    [TestFixture]
    public class DefaultFunctionalTests
    {
        /// <summary>
        /// Contains the current testoutput
        /// </summary>
        private StringBuilder testOutput;

        /// <summary>
        /// Setups the test.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            this.testOutput = new StringBuilder();
            Console.SetOut(new StringWriter(this.testOutput));
        }

        /// <summary>
        /// Tears down the test.
        /// </summary>
        [TearDown]
        public void TeardownTest()
        {
            Console.SetOut(Console.Out);
            this.testOutput.Clear();
            this.testOutput = null;
        }

        /// <summary>
        /// Tests the output of the application with default test data.
        /// </summary>
        [Test]
        public void TestOutputWithDefaultData()
        {
            // Arrange            
            string output = string.Empty;
            string expected = string.Format(
                "Output #1: 9{0}" +
                "Output #2: 5{0}" +
                "Output #3: 13{0}" +
                "Output #4: 22{0}" +
                "Output #5: NO SUCH ROUTE{0}" +
                "Output #6: 2{0}" +
                "Output #7: 3{0}" +
                "Output #8: 9{0}" +
                "Output #9: 9{0}" +
                "Output #10: 7{0}",
                Environment.NewLine);

            // Act
            RouteCalculator.Program.Main(new string[] { });
            output = this.testOutput.ToString();

            // Assert
            StringAssert.Contains(expected, output);
        }
    }
}
