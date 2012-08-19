namespace RouteCalculator.Plan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RouteCalculator.Map;
    using RouteCalculator.Specify;

    /// <summary>
    /// This class is in charge of finding the shortest route that conforms to a specification.
    /// NOTE: Its algorithm assumes that more depth to the search will only make the Route worse.
    /// </summary>
    public class ShortestRouteFinder : IRouteFinder
    {
        /// <summary>
        /// The railroads map
        /// </summary>
        private IRailroadMap map;

        /// <summary>
        /// The numbers of railroads in the map
        /// </summary>
        private int railroadCount;

        /// <summary>
        /// The criteria used to compare the routes
        /// </summary>
        private IRouteComparer criteria;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortestRouteFinder"/> class.
        /// </summary>
        /// <param name="map">The railroad map.</param>
        /// <param name="routeComparer">The route comparer.</param>
        public ShortestRouteFinder(IRailroadMap map, IRouteComparer routeComparer)
        {
            this.map = map;
            this.railroadCount = map.Railroads.Count();
            this.criteria = routeComparer;
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
            throw new NotImplementedException(this.map.ToString());
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
            IRoute bestKnownRoute = Worst.Route;
            foreach (ICity city in this.map.Cities.Where(city => city.Outgoing != null && city.Outgoing.Count > 0))
            {
                foreach (IRailroad railroad in city.Outgoing)
                {
                    IRoute root = new Route();
                    root.AddLeg(railroad);
                    IRoute result = this.FindMinRoute(root, bestKnownRoute, specification);
                    if (!(result is Worst))
                    {
                        bestKnownRoute = result;
                    }
                }
            }

            return bestKnownRoute is Worst ? default(IRoute) : bestKnownRoute;
        }

        #endregion

        /// <summary>
        /// Finds the best route.
        /// It assumes that if the algorithm goes deeper, it can only get worse, not better.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="bestAtTheMoment">The best route at the moment.</param>
        /// <param name="specification">The specification.</param>
        /// <returns>
        /// The best route (if any)
        /// </returns>
        private IRoute FindMinRoute(IRoute route, IRoute bestAtTheMoment, IRouteSpecification specification)
        {
            if (route.Legs.Count() > this.railroadCount * 10)
            {
                // Limit the number of cycles it can do (at most allows to cycle 10 times the railroads)
                return Worst.Route;
            }

            if (!specification.MightBeSatisfiedBy(route))
            {
                return Worst.Route;
            }

            if (this.criteria.Is(bestAtTheMoment)
                             .BetterThan(route))
            {
                // No point in trying to go deeper, if going deeper will only worsen the evaluation.
                return Worst.Route;
            }

            if (specification.IsSatisfiedBy(route))
            {
                // We dig no deeper, because we assume that if we keep on going deeper,
                // then the IRoute can only we worse (bigger)
                return route;
            }

            foreach (IRailroad railroad in route.Destination.Outgoing)
            {
                IRoute prospect = route.FlyweightCopy();
                prospect.AddLeg(railroad);
                prospect = this.FindMinRoute(prospect, bestAtTheMoment, specification);

                if (prospect != null && this.criteria.Is(bestAtTheMoment)
                                                     .WorseThan(prospect))
                {
                    bestAtTheMoment = prospect;
                }
            }

            return bestAtTheMoment;
        }
    }
}
