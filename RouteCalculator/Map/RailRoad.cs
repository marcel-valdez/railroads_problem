namespace RouteCalculator.Map
{
    /// <summary>
    /// This class will represent the railroad connecting two cities.
    /// </summary>
    public class Railroad : IRailroad
    {
        /// <summary>
        /// Gets or sets the length of the railroad.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public virtual int Length
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the City origin of the Railroad.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public ICity Origin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the City destination of the railroad.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public ICity Destination
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            IRailroad railroad = obj as IRailroad;
            if (railroad != null)
            {
                return CompareRailroadCities(this.Origin, railroad.Origin) &&
                       CompareRailroadCities(this.Destination, railroad.Destination) &&
                       this.Length == railroad.Length;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            if (this.Origin == null || this.Destination == null)
            {
                return base.GetHashCode();
            }

            return (this.Origin.Name + this.Destination.Name + this.Length).GetHashCode();
        }

        /// <summary>
        /// Compares the railroad cities.
        /// </summary>
        /// <param name="city">The city to compare.</param>
        /// <param name="otherCity">The other city.</param>
        /// <returns>
        /// true if they are equivalent, false otherwise.
        /// </returns>
        private static bool CompareRailroadCities(ICity city, ICity otherCity)
        {
            if (city == null || (city != null && (otherCity == null || city.Name != otherCity.Name)))
            {
                return false;
            }



            return true;
        }
    }
}
