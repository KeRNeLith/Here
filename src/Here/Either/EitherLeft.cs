using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="EitherLeft{TLeft}"/> is an object that represents an <see cref="Either{TLeft,TRight}"/> initialized in a "Left" state.
    /// Convention is that Left is used for failure.
    /// </summary>
    /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="EitherLeft{TLeft}"/>.</typeparam>
    [PublicAPI]
    [DebuggerDisplay("IsLeft, Value = {" + nameof(Left) + "}")]
    public readonly struct EitherLeft<TLeft> :
        IEither,
        IEquatable<TLeft>,
        IEquatable<EitherLeft<TLeft>>, 
        IComparable,
        IComparable<TLeft>,
        IComparable<EitherLeft<TLeft>>
    {
        /// <inheritdoc />
        public bool IsLeft => true;

        /// <inheritdoc />
        public bool IsRight => false;

        [NotNull]
        internal readonly TLeft Left;

        /// <summary>
        /// Construct an <see cref="EitherLeft{TLeft}"/> with a value.
        /// </summary>
        /// <param name="value">A value.</param>
        internal EitherLeft([NotNull] TLeft value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an EitherLeft<TLeft> with a null value.");

            Left = value;
        }

        #region Equality / IEquatable

        /// <inheritdoc />
        public bool Equals(TLeft other)
        {
            return EqualityComparer<TLeft>.Default.Equals(Left, other);
        }

        /// <inheritdoc />
        public bool Equals(EitherLeft<TLeft> other)
        {
            return Equals(Left, other.Left);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is EitherLeft<TLeft> eitherLeft)
                return Equals(eitherLeft);
            return false;
        }

        /// <summary>
        /// Indicates whether both <see cref="EitherLeft{TLeft}"/> are equal.
        /// </summary>
        /// <param name="either1">First <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either2">Second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>True if both <see cref="EitherLeft{TLeft}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in EitherLeft<TLeft> either1, in EitherLeft<TLeft> either2)
        {
            return either1.Equals(either2);
        }

        /// <summary>
        /// Indicates whether both <see cref="EitherLeft{TLeft}"/> are not equal.
        /// </summary>
        /// <param name="either1">First <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="either2">Second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>True if both <see cref="EitherLeft{TLeft}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in EitherLeft<TLeft> either1, in EitherLeft<TLeft> either2)
        {
            return !(either1 == either2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return EqualityComparer<TLeft>.Default.GetHashCode(Left);
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is EitherLeft<TLeft> other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with an {typeof(EitherLeft<>)}");
        }

        /// <inheritdoc />
        public int CompareTo(TLeft other)
        {
            return Comparer<TLeft>.Default.Compare(Left, other);
        }

        /// <inheritdoc />
        public int CompareTo(EitherLeft<TLeft> other)
        {
            return CompareTo(other.Left);
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in EitherLeft<TLeft> left, in EitherLeft<TLeft> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in EitherLeft<TLeft> left, in EitherLeft<TLeft> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in EitherLeft<TLeft> left, in EitherLeft<TLeft> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="EitherLeft{TLeft}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <param name="right">The second <see cref="EitherLeft{TLeft}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in EitherLeft<TLeft> left, in EitherLeft<TLeft> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Left({Left})";
        }
    }
}