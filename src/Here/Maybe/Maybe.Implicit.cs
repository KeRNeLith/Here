using JetBrains.Annotations;
using Here.Maybes.Extensions;
using Here.Results;

namespace Here.Maybes
{
	// Implicit operators
    public partial struct Maybe<T>
    {
        /// <summary>
        /// Implicit constructor for an empty <see cref="Maybe{T}"/>.
        /// </summary>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>([NotNull] Maybe.NoneClass none)
        {
            return None;
        }

        /// <summary>
        /// Implicit constructor of <see cref="Maybe{T}"/>.
        /// </summary>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>([CanBeNull] T value)
        {
            if (value == null)
                return None;

            return Some(value);
        }

        /// <summary>
        /// Implicit convertion from <see cref="Maybe{Maybe}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="embeddedMaybe">A <see cref="Maybe{Maybe}"/>.</param>
        /// <returns>A <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>(Maybe<Maybe<T>> embeddedMaybe)
        {
            return embeddedMaybe.Flatten();
        }

        /// <summary>
        /// Implicit convertion from <see cref="Maybe{T}"/> to a boolean.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{T}"/> to convert.</param>
        /// <returns>A corresponding boolean.</returns>
        [PublicAPI, Pure]
        public static implicit operator bool(Maybe<T> maybe)
        {
            return maybe.HasValue;
        }

        /// <summary>
        /// Implicit convertion from <see cref="Maybe{T}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="maybe">A <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result(Maybe<T> maybe)
        {
            return maybe.ToResult();
        }

        /// <summary>
        /// Implicit convertion from <see cref="Maybe{T}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="maybe">A <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result<T>(Maybe<T> maybe)
        {
            return maybe.ToValueResult();
        }
    }
}
