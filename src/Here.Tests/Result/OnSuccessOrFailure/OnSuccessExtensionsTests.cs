using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> extensions (On success or failure).
    /// </summary>
    [TestFixture]
    internal class OnSuccessOrFailureExtensionsTests : ResultTestsBase
    {
        #region OnSuccessOrFailure Result

        [Test]
        public void ResultOnSuccessOrFailureWithoutParamToResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result res = result.OnSuccessOrFailure(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    () =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok();
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ResultOnSuccessOrFailureWithFailureParamToResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result res = result.OnSuccessOrFailure(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    r =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok();
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ResultOnSuccessOrFailureWithParamToResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                    },
                    r =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok();
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ResultOnSuccessOrFailureToValue()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result result, bool treatWarningAsError, float expectedValue, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                float res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                        return 1.0f;
                    },
                    r =>
                    {
                        ++counterFailure;
                        return -1.0f;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(expectedValue, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok();
            CheckOnSuccessOrFailure(ok, false, 1.0f, true);
            CheckOnSuccessOrFailure(ok, true, 1.0f, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnSuccessOrFailure(warning, false, 1.0f, true);
            CheckOnSuccessOrFailure(warning, true, -1.0f, false);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnSuccessOrFailure(failure, false, -1.0f, false);
            CheckOnSuccessOrFailure(failure, true, -1.0f, false);
        }

        #endregion

        #region OnSuccessOrFailure Result<T>

        [Test]
        public void ValueResultOnSuccessOrFailureWithoutParamToValueResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result<int> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result<int> res = result.OnSuccessOrFailure(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    () =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(1);
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn(2, "My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ValueResultOnSuccessOrFailureWithParamToValueResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result<int> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result<int> res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                    },
                    r =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(1);
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn(2, "My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ValueResultOnSuccessOrFailureToValue()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result<int> result, bool treatWarningAsError, float expectedValue, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                float res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                        return r.Value + 1.0f;
                    },
                    r =>
                    {
                        ++counterFailure;
                        return -1.0f;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(expectedValue, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(1);
            CheckOnSuccessOrFailure(ok, false, 2.0f, true);
            CheckOnSuccessOrFailure(ok, true, 2.0f, true);

            // Warning result
            var warning = Result.Warn(2, "My warning");
            CheckOnSuccessOrFailure(warning, false, 3.0f, true);
            CheckOnSuccessOrFailure(warning, true, -1.0f, false);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnSuccessOrFailure(failure, false, -1.0f, false);
            CheckOnSuccessOrFailure(failure, true, -1.0f, false);
        }

        #endregion

        #region OnSuccessOrFailure CustomResult<TError>

        [Test]
        public void CustomResultOnSuccessOrFailureWithoutParamToCustomResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                CustomResult<CustomErrorTest> res = result.OnSuccessOrFailure(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    () =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -1 });
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void CustomResultOnSuccessOrFailureWithFailureParamToCustomResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                CustomResult<CustomErrorTest> res = result.OnSuccessOrFailure(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    r =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -1 });
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void CustomResultOnSuccessOrFailureWithParamToCustomResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                CustomResult<CustomErrorTest> res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                    },
                    r =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -1 });
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void CustomResultOnSuccessOrFailureToValue()
        {
            #region Local function

            void CheckOnSuccessOrFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, float expectedValue, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                float res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                        return 1.0f;
                    },
                    r =>
                    {
                        ++counterFailure;
                        return -1.0f;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(expectedValue, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnSuccessOrFailure(ok, false, 1.0f, true);
            CheckOnSuccessOrFailure(ok, true, 1.0f, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnSuccessOrFailure(warning, false, 1.0f, true);
            CheckOnSuccessOrFailure(warning, true, -1.0f, false);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -1 });
            CheckOnSuccessOrFailure(failure, false, -1.0f, false);
            CheckOnSuccessOrFailure(failure, true, -1.0f, false);
        }

        #endregion

        #region OnSuccessOrFailure Result<T, TError>

        [Test]
        public void ValueCustomResultOnSuccessOrFailureWithoutParamToValueCustomResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result<int, CustomErrorTest> res = result.OnSuccessOrFailure(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    () =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(1);
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(2, "My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -2 });
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ValueCustomResultOnSuccessOrFailureWithParamToValueCustomResult()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                Result<int, CustomErrorTest> res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                    },
                    r =>
                    {
                        ++counterFailure;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(1);
            CheckOnSuccessOrFailure(ok, false, true);
            CheckOnSuccessOrFailure(ok, true, true);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(2, "My warning");
            CheckOnSuccessOrFailure(warning, false, true);
            CheckOnSuccessOrFailure(warning, true, false);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -2 });
            CheckOnSuccessOrFailure(failure, false, false);
            CheckOnSuccessOrFailure(failure, true, false);
        }

        [Test]
        public void ValueCustomResultOnSuccessOrFailureToValue()
        {
            #region Local function

            void CheckOnSuccessOrFailure(Result<int, CustomErrorTest> result, bool treatWarningAsError, float expectedValue, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFailure = 0;
                float res = result.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterSuccess;
                        return r.Value + 1.0f;
                    },
                    r =>
                    {
                        ++counterFailure;
                        return -1.0f;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFailure);
                Assert.AreEqual(expectedValue, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(1);
            CheckOnSuccessOrFailure(ok, false, 2.0f, true);
            CheckOnSuccessOrFailure(ok, true, 2.0f, true);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(2, "My warning");
            CheckOnSuccessOrFailure(warning, false, 3.0f, true);
            CheckOnSuccessOrFailure(warning, true, -1.0f, false);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -2 });
            CheckOnSuccessOrFailure(failure, false, -1.0f, false);
            CheckOnSuccessOrFailure(failure, true, -1.0f, false);
        }

        #endregion
    }
}
