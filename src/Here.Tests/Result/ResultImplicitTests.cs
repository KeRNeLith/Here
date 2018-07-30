using System;
using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> implicit conversions.
    /// </summary>
    [TestFixture]
    internal class ResultImplicitTests : HereTestsBase
    {
        [Test]
        public void ResultTToResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok(42);
            Result okWithoutValue = ok;
            Assert.IsTrue(okWithoutValue.IsSuccess);
            Assert.IsFalse(okWithoutValue.IsWarning);
            Assert.IsFalse(okWithoutValue.IsFailure);
            Assert.IsNull(okWithoutValue.Message);

            // Warning result
            var warning = Result.Warn(12, "My warning");
            Result warningWithoutValue = warning;
            Assert.IsTrue(warningWithoutValue.IsSuccess);
            Assert.IsTrue(warningWithoutValue.IsWarning);
            Assert.IsFalse(warningWithoutValue.IsFailure);
            Assert.AreEqual("My warning", warningWithoutValue.Message);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            Result failureWithoutValue = failure;
            Assert.IsFalse(failureWithoutValue.IsSuccess);
            Assert.IsFalse(failureWithoutValue.IsWarning);
            Assert.IsTrue(failureWithoutValue.IsFailure);
            Assert.AreEqual("My failure", failureWithoutValue.Message);
        }

        [Test]
        public void CustomResultToResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.CustomOk<Exception>();
            Result okWithoutValue = ok;
            Assert.IsTrue(okWithoutValue.IsSuccess);
            Assert.IsFalse(okWithoutValue.IsWarning);
            Assert.IsFalse(okWithoutValue.IsFailure);
            Assert.IsNull(okWithoutValue.Message);

            // Warning result
            var warning = Result.CustomWarn<Exception>("My warning");
            Result warningWithoutValue = warning;
            Assert.IsTrue(warningWithoutValue.IsSuccess);
            Assert.IsTrue(warningWithoutValue.IsWarning);
            Assert.IsFalse(warningWithoutValue.IsFailure);
            Assert.AreEqual("My warning", warningWithoutValue.Message);

            // Failure result
            var failure = Result.CustomFail("My failure", new Exception("My test exception"));
            Result failureWithoutValue = failure;
            Assert.IsFalse(failureWithoutValue.IsSuccess);
            Assert.IsFalse(failureWithoutValue.IsWarning);
            Assert.IsTrue(failureWithoutValue.IsFailure);
            Assert.AreEqual("My failure", failureWithoutValue.Message);
        }

        [Test]
        public void ResultValueTErrorToResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            Result okWithoutValue = ok;
            Assert.IsTrue(okWithoutValue.IsSuccess);
            Assert.IsFalse(okWithoutValue.IsWarning);
            Assert.IsFalse(okWithoutValue.IsFailure);
            Assert.IsNull(okWithoutValue.Message);

            // Warning result
            var warning = Result.Warn<int, Exception>(42, "My warning");
            Result warningWithoutValue = warning;
            Assert.IsTrue(warningWithoutValue.IsSuccess);
            Assert.IsTrue(warningWithoutValue.IsWarning);
            Assert.IsFalse(warningWithoutValue.IsFailure);
            Assert.AreEqual("My warning", warningWithoutValue.Message);

            // Failure result
            var failure = Result.Fail<int, Exception>("My failure", new Exception("My test exception"));
            Result failureWithoutValue = failure;
            Assert.IsFalse(failureWithoutValue.IsSuccess);
            Assert.IsFalse(failureWithoutValue.IsWarning);
            Assert.IsTrue(failureWithoutValue.IsFailure);
            Assert.AreEqual("My failure", failureWithoutValue.Message);
        }

        [Test]
        public void ResultValueTErrorToCustomResultImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            CustomResult<Exception> customResultOk = ok;
            Assert.IsTrue(customResultOk.IsSuccess);
            Assert.IsFalse(customResultOk.IsWarning);
            Assert.IsFalse(customResultOk.IsFailure);
            Assert.IsNull(customResultOk.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = customResultOk.Error; });

            // Warning result
            var warning = Result.Warn<int, Exception>(42, "My warning");
            CustomResult<Exception> customResultWarning = warning;
            Assert.IsTrue(customResultWarning.IsSuccess);
            Assert.IsTrue(customResultWarning.IsWarning);
            Assert.IsFalse(customResultWarning.IsFailure);
            Assert.AreEqual("My warning", customResultWarning.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = customResultWarning.Error; });

            // Failure result
            var customErrorObject = new Exception("My test exception");
            var failure = Result.Fail<int, Exception>("My failure", customErrorObject);
            CustomResult<Exception> customResultFailure = failure;
            Assert.IsFalse(customResultFailure.IsSuccess);
            Assert.IsFalse(customResultFailure.IsWarning);
            Assert.IsTrue(customResultFailure.IsFailure);
            Assert.AreEqual("My failure", customResultFailure.Message);
            Assert.AreSame(customErrorObject, customResultFailure.Error);
        }

        [Test]
        public void ResultValueTErrorToResultTImplicitConstruction()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            Result<int> okResultWithValue = ok;
            Assert.IsTrue(okResultWithValue.IsSuccess);
            Assert.IsFalse(okResultWithValue.IsWarning);
            Assert.IsFalse(okResultWithValue.IsFailure);
            Assert.IsNull(okResultWithValue.Message);
            Assert.AreEqual(42, okResultWithValue.Value);

            // Warning result
            var warning = Result.Warn<int, Exception>(42, "My warning");
            Result<int> warningResultWithValue = warning;
            Assert.IsTrue(warningResultWithValue.IsSuccess);
            Assert.IsTrue(warningResultWithValue.IsWarning);
            Assert.IsFalse(warningResultWithValue.IsFailure);
            Assert.AreEqual("My warning", warningResultWithValue.Message);
            Assert.AreEqual(42, warningResultWithValue.Value);

            // Failure result
            var failure = Result.Fail<int, Exception>("My failure", new Exception("My test exception"));
            Result<int> failureResultWithValue = failure;
            Assert.IsFalse(failureResultWithValue.IsSuccess);
            Assert.IsFalse(failureResultWithValue.IsWarning);
            Assert.IsTrue(failureResultWithValue.IsFailure);
            Assert.AreEqual("My failure", failureResultWithValue.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = failureResultWithValue.Value; });
        }
    }
}