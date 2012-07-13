namespace RouteCalculator.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using NSubstitute;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;

    /// <summary>
    /// Represents a reflected method call
    /// </summary>
    /// <param name="arguments">The arguments to be passed to the method.</param>
    /// <returns>The result of calling the method</returns>
    public delegate object MethodCall(params object[] arguments);

    /// <summary>
    /// This class contains method extensions to be used by the test classes.
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Private instance binding flags
        /// </summary>
        private const BindingFlags PRIVATE = BindingFlags.Instance | BindingFlags.NonPublic;

        /// <summary>
        /// Private static binding flags
        /// </summary>
        private const BindingFlags PRIVATE_STATIC = BindingFlags.NonPublic | BindingFlags.Static;

        /// <summary>
        /// Gets a private static method.
        /// </summary>
        /// <typeparam name="T">Type reflect</typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>A MethodCall delegate</returns>
        [SuppressMessage(
            category: "Microsoft.Design",
            checkId: "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "The type parameter is necessary.")]
        public static MethodCall GetPrivateStaticMethod<T>(string methodName)
        {
            MethodInfo methodInfo = typeof(T).GetMethod(methodName, PRIVATE_STATIC);
            return (object[] parameters) => methodInfo.Invoke(null, parameters);
        }

        /// <summary>
        /// Gets a private method.
        /// </summary>
        /// <typeparam name="T">The type to reflect</typeparam>
        /// <param name="obj">The object to receive the invocation.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The value returned by the method</returns>
        public static MethodCall GetPrivateMethod<T>(this T obj, string methodName)
        {
            return (object[] parameters) => typeof(T).GetMethod(methodName, PRIVATE).Invoke(obj, parameters);
        }

        /// <summary>
        /// Generates the legs using the routeConfiguration.
        /// </summary>
        /// <param name="routeConfiguration">The route configuration.</param>
        /// <param name="cities">The preconfigured cities.</param>
        /// <returns>
        /// The legs configured
        /// </returns>
        public static IList<IRailroad> GenerateLegs(string routeConfiguration, IList<ICity> cities = null)
        {
            return GenerateLegs(routeConfiguration.Split(' ', ','), cities);
        }

        /// <summary>
        /// Generates the legs using the routeConfiguration.
        /// </summary>
        /// <param name="routeConfiguration">The route configuration.</param>
        /// <param name="cities">The preconfigured cities.</param>
        /// <returns>
        /// The legs configured
        /// </returns>
        public static IList<IRailroad> GenerateLegs(string[] routeConfiguration, IList<ICity> cities = null)
        {
            IList<IRailroad> legs = new List<IRailroad>();
            if (cities == null)
            {
                cities = GenerateCities(routeConfiguration);
            }

            foreach (string railroadConfiguration in routeConfiguration)
            {
                IRailroad railroad = Substitute.For<IRailroad>();
                string originName = railroadConfiguration[0].ToString();
                string destinationName = railroadConfiguration[1].ToString();
                ICity originCity = cities.First(city => city.Name.Equals(originName));
                ICity destinationCity = cities.First(city => city.Name.Equals(destinationName));
                if (railroadConfiguration.Length > 2)
                {
                    railroad.Length.ReturnsForAnyArgs(int.Parse(railroadConfiguration.Substring(2)));
                }

                railroad.Origin.ReturnsForAnyArgs(originCity);
                railroad.Destination.ReturnsForAnyArgs(destinationCity);
                legs.Add(railroad);
                IList<IRailroad> outgoingRailroads = originCity.Outgoing;
                outgoingRailroads.Add(railroad);
            }

            return legs;
        }

        /// <summary>
        /// Generates the cities using the graph configuration.
        /// </summary>
        /// <param name="graphConfiguration">The graph configuration.</param>
        /// <returns>The cities identified</returns>
        public static IList<ICity> GenerateCities(string graphConfiguration)
        {
            return GenerateCities(graphConfiguration.Split(' ', ','));
        }

        /// <summary>
        /// Generates the cities using the route configuration.
        /// </summary>
        /// <param name="routeConfiguration">The route configuration.</param>
        /// <returns>The cities identified</returns>
        public static IList<ICity> GenerateCities(string[] routeConfiguration)
        {
            IList<ICity> cities = new List<ICity>();

            foreach (string railroadConfiguration in routeConfiguration)
            {
                string originCityName = railroadConfiguration[0].ToString();
                string destinationCityName = railroadConfiguration[1].ToString();
                AddIfNotIncluded(cities, originCityName);
                AddIfNotIncluded(cities, destinationCityName);
            }

            return cities;
        }

        /// <summary>
        /// Builds the route from a route path string.
        /// Graph examples: [AB1 BC1] or [AB1,BC1] or [AB1, BC1] all are good.
        /// </summary>
        /// <param name="routePath">The route path.</param>
        /// <returns>The resulting route.</returns>
        public static Route BuildRouteFromString(string routePath)
        {
            Route route = new Route();
            string[] railroadsConfigs = routePath.Split(new string[] { ", ", " ", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string railroadConfig in railroadsConfigs)
            {
                string originCityName = railroadConfig.Substring(0, 1);
                string destinationCityName = railroadConfig.Substring(1, 1);
                int legLength = int.Parse(railroadConfig.Substring(2));
                City originCity = GetOrCreateCity(route, originCityName);
                City destinationCity = GetOrCreateCity(route, destinationCityName);
                Railroad railroad = new Railroad()
                {
                    Origin = originCity,
                    Destination = destinationCity,
                    Length = legLength
                };

                route.AddLeg(railroad);
            }

            return route;
        }

        /// <summary>
        /// Builds the mock route.
        /// </summary>
        /// <param name="specifiedRoute">The specified route.</param>
        /// <returns>The build route</returns>
        public static IRoute BuildMockRoute(string specifiedRoute)
        {
            return BuildMockRoute(specifiedRoute.Split(' ', ','));
        }

        /// <summary>
        /// Builds a mock IRoute.
        /// </summary>
        /// <param name="specifiedRoute">The specified route.</param>
        /// <returns>The built route.</returns>
        public static IRoute BuildMockRoute(string[] specifiedRoute)
        {
            IRoute routeSpec = Substitute.For<IRoute>();
            IList<IRailroad> specifiedLegs = TestHelper.GenerateLegs(specifiedRoute);
            routeSpec.Legs.ReturnsForAnyArgs(specifiedLegs);

            // Specify distance
            int distance = specifiedLegs.Sum(leg => leg.Length);
            routeSpec.Distance.ReturnsForAnyArgs(distance);

            // Specify origin
            ICity origin = specifiedLegs.First().Origin;
            routeSpec.Origin.ReturnsForAnyArgs(origin);

            // Specify destination
            ICity destination = specifiedLegs.Last().Destination;
            routeSpec.Destination.ReturnsForAnyArgs(destination);

            return routeSpec;
        }

        /// <summary>
        /// Adds a city with the <paramref name="originCityName"/> if not included already in the city list.
        /// </summary>
        /// <param name="cities">The city list.</param>
        /// <param name="originCityName">Name of the city.</param>
        private static void AddIfNotIncluded(IList<ICity> cities, string originCityName)
        {
            if (!cities.Any(city => city.Name.Equals(originCityName)))
            {
                ICity city = Substitute.For<ICity>();
                city.Name.Returns(originCityName);
                IList<IRailroad> railroadList = new List<IRailroad>();
                city.Outgoing.ReturnsForAnyArgs(railroadList);
                cities.Add(city);
            }
        }

        /// <summary>
        /// Gets the city. Null if it is not found.
        /// </summary>
        /// <param name="route">The route on which to look.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <returns>The city with the city name, null if not found.</returns>
        private static City GetOrCreateCity(Route route, string cityName)
        {
            City city = route.Legs.SelectMany(leg => new ICity[] { leg.Origin, leg.Destination })
                             .FirstOrDefault(item => item.Name.Equals(cityName)) as City;

             if (city == null)
             {
                 city = new City()
                 {
                     Name = cityName
                 };
             }

             return city;
        }
    }
}
