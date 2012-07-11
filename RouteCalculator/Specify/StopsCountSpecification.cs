namespace RouteCalculator.Specify
{
    using System.Linq;
    using RouteCalculator.Plan;    

    /// <summary>
    /// This classed is used to specify the origin and destination of a route
    /// </summary>
    public class StopsCountSpecification : IRouteSpecification
    {
        /// <summary>
        /// The maximum number of stops specified
        /// </summary>
        private int maxStopsCount;

        /// <summary>
        /// The minimum number of stops specified
        /// </summary>
        private int minStopsCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="StopsCountSpecification"/> class.
        /// </summary>
        public StopsCountSpecification()
        {
            this.maxStopsCount = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopsCountSpecification"/> class.
        /// </summary>
        /// <param name="minStops">The minimum number of stops.</param>
        /// <param name="maxStops">The maximum number of stops.</param>
        public StopsCountSpecification(int minStops, int maxStops)
        {
            this.maxStopsCount = maxStops;
            this.minStopsCount = minStops;
        }

        #region IRouteSpecification Members

        /// <summary>
        /// Validates the specified the object.
        /// </summary>
        /// <param name="validatedOn">The object to validate with this specification.</param>
        /// <returns>
        /// true if the object conforms to this specification
        /// </returns>
        public bool Validate(IRoute validatedOn)
        {
            IRoute route = (IRoute)validatedOn;
            int legsCount = route.Legs.Count();
            return legsCount <= this.maxStopsCount && legsCount >= this.minStopsCount;
        }

        #endregion
    }
}
