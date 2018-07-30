using System;
using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> implicit conversions.
    /// </summary>
    [TestFixture]
    internal class ResultImplicitTests : ResultTestsBase
    {
        [Test]
        public void ResultTToResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok(42);
            Result okWithoutValue = ok;
            CheckResultOk(okWithoutValue);

            // Warning result
            var warning = Result.Warn(12, "My warning");
            Result warningWithoutValue = warning;
            CheckResultWarn(warningWithoutValue, "My warning");

            var warnException = new Exception("Warning Exception");
            warning = Result.Warn(12, "My warning", warnException);
            warningWithoutValue = warning;
            CheckResultWarn(warningWithoutValue, "My warning", warnException);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            Result failureWithoutValue = failure;
            CheckResultFail(failureWithoutValue, "My failure");

            var failException = new Exception("Failure Exception");
            failure = Result.Fail<int>("My failure", failException);
            failureWithoutValue = failure;
            CheckResultFail(failureWithoutValue, "My failure", failException);
        }

        [Test]
        public void CustomResultToResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();
            Result okWithoutValue = ok;
            CheckResultOk(okWithoutValue);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");
            Result warningWithoutValue = warning;
            CheckResultWarn(warningWithoutValue, "My warning");

            var warnException = new Exception("Warning Exception");
            warning = Result.CustomWarn<CustomErrorTest>("My warning", warnException);
            warningWithoutValue = warning;
            CheckResultWarn(warningWithoutValue, "My warning", warnException);

            // Failure result
            var failure = Result.CustomFail("My failure", new CustomErrorTest());
            Result failureWithoutValue = failure;
            CheckResultFail(failureWithoutValue, "My failure");

            var failException = new Exception("Failure Exception");
            failure = Result.CustomFail("My failure", new CustomErrorTest(), failException);
            failureWithoutValue = failure;
            CheckResultFail(failureWithoutValue, "My failure", failException);
        }

        [Test]
        public void ResultValueTErrorToResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            Result okWithoutValue = ok;
            CheckResultOk(okWithoutValue);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            Result warningWithoutValue = warning;
            CheckResultWarn(warningWithoutValue, "My warning");

            var warnException = new Exception("Warning Exception");
            warning = Result.Warn<int, CustomErrorTest>(42, "My warning", warnException);
            warningWithoutValue = warning;
            CheckResultWarn(warningWithoutValue, "My warning", warnException);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            Result failureWithoutValue = failure;
            CheckResultFail(failureWithoutValue, "My failure");

            var failException = new Exception("Failure Exception");
            failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest(), failException);
            failureWithoutValue = failure;
            CheckResultFail(failureWithoutValue, "My failure", failException);
        }

        [Test]
        public void ResultValueTErrorToCustomResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(42);
            CustomResult<CustomErrorTest> customResultOk = ok;
            CheckResultOk(customResultOk);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            CustomResult<CustomErrorTest> customResultWarning = warning;
            CheckResultWarn(customResultWarning, "My warning");

            var warnException = new Exception("Warning Exception");
            warning = Result.Warn<int, CustomErrorTest>(42, "My warning", warnException);
            customResultWarning = warning;
            CheckResultWarn(customResultWarning, "My warning", warnException);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -12 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CustomResult<CustomErrorTest> customResultFailure = failure;
            CheckResultFail(customResultFailure, "My failure", customErrorObject);

            var failException = new Exception("Failure Exception");
            failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject, failException);
            customResultFailure = failure;
            CheckResultFail(customResultFailure, "My failure", customErrorObject, failException);
        }

        [Test]
        public void ResultValueTErrorToResultTImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(42);
            Result<int> okResultWithValue = ok;
            CheckResultOk(okResultWithValue, 42);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            Result<int> warningResultWithValue = warning;
            CheckResultWarn(warningResultWithValue, 42, "My warning");

            var warnException = new Exception("Warning Exception");
            warning = Result.Warn<int, CustomErrorTest>(42, "My warning", warnException);
            warningResultWithValue = warning;
            CheckResultWarn(warningResultWithValue, 42, "My warning", warnException);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            Result<int> failureResultWithValue = failure;
            CheckResultFail(failureResultWithValue, "My failure");

            var failException = new Exception("Failure Exception");
            failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest(), failException);
            failureResultWithValue = failure;
            CheckResultFail(failureResultWithValue, "My failure", failException);
        }
    }
}