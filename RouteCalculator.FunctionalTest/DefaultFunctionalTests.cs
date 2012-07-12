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
        /// Tests the route calculator results.
        /// </summary>
        /// <param name="filename">The filename with the test data.</param>
        /// <param name="expectedOutput">The expected output of the target.</param>
        [Test]
        [TestCase("default_data.txt", "Output #1: 9")]
        [TestCase("default_data.txt", "Output #2: 5")]
        [TestCase("default_data.txt", "Output #3: 13")]
        [TestCase("default_data.txt", "Output #4: 22")]
        [TestCase("default_data.txt", "Output #5: NO SUCH ROUTE")]
        [TestCase("default_data.txt", "Output #6: 2")]
        [TestCase("default_data.txt", "Output #7: 3")]
        [TestCase("default_data.txt", "Output #8: 9")]
        [TestCase("default_data.txt", "Output #9: 9")]
        [TestCase("default_data.txt", "Output #10: 7")]
        public void TestRouteCalculatorResults(string filename, string expectedOutput)
        {
            // Arrange            
            string output = string.Empty;

            // Act
            RouteCalculator.Program.Main(new string[] { filename });
            output = this.testOutput.ToString();

            // Assert
            StringAssert.Contains(expectedOutput, output);
        }
    }
}
