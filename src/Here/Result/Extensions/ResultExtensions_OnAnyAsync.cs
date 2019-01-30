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
        /// <param name="onAny">Action to run.</param>
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
        /// <param name="onAny">Action to run.</param>
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Action onAny,
            bool continueOnCapturedContext = false)
        {
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
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Action<Result> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TOut>([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, TOut> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return result.OnAny(onAny);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task OnAnyAsync([NotNull] this Task<Result> resultTask,
            [NotNull, InstantHandle] Func<Result, Task> onAny,
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary>
        /// Calls the <paramref name="onAny"/> action all the time.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="resultTask"><see cref="Task{Result}"/>.</param>
        /// <param name="onAny">Action to run.</param>
        /// <param name="continueOnCapturedContext">Indicates if asynchronous execution should continue on captured context.</param>
        /// <returns>A <see cref="Task{TOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onAny"/> is null.</exception>
        [PublicAPI]
        public static async Task<TOut> OnAnyAsync<TOut>([NotNull] this Task<Result> resultTask, 
            [NotNull, InstantHandle] Func<Result, Task<TOut>> onAny, 
            bool continueOnCapturedContext = false)
        {
            Throw.IfArgumentNull(onAny, nameof(onAny));

            Result result = await resultTask.ConfigureAwait(continueOnCapturedContext);
            return await onAny(result).ConfigureAwait(continueOnCapturedContext);
        }

        #endregion
    }
}
#endif
