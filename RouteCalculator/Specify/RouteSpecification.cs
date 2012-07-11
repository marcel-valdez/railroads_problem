namespace RouteCalculator.Specify
{
    /// <summary>
    /// A specification that applies to a Route
    /// </summary>
    public class RouteSpecification : ISpecification
    {
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
