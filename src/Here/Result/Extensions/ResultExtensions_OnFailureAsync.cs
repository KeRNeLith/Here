#if SUPPORTS_ASYNC
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using static Here.ResultInternalHelpers;

namespace Here.Extensions
{
    /// <summary>
    /// Asynchronous extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On Failure).
    /// </summary>
    public static partial class ResultAsyncExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync(this Result result,
            [NotNull, InstantHandle] Func<Result, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TOut>(this Result result,
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TOut>(this Result result,
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Action<Result> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TOut>([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TOut>([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, TOut> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TOut>([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TOut>([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return valueFactory();
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T>(this Result<T> result,
            [NotNull, InstantHandle] Func<Result<T>, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T>([NotNull] this Task<Result<T>> resultTask,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T>([NotNull] this Task<Result<T>> resultTask,
            [NotNull, InstantHandle] Action<Result<T>> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TOut>([NotNull] this Task<Result<TIn>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<TIn> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TOut>([NotNull] this Task<Result<TIn>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            Result<TIn> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T>([NotNull] this Task<Result<T>> resultTask,
            [NotNull, InstantHandle] Func<Result<T>, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TOut>([NotNull] this Task<Result<TIn>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn>, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<TIn> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TOut>([NotNull] this Task<Result<TIn>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn>, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            Result<TIn> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return valueFactory();
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TError, TOut>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TError, TOut>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<TError>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<TError>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TError, TOut>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TError, TOut>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<TError>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TError, TOut>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TError, TOut>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return valueFactory();
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<Result<T, TError>, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TError, TOut>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TError, TOut>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result).ConfigureAwait(continueOnCapturedContext);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T, TError>([NotNull] this Task<Result<T, TError>> resultTask,
            [NotNull, InstantHandle] Action onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<T, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T, TError>([NotNull] this Task<Result<T, TError>> resultTask,
            [NotNull, InstantHandle] Action<Result<T, TError>> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<T, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn, TError}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TError, TOut>([NotNull] this Task<Result<TIn, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<TIn, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn, TError}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TError, TOut>([NotNull] this Task<Result<TIn, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            Result<TIn, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return onFailure(result);

            return valueFactory();
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task OnFailureAsync<T, TError>([NotNull] this Task<Result<T, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<T, TError>, Task> onFailure,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<T, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                await onFailure(result);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn, TError}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="defaultValue">Value to return if the result is not a failure.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn, TError}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TError, TOut>([NotNull] this Task<Result<TIn, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, Task<TOut>> onFailure,
            [CanBeNull] TOut defaultValue,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));

            Result<TIn, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return defaultValue;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> function when the <paramref name="resultTask"/> embed a failed <see cref="Result{TIn}"/>.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="valueFactory">Function called to create a value to return if the result is a success.</param>
        /// <param name="onFailure">Function to run if the <see cref="Result{TIn}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnFailureAsync<TIn, TError, TOut>([NotNull] this Task<Result<TIn, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, Task<TOut>> onFailure,
            [NotNull, InstantHandle] Func<TOut> valueFactory,
            bool treatWarningAsError = false,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onFailure, nameof(onFailure));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            Result<TIn, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);

            if (IsConsideredFailure(result, treatWarningAsError))
                return await onFailure(result);

            return valueFactory();
        }

        #endregion
    }
}
#endif
