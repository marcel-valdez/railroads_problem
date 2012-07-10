namespace RouteCalculator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// This class will be in charge of parsing a file's content and produce the corresponding RailRoad objects.
    /// </summary>
    public class RailroadMap
    {
        /// <summary>
        /// The filestream with read permissions to obtain the graph data.
        /// </summary>
        private FileStream testDataFileStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="RailroadMap"/> class.
        /// </summary>
        /// <param name="configGraphfileStream">The config graphfile stream used for initialization.</param>
        public RailroadMap(FileStream configGraphfileStream)
        {
            // TODO: Complete member initialization
            this.testDataFileStream = configGraphfileStream;
        }

        /// <summary>
        /// Gets the rail roads read from the file stream.
        /// </summary>
        /// <returns>The railroads read from the file stream</returns>
        public IEnumerable<Railroad> GetRailRoads()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the cities read from the file stream.
        /// </summary>
        /// <returns>The cities read from the file stream</returns>
        public IEnumerable<City> GetCities()
        {
            throw new NotImplementedException();
        }
    }
}
