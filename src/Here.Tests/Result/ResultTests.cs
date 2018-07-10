using System;
using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultTests : HereTestsBase
    {
        [Test]
        public void ResultConstruction()
        {
            // Ok result
            var ok = Result.Ok();
            Assert.IsTrue(ok.IsSuccess);
            Assert.IsFalse(ok.IsWarning);
            Assert.IsFalse(ok.IsFailure);
            Assert.IsNull(ok.Message);

            // Warning result
            var warning = Result.Warn("My warning");
            Assert.IsTrue(warning.IsSuccess);
            Assert.IsTrue(warning.IsWarning);
            Assert.IsFalse(warning.IsFailure);
            Assert.AreEqual("My warning", warning.Message);

            // failure result
            var failure = Result.Fail("My failure");
            Assert.IsFalse(failure.IsSuccess);
            Assert.IsFalse(failure.IsWarning);
            Assert.IsTrue(failure.IsFailure);
            Assert.AreEqual("My failure", failure.Message);
        }

        [Test]
        public void CustomResultConstruction()
        {
            // Ok result
            var ok = Result.CustomOk<Exception>();
            Assert.IsTrue(ok.IsSuccess);
            Assert.IsFalse(ok.IsWarning);
            Assert.IsFalse(ok.IsFailure);
            Assert.IsNull(ok.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = ok.Error; });

            // Warning result
            var warning = Result.CustomWarn<Exception>("My warning");
            Assert.IsTrue(warning.IsSuccess);
            Assert.IsTrue(warning.IsWarning);
            Assert.IsFalse(warning.IsFailure);
            Assert.AreEqual("My warning", warning.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = warning.Error; });

            // failure result
            var errorObject = new Exception("My failure error object");
            var failure = Result.CustomFail("My failure", errorObject);
            Assert.IsFalse(failure.IsSuccess);
            Assert.IsFalse(failure.IsWarning);
            Assert.IsTrue(failure.IsFailure);
            Assert.AreEqual("My failure", failure.Message);
            Assert.AreSame(errorObject, failure.Error);
        }

        [Test]
        public void ResultConstructionWithValue()
        {
            // Ok result
            var ok = Result.Ok(42);
            Assert.IsTrue(ok.IsSuccess);
            Assert.IsFalse(ok.IsWarning);
            Assert.IsFalse(ok.IsFailure);
            Assert.IsNull(ok.Message);
            Assert.AreEqual(42, ok.Value);

            // Warning result
            var warning = Result.Warn(12, "My warning");
            Assert.IsTrue(warning.IsSuccess);
            Assert.IsTrue(warning.IsWarning);
            Assert.IsFalse(warning.IsFailure);
            Assert.AreEqual("My warning", warning.Message);
            Assert.AreEqual(12, warning.Value);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Warn(12, null));
            Assert.Throws<ArgumentNullException>(() => Result.Warn(12, ""));

            // failure result
            var failure = Result.Fail<int>("My failure");
            Assert.IsFalse(failure.IsSuccess);
            Assert.IsFalse(failure.IsWarning);
            Assert.IsTrue(failure.IsFailure);
            Assert.AreEqual("My failure", failure.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = failure.Value; });

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int>(null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int>(""));
        }

        [Test]
        public void ResultConstructionWithValueCustomError()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            Assert.IsTrue(ok.IsSuccess);
            Assert.IsFalse(ok.IsWarning);
            Assert.IsFalse(ok.IsFailure);
            Assert.IsNull(ok.Message);
            Assert.AreEqual(42, ok.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = ok.Error; });

            // Warning result
            var warning = Result.Warn<int, Exception>(12, "My warning");
            Assert.IsTrue(warning.IsSuccess);
            Assert.IsTrue(warning.IsWarning);
            Assert.IsFalse(warning.IsFailure);
            Assert.AreEqual("My warning", warning.Message);
            Assert.AreEqual(12, warning.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = warning.Error; });

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Warn<int, Exception>(12, null));
            Assert.Throws<ArgumentNullException>(() => Result.Warn<int, Exception>(12, ""));

            // failure result
            var customErrorObject = new Exception("My test exception");
            var failure = Result.Fail<int, Exception>("My failure", customErrorObject);
            Assert.IsFalse(failure.IsSuccess);
            Assert.IsFalse(failure.IsWarning);
            Assert.IsTrue(failure.IsFailure);
            Assert.AreEqual("My failure", failure.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = failure.Value; });
            Assert.AreSame(customErrorObject, failure.Error);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int, Exception>(null, null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int, Exception>("", null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int, Exception>("My failure", null));
        }

        [Test]
        public void ResultImplicitConstruction()
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

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Warn(12, null));
            Assert.Throws<ArgumentNullException>(() => Result.Warn(12, ""));

            // failure result
            var failure = Result.Fail<int>("My failure");
            Result failureWithoutValue = failure;
            Assert.IsFalse(failureWithoutValue.IsSuccess);
            Assert.IsFalse(failureWithoutValue.IsWarning);
            Assert.IsTrue(failureWithoutValue.IsFailure);
            Assert.AreEqual("My failure", failureWithoutValue.Message);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int>(null));
            Assert.Throws<ArgumentNullException>(() => Result.Fail<int>(""));
        }
        // TODO other implicit with error

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