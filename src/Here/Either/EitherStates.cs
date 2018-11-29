namespace Here
{
    /// <summary>
    /// <see cref="Either{TLeft,TRight}"/> possible states.
    /// </summary>
    internal enum EitherStates : byte
    {
        /// <summary>
        /// None state (when an operation with predicate returns false,
        /// put an <see cref="Either{TLeft,TRight}"/> in a state that is neither left nor right.
        /// </summary>
        None = 0,

        /// <summary>
        /// Left state (usually for failure state).
        /// </summary>
        Left = 1,

        /// <summary>
        /// Right state (usually for success state).
        /// </summary>
        Right = 2
    }
}