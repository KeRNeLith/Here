using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ValueResultOnFailureToValue()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(55);

            var result = ok.OnFailure(
                res =>
                {
                    ++counter;
                    return res.Value + 0.1f;
                },
                -1f);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(-1f, result);

            result = ok.OnFailure(
                res =>
                {
                    ++counter;
                    return res.Value + 0.2f;
                },
                -2f,
                true);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(-2f, result);

            // Warning result
            var warning = Result.Warn(65, "My warning");

            result = warning.OnFailure(
                res =>
                {
                    ++counter;
                    return res.Value + 0.1f;
                },
                -3f);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(-3f, result);

            result = warning.OnFailure(
                res =>
                {
                    ++counter;
                    return res.Value + 0.2f;
                },
                -4f,
                true);
            Assert.AreEqual(1, counter);
            Assert.AreEqual(65.2f, result);

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnFailure(
                res =>
                {
                    ++counter;
                    return -12f;
                },
                -4f);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(-12f, result);

            result = failure.OnFailure(
                res =>
                {
                    ++counter;
                    return -13f;
                },
                -4f,
                true);
            Assert.AreEqual(3, counter);
            Assert.AreEqual(-13f, result);
        }

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
