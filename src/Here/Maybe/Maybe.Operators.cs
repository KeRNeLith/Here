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

        /// <summary>
        /// Perform the bitwise OR of given operands.
        /// </summary>
        /// <param name="leftOperand">First <see cref="Maybe{T}"/> operand.</param>
        /// <param name="rightOperand">Second <see cref="Maybe{T}"/> operand.</param>
        /// <returns>The first operand that <see cref="HasValue"/>, otherwise <see cref="None"/>.</returns>
        public static Maybe<T> operator |(Maybe<T> leftOperand, Maybe<T> rightOperand)
        {
            if (leftOperand.HasValue)
                return leftOperand;
            return rightOperand;
        }

        /// <summary>
        /// Perform the bitwise AND of given operands.
        /// </summary>
        /// <param name="leftOperand">First <see cref="Maybe{T}"/> operand.</param>
        /// <param name="rightOperand">Second <see cref="Maybe{T}"/> operand.</param>
        /// <returns>The last operand that <see cref="HasValue"/>, otherwise <see cref="None"/>.</returns>
        public static Maybe<T> operator &(Maybe<T> leftOperand, Maybe<T> rightOperand)
        {
            if (leftOperand.HasValue)
                return rightOperand;
            return None;
        }
    }
}
