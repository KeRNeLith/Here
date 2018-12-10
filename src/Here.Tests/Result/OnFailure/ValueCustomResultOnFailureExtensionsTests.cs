using NUnit.Framework;
using Here.Extensions;

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

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(66);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(55, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
        }
    }
}
