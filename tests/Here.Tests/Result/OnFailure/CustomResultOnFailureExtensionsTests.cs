using System;
using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Results.ResultTestHelpers;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="CustomResult{TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class CustomResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void CustomResultOnFailureToValue()
        {
            #region Local functions

            void CheckOnFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
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

            void CheckOnFailureFunc(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                int counterFactory = 0;
                float res = result.OnFailure(
                    r =>
                    {
                        ++counterFailure;
                        return 43.5f;
                    },
                    () =>
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
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);
            CheckOnFailureFunc(ok, false, false);
            CheckOnFailureFunc(ok, true, false);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);
            CheckOnFailureFunc(warning, false, false);
            CheckOnFailureFunc(warning, true, true);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest());
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
            CheckOnFailureFunc(failure, false, true);
            CheckOnFailureFunc(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null, 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null, () => 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(r => 1.0f, null));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<CustomResult<CustomErrorTest>, float>)null, null));
        }

        [Test]
        public void CustomResultOnFailureToCustomResult()
        {
            #region Local function

            void CheckOnFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                CustomResult<CustomErrorTest> res = result.OnFailure(() => ++counterFailure, treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Action)null));
        }

        [Test]
        public void CustomResultOnFailureToResult()
        {
            #region Local function

            void CheckOnFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
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
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest());
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<CustomResult<CustomErrorTest>, Result>)null));
        }

        [Test]
        public void CustomResultOnFailureToCustomResultWithParam()
        {
            #region Local function

            void CheckOnFailure(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                CustomResult<CustomErrorTest> res = result.OnFailure(r => ++counterFailure, treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(result, res);
            }

            void CheckOnFailureFunc(CustomResult<CustomErrorTest> result, bool treatWarningAsError, bool expectFailure)
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
                    Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);
            CheckOnFailureFunc(ok, false, false);
            CheckOnFailureFunc(ok, true, false);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);
            CheckOnFailureFunc(warning, false, false);
            CheckOnFailureFunc(warning, true, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
            CheckOnFailureFunc(failure, false, true);
            CheckOnFailureFunc(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Action<CustomResult<CustomErrorTest>>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null));
        }
    }
}