namespace RouteCalculator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// This class will be in charge of parsing a file's content and produce the corresponding RailRoad objects.
    /// </summary>
    public class RailroadMap
    {
        /// <summary>
        /// The railroads in the map
        /// </summary>
        private IList<Railroad> railroads;

        /// <summary>
        /// The cities in the map
        /// </summary>
        private IList<City> cities;

        /// <summary>
        /// Initializes a new instance of the <see cref="RailroadMap"/> class.
        /// </summary>
        public RailroadMap()
        {
            this.railroads = new List<Railroad>();
            this.cities = new List<City>();
        }

        #region Properties
        /// <summary>
        /// Gets the rail roads read from the file stream.
        /// </summary>        
        public IEnumerable<Railroad> Railroads
        {
            get
            {
                return this.railroads;
            }
        }

        /// <summary>
        /// Gets the cities read from the file stream.
        /// </summary>
        public IEnumerable<City> Cities
        {
            get
            {
                return this.cities;
            }
        }
        #endregion
        #region Public methods
        /// <summary>
        /// Initializes this class with the railroads and cities.
        /// </summary>
        /// <param name="stream">The stream of the configuration file.</param>
        public void Init(FileStream stream)
        {
            string configGraph = String.Empty;
            configGraph = RailroadMap.ReadContent(stream);
            this.BuildMap(configGraph);
        }
        #endregion
        #region Private methods
        /// <summary>
        /// Reads the file stream content.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <returns>The configuration of the paths in the map</returns>
        private static string ReadContent(FileStream stream)
        {
            string configPath;
            using (stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    configPath = reader.ReadLine();
                }
            }

            return configPath.Replace("Graph: ", String.Empty);
        }

        /// <summary>
        /// Gets or creates a city.
        /// </summary>
        /// <param name="originCityName">Name of the origin city.</param>
        /// <returns>The city with the name <paramref name="originCityName"/></returns>
        private City GetOrCreateCity(string originCityName)
        {
            City city = null;
            if (!this.cities.Any(item => item.Name == originCityName))
            {
                city = new City();
                city.Name = originCityName;
                this.cities.Add(city);
            }
            else
            {
                city = this.cities.First(item => item.Name == originCityName);
            }

            return city;
        }

        /// <summary>
        /// Builds the railroad map.
        /// </summary>
        /// <param name="graph">The string with the railroads configuration graph.</param>
        private void BuildMap(string graph)
        {
            string[] paths = graph.Split(' ');
            foreach (string path in paths)
            {
                string originCityName = path.Substring(0, 1);
                string destinationCityName = path.Substring(1, 1);
                int railroadLength = Int32.Parse(path.Substring(2));

                City originCity = this.GetOrCreateCity(originCityName);
                City destinationCity = this.GetOrCreateCity(destinationCityName);
                Railroad newRailroad = new Railroad();
                newRailroad.Origin = originCity;
                newRailroad.Destination = destinationCity;
                newRailroad.Length = railroadLength;
                this.railroads.Add(newRailroad);
                originCity.Outgoing.Add(newRailroad);
                destinationCity.Incoming.Add(newRailroad);
            }
        }
        #endregion
    }
}
