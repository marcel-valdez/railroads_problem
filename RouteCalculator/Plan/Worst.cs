namespace RouteCalculator.Plan
{
    /// <summary>
    /// Represents the worst possible route in every way.
    /// </summary>
    public sealed class Worst : Route
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
    }
}
