using System;
using JetBrains.Annotations;
#if SUPPORTS_SERIALIZATION
using System.Runtime.Serialization;
#endif

namespace Here
{
    /// <summary>
    /// Exception thrown when getting a null result where it's not authorized.
    /// </summary>
#if SUPPORTS_SERIALIZATION
    [Serializable]
#endif
    public sealed class NullResultException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NullResultException()
            : base("Result is null where it is forbidden.")
        {
        }

#if SUPPORTS_SERIALIZATION
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        private NullResultException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}