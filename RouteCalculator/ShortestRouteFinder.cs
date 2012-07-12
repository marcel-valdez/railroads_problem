namespace RouteCalculator.Plan
{
    using System;
    using System.Collections.Generic;
    using RouteCalculator.Map;
    using RouteCalculator.Specify;

    /// <summary>
    /// This class is in charge of finding the shortest route that conforms to a specification.
    /// </summary>
    public class ShortestRouteFinder : IRouteFinder
    {
        /// <summary>
        /// The railroads map
        /// </summary>
        private RailroadMap map;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortestRouteFinder"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public ShortestRouteFinder(RailroadMap map)
        {
            this.map = map;
        }

        #region IRouteFinder Members

        /// <summary>
        /// Finds the routes that satisfy a specification
        /// </summary>
        /// <param name="specification">The specification to satisfy.</param>
        /// <returns>
        /// The routes that satisfy the specified attributes
        /// </returns>
        public IEnumerable<IRoute> FindRoutes(IRouteSpecification specification)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the first satisfying route to the specification.
        /// </summary>
        /// <param name="specification">The specification to satisfy.</param>
        /// <returns>
        /// The first route to satisfy the previously specified attributes
        /// </returns>
        public IRoute FindFirstSatisfyingRoute(IRouteSpecification specification)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
