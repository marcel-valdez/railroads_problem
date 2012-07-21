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
        private readonly IRailroadMap map;

        /// <summary>
        /// The numbers of railroads in the map
        /// </summary>
        private readonly int railroadCount;

        /// <summary>
        /// The criteria used to compare the routes
        /// </summary>
        private readonly IRouteComparer criteria;

        /// <summary>
        /// It is a hash used to memoize calculated routes.
        /// </summary>
        private readonly IDictionary<string, IRoute> evaluatedRoutes = new Dictionary<string, IRoute>();

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
            this.evaluatedRoutes.Clear();
            foreach (ICity city in this.map.Cities.Where(city => city.Outgoing != null && city.Outgoing.Count > 0))
            {
                foreach (IRailroad railroad in city.Outgoing)
                {
                    IRoute root = new Route();
                    root.AddLeg(railroad);
                    IRoute result = this.FindMinRoute(root, bestKnownRoute, specification);
                    if (result != default(IRoute)
                        && this.criteria.Is(result).BetterThan(bestKnownRoute))
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
            IRoute result = bestAtTheMoment;
            string rootLegKey = GetLegKey(route.Legs.Last(), specification);

            if (this.evaluatedRoutes.ContainsKey(rootLegKey))
            {
                result = route.Append(this.evaluatedRoutes[rootLegKey]);
            } 
            else if (route.Legs.Count() > this.railroadCount * 10)
            {
                // Limit the number of cycles it can do (at most allows to cycle 10 times the railroads)
                result = Worst.Route;
            } 
            else if (!specification.MightBeSatisfiedBy(route))
            {
                result = Worst.Route;
            }
            else if (this.criteria.Is(bestAtTheMoment)
                             .BetterThan(route))
            {
                // No point in trying to go deeper, if going deeper will only worsen the evaluation.
                result = Worst.Route;
            }
            else if (specification.IsSatisfiedBy(route))
            {
                // We dig no deeper, because we assume that if we keep on going deeper,
                // then the IRoute can only get worse (bigger)
                result = route;
            }
            else
            {
                foreach (IRailroad railroad in route.Destination.Outgoing)
                {
                    IRoute prospect = route.FlyweightCopy();
                    prospect.AddLeg(railroad);
                    prospect = this.FindMinRoute(prospect, bestAtTheMoment, specification);

                    if (prospect != Worst.Route)
                    {
                        MemoizeProspect(route, prospect, GetLegKey(prospect.Legs.Last(), specification));
                    }

                    if (prospect != default(IRoute)
                        && this.criteria.Is(bestAtTheMoment).WorseThan(prospect))
                    {
                        bestAtTheMoment = prospect;
                    }
                }

                result = bestAtTheMoment;
            }

            return result;
        }

        /// <summary>
        /// Memoizes the prospect.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="prospect">The prospect.</param>
        /// <param name="legKey">The leg key.</param>
        private void MemoizeProspect(IRoute rootRoute, IRoute prospect, string legKey)
        {
            if (!this.evaluatedRoutes.ContainsKey(legKey))
            {
                IRoute toStore = GetSubroute(prospect, rootRoute);
                this.evaluatedRoutes.Add(legKey, toStore);                
            }
        }

        /// <summary>
        /// Gets the subroute.
        /// </summary>
        /// <param name="fullRoute">The full route.</param>
        /// <param name="rootRoute">The root route.</param>
        /// <returns>The subroute corresponding to the substraction of fullRoute minus rootRoute</returns>
        public static IRoute GetSubroute(IRoute fullRoute, IRoute rootRoute)
        {
            int fullRouteLegCount = fullRoute.Legs.Count();

            if (rootRoute == default(IRoute) || rootRoute.Legs.Count() == 0)
            {
                return fullRoute.FlyweightCopy();
            }

            int rootRouteLegCount = rootRoute.Legs.Count();
            if (fullRouteLegCount.Equals(rootRouteLegCount))
            {
                return default(IRoute);
            }

            int remainingLegCount = fullRouteLegCount - rootRouteLegCount;
            int startLegIndex = rootRouteLegCount;

            return fullRoute.GetSubroute(startLegIndex, remainingLegCount);
        }

        /// <summary>
        /// Gets the leg key.
        /// </summary>
        /// <param name="leg">The leg to process.</param>
        /// <param name="specification">The specification.</param>
        /// <returns>The key for memoization</returns>
        private static string GetLegKey(IRailroad leg, IRouteSpecification specification)
        {
            return leg.Origin.Name + leg.Destination.Name + leg.Length + specification.GetHashCode();
        }
    }
}
