#if !SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
using JetBrains.Annotations;
#if SUPPORTS_SYSTEM_CORE
using System.Linq;
#else
using Here.Utils;
#endif
#endif

namespace Here
{
    /// <summary>
    /// Helpers to be used inside the Here library.
    /// </summary>
    internal static class HereHelpers
    {
        /// <summary>
        /// Identity function.
        /// </summary>
        public static T Identity<T>(T param) => param;

        /// <summary>
        /// Do nothing action.
        /// </summary>
        public static void DoNothing()
        {
            // Do nothing because it's an helper to pass as functor that should do nothing.
        }

        /// <summary>
        /// Do nothing action.
        /// </summary>
        public static void DoNothing<T>(T param)
        {
            // Do nothing because it's an helper to pass as functor that should do nothing.
        }

#if !SUPPORTS_NULL_EMPTY_OR_WHITE_SPACE
        /// <summary>
        /// Indicates if the given string is null or is only composed of spaces.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>True if the string is null or only composed of spaces.</returns>
        [ContractAnnotation("str:null => true")]
        internal static bool IsNullOrWhiteSpace([CanBeNull] string str)
        {
            return str is null || str.All(char.IsWhiteSpace);
        }
#endif
    }
}