using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="Either{TLeft,TRight}"/> is an object that represents a value of one of two possible types.
    /// <see cref="Either{TLeft,TRight}"/> is an instance of "Left" or "Right".
    /// A common use of <see cref="Either{TLeft,TRight}"/> is as an alternative to <see cref="Option{T}"/> for dealing with possible missing values.
    /// In this usage, <see cref="Option{TLeft}.None"/> is replaced with a Left which can contain useful information. Right takes the place of <see cref="Option{TRight}.Some"/>.
    /// Convention is that Left is used for failure and Right is used for success.
    /// </summary>
    /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
    /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
    [PublicAPI]
    [DebuggerDisplay("{\"Is\" + " + nameof(_state) + " + (" + nameof(IsNone) + " ? System.String.Empty : \", Value = \" + (" + nameof(IsLeft) + " ?" + nameof(_left) + ".ToString() : " + nameof(_right) + ".ToString()))}")]
    public readonly partial struct Either<TLeft, TRight> :
        IEither,
        IEquatable<TRight>,
        IEquatable<EitherRight<TRight>>,
        IEquatable<Either<TLeft, TRight>>, 
        IComparable,
        IComparable<TRight>,
        IComparable<EitherRight<TRight>>,
        IComparable<Either<TLeft, TRight>>
    {
        /// <summary>
        /// <see cref="Either{TLeft,TRight}"/> that is not in <see cref="EitherStates.Left"/> state neither in <see cref="EitherStates.Right"/> state.
        /// </summary>
        [PublicAPI]
        public static readonly Either<TLeft, TRight> None;

        /// <summary>
        /// State of this <see cref="Either{TLeft,TRight}"/>.
        /// </summary>
        /// <seealso cref="EitherStates"/>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly EitherStates _state;

        /// <summary>
        /// Indicates if this <see cref="Either{TLeft,TRight}"/> is a <see cref="EitherStates.None"/> state.
        /// <see cref="Either{TLeft,TRight}"/> becomes <see cref="EitherStates.None"/> when an operation with predicate
        /// returns false, this put it in a state that is neither left nor right.
        /// </summary>
        [PublicAPI]
        public bool IsNone => _state == EitherStates.None;

        /// <inheritdoc />
        public bool IsLeft => _state == EitherStates.Left;

        /// <inheritdoc />
        public bool IsRight => _state == EitherStates.Right;

        [CanBeNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        // ReSharper disable once InconsistentNaming
        internal readonly TLeft _left;

        /// <summary>
        /// Gets the left value.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the either is not in <see cref="EitherStates.Left"/> state.</exception>
        [PublicAPI, NotNull]
        public TLeft LeftValue
        {
            get
            {
                if (!IsLeft)
                    throw new InvalidOperationException("Trying to get the left value while there is no one.");

                // ReSharper disable once AssignNullToNotNullAttribute, Justification: Here the value should be not null because the Either is in a left state.
                return _left;
            }
        }

        [CanBeNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        // ReSharper disable once InconsistentNaming
        internal readonly TRight _right;

        /// <summary>
        /// Gets the right value.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the either is not in <see cref="EitherStates.Right"/> state.</exception>
        [PublicAPI, NotNull]
        public TRight RightValue
        {
            get
            {
                if (!IsRight)
                    throw new InvalidOperationException("Trying to get the right value while there is no one.");

                // ReSharper disable once AssignNullToNotNullAttribute, Justification: Here the value should be not null because the Either is in a right state.
                return _right;
            }
        }

        /// <summary>
        /// Constructs an <see cref="Either{TLeft,TRight}"/> in a <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="value">Left value.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="value"/> is null.</exception>
        private Either([NotNull] in TLeft value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an Either<TLeft, TRight> in Left state with a null value.");

            _state = EitherStates.Left;
            _left = value;
            _right = default;
        }

        /// <summary>
        /// Constructs an <see cref="Either{TLeft,TRight}"/> in a <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="value">Right value.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="value"/> is null.</exception>
        private Either([NotNull] in TRight value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an Either<TLeft, TRight> in Right with a null value.");

            _state = EitherStates.Right;
            _left = default;
            _right = value;
        }

        /// <summary>
        /// Constructs an <see cref="Either{TLeft,TRight}"/> in a <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <param name="value">Left value.</param>
        /// <returns>An <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="value"/> is null.</exception>
        [Pure]
        internal static Either<TLeft, TRight> Left([NotNull] in TLeft value)
        {
            return new Either<TLeft, TRight>(value);
        }

        /// <summary>
        /// Constructs an <see cref="Either{TLeft,TRight}"/> in a <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <param name="value">Right value.</param>
        /// <returns>An <see cref="Either{TLeft,TRight}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="value"/> is null.</exception>
        [Pure]
        internal static Either<TLeft, TRight> Right([NotNull] in TRight value)
        {
            return new Either<TLeft, TRight>(value);
        }

        /// <summary>
        /// Explicit conversion operator from <see cref="Either{TLeft,TRight}"/> to a value of left type.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>The left value.</returns>
        /// <exception cref="InvalidCastException">If the either is casted to a left value while not being in <see cref="EitherStates.Left"/> state.</exception>
        [PublicAPI, Pure]
        public static explicit operator TLeft(in Either<TLeft, TRight> either)
        {
            return either.IsLeft
                ? either._left
                : throw new InvalidCastException("Either<TLeft, TRight> is not in Left state.");
        }

        /// <summary>
        /// Explicit conversion operator from <see cref="Either{TLeft,TRight}"/> to a value of right type.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft,TRight}"/> to convert.</param>
        /// <returns>The right value.</returns>
        /// <exception cref="InvalidCastException">If the either is casted to a left value while not being in <see cref="EitherStates.Right"/> state.</exception>
        [PublicAPI, Pure]
        public static explicit operator TRight(in Either<TLeft, TRight> either)
        {
            return either.IsRight
                ? either._right
                : throw new InvalidCastException("Either<TLeft, TRight> is not in Right state.");
        }

        #region Equality / IEquatable<T>

        /// <summary>
        /// Indicates whether this <see cref="Either{TLeft,TRight}"/> is equals to the given value.
        /// </summary>
        /// <param name="other">Value to compare with.</param>
        /// <returns>True if both <see cref="Either{TLeft,TRight}"/> is equals to the value, otherwise false.</returns>
        public bool Equals(TLeft other)
        {
            return AreEqual(this, Left(other));
        }

        /// <inheritdoc />
        public bool Equals(TRight other)
        {
            return AreEqual(this, Right(other));
        }

        /// <summary>
        /// Indicates whether this <see cref="Either{TLeft,TRight}"/> is equals to the given <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <param name="other"><see cref="EitherLeft{TLeft}"/> to compare with.</param>
        /// <returns>True if both <see cref="Either{TLeft,TRight}"/> is equals to the <see cref="EitherLeft{TLeft}"/>, otherwise false.</returns>
        public bool Equals(EitherLeft<TLeft> other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public bool Equals(EitherRight<TRight> other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public bool Equals(Either<TLeft, TRight> other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is Either<TLeft, TRight> either)
                return AreEqual(this, either);
            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="Either{TLeft,TRight}"/> are equal.
        /// </summary>
        /// <param name="either1">First <see cref="Either{TLeft,TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="Either{TLeft,TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="Either{TLeft,TRight}"/> are equal, otherwise false.</returns>
        [Pure]
        internal static bool AreEqual(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            if (either1.IsLeft && either2.IsLeft)
                return EqualityComparer<TLeft>.Default.Equals(either1._left, either2._left);

            if (either1.IsRight && either2.IsRight)
                return EqualityComparer<TRight>.Default.Equals(either1._right, either2._right);

            if (either1.IsNone == either2.IsNone)
                return either1.IsNone;

            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="Either{TLeft, TRight}"/> are equal.
        /// </summary>
        /// <param name="either1">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="Either{TLeft, TRight}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return AreEqual(either1, either2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Either{TLeft, TRight}"/> are not equal.
        /// </summary>
        /// <param name="either1">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="Either{TLeft, TRight}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return !(either1 == either2);
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are equal.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Either<TLeft, TRight> either, in EitherLeft<TLeft> eitherLeft)
        {
            return either.Equals(eitherLeft);
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are not equal.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Either<TLeft, TRight> either, in EitherLeft<TLeft> eitherLeft)
        {
            return !(either == eitherLeft);
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are equal.
        /// </summary>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in EitherLeft<TLeft> eitherLeft, in Either<TLeft, TRight> either)
        {
            return either == eitherLeft;
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are not equal.
        /// </summary>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherLeft{TLeft}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in EitherLeft<TLeft> eitherLeft, in Either<TLeft, TRight> either)
        {
            return !(either == eitherLeft);
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are equal.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Either<TLeft, TRight> either, in EitherRight<TRight> eitherRight)
        {
            return either.Equals(eitherRight);
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are not equal.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Either<TLeft, TRight> either, in EitherRight<TRight> eitherRight)
        {
            return !(either == eitherRight);
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are equal.
        /// </summary>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in EitherRight<TRight> eitherRight, in Either<TLeft, TRight> either)
        {
            return either == eitherRight;
        }

        /// <summary>
        /// Indicates whether <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are not equal.
        /// </summary>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if <see cref="Either{TLeft, TRight}"/> and <see cref="EitherRight{TRight}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in EitherRight<TRight> eitherRight, in Either<TLeft, TRight> either)
        {
            return !(either == eitherRight);
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is equals to the value.
        /// </summary>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="rightValue">Value to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is in <see cref="EitherStates.Right"/> state
        /// and its value is equals to the <paramref name="rightValue"/>, otherwise false.</returns>
        public static bool operator ==(in Either<TLeft, TRight> either, in TRight rightValue)
        {
            return either.Equals(rightValue);
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is not equals to the value.
        /// </summary>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="rightValue">Value to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is not in <see cref="EitherStates.Right"/> state
        /// with its value equals to the <paramref name="rightValue"/>, otherwise false.</returns>
        public static bool operator !=(in Either<TLeft, TRight> either, in TRight rightValue)
        {
            return !(either == rightValue);
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is equals to the value.
        /// </summary>
        /// <param name="rightValue">Value to compare.</param>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is in <see cref="EitherStates.Right"/> state
        /// and its value is equals to the <paramref name="rightValue"/>, otherwise false.</returns>
        public static bool operator ==(in TRight rightValue, in Either<TLeft, TRight> either)
        {
            return either == rightValue;
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is not equals to the value.
        /// </summary>
        /// <param name="rightValue">Value to compare.</param>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is not in <see cref="EitherStates.Right"/> state
        /// with its value equals to the <paramref name="rightValue"/>, otherwise false.</returns>
        public static bool operator !=(in TRight rightValue, in Either<TLeft, TRight> either)
        {
            return !(either == rightValue);
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is equals to the value.
        /// </summary>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="leftValue">Value to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is in <see cref="EitherStates.Left"/> state
        /// and its value is equals to the <paramref name="leftValue"/>, otherwise false.</returns>
        public static bool operator ==(in Either<TLeft, TRight> either, in TLeft leftValue)
        {
            return either.Equals(leftValue);
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is not equals to the value.
        /// </summary>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="leftValue">Value to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is not in <see cref="EitherStates.Left"/> state
        /// with its value equals to the <paramref name="leftValue"/>, otherwise false.</returns>
        public static bool operator !=(in Either<TLeft, TRight> either, in TLeft leftValue)
        {
            return !(either == leftValue);
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is equals to the value.
        /// </summary>
        /// <param name="leftValue">Value to compare.</param>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is in <see cref="EitherStates.Left"/> state
        /// and its value is equals to the <paramref name="leftValue"/>, otherwise false.</returns>
        public static bool operator ==(in TLeft leftValue, in Either<TLeft, TRight> either)
        {
            return either == leftValue;
        }

        /// <summary>
        /// Indicates whether the <see cref="Either{TLeft, TRight}"/> is not equals to the value.
        /// </summary>
        /// <param name="leftValue">Value to compare.</param>
        /// <param name="either">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>True if the <see cref="Either{TLeft, TRight}"/> is not in <see cref="EitherStates.Left"/> state
        /// with its value equals to the <paramref name="leftValue"/>, otherwise false.</returns>
        public static bool operator !=(in TLeft leftValue, in Either<TLeft, TRight> either)
        {
            return !(either == leftValue);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            switch (_state)
            {
                case EitherStates.Left:
                    return EqualityComparer<TLeft>.Default.GetHashCode(_left);
                case EitherStates.Right:
                    return EqualityComparer<TRight>.Default.GetHashCode(_right);
                default:
                    return 0;
            }
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is Either<TLeft, TRight> other)
                return Compare(this, other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with an {typeof(Either<TLeft, TRight>)}.");
        }

        /// <summary>
        /// Compares the given value with this <see cref="Either{TLeft,TRight}"/>.
        /// Order keeps None first, then Left and finally Right either.
        /// </summary>
        /// <param name="other">Value to compare with.</param>
        /// <returns>The comparison with the given value.</returns>
        public int CompareTo(TLeft other)
        {
            return Compare(this, Left(other));
        }

        /// <summary>
        /// Compares the given <see cref="EitherLeft{TLeft}"/> with this <see cref="Either{TLeft,TRight}"/>.
        /// Order keeps None first, then Left and finally Right either.
        /// </summary>
        /// <param name="other">Value to compare with.</param>
        /// <returns>The comparison with the given <see cref="EitherLeft{TLeft}"/>.</returns>
        public int CompareTo(EitherLeft<TLeft> other)
        {
            return Compare(this, Left(other.Left));
        }

        /// <inheritdoc />
        public int CompareTo(TRight other)
        {
            return Compare(this, Right(other));
        }

        /// <inheritdoc />
        public int CompareTo(EitherRight<TRight> other)
        {
            return Compare(this, Right(other.Value));
        }

        /// <inheritdoc />
        public int CompareTo(Either<TLeft, TRight> other)
        {
            return Compare(this, other);
        }

        /// <summary>
        /// Compares both <see cref="Either{TLeft, TRight}"/>.
        /// Order keeps None first, then Left and finally Right either.
        /// </summary>
        /// <param name="either1">First <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [Pure]
        internal static int Compare(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            if (either1.IsNone)
                return either2.IsNone ? 0 : -1;

            if (either1.IsLeft)
            {
                if (either2.IsLeft)
                    return Comparer<TLeft>.Default.Compare(either1._left, either2._left);
                return either2.IsNone ? 1 : -1;
            }

            // Implicitly either1 is Right
            if (either2.IsRight)
                return Comparer<TRight>.Default.Compare(either1._right, either2._right);
            return 1;
        }

        #region Operators < <= > >= (Either<TLeft, TRight> Vs Either<TLeft, TRight>)

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "less" than the other one.
        /// </summary>
        /// <param name="either1">The first <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">The second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return either1.CompareTo(either2) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "less" than or equal the other one.
        /// </summary>
        /// <param name="either1">The first <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">The second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return either1.CompareTo(either2) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="either1">The first <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">The second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return either1.CompareTo(either2) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "greater" than or equal the other one.
        /// </summary>
        /// <param name="either1">The first <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="either2">The second <see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Either<TLeft, TRight> either1, in Either<TLeft, TRight> either2)
        {
            return either1.CompareTo(either2) >= 0;
        }

        #endregion

        #region Operators < <= > >= (Either<TLeft, TRight> EitherLeft<TLeft>)

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "less" than the <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Either<TLeft, TRight> either, in EitherLeft<TLeft> eitherLeft)
        {
            return Compare(either, eitherLeft) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "less" than or equal the <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Either<TLeft, TRight> either, in EitherLeft<TLeft> eitherLeft)
        {
            return Compare(either, eitherLeft) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "greater" than the <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Either<TLeft, TRight> either, in EitherLeft<TLeft> eitherLeft)
        {
            return Compare(either, eitherLeft) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "greater" than or equal the <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Either<TLeft, TRight> either, in EitherLeft<TLeft> eitherLeft)
        {
            return Compare(either, eitherLeft) >= 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "less" than the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in EitherLeft<TLeft> eitherLeft, in Either<TLeft, TRight> either)
        {
            return Compare(eitherLeft, either) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "less" than or equal the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in EitherLeft<TLeft> eitherLeft, in Either<TLeft, TRight> either)
        {
            return Compare(eitherLeft, either) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "greater" than the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in EitherLeft<TLeft> eitherLeft, in Either<TLeft, TRight> either)
        {
            return Compare(eitherLeft, either) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "greater" than or equal the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherLeft"><see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in EitherLeft<TLeft> eitherLeft, in Either<TLeft, TRight> either)
        {
            return Compare(eitherLeft, either) >= 0;
        }

        #endregion

        #region Operators < <= > >= (Either<TLeft, TRight> EitherLeft<TLeft>)

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "less" than the <see cref="EitherRight{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Either<TLeft, TRight> either, in EitherRight<TRight> eitherRight)
        {
            return Compare(either, eitherRight) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "less" than or equal the <see cref="EitherRight{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Either<TLeft, TRight> either, in EitherRight<TRight> eitherRight)
        {
            return Compare(either, eitherRight) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "greater" than the <see cref="EitherRight{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Either<TLeft, TRight> either, in EitherRight<TRight> eitherRight)
        {
            return Compare(either, eitherRight) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Either{TLeft, TRight}"/> is "greater" than or equal the <see cref="EitherRight{TRight}"/>.
        /// </summary>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Either<TLeft, TRight> either, in EitherRight<TRight> eitherRight)
        {
            return Compare(either, eitherRight) >= 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "less" than the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in EitherRight<TRight> eitherRight, in Either<TLeft, TRight> either)
        {
            return Compare(eitherRight, either) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "less" than or equal the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in EitherRight<TRight> eitherRight, in Either<TLeft, TRight> either)
        {
            return Compare(eitherRight, either) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "greater" than the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in EitherRight<TRight> eitherRight, in Either<TLeft, TRight> either)
        {
            return Compare(eitherRight, either) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "greater" than or equal the <see cref="Either{TLeft, TRight}"/>.
        /// </summary>
        /// <param name="eitherRight"><see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either"><see cref="Either{TLeft, TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in EitherRight<TRight> eitherRight, in Either<TLeft, TRight> either)
        {
            return Compare(eitherRight, either) >= 0;
        }

        #endregion

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            switch (_state)
            {
                case EitherStates.Left:
                    return $"Left({_left})";
                case EitherStates.Right:
                    return $"Right({_right})";
                default:
                    return "None";
            }
        }
    }
}