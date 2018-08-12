using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="CustomResult{TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class CustomResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void CustomResultOnFailureToCustomResult()
        {
            int counterFailure = 0;
            int counterFailureFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -4 };

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();

            var result = ok.OnFailure(() => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result);

            result = ok.OnFailure(
                () => ++counterFailure, 
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            result = ok.OnFailure(() => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result);

            result = ok.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.OnFailure(() => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultWarn(result, "My warning");

            result = warning.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, "My warning");

            result = warning.OnFailure(() => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(1, counterFailure);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            result = warning.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(2, counterFailure);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            // Failure result
            var failure = Result.CustomFail("My failure", customErrorObject);

            result = failure.OnFailure(() => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(3, counterFailure);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterFailure);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnFailure(() => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(5, counterFailure);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterFailure);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);
        }

        [Test]
        public void CustomResultOnFailureToCustomResultWithParam()
        {
            int counterFailure = 0;
            int counterFailureFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -4 };

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();

            var result = ok.OnFailure(res => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result);

            result = ok.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            result = ok.OnFailure(res => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result);

            result = ok.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.OnFailure(res => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultWarn(result, "My warning");

            result = warning.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, "My warning");

            result = warning.OnFailure(res => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(1, counterFailure);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            result = warning.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(2, counterFailure);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            // Failure result
            var failure = Result.CustomFail("My failure", customErrorObject);

            result = failure.OnFailure(res => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(3, counterFailure);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterFailure);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnFailure(res => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(5, counterFailure);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterFailure);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);
        }
    }
}
