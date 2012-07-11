namespace RouteCalculator.Map
{
    using System.Collections.Generic;

    /// <summary>
    /// This is the interface used by any railroad map
    /// </summary>
    public interface IRailroadMap
    {
        /// <summary>
        /// Gets the rail roads read from the file stream.
        /// </summary>
        IEnumerable<IRailroad> Railroads
        {
            get;
        }

        /// <summary>
        /// Gets the cities read from the file stream.
        /// </summary>
        IEnumerable<ICity> Cities
        {
            get;
        }
    }
}
