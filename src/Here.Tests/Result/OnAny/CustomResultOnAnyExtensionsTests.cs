﻿using NUnit.Framework;
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
            #region Local function

            void CheckOnAny(CustomResult<CustomErrorTest> result, bool treatWarningAsError)
            {
                int counter = 0;
                CustomResult<CustomErrorTest> res = result.OnAny(() => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -8 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
        }

        [Test]
        public void CustomResultOnAnyToCustomResultWithParam()
        {
            #region Local function

            void CheckOnAny(CustomResult<CustomErrorTest> result, bool treatWarningAsError)
            {
                int counter = 0;
                CustomResult<CustomErrorTest> res = result.OnAny(r => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -12 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
        }

        [Test]
        public void CustomResultOnAnyTOut()
        {
            #region Local functions

            void CheckOnAny(CustomResult<CustomErrorTest> result, bool treatWarningAsError)
            {
                int counter = 0;
                float res = result.OnAny(r =>
                {
                    ++counter;
                    return 12.5f;
                });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(12.5f, res);
            }

            void CheckOnAnyNoInput(CustomResult<CustomErrorTest> result, bool treatWarningAsError)
            {
                int counter = 0;
                float res = result.OnAny(() =>
                {
                    ++counter;
                    return 13.5f;
                });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(13.5f, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);
            CheckOnAnyNoInput(ok, false);
            CheckOnAnyNoInput(ok, true);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);
            CheckOnAnyNoInput(warning, false);
            CheckOnAnyNoInput(warning, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -15 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
            CheckOnAnyNoInput(failure, false);
            CheckOnAnyNoInput(failure, true);
        }
    }
}
