using NUnit.Framework;
using Here.Results;
using Here.Results.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ResultValueOnSuccessExtensionsTests : ResultTestsBase
    {
        #region OnSuccess Result<T> => Result

        [Test]
        public void ValueResultOkOnSuccessToResult()
        {
            int counter = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok(42);

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
        public void ValueResultWarnOnSuccessToResult()
        {
            int counter = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn(42, warningMessage);

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
        public void ValueResultFailOnSuccessToResult()
        {
            int counter = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail<int>(failureMessage);

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

        #region OnSuccess Result<T> => Result<T>

        [Test]
        public void ValueResultOnSuccessToValueResult()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(42);

            var result = ok.OnSuccess(value => { ++counter; });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 42);

            result = ok.OnSuccess(value => { ++counter; }, true);
            Assert.AreEqual(2, counter);
            CheckResultOk(result, 42);

            // Warning result
            var warning = Result.Warn(42, "My warning");

            result = warning.OnSuccess(value => { ++counter; });
            Assert.AreEqual(3, counter);
            CheckResultWarn(result, 42, "My warning");

            result = warning.OnSuccess(value => { ++counter; }, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnSuccess(value => { ++counter; });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnSuccess(value => { ++counter; }, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ValueResultInOnSuccessToValueResultOut()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok(42);

            var result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return 42.2f;
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 42.2f);

            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return 43.2f;
                },
                true);
            Assert.AreEqual(2, counter);
            CheckResultOk(result, 43.2f);

            // Warning result
            var warning = Result.Warn(42, "My warning");

            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return 44.2f;
                });
            Assert.AreEqual(3, counter);
            CheckResultWarn(result, 44.2f, "My warning");

            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return 45.2f;
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail<int>("My failure");

            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return 46.2f;
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return 47.2f;
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");
        }

        [Test]
        public void ValueResultInOkOnSuccessToValueResultOut()
        {
            int counter = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok(42);

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(43);
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 43);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(44, produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 44, produceWarningMessage);

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
                    return Result.Ok(45);
                },
                true);
            Assert.AreEqual(4, counter);
            CheckResultOk(result, 45);

            // Produce a Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(46, produceWarningMessage);
                },
                true);
            Assert.AreEqual(5, counter);
            CheckResultWarn(result, 46, produceWarningMessage);

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
        public void ValueResultInWarnOnSuccessToValueResultOut()
        {
            int counter = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn(42, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(43);
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 43);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(44, produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 44, produceWarningMessage);

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
                    return Result.Ok(45);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counter;
                    return Result.Warn(46, produceWarningMessage);
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
        public void ValueResultInFailOnSuccessToValueResultOut()
        {
            int counter = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail<int>(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Ok(42);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(43, produceWarningMessage);
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
                    return Result.Ok(44);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counter;
                    return Result.Warn(45, produceWarningMessage);
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

        #region OnSuccess Result<T> => CustomResult<TError>

        [Test]
        public void ValueResultOkOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterErrorFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -26 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -27 };
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok(42);

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(2, counterSuccess);
            CheckResultOk(result);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(3, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(4, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(5, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(7, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(8, counterSuccess);
            CheckResultOk(result);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(9, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(10, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(11, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(12, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);
        }

        [Test]
        public void ValueResultWarnOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterErrorFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -26 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -24 };
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn(42, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(2, counterSuccess);
            CheckResultOk(result);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(3, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, produceWarningMessage);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(5, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

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

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

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

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(3, counterErrorFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

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
        }

        [Test]
        public void ValueResultFailOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterErrorFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -26 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -24 };
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail<int>(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(3, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(4, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(5, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(6, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);
        }

        #endregion

        #region OnSuccess Result<T> => Result<T, TError>

        [Test]
        public void ValueResultOkOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterErrorFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -26 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -27 };
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok(42);

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(12);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 12);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(13);
                },
                customErrorObjectFactory);
            Assert.AreEqual(2, counterSuccess);
            CheckResultOk(result, 13);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(14, produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(3, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 14, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(15, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(4, counterSuccess);
            CheckResultWarn(result, 15, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(5, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(16);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(7, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 16);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(17);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(8, counterSuccess);
            CheckResultOk(result, 17);

            // Produce an Warn
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(18, produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(9, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 18, produceWarningMessage);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(19, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(10, counterSuccess);
            CheckResultWarn(result, 19, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(11, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(12, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);
        }

        [Test]
        public void ValueResultWarnOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterErrorFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -26 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -24 };
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn(42, warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(1);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 1);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(2);
                },
                customErrorObjectFactory);
            Assert.AreEqual(2, counterSuccess);
            CheckResultOk(result, 2);

            // Produce a Warn
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(3, produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(3, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 3, produceWarningMessage);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(4, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(4, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 4, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(5, counterSuccess);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = warning.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(5);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(6);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(7, produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(8, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(3, counterErrorFactory);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

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
        }

        [Test]
        public void ValueResultFailOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterErrorFactory = 0;
            var customErrorObject = new CustomErrorTest { ErrorCode = -26 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -24 };
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail<int>(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(0);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(1);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(2, produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(3, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(3, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(4);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(4, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(5);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(6, produceWarningMessage);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(5, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(7, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(6, counterErrorFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                value =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);
        }

        #endregion
    }
}
