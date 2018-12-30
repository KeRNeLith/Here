using System;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ValueResultOnSuccessExtensionsTests : ResultTestsBase
    {
        #region OnSuccess Result<T> => Value

        [Test]
        public void ValueResultOnSuccessToValue()
        {
            #region Local functions

            void CheckOnSuccess(Result<int> result, bool treatWarningAsError, bool expectSuccess)
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

            void CheckOnSuccessFunc(Result<int> result, bool treatWarningAsError, bool expectSuccess)
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
            var ok = Result.Ok(26);
            CheckOnSuccess(ok, false, true);
            CheckOnSuccess(ok, true, true);
            CheckOnSuccessFunc(ok, false, true);
            CheckOnSuccessFunc(ok, true, true);

            // Warning result
            var warning = Result.Warn(32, "My warning");
            CheckOnSuccess(warning, false, true);
            CheckOnSuccess(warning, true, false);
            CheckOnSuccessFunc(warning, false, true);
            CheckOnSuccessFunc(warning, true, false);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnSuccess(failure, false, false);
            CheckOnSuccess(failure, true, false);
            CheckOnSuccessFunc(failure, false, false);
            CheckOnSuccessFunc(failure, true, false);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result<int>, float>)null, 12.5f));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result<int>, float>)null, r => 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result<int>, float>)null, null));
        }

        #endregion

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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(null));
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
            #region Local function

            void CheckOnSuccess(Result<int> result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                Result<int> res = result.OnSuccess(
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
            var ok = Result.Ok(42);
            CheckOnSuccess(ok, false, true);
            CheckOnSuccess(ok, true, true);

            // Warning result
            var warning = Result.Warn(42, "My warning");
            CheckOnSuccess(warning, false, true);
            CheckOnSuccess(warning, true, false);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckOnSuccess(failure, false, false);
            CheckOnSuccess(failure, true, false);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Action<int>)null));
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, float>)null));
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<float>>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, customErrorObject));
            
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, r => customErrorObjectFactory));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => CustomResult<CustomErrorTest>.ResultOk, (Func<Result<int>, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, CustomResult<CustomErrorTest>>)null, (Func<Result<int>, CustomErrorTest>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<int, CustomErrorTest>>)null, customErrorObject));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<int, CustomErrorTest>>)null, r => customErrorObjectFactory));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(val => Result.Ok<int, CustomErrorTest>(12), (Func<Result<int>, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<int, Result<int, CustomErrorTest>>)null, (Func<Result<int>, CustomErrorTest>)null));
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
                r =>
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
