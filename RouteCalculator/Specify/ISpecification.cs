namespace RouteCalculator.Specify
{
    /// <summary>
    /// Represnts a specification that can be applied to any object
    /// </summary>
    public interface ISpecification
    {
        /// <summary>
        /// Determins wether this Specification applies to a given object
        /// </summary>
        /// <param name="appliedTo">The object to determine if this specification applies to.</param>
        /// <returns>true if this specification applies to <paramref name="appliedTo"/></returns>
        bool AppliesTo(object appliedTo);

        /// <summary>
        /// Validates the specified the object.
        /// </summary>
        /// <param name="validatedOn">The object to validate with this specification.</param>
        /// <returns>true if the object conforms to this specification</returns>
        bool Validate(object validatedOn);
    }
}
