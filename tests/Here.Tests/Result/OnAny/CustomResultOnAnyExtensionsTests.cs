using System;
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
            #region Local function

            void CheckOnAny(CustomResult<CustomErrorTest> result)
            {
                int counter = 0;
                CustomResult<CustomErrorTest> res = result.OnAny(() => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnAny(ok);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnAny(warning);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -8 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnAny(failure);

            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Action)null));
        }

        [Test]
        public void CustomResultOnAnyToCustomResultWithParam()
        {
            #region Local function

            void CheckOnAny(CustomResult<CustomErrorTest> result)
            {
                int counter = 0;
                CustomResult<CustomErrorTest> res = result.OnAny(r => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            CheckOnAny(ok);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnAny(warning);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -12 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnAny(failure);

            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Action<CustomResult<CustomErrorTest>>)null));
        }

        [Test]
        public void CustomResultOnAnyTOut()
        {
            #region Local functions

            void CheckOnAny(CustomResult<CustomErrorTest> result)
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

            void CheckOnAnyNoInput(CustomResult<CustomErrorTest> result)
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
            CheckOnAny(ok);
            CheckOnAnyNoInput(ok);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            CheckOnAny(warning);
            CheckOnAnyNoInput(warning);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -15 };
            var failure = Result.CustomFail("My failure", customErrorObject);
            CheckOnAny(failure);
            CheckOnAnyNoInput(failure);
            
            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Func<float>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Func<CustomResult<CustomErrorTest>, float>)null));
        }
    }
}
