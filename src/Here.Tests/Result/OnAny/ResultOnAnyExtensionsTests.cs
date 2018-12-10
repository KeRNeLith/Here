using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ResultOnAnyToResult()
        {
            #region Local function

            void CheckOnAny(Result result, bool treatWarningAsError)
            {
                int counter = 0;
                Result res = result.OnAny(() => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion
            
            // Ok result
            var ok = Result.Ok();
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
        }

        [Test]
        public void ResultOnAnyToResultWithParam()
        {
            #region Local function

            void CheckOnAny(Result result, bool treatWarningAsError)
            {
                int counter = 0;
                Result res = result.OnAny(r => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok();
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
        }

        [Test]
        public void ResultOnAnyTOut()
        {
            #region Local functions

            void CheckOnAny(Result result, bool treatWarningAsError)
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

            void CheckOnAnyNoInput(Result result, bool treatWarningAsError)
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
            var ok = Result.Ok();
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);
            CheckOnAnyNoInput(ok, false);
            CheckOnAnyNoInput(ok, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);
            CheckOnAnyNoInput(warning, false);
            CheckOnAnyNoInput(warning, true);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
            CheckOnAnyNoInput(failure, false);
            CheckOnAnyNoInput(failure, true);
        }
    }
}
