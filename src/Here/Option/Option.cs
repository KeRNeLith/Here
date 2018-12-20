using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="Option{T}"/> is an object that may embed something (otherwise it's nothing).
    /// </summary>
    /// <typeparam name="T">Type of the value embedded in the <see cref="Option{T}"/>.</typeparam>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(HasValue) + " ? \"Value = \" + " + nameof(Value) + " : \"No value\"}")]
    public readonly partial struct Option<T> : IEquatable<T>, IEquatable<Option<T>>, IComparable, IComparable<Option<T>>
    {
        /// <summary>
        /// Nothing value.
        /// </summary>
        [PublicAPI]
        public static readonly Option<T> None;

        /// <summary>
        /// Flag that indicates if this <see cref="Option{T}"/> has a value.
        /// </summary>
        [PublicAPI]
        public bool HasValue { get; }

        /// <summary>
        /// Flag that indicates if this <see cref="Option{T}"/> has no value.
        /// </summary>
        [PublicAPI]
        public bool HasNoValue => !HasValue;

        /// <summary>
        /// Option value.
        /// </summary>
        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T _value;

        /// <summary>
        /// Gets the value stored in the <see cref="Option{T}"/> if present otherwise throws.
        /// </summary>
        /// <exception cref="InvalidOperationException"> if the <see cref="Option{T}"/> has no value.</exception>
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
        private Option([NotNull] in T value)
        {
            HasValue = true;
            _value = value;
        }

        /// <summary>
        /// Constructs an <see cref="Option{T}"/> with a value.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="value"/> is null.</exception>
        [PublicAPI]
        public static Option<T> Some([NotNull] in T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an Option with a null value.");

            return new Option<T>(value);
        }

        #region Equality / IEquatable

        /// <inheritdoc />
        public bool Equals(T other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public bool Equals(Option<T> other)
        {
            return AreEqual(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (obj is Option<T> option)
                return AreEqual(this, option);
            return obj is T value && AreEqual(this, value);
        }

        /// <summary>
        /// Indicates whether <see cref="Option{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if the <see cref="Option{T}"/> value is equals to the given value, otherwise false.</returns>
        [Pure]
        internal static bool AreEqual(in Option<T> option, [CanBeNull] in T value, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            if (option.HasNoValue)
                return false;
            return (equalityComparer ?? EqualityComparer<T>.Default).Equals(option._value, value);
        }

        /// <summary>
        /// Indicates whether both <see cref="Option{T}"/> are equal.
        /// </summary>
        /// <param name="option1">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="option2">Second <see cref="Option{T}"/> to compare.</param>
        /// <param name="equalityComparer">Equality comparer to use to compare values.</param>
        /// <returns>True if both <see cref="Option{T}"/> are equal, otherwise false.</returns>
        [Pure]
        internal static bool AreEqual(in Option<T> option1, in Option<T> option2, [CanBeNull] in IEqualityComparer<T> equalityComparer = null)
        {
            if (option1.HasNoValue && option2.HasNoValue)
                return true;

            if (option1.HasNoValue || option2.HasNoValue)
                return false;

            return (equalityComparer ?? EqualityComparer<T>.Default).Equals(option1._value, option2._value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ HasValue.GetHashCode();
        }

        /// <summary>
        /// Indicates whether both <see cref="Option{T}"/> are equal.
        /// </summary>
        /// <param name="option1">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="option2">Second <see cref="Option{T}"/> to compare.</param>
        /// <returns>True if both <see cref="Option{T}"/> are equal, otherwise false.</returns>
        public static bool operator==(in Option<T> option1, in Option<T> option2)
        {
            return AreEqual(option1, option2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Option{T}"/> are not equal.
        /// </summary>
        /// <param name="option1">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="option2">Second <see cref="Option{T}"/> to compare.</param>
        /// <returns>True if <see cref="Option{T}"/> are not equal, otherwise false.</returns>
        public static bool operator!=(in Option<T> option1, in Option<T> option2)
        {
            return !(option1 == option2);
        }

        /// <summary>
        /// Indicates whether <see cref="Option{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Option{T}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in Option<T> option, in T value)
        {
            return AreEqual(option, value);
        }

        /// <summary>
        /// Indicates whether <see cref="Option{T}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Option{T}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in Option<T> option, in T value)
        {
            return !(option == value);
        }

        /// <summary>
        /// Indicates whether <see cref="Option{T}"/> value is equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="option"><see cref="Option{T}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Option{T}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in T value, in Option<T> option)
        {
            return option == value;
        }

        /// <summary>
        /// Indicates whether <see cref="Option{T}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="option"><see cref="Option{T}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Option{T}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in T value, in Option<T> option)
        {
            return !(option == value);
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return Compare(this, None);
            if (obj is Option<T> other)
                return Compare(this, other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Option<T>)}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares this <see cref="Option{T}"/> with the given one.
        /// Order keeps <see cref="Option{T}.None"/> first and <see cref="Option{T}"/> with value after.
        /// Then it uses the <see cref="Value"/> for the comparison.
        /// </summary>
        public int CompareTo(Option<T> other)
        {
            return Compare(this, other);
        }

        /// <summary>
        /// Compares both <see cref="Option{T}"/>.
        /// Order keeps <see cref="Option{T}.None"/> first and <see cref="Option{T}"/> with value after.
        /// Then it uses the <see cref="Option{T}.Value"/> for the comparison.
        /// </summary>
        /// <param name="option1">First <see cref="Option{T}"/> to compare.</param>
        /// <param name="option2">Second <see cref="Option{T}"/> to compare.</param>
        /// <param name="comparer">Equality comparer to use to compare values.</param>
        /// <returns>An integer that indicates the relative order of compared objects.</returns>
        [Pure]
        internal static int Compare(in Option<T> option1, in Option<T> option2, [CanBeNull] in IComparer<T> comparer = null)
        {
            if (option1.HasValue && !option2.HasValue)
                return 1;
            if (!option1.HasValue && option2.HasValue)
                return -1;
            return (comparer ?? Comparer<T>.Default).Compare(option1._value, option2._value);
        }

        /// <summary>
        /// Determines if this <see cref="Option{T}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Option{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Option{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Option<T> left, in Option<T> right)
        {
            return Compare(left, right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Option{T}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Option{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Option{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Option<T> left, in Option<T> right)
        {
            return Compare(left, right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Option{T}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Option{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Option{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Option<T> left, in Option<T> right)
        {
            return Compare(left, right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Option{T}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Option{T}"/> to compare.</param>
        /// <param name="right">The second <see cref="Option{T}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Option<T> left, in Option<T> right)
        {
            return Compare(left, right) >= 0;
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