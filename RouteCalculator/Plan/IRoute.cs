namespace RouteCalculator.Plan
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single route from an origin to a destination
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// Gets or sets the legs that conform the route.
        /// </summary>
        /// <value>
        /// The legs that conform the route.
        /// </value>
        IEnumerable<Railroad> Legs
        {
            get;
            set;
        }
    }
}
