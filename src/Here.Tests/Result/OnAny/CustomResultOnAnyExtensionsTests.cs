using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="CustomResult{TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class CustomResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void CustomResultOnAnyToCustomResult()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -8 };
            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();

            var result = ok.OnAny(() => ++counter);
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.OnAny(() => ++counter);
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, "My warning");

            // Failure result
            var failure = Result.CustomFail("My failure", customErrorObject);

            result = failure.OnAny(() => ++counter);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void CustomResultOnAnyToCustomResultWithParam()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -12 };
            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();

            var result = ok.OnAny(res => { ++counter; });
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.OnAny(res => { ++counter; });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, "My warning");

            // Failure result
            var failure = Result.CustomFail("My failure", customErrorObject);

            result = failure.OnAny(res => { ++counter; });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void CustomResultOnAnyTOut()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -15 };
            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();

            float result = ok.OnAny(
                res => 
                {
                    ++counter;
                    return 12.2f;
                });
            Assert.AreEqual(1, counter);
            Assert.AreEqual(12.2f, result);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.OnAny(
                res => 
                {
                    ++counter;
                    return 42.2f;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(42.2f, result);

            // Failure result
            var failure = Result.CustomFail("My failure", customErrorObject);

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
