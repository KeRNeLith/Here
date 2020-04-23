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
        public static Result OnSuccessOrFailure(in this Result result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
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
        public static Result OnSuccessOrFailure(in this Result result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Action<Result> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
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
        public static Result OnSuccessOrFailure(in this Result result,
            [NotNull, InstantHandle] in Action<Result> onSuccess,
            [NotNull, InstantHandle] in Action<Result> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
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
        public static TOut OnSuccessOrFailure<TOut>(in this Result result,
            [NotNull, InstantHandle] in Func<Result, TOut> onSuccess,
            [NotNull, InstantHandle] in Func<Result, TOut> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccessOrFailure<T>(in this Result<T> result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T> OnSuccessOrFailure<T>(in this Result<T> result,
            [NotNull, InstantHandle] in Action<Result<T>> onSuccess,
            [NotNull, InstantHandle] in Action<Result<T>> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                onSuccess(result);
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TIn, TOut>(in this Result<TIn> result,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onSuccess,
            [NotNull, InstantHandle] in Func<Result<TIn>, TOut> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccessOrFailure<TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccessOrFailure<TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                onSuccess();
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static CustomResult<TError> OnSuccessOrFailure<TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onSuccess,
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
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
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="CustomResult{TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TOut, TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onSuccess,
            [NotNull, InstantHandle] in Func<CustomResult<TError>, TOut> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccessOrFailure<T, TError>(in this Result<T, TError> result,
            [NotNull, InstantHandle] in Action onSuccess,
            [NotNull, InstantHandle] in Action onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                onSuccess();
            else
                onFailure();
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static Result<T, TError> OnSuccessOrFailure<T, TError>(in this Result<T, TError> result,
            [NotNull, InstantHandle] in Action<Result<T, TError>> onSuccess,
            [NotNull, InstantHandle] in Action<Result<T, TError>> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                onSuccess(result);
            else
                onFailure(result);
            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> function when the <paramref name="result"/> is a success,
        /// otherwise the <paramref name="onFailure"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value type.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Result{TIn, TError}"/> is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static TOut OnSuccessOrFailure<TIn, TOut, TError>(in this Result<TIn, TError> result,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onSuccess,
            [NotNull, InstantHandle] in Func<Result<TIn, TError>, TOut> onFailure,
            in bool treatWarningAsError = false)
        {
            if (onSuccess is null)
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure is null)
                throw new ArgumentNullException(nameof(onFailure));

            if (IsConsideredSuccess(result.Logic, treatWarningAsError))
                return onSuccess(result);
            return onFailure(result);
        }

        #endregion
    }
}
