#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Internal helpers to work with results.
    /// </summary>
    internal static class ResultInternalHelpers
    {
        /// <summary>
        /// Checks if the given result should be considered as a failure or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a failure, otherwise false.</returns>
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsConsideredFailure([NotNull] in IResult result, in bool treatWarningAsError)
        {
            return result.IsFailure || (treatWarningAsError && result.IsWarning);
        }

        /// <summary>
        /// Checks if the given result should be considered as a success or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a success, otherwise false.</returns>
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static bool IsConsideredSuccess([NotNull] in IResult result, in bool treatWarningAsError)
        {
            return !IsConsideredFailure(result, treatWarningAsError);
        }
    }
}