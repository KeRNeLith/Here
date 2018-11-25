using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On success).
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Checks if the given result should be considered as a failure or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a failure, otherwise false.</returns>
        private static bool IsConsideredFailure(in IResult result, in bool treatWarningAsError)
        {
            return result.IsFailure || (treatWarningAsError && result.IsWarning);
        }

        /// <summary>
        /// Checks if the given result should be considered as a success or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a success, otherwise false.</returns>
        private static bool IsConsideredSuccess(in IResult result, in bool treatWarningAsError)
        {
            return !IsConsideredFailure(result, treatWarningAsError);
        }

        #region Result

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] in Action onSuccess, in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailResult();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] in Func<Result> onSuccess, in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailResult();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] in Func<T> onSuccess, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return new Result<T>(onSuccess(), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] in Func<Result<T>> onSuccess, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Use the <paramref name="errorObject"/> if needed to create a <see cref="CustomResult{TError}"/> in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="errorObject">Custom error object to use if this <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorObject);

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorFactory());

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorObject"/> if needed to create a Failure <see cref="Result{T, TError}"/> in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="errorObject">Custom error object to use if this <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result,
            [NotNull, InstantHandle] in Func<Result<T, TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<T, TError>(errorObject);

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result, 
            [NotNull, InstantHandle] in Func<Result<T, TError>> onSuccess, 
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<T, TError>(errorFactory());

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="defaultValue">Value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnSuccess<TOut>(this Result result,
            [NotNull, InstantHandle] in Func<Result, TOut> onSuccess,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result{T}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result<T> result,
            [NotNull, InstantHandle] in Action<T> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);
            else if (result.IsWarning)  // Warning as error
                return result.ToFailValueResult<T>();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="converter"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Input result value type.</typeparam>
        /// <typeparam name="TOut">Output result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="converter">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/>.</returns>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] in Func<TIn, TOut> converter,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<TOut>();

            return new Result<TOut>(converter(result.Value), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess<T>(this Result<T> result,
            [NotNull, InstantHandle] in Func<T, Result> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Input result value type.</typeparam>
        /// <typeparam name="TOut">Output result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] in Func<TIn, Result<TOut>> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<TOut>();

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorObject"/> if needed to create a <see cref="CustomResult{TError}"/> in cases 
        /// the <see cref="Result{T}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T> result,
            [NotNull, InstantHandle] in Func<T, CustomResult<TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorObject);

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result{T}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="T">Input result value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T> result,
            [NotNull, InstantHandle] in Func<T, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorFactory());

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorObject"/> if needed to create a <see cref="Result{TOut, TError}"/> in cases 
        /// the <see cref="Result{TIn}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn> result,
            [NotNull, InstantHandle] in Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<TOut, TError>(errorObject);

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// Calls the <paramref name="errorFactory"/> function if needed to create a custom error object in cases 
        /// the <see cref="Result{TIn}"/> is a failure or a warning treated as error.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Output result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn> result,
            [NotNull, InstantHandle] in Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<TOut, TError>(errorFactory());

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="defaultValue">Value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onSuccess,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailCustomResult(errorObject);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else if (result.IsWarning)  // Warning as error
                return result.ToFailCustomResult(errorFactory());

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<T> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning 
                    ? result.ToFailValueCustomResult<T>(errorObject) 
                    : result.ToFailValueCustomResult<T>();
            }

            return new Result<T, TError>(onSuccess(), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<T> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<T>(errorFactory())
                    : result.ToFailValueCustomResult<T>();
            }

            return new Result<T, TError>(onSuccess(), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<Result> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<Result<T>> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorObject) : result;
            }

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorFactory()) : result;
            }

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<Result<T, TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<T>(errorObject)
                    : result.ToFailValueCustomResult<T>();
            }

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<Result<T, TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<T>(errorFactory())
                    : result.ToFailValueCustomResult<T>();
            }

            return onSuccess();
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="defaultValue">Value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnSuccess<TOut, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onSuccess,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result{T, TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Action<T> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);
            else if (result.IsWarning)  // Warning as error
                return result.ToFailValueCustomResult<T>(errorObject);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result{T, TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Action<T> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);
            else if (result.IsWarning)  // Warning as error
                return result.ToFailValueCustomResult<T>(errorFactory());

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="converter"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="converter">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<TIn, TOut> converter, 
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                if (result.IsWarning)
                    return result.ToFailValueCustomResult<TOut>(errorObject);
                return result.ToFailValueCustomResult<TOut>();
            }

            return new Result<TOut, TError>(converter(result.Value), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="converter"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="converter">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<TIn, TOut> converter,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                if (result.IsWarning)
                    return result.ToFailValueCustomResult<TOut>(errorFactory());
                return result.ToFailValueCustomResult<TOut>();
            }

            return new Result<TOut, TError>(converter(result.Value), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T, TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Func<T, Result> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<TIn, Result<TOut>> onSuccess,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<TOut>();

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T, TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Func<T, CustomResult<TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorObject) : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result input value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{T, TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Func<T, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorFactory()) : result;
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning 
                    ? result.ToFailValueCustomResult<TOut>(errorObject) 
                    : result.ToFailValueCustomResult<TOut>();
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Result output value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{TOut, TError}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<TOut>(errorFactory())
                    : result.ToFailValueCustomResult<TOut>();
            }

            return onSuccess(result.Value);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="defaultValue">Value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        [PublicAPI]
        public static TOut OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onSuccess,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        #endregion
    }
}
