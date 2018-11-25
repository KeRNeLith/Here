using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ResultOnAnyToResult()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.OnAny(() => ++counter);
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnAny(() => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, "My warning");

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.OnAny(() => ++counter);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ResultOnAnyToResultWithParam()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.OnAny(res => { ++counter; });
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnAny(res => { ++counter; });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, "My warning");

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.OnAny(res => { ++counter; });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ResultOnAnyTOut()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            float result = ok.OnAny(
                res => 
                {
                    ++counter;
                    return 12.2f;
                });
            Assert.AreEqual(1, counter);
            Assert.AreEqual(12.2f, result);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnAny(
                res => 
                {
                    ++counter;
                    return 42.2f;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(42.2f, result);

            // Failure result
            var failure = Result.Fail("My failure");

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
