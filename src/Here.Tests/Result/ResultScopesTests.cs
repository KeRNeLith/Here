using System;
using NUnit.Framework;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> safe scopes.
    /// </summary>
    [TestFixture]
    internal class ResultScopesTests : ResultTestsBase
    {
        [Test]
        public void ResultActionSafeScope()
        {
            int counter = 0;

            Result result = ResultScope.SafeResult(() => ++counter);
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            var thrownException = new Exception("My exception");
            int testVar = 12;
            result = ResultScope.SafeResult(
                () => 
                {
                    ++counter;
                    if (testVar > 0)
                    {
                        throw thrownException;
                    }
                    ++counter;
                });
            Assert.AreEqual(2, counter);
            CheckResultFail(result, "My exception", thrownException);

            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeResult((Action)null));
        }

        [Test]
        public void ResultSafeScope()
        {
            Result result = ResultScope.SafeResult(Result.Ok);
            CheckResultOk(result);

            result = ResultScope.SafeResult(() => Result.Warn("My warning"));
            CheckResultWarn(result, "My warning");

            result = ResultScope.SafeResult(() => Result.Fail("My failure"));
            CheckResultFail(result, "My failure");

            var thrownException = new Exception("My exception");
            result = ResultScope.SafeResult(() => throw thrownException);
            CheckResultFail(result, "My exception", thrownException);

            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeResult(null));
        }

        [Test]
        public void ValueResultSafeScope()
        {
            Result<int> result = ResultScope.SafeValueResult(() => Result.Ok(12));
            CheckResultOk(result, 12);

            result = ResultScope.SafeValueResult(() => Result.Warn(42, "My warning"));
            CheckResultWarn(result, 42, "My warning");

            result = ResultScope.SafeValueResult(() => Result.Fail<int>("My failure"));
            CheckResultFail(result, "My failure");

            var thrownException = new Exception("My exception");
            result = ResultScope.SafeValueResult<int>(() => throw thrownException);
            CheckResultFail(result, "My exception", thrownException);

            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueResult((Func<Result<int>>)null));
        }

        [Test]
        public void CustomResultSafeScope()
        {
            var defaultError = new CustomErrorTest { ErrorCode = 51 };
            CustomResult<CustomErrorTest> result = ResultScope.SafeCustomResult(Result.CustomOk<CustomErrorTest>, () => defaultError);
            CheckResultOk(result);

            result = ResultScope.SafeCustomResult(Result.CustomOk<CustomErrorTest>, defaultError);
            CheckResultOk(result);

            result = ResultScope.SafeCustomResult(() => Result.CustomWarn<CustomErrorTest>("My warning"), () => defaultError);
            CheckResultWarn(result, "My warning");

            result = ResultScope.SafeCustomResult(() => Result.CustomWarn<CustomErrorTest>("My warning"), defaultError);
            CheckResultWarn(result, "My warning");

            var errorObject = new CustomErrorTest { ErrorCode = 12 };
            result = ResultScope.SafeCustomResult(() => Result.CustomFail("My failure", errorObject), () => defaultError);
            CheckResultFail(result, "My failure", errorObject);

            result = ResultScope.SafeCustomResult(() => Result.CustomFail("My failure", errorObject), defaultError);
            CheckResultFail(result, "My failure", errorObject);

            var thrownException = new Exception("My exception");
            result = ResultScope.SafeCustomResult(() => throw thrownException, () => defaultError);
            CheckResultFail(result, "My exception", defaultError, thrownException);

            result = ResultScope.SafeCustomResult(() => throw thrownException, defaultError);
            CheckResultFail(result, "My exception", defaultError, thrownException);

            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeCustomResult(null, defaultError));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeCustomResult(null, () => defaultError));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeCustomResult(Result.CustomOk<CustomErrorTest>, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeCustomResult(Result.CustomOk<CustomErrorTest>, (Func<CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeCustomResult(null, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeCustomResult((Func<CustomResult<CustomErrorTest>>)null, (Func<CustomErrorTest>)null));
        }

        [Test]
        public void ValueCustomResultSafeScope()
        {
            var defaultError = new CustomErrorTest { ErrorCode = 51 };
            Result<int, CustomErrorTest> result = ResultScope.SafeValueCustomResult(() => Result.Ok<int, CustomErrorTest>(42), () => defaultError);
            CheckResultOk(result, 42);

            result = ResultScope.SafeValueCustomResult(() => Result.Ok<int, CustomErrorTest>(12), defaultError);
            CheckResultOk(result, 12);

            result = ResultScope.SafeValueCustomResult(() => Result.Warn<int, CustomErrorTest>(21, "My warning"), () => defaultError);
            CheckResultWarn(result, 21, "My warning");

            result = ResultScope.SafeValueCustomResult(() => Result.Warn<int, CustomErrorTest>(51, "My warning"), defaultError);
            CheckResultWarn(result, 51, "My warning");

            var errorObject = new CustomErrorTest { ErrorCode = 12 };
            result = ResultScope.SafeValueCustomResult(() => Result.Fail<int, CustomErrorTest>("My failure", errorObject), () => defaultError);
            CheckResultFail(result, "My failure", errorObject);

            result = ResultScope.SafeValueCustomResult(() => Result.Fail<int, CustomErrorTest>("My failure", errorObject), defaultError);
            CheckResultFail(result, "My failure", errorObject);

            var thrownException = new Exception("My exception");
            result = ResultScope.SafeValueCustomResult<int, CustomErrorTest>(() => throw thrownException, () => defaultError);
            CheckResultFail(result, "My exception", defaultError, thrownException);

            result = ResultScope.SafeValueCustomResult<int, CustomErrorTest>(() => throw thrownException, defaultError);
            CheckResultFail(result, "My exception", defaultError, thrownException);

            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueCustomResult((Func<Result<int, CustomErrorTest>>)null, defaultError));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueCustomResult((Func<Result<int, CustomErrorTest>>)null, () => defaultError));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueCustomResult(() => Result.Ok<int, CustomErrorTest>(12), (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueCustomResult(() => Result.Ok<int, CustomErrorTest>(12), (Func<CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueCustomResult((Func<Result<int, CustomErrorTest>>)null, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ResultScope.SafeValueCustomResult((Func<Result<int, CustomErrorTest>>)null, (Func<CustomErrorTest>)null));
        }
    }
}