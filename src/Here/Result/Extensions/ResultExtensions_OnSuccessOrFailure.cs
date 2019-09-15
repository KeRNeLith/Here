using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On success or failure).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccessOrFailure(this Result result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccessOrFailure(this Result result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Action<Result> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>This <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result OnSuccessOrFailure(this Result result,
            [NotNull, InstantHandle] Action<Result> onSuccess,
            [NotNull, InstantHandle] Action<Result> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result);
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TOut>(this Result result,
            [NotNull, InstantHandle] Func<Result, TOut> onSuccess,
            [NotNull, InstantHandle] Func<Result, TOut> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccessOrFailure<T>(this Result<T> result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccessOrFailure<T>(this Result<T> result,
            [NotNull, InstantHandle] Action<Result<T>> onSuccess,
            [NotNull, InstantHandle] Action<Result<T>> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result);
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onSuccess,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccessOrFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccessOrFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccessOrFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result);
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TOut, TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onSuccess,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccessOrFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action onSuccess,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccessOrFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action<Result<T, TError>> onSuccess,
            [NotNull, InstantHandle] Action<Result<T, TError>> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                onSuccess(result);
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TIn">Result input value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TIn, TOut, TError>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onSuccess,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onFailure,
            bool treatWarningAsError = false)
        {
            Throw.IfArgumentNull(onSuccess, nameof(onSuccess));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredSuccess(result, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion
    }
}
