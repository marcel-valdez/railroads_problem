namespace RouteCalculator.Plan
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// This interface represents the base comparer for routes.
    /// </summary>
    public interface IRouteComparer
    {
        /// <summary>
        /// Starts a new comparison for the route.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns>The new comparison.</returns>
        IRouteComparison Is(IRoute route);
    }
}
