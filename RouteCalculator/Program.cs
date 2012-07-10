namespace RouteCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The main program in charge of execution.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Program"/> class from being created.
        /// </summary>
        private Program()
        {
        }

        /// <summary>
        /// Main entry point for the application
        /// </summary>
        /// <param name="args">The console arguments.</param>
        public static void Main(string[] args)
        {
            string filename = "default_data.txt";
            if (args.Length > 0)
            {
                filename = args[0];
            }

            Console.WriteLine(filename);
        }
    }
}
