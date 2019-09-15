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
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] Action onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
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
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] Action<Result> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
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
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] Func<Result, Result> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
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
        public static TOut OnFailure<TOut>(this Result result, 
            [NotNull, InstantHandle] Func<Result, TOut> onFailure, 
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
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
        public static TOut OnFailure<TOut>(this Result result,
            [NotNull, InstantHandle] Func<Result, TOut> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory();
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] Action onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] Action<Result<T>> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] Func<Result<T>, Result> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] Func<Result<T>, Result<T>> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onFailure,
            [NotNull, InstantHandle] Func<TIn, TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory(result.Value);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] Action<CustomResult<TError>> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure<TError>(this CustomResult<TError> result, [NotNull, InstantHandle] Func<CustomResult<TError>, Result> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, [NotNull, InstantHandle] Func<CustomResult<TError>, CustomResult<TError>> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TOut, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure,
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TOut, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory();
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action<Result<T, TError>> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnFailure<T, TError>(this Result<T, TError> result, [NotNull, InstantHandle] Func<Result<T, TError>, Result> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnFailure<T, TError>(this Result<T, TError> result, [NotNull, InstantHandle] Func<Result<T, TError>, Result<T>> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<T, TError>(this Result<T, TError> result, [NotNull, InstantHandle] Func<Result<T, TError>, CustomResult<TError>> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T,TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T,TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T,TError}"/> resulting of <paramref name="onFailure"/>, otherwise a success.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result, [NotNull, InstantHandle] Func<Result<T, TError>, Result<T, TError>> onFailure, bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure, otherwise return the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure
        /// otherwise return the <paramref name="valueFactory"/> return value.
        /// </summary>
        /// <typeparam name="TIn">Result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static TOut OnFailure<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onFailure,
            [NotNull, InstantHandle] Func<TIn, TOut> valueFactory,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);
            return valueFactory(result.Value);
        }

        #endregion
    }
}
