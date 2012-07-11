namespace RouteCalculator.Specify
{
    /// <summary>
    /// A specification to connect two or more specification with the logical AND.
    /// If a specification in the And connector does not apply to a given object, it is ignored (won't fail).
    /// </summary>
    public class AndSpecification : ISpecification
    {
        /// <summary>
        /// The specifications that will be AND connected.
        /// </summary>
        private ISpecification[] specifications;

        /// <summary>
        /// Initializes a new instance of the <see cref="AndSpecification"/> class.
        /// </summary>
        /// <param name="specifications">The specifications to connect with AND operator.</param>
        public AndSpecification(params ISpecification[] specifications)
        {
            this.specifications = specifications;
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
            foreach (ISpecification specification in this.specifications)
            {
                if (specification.AppliesTo(appliedTo))
                {
                    return true;
                }
            }

            return false;
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
            foreach (ISpecification specification in this.specifications)
            {
                if (specification.AppliesTo(validatedOn) && !specification.Validate(validatedOn))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
