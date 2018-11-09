using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here.Maybes
{
    /// <summary>
    /// <see cref="Maybe{T}"/> is an object that may embed something (otherwise it's nothing).
    /// </summary>
    /// <typeparam name="T">Type of the value embedded in the <see cref="Maybe{T}"/>.</typeparam>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(HasValue) + " ? \"Value = \" + " + nameof(Value) + " : \"No value\"}")]
    public partial struct Maybe<T> : IEquatable<Maybe<T>>, IComparable, IComparable<Maybe<T>>
    {
        /// <summary>
        /// Nothing value.
        /// </summary>
        [PublicAPI]
        public static readonly Maybe<T> None;

        /// <summary>
        /// Flag that indicates if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        [PublicAPI]
        public bool HasValue { get; }

        /// <summary>
        /// Flag that indicates if this <see cref="Maybe{T}"/> has no value.
        /// </summary>
        [PublicAPI]
        public bool HasNoValue => !HasValue;

        /// <summary>
        /// Maybe value.
        /// </summary>
        [NotNull]
        private readonly T _value;

        /// <summary>
        /// Gets the value stored in the <see cref="Maybe{T}"/> if present otherwise throws.
        /// </summary>
        /// <exception cref="InvalidOperationException"> if no value is present.</exception>
        [PublicAPI, NotNull]
        public T Value
        {
            get
            {
                if (HasNoValue)
                    throw new InvalidOperationException("Trying to get the value while there is no one.");
                return _value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        private Maybe([NotNull] T value)
        {
            HasValue = true;
            _value = value;
        }

        /// <summary>
        /// Construct a <see cref="Maybe{T}"/> with a value.
        /// </summary>
        /// <param name="value">A value.</param>
        [PublicAPI]
        public static Maybe<T> Some([NotNull] T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize a Maybe with a null value.");

            return new Maybe<T>(value);
        }

        #region Equality / IEquatable

        /// <inheritdoc />
        public bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value)
                && HasValue.Equals(other.HasValue);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is Maybe<T> maybe && Equals(maybe);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ HasValue.GetHashCode();
        }

        /// <summary>
        /// Indicates whether both <see cref="Maybe{T}"/> are equal.
        /// </summary>
        /// <param name="maybe1">First <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="maybe2">Second <see cref="Maybe{T}"/> to compare.</param>
        /// <returns>True if both <see cref="Maybe{T}"/> are equal, otherwise false.</returns>
        public static bool operator==(Maybe<T> maybe1, Maybe<T> maybe2)
        {
            return maybe1.Equals(maybe2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Maybe{T}"/> are not equal.
        /// </summary>
        /// <param name="maybe1">First <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="maybe2">Second <see cref="Maybe{T}"/> to compare.</param>
        /// <returns>True if <see cref="Maybe{T}"/> are not equal, otherwise false.</returns>
        public static bool operator!=(Maybe<T> maybe1, Maybe<T> maybe2)
        {
            return !(maybe1 == maybe2);
        }

        /// <summary>
        /// Indicates whether <see cref="Maybe{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Maybe{T}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(Maybe<T> maybe, T value)
        {
            if (maybe.HasNoValue)
                return false;
            return maybe.Value.Equals(value);
        }

        /// <summary>
        /// Indicates whether <see cref="Maybe{T}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Maybe{T}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return !(maybe == value);
        }

        /// <summary>
        /// Indicates whether <see cref="Maybe{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="maybe"><see cref="Maybe{T}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Maybe{T}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(T value, Maybe<T> maybe)
        {
            return maybe == value;
        }

        /// <summary>
        /// Indicates whether <see cref="Maybe{T}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="maybe"><see cref="Maybe{T}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Maybe{T}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(T value, Maybe<T> maybe)
        {
            return !(maybe == value);
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return CompareTo(None);
            if (obj is Maybe<T> other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Maybe<T>)}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compare this <see cref="Maybe{T}"/> with the given one.
        /// Order keeps <see cref="Maybe{T}.None"/> first and <see cref="Maybe{T}"/> with value after.
        /// Then it uses the <see cref="Value"/> for the comparison.
        /// </summary>
        public int CompareTo(Maybe<T> other)
        {
            if (HasValue && !other.HasValue)
                return 1;
            if (!HasValue && other.HasValue)
                return -1;
            return Comparer<T>.Default.Compare(_value, other._value);
        }

        /// <summary>
        /// Determines if this <see cref="Maybe{T}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Maybe{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(Maybe<T> left, Maybe<T> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Maybe{T}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Maybe{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(Maybe<T> left, Maybe<T> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Maybe{T}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Maybe{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(Maybe<T> left, Maybe<T> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Maybe{T}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Maybe{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Maybe{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(Maybe<T> left, Maybe<T> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            if (HasNoValue)
                return "None";
            return Value.ToString();
        }
    }
}