using NUnit.Framework;
using Here.Maybes;
using Here.Results;
using Here.Tests.Results;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> conversions.
    /// </summary>
    [TestFixture]
    internal class MaybeConversionsTests : ResultTestsBase
    {
        [Test]
        public void MaybeToResult()
        {
            // Maybe has value
            // Explicit conversion
            var maybeInt = Maybe<int>.Some(42);
            Result result = maybeInt.ToResult();
            CheckResultOk(result);

            result = maybeInt.ToResult("Empty maybeInt");
            CheckResultOk(result);

            // Implicit conversion
            result = maybeInt;
            CheckResultOk(result);

            // Empty Maybe
            // Explicit conversion
            var emptyMaybeInt = Maybe<int>.None;
            result = emptyMaybeInt.ToResult();
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)));

            result = emptyMaybeInt.ToResult("Empty emptyMaybeInt");
            CheckResultFail(result, "Empty emptyMaybeInt");

            // Implicit conversion
            result = emptyMaybeInt;
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)));
        }

        [Test]
        public void MaybeToValueResult()
        {
            // Maybe has value
            // Explicit conversion
            var maybeInt = Maybe<int>.Some(42);
            Result<int> result = maybeInt.ToValueResult();
            CheckResultOk(result, 42);

            result = maybeInt.ToValueResult("Empty maybeInt");
            CheckResultOk(result, 42);

            // Implicit conversion
            result = maybeInt;
            CheckResultOk(result, 42);

            // Empty Maybe
            // Explicit conversion
            var emptyMaybeInt = Maybe<int>.None;
            result = emptyMaybeInt.ToValueResult();
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)));

            result = emptyMaybeInt.ToValueResult("Empty emptyMaybeInt");
            CheckResultFail(result, "Empty emptyMaybeInt");

            // Implicit conversion
            result = emptyMaybeInt;
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)));
        }

        [Test]
        public void MaybeToCustomResult()
        {
            // Maybe has value
            var customErrorObject = new CustomErrorTest { ErrorCode = -32 };
            var maybeInt = Maybe<int>.Some(42);
            CustomResult<CustomErrorTest> result = maybeInt.ToCustomResult(customErrorObject);
            CheckResultOk(result);

            result = maybeInt.ToCustomResult(customErrorObject, "Empty maybeInt");
            CheckResultOk(result);

            result = maybeInt.ToCustomResult(() => customErrorObject);
            CheckResultOk(result);

            result = maybeInt.ToCustomResult(() => customErrorObject, "Empty maybeInt");
            CheckResultOk(result);

            // Empty Maybe
            var emptyMaybeInt = Maybe<int>.None;
            result = emptyMaybeInt.ToCustomResult(customErrorObject);
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyMaybeInt.ToCustomResult(customErrorObject, "Empty emptyMaybeInt");
            CheckResultFail(result, "Empty emptyMaybeInt", customErrorObject);

            result = emptyMaybeInt.ToCustomResult(() => customErrorObject);
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyMaybeInt.ToCustomResult(() => customErrorObject, "Empty emptyMaybeInt");
            CheckResultFail(result, "Empty emptyMaybeInt", customErrorObject);
        }

        [Test]
        public void MaybeToValueCustomResult()
        {
            // Maybe has value
            var customErrorObject = new CustomErrorTest { ErrorCode = -32 };
            var maybeInt = Maybe<int>.Some(42);
            Result<int, CustomErrorTest> result = maybeInt.ToValueCustomResult(customErrorObject);
            CheckResultOk(result, 42);

            result = maybeInt.ToValueCustomResult(customErrorObject, "Empty maybeInt");
            CheckResultOk(result, 42);

            result = maybeInt.ToValueCustomResult(() => customErrorObject);
            CheckResultOk(result, 42);

            result = maybeInt.ToValueCustomResult(() => customErrorObject, "Empty maybeInt");
            CheckResultOk(result, 42);

            // Empty Maybe
            var emptyMaybeInt = Maybe<int>.None;
            result = emptyMaybeInt.ToValueCustomResult(customErrorObject);
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyMaybeInt.ToValueCustomResult(customErrorObject, "Empty emptyMaybeInt");
            CheckResultFail(result, "Empty emptyMaybeInt", customErrorObject);

            result = emptyMaybeInt.ToValueCustomResult(() => customErrorObject);
            CheckResultFail(result, string.Format(Maybe.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyMaybeInt.ToValueCustomResult(() => customErrorObject, "Empty emptyMaybeInt");
            CheckResultFail(result, "Empty emptyMaybeInt", customErrorObject);
        }
    }
}