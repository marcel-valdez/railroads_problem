namespace RouteCalculator.Specify
{
    using RouteCalculator.Plan;

    /// <summary>
    /// A specification to connect two or more specification with the logical AND.
    /// If a specification in the And connector does not apply to a given object, it is ignored (won't fail).
    /// </summary>
    public class AndSpecification : IRouteSpecification
    {
        /// <summary>
        /// The specifications that will be AND connected.
        /// </summary>
        private IRouteSpecification[] specifications;

        /// <summary>
        /// Initializes a new instance of the <see cref="AndSpecification"/> class.
        /// </summary>
        /// <param name="specifications">The specifications to connect with AND operator.</param>
        public AndSpecification(params IRouteSpecification[] specifications)
        {
            this.specifications = specifications;
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
            foreach (IRouteSpecification specification in this.specifications)
            {
                if (!specification.Validate(validatedOn))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
