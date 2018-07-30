using System;
using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Basic tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultTests : ResultTestsBase
    {
        [Test]
        public void ResultConstruction()
        {
            // Ok result
            var ok = Result.Ok();
            CheckResultOk(ok);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckResultWarn(warning, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.Warn("My warning", warnException);
            CheckResultWarn(warning, "My warning", warnException);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckResultFail(failure, "My failure");

            var failException = new Exception("Failure exception");
            failure = Result.Fail("My failure", failException);
            CheckResultFail(failure, "My failure", failException);
        }

        [Test]
        public void CustomResultConstruction()
        {
            // Ok result
            var ok = Result.CustomOk<Exception>();
            CheckResultOk(ok);

            // Warning result
            var warning = Result.CustomWarn<Exception>("My warning");
            CheckResultWarn(warning, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.CustomWarn<Exception>("My warning", warnException);
            CheckResultWarn(warning, "My warning", warnException);

            // Failure result
            var errorObject = new CustomErrorTest { ErrorCode = -42 };
            var failure = Result.CustomFail("My failure", errorObject);
            CheckResultFail(failure, "My failure", errorObject);

            var failException = new Exception("Failure exception");
            failure = Result.CustomFail("My failure", errorObject, failException);
            CheckResultFail(failure, "My failure", errorObject, failException);
        }

        [Test]
        public void ResultConstructionWithValue()
        {
            // Ok result
            var ok = Result.Ok(42);
            CheckResultOk(ok, 42);

            // Warning result
            var warning = Result.Warn(12, "My warning");
            CheckResultWarn(warning, 12, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.Warn(12, "My warning", warnException);
            CheckResultWarn(warning, 12, "My warning", warnException);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Warn(12, null));
            Assert.Throws<ArgumentNullException>(() => Result.Warn(12, ""));

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckResultFail(failure, "My failure");

            var failException = new Exception("Failure exception");
            failure = Result.Fail<int>("My failure", failException);
            CheckResultFail(failure, "My failure", failException);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int>(null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int>(""));
        }

        [Test]
        public void ResultConstructionWithValueCustomError()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            CheckResultOk(ok, 42);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(12, "My warning");
            CheckResultWarn(warning, 12, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.Warn<int, CustomErrorTest>(42, "My warning", warnException);
            CheckResultWarn(warning, 42, "My warning", warnException);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Warn<int, Exception>(12, null));
            Assert.Throws<ArgumentNullException>(() => Result.Warn<int, Exception>(12, ""));

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -5 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckResultFail(failure, "My failure", customErrorObject);

            var failException = new Exception("Failure exception");
            failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject, failException);
            CheckResultFail(failure, "My failure", customErrorObject, failException);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int, Exception>(null, null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int, Exception>("", null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int, Exception>("My failure", null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void ResultToString()
        {
            // Result without value
            Assert.AreEqual("Success", Result.Ok().ToString());
            Assert.AreEqual("Warning", Result.Warn("Warn").ToString());
            Assert.AreEqual("Failure", Result.Fail("Fail").ToString());

            // Result with value
            Assert.AreEqual("Success", Result.Ok(42).ToString());
            Assert.AreEqual("Warning", Result.Warn(42, "Warn").ToString());
            Assert.AreEqual("Failure", Result.Fail<int>("Fail").ToString());

            var errorObject = new Exception("My failure error object");
            // Result with custom error without value
            Assert.AreEqual("Success", Result.CustomOk<Exception>().ToString());
            Assert.AreEqual("Warning", Result.CustomWarn<Exception>("Warn").ToString());
            Assert.AreEqual("Failure", Result.CustomFail("Fail", errorObject).ToString());

            // Result with value
            Assert.AreEqual("Success", Result.Ok<int, Exception>(42).ToString());
            Assert.AreEqual("Warning", Result.Warn<int, Exception>(42, "Warn").ToString());
            Assert.AreEqual("Failure", Result.Fail<int, Exception>("Fail", errorObject).ToString());
        }
    }
}