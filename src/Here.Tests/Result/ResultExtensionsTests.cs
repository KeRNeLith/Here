using System;
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
        #region Unwrapping

        [Test]
        public void ValueResultUnwrapping()
        {
            // Results
            var valueResultOk = Result.Ok(12);
            var valueResultWarn = Result.Warn(42, "My warning");
            var valueResultFail = Result.Fail<int>("My failure");

            var customValueResultOk = Result.Ok<int, CustomErrorTest>(12);
            var customValueResultWarn = Result.Warn<int, CustomErrorTest>(42, "My warning");
            var customValueResultFail = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());

            // Unwraps
            Assert.AreEqual(12, valueResultOk.Unwrap());
            Assert.AreEqual(12, valueResultOk.Unwrap(123));
            Assert.AreEqual(12, valueResultOk.Unwrap(() => 456));
            Assert.AreEqual(12.5f, valueResultOk.Unwrap(val => val + 0.5f));
            Assert.AreEqual(12.5f, valueResultOk.Unwrap(val => val + 0.5f, 789f));
            Assert.AreEqual(12.5f, valueResultOk.Unwrap(val => val + 0.5f, () => 101112f));

            Assert.AreEqual(42, valueResultWarn.Unwrap());
            Assert.AreEqual(42, valueResultWarn.Unwrap(123));
            Assert.AreEqual(42, valueResultWarn.Unwrap(() => 456));
            Assert.AreEqual(42.5f, valueResultWarn.Unwrap(val => val + 0.5f));
            Assert.AreEqual(42.5f, valueResultWarn.Unwrap(val => val + 0.5f, 789f));
            Assert.AreEqual(42.5f, valueResultWarn.Unwrap(val => val + 0.5f, () => 101112f));

            Assert.AreEqual(0, valueResultFail.Unwrap());
            Assert.AreEqual(123, valueResultFail.Unwrap(123));
            Assert.AreEqual(456, valueResultFail.Unwrap(() => 456));
            Assert.AreEqual(0f, valueResultFail.Unwrap(val => val + 0.5f));
            Assert.AreEqual(789f, valueResultFail.Unwrap(val => val + 0.5f, 789f));
            Assert.AreEqual(101112f, valueResultFail.Unwrap(val => val + 0.5f, () => 101112f));


            Assert.AreEqual(12, customValueResultOk.Unwrap());
            Assert.AreEqual(12, customValueResultOk.Unwrap(123));
            Assert.AreEqual(12, customValueResultOk.Unwrap(() => 456));
            Assert.AreEqual(12.5f, customValueResultOk.Unwrap(val => val + 0.5f));
            Assert.AreEqual(12.5f, customValueResultOk.Unwrap(val => val + 0.5f, 789f));
            Assert.AreEqual(12.5f, customValueResultOk.Unwrap(val => val + 0.5f, () => 101112f));

            Assert.AreEqual(42, customValueResultWarn.Unwrap());
            Assert.AreEqual(42, customValueResultWarn.Unwrap(123));
            Assert.AreEqual(42, customValueResultWarn.Unwrap(() => 456));
            Assert.AreEqual(42.5f, customValueResultWarn.Unwrap(val => val + 0.5f));
            Assert.AreEqual(42.5f, customValueResultWarn.Unwrap(val => val + 0.5f, 789f));
            Assert.AreEqual(42.5f, customValueResultWarn.Unwrap(val => val + 0.5f, () => 101112f));

            Assert.AreEqual(0, customValueResultFail.Unwrap());
            Assert.AreEqual(123, customValueResultFail.Unwrap(123));
            Assert.AreEqual(456, customValueResultFail.Unwrap(() => 456));
            Assert.AreEqual(0f, customValueResultFail.Unwrap(val => val + 0.5f));
            Assert.AreEqual(789f, customValueResultFail.Unwrap(val => val + 0.5f, 789f));
            Assert.AreEqual(101112f, customValueResultFail.Unwrap(val => val + 0.5f, () => 101112f));
        }

        #endregion

        #region Ensure

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
            var failure = Result.CustomFail("My failure", customErrorObject);

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

        #endregion

        #region Flatten

        [Test]
        public void ValueResultFlatten()
        {
            string warningMessage = "My warning message";
            string warningDeepMessage = "My deep warning message";
            string errorMessage = "My error message";
            string errorDeepMessage = "My deep error message";

            Exception exception = new Exception("Exception");
            Exception deepException = new Exception("Deep exception");

            // Ok embed XXX
            Result<Result<int>> successEmbedSuccess = Result.Ok(Result.Ok(2));
            CheckResultOk(successEmbedSuccess.Flatten(), 2);

            Result<int> result = successEmbedSuccess;
            CheckResultOk(successEmbedSuccess.Flatten(), 2);


            Result<Result<int>> successEmbedWarn = Result.Ok(Result.Warn(12, warningDeepMessage));
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage);
            result = successEmbedWarn;
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage);

            successEmbedWarn = Result.Ok(Result.Warn(12, warningDeepMessage, deepException));
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage, deepException);
            result = successEmbedWarn;
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage, deepException);


            Result<Result<int>> successEmbedFail = Result.Ok(Result.Fail<int>(errorDeepMessage));
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage);
            result = successEmbedFail;
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage);

            successEmbedFail = Result.Ok(Result.Fail<int>(errorDeepMessage, deepException));
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage, deepException);
            result = successEmbedFail;
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage, deepException);

            // Warn embed XXX
            Result<Result<int>> warnEmbedSuccess = Result.Warn(Result.Ok(22), warningMessage);
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage);
            result = warnEmbedSuccess;
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage);

            warnEmbedSuccess = Result.Warn(Result.Ok(22), warningMessage, exception);
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage, exception);
            result = warnEmbedSuccess;
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage, exception);


            Result<Result<int>> warnEmbedWarn = Result.Warn(Result.Warn(32, warningDeepMessage), warningMessage);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}");
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}");

            warnEmbedWarn = Result.Warn(Result.Warn(32, warningDeepMessage), warningMessage, exception);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", exception);
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", exception);

            warnEmbedWarn = Result.Warn(Result.Warn(32, warningDeepMessage, deepException), warningMessage);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);

            warnEmbedWarn = Result.Warn(Result.Warn(32, warningDeepMessage, deepException), warningMessage, exception);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);    // Keeps only the deepest exception
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);    // Keeps only the deepest exception


            Result<Result<int>> warnEmbedFail = Result.Warn(Result.Fail<int>(errorDeepMessage), warningMessage);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}");
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}");

            warnEmbedFail = Result.Warn(Result.Fail<int>(errorDeepMessage), warningMessage, exception);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", exception);
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", exception);

            warnEmbedFail = Result.Warn(Result.Fail<int>(errorDeepMessage, deepException), warningMessage);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);

            warnEmbedFail = Result.Warn(Result.Fail<int>(errorDeepMessage, deepException), warningMessage, exception);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);          // Keeps only the deepest exception
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);          // Keeps only the deepest exception

            // Fail
            Result<Result<int>> fail = Result.Fail<Result<int>>(errorMessage);
            CheckResultFail(fail.Flatten(), errorMessage);
            result = fail;
            CheckResultFail(fail.Flatten(), errorMessage);
            fail = Result.Fail<Result<int>>(errorMessage, exception);
            CheckResultFail(fail.Flatten(), errorMessage, exception);
            result = fail;
            CheckResultFail(fail.Flatten(), errorMessage, exception);
        }

        [Test]
        public void CustomValueResultFlatten()
        {
            string warningMessage = "My warning message";
            string warningDeepMessage = "My deep warning message";
            string errorMessage = "My error message";
            string errorDeepMessage = "My deep error message";

            Exception exception = new Exception("Exception");
            Exception deepException = new Exception("Deep exception");

            CustomErrorTest customErrorObject = new CustomErrorTest { ErrorCode = 1 };
            CustomErrorTest deepCustomErrorObject = new CustomErrorTest { ErrorCode = 2 };

            // Ok embed XXX
            Result<Result<int, CustomErrorTest>, CustomErrorTest> successEmbedSuccess = 
                Result.Ok<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Ok<int, CustomErrorTest>(2));
            CheckResultOk(successEmbedSuccess.Flatten(), 2);

            Result<int, CustomErrorTest> result = successEmbedSuccess;
            CheckResultOk(successEmbedSuccess.Flatten(), 2);


            Result<Result<int, CustomErrorTest>, CustomErrorTest> successEmbedWarn = 
                Result.Ok<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Warn<int, CustomErrorTest>(12, warningDeepMessage));
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage);
            result = successEmbedWarn;
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage);

            successEmbedWarn = 
                Result.Ok<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Warn<int, CustomErrorTest>(12, warningDeepMessage, deepException));
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage, deepException);
            result = successEmbedWarn;
            CheckResultWarn(successEmbedWarn.Flatten(), 12, warningDeepMessage, deepException);


            Result<Result<int, CustomErrorTest>, CustomErrorTest> successEmbedFail = 
                Result.Ok<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Fail<int, CustomErrorTest>(errorDeepMessage, deepCustomErrorObject));
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage, deepCustomErrorObject);
            result = successEmbedFail;
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage, deepCustomErrorObject);

            successEmbedFail = 
                Result.Ok<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Fail<int, CustomErrorTest>(errorDeepMessage, deepCustomErrorObject, deepException));
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage, deepCustomErrorObject, deepException);
            result = successEmbedFail;
            CheckResultFail(successEmbedFail.Flatten(), errorDeepMessage, deepCustomErrorObject, deepException);

            // Warn embed XXX
            Result<Result<int, CustomErrorTest>, CustomErrorTest> warnEmbedSuccess = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Ok<int, CustomErrorTest>(22), warningMessage);
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage);
            result = warnEmbedSuccess;
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage);

            warnEmbedSuccess = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Ok<int, CustomErrorTest>(22), warningMessage, exception);
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage, exception);
            result = warnEmbedSuccess;
            CheckResultWarn(warnEmbedSuccess.Flatten(), 22, warningMessage, exception);


            Result<Result<int, CustomErrorTest>, CustomErrorTest> warnEmbedWarn = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Warn<int, CustomErrorTest>(32, warningDeepMessage), warningMessage);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}");
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}");

            warnEmbedWarn = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Warn<int, CustomErrorTest>(32, warningDeepMessage), warningMessage, exception);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", exception);
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", exception);

            warnEmbedWarn = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Warn<int, CustomErrorTest>(32, warningDeepMessage, deepException), warningMessage);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);

            warnEmbedWarn = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Warn<int, CustomErrorTest>(32, warningDeepMessage, deepException), warningMessage, exception);
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);    // Keeps only the deepest exception
            result = warnEmbedWarn;
            CheckResultWarn(warnEmbedWarn.Flatten(), 32, $"{warningDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepException);    // Keeps only the deepest exception


            Result<Result<int, CustomErrorTest>, CustomErrorTest> warnEmbedFail = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Fail<int, CustomErrorTest>(errorDeepMessage, deepCustomErrorObject), warningMessage);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject);
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject);

            warnEmbedFail = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Fail<int, CustomErrorTest>(errorDeepMessage, deepCustomErrorObject), warningMessage, exception);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject, exception);
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject, exception);

            warnEmbedFail = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Fail<int, CustomErrorTest>(errorDeepMessage, deepCustomErrorObject, deepException), warningMessage);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject, deepException);
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject, deepException);

            warnEmbedFail = 
                Result.Warn<Result<int, CustomErrorTest>, CustomErrorTest>(Result.Fail<int, CustomErrorTest>(errorDeepMessage, deepCustomErrorObject, deepException), warningMessage, exception);
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject, deepException);   // Keeps only the deepest exception and error object
            result = warnEmbedFail;
            CheckResultFail(warnEmbedFail.Flatten(), $"{errorDeepMessage}{Environment.NewLine}Resulting in: {warningMessage}", deepCustomErrorObject, deepException);   // Keeps only the deepest exception and error object

            // Fail
            Result<Result<int, CustomErrorTest>, CustomErrorTest> fail = Result.Fail<Result<int, CustomErrorTest>, CustomErrorTest>(errorMessage, customErrorObject);
            CheckResultFail(fail.Flatten(), errorMessage, customErrorObject);
            result = fail;
            CheckResultFail(fail.Flatten(), errorMessage, customErrorObject);
            fail = Result.Fail<Result<int, CustomErrorTest>, CustomErrorTest>(errorMessage, customErrorObject, exception);
            CheckResultFail(fail.Flatten(), errorMessage, customErrorObject, exception);
            result = fail;
            CheckResultFail(fail.Flatten(), errorMessage, customErrorObject, exception);
        }

        #endregion
    }
}
