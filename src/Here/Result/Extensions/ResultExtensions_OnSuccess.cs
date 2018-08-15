using System;
using JetBrains.Annotations;

namespace Here.Results.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On success).
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Check if the given result should be considered as a failure or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a failure.</returns>
        private static bool IsConsideredFailure(IResult result, bool treatWarningAsError)
        {
            return result.IsFailure || (treatWarningAsError && result.IsWarning);
        }

        /// <summary>
        /// Check if the given result should be considered as a success or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a success.</returns>
        private static bool IsConsideredSuccess(IResult result, bool treatWarningAsError)
        {
            return !IsConsideredFailure(result, treatWarningAsError);
        }

        #region Result

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Action onSuccess, bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailResult();

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> computed with <paramref name="onSuccess"/>, or a failure corresponding to this one.</returns>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Func<Result> onSuccess, bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailResult();

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] Func<T> onSuccess, bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return new Result<T>(onSuccess(), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] Func<Result<T>> onSuccess, bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Use the <paramref name="errorObject"/> if needed to create a <see cref="CustomResult{TError}"/> in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object to use if this <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] Func<CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorObject);

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Call the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] Func<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorFactory());

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Use the <paramref name="errorObject"/> if needed to create a Failure <see cref="Result{T, TError}"/> in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object to use if this <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result,
            [NotNull, InstantHandle] Func<Result<T, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomValueResult<T, TError>(errorObject);

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result.IsSuccess"/>.
        /// Call the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is <see cref="Result.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result, 
            [NotNull, InstantHandle] Func<Result<T, TError>> onSuccess, 
            [NotNull, InstantHandle] Func<TError> errorFactory, 
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomValueResult<T, TError>(errorFactory());

            return onSuccess();
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="Result{T}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result{T}"/> is <see cref="Result{T}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result<T> result,
            [NotNull, InstantHandle] Action<T> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);
            else if (result.IsWarning)  // Warning as error
                return result.ToFailValueResult<T>();

            return result;
        }

        /// <summary>
        /// Call the <paramref name="converter"/> function when the <paramref name="result"/> is <see cref="Result{TIn}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Input result value type.</typeparam>
        /// <typeparam name="TOut">Output result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="converter">Function to run if the <see cref="Result{TIn}"/> is <see cref="Result{TIn}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, TOut> converter,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<TOut>();

            return new Result<TOut>(converter(result.Value), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Input result value type.</typeparam>
        /// <typeparam name="TOut">Output result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is <see cref="Result{TIn}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess<T>(this Result<T> result,
            [NotNull, InstantHandle] Func<T, Result> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Input result value type.</typeparam>
        /// <typeparam name="TOut">Output result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is <see cref="Result{TIn}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut>> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<TOut>();

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{T}.IsSuccess"/>.
        /// Use the <paramref name="errorObject"/> if needed to create a <see cref="CustomResult{TError}"/> in cases 
        /// the <see cref="Result{T}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="Result{T}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorObject);

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{T}.IsSuccess"/>.
        /// Call the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result{T}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T}"/> is <see cref="Result{T}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorFactory());

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn}.IsSuccess"/>.
        /// Use the <paramref name="errorObject"/> if needed to create a <see cref="Result{TOut, TError}"/> in cases 
        /// the <see cref="Result{TIn}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is <see cref="Result{TIn}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomValueResult<TOut, TError>(errorObject);

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn}.IsSuccess"/>.
        /// Call the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result{TIn}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is <see cref="Result{TIn}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomValueResult<TOut, TError>(errorFactory());

            return onSuccess(result.Value);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailCustomResult(errorObject);

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailCustomResult(errorFactory());

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<T> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning 
                    ? result.ToFailCustomValueResult<T>(errorObject) 
                    : result.ToFailCustomValueResult<T>();
            }

            return new Result<T, TError>(onSuccess(), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<T> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailCustomValueResult<T>(errorFactory())
                    : result.ToFailCustomValueResult<T>();
            }

            return new Result<T, TError>(onSuccess(), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<Result> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<Result<T>> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorObject) : result;
            }

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorFactory()) : result;
            }

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<Result<T, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailCustomValueResult<T>(errorObject)
                    : result.ToFailCustomValueResult<T>();
            }

            return onSuccess();
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<Result<T, TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailCustomValueResult<T>(errorFactory())
                    : result.ToFailCustomValueResult<T>();
            }

            return onSuccess();
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="Result{T, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action<T> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);
            else if (result.IsWarning)  // Warning as error
                return result.ToFailCustomValueResult<T>(errorObject);

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> action when the <paramref name="result"/> is <see cref="Result{T, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action<T> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);
            else if (result.IsWarning)  // Warning as error
                return result.ToFailCustomValueResult<T>(errorFactory());

            return result;
        }

        /// <summary>
        /// Call the <paramref name="converter"/> function when the <paramref name="result"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="converter">Function to run if the <see cref="Result{TIn, TError}"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, TOut> converter, 
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                if (result.IsWarning)
                    return result.ToFailCustomValueResult<TOut>(errorObject);
                return result.ToFailCustomValueResult<TOut>();
            }

            return new Result<TOut, TError>(converter(result.Value), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="converter"/> function when the <paramref name="result"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="converter">Function to run if the <see cref="Result{TIn, TError}"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, TOut> converter,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                if (result.IsWarning)
                    return result.ToFailCustomValueResult<TOut>(errorFactory());
                return result.ToFailCustomValueResult<TOut>();
            }

            return new Result<TOut, TError>(converter(result.Value), result._logic);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{T, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<T, Result> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut>> onSuccess,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<TOut>();

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{T, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorObject) : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{T, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorFactory()) : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning 
                    ? result.ToFailCustomValueResult<TOut>(errorObject) 
                    : result.ToFailCustomValueResult<TOut>();
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Call the <paramref name="onSuccess"/> function when the <paramref name="result"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is <see cref="Result{TIn, TError}.IsSuccess"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailCustomValueResult<TOut>(errorFactory())
                    : result.ToFailCustomValueResult<TOut>();
            }

            return onSuccess(result.Value);
        }

        #endregion
    }
}
