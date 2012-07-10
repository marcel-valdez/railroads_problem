namespace RouteCalculator
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents a city in the railroad calculator (a node)
    /// </summary>
    public class City
    {
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the outgoing railroads.
        /// </summary>
        /// <value>
        /// The outgoing railroads.
        /// </value>
        public IEnumerable<Railroad> Outgoing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the incoming railroads.
        /// </summary>
        /// <value>
        /// The incoming railroads.
        /// </value>
        public IEnumerable<Railroad> Incoming
        {
            get;
            set;
        }
    }
}
