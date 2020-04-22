using System;
using System.Diagnostics;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Throw and assert helpers.
    /// </summary>
    internal static class Throw
    {
        /// <summary>
        /// Throws a <see cref="NullResultException"/> if the given result is null.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <returns>The input value if not null.</returns>
        [DebuggerStepThrough]
        [ContractAnnotation("result:null => halt")]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static T IfResultNull<T>([CanBeNull] T result)
        {
            if (result == null)
                throw new NullResultException();
            return result;
        }
    }
}