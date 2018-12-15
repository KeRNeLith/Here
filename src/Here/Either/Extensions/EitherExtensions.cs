using System;
#if (!NET20 && !NET30 && !NET35 && !NET40)
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;
using static Here.HereHelpers;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Either{TLeft,TRight}"/>.
    /// </summary>
    public static class EitherExtensions
    {
        /// <summary>
        /// Indicates if this <see cref="IEither"/> is a success (right state).
        /// </summary>
        /// <param name="either"><see cref="IEither"/> to check.</param>
        /// <returns>True if the <see cref="IEither"/> is a success, otherwise false.</returns>
        [PublicAPI]
        public static bool IsSuccess(this IEither either) => either.IsRight;

        /// <summary>
        /// Indicates if this <see cref="IEither"/> is a success (left state).
        /// </summary>
        /// <param name="either"><see cref="IEither"/> to check.</param>
        /// <returns>True if the <see cref="IEither"/> is a failure, otherwise false.</returns>
        [PublicAPI]
        public static bool IsFailure(this IEither either) => either.IsLeft;
        
        #region Match

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when the <paramref name="either"/> is a success.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Action to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Action to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="none">Action to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/>.</param>
        /// <returns>A <see cref="Unit"/>.</returns>
        [PublicAPI, Pure]
        public static Unit Match<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Action<TRight> onRight,
            [NotNull, InstantHandle] in Action<TLeft> onLeft,
            [CanBeNull, InstantHandle] in Action none = null)
        {
            if (either.IsNone)
            {
                if (none is null)
                {
                    throw new InvalidOperationException(
                        $"Trying to run a Match operation on an Either in \"{EitherStates.None}\" state without specifying a \"{nameof(none)}\" action.");
                }
                else
                    none();
            }
            else if (either.IsRight)
                onRight(either._right);
            else
                onLeft(either._left);

            return Unit.Default;
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when the <paramref name="either"/> is a success.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="none">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/>.</param>
        /// <returns>The result of the applied treatment.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static TOut MatchNullable<TLeft, TRight, TOut>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Func<TRight, TOut> onRight,
            [NotNull, InstantHandle] in Func<TLeft, TOut> onLeft,
            [CanBeNull, InstantHandle] in Func<TOut> none = null)
        {
            if (either.IsNone)
            {
                return none is null
                    ? throw new InvalidOperationException(
                        $"Trying to run a Match operation on an Either in \"{EitherStates.None}\" state without specifying a \"{nameof(none)}\" action.")
                    : none();
            }

            if (either.IsRight)
                return onRight(either._right);
            return onLeft(either._left);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when the <paramref name="either"/> is a success.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="none">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/>.</param>
        /// <returns>The result of the applied treatment.</returns>
        [PublicAPI, NotNull, Pure]
        public static TOut Match<TLeft, TRight, TOut>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Func<TRight, TOut> onRight,
            [NotNull, InstantHandle] in Func<TLeft, TOut> onLeft,
            [CanBeNull, InstantHandle] in Func<TOut> none = null)
        {
            TOut result = either.MatchNullable(onRight, onLeft, none);
            if (result == null)
                throw new NullResultException();
            return result;
        }

        #endregion

        #region IfLeft/IfFailure

        /// <summary>
        /// Calls the <paramref name="onLeft"/> action when the <paramref name="either"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>The result of the applied treatment.</returns>
        [PublicAPI, NotNull]
        public static Unit IfLeft<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Action<TLeft> onLeft)
        {
            return either.Match(DoNothing, onLeft, DoNothing);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="either"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onFailure">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>The result of the applied treatment.</returns>
        [PublicAPI, NotNull]
#if (!NET20 && !NET30 && !NET35 && !NET40)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Unit IfFailure<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Action<TLeft> onFailure)
        {
            return either.IfLeft(onFailure);
        }

        /// <summary>
        /// Returns the <paramref name="defaultValue"/> when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Left"/> state, otherwise returns the
        /// right value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="defaultValue">Default right value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>A right value.</returns>
        [PublicAPI, NotNull]
        public static TRight IfLeft<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull] TRight defaultValue)
        {
            return either.Match(Identity, _ => defaultValue);
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Left"/> state to create a right value, otherwise returns the
        /// right value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>A right value.</returns>
        [PublicAPI, NotNull]
        public static TRight IfLeft<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TRight> onLeft)
        {
            return either.Match(Identity, _ => onLeft());
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Left"/> state to create a right value, otherwise returns the
        /// right value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>A right value.</returns>
        [PublicAPI, NotNull]
        public static TRight IfLeft<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TLeft, TRight> onLeft)
        {
            return either.Match(Identity, left => onLeft(left));
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Left"/> state to create an output value, otherwise returns the
        /// default value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="defaultValue">Default value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>An output value.</returns>
        [PublicAPI, NotNull]
        public static TOut IfLeft<TLeft, TRight, TOut>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TLeft, TOut> onLeft,
            [NotNull] TOut defaultValue)
        {
            return either.Match(_ => defaultValue, left => onLeft(left));
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Left"/> state to create an output value, otherwise returns the
        /// result of <paramref name="valueFactory"/> if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="valueFactory">Function to run to create a value if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>An output value.</returns>
        [PublicAPI, NotNull]
        public static TOut IfLeft<TLeft, TRight, TOut>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TLeft, TOut> onLeft,
            [NotNull, InstantHandle] Func<TOut> valueFactory)
        {
            return either.Match(_ => valueFactory(), left => onLeft(left));
        }

        #endregion

        #region IfRight

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when the <paramref name="either"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>The result of the applied treatment.</returns>
        [PublicAPI, NotNull]
        public static Unit IfRight<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Action<TRight> onRight)
        {
            return either.Match(onRight, DoNothing, DoNothing);
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when the <paramref name="either"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onSuccess">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>The result of the applied treatment.</returns>
        [PublicAPI, NotNull]
#if (!NET20 && !NET30 && !NET35 && !NET40)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static Unit IfSuccess<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] in Action<TRight> onSuccess)
        {
            return either.IfRight(onSuccess);
        }

        /// <summary>
        /// Returns the <paramref name="defaultValue"/> when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Right"/> state, otherwise returns the
        /// left value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="defaultValue">Default left value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A left value.</returns>
        [PublicAPI, NotNull]
        public static TLeft IfRight<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull] TLeft defaultValue)
        {
            return either.Match(_ => defaultValue, Identity);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Right"/> state to create a left value, otherwise returns the
        /// left value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A left value.</returns>
        [PublicAPI, NotNull]
        public static TLeft IfRight<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TLeft> onRight)
        {
            return either.Match(_ => onRight(), Identity);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Right"/> state to create a left value, otherwise returns the
        /// left value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A left value.</returns>
        [PublicAPI, NotNull]
        public static TLeft IfRight<TLeft, TRight>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TRight, TLeft> onRight)
        {
            return either.Match(right => onRight(right), Identity);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Right"/> state to create an output value, otherwise returns the
        /// default value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="defaultValue">Default value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An output value.</returns>
        [PublicAPI, NotNull]
        public static TOut IfRight<TLeft, TRight, TOut>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TRight, TOut> onRight,
            [NotNull] TOut defaultValue)
        {
            return either.Match(right => onRight(right), _ => defaultValue);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when the <paramref name="either"/> is in
        /// <see cref="EitherStates.Right"/> state to create an output value, otherwise returns the
        /// result of <paramref name="valueFactory"/> if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to check.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="valueFactory">Function to run to create a value if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An output value.</returns>
        [PublicAPI, NotNull]
        public static TOut IfRight<TLeft, TRight, TOut>(
            in this Either<TLeft, TRight> either,
            [NotNull, InstantHandle] Func<TRight, TOut> onRight,
            [NotNull, InstantHandle] Func<TOut> valueFactory)
        {
            return either.Match(right => onRight(right), _ => valueFactory());
        }

        #endregion
    }
}