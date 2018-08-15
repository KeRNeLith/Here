using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ValueResultOnAnyToValueResult()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(81);

            var result = ok.OnAny(() => ++counter);
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 81);

            // Warning result
            var warning = Result.Warn(92, "My warning");

            result = warning.OnAny(() => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 92, "My warning");

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnAny(() => ++counter);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ValueResultOnAnyToValueResultWithParam()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(45);

            var result = ok.OnAny(res => { ++counter; });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 45);

            // Warning result
            var warning = Result.Warn(78, "My warning");

            result = warning.OnAny(res => { ++counter; });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 78, "My warning");

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnAny(res => { ++counter; });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ValueResultOnAnyTOut()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(96);

            float result = ok.OnAny(
                res => 
                {
                    ++counter;
                    return 12.2f;
                });
            Assert.AreEqual(1, counter);
            Assert.AreEqual(12.2f, result);

            // Warning result
            var warning = Result.Warn(102, "My warning");

            result = warning.OnAny(
                res => 
                {
                    ++counter;
                    return 42.2f;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(42.2f, result);

            // Failure result
            var failure = Result.Fail<int>("My failure");

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
