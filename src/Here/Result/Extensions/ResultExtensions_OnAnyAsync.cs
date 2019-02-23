#if SUPPORTS_ASYNC
using System;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Asynchronous extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On any).
    /// </summary>
    public static partial class ResultAsyncExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task OnAnyAsync(this Result result,
            [NotNull, InstantHandle] Func<Result, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task<TOut> OnAnyAsync<TOut>(this Result result, 
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Action onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Action<Result> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TOut>([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, TOut> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TOut>([NotNull] this Task<Result> resultTask, 
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onAny, 
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task OnAnyAsync<T>(this Result<T> result,
            [NotNull, InstantHandle] Func<Result<T>, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task<TOut> OnAnyAsync<TIn, TOut>(this Result<TIn> result,
            [NotNull, InstantHandle] Func<Result<TIn>, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<T>([NotNull] this Task<Result<T>> resultTask,
            [NotNull, InstantHandle] Action onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<T>([NotNull] this Task<Result<T>> resultTask,
            [NotNull, InstantHandle] Action<Result<T>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TIn, TOut>([NotNull] this Task<Result<TIn>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn>, TOut> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<TIn> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<T>([NotNull] this Task<Result<T>> resultTask,
            [NotNull, InstantHandle] Func<Result<T>, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<T> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TIn, TOut>([NotNull] this Task<Result<TIn>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn>, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<TIn> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task OnAnyAsync<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task<TOut> OnAnyAsync<TError, TOut>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<TError>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Action onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<TError>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TError, TOut>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, TOut> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<TError>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TError, TOut>([NotNull] this Task<CustomResult<TError>> resultTask,
            [NotNull, InstantHandle] Func<CustomResult<TError>, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            CustomResult<TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task OnAnyAsync<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Func<Result<T, TError>, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="result"><see cref="Result{TIn, TError}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static async Task<TOut> OnAnyAsync<TIn, TError, TOut>(this Result<TIn, TError> result,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<T, TError>([NotNull] this Task<Result<T, TError>> resultTask,
            [NotNull, InstantHandle] Action onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<T, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<T, TError>([NotNull] this Task<Result<T, TError>> resultTask,
            [NotNull, InstantHandle] Action<Result<T, TError>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<T, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TIn, TError, TOut>([NotNull] this Task<Result<TIn, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, TOut> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<TIn, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync<T, TError>([NotNull] this Task<Result<T, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<T, TError>, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<T, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TIn">Type of the input result value.</typeparam>
        /// <typeparam name="TError">Type of the result error.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Function to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="resultTask"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TIn, TError, TOut>([NotNull] this Task<Result<TIn, TError>> resultTask,
            [NotNull, InstantHandle] Func<Result<TIn, TError>, Task<TOut>> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(resultTask, nameof(resultTask));
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result<TIn, TError> result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        #endregion
    }
}
#endif
