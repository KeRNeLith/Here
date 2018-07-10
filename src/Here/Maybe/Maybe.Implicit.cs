using JetBrains.Annotations;

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
        /// Implicit convertion from <see cref="Maybe{U}"/> (where U is a <see cref="Maybe{T}"/>) to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="embeddedMaybe">A <see cref="Maybe{T}"/> that encapsulate another <see cref="Maybe{T}"/>.</param>
        /// <returns>The simple corresponding <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>(Maybe<Maybe<T>> embeddedMaybe)
        {
            return embeddedMaybe.HasValue ? embeddedMaybe.Value : None;
        }
    }
}
