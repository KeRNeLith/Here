using System;

namespace Here
{
    /// <summary>
    /// Exception thrown when getting a null result where it's not authorized.
    /// </summary>
    public class NullResultException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NullResultException()
            : base("Result is null where it is forbidden.")
        {
        }
    }
}