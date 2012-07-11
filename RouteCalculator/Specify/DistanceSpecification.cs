namespace RouteCalculator.Specify
{
    using RouteCalculator.Plan;

    /// <summary>
    /// This class is used to specify the minimum and maximum distance of a route
    /// </summary>
    public class DistanceSpecification : IRouteSpecification
    {
        /// <summary>
        /// The minimum total distance specified for a route
        /// </summary>
        private int minDistance;

        /// <summary>
        /// The maximum total distance specified for a route
        /// </summary>
        private int maxDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceSpecification"/> class.
        /// </summary>
        public DistanceSpecification()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceSpecification"/> class.
        /// </summary>
        /// <param name="minDistance">The min route distance.</param>
        /// <param name="maxDistance">The max route distance.</param>
        public DistanceSpecification(int minDistance, int maxDistance)
        {
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
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
            return route.Distance <= this.maxDistance && route.Distance >= this.minDistance;
        }

        #endregion
    }
}
