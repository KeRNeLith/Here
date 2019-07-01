using System;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueResultOnFailureExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ValueResultOnFailureToValue()
        {
            #region Local functions

            void CheckOnFailure(Result<int> result, bool treatWarningAsError, bool expectFailure)
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

            void CheckOnFailureFunc(Result<int> result, bool treatWarningAsError, bool expectFailure)
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
            var ok = Result.Ok(55);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);
            CheckOnFailureFunc(ok, false, false);
            CheckOnFailureFunc(ok, true, false);

            // Warning result
            var warning = Result.Warn(65, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);
            CheckOnFailureFunc(warning, false, false);
            CheckOnFailureFunc(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
            CheckOnFailureFunc(failure, false, true);
            CheckOnFailureFunc(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null, 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<Result<int>, float>)null, null));
        }

        [Test]
        public void ValueResultOnFailureToValueResult()
        {
            #region Local function

            void CheckOnFailure(Result<int> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result<int> res = result.OnFailure(() => ++counterFailure, treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(42);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn(25, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Action)null));
        }

        [Test]
        public void ValueResultOnFailureToResultWithParam()
        {
            #region Local function

            void CheckOnFailure(Result<int> result, bool treatWarningAsError, bool expectFailure)
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
            var ok = Result.Ok(12);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn(32, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Func<Result<int>, Result>)null));
        }

        [Test]
        public void ValueResultOnFailureToValueResultWithParam()
        {
            #region Local function

            void CheckOnFailure(Result<int> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                Result<int> res = result.OnFailure(r => ++counterFailure, treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(result, res);
            }

            void CheckOnFailureFunc(Result<int> result, bool treatWarningAsError, bool expectFailure)
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
                    Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(12);
            CheckOnFailure(ok, false, false);
            CheckOnFailure(ok, true, false);
            CheckOnFailureFunc(ok, false, false);
            CheckOnFailureFunc(ok, true, false);

            // Warning result
            var warning = Result.Warn(32, "My warning");
            CheckOnFailure(warning, false, false);
            CheckOnFailure(warning, true, true);
            CheckOnFailureFunc(warning, false, false);
            CheckOnFailureFunc(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnFailure(failure, false, true);
            CheckOnFailure(failure, true, true);
            CheckOnFailureFunc(failure, false, true);
            CheckOnFailureFunc(failure, true, true);

            Assert.Throws<ArgumentNullException>(() => ok.OnFailure((Action<Result<int>>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnFailure(null));
        }
    }
}
