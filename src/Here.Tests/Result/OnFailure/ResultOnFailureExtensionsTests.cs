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
        public void ResultOnFailureToValue()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.OnFailure(
                res =>
                {
                    ++counter;
                    return 42.5f;
                },
                -1f);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(-1f, result);

            result = ok.OnFailure(
                res =>
                {
                    ++counter;
                    return 43.5f;
                },
                -2f,
                true);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(-2f, result);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnFailure(
                res =>
                {
                    ++counter;
                    return 44.5f;
                },
                -3f);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(-3f, result);

            result = warning.OnFailure(
                res =>
                {
                    ++counter;
                    return 45.5f;
                },
                -4f,
                true);
            Assert.AreEqual(1, counter);
            Assert.AreEqual(45.5f, result);

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.OnFailure(
                res =>
                {
                    ++counter;
                    return 46.5f;
                },
                -4f);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(46.5f, result);

            result = failure.OnFailure(
                res =>
                {
                    ++counter;
                    return 47.5f;
                },
                -4f,
                true);
            Assert.AreEqual(3, counter);
            Assert.AreEqual(47.5f, result);
        }

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
