using JetBrains.Annotations;

namespace Here.Maybes
{
	// Implicit operators
    public partial struct Maybe<T>
    {
        /// <summary>
        /// Implicit constructor for an empty <see cref="Maybe{T}"/>.
        /// </summary>
        public static implicit operator Maybe<T>([NotNull] Maybe.NoneClass none)
        {
            return None;
        }

        /// <summary>
        /// Implicit convertion from <see cref="Maybe{Maybe{T}}"/> to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="embeddedMaybe">A <see cref="Maybe{T}"/> that encapsulate another <see cref="Maybe{T}"/>.</param>
        /// <returns>The simple corresponding <see cref="Maybe{T}"/>.</returns>
        public static implicit operator Maybe<T>([NotNull] Maybe<Maybe<T>> embeddedMaybe)
        {
            return embeddedMaybe.HasValue ? embeddedMaybe.Value : None;
        }
    }
}
