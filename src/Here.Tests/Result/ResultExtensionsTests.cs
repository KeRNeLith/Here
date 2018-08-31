using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ResultExtensionsTests : ResultTestsBase
    {
        [Test]
        public void ResultEnsure()
        {
            string errorMessage = "My error message";
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.Ensure(
                () =>
                {
                    ++counter;
                    return true;
                },
                errorMessage);
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            result = ok.Ensure(
                () =>
                {
                    ++counter;
                    return false;
                },
                errorMessage);
            Assert.AreEqual(2, counter);
            CheckResultFail(result, errorMessage);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.Ensure(
                () =>
                {
                    ++counter;
                    return true;
                },
                errorMessage);
            Assert.AreEqual(3, counter);
            CheckResultWarn(result, "My warning");

            result = warning.Ensure(
                () =>
                {
                    ++counter;
                    return false;
                },
                errorMessage);
            Assert.AreEqual(4, counter);
            CheckResultFail(result, errorMessage);

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.Ensure(
                () =>
                {
                    ++counter;
                    return true;
                },
                errorMessage);
            Assert.AreEqual(4, counter);
            CheckResultFail(result, "My failure");

            result = failure.Ensure(
                () =>
                {
                    ++counter;
                    return false;
                },
                errorMessage);
            Assert.AreEqual(4, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ValueResultEnsure()
        {
            string errorMessage = "My error message";
            int counter = 0;
            // Ok result
            var ok = Result.Ok(42);

            var result = ok.Ensure(
                value =>
                {
                    ++counter;
                    return true;
                },
                errorMessage);
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            result = ok.Ensure(
                value =>
                {
                    ++counter;
                    return false;
                },
                errorMessage);
            Assert.AreEqual(2, counter);
            CheckResultFail(result, errorMessage);

            // Warning result
            var warning = Result.Warn(15, "My warning");

            result = warning.Ensure(
                value =>
                {
                    ++counter;
                    return true;
                },
                errorMessage);
            Assert.AreEqual(3, counter);
            CheckResultWarn(result, "My warning");

            result = warning.Ensure(
                value =>
                {
                    ++counter;
                    return false;
                },
                errorMessage);
            Assert.AreEqual(4, counter);
            CheckResultFail(result, errorMessage);

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.Ensure(
                value =>
                {
                    ++counter;
                    return true;
                },
                errorMessage);
            Assert.AreEqual(4, counter);
            CheckResultFail(result, "My failure");

            result = failure.Ensure(
                value =>
                {
                    ++counter;
                    return false;
                },
                errorMessage);
            Assert.AreEqual(4, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void CustomResultEnsure()
        {
            string errorMessage = "My error message";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };
            int counterPredicate = 0;
            int counterErrorFactory = 0;
            // Ok result
            var ok = Result.CustomOk<CustomErrorTest>();

            var result = ok.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(1, counterPredicate);
            CheckResultOk(result);

            result = ok.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterPredicate);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result);

            result = ok.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(3, counterPredicate);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            result = ok.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterPredicate);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            // Warning result
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(5, counterPredicate);
            CheckResultWarn(result, "My warning");

            result = warning.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterPredicate);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultWarn(result, "My warning");

            result = warning.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(7, counterPredicate);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            result = warning.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(8, counterPredicate);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            // Failure result
            var failure = Result.CustomFail<CustomErrorTest>("My failure", customErrorObject);

            result = failure.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(8, counterPredicate);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(8, counterPredicate);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(8, counterPredicate);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.Ensure(
                () =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(8, counterPredicate);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObject);
        }

        [Test]
        public void CustomValueResultEnsure()
        {
            string errorMessage = "My error message";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };
            int counterPredicate = 0;
            int counterErrorFactory = 0;
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(42);

            var result = ok.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(1, counterPredicate);
            CheckResultOk(result, 42);

            result = ok.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterPredicate);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 42);

            result = ok.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(3, counterPredicate);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            result = ok.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterPredicate);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(15, "My warning");

            result = warning.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(5, counterPredicate);
            CheckResultWarn(result, 15, "My warning");

            result = warning.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterPredicate);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultWarn(result, 15, "My warning");

            result = warning.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(7, counterPredicate);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            result = warning.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(8, counterPredicate);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, errorMessage, customErrorObjectFactory);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(8, counterPredicate);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return true;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(8, counterPredicate);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                customErrorObjectFactory);
            Assert.AreEqual(8, counterPredicate);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.Ensure(
                value =>
                {
                    ++counterPredicate;
                    return false;
                },
                errorMessage,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(8, counterPredicate);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObject);
        }
    }
}
