using System;
using NUnit.Framework;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Either{TLeft,TRight}"/> conversions to results.
    /// </summary>
    [TestFixture]
    internal class EitherConversionsTests : ResultTestsBase
    {
        [Test]
        public void EitherToResult()
        {
            // Either left
            var eitherLeft = Either<string, int>.Left("Error");
            Result result = eitherLeft.ToResult();
            CheckResultFail(result, ResultConstants.EitherToFailedResult);

            // Either right
            var eitherRight = Either<string, int>.Right(42);
            result = eitherRight.ToResult();
            CheckResultOk(result);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.ToResult();
            CheckResultFail(result, ResultConstants.EitherToFailedResult);
        }

        [Test]
        public void EitherToValueResult()
        {
            // Either left
            var eitherLeft = Either<string, int>.Left("Error");
            Result<int> result = eitherLeft.ToValueResult();
            CheckResultFail(result, ResultConstants.EitherToFailedResult);

            // Either right
            var eitherRight = Either<string, int>.Right(42);
            result = eitherRight.ToValueResult();
            CheckResultOk(result, 42);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.ToValueResult();
            CheckResultFail(result, ResultConstants.EitherToFailedResult);
        }

        [Test]
        public void EitherToCustomResult()
        {
            // Either left
            var eitherLeft = Either<string, int>.Left("Error");
            CustomResult<string> result = eitherLeft.ToCustomResult();
            CheckResultFail(result, ResultConstants.EitherToFailedResult);

            // Either right
            var eitherRight = Either<string, int>.Right(42);
            result = eitherRight.ToCustomResult();
            CheckResultOk(result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(() => eitherNone.ToCustomResult());
        }

        [Test]
        public void EitherToValueCustomResult()
        {
            // Either left
            var eitherLeft = Either<string, int>.Left("Error");
            Result<int, string> result = eitherLeft.ToValueCustomResult();
            CheckResultFail(result, ResultConstants.EitherToFailedResult, "Error");

            // Either right
            var eitherRight = Either<string, int>.Right(42);
            result = eitherRight.ToValueCustomResult();
            CheckResultOk(result, 42);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(() => eitherNone.ToValueCustomResult());
        }
    }
}