using System;
#if SUPPORTS_AGGRESSIVE_INLINING
using System.Runtime.CompilerServices;
#endif
using JetBrains.Annotations;
using static Here.HereHelpers;

namespace Here
{
    // Operations
    public partial struct Either<TLeft, TRight>
    {
        #region Match

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// the <paramref name="onLeft"/> function if it is in <see cref="EitherStates.Left"/> state, otherwise calls the <paramref name="none"/> function.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="none">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/>.</param>
        /// <returns>The result of the applied treatment.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state without providing a <paramref name="none"/> action.</exception>
        [PublicAPI, CanBeNull, Pure]
        public TOut MatchNullable<TOut>(
            [NotNull, InstantHandle] in Func<TRight, TOut> onRight,
            [NotNull, InstantHandle] in Func<TLeft, TOut> onLeft,
            [CanBeNull, InstantHandle] in Func<TOut> none = null)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            if (IsNone)
            {
                return none is null
                    ? throw new InvalidOperationException(
                        $"Trying to run a Match operation on an Either in \"{EitherStates.None}\" state without specifying a \"{nameof(none)}\" action.")
                    : none();
            }

            if (IsRight)
                return onRight(_right);
            return onLeft(_left);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// the <paramref name="onLeft"/> if it is in <see cref="EitherStates.Left"/> state, otherwise calls the <paramref name="none"/> action.
        /// </summary>
        /// <param name="onRight">Action to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Action to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="none">Action to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/>.</param>
        /// <returns>A <see cref="Unit"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state without providing a <paramref name="none"/> action.</exception>
        [PublicAPI]
        public Unit Match(
            [NotNull, InstantHandle] in Action<TRight> onRight,
            [NotNull, InstantHandle] in Action<TLeft> onLeft,
            [CanBeNull, InstantHandle] in Action none = null)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            if (IsNone)
            {
                if (none is null)
                {
                    throw new InvalidOperationException(
                        $"Trying to run a Match operation on an Either in \"{EitherStates.None}\" state without specifying a \"{nameof(none)}\" action.");
                }

                none();
            }
            else if (IsRight)
                onRight(_right);
            else
                onLeft(_left);

            return Unit.Default;
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// the <paramref name="onLeft"/> function if it is in <see cref="EitherStates.Left"/> state, otherwise calls the <paramref name="none"/> function.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="none">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/>.</param>
        /// <returns>The result of the applied treatment.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state without providing a <paramref name="none"/> action.</exception>
        /// <exception cref="NullResultException">If the result of the match is null.</exception>
        [PublicAPI, NotNull, Pure]
        public TOut Match<TOut>(
            [NotNull, InstantHandle] in Func<TRight, TOut> onRight,
            [NotNull, InstantHandle] in Func<TLeft, TOut> onLeft,
            [CanBeNull, InstantHandle] in Func<TOut> none = null)
        {
            TOut result = MatchNullable(onRight, onLeft, none);
            return Throw.IfResultNull(result);
        }

        #endregion

        #region Map/BiMap

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeftOut">Type of the value embedded as left value in the output <see cref="Either{TLeftOut, TRight}"/>.</typeparam>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An <see cref="Either{TLeftOut,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeftOut, TRight> Map<TLeftOut>(
            [NotNull, InstantHandle] Func<TLeft, TLeftOut> onLeft)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(
                right => Either<TLeftOut, TRight>.Right(right),
                left => Either<TLeftOut, TRight>.Left(Throw.IfResultNull(onLeft(left))),
                () => Either<TLeftOut, TRight>.None);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TRightOut">Type of the value embedded as right value in the output <see cref="Either{TLeft, TRightOut}"/>.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>An <see cref="Either{TLeft,TRightOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeft, TRightOut> Map<TRightOut>(
            [NotNull, InstantHandle] Func<TRight, TRightOut> onRight)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));

            return Match(
                right => Either<TLeft, TRightOut>.Right(Throw.IfResultNull(onRight(right))),
                left => Either<TLeft, TRightOut>.Left(left),
                () => Either<TLeft, TRightOut>.None);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// or the <paramref name="onLeft"/> function if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TRightOut">Type of the value embedded as right value in the output <see cref="Either{TLeft, TRightOut}"/>.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An <see cref="Either{TLeft,TRightOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> or <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeft, TRightOut> BiMap<TRightOut>(
            [NotNull, InstantHandle] Func<TRight, TRightOut> onRight,
            [NotNull, InstantHandle] Func<TLeft, TRightOut> onLeft)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(
                right => Either<TLeft, TRightOut>.Right(Throw.IfResultNull(onRight(right))),
                left => Either<TLeft, TRightOut>.Right(Throw.IfResultNull(onLeft(left))),
                () => Either<TLeft, TRightOut>.None);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// or the <paramref name="onLeft"/> function if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeftOut">Type of the value embedded as left value in the output <see cref="Either{TLeftOut, TRightOut}"/>.</typeparam>
        /// <typeparam name="TRightOut">Type of the value embedded as right value in the output <see cref="Either{TLeftOut, TRightOut}"/>.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An <see cref="Either{TLeftOut,TRightOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> or <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeftOut, TRightOut> BiMap<TLeftOut, TRightOut>(
            [NotNull, InstantHandle] Func<TRight, TRightOut> onRight,
            [NotNull, InstantHandle] Func<TLeft, TLeftOut> onLeft)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(
                right => Either<TLeftOut, TRightOut>.Right(Throw.IfResultNull(onRight(right))),
                left => Either<TLeftOut, TRightOut>.Left(Throw.IfResultNull(onLeft(left))),
                () => Either<TLeftOut, TRightOut>.None);
        }

        #endregion

        #region Bind

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeftOut">Type of the value embedded as left value in the output <see cref="Either{TLeftOut, TRight}"/>.</typeparam>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An <see cref="Either{TLeftOut,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeftOut, TRight> Bind<TLeftOut>(
            [NotNull, InstantHandle] Func<TLeft, Either<TLeftOut, TRight>> onLeft)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(
                right => Either<TLeftOut, TRight>.Right(right),
                onLeft,
                () => Either<TLeftOut, TRight>.None);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TRightOut">Type of the value embedded as right value in the output <see cref="Either{TLeft, TRightOut}"/>.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>An <see cref="Either{TLeft,TRightOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeft, TRightOut> Bind<TRightOut>(
            [NotNull, InstantHandle] Func<TRight, Either<TLeft, TRightOut>> onRight)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));

            return Match(
                onRight,
                left => Either<TLeft, TRightOut>.Left(left),
                () => Either<TLeft, TRightOut>.None);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// or the <paramref name="onLeft"/> function if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TRightOut">Type of the value embedded as right value in the output <see cref="Either{TLeft, TRightOut}"/>.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An <see cref="Either{TLeft,TRightOut}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public Either<TLeft, TRightOut> BiBind<TRightOut>(
            [NotNull, InstantHandle] Func<TRight, Either<TLeft, TRightOut>> onRight,
            [NotNull, InstantHandle] Func<TLeft, Either<TLeft, TRightOut>> onLeft)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(
                onRight,
                onLeft,
                () => Either<TLeft, TRightOut>.None);
        }

        #endregion

        #region Fold/BiFold

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state
        /// to combine the initial state with, otherwise returns the initial state.
        /// </summary>
        /// <typeparam name="TState">Type of the value to combine.</typeparam>
        /// <param name="state">The initial value to combine with the left value.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>Combined state value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="state"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public TState Fold<TState>(
            [NotNull] TState state,
            [NotNull, InstantHandle] Func<TState, TLeft, TState> onLeft)
        {
            Throw.IfArgumentNull(state, nameof(state));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return 
                Throw.IfResultNull(
                    Match(
                        right => state,
                        left => onLeft(state, left),
                        () => state));
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state
        /// to combine the initial state with, otherwise returns the initial state.
        /// </summary>
        /// <typeparam name="TState">Type of the value to combine.</typeparam>
        /// <param name="state">The initial value to combine with the left value.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>Combined state value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="state"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> is null.</exception>
        [PublicAPI, Pure]
        public TState Fold<TState>(
            [NotNull] TState state,
            [NotNull, InstantHandle] Func<TState, TRight, TState> onRight)
        {
            Throw.IfArgumentNull(state, nameof(state));
            Throw.IfArgumentNull(onRight, nameof(onRight));

            return 
                Throw.IfResultNull(
                    Match(
                        right => onRight(state, right),
                        left => state,
                        () => state));
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state
        /// to combine the initial state with, or the <paramref name="onLeft"/> function if it is in <see cref="EitherStates.Left"/> state,
        /// otherwise returns the initial state.
        /// </summary>
        /// <typeparam name="TState">Type of the value to combine.</typeparam>
        /// <param name="state">The initial value to combine with the left value.</param>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>Combined state value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="state"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> or <paramref name="onLeft"/> is null.</exception>
        [PublicAPI, Pure]
        public TState BiFold<TState>(
            [NotNull] TState state,
            [NotNull, InstantHandle] Func<TState, TRight, TState> onRight,
            [NotNull, InstantHandle] Func<TState, TLeft, TState> onLeft)
        {
            Throw.IfArgumentNull(state, nameof(state));
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return 
                Throw.IfResultNull(
                    Match(
                        right => onRight(state, right),
                        left => onLeft(state, left),
                        () => state));
        }

        #endregion

        #region IfLeft/IfFailure

        /// <summary>
        /// Calls the <paramref name="onLeft"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>The result of the applied treatment.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI]
        public Unit IfLeft([NotNull, InstantHandle] in Action<TLeft> onLeft)
        {
            return Match(DoNothing, onLeft, DoNothing);
        }

        /// <summary>
        /// Returns the <paramref name="defaultValue"/> when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Left"/> state, otherwise returns the
        /// right value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="defaultValue">Default right value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>A right value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="defaultValue"/> is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TRight IfLeft([NotNull] TRight defaultValue)
        {
            Throw.IfArgumentNull(defaultValue, nameof(defaultValue));

            return Match(Identity, _ => defaultValue);
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Left"/> state to create a right value, otherwise returns the
        /// right value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>A right value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onLeft"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TRight IfLeft([NotNull, InstantHandle] Func<TRight> onLeft)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(Identity, _ => onLeft());
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Left"/> state to create a right value, otherwise returns the
        /// right value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>A right value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onLeft"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TRight IfLeft([NotNull, InstantHandle] Func<TLeft, TRight> onLeft)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            return Match(Identity, onLeft);
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Left"/> state to create an output value, otherwise returns the
        /// default value if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="defaultValue">Default value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="defaultValue"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onLeft"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TOut IfLeft<TOut>(
            [NotNull, InstantHandle] Func<TLeft, TOut> onLeft,
            [NotNull] TOut defaultValue)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));
            Throw.IfArgumentNull(defaultValue, nameof(defaultValue));

            return Match(_ => defaultValue, onLeft);
        }

        /// <summary>
        /// Calls the <paramref name="onLeft"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Left"/> state to create an output value, otherwise returns the
        /// result of <paramref name="valueFactory"/> if it is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <param name="valueFactory">Function to run to create a value if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onLeft"/> or <paramref name="valueFactory"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TOut IfLeft<TOut>(
            [NotNull, InstantHandle] Func<TLeft, TOut> onLeft,
            [NotNull, InstantHandle] Func<TOut> valueFactory)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            return Match(_ => valueFactory(), onLeft);
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onFailure">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>The result of the applied treatment.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Unit IfFailure([NotNull, InstantHandle] in Action<TLeft> onFailure)
        {
            return IfLeft(onFailure);
        }

        #endregion

        #region OnLeft/OnFailure

        /// <summary>
        /// Calls the <paramref name="onLeft"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI]
        public Either<TLeft, TRight> OnLeft([NotNull, InstantHandle] in Action<TLeft> onLeft)
        {
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            if (IsLeft)
                onLeft(_left);
            return this;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onFailure">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Either<TLeft, TRight> OnFailure([NotNull, InstantHandle] in Action<TLeft> onFailure)
        {
            return OnLeft(onFailure);
        }

        #endregion

        #region IfRight/IfSuccess

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A <see cref="Unit"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        [PublicAPI]
        public Unit IfRight([NotNull, InstantHandle] in Action<TRight> onRight)
        {
            return Match(onRight, DoNothing, DoNothing);
        }

        /// <summary>
        /// Returns the <paramref name="defaultValue"/> when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Right"/> state, otherwise returns the
        /// left value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="defaultValue">Default left value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A left value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="defaultValue"/> is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TLeft IfRight([NotNull] TLeft defaultValue)
        {
            Throw.IfArgumentNull(defaultValue, nameof(defaultValue));

            return Match(_ => defaultValue, Identity);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Right"/> state to create a left value, otherwise returns the
        /// left value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A left value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TLeft IfRight([NotNull, InstantHandle] Func<TLeft> onRight)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));

            return Match(_ => onRight(), Identity);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Right"/> state to create a left value, otherwise returns the
        /// left value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A left value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TLeft IfRight([NotNull, InstantHandle] Func<TRight, TLeft> onRight)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));

            return Match(onRight, Identity);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Right"/> state to create an output value, otherwise returns the
        /// default value if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="defaultValue">Default value to use if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="defaultValue"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TOut IfRight<TOut>(
            [NotNull, InstantHandle] Func<TRight, TOut> onRight,
            [NotNull] TOut defaultValue)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(defaultValue, nameof(defaultValue));

            return Match(onRight, _ => defaultValue);
        }

        /// <summary>
        /// Calls the <paramref name="onRight"/> function when this <see cref="Either{TLeft,TRight}"/> is in
        /// <see cref="EitherStates.Right"/> state to create an output value, otherwise returns the
        /// result of <paramref name="valueFactory"/> if it is in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TOut">Type of the output value.</typeparam>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="valueFactory">Function to run to create a value if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>An output value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="onRight"/> or <paramref name="valueFactory"/> function is null.</exception>
        /// <exception cref="InvalidOperationException">If this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.None"/> state.</exception>
        [PublicAPI, NotNull]
        public TOut IfRight<TOut>(
            [NotNull, InstantHandle] Func<TRight, TOut> onRight,
            [NotNull, InstantHandle] Func<TOut> valueFactory)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            return Match(onRight, _ => valueFactory());
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="onSuccess">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>A <see cref="Unit"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Unit IfSuccess([NotNull, InstantHandle] in Action<TRight> onSuccess)
        {
            return IfRight(onSuccess);
        }

        #endregion

        #region OnRight/OnSuccess

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        [PublicAPI]
        public Either<TLeft, TRight> OnRight([NotNull, InstantHandle] in Action<TRight> onRight)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));

            if (IsRight)
                onRight(_right);
            return this;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="onSuccess">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Either<TLeft, TRight> OnSuccess([NotNull, InstantHandle] in Action<TRight> onSuccess)
        {
            return OnRight(onSuccess);
        }

        #endregion

        #region OnRightOrLeft/OnSuccessOrFailure

        /// <summary>
        /// Calls the <paramref name="onRight"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// or the <paramref name="onLeft"/> action when in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onRight">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onLeft">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onRight"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onLeft"/> is null.</exception>
        [PublicAPI]
        public Either<TLeft, TRight> OnRightOrLeft(
            [NotNull, InstantHandle] in Action<TRight> onRight,
            [NotNull, InstantHandle] in Action<TLeft> onLeft)
        {
            Throw.IfArgumentNull(onRight, nameof(onRight));
            Throw.IfArgumentNull(onLeft, nameof(onLeft));

            if (IsRight)
            {
                onRight(_right);
                return this;
            }

            if (IsLeft)
                onLeft(_left);

            return this;
        }

        /// <summary>
        /// Calls the <paramref name="onSuccess"/> action when this <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state,
        /// or the <paramref name="onFailure"/> action when in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="onSuccess">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Right"/> state.</param>
        /// <param name="onFailure">Function to run if the <see cref="Either{TLeft,TRight}"/> is in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>This <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="onSuccess"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="onFailure"/> is null.</exception>
        [PublicAPI]
#if SUPPORTS_AGGRESSIVE_INLINING
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public Either<TLeft, TRight> OnSuccessOrFailure(
            [NotNull, InstantHandle] in Action<TRight> onSuccess,
            [NotNull, InstantHandle] in Action<TLeft> onFailure)
        {
            return OnRightOrLeft(onSuccess, onFailure);
        }

        #endregion

        #region Unwrapping

        /// <summary>
        /// Gets the right value if the <see cref="Either{TLeft,TRight}"/> is in the
        /// <see cref="EitherStates.Right"/> state, otherwise return the default value of <typeparamref name="TRight"/>.
        /// </summary>
        /// <returns>The either right value, otherwise the default <typeparamref name="TRight"/> value.</returns>
        [PublicAPI, Pure, CanBeNull]
        public TRight RightOrDefault()
        {
            return _right;
        }

        /// <summary>
        /// Gets the right value if the <see cref="Either{TLeft,TRight}"/> is in the
        /// <see cref="EitherStates.Right"/> state, otherwise return the provided default value.
        /// </summary>
        /// <param name="defaultValue">Default value to use if the <see cref="Either{TLeft,TRight}"/> is not in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>The either right value, otherwise the provided default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="defaultValue"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public TRight RightOr([NotNull] in TRight defaultValue)
        {
            Throw.IfArgumentNull(defaultValue, nameof(defaultValue));

            if (IsRight)
            {
                // ReSharper disable once AssignNullToNotNullAttribute, Justification: Here the either is in right state.
                return _right;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the right value if the <see cref="Either{TLeft,TRight}"/> is in the
        /// <see cref="EitherStates.Right"/> state, otherwise return the provided default value.
        /// </summary>
        /// <param name="valueFactory">Function to create a value to use if the <see cref="Either{TLeft,TRight}"/> is not in <see cref="EitherStates.Right"/> state.</param>
        /// <returns>The either right value, otherwise the provided default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="valueFactory"/> function is null.</exception>
        [PublicAPI, Pure, NotNull]
        public TRight RightOr([NotNull, InstantHandle] in Func<TRight> valueFactory)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsRight)
            {
                // ReSharper disable once AssignNullToNotNullAttribute, Justification: Here the either is in right state.
                return _right;
            }

            TRight orValue = valueFactory();
            return Throw.IfResultNull(orValue);
        }

        /// <summary>
        /// Gets the left value if the <see cref="Either{TLeft,TRight}"/> is in the
        /// <see cref="EitherStates.Left"/> state, otherwise return the default value of <typeparamref name="TLeft"/>.
        /// </summary>
        /// <returns>The either left value, otherwise the default <typeparamref name="TLeft"/> value.</returns>
        [PublicAPI, Pure, CanBeNull]
        public TLeft LeftOrDefault()
        {
            return _left;
        }

        /// <summary>
        /// Gets the left value if the <see cref="Either{TLeft,TRight}"/> is in the
        /// <see cref="EitherStates.Left"/> state, otherwise return the provided default value.
        /// </summary>
        /// <param name="defaultValue">Default value to use if the <see cref="Either{TLeft,TRight}"/> is not in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>The either left value, otherwise the provided default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="defaultValue"/> is null.</exception>
        [PublicAPI, Pure, NotNull]
        public TLeft LeftOr([NotNull] in TLeft defaultValue)
        {
            Throw.IfArgumentNull(defaultValue, nameof(defaultValue));

            if (IsLeft)
            {
                // ReSharper disable once AssignNullToNotNullAttribute, Justification: Here the either is in left state.
                return _left;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the left value if the <see cref="Either{TLeft,TRight}"/> is in the
        /// <see cref="EitherStates.Left"/> state, otherwise return the provided default value.
        /// </summary>
        /// <param name="valueFactory">Function to create a value to use if the <see cref="Either{TLeft,TRight}"/> is not in <see cref="EitherStates.Left"/> state.</param>
        /// <returns>The either left value, otherwise the provided default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="NullResultException">If the result of the <paramref name="valueFactory"/> function is null.</exception>
        [PublicAPI, Pure, NotNull]
        public TLeft LeftOr([NotNull, InstantHandle] in Func<TLeft> valueFactory)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsLeft)
            {
                // ReSharper disable once AssignNullToNotNullAttribute, Justification: Here the either is in left state.
                return _left;
            }

            TLeft orValue = valueFactory();
            return Throw.IfResultNull(orValue);
        }

        #endregion
    }
}