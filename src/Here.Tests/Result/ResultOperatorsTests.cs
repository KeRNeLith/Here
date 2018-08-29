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

        #region Result<T>

        [Test]
        public void ValueResultBoolOpertors()
        {
            // Result ok
            var resultOk = Result.Ok(42);
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
            var resultWarn = Result.Warn(25, "My warning");
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
            var resultFail = Result.Fail<int>("My failure");
            if (resultFail)
                Assert.Fail("ResultFail should not be true.");

            if (!resultFail)
            {
            }
            else
                Assert.Fail("!ResultFail should not be false.");
        }

        [Test]
        public void ValueResultLogicalOrOperator()
        {
            // Results ok
            var first = Result.Ok(42);
            var second = Result.Ok(45);

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
            first = Result.Warn(25, "My warning 1");
            second = Result.Warn(45, "My warning 2");

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
            first = Result.Ok(78);
            second = Result.Warn(89, "My warning");

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
            first = Result.Ok(65);
            second = Result.Fail<int>("My failure");

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
            first = Result.Warn(46, "My warning");
            second = Result.Fail<int>("My failure");

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
            first = Result.Fail<int>("My failure 1");
            second = Result.Fail<int>("My failure 2");

            if (first || second)
                Assert.Fail("|| performed on 2 Fail results should be false.");

            if (second || first)
                Assert.Fail("|| performed on 2 Fail results should be false.");

            // Mixed types
            first = Result.Ok(7);
            var secondFloat = Result.Warn(1.7f, "My warning");

            if (first || secondFloat)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Warn results should be true.");

            if (secondFloat || first)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Warn results should be true.");
        }

        [Test]
        public void ValueResultLogicalAndOperator()
        {
            // Results ok
            var first = Result.Ok(12);
            var second = Result.Ok(47);

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
            first = Result.Warn(46, "My warning 1");
            second = Result.Warn(88, "My warning 2");

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
            first = Result.Ok(77);
            second = Result.Warn(49, "My warning");

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
            first = Result.Ok(36);
            second = Result.Fail<int>("My failure");

            if (first && second)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            // Result warn & fail
            first = Result.Warn(79, "My warning");
            second = Result.Fail<int>("My failure");

            if (first && second)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            // Results failure
            first = Result.Fail<int>("My failure 1");
            second = Result.Fail<int>("My failure 2");

            if (first && second)
                Assert.Fail("&& performed on 2 Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on 2 Fail results should be false.");

            // Mixed types
            first = Result.Ok(77);
            var secondFloat = Result.Warn(1.2f, "My warning");

            if (first && secondFloat)
            {
            }
            else
                Assert.Fail("&& performed on a Ok and a Warn results should be true.");

            if (secondFloat && first)
            {
            }
            else
                Assert.Fail("&& performed on a Ok and a Warn results should be true.");
        }

        #endregion

        #region CustomResult<TError>

        [Test]
        public void CustomResultBoolOpertors()
        {
            // Result ok
            var resultOk = Result.CustomOk<CustomErrorTest>();
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
            var resultWarn = Result.CustomWarn<CustomErrorTest>("My warning");
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
            var resultFail = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -9 });
            if (resultFail)
                Assert.Fail("ResultFail should not be true.");

            if (!resultFail)
            {
            }
            else
                Assert.Fail("!ResultFail should not be false.");
        }

        [Test]
        public void CustomResultLogicalOrOperator()
        {
            // Results ok
            var first = Result.CustomOk<CustomErrorTest>();
            var second = Result.CustomOk<CustomErrorTest>();

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
            first = Result.CustomWarn<CustomErrorTest>("My warning 1");
            second = Result.CustomWarn<CustomErrorTest>("My warning 2");

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
            first = Result.CustomOk<CustomErrorTest>();
            second = Result.CustomWarn<CustomErrorTest>("My warning");

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
            first = Result.CustomOk<CustomErrorTest>();
            second = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -2 });

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
            first = Result.CustomWarn<CustomErrorTest>("My warning");
            second = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -6 });

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
            first = Result.CustomFail("My failure 1", new CustomErrorTest { ErrorCode = -4 });
            second = Result.CustomFail("My failure 2", new CustomErrorTest { ErrorCode = -3 });

            if (first || second)
                Assert.Fail("|| performed on 2 Fail results should be false.");

            if (second || first)
                Assert.Fail("|| performed on 2 Fail results should be false.");
        }

        [Test]
        public void CustomResultLogicalAndOperator()
        {
            // Results ok
            var first = Result.CustomOk<CustomErrorTest>();
            var second = Result.CustomOk<CustomErrorTest>();

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
            first = Result.CustomWarn<CustomErrorTest>("My warning 1");
            second = Result.CustomWarn<CustomErrorTest>("My warning 2");

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
            first = Result.CustomOk<CustomErrorTest>();
            second = Result.CustomWarn<CustomErrorTest>("My warning");

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
            first = Result.CustomOk<CustomErrorTest>();
            second = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -8 });

            if (first && second)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            // Result warn & fail
            first = Result.CustomWarn<CustomErrorTest>("My warning");
            second = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -11 });

            if (first && second)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            // Results failure
            first = Result.CustomFail("My failure 1", new CustomErrorTest { ErrorCode = -1 });
            second = Result.CustomFail("My failure 2", new CustomErrorTest { ErrorCode = -128 });

            if (first && second)
                Assert.Fail("&& performed on 2 Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on 2 Fail results should be false.");
        }

        #endregion

        #region Result<T, TError>

        [Test]
        public void CustomValueResultBoolOpertors()
        {
            // Result ok
            var resultOk = Result.Ok<int, CustomErrorTest>(42);
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
            var resultWarn = Result.Warn<int, CustomErrorTest>(25, "My warning");
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
            var resultFail = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -9 });
            if (resultFail)
                Assert.Fail("ResultFail should not be true.");

            if (!resultFail)
            {
            }
            else
                Assert.Fail("!ResultFail should not be false.");
        }

        [Test]
        public void CustomValueResultLogicalOrOperator()
        {
            // Results ok
            var first = Result.Ok<int, CustomErrorTest>(42);
            var second = Result.Ok<int, CustomErrorTest>(45);

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
            first = Result.Warn<int, CustomErrorTest>(25, "My warning 1");
            second = Result.Warn<int, CustomErrorTest>(45, "My warning 2");

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
            first = Result.Ok<int, CustomErrorTest>(78);
            second = Result.Warn<int, CustomErrorTest>(89, "My warning");

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
            first = Result.Ok<int, CustomErrorTest>(65);
            second = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -2 });

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
            first = Result.Warn<int, CustomErrorTest>(46, "My warning");
            second = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -6 });

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
            first = Result.Fail<int, CustomErrorTest>("My failure 1", new CustomErrorTest { ErrorCode = -4 });
            second = Result.Fail<int, CustomErrorTest>("My failure 2", new CustomErrorTest { ErrorCode = -3 });

            if (first || second)
                Assert.Fail("|| performed on 2 Fail results should be false.");

            if (second || first)
                Assert.Fail("|| performed on 2 Fail results should be false.");

            // Mixed types
            first = Result.Ok<int, CustomErrorTest>(7);
            var secondFloat = Result.Warn<float, CustomErrorTest>(1.7f, "My warning");

            if (first || secondFloat)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Warn results should be true.");

            if (secondFloat || first)
            {
            }
            else
                Assert.Fail("|| performed on a Ok and a Warn results should be true.");
        }

        [Test]
        public void CustomValueResultLogicalAndOperator()
        {
            // Results ok
            var first = Result.Ok<int, CustomErrorTest>(12);
            var second = Result.Ok<int, CustomErrorTest>(47);

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
            first = Result.Warn<int, CustomErrorTest>(46, "My warning 1");
            second = Result.Warn<int, CustomErrorTest>(88, "My warning 2");

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
            first = Result.Ok<int, CustomErrorTest>(77);
            second = Result.Warn<int, CustomErrorTest>(49, "My warning");

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
            first = Result.Ok<int, CustomErrorTest>(36);
            second = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -8 });

            if (first && second)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Ok and a Fail results should be false.");

            // Result warn & fail
            first = Result.Warn<int, CustomErrorTest>(79, "My warning");
            second = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -11 });

            if (first && second)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on a Warn and a Fail results should be false.");

            // Results failure
            first = Result.Fail<int, CustomErrorTest>("My failure 1", new CustomErrorTest { ErrorCode = -1 });
            second = Result.Fail<int, CustomErrorTest>("My failure 2", new CustomErrorTest { ErrorCode = -128 });

            if (first && second)
                Assert.Fail("&& performed on 2 Fail results should be false.");

            if (second && first)
                Assert.Fail("&& performed on 2 Fail results should be false.");

            // Mixed types
            first = Result.Ok<int, CustomErrorTest>(77);
            var secondFloat = Result.Warn<float, CustomErrorTest>(1.2f, "My warning");

            if (first && secondFloat)
            {
            }
            else
                Assert.Fail("&& performed on a Ok and a Warn results should be true.");

            if (secondFloat && first)
            {
            }
            else
                Assert.Fail("&& performed on a Ok and a Warn results should be true.");
        }

        #endregion

        #region Mixed Results

        [Test]
        public void MixedResultsLogicalOrOperator()
        {
            // Results
            var result = Result.Ok();
            var valueResultInt = Result.Ok(42);
            var valueResultFloat = Result.Ok(45.3f);
            var customResultCustomErrorTest = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -8 });
            var customResultInt = Result.CustomFail("My failure", -9);
            var customValueResultInt = Result.Ok<int, CustomErrorTest>(45);
            var customValueResultFloat = Result.Ok<float, CustomErrorTest>(12.2f);

            // Result & Result<T>
            if (result || valueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok result and value result should be true.");

            if (valueResultInt || result)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok result and value result should be true.");

            // Result & CustomResult<TError>
            if (result || customResultCustomErrorTest)
            {
            }
            else
                Assert.Fail("|| performed on a Ok result and a Fail custom result should be true.");

            if (customResultCustomErrorTest || result)
            {
            }
            else
                Assert.Fail("|| performed on a Ok result and a Fail custom result should be true.");

            // Result & Result<T, TError>
            if (result || customValueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok result and custom value result should be true.");

            if (customValueResultInt || result)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok result and custom value result should be true.");


            // Result<T> & Result<T2>
            if (valueResultInt || valueResultFloat)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok value results should be true.");

            if (valueResultFloat || valueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok value results should be true.");

            // Result<T> & CustomResult<TError>
            if (valueResultInt || customResultCustomErrorTest)
            {
            }
            else
                Assert.Fail("|| performed on a Ok value result and a Fail custom result should be true.");

            if (customResultCustomErrorTest || valueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on a Ok value result and a Fail custom result should be true.");

            // Result<T> & Result<T, TError>
            if (valueResultInt || customValueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok value result and a custom value result should be true.");

            if (customValueResultInt || valueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on 2 Ok value result and a custom value result should be true.");


            // CustomResult<TError> & CustomResult<TError2>
            if (customResultCustomErrorTest || customResultInt)
                Assert.Fail("|| performed on 2 Fail custom results should be false.");

            if (customResultInt || customResultCustomErrorTest)
                Assert.Fail("|| performed on 2 Fail custom results should be false.");

            // CustomResult<TError> & Result<T, TError>
            if (customResultCustomErrorTest || customValueResultInt)
            {
            }
            else
                Assert.Fail("|| performed on a Ok custom value result and a Fail custom result should be true.");

            if (customValueResultInt || customResultCustomErrorTest)
            {
            }
            else
                Assert.Fail("|| performed on a Ok custom value result and a Fail custom result should be true.");
        }

        [Test]
        public void MixedResultsLogicalAndOperator()
        {
            // Results
            var result = Result.Ok();
            var valueResultInt = Result.Ok(42);
            var valueResultFloat = Result.Ok(45.3f);
            var customResultCustomErrorTest = Result.CustomFail("My failure", new CustomErrorTest { ErrorCode = -8 });
            var customResultInt = Result.CustomFail("My failure", -9);
            var customValueResultInt = Result.Ok<int, CustomErrorTest>(45);
            var customValueResultFloat = Result.Ok<float, CustomErrorTest>(12.2f);

            // Result & Result<T>
            if (result && valueResultInt)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok result and value result should be true.");

            if (valueResultInt && result)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok result and value result should be true.");

            // Result & CustomResult<TError>
            if (result && customResultCustomErrorTest)
                Assert.Fail("&& performed on a Ok result and a Fail custom result should be false.");

            if (customResultCustomErrorTest && result)
                Assert.Fail("&& performed on a Ok result and a Fail custom result should be false.");

            // Result & Result<T, TError>
            if (result && customValueResultInt)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok result and custom value result should be true.");

            if (customValueResultInt && result)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok result and custom value result should be true.");


            // Result<T> & Result<T2>
            if (valueResultInt && valueResultFloat)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok value results should be true.");

            if (valueResultFloat && valueResultInt)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok value results should be true.");

            // Result<T> & CustomResult<TError>
            if (valueResultInt && customResultCustomErrorTest)
                Assert.Fail("&& performed on a Ok value result and a Fail custom result should be false.");
            
            if (customResultCustomErrorTest && valueResultInt)
                Assert.Fail("&& performed on a Ok value result and a Fail custom result should be false.");

            // Result<T> & Result<T, TError>
            if (valueResultInt && customValueResultInt)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok value result and a custom value result should be true.");

            if (customValueResultInt && valueResultInt)
            {
            }
            else
                Assert.Fail("&& performed on 2 Ok value result and a custom value result should be true.");


            // CustomResult<TError> & CustomResult<TError2>
            if (customResultCustomErrorTest && customResultInt)
                Assert.Fail("&& performed on 2 Fail custom results should be false.");

            if (customResultInt && customResultCustomErrorTest)
                Assert.Fail("&& performed on 2 Fail custom results should be false.");

            // CustomResult<TError> & Result<T, TError>
            if (customResultCustomErrorTest && customValueResultInt)
                Assert.Fail("&& performed on a Ok custom value result and a Fail custom result should be true.");

            if (customValueResultInt && customResultCustomErrorTest)
                Assert.Fail("&& performed on a Ok custom value result and a Fail custom result should be true.");
        }

        #endregion
    }
}