using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ResultOnFailureToResult()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.OnFailure(() => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultOk(result);

            result = ok.OnFailure(() => ++counter, true);
            Assert.AreEqual(0, counter);
            CheckResultOk(result);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnFailure(() => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultWarn(result, "My warning");

            result = warning.OnFailure(() => ++counter, true);
            Assert.AreEqual(1, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.OnFailure(() => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnFailure(() => ++counter, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ResultOnFailureToResultWithParam()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.OnFailure(res => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultOk(result);

            result = ok.OnFailure(res => ++counter, true);
            Assert.AreEqual(0, counter);
            CheckResultOk(result);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnFailure(res => ++counter);
            Assert.AreEqual(0, counter);
            CheckResultWarn(result, "My warning");

            result = warning.OnFailure(res => ++counter, true);
            Assert.AreEqual(1, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.OnFailure(res => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnFailure(res => ++counter, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }
    }
}
