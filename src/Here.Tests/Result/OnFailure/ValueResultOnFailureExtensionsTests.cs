using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ValueResultOnFailureToValueResult()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(42);

            var result = ok.OnFailure(() => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultOk(result, 42);

            result = ok.OnFailure(() => ++counter, true);
            Assert.AreEqual(0, counter);
            CheckResultOk(result, 42);

            // Warning result
            var warning = Result.Warn(25, "My warning");

            result = warning.OnFailure(() => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultWarn(result, 25, "My warning");

            result = warning.OnFailure(() => ++counter, true);
            Assert.AreEqual(1, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnFailure(() => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnFailure(() => ++counter, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ValueResultOnFailureToValueResultWithParam()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(12);

            var result = ok.OnFailure(res => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultOk(result, 12);

            result = ok.OnFailure(res => ++counter, true);
            Assert.AreEqual(0, counter);
            CheckResultOk(result, 12);

            // Warning result
            var warning = Result.Warn(32, "My warning");

            result = warning.OnFailure(res => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultWarn(result, 32, "My warning");

            result = warning.OnFailure(res => ++counter, true);
            Assert.AreEqual(1, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnFailure(res => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnFailure(res => ++counter, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }
    }
}
