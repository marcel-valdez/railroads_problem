namespace RouteCalculator.Plan
{
    /// <summary>
    /// It executes a comparison of a route, using the Route Distance as criteria.
    /// </summary>
    public class ShortestRouteComparison : IRouteComparison
    {
        /// <summary>
        /// The route being compared.
        /// </summary>
        private IRoute firstRoute;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortestRouteComparison"/> class.
        /// </summary>
        /// <param name="firstRoute">The route A.</param>
        public ShortestRouteComparison(IRoute firstRoute)
        {
            this.firstRoute = firstRoute;
        }
        #region IRouteComparison Members

        /// <summary>
        /// Determines if a given route is betters the than another.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// true if the first route is better than the one received, false otherwise.
        /// </returns>
        public bool BetterThan(IRoute route)
        {
            if (this.firstRoute is Worst)
            {
                return false;
            }

            if (route is Worst)
            {
                return true;
            }

            return this.firstRoute.Distance < route.Distance;
        }

        /// <summary>
        /// Determins if a given route is worse than another.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>
        /// true if the given route is worse than the one received.
        /// </returns>
        public bool WorseThan(IRoute route)
        {
            if (this.firstRoute is Worst)
            {
                return true;
            }

            if (route is Worst)
            {
                return false;
            }

            return this.firstRoute.Distance > route.Distance;
        }
        #endregion
    }
}
