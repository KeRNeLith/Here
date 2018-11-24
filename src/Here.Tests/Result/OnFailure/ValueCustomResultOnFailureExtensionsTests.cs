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
        public void ValueCustomResultOnFailureToValue()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(55);

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
            var warning = Result.Warn<int, CustomErrorTest>(65, "My warning");

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
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());

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
        public void ValueCustomResultOnFailureToValueCustomResult()
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
        public void ValueCustomResultOnFailureToValueCustomResultWithParam()
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
