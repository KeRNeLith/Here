using System;
using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Results.ResultTestHelpers;

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
            #region Local functions

            void CheckOnFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                float res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return 42.5f;
                    },
                    -1f,
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? 42.5f : -1f, res);
            }

            void CheckOnFailureFunc(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                int counterFactory = 0;
                float res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return 43.5f;
                    },
                    val =>
                    {
                        ++counterFactory;
                        return -2f;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? 0 : 1, counterFactory);
                Assert.AreEqual(expectFailure ? 43.5f : -2f, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(55);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);
            CheckOnFailureFunc(ok, false, false);
            CheckOnFailureFunc(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(65, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);
            CheckOnFailureFunc(warning, false, false);
            CheckOnFailureFunc(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
            CheckOnFailureFunc(failure, false, true);
            CheckOnFailureFunc(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null, 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null, v => 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<Result<int, CustomErrorTest>, float>)null, null));
        }

        [Test]
        public void ValueCustomResultOnFailureToResult()
        {
            #region Local function

            void CheckOnFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return Result.Ok();
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                if (expectFailure)
                    CheckResultOk(res);
                else
                    Assert.AreEqual((Result)result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(32, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<Result<int, CustomErrorTest>, Result>)null));
        }

        [Test]
        public void ValueCustomResultOnFailureToValueResult()
        {
            #region Local function

            void CheckOnFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result<int> res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return Result.Ok(42);
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                if (expectFailure)
                    CheckResultOk(res, 42);
                else
                    Assert.AreEqual((Result<int>)result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(32, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<Result<int, CustomErrorTest>, Result<int>>)null));
        }

        [Test]
        public void ValueCustomResultOnFailureToCustomResult()
        {
            #region Local function

            void CheckOnFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                CustomResult<CustomErrorTest> res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return Result.CustomOk<CustomErrorTest>();
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                if (expectFailure)
                    CheckResultOk(res);
                else
                    Assert.AreEqual((CustomResult<CustomErrorTest>)result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(32, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<Result<int, CustomErrorTest>, CustomResult<CustomErrorTest>>)null));
        }

        [Test]
        public void ValueCustomResultOnFailureToValueCustomResult()
        {
            #region Local function

            void CheckOnFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result<int, CustomErrorTest> res = result.OnFailure(() => ++counterFailure, treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(24);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(14, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Action)null));
        }

        [Test]
        public void ValueCustomResultOnFailureToValueCustomResultWithParam()
        {
            #region Local function

            void CheckOnFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result<int, CustomErrorTest> res = result.OnFailure(r => ++counterFailure, treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(result, res);
            }

            void CheckOnFailureFunc(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result<int, CustomErrorTest> res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return Result.Ok<int, CustomErrorTest>(42);
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                if (expectFailure)
                    CheckResultOk(res, 42);
                else
                    Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(66);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);
            CheckOnFailureFunc(ok, false, false);
            CheckOnFailureFunc(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(55, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);
            CheckOnFailureFunc(warning, false, false);
            CheckOnFailureFunc(warning, true, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
            CheckOnFailureFunc(failure, false, true);
            CheckOnFailureFunc(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Action<Result<int, CustomErrorTest>>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null));
        }
    }
}