using System;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Type that only have a unique value.
    /// </summary>
    [PublicAPI]
#if SUPPORTS_SERIALIZATION
    [Serializable]
#endif
    public readonly struct Unit : IEquatable<Unit>, IComparable, IComparable<Unit>
    {
        /// <summary>
        /// Unique <see cref="Unit"/> value.
        /// </summary>
        [PublicAPI]
        public static readonly Unit Default;

        /// <summary>
        /// Returns a value other than this <see cref="Unit"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to return.</typeparam>
        /// <param name="value">Value to return.</param>
        /// <returns>A value.</returns>
        [PublicAPI, Pure]
        public T Return<T>([CanBeNull] T value)
        {
            return value;
        }

        /// <summary>
        /// Returns a value other than this <see cref="Unit"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value to return.</typeparam>
        /// <param name="valueFactory">Function that will create a value to return.</param>
        /// <returns>A value.</returns>
        [PublicAPI, Pure]
        public T Return<T>([NotNull, InstantHandle] Func<T> valueFactory)
        {
            return valueFactory();
        }

        #region Equality / IEquatable

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Unit;
        }

        /// <inheritdoc />
        public bool Equals(Unit other)
        {
            return true;
        }

        /// <summary>
        /// Indicates whether both <see cref="Unit"/> are equal (always true).
        /// </summary>
        /// <param name="unit1">First <see cref="Unit"/> to compare.</param>
        /// <param name="unit2">Second <see cref="Unit"/> to compare.</param>
        /// <returns>True.</returns>
        public static bool operator ==(Unit unit1, Unit unit2)
        {
            return true;
        }

        /// <summary>
        /// Indicates whether both <see cref="Unit"/> are not equal (always false).
        /// </summary>
        /// <param name="unit1">First <see cref="Unit"/> to compare.</param>
        /// <param name="unit2">Second <see cref="Unit"/> to compare.</param>
        /// <returns>False.</returns>
        public static bool operator !=(Unit unit1, Unit unit2)
        {
            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 0;
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        /// <remarks><see cref="Unit"/> are always equal to each other.</remarks>
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is Unit)
                return 0;

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with an {typeof(Unit)}.");
        }

        /// <inheritdoc />
        /// <remarks><see cref="Unit"/> are always equal to each other.</remarks>
        public int CompareTo(Unit other)
        {
            return 0;
        }

        /// <summary>
        /// A <see cref="Unit"/> is never "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/> to compare.</param>
        /// <param name="right">The second <see cref="Unit"/> to compare.</param>
        /// <returns>False.</returns>
        public static bool operator <(Unit left, Unit right)
        {
            return false;
        }

        /// <summary>
        /// A <see cref="Unit"/> is always "less" than or equals to the other one (Always equals).
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/> to compare.</param>
        /// <param name="right">The second <see cref="Unit"/> to compare.</param>
        /// <returns>True.</returns>
        public static bool operator <=(Unit left, Unit right)
        {
            return true;
        }

        /// <summary>
        /// A <see cref="Unit"/> is never "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/> to compare.</param>
        /// <param name="right">The second <see cref="Unit"/> to compare.</param>
        /// <returns>False.</returns>
        public static bool operator >(Unit left, Unit right)
        {
            return false;
        }

        /// <summary>
        /// A <see cref="Unit"/> is always "greater" than or equals to the other one (Always equals).
        /// </summary>
        /// <param name="left">The first <see cref="Unit"/> to compare.</param>
        /// <param name="right">The second <see cref="Unit"/> to compare.</param>
        /// <returns>True.</returns>
        public static bool operator >=(Unit left, Unit right)
        {
            return true;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return "()";
        }
    }
}
