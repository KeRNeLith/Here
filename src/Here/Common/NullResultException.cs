using System;
#if SUPPORTS_BINARY_EXCEPTION_SERIALIZATION
using System.Runtime.Serialization;
#endif

namespace Here
{
    /// <summary>
    /// Exception thrown when getting a null result where it's not authorized.
    /// </summary>
#if SUPPORTS_BINARY_EXCEPTION_SERIALIZATION
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

#if SUPPORTS_BINARY_EXCEPTION_SERIALIZATION
        /// <summary>
        /// Serialization constructor.
        /// </summary>
        private NullResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}