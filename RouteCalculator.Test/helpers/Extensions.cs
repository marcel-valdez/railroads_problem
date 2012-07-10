namespace RouteCalculator.Test
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Represents a reflected method call
    /// </summary>
    /// <param name="arguments">The arguments to be passed to the method.</param>
    /// <returns>The result of calling the method</returns>
    public delegate object MethodCall(params object[] arguments);

    /// <summary>
    /// This class contains method extensions to be used by the test classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Private instance binding flags
        /// </summary>
        private const BindingFlags PRIVATE = BindingFlags.Instance | BindingFlags.NonPublic;

        /// <summary>
        /// Private static binding flags
        /// </summary>
        private const BindingFlags PRIVATE_STATIC = BindingFlags.NonPublic | BindingFlags.Static;

        /// <summary>
        /// Gets a private static method.
        /// </summary>
        /// <typeparam name="T">Type reflect</typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>A MethodCall delegate</returns>
        [SuppressMessage(
            category: "Microsoft.Design",
            checkId: "CA1004:GenericMethodsShouldProvideTypeParameter", 
            Justification = "The type parameter is necessary.")]
        public static MethodCall GetPrivateStaticMethod<T>(string methodName)
        {
            return (object[] parameters) => typeof(T).GetMethod(methodName, PRIVATE_STATIC).Invoke(null, parameters);
        }

        /// <summary>
        /// Gets a private method.
        /// </summary>
        /// <typeparam name="T">The type to reflect</typeparam>
        /// <param name="obj">The object to receive the invocation.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The value returned by the method</returns>
        public static MethodCall GetPrivateMethod<T>(this T obj, string methodName)
        {
            return (object[] parameters) => typeof(T).GetMethod(methodName, PRIVATE).Invoke(obj, parameters);
        }
    }
}
