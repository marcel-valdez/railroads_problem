namespace RouteCalculator.Plan
{
    using System.Collections.Generic;

    /// <summary>
    /// This class represnts a set of connected and directed railroads.
    /// </summary>
    public class Route : IRoute
    {
        /// <summary>
        /// The legs that make up this Route
        /// </summary>
        private IList<Railroad> legs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
        /// </summary>
        public Route()
        {
            this.legs = new List<Railroad>();
        }

        #region IRoute Members

        /// <summary>
        /// Gets the legs that conform the route.
        /// </summary>
        /// <value>
        /// The legs that conform the route.
        /// </value>
        public IEnumerable<Railroad> Legs
        {
            get
            {
                return this.legs;
            }
        }

        /// <summary>
        /// Gets the total distance.
        /// </summary>
        public int Distance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        public City Origin
        {
            get
            {
                return this.legs[0].Origin;
            }
        }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        public City Destination
        {
            get
            {
                return this.legs[this.legs.Count - 1].Destination;
            }
        }

        #endregion

        /// <summary>
        /// Adds a leg to the route.
        /// </summary>
        /// <param name="leg">The leg to be added.</param>
        public void AddLeg(Railroad leg)
        {
            this.Distance += leg.Length;
            this.legs.Add(leg);
        }
    }
}
