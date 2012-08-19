namespace RouteCalculator.Plan
{
    /// <summary>
    /// Starts a new shortest route comparison.
    /// </summary>
    public class ShortestRouteComparer : IRouteComparer
    {
        #region IRouteComparer Members

        /// <summary>
        /// Starts a new comparison for the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// The new comparison.
        /// </returns>
        public IRouteComparison Is(IRoute route)
        {
            return new ShortestRouteComparison(route);
        }

        #endregion
    }
}
