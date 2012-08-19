namespace RouteCalculator.Plan
{
    using System;
    using System.Collections.Generic;
    using RouteCalculator.Map;

    /// <summary>
    /// Represents the worst possible route in every way.
    /// </summary>
    public sealed class Worst : IRoute
    {
        /// <summary>
        /// The value of the worst possible Route.
        /// </summary>
        private static readonly Worst WorstRoute = new Worst();

        /// <summary>
        /// Prevents a default instance of the <see cref="Worst"/> class from being created.
        /// </summary>
        private Worst()
        {
        }

        /// <summary>
        /// Gets the value of the worst possible Route.
        /// </summary>
        public static Worst Route
        {
            get
            {
                return WorstRoute;
            }
        }

        #region IRoute Members

        /// <summary>
        /// Gets the legs that conform the route.
        /// </summary>
        /// <value>
        /// The legs that conform the route.
        /// </value>
        public IEnumerable<IRailroad> Legs
        {
            get
            {
                return default(IEnumerable<IRailroad>);
            }
        }

        /// <summary>
        /// Gets the total distance.
        /// </summary>
        public int Distance
        {
            get
            {
                return int.MaxValue >> 1;
            }
        }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        public ICity Origin
        {
            get
            {
                return default(ICity);
            }
        }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        public ICity Destination
        {
            get
            {
                return default(ICity);
            }
        }

        /// <summary>
        /// Adds the railroad leg stop.
        /// NOTE: Legs can repeat, as long as they are not continuous.
        /// </summary>
        /// <param name="railroad">The railroad to add.</param>
        public void AddLeg(IRailroad railroad)
        {
        }

        /// <summary>
        /// Creates a flyweight copy of this instance.
        /// </summary>
        /// <returns>
        /// A fly weight copy
        /// </returns>
        public IRoute FlyweightCopy()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
