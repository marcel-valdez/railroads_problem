namespace RouteCalculator.Specify
{
    using RouteCalculator.Plan;

    /// <summary>
    /// Represents a specification that can be applied to a route
    /// </summary>
    public interface IRouteSpecification
    {
        /// <summary>
        /// Validates the specified the object.
        /// </summary>
        /// <param name="validatedOn">The object to validate with this specification.</param>
        /// <returns>true if the object conforms to this specification</returns>
        bool Validate(IRoute validatedOn);
    }
}
