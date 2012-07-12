namespace RouteCalculator.Testing
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;    
    using NSubstitute;
    using RouteCalculator.Map;

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
            return (object[] parameters) => typeof(T).GetMethod(methodName, PRIVATE_STATIC).Invoke(null, parameters);
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
                    railroad.Length.Returns(int.Parse(railroadConfiguration.Substring(2)));
                }

                railroad.Origin.Returns(originCity);
                railroad.Destination.Returns(destinationCity);
                legs.Add(railroad);
                originCity.Outgoing.Add(railroad);
            }

            return legs;
        }

        /// <summary>
        /// Generates the cities using the routeConfiguration.
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
                city.Outgoing.Returns(railroadList);
                cities.Add(city);
            }
        }
    }
}
