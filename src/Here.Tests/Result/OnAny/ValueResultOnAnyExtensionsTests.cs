using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueResultOnAnyExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ValueResultOnAnyToValueResult()
        {
            #region Local function

            void CheckOnAny(Result<int> result, bool treatWarningAsError)
            {
                int counter = 0;
                Result<int> res = result.OnAny(() => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(81);
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);

            // Warning result
            var warning = Result.Warn(92, "My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
        }

        [Test]
        public void ValueResultOnAnyToValueResultWithParam()
        {
            #region Local function

            void CheckOnAny(Result<int> result, bool treatWarningAsError)
            {
                int counter = 0;
                Result<int> res = result.OnAny(r => { ++counter; });
                Assert.AreEqual(1, counter);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(42);
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);

            // Warning result
            var warning = Result.Warn(78, "My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
        }

        [Test]
        public void ValueResultOnAnyTOut()
        {
            #region Local functions

            void CheckOnAny(Result<int> result, bool treatWarningAsError)
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

            void CheckOnAnyNoInput(Result<int> result, bool treatWarningAsError)
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
            var ok = Result.Ok(96);
            CheckOnAny(ok, false);
            CheckOnAny(ok, true);
            CheckOnAnyNoInput(ok, false);
            CheckOnAnyNoInput(ok, true);

            // Warning result
            var warning = Result.Warn(102, "My warning");
            CheckOnAny(warning, false);
            CheckOnAny(warning, true);
            CheckOnAnyNoInput(warning, false);
            CheckOnAnyNoInput(warning, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnAny(failure, false);
            CheckOnAny(failure, true);
            CheckOnAnyNoInput(failure, false);
            CheckOnAnyNoInput(failure, true);
        }
    }
}
