using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Here.ValueObjects
{
    /// <summary>
    /// Base class for Value object which is an object that represents something whose equality is not based on identity but on value.
    /// Meaning two objects are equal when they have the same value and not necessarily being the same object.
    /// This object should be immutable.
    /// </summary>
    [PublicAPI]
    public abstract class ValueObject
    {
        /// <summary>
        /// Gets elements to take into account when comparing this <see cref="ValueObject"/>.
        /// </summary>
        /// <returns>Elements to compare.</returns>
        [Pure, NotNull]
        protected abstract IEnumerable<object> GetEqualityElements();

        /// <summary>
        /// Indicates whether both <see cref="ValueObject"/> are equal.
        /// </summary>
        /// <param name="object1">First <see cref="ValueObject"/> to compare.</param>
        /// <param name="object2">Second <see cref="ValueObject"/> to compare.</param>
        /// <returns>True if both <see cref="ValueObject"/> are equal, otherwise false.</returns>
        protected static bool EqualOperator(in ValueObject object1, in ValueObject object2)
        {
            if (object1 is null)
                return object2 is null;
            if (object2 is null)
                return false;
            return ReferenceEquals(object1, object2) || object1.Equals(object2);
        }

        /// <summary>
        /// Indicates whether both <see cref="ValueObject"/> are not equal.
        /// </summary>
        /// <param name="object1">First <see cref="ValueObject"/> to compare.</param>
        /// <param name="object2">Second <see cref="ValueObject"/> to compare.</param>
        /// <returns>True if both <see cref="ValueObject"/> are not equal, otherwise false.</returns>
        protected static bool NotEqualOperator(in ValueObject object1, in ValueObject object2)
        {
            return !EqualOperator(object1, object2);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null || GetType() != obj.GetType())
                return false;

            var valueObject = (ValueObject)obj;
            return GetEqualityElements().SequenceEqual(valueObject.GetEqualityElements());
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return GetEqualityElements()
                .Select(elem => elem != null ? elem.GetHashCode() : 0)
                .Aggregate((cur, next) => cur ^ next);
        }
    }
}