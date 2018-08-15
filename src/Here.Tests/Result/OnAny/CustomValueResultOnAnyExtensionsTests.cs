using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T, TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class CustomValueResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void CustomValueResultOnAnyToCustomValueResult()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -7 };
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(25);

            var result = ok.OnAny(() => ++counter);
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 25);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");

            result = warning.OnAny(() => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 42, "My warning");

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.OnAny(() => ++counter);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void CustomValueResultOnAnyToCustomValueResultWithParam()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(14);

            var result = ok.OnAny(res => { ++counter; });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 14);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(78, "My warning");

            result = warning.OnAny(res => { ++counter; });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 78, "My warning");

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.OnAny(res => { ++counter; });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void CustomValueResultOnAnyTOut()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -28 };
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(46);

            float result = ok.OnAny(
                res => 
                {
                    ++counter;
                    return 12.2f;
                });
            Assert.AreEqual(1, counter);
            Assert.AreEqual(12.2f, result);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(96, "My warning");

            result = warning.OnAny(
                res => 
                {
                    ++counter;
                    return 42.2f;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(42.2f, result);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.OnAny(
                res => 
                {
                    ++counter;
                    return 62.2f;
                });
            Assert.AreEqual(3, counter);
            Assert.AreEqual(62.2f, result);
        }
    }
}
