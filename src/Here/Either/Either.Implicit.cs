using JetBrains.Annotations;

namespace Here
{
    // Implicit operators
    public partial struct Either<TLeft, TRight>
    {
        /// <summary>
        /// Implicit conversion operator from an <see cref="EitherLeft{TLeft}"/> to an <see cref="Either{TLeft,TRight}"/>.
        /// </summary>
        /// <param name="eitherLeft">Left either.</param>
        /// <returns>An <see cref="Either{TLeft,TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Either<TLeft, TRight>(EitherLeft<TLeft> eitherLeft)
        {
            return Left(eitherLeft.Left);
        }

        /// <summary>
        /// Implicit conversion operator from an <see cref="EitherRight{TRight}"/> to an <see cref="Either{TLeft,TRight}"/>.
        /// </summary>
        /// <param name="eitherRight">Right either.</param>
        /// <returns>An <see cref="Either{TLeft,TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Either<TLeft, TRight>(EitherRight<TRight> eitherRight)
        {
            return Right(eitherRight.Value);
        }
    }
}