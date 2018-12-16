namespace Here
{
    /// <summary>
    /// Constants for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/>
    /// or <see cref="Result{T,TError}"/> used through the library.
    /// </summary>
    internal static class ResultConstants
    {
        /// <summary>
        /// Value of a failed conversion from a <see cref="Result{T}"/> to an
        /// <see cref="Either{String,TRight}"/>, while <see cref="Result{T}"/> had a null value.
        /// </summary>
        public const string ValueResultToEitherNullValue = "Result<T> had a null value.";

        /// <summary>
        /// Value of a failed conversion from a <see cref="Result{T, TError}"/> to an
        /// <see cref="Either{String,TRight}"/>, while <see cref="Result{T, TError}"/> had a null value.
        /// </summary>
        public const string ValueCustomResultToEitherNullValue = "Result<T, TError> had a null value.";

        /// <summary>
        /// <see cref="Either{TLeft,TRight}"/> is in a <see cref="EitherStates.Left"/> state while trying
        /// to convert it to a <see cref="Result"/>, so it produces a failed <see cref="Result"/>.
        /// </summary>
        public const string EitherToFailedResult = "Either<TLeft, TRight> was in Left state.";
    }
}