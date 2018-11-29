using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="EitherRight{TRight}"/> is an object that represents an <see cref="Either{TLeft,TRight}"/> initialized in a "Right" state.
    /// Convention is that Right is used for success.
    /// </summary>
    /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="EitherRight{TRight}"/>.</typeparam>
    [PublicAPI]
    [DebuggerDisplay("IsRight, Value = {" + nameof(_right) + "}")]
    public readonly struct EitherRight<TRight> :
        IEither,
        IEquatable<TRight>,
        IEquatable<EitherRight<TRight>>, 
        IComparable,
        IComparable<TRight>,
        IComparable<EitherRight<TRight>>
    {
        /// <inheritdoc />
        public bool IsLeft => false;

        /// <inheritdoc />
        public bool IsRight => true;

        [NotNull]
        internal readonly TRight _right;

        /// <summary>
        /// Construct an <see cref="EitherRight{TRight}"/> with a value.
        /// </summary>
        /// <param name="value">A value.</param>
        internal EitherRight([NotNull] TRight value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an EitherRight<TRight> with a null value.");

            _right = value;
        }

        #region Equality / IEquatable

        /// <inheritdoc />
        public bool Equals(TRight other)
        {
            return EqualityComparer<TRight>.Default.Equals(_right, other);
        }

        /// <inheritdoc />
        public bool Equals(EitherRight<TRight> other)
        {
            return Equals(_right, other._right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is EitherRight<TRight> eitherTRight)
                return Equals(eitherTRight);
            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="EitherRight{TRight}"/> are equal.
        /// </summary>
        /// <param name="either1">First <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="EitherRight{TRight}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in EitherRight<TRight> either1, in EitherRight<TRight> either2)
        {
            return either1.Equals(either2);
        }

        /// <summary>
        /// Indicates whether both <see cref="EitherRight{TRight}"/> are not equal.
        /// </summary>
        /// <param name="either1">First <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="either2">Second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>True if both <see cref="EitherRight{TRight}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in EitherRight<TRight> either1, in EitherRight<TRight> either2)
        {
            return !(either1 == either2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return EqualityComparer<TRight>.Default.GetHashCode(_right);
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is EitherRight<TRight> other)
                return CompareTo(other);
            if (obj is TRight value)
                return CompareTo(value);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with an {typeof(EitherRight<>)}");
        }

        /// <inheritdoc />
        public int CompareTo(TRight other)
        {
            return Comparer<TRight>.Default.Compare(_right, other);
        }

        /// <inheritdoc />
        public int CompareTo(EitherRight<TRight> other)
        {
            return CompareTo(other._right);
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in EitherRight<TRight> left, in EitherRight<TRight> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in EitherRight<TRight> left, in EitherRight<TRight> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in EitherRight<TRight> left, in EitherRight<TRight> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherRight{TRight}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherRight{TRight}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in EitherRight<TRight> left, in EitherRight<TRight> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Right({_right})";
        }
    }
}