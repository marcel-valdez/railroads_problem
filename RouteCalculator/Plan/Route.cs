namespace RouteCalculator.Plan
{
    using System.Collections.Generic;
    using RouteCalculator.Map;

    /// <summary>
    /// This class represnts a set of connected and directed railroads.
    /// </summary>
    public class Route : IRoute
    {
        /// <summary>
        /// The legs that make up this Route
        /// </summary>
        private IList<IRailroad> legs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
        /// </summary>
        public Route()
        {
            this.legs = new List<IRailroad>();
        }

        #region IRoute Members

        /// <summary>
        /// Gets the legs that conform the route.
        /// </summary>
        /// <value>
        /// The legs that conform the route.
        /// </value>
        public IEnumerable<IRailroad> Legs
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
        public ICity Origin
        {
            get
            {
                return this.legs[0].Origin;
            }
        }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        public ICity Destination
        {
            get
            {
                return this.legs[this.legs.Count - 1].Destination;
            }
        }

        /// <summary>
        /// Adds a leg to the route.
        /// </summary>
        /// <param name="railroad">The railroad leg to be added.</param>
        public void AddLeg(IRailroad railroad)
        {
            this.Distance += railroad.Length;
            this.legs.Add(railroad);
        }

        /// <summary>
        /// Creates a fly weight copy of this instance.
        /// </summary>
        /// <returns>
        /// A fly weight copy
        /// </returns>
        public IRoute FlyweightCopy()
        {
            Route flyCopy = new Route();
            flyCopy.legs = new List<IRailroad>(this.legs);
            flyCopy.Distance = this.Distance;

            return flyCopy;
        }
        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string graph = "{";

            foreach (var leg in this.legs)
            {
                graph += " " + leg.Origin.Name + leg.Destination.Name + leg.Length;
            }

            return graph + " }";
        }
    }
}
