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
        public void ResultBitwiseOrOperator()
        {
            // Results ok
            var first = Result.Ok();
            var second = Result.Ok();

            var bitwiseOrResult = first | second;   // The first operand should always be selected (Both are success)
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultOk(bitwiseOrResult);

            bitwiseOrResult = second | first;       // The first operand should always be selected (Both are success)
            Assert.AreEqual(second, bitwiseOrResult);
            CheckResultOk(bitwiseOrResult);

            // Results warn
            first = Result.Warn("My warning 1");
            second = Result.Warn("My warning 2");

            bitwiseOrResult = first | second;       // The first operand should always be selected (Both are success)
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultWarn(bitwiseOrResult, "My warning 1");

            bitwiseOrResult = second | first;       // The first operand should always be selected (Both are success)
            Assert.AreEqual(second, bitwiseOrResult);
            CheckResultWarn(bitwiseOrResult, "My warning 2");

            // Result ok & warn
            first = Result.Ok();
            second = Result.Warn("My warning");

            bitwiseOrResult = first | second;       // The first operand should always be selected (Both are success)
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultOk(bitwiseOrResult);

            bitwiseOrResult = second | first;       // The first operand should always be selected (Both are success)
            Assert.AreEqual(second, bitwiseOrResult);
            CheckResultWarn(bitwiseOrResult, "My warning");

            // Result ok & fail
            first = Result.Ok();
            second = Result.Fail("My failure");

            bitwiseOrResult = first | second;       // First should always be selected as it is success
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultOk(bitwiseOrResult);

            bitwiseOrResult = second | first;       // First should always be selected as it is success
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultOk(bitwiseOrResult);

            // Result warn & fail
            first = Result.Warn("My warning");
            second = Result.Fail("My failure");

            bitwiseOrResult = first | second;       // First should always be selected as it is success (warning)
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultWarn(bitwiseOrResult, "My warning");

            bitwiseOrResult = second | first;       // First should always be selected as it is success (warning)
            Assert.AreEqual(first, bitwiseOrResult);
            CheckResultWarn(bitwiseOrResult, "My warning");

            // Results failure
            first = Result.Fail("My failure 1");
            second = Result.Fail("My failure 2");

            bitwiseOrResult = first | second;       // Should be a failure (Both are failure)
            CheckResultFail(bitwiseOrResult, ResultConstants.ResultBitwiseOrOperatorErrorMessage);

            bitwiseOrResult = second | first;       // Should be a failure (Both are failure)
            CheckResultFail(bitwiseOrResult, ResultConstants.ResultBitwiseOrOperatorErrorMessage);
        }

        [Test]
        public void ResultLogicalOrOperator()
        {
            // Results ok
            var first = Result.Ok();
            var second = Result.Ok();

            var logicalOrResult = first || second;  // The first operand should always be selected (Both are success)
            Assert.AreEqual(first, logicalOrResult);
            CheckResultOk(logicalOrResult);

            logicalOrResult = second || first;      // The first operand should always be selected (Both are success)
            Assert.AreEqual(second, logicalOrResult);
            CheckResultOk(logicalOrResult);

            // Results warn
            first = Result.Warn("My warning 1");
            second = Result.Warn("My warning 2");

            logicalOrResult = first || second;      // The first operand should always be selected (Both are success)
            Assert.AreEqual(first, logicalOrResult);
            CheckResultWarn(logicalOrResult, "My warning 1");

            logicalOrResult = second || first;      // The first operand should always be selected (Both are success)
            Assert.AreEqual(second, logicalOrResult);
            CheckResultWarn(logicalOrResult, "My warning 2");

            // Result ok & warn
            first = Result.Ok();
            second = Result.Warn("My warning");

            logicalOrResult = first || second;      // The first operand should always be selected (Both are success)
            Assert.AreEqual(first, logicalOrResult);
            CheckResultOk(logicalOrResult);

            logicalOrResult = second || first;      // The first operand should always be selected (Both are success)
            Assert.AreEqual(second, logicalOrResult);
            CheckResultWarn(logicalOrResult, "My warning");

            // Result ok & fail
            first = Result.Ok();
            second = Result.Fail("My failure");

            logicalOrResult = first || second;      // First should always be selected as it is success
            Assert.AreEqual(first, logicalOrResult);
            CheckResultOk(logicalOrResult);

            logicalOrResult = second || first;      // First should always be selected as it is success
            Assert.AreEqual(first, logicalOrResult);
            CheckResultOk(logicalOrResult);

            // Result warn & fail
            first = Result.Warn("My warning");
            second = Result.Fail("My failure");

            logicalOrResult = first || second;      // First should always be selected as it is success (warning)
            Assert.AreEqual(first, logicalOrResult);
            CheckResultWarn(logicalOrResult, "My warning");

            logicalOrResult = second || first;      // First should always be selected as it is success (warning)
            Assert.AreEqual(first, logicalOrResult);
            CheckResultWarn(logicalOrResult, "My warning");

            // Results failure
            first = Result.Fail("My failure 1");
            second = Result.Fail("My failure 2");

            logicalOrResult = first || second;      // Should be a failure (Both are failure)
            CheckResultFail(logicalOrResult, ResultConstants.ResultBitwiseOrOperatorErrorMessage);

            logicalOrResult = second || first;      // Should be a failure (Both are failure)
            CheckResultFail(logicalOrResult, ResultConstants.ResultBitwiseOrOperatorErrorMessage);
        }

        [Test]
        public void ResultBitwiseAndOperator()
        {
            // Results ok
            var first = Result.Ok();
            var second = Result.Ok();

            var bitwiseAndResult = first & second;   // The second operand should always be selected (Both are success)
            Assert.AreEqual(second, bitwiseAndResult);
            CheckResultOk(bitwiseAndResult);

            bitwiseAndResult = second & first;       // The second operand should always be selected (Both are success)
            Assert.AreEqual(first, bitwiseAndResult);
            CheckResultOk(bitwiseAndResult);

            // Results warn
            first = Result.Warn("My warning 1");
            second = Result.Warn("My warning 2");

            bitwiseAndResult = first & second;       // The second operand should always be selected (Both are success)
            Assert.AreEqual(second, bitwiseAndResult);
            CheckResultWarn(bitwiseAndResult, "My warning 2");

            bitwiseAndResult = second & first;       // The second operand should always be selected (Both are success)
            Assert.AreEqual(first, bitwiseAndResult);
            CheckResultWarn(bitwiseAndResult, "My warning 1");

            // Result ok & warn
            first = Result.Ok();
            second = Result.Warn("My warning");

            bitwiseAndResult = first & second;       // The second operand should always be selected (Both are success)
            Assert.AreEqual(second, bitwiseAndResult);
            CheckResultWarn(bitwiseAndResult, "My warning");

            bitwiseAndResult = second & first;       // The second operand should always be selected (Both are success)
            Assert.AreEqual(first, bitwiseAndResult);
            CheckResultOk(bitwiseAndResult);

            // Result ok & fail
            first = Result.Ok();
            second = Result.Fail("My failure");

            bitwiseAndResult = first & second;       // At least one failure so it's failure
            CheckResultFail(bitwiseAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            bitwiseAndResult = second & first;       // At least one failure so it's failure
            CheckResultFail(bitwiseAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            // Result warn & fail
            first = Result.Warn("My warning");
            second = Result.Fail("My failure");

            bitwiseAndResult = first & second;       // At least one failure so it's failure
            CheckResultFail(bitwiseAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            bitwiseAndResult = second & first;       // At least one failure so it's failure
            CheckResultFail(bitwiseAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            // Results failure
            first = Result.Fail("My failure 1");
            second = Result.Fail("My failure 2");

            bitwiseAndResult = first & second;      // Should be a failure (Both are failure)
            CheckResultFail(bitwiseAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            bitwiseAndResult = second & first;      // Should be a failure (Both are failure)
            CheckResultFail(bitwiseAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);
        }

        [Test]
        public void ResultLogicalAndOperator()
        {
            // Results ok
            var first = Result.Ok();
            var second = Result.Ok();

            var logicalAndResult = first && second;  // The second operand should always be selected (Both are success)
            Assert.AreEqual(second, logicalAndResult);
            CheckResultOk(logicalAndResult);

            logicalAndResult = second && first;      // The second operand should always be selected (Both are success)
            Assert.AreEqual(first, logicalAndResult);
            CheckResultOk(logicalAndResult);

            // Results warn
            first = Result.Warn("My warning 1");
            second = Result.Warn("My warning 2");

            logicalAndResult = first && second;      // The second operand should always be selected (Both are success)
            Assert.AreEqual(second, logicalAndResult);
            CheckResultWarn(logicalAndResult, "My warning 2");

            logicalAndResult = second && first;      // The second operand should always be selected (Both are success)
            Assert.AreEqual(first, logicalAndResult);
            CheckResultWarn(logicalAndResult, "My warning 1");

            // Result ok & warn
            first = Result.Ok();
            second = Result.Warn("My warning");

            logicalAndResult = first && second;      // The second operand should always be selected (Both are success)
            Assert.AreEqual(second, logicalAndResult);
            CheckResultWarn(logicalAndResult, "My warning");

            logicalAndResult = second && first;      // The second operand should always be selected (Both are success)
            Assert.AreEqual(first, logicalAndResult);
            CheckResultOk(logicalAndResult);

            // Result ok & fail
            first = Result.Ok();
            second = Result.Fail("My failure");

            logicalAndResult = first && second;      // At least one failure so it's failure
            CheckResultFail(logicalAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            logicalAndResult = second && first;      // At least one failure so it's failure
            CheckResultFail(logicalAndResult, "My failure");  // Not ResultConstants.ResultBitwiseAndOperatorErrorMessage 
                                                              // because it calls the false operator before & so the returned result is Second

            // Result warn & fail
            first = Result.Warn("My warning");
            second = Result.Fail("My failure");

            logicalAndResult = first && second;      // At least one failure so it's failure
            CheckResultFail(logicalAndResult, ResultConstants.ResultBitwiseAndOperatorErrorMessage);

            logicalAndResult = second && first;      // At least one failure so it's failure
            CheckResultFail(logicalAndResult, "My failure");  // Not ResultConstants.ResultBitwiseAndOperatorErrorMessage 
                                                              // because it calls the false operator before & so the returned result is Second

            // Results failure
            first = Result.Fail("My failure 1");
            second = Result.Fail("My failure 2");

            logicalAndResult = first && second;     // Should be a failure (Both are failure)
            CheckResultFail(logicalAndResult, "My failure 1");// Not ResultConstants.ResultBitwiseAndOperatorErrorMessage 
                                                              // because it calls the false operator before & so the returned result is First

            logicalAndResult = second && first;     // Should be a failure (Both are failure)
            CheckResultFail(logicalAndResult, "My failure 2");  // Not ResultConstants.ResultBitwiseAndOperatorErrorMessage 
                                                              // because it calls the false operator before & so the returned result is Second
        }

        #endregion
    }
}