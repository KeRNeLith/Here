using System;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T, TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueCustomResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ValueCustomResultOnAnyToValueCustomResult()
        {
            #region Local function

            void CheckOnAny(Result<int, CustomErrorTest> result)
            {
                int counter = 0;
                Result<int, CustomErrorTest> res = result.OnAny(() => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(25);
            CheckOnAny(ok);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            CheckOnAny(warning);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -7 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnAny(failure);

            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Action)null));
        }

        [Test]
        public void ValueCustomResultOnAnyToValueCustomResultWithParam()
        {
            #region Local function

            void CheckOnAny(Result<int, CustomErrorTest> result)
            {
                int counter = 0;
                Result<int, CustomErrorTest> res = result.OnAny(r => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(14);
            CheckOnAny(ok);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(78, "My warning");
            CheckOnAny(warning);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnAny(failure);

            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Action<Result<int, CustomErrorTest>>)null));
        }

        [Test]
        public void ValueCustomResultOnAnyTOut()
        {
            #region Local functions

            void CheckOnAny(Result<int, CustomErrorTest> result)
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

            void CheckOnAnyNoInput(Result<int, CustomErrorTest> result)
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
            var ok = Result.Ok<int, CustomErrorTest>(46);
            CheckOnAny(ok);
            CheckOnAnyNoInput(ok);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(96, "My warning");
            CheckOnAny(warning);
            CheckOnAnyNoInput(warning);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -28 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnAny(failure);
            CheckOnAnyNoInput(failure);

            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Func<float>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnAny((Func<Result<int, CustomErrorTest>, float>)null));
        }
    }
}