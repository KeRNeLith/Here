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
        /// Throws an <see cref="ArgumentNullException"/> if the given argument is null.
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

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the given condition is invalid.
        /// </summary>
        /// <param name="isInvalid">Result of the condition to assert.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <param name="message">Exception message.</param>
        [DebuggerStepThrough]
        [ContractAnnotation("isInvalid:true => halt")]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void IfArgument(bool isInvalid, [NotNull, InvokerParameterName] string argumentName, [CanBeNull] string message = null)
        {
            if (isInvalid)
                throw new ArgumentException(message, argumentName);
        }

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