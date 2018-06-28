namespace Here.Maybes
{
    // Operators
    public partial struct Maybe<T>
    {
		/// <summary>
        /// Check the <see cref="Maybe{T}"/> state, it matches true if it <see cref="HasValue"/>.
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasValue"/>.</returns>
        public static bool operator true(Maybe<T> maybe) => maybe.HasValue;

        /// <summary>
        /// Check the <see cref="Maybe{T}"/> state, it matches false if it <see cref="HasNoValue"/>.
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        public static bool operator false(Maybe<T> maybe) => maybe.HasNoValue;

        /// <summary>
        /// Check if the <see cref="Maybe{T}"/> state is empty.
        /// It means it <see cref="HasNoValue"/>).
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        public static bool operator !(Maybe<T> maybe) => maybe.HasNoValue;
    }
}
