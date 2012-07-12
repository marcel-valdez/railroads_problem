namespace RouteCalculator
{
    using System;
    using RouteCalculator.Map;
    using System.IO;
    using System.Linq;
    using RouteCalculator.Specify;
    using System.Collections.Generic;
    using RouteCalculator.Plan;

    /// <summary>
    /// The main program in charge of execution.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// The format for the console messages.
        /// </summary>
        private const string outputMessageFormat = "Output #{0}: {1}";

        /// <summary>
        /// The output count for the console messages format.
        /// </summary>
        private static int outputCount = 1;

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
            string filename = "";
            if (args.Length > 0)
            {
                filename = args[0];
            }

            RailroadMap map = new RailroadMap();
            map.Init(File.OpenRead(filename));
            outputCount = 1;

            // Create the path specifications
            IList<IRouteSpecification> pathSpecs = new List<IRouteSpecification>();
            pathSpecs.Add(new PathSpecification("A", "B", "C"));
            pathSpecs.Add(new PathSpecification("A", "D"));
            pathSpecs.Add(new PathSpecification("A", "D", "C"));
            pathSpecs.Add(new PathSpecification("A", "E", "B", "C", "D"));
            pathSpecs.Add(new PathSpecification("A", "E", "D"));

            // Run the route finder for each path specification
            RouteFinder finder = new RouteFinder(map);
            foreach (IRouteSpecification spec in pathSpecs)
            {
                IRoute route = finder.FindFirstSatisfyingRoute(spec);
                if (route != null)
                {
                    WriteMessage(route.Distance);
                }
                else
                {
                    WriteMessage("NO SUCH ROUTE");
                }
            }
            
            // TODO: Add finder functionality for multiple results.
            IList<IRouteSpecification> tripCountSpecs = new List<IRouteSpecification>();
            tripCountSpecs.Add(
                new AndSpecification(
                    new OriginAndDestinationSpecification("C", "C"),
                    new StopsCountSpecification(0, 3)));
            tripCountSpecs.Add(
                new AndSpecification(
                    new OriginAndDestinationSpecification("A", "C"),
                    new StopsCountSpecification(4, 3)));
            foreach (IRouteSpecification spec in tripCountSpecs)
            {
                IEnumerable<IRoute> routes = finder.FindRoutes(spec);
                WriteMessage(routes.Count());                
            }
        }

        /// <summary>
        /// Writes the message to the console.
        /// </summary>
        /// <param name="result">The operation result.</param>
        private static void WriteMessage(object result)
        {
            Console.WriteLine(outputMessageFormat, outputCount++, result.ToString());
        }
    }
}
