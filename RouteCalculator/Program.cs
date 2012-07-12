namespace RouteCalculator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;
    using RouteCalculator.Specify;

    /// <summary>
    /// The main program in charge of execution.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// The format for the console messages.
        /// </summary>
        private const string OUTPUT_MESSAGE_FORMAT = "Output #{0}: {1}";

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
            string filename = string.Empty;
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
            FindFirstConformingRoute(finder, pathSpecs);

            // TODO: Add finder functionality for multiple results.
            var tripCountSpecs = new List<IRouteSpecification>();
            tripCountSpecs.Add(
                new AndSpecification(
                    new OriginAndDestinationSpecification("C", "C"),
                    new StopsCountSpecification(0, 3)));
            tripCountSpecs.Add(
                new AndSpecification(
                    new OriginAndDestinationSpecification("A", "C"),
                    new StopsCountSpecification(4, 3)));
            FindConformingRouteCount(finder, tripCountSpecs);

            // TODO: Add a shortest route finder
            IRouteFinder shortestRouteFinder = new ShortestRouteFinder(map);
            var shortestRouteSpecs = new List<IRouteSpecification>();
            shortestRouteSpecs.Add(
                new OriginAndDestinationSpecification("A", "C"));
            shortestRouteSpecs.Add(
                new OriginAndDestinationSpecification("B", "B"));

            tripCountSpecs.Clear();
            tripCountSpecs.Add(
                new AndSpecification(
                    new OriginAndDestinationSpecification("C", "C"),
                    new DistanceSpecification(0, 29)));
            FindConformingRouteCount(finder, tripCountSpecs);
        }

        /// <summary>
        /// Finds the conforming route count.
        /// </summary>
        /// <param name="finder">The route finder.</param>
        /// <param name="specifications">The route specifications.</param>
        private static void FindConformingRouteCount(IRouteFinder finder, IList<IRouteSpecification> specifications)
        {
            foreach (IRouteSpecification spec in specifications)
            {
                IEnumerable<IRoute> routes = finder.FindRoutes(spec);
                WriteMessage(routes.Count());
            }
        }

        /// <summary>
        /// Finds the first conforming route.
        /// </summary>
        /// <param name="routeFinder">The route finder.</param>
        /// <param name="specifications">The specifications.</param>
        private static void FindFirstConformingRoute(IRouteFinder routeFinder, IList<IRouteSpecification> specifications)
        {
            foreach (IRouteSpecification spec in specifications)
            {
                IRoute route = routeFinder.FindFirstSatisfyingRoute(spec);

                if (route != null)
                {
                    WriteMessage(route.Distance);
                }
                else
                {
                    WriteMessage("NO SUCH ROUTE");
                }
            }
        }

        /// <summary>
        /// Writes the message to the console.
        /// </summary>
        /// <param name="result">The operation result.</param>
        private static void WriteMessage(object result)
        {
            Console.WriteLine(OUTPUT_MESSAGE_FORMAT, outputCount++, result.ToString());
        }
    }
}
