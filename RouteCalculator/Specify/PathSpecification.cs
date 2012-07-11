namespace RouteCalculator.Specify
{
    using System.Collections.Generic;
    using System.Linq;
    using RouteCalculator.Map;
    using RouteCalculator.Plan;

    /// <summary>
    /// A trip specification that delimits the possible routes to a destination from an origin.
    /// </summary>
    public class PathSpecification : IRouteSpecification
    {
        /// <summary>
        /// The city route to specify
        /// </summary>
        private string[] citiesRoute;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSpecification"/> class.
        /// </summary>
        public PathSpecification()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSpecification"/> class.
        /// </summary>
        /// <param name="cityNames">The city names.</param>
        public PathSpecification(params string[] cityNames)
        {
            this.citiesRoute = cityNames;
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
