using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On failure).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure(in this Result result, [NotNull, InstantHandle] in Action onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure(in this Result result, [NotNull, InstantHandle] in Action<Result> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure(in this Result result, [NotNull, InstantHandle] in Func<Result, Result> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TOut>(in this Result result, 
            [NotNull, InstantHandle] in Func<Result, TOut> onFailure, 
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TOut>(in this Result result,
            [NotNull, InstantHandle] in Func<Result, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return valueFactory();
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T>(in this Result<T> result, [NotNull, InstantHandle] in Action onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T>(in this Result<T> result, [NotNull, InstantHandle] in Action<Result<T>> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure<T>(in this Result<T> result, [NotNull, InstantHandle] in Func<Result<T>, Result> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T>(in this Result<T> result, [NotNull, InstantHandle] in Func<Result<T>, Result<T>> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut>(
            in this Result<TIn> result,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onFailure,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Type of the result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut>(
            in this Result<TIn> result,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TIn, TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return valueFactory(result._value);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(
            in this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(
            in this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure<TError>(in this CustomResult<TError> result, [NotNull, InstantHandle] in Func<CustomResult<TError>, Result> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(in this CustomResult<TError> result, [NotNull, InstantHandle] in Func<CustomResult<TError>, CustomResult<TError>> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TOut, TError>(
            in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onFailure,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TOut, TError>(
            in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return valueFactory();
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(
            in this Result<T, TError> result,
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(
            in this Result<T, TError> result,
            [NotNull, InstantHandle] in Action<Result<T, TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure<T, TError>(in this Result<T, TError> result, [NotNull, InstantHandle] in Func<Result<T, TError>, Result> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T, TError>(in this Result<T, TError> result, [NotNull, InstantHandle] in Func<Result<T, TError>, Result<T>> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<T, TError>(in this Result<T, TError> result, [NotNull, InstantHandle] in Func<Result<T, TError>, CustomResult<TError>> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T,TError}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(in this Result<T, TError> result, [NotNull, InstantHandle] in Func<Result<T, TError>, Result<T, TError>> onFailure, in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut, TError>(
            in this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onFailure,
            [CanBeNull] in TOut defaultValue,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Type of the result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut, TError>(
            in this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onFailure,
            [NotNull, InstantHandle] in Func<TIn, TOut> valueFactory,
            in bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result.Logic, treatWarningAsError))
                return onFailure(result);
            return valueFactory(result._value);
        }

        #endregion
    }
}
