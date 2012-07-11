namespace RouteCalculator.Plan
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single route from an origin to a destination
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// Gets the legs that conform the route.
        /// </summary>
        /// <value>
        /// The legs that conform the route.
        /// </value>
        IEnumerable<Railroad> Legs
        {
            get;
        }

        /// <summary>
        /// Gets the total distance.
        /// </summary>
        int Distance
        {
            get;
        }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        City Origin
        {
            get;
        }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        City Destination
        {
            get;
        }
    }
}
