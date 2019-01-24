using System;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
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
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool IsConsideredFailure([NotNull] IResult result, bool treatWarningAsError)
        {
            return result.IsFailure || (treatWarningAsError && result.IsWarning);
        }

        /// <summary>
        /// Checks if the given result should be considered as a success or not.
        /// </summary>
        /// <param name="result">Result to check.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>True if the result is considered as a success, otherwise false.</returns>
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool IsConsideredSuccess([NotNull] IResult result, bool treatWarningAsError)
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
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Action onSuccess, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Action<Result> onSuccess, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccess(this Result result, [NotNull, InstantHandle] Func<Result, Result> onSuccess, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            if (result.IsWarning)  // Warning as error
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
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] Func<Result, T> onSuccess, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return new Result<T>(onSuccess(result), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result result, [NotNull, InstantHandle] Func<Result, Result<T>> onSuccess, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] Func<Result, CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorObject);

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this Result result,
            [NotNull, InstantHandle] Func<Result, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<Result, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorFactory(result));

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result,
            [NotNull, InstantHandle] Func<Result, Result<T, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<T, TError>(errorObject);

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result result, 
            [NotNull, InstantHandle] Func<Result, Result<T, TError>> onSuccess, 
            [NotNull, InstantHandle] Func<Result, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<T, TError>(errorFactory(result));

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TOut>(this Result result,
            [NotNull, InstantHandle] Func<Result, TOut> onSuccess,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TOut>(this Result result,
            [NotNull, InstantHandle] Func<Result, TOut> onSuccess,
            [NotNull, InstantHandle] Func<Result, TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return valueFactory(result);
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
        /// <returns>This <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccess<T>(this Result<T> result,
            [NotNull, InstantHandle] Action<T> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);

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
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, TOut> converter,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(converter, nameof(converter));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccess<T>(this Result<T> result,
            [NotNull, InstantHandle] Func<T, Result> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut>> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<Result<T>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailCustomResult(errorFactory(result));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull, InstantHandle] Func<Result<TIn>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueCustomResult<TOut, TError>(errorFactory(result));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onSuccess,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onSuccess,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return valueFactory(result);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Action to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result);

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, T> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning 
                    ? result.ToFailValueCustomResult<T>(errorObject) 
                    : result.ToFailValueCustomResult<T>();
            }

            return new Result<T, TError>(onSuccess(result), result.Logic);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, T> onSuccess,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<T>(errorFactory(result))
                    : result.ToFailValueCustomResult<T>();
            }

            return new Result<T, TError>(onSuccess(result), result.Logic);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onSuccess"/>, otherwise a failure.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Result> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailResult() : result;
            }

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Result<T>> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredFailure(result, treatWarningAsError))
                return result.ToFailValueResult<T>();

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorObject) : result;
            }

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorFactory(result)) : result;
            }

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Result<T, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<T>(errorObject)
                    : result.ToFailValueCustomResult<T>();
            }

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Result<T, TError>> onSuccess,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<T>(errorFactory(result))
                    : result.ToFailValueCustomResult<T>();
            }

            return onSuccess(result);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TOut, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onSuccess,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TOut, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onSuccess,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return valueFactory(result);
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
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action<T> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result.Value);

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
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, TOut> converter, 
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(converter, nameof(converter));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, TOut> converter,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(converter, nameof(converter));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                if (result.IsWarning)
                    return result.ToFailValueCustomResult<TOut>(errorFactory(result));
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<T, Result> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut>> onSuccess,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccess<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<T, CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Func<Result<T, TError>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning ? result.ToFailCustomResult(errorFactory(result)) : result;
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI]
        public static Result<TOut, TError> OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<TIn, Result<TOut, TError>> onSuccess,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TError> errorFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
            {
                // Warning as error
                return result.IsWarning
                    ? result.ToFailValueCustomResult<TOut>(errorFactory(result))
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onSuccess,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is not a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccess<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onSuccess,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return valueFactory(result);
        }

        #endregion
    }
}
