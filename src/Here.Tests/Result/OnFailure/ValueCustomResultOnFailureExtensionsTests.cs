using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T, TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueCustomResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void CustomValueResultOnFailureToCustomValueResult()
        {
            int counterFailure = 0;
            int counterFailureFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -4 };

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(24);

            var result = ok.OnFailure(() => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result, 24);

            result = ok.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 24);

            result = ok.OnFailure(() => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result, 24);

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
            CheckResultOk(result, 24);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(14, "My warning");

            result = warning.OnFailure(() => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultWarn(result, 14, "My warning");

            result = warning.OnFailure(
                () => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 14, "My warning");

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
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

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
        public void CustomValueResultOnFailureToCustomValueResultWithParam()
        {
            int counterFailure = 0;
            int counterFailureFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -4 };

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(66);

            var result = ok.OnFailure(res => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result, 66);

            result = ok.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 66);

            result = ok.OnFailure(res => ++counterFailure, customErrorObjectFactory, true);
            Assert.AreEqual(0, counterFailure);
            CheckResultOk(result, 66);

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
            CheckResultOk(result, 66);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(55, "My warning");

            result = warning.OnFailure(res => ++counterFailure, customErrorObjectFactory);
            Assert.AreEqual(0, counterFailure);
            CheckResultWarn(result, 55, "My warning");

            result = warning.OnFailure(
                res => ++counterFailure,
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterFailure);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 55, "My warning");

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
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

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
