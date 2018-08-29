using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> operators.
    /// </summary>
    [TestFixture]
    internal class ResultOperatorsTests : ResultTestsBase
    {
        #region Result

        [Test]
        public void ResultBoolOpertors()
        {
            // Result ok
            var resultOk = Result.Ok();
            if (resultOk)
            {
                // Does not work because it expects a bool?
                //Assert.IsTrue(resultOk);
            }
            else
                Assert.Fail("ResultOk should be true.");

            if (!resultOk)
                Assert.Fail("!ResultOk should not be true.");

            // Result warn
            var resultWarn = Result.Warn("My warning");
            if (resultWarn)
            {
                // Does not work because it expects a bool?
                //Assert.IsTrue(resultWarn);
            }
            else
                Assert.Fail("ResultWarn should be true.");

            if (!resultWarn)
                Assert.Fail("!ResultWarn should not be true.");

            // Result fail
            var resultFail = Result.Fail("My failure");
            if (resultFail)
                Assert.Fail("ResultFail should not be true.");

            if (!resultFail)
            {
            }
            else
                Assert.Fail("!ResultFail should not be false.");
        }

        [Test]
        public void ResultLogicalOrOperator()
        {
            // Results ok
            var first = Result.Ok();
            var second = Result.Ok();

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok results should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok results should be true.");

            // Results warn
            first = Result.Warn("My warning 1");
            second = Result.Warn("My warning 2");

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on 2 Warn results should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on 2 Warn results should be true.");

            // Result ok & warn
            first = Result.Ok();
            second = Result.Warn("My warning");

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Warn results should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Warn results should be true.");

            // Result ok & fail
            first = Result.Ok();
            second = Result.Fail("My failure");

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Fail results should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Fail results should be true.");

            // Result warn & fail
            first = Result.Warn("My warning");
            second = Result.Fail("My failure");

            if (first || second)
            {
            }
            else
                Assert.Fail("|| performed on a Warn and a Fail results should be true.");

            if (second || first)
            {
            }
            else
                Assert.Fail("|| performed on a Warn and a Fail results should be true.");

            // Results failure
            first = Result.Fail("My failure 1");
            second = Result.Fail("My failure 2");

            if (first || second)
                Assert.Fail("|| performed on 2 Fail results should be false.");

            if (second || first)
                Assert.Fail("|| performed on 2 Fail results should be false.");
        }

        [Test]
        public void ResultLogicalAndOperator()
        {
            // Results ok
            var first = Result.Ok();
            var second = Result.Ok();

            if (first && second)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok results should be true.");

            if (second && first)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok results should be true.");

            // Results warn
            first = Result.Warn("My warning 1");
            second = Result.Warn("My warning 2");

            if (first && second)
            {
            }
            else
                Assert.Fail("&& performed on 2 Warn results should be true.");

            if (second && first)
            {
            }
            else
                Assert.Fail("&& performed on 2 Warn results should be true.");

            // Result ok & warn
            first = Result.Ok();
            second = Result.Warn("My warning");

            if (first && second)
            {
            }
            else
                Assert.Fail("&& performed on a Ok and a Warn results should be true.");

            if (second && first)
            {
            }
            else
                Assert.Fail("&& performed on a Ok and a Warn results should be true.");

            // Result ok & fail
            first = Result.Ok();
            second = Result.Fail("My failure");

            if (first && second)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            // Result warn & fail
            first = Result.Warn("My warning");
            second = Result.Fail("My failure");

            if (first && second)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            // Results failure
            first = Result.Fail("My failure 1");
            second = Result.Fail("My failure 2");

            if (first && second)
                Assert.Fail("&& performed on 2 Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on 2 Fail results should be false.");
        }

        #endregion
    }
}