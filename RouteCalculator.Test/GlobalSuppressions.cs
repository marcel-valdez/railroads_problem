﻿using System.Diagnostics.CodeAnalysis;
[module: SuppressMessage(
    category: "Microsoft.Performance",
    checkId: "CA1822:MarkMembersAsStatic",
    Justification = "Most methods inside test classes are called by the NUnit framework indirectly.",
    Scope = "Member",
    Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItCanBuildASingleRailroad()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItCanReadFileContent()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItCanBuildRailroads(System.String,System.String[],System.Int32[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItCanCreateCity()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItDoesNotBreakWithAValidFile()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItCanReadFileContent()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "RouteCalculator.Test.RailroadMapTest.#TestIfItDoesNotBreakWithAValidFile()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Specify.PathSpecificationTest.#routesTestDataConfiguration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.PathSpecificationTest.#TestIfItAppliesToARoute()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.PathSpecificationTest.#TestIfItDoesNotApplyToAnObject()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.PathSpecificationTest.#TestIfTripSpecificationCanSpecifyACitiesRoute(System.String[],System.String[],System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Specify.AndSpecificationTest.#specificationData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.AndSpecificationTest.#TestIfItConnectsTwoSpecificationsCorrectly(System.Boolean,System.Boolean,System.Boolean,System.Boolean,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Specify.OriginAndEndSpecificationTest.#routesTestDataConfiguration")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.OriginAndEndSpecificationTest.#TestIfItAppliesToARoute()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.OriginAndEndSpecificationTest.#TestIfItCanSpecifyOriginAndDestinationCorrectly(System.String,System.String,System.String,System.String,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.OriginAndEndSpecificationTest.#TestIfItDoesNotApplyToAnObject()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Specify.StopsCountSpecificationTest.#testData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.StopsCountSpecificationTest.#TestIfStopsCountSpecificationCanValidateCorrectly(System.Int32,System.Int32,System.Int32,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Specify.DistanceSpecificationTest.#distanceRouteData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.DistanceSpecificationTest.#TestIfItCanSpecifyCorrectlyTheRequiredDistance(System.Int32,System.Int32,System.Int32,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Specify.AndSpecificationTest.#TestIfItConnectsTwoSpecificationsCorrectly(System.Boolean,System.Boolean,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Plan.RouteTest.#cityData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "RouteCalculator.Test.Plan.RouteTest.#distanceData")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Plan.RouteTest.#TestIfItCanCalculateDistanceCorrectly(System.Int32[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "RouteCalculator.Test.Plan.RouteTest.#TestIfItKnowItsOriginAndDestination(System.String[])")]
