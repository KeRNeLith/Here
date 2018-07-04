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
            Assert.Throws<InvalidOperationException>(() => { var _ = ok.Message; });

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
        public void ResultConstructionWithValue()
        {
            // Ok result
            var ok = Result.Ok(42);
            Assert.IsTrue(ok.IsSuccess);
            Assert.IsFalse(ok.IsWarning);
            Assert.IsFalse(ok.IsFailure);
            Assert.Throws<InvalidOperationException>(() => { var _ = ok.Message; });
            Assert.AreEqual(42, ok.Value);

            // Warning result
            var warning = Result.Warn(12, "My warning");
            Assert.IsTrue(warning.IsSuccess);
            Assert.IsTrue(warning.IsWarning);
            Assert.IsFalse(warning.IsFailure);
            Assert.AreEqual("My warning", warning.Message);
            Assert.AreEqual(12, warning.Value);

            // failure result
            var failure = Result.Fail<int>("My failure");
            Assert.IsFalse(failure.IsSuccess);
            Assert.IsFalse(failure.IsWarning);
            Assert.IsTrue(failure.IsFailure);
            Assert.AreEqual("My failure", failure.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = failure.Value; });
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
            Assert.Throws<InvalidOperationException>(() => { var _ = okWithoutValue.Message; });

            // Warning result
            var warning = Result.Warn(12, "My warning");
            Result warningWithoutValue = warning;
            Assert.IsTrue(warningWithoutValue.IsSuccess);
            Assert.IsTrue(warningWithoutValue.IsWarning);
            Assert.IsFalse(warningWithoutValue.IsFailure);
            Assert.AreEqual("My warning", warningWithoutValue.Message);

            // failure result
            var failure = Result.Fail<int>("My failure");
            Result failureWithoutValue = failure;
            Assert.IsFalse(failureWithoutValue.IsSuccess);
            Assert.IsFalse(failureWithoutValue.IsWarning);
            Assert.IsTrue(failureWithoutValue.IsFailure);
            Assert.AreEqual("My failure", failureWithoutValue.Message);
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
        }
    }
}