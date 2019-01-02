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
        /// Throws an <see cref="ArgumentNullException"/> if the given value is null.
        /// </summary>
        /// <param name="argument">Argument to check.</param>
        /// <param name="argumentName">Name of the object to check nullity.</param>
        /// <param name="message">Exception message.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("argument:null => halt")]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void IfArgumentNull([CanBeNull] object argument, [NotNull, InvokerParameterName] string argumentName, [CanBeNull] string message = null)
        {
            if (argument is null)
                throw new ArgumentNullException(argumentName, message);
        }
    }
}