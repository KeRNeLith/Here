using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T, TError}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueCustomResultOnSuccessExtensionsTests : ResultTestsBase
    {
        #region OnSuccess Result<T, TError> => Value

        [Test]
        public void ValueCustomResultOnSuccessToValue()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(98);

            float result = ok.OnSuccess(
                res =>
                {
                    ++counter;
                    return res.Value + 0.1f;
                },
                -1f);
            Assert.AreEqual(1, counter);
            Assert.AreEqual(98.1f, result);

            result = ok.OnSuccess(
                res =>
                {
                    ++counter;
                    return res.Value + 0.2f;
                },
                -1f,
                true);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(98.2f, result);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(99, "My warning");

            result = warning.OnSuccess(
                res =>
                {
                    ++counter;
                    return res.Value + 0.1f;
                },
                -1f);
            Assert.AreEqual(3, counter);
            Assert.AreEqual(99.1f, result);

            result = warning.OnSuccess(
                res =>
                {
                    ++counter;
                    return res.Value + 0.2f;
                },
                -1f,
                true);
            Assert.AreEqual(3, counter);
            Assert.AreEqual(-1f, result);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());

            result = failure.OnSuccess(
                res =>
                {
                    ++counter;
                    return res.Value + 0.1f;
                },
                -2f);
            Assert.AreEqual(3, counter);
            Assert.AreEqual(-2f, result);

            result = failure.OnSuccess(
                res =>
                {
                    ++counter;
                    return res.Value + 0.2f;
                },
                -3f,
                true);
            Assert.AreEqual(3, counter);
            Assert.AreEqual(-3f, result);
        }

        #endregion

        #region OnSuccess Result<T, TError> => Result

        [Test]
        public void ValueCustomResultOkOnSuccessToResult()
        {
            int counter = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);

            // Treat warning as error => false
            // Produce an OK
            Result result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok();
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok();
                },
                true);
            Assert.AreEqual(4, counter);
            CheckResultOk(result);

            // Produce a Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                },
                true);
            Assert.AreEqual(5, counter);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                },
                true);
            Assert.AreEqual(6, counter);
            CheckResultFail(result, produceFailureMessage);
        }

        [Test]
        public void ValueCustomResultWarnOnSuccessToResult()
        {
            int counter = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(25, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok();
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Ok();
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);
        }

        [Test]
        public void ValueCustomResultFailOnSuccessToResult()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -6 };
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>(failureMessage, customErrorObject);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok();
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok();
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);
        }

        #endregion

        #region OnSuccess Result<T, TError> => Result<T>

        [Test]
        public void ValueCustomResultOkOnSuccessToValueResult()
        {
            int counter = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(14);

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                value => 
                {
                    ++counter;
                    return Result.Ok(12);
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 12);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(42, produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 42, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(42);
                },
                true);
            Assert.AreEqual(4, counter);
            CheckResultOk(result, 42);

            // Produce a Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(12, produceWarningMessage);
                },
                true);
            Assert.AreEqual(5, counter);
            CheckResultWarn(result, 12, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                },
                true);
            Assert.AreEqual(6, counter);
            CheckResultFail(result, produceFailureMessage);
        }

        [Test]
        public void ValueCustomResultWarnOnSuccessToValueResult()
        {
            int counter = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(96, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(1);
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 1);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(2, produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 2, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Ok(3);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Warn(4, produceWarningMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);
        }

        [Test]
        public void ValueCustomResultFailOnSuccessToResultValue()
        {
            int counter = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -3 };
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>(failureMessage, customErrorObject);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(45);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(51, produceWarningMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(66);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(88, produceWarningMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);
        }

        #endregion

        #region OnSuccess Result<T, TError> => CustomResult<TError>

        [Test]
        public void ValueCustomResultOkOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(78);

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(7, counterSuccess);
            CheckResultOk(result);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(8, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            // Produce a Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(9, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(10, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(11, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(12, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);
        }

        [Test]
        public void ValueCustomResultWarnOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(66, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(2, counterFailureFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(3, counterFailureFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);
        }

        [Test]
        public void ValueCustomResultFailOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var initialCustomErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var produceCustomErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -3 };

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>(failureMessage, initialCustomErrorObject);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, produceCustomErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, produceCustomErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, produceCustomErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, produceCustomErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);
        }

        #endregion

        #region OnSuccess Result<T, TError> => Result<T, TError>

        [Test]
        public void ValueCustomResultOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -4 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -25 };

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);

            var result = ok.OnSuccess(
                value => { ++counterSuccess; },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result, 12);

            result = ok.OnSuccess(
                value => { ++counterSuccess; },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 12);

            result = ok.OnSuccess(
                value => { ++counterSuccess; },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(3, counterSuccess);
            CheckResultOk(result, 12);

            result = ok.OnSuccess(
                value => { ++counterSuccess; },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 12);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(13, "My warning");

            result = warning.OnSuccess(
                value => { ++counterSuccess; },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultWarn(result, 13, "My warning");

            result = warning.OnSuccess(
                value => { ++counterSuccess; },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 13, "My warning");

            result = warning.OnSuccess(
                value => { ++counterSuccess; },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            result = warning.OnSuccess(
                value => { ++counterSuccess; },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.OnSuccess(
                value => { ++counterSuccess; },
                customErrorObjectFactory);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnSuccess(
                value => { ++counterSuccess; },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnSuccess(
                value => { ++counterSuccess; },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnSuccess(
                value => { ++counterSuccess; },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);
        }

        [Test]
        public void ValueCustomResultTInOnSuccessToValueCustomResultTOut()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -4 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -25 };

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);

            var result = ok.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.1f;
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result, 12.1f);

            result = ok.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.2f;
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 12.2f);

            result = ok.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.3f;
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(3, counterSuccess);
            CheckResultOk(result, 12.3f);

            result = ok.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.4f;
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 12.4f);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(13, "My warning");

            result = warning.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.1f;
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultWarn(result, 13.1f, "My warning");

            result = warning.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.2f;
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 13.2f, "My warning");

            result = warning.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.3f;
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            result = warning.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.4f;
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My warning", customErrorObjectFactory);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.1f;
                },
                customErrorObjectFactory);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.2f;
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.3f;
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.OnSuccess(
                valueInt => 
                {
                    ++counterSuccess;
                    return valueInt + 0.4f;
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);
        }

        [Test]
        public void ValueCustomResultOkOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(25);

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(42);
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result, 42);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(42);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 42);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(12, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, 12, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(12, produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 12, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(42);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(7, counterSuccess);
            CheckResultOk(result, 42);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(42);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(8, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 42);

            // Produce a Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(12, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(9, counterSuccess);
            CheckResultWarn(result, 12, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(12, produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(10, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 12, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(11, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(12, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);
        }

        [Test]
        public void ValueCustomResultWarnOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(45, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(25);
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result, 25);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(25);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultOk(result, 25);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(32, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, 32, produceWarningMessage);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(32, produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultWarn(result, 32, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(81);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(81);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(92, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(92, produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(2, counterFailureFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(3, counterFailureFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);
        }

        [Test]
        public void ValueCustomResultFailOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var initialCustomErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var produceCustomErrorObject = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -3 };

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>(failureMessage, initialCustomErrorObject);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(51);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(51);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(45, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(45, produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, produceCustomErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, produceCustomErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(21);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(21);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(75, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(75, produceWarningMessage);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, produceCustomErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, produceCustomErrorObject);
                },
                () =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, failureMessage, initialCustomErrorObject);
        }

        #endregion
    }
}
