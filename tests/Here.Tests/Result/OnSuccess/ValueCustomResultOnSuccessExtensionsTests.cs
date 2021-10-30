using System;
using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Results.ResultTestHelpers;

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
            #region Local functions

            void CheckOnSuccess(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                float res = result.OnSuccess(
                    r =>
                    {
                        ++counterSuccess;
                        return 42.5f;
                    },
                    -1f,
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 42.5f : -1f, res);
            }

            void CheckOnSuccessFunc(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                int counterFactory = 0;
                float res = result.OnSuccess(
                    r =>
                    {
                        ++counterSuccess;
                        return 43.5f;
                    },
                    r =>
                    {
                        ++counterFactory;
                        return -2f;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(expectSuccess ? 0 : 1, counterFactory);
                Assert.AreEqual(expectSuccess ? 43.5f : -2f, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(98);
            CheckOnSuccess(ok, false, true);
            CheckOnSuccess(ok, true, true);
            CheckOnSuccessFunc(ok, false, true);
            CheckOnSuccessFunc(ok, true, true);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(99, "My warning");
            CheckOnSuccess(warning, false, true);
            CheckOnSuccess(warning, true, false);
            CheckOnSuccessFunc(warning, false, true);
            CheckOnSuccessFunc(warning, true, false);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            CheckOnSuccess(failure, false, false);
            CheckOnSuccess(failure, true, false);
            CheckOnSuccessFunc(failure, false, false);
            CheckOnSuccessFunc(failure, true, false);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(null, 12.5f));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(null, r => 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(r => 12.5f, (Func<Result<int, CustomErrorTest>, float>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, float>)null, (Func<Result<int, CustomErrorTest>, float>)null));
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(null));
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<float>>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(12, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => Result.CustomOk<CustomErrorTest>(), (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, (CustomErrorTest)null));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, r => customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => Result.CustomOk<CustomErrorTest>(), (Func<Result<int, CustomErrorTest>, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, (Func<Result<int, CustomErrorTest>, CustomErrorTest>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
            #region Local function

            void CheckOnSuccess(Result<int, CustomErrorTest> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                Result<int, CustomErrorTest> res = result.OnSuccess(
                    val =>
                    {
                        ++counterSuccess;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            CheckOnSuccess(ok, false, true);
            CheckOnSuccess(ok, true, true);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(13, "My warning");
            CheckOnSuccess(warning, false, true);
            CheckOnSuccess(warning, true, false);

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -4 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckOnSuccess(failure, false, false);
            CheckOnSuccess(failure, true, false);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Action<int>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(6, counterSuccess);
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, float>)null, customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => val + 0.1f, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, float>)null, (CustomErrorTest)null));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, float>)null, r => customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => val + 0.1f, (Func<Result<int, CustomErrorTest>, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, float>)null, (Func<Result<int, CustomErrorTest>, CustomErrorTest>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                },
                true);
            Assert.AreEqual(12, counterSuccess);
            Assert.AreEqual(0, counterFailureFactory);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<float, CustomErrorTest>>)null, customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => Result.Ok<float, CustomErrorTest>(12.5f), (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<float, CustomErrorTest>>)null, (CustomErrorTest)null));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<float, CustomErrorTest>>)null, r => customErrorObjectFactory));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => Result.Ok<float, CustomErrorTest>(12.5f), (Func<Result<int, CustomErrorTest>, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<float, CustomErrorTest>>)null, (Func<Result<int, CustomErrorTest>, CustomErrorTest>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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