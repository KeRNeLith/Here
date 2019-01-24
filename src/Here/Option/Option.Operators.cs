using JetBrains.Annotations;

namespace Here
{
    // Operators
    public partial struct Option<T>
    {
        /// <summary>
        /// Checks if the <see cref="Option{T}"/> state is empty.
        /// It means that it <see cref="HasNoValue"/>.
        /// </summary>
        /// <param name="option"><see cref="Option{T}"/> to check.</param>
        /// <returns>True if <see cref="Option{T}"/> <see cref="HasNoValue"/>.</returns>
        [PublicAPI, Pure]
        public static bool operator !(Option<T> option) => option.HasNoValue;

        /// <summary>
        /// Converts this <see cref="Option{T}"/> if it has a value to an <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the converted <see cref="Option{T}"/>.</typeparam>
        /// <returns>Converted <see cref="Option{T}"/>.</returns>
        [PublicAPI, Pure]
        public Option<TOut> Cast<TOut>()
            where TOut : class
        {
            if (HasValue)
                return Value as TOut;
            return Option<TOut>.None;
        }

        /// <summary>
        /// Returns this <see cref="Option{T}"/> as an <see cref="Option{TOut}"/> if it has a value and is of type <typeparamref name="TOut"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the resulting <see cref="Option{TOut}"/>.</typeparam>
        /// <returns>Cast <see cref="Option{TOut}"/>.</returns>
        [PublicAPI, Pure]
        public Option<TOut> OfType<TOut>()
            where TOut : class
        {
            return Cast<TOut>();
        }
    }
}
