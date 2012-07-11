namespace RouteCalculator.Specify
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RouteCalculator.Plan;

    /// <summary>
    /// A trip specification that delimits the possible routes to a destination from an origin.
    /// </summary>
    public class PathSpecification : ISpecification
    {
        /// <summary>
        /// The city route to specify
        /// </summary>
        private string[] citiesRoute = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSpecification"/> class.
        /// </summary>
        /// <param name="cityNames">The city names.</param>
        public PathSpecification(params string[] cityNames)
        {
            this.citiesRoute = cityNames;
        }

        #region ISpecification Members
        /// <summary>
        /// Determins wether this Specification applies to a given object
        /// </summary>
        /// <param name="appliedTo">The object to determine if this specification applies to.</param>
        /// <returns>
        /// true if this specification applies to <paramref name="theObject"/>
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
            IEnumerable<Railroad> legs = route.Legs;

            if (legs.Count() + 1 != this.citiesRoute.Length)
            {
                return false;
            }

            for (int i = 0; i < legs.Count(); i++)
            {
                if (legs.ElementAt(i).Origin.Name != this.citiesRoute[i] ||
                   legs.ElementAt(i).Destination.Name != this.citiesRoute[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
