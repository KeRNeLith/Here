using System;
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// Either construction helpers.
    /// </summary>
    [PublicAPI]
    public static class Either
    {
        #region Left construction

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="value">A value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> Left<TLeft, TRight>([NotNull] in TLeft value)
        {
            return Either<TLeft, TRight>.Left(value);
        }

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="nullable">A nullable value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> Left<TLeft, TRight>([NotNull] in TLeft? nullable)
            where TLeft : struct
        {
            if (nullable == null)
                throw new ArgumentNullException(nameof(nullable), "Cannot initialize an Either<TLeft, TRight> with a null nullable value.");
            return Either<TLeft, TRight>.Left(nullable.Value);
        }

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="value">A value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>([NotNull] this TLeft value)
        {
            return Left<TLeft, TRight>(value);
        }

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Left"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="nullable">A nullable value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>([NotNull] in this TLeft? nullable)
            where TLeft : struct
        {
            return Left<TLeft, TRight>(nullable);
        }

        /// <summary>
        /// Creates an instance of <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="value">A value.</param>
        /// <returns>An <see cref="EitherLeft{TLeft}"/>.</returns>
        [PublicAPI, Pure]
        public static EitherLeft<TLeft> Left<TLeft>([NotNull] in TLeft value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an EitherLeft with a null value.");
            return new EitherLeft<TLeft>(value);
        }

        /// <summary>
        /// Creates an instance of <see cref="EitherLeft{TLeft}"/>.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="nullable">A nullable value.</param>
        /// <returns>An <see cref="EitherLeft{TLeft}"/>.</returns>
        [PublicAPI, Pure]
        public static EitherLeft<TLeft> Left<TLeft>([NotNull] in TLeft? nullable)
            where TLeft : struct
        {
            if (nullable == null)
                throw new ArgumentNullException(nameof(nullable), "Cannot initialize an EitherLeft with a null nullable value.");
            return new EitherLeft<TLeft>(nullable.Value);
        }

        #endregion

        #region Right construction

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="value">A value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> Right<TLeft, TRight>([NotNull] in TRight value)
        {
            return Either<TLeft, TRight>.Right(value);
        }

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="nullable">A nullable value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> Right<TLeft, TRight>([NotNull] in TRight? nullable)
            where TRight : struct
        {
            if (nullable == null)
                throw new ArgumentNullException(nameof(nullable), "Cannot initialize an Either<TLeft, TRight> with a null nullable value.");
            return Either<TLeft, TRight>.Right(nullable.Value);
        }

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="value">A value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>([NotNull] this TRight value)
        {
            return Right<TLeft, TRight>(value);
        }

        /// <summary>
        /// Creates an instance of <see cref="Either{TLeft, TRight}"/> in <see cref="EitherStates.Right"/> state.
        /// </summary>
        /// <typeparam name="TLeft">Type of the value embedded as left value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="nullable">A nullable value.</param>
        /// <returns>An <see cref="Either{TLeft, TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static Either<TLeft, TRight> ToEither<TLeft, TRight>([NotNull] in this TRight? nullable)
            where TRight : struct
        {
            return Right<TLeft, TRight>(nullable);
        }

        /// <summary>
        /// Creates an instance of <see cref="EitherRight{TRight}"/>.
        /// </summary>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="value">A value.</param>
        /// <returns>An <see cref="EitherRight{TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static EitherRight<TRight> Right<TRight>([NotNull] in TRight value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Cannot initialize an EitherRight with a null value.");
            return new EitherRight<TRight>(value);
        }

        /// <summary>
        /// Creates an instance of <see cref="EitherRight{TRight}"/>.
        /// </summary>
        /// <typeparam name="TRight">Type of the value embedded as right value in the <see cref="Either{TLeft, TRight}"/>.</typeparam>
        /// <param name="nullable">A nullable value.</param>
        /// <returns>An <see cref="EitherRight{TRight}"/>.</returns>
        [PublicAPI, Pure]
        public static EitherRight<TRight> Right<TRight>([NotNull] in TRight? nullable)
            where TRight : struct
        {
            if (nullable == null)
                throw new ArgumentNullException(nameof(nullable), "Cannot initialize an EitherRight with a null nullable value.");
            return new EitherRight<TRight>(nullable.Value);
        }

        #endregion
    }
}