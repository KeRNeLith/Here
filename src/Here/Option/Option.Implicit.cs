using JetBrains.Annotations;
using Here.Extensions;

namespace Here
{
	// Implicit operators
    public partial struct Option<T>
    {
        /// <summary>
        /// Implicit constructor for an empty <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>A None option.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<T>([CanBeNull] in Option.NoneOption none)
        {
            return None;
        }

        /// <summary>
        /// Implicit constructor of <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="value">Value to initialize the <see cref="Option{T}"/>.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<T>([CanBeNull] in T value)
        {
            return Option.From(value);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Option{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="embeddedOption">A <see cref="Option{T}"/>.</param>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Option<T>(in Option<Option<T>> embeddedOption)
        {
            return embeddedOption.Flatten();
        }

        /// <summary>
        /// Implicit conversion from <see cref="Option{T}"/> to a boolean.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> to convert.</param>
        /// <returns>A corresponding boolean.</returns>
        [PublicAPI, Pure]
        public static implicit operator bool(in Option<T> option)
        {
            return option.HasValue;
        }

        /// <summary>
        /// Implicit conversion from <see cref="Option{T}"/> to an <see cref="Result"/>.
        /// </summary>
        /// <param name="option">An <see cref="Option{T}"/> to convert.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result(in Option<T> option)
        {
            return option.ToResult();
        }

        /// <summary>
        /// Implicit conversion from <see cref="Option{T}"/> to an <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="option">An <see cref="Option{T}"/> to convert.</param>
        /// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result<T>(in Option<T> option)
        {
            return option.ToValueResult();
        }
    }
}
