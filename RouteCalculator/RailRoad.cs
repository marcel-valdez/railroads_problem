namespace RouteCalculator
{
    /// <summary>
    /// This class will represent the railroad connecting two cities.
    /// </summary>
    public class Railroad
    {
        /// <summary>
        /// Gets or sets the length of the railroad.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public virtual int Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the City origin of the Railroad.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public City Origin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the City destination of the railroad.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public City Destination
        {
            get;
            set;
        }
    }
}
