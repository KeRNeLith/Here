using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Here.Maybes
{
    /// <summary>
    /// <see cref="Maybe{T}"/> is an object that embed something or nothing.
    /// </summary>
    /// <typeparam name="T">Type of the value embedded in the <see cref="Maybe{T}"/>.</typeparam>
    public partial struct Maybe<T> : IEquatable<Maybe<T>>
    {
        /// <summary>
        /// Nothing value.
        /// </summary>
        public static readonly Maybe<T> None = new Maybe<T>();

        private readonly bool _hasValue;

        /// <summary>
        /// Flag that indicate if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        /// Flag that indicate if this <see cref="Maybe{T}"/> has no value.
        /// </summary>
        public bool HasNoValue => !HasValue;

        /// <summary>
        /// Maybe value.
        /// </summary>
        private readonly T _value;

        /// <summary>
        /// Get the value stored in the <see cref="Maybe{T}"/> if present otherwise throws.
        /// </summary>
        /// <exception cref="InvalidOperationException"> if no value is present.</exception>
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
        /// <param name="hasValue">Flag that indicate if this <see cref="Maybe{T}"/> has a value.</param>
        /// <param name="value"><see cref="Maybe{T}"/> value.</param>
        private Maybe(T value)
        {
            _hasValue = true;
            _value = value;
        }

        /// <summary>
        /// Construct a <see cref="Maybe{T}"/> with a value.
        /// </summary>
        public static Maybe<T> Some([NotNull] T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize a Maybe with a null value.");

            return new Maybe<T>(value);
        }

        #region Equality / IEquatable

        public bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) && _hasValue.Equals(other._hasValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Maybe<T> && Equals((Maybe<T>)obj);
        }

        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ _hasValue.GetHashCode();
        }

        public static bool operator==(Maybe<T> maybe1, Maybe<T> maybe2)
        {
            return maybe1.Equals(maybe2);
        }

        public static bool operator!=(Maybe<T> maybe1, Maybe<T> maybe2)
        {
            return !maybe1.Equals(maybe2);
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