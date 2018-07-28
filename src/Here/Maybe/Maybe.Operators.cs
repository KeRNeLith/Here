using JetBrains.Annotations;

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
        [Pure]
        public static bool operator true(Maybe<T> maybe) => maybe.HasValue;

        /// <summary>
        /// Check the <see cref="Maybe{T}"/> state, it matches false if it <see cref="HasNoValue"/>.
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        [Pure]
        public static bool operator false(Maybe<T> maybe) => maybe.HasNoValue;

        /// <summary>
        /// Check if the <see cref="Maybe{T}"/> state is empty.
        /// It means it <see cref="HasNoValue"/>).
        /// </summary>
        /// <param name="maybe">Maybe to check.</param>
        /// <returns>True if <see cref="Maybe{T}"/> <see cref="HasNoValue"/>.</returns>
        [Pure]
        public static bool operator !(Maybe<T> maybe) => maybe.HasNoValue;

        /// <summary>
        /// Perform the bitwise OR of given operands.
        /// </summary>
        /// <param name="leftOperand">First <see cref="Maybe{T}"/> operand.</param>
        /// <param name="rightOperand">Second <see cref="Maybe{T}"/> operand.</param>
        /// <returns>The first operand that <see cref="HasValue"/>, otherwise <see cref="None"/>.</returns>
        [Pure]
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
        [Pure]
        public static Maybe<T> operator &(Maybe<T> leftOperand, Maybe<T> rightOperand)
        {
            if (leftOperand.HasValue)
                return rightOperand;
            return None;
        }
        
        /// <summary>
        /// Convert this <see cref="Maybe{T}"/> if it has a value to a <see cref="Maybe{TOut}"/>.
        /// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the converted <see cref="Maybe{TOut}"/>.</typeparam>
        /// <returns>The conversion of this <see cref="Maybe{T}"/> to <see cref="Maybe{TOut}"/>.</returns>
        [Pure]
        public Maybe<TOut> Cast<TOut>()
            where TOut : class
        {
            if (HasValue)
                return Value as TOut;
            return Maybe<TOut>.None;
        }

        /// <summary>
		/// Returns this <see cref="Maybe{T}"/> as a <see cref="Maybe{TOut}"/> if it has a value and is of type <typeparamref name="TOut"/>.
		/// </summary>
        /// <typeparam name="TOut">Type of the value embedded in the resulting <see cref="Maybe{TOut}"/>.</typeparam>
		/// <returns>This casted as <see cref="Maybe{TOut}"/>.</returns>
        [Pure]
        public Maybe<TOut> OfType<TOut>()
            where TOut : class
        {
            return Cast<TOut>();
        }
    }
}
