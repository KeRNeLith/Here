using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Here.Tests.Eithers
{
    /// <summary>
    /// Helpers to test <see cref="Either{TLeft,TRight}"/>, <see cref="EitherLeft{TLeft}"/>, <see cref="EitherRight{TRight}"/>.
    /// </summary>
    internal class EitherTestHelpers
    {
        // Methods to check either

        #region Check Either<TLeft, TRight>

        public static void CheckLeftEither<TLeft, TRight>(Either<TLeft, TRight> either, [NotNull] TLeft expectedValue)
        {
            Assert.IsFalse(either.IsNone);
            Assert.IsTrue(either.IsLeft);
            Assert.IsFalse(either.IsRight);
            Assert.AreEqual(expectedValue, either.LeftValue);
            Assert.Throws<InvalidOperationException>(() => { var _ = either.RightValue; });
        }

        public static void CheckRightEither<TLeft, TRight>(Either<TLeft, TRight> either, [NotNull] TRight expectedValue)
        {
            Assert.IsFalse(either.IsNone);
            Assert.IsFalse(either.IsLeft);
            Assert.IsTrue(either.IsRight);
            Assert.Throws<InvalidOperationException>(() => { var _ = either.LeftValue; });
            Assert.AreEqual(expectedValue, either.RightValue);
        }

        public static void CheckNoneEither<TLeft, TRight>(Either<TLeft, TRight> either)
        {
            Assert.IsTrue(either.IsNone);
            Assert.IsFalse(either.IsLeft);
            Assert.IsFalse(either.IsRight);
            Assert.Throws<InvalidOperationException>(() => { var _ = either.LeftValue; });
            Assert.Throws<InvalidOperationException>(() => { var _ = either.RightValue; });
        }

        #endregion

        #region Check EitherLeft

        public static void CheckEitherLeft<TLeft>(EitherLeft<TLeft> either, [NotNull] TLeft expectedValue)
        {
            Assert.IsTrue(either.IsLeft);
            Assert.IsFalse(either.IsRight);

            CheckLeftEither<TLeft, int>(either, expectedValue); // Use int as fallback for right value, but it's not relevant
        }

        #endregion

        #region Check EitherRight

        public static void CheckEitherRight<TRight>(EitherRight<TRight> either, [NotNull] TRight expectedValue)
        {
            Assert.IsFalse(either.IsLeft);
            Assert.IsTrue(either.IsRight);
            Assert.AreEqual(expectedValue, either.Value);
        }

        #endregion
    }
}