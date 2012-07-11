namespace RouteCalculator.Map
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represents a city in the railroad calculator (a node)
    /// </summary>
    public class City
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="City"/> class.
        /// </summary>
        public City()
        {
            this.Incoming = new List<Railroad>();
            this.Outgoing = new List<Railroad>();
        }

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
        /// Gets the outgoing railroads.
        /// </summary>
        /// <value>
        /// The outgoing railroads.
        /// </value>
        public IList<Railroad> Outgoing
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the incoming railroads.
        /// </summary>
        /// <value>
        /// The incoming railroads.
        /// </value>
        public IList<Railroad> Incoming
        {
            get;
            private set;
        }
    }
}
