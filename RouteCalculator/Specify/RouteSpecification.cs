namespace RouteCalculator.Specify
{
    using RouteCalculator.Plan;

    /// <summary>
    /// A specification that applies to a Route
    /// </summary>
    public class OriginAndEndSpecification : ISpecification
    {
        /// <summary>
        /// The origin city name
        /// </summary>
        private string originName;

        /// <summary>
        /// The destination city name
        /// </summary>
        private string destinationName;

        /// <summary>
        /// Initializes a new instance of the <see cref="OriginAndEndSpecification"/> class.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="destination">The destination.</param>
        public OriginAndEndSpecification(string origin, string destination)
        {
            this.originName = origin;
            this.destinationName = destination;
        }
        #region ISpecification Members

        /// <summary>
        /// Determins wether this Specification applies to a given object
        /// </summary>
        /// <param name="appliedTo">The object to determine if this specification applies to.</param>
        /// <returns>
        /// true if this specification applies to <paramref name="appliedTo"/>
        /// </returns>
        public bool AppliesTo(object appliedTo)
        {
            return typeof(IRoute).IsInstanceOfType(appliedTo);
        }

        /// <summary>
        /// Validates the specified the object.
        /// </summary>
        /// <param name="validatedOn">The object to validate with this specification.</param>
        /// <returns>
        /// true if the object conforms to this specification
        /// </returns>
        public bool Validate(object validatedOn)
        {
            IRoute route = (IRoute)validatedOn;
            return route.Destination.Name == this.destinationName && route.Origin.Name == this.originName;
        }

        #endregion
    }
}
