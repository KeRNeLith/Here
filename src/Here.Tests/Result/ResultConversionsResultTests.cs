using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> conversions.
    /// </summary>
    [TestFixture]
    internal class ResultConversionsTests : ResultTestsBase
    {
        #region Result Cast

        [Test]
        public void CastResultToValueResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterValueFactory = 0;

            // Result ok
            var ok = Result.Ok();

            var valueResult = ok.Cast(12);
            CheckResultOk(valueResult, 12);

            valueResult = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(valueResult, 45);

            // Result warn
            var warning = Result.Warn("My warning");

            valueResult = warning.Cast(12);
            CheckResultWarn(valueResult, 12, "My warning");

            valueResult = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultWarn(valueResult, 45, "My warning");

            // Result fail
            var failure = Result.Fail("My failure");

            valueResult = failure.Cast(12, customErrorObjectFactory);
            CheckResultFail(valueResult, "My failure");

            valueResult = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                }, 
                customErrorObjectFactory);
            Assert.AreEqual(2, counterValueFactory);
            CheckResultFail(valueResult, "My failure");
        }

        [Test]
        public void CastResultToCustomResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok();

            var customResult = ok.CustomCast(customErrorObjectFactory);
            CheckResultOk(customResult);

            customResult = ok.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(customResult);

            // Result warn
            var warning = Result.Warn("My warning");

            customResult = warning.CustomCast(customErrorObjectFactory);
            CheckResultWarn(customResult, "My warning");

            customResult = warning.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(customResult, "My warning");

            // Result fail
            var failure = Result.Fail("My failure");

            customResult = failure.CustomCast(customErrorObjectFactory);
            CheckResultFail(customResult, "My failure", customErrorObjectFactory);

            customResult = failure.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(customResult, "My failure", customErrorObjectFactory);
        }

        [Test]
        public void CastResultToCustomValueResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterValueFactory = 0;
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok();

            var result = ok.Cast(22, customErrorObjectFactory);
            CheckResultOk(result, 22);

            result = ok.Cast(11,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 11);

            result = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                }, customErrorObjectFactory);
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(result, 33);

            result = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 44;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterValueFactory);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 44);

            // Result warn
            var warning = Result.Warn("My warning");

            result = warning.Cast(22, customErrorObjectFactory);
            CheckResultWarn(result, 22, "My warning");

            result = warning.Cast(11,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 11, "My warning");

            result = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                }, customErrorObjectFactory);
            Assert.AreEqual(3, counterValueFactory);
            CheckResultWarn(result, 33, "My warning");

            result = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 44;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterValueFactory);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 44, "My warning");

            // Result fail
            var failure = Result.Fail("My failure");

            result = failure.Cast(22, customErrorObjectFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(11,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                }, 
                customErrorObjectFactory);
            Assert.AreEqual(4, counterValueFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 44;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterValueFactory);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);
        }

        #endregion
    }
}