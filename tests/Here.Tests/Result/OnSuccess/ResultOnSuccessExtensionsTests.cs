using System;
using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Results.ResultTestHelpers;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/> extensions.
    /// </summary>
    [TestFixture]
    internal class ResultOnSuccessExtensionsTests : ResultTestsBase
    {
        #region OnSuccess Result => Value

        [Test]
        public void ResultOnSuccessToValue()
        {
            #region Local functions

            void CheckOnSuccess(Result result, bool treatWarningAsError, bool expectSuccess)
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

            void CheckOnSuccessFunc(Result result, bool treatWarningAsError, bool expectSuccess)
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
            var ok = Result.Ok();
            CheckOnSuccess(ok, false, true);
            CheckOnSuccess(ok, true, true);
            CheckOnSuccessFunc(ok, false, true);
            CheckOnSuccessFunc(ok, true, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnSuccess(warning, false, true);
            CheckOnSuccess(warning, true, false);
            CheckOnSuccessFunc(warning, false, true);
            CheckOnSuccessFunc(warning, true, false);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnSuccess(failure, false, false);
            CheckOnSuccess(failure, true, false);
            CheckOnSuccessFunc(failure, false, false);
            CheckOnSuccessFunc(failure, true, false);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, float>)null, 12.5f));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, float>)null, r => 12.5f));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, float>)null, null));
        }

        #endregion

        #region OnSuccess Result => Result

        [Test]
        public void ResultOnSuccessToResult()
        {
            #region Local functions

            void CheckOnSuccess(Result result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                Result res = result.OnSuccess(
                    r =>
                    {
                        ++counterSuccess;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(result, res);
            }

            void CheckOnSuccessNoInput(Result result, bool treatWarningAsError, bool expectSuccess)
            {
                int counterSuccess = 0;
                Result res = result.OnSuccess(
                    () =>
                    {
                        ++counterSuccess;
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectSuccess ? 1 : 0, counterSuccess);
                Assert.AreEqual(result, res);
            }

            #endregion

            // Ok result
            var ok = Result.Ok();
            CheckOnSuccess(ok, false, true);
            CheckOnSuccess(ok, true, true);
            CheckOnSuccessNoInput(ok, false, true);
            CheckOnSuccessNoInput(ok, true, true);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckOnSuccess(warning, false, true);
            CheckOnSuccess(warning, true, false);
            CheckOnSuccessNoInput(warning, false, true);
            CheckOnSuccessNoInput(warning, true, false);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckOnSuccess(failure, false, false);
            CheckOnSuccess(failure, true, false);
            CheckOnSuccessNoInput(failure, false, false);
            CheckOnSuccessNoInput(failure, true, false);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Action)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Action<Result>)null));
        }

        [Test]
        public void ResultOkOnSuccessToResult()
        {
            int counter = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok();

            // Treat warning as error => false
            // Produce an OK
            Result result = ok.OnSuccess(
                r => 
                {
                    ++counter;
                    return Result.Ok();
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Produce an Warn
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok();
                },
                true);
            Assert.AreEqual(4, counter);
            CheckResultOk(result);

            // Produce a Warn
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                },
                true);
            Assert.AreEqual(5, counter);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                r =>
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
        public void ResultWarnOnSuccessToResult()
        {
            int counter = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn(warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok();
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result);

            // Produce a Warn
            result = warning.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
                {
                    ++counter;
                    return Result.Ok();
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);
        }

        [Test]
        public void ResultFailOnSuccessToResult()
        {
            int counter = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok();
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok();
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(produceWarningMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail(produceFailureMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);
        }

        #endregion

        #region OnSuccess Result => Result<T>

        [Test]
        public void ResultOnSuccessToValueResult()
        {
            int counter = 0;
            // Ok result
            var ok = Result.Ok();

            var result = ok.OnSuccess(r => ++counter);
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 1);

            result = ok.OnSuccess(r => ++counter, true);
            Assert.AreEqual(2, counter);
            CheckResultOk(result, 2);

            // Warning result
            var warning = Result.Warn("My warning");

            result = warning.OnSuccess(r => ++counter);
            Assert.AreEqual(3, counter);
            CheckResultWarn(result, 3, "My warning");

            result = warning.OnSuccess(r => ++counter, true);  // Warning becomes a fail
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My warning");

            // Failure result
            var failure = Result.Fail("My failure");

            result = failure.OnSuccess(r => ++counter);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");

            result = failure.OnSuccess(r => ++counter, true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, "My failure");

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, float>)null));
        }

        [Test]
        public void ResultOkOnSuccessToValueResult()
        {
            int counter = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Ok result
            var ok = Result.Ok();

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                r => 
                {
                    ++counter;
                    return Result.Ok(12);
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 12);

            // Produce an Warn
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(42, produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 42, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok(42);
                },
                true);
            Assert.AreEqual(4, counter);
            CheckResultOk(result, 42);

            // Produce a Warn
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(12, produceWarningMessage);
                },
                true);
            Assert.AreEqual(5, counter);
            CheckResultWarn(result, 12, produceWarningMessage);

            // Produce a Fail
            result = ok.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                },
                true);
            Assert.AreEqual(6, counter);
            CheckResultFail(result, produceFailureMessage);

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, Result<float>>)null));
        }

        [Test]
        public void ResultWarnOnSuccessToValueResult()
        {
            int counter = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Warning result
            var warning = Result.Warn(warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok(1);
                });
            Assert.AreEqual(1, counter);
            CheckResultOk(result, 1);

            // Produce a Warn
            result = warning.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(2, produceWarningMessage);
                });
            Assert.AreEqual(2, counter);
            CheckResultWarn(result, 2, produceWarningMessage);

            // Produce a Fail
            result = warning.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                });
            Assert.AreEqual(3, counter);
            CheckResultFail(result, produceFailureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
                {
                    ++counter;
                    return Result.Ok(3);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Warn
            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
                {
                    ++counter;
                    return Result.Warn(4, produceWarningMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);

            // Produce a Fail
            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                },
                true);
            Assert.AreEqual(3, counter);
            CheckResultFail(result, warningMessage);
        }

        [Test]
        public void ResultFailOnSuccessToValueResult()
        {
            int counter = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";

            // Failure result
            var failure = Result.Fail(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok(45);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(51, produceWarningMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                });
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Ok(66);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Warn(88, produceWarningMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counter;
                    return Result.Fail<int>(produceFailureMessage);
                },
                true);
            Assert.AreEqual(0, counter);
            CheckResultFail(result, failureMessage);
        }

        #endregion

        #region OnSuccess Result => CustomResult<TError>

        [Test]
        public void ResultOkOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Ok result
            var ok = Result.Ok();

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(7, counterSuccess);
            CheckResultOk(result);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(9, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(11, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                r =>
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, CustomResult<CustomErrorTest>>)null, customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(r => CustomResult<CustomErrorTest>.ResultOk, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, CustomResult<CustomErrorTest>>)null, (CustomErrorTest)null));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, CustomResult<CustomErrorTest>>)null, r => customErrorObjectFactory));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(r => CustomResult<CustomErrorTest>.ResultOk, (Func<Result, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, CustomResult<CustomErrorTest>>)null, (Func<Result, CustomErrorTest>)null));
        }

        [Test]
        public void ResultWarnOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Warning result
            var warning = Result.Warn(warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result);

            result = warning.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, produceWarningMessage);

            result = warning.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = warning.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
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
        public void ResultFailOnSuccessToCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Failure result
            var failure = Result.Fail(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(2, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                r =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(3, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomOk<CustomErrorTest>();
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(4, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomWarn<CustomErrorTest>(produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(5, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.CustomFail(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(6, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);
        }

        #endregion

        #region OnSuccess Result => Result<T, TError>

        [Test]
        public void ResultOkOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Ok result
            var ok = Result.Ok();

            // Treat warning as error => false
            // Produce an OK
            var result = ok.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(42);
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result, 42);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(12, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, 12, produceWarningMessage);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(42);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(7, counterSuccess);
            CheckResultOk(result, 42);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(12, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(9, counterSuccess);
            CheckResultWarn(result, 12, produceWarningMessage);

            result = ok.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(11, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = ok.OnSuccess(
                r =>
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

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, Result<int, CustomErrorTest>>)null, customErrorObject));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(r => Result.Ok<int, CustomErrorTest>(12), (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, Result<int, CustomErrorTest>>)null, (CustomErrorTest)null));

            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, Result<int, CustomErrorTest>>)null, r => customErrorObjectFactory));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess(r => Result.Ok<int, CustomErrorTest>(12), (Func<Result, CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.OnSuccess((Func<Result, Result<int, CustomErrorTest>>)null, (Func<Result, CustomErrorTest>)null));
        }

        [Test]
        public void ResultWarnOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string warningMessage = "My warning";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Warning result
            var warning = Result.Warn(warningMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = warning.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(25);
                },
                customErrorObjectFactory);
            Assert.AreEqual(1, counterSuccess);
            CheckResultOk(result, 25);

            result = warning.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(32, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(3, counterSuccess);
            CheckResultWarn(result, 32, produceWarningMessage);

            result = warning.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(5, counterSuccess);
            CheckResultFail(result, produceFailureMessage, customErrorObject);

            result = warning.OnSuccess(
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(81);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(92, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
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
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(6, counterSuccess);
            CheckResultFail(result, warningMessage, customErrorObjectFactory);

            result = warning.OnSuccess(         // Warning becomes a fail
                r =>
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
        public void ResultFailOnSuccessToValueCustomResult()
        {
            int counterSuccess = 0;
            int counterFailureFactory = 0;
            string failureMessage = "My failure";
            string produceWarningMessage = "OnSuccess produce a warning.";
            string produceFailureMessage = "OnSuccess produce a failure.";
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -2 };

            // Failure result
            var failure = Result.Fail(failureMessage);

            // Treat warning as error => false
            // Produce an OK
            var result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(51);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(1, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(45, produceWarningMessage);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(2, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                r =>
                {
                    ++counterFailureFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(3, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Treat warning as error => true
            // Produce an OK
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Ok<int, CustomErrorTest>(21);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(4, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Warn
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Warn<int, CustomErrorTest>(75, produceWarningMessage);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(5, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            // Produce a Fail
            result = failure.OnSuccess(
                r =>
                {
                    ++counterSuccess;
                    return Result.Fail<int, CustomErrorTest>(produceFailureMessage, customErrorObject);
                },
                customErrorObjectFactory,
                true);
            Assert.AreEqual(0, counterSuccess);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);

            result = failure.OnSuccess(
                r =>
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
            Assert.AreEqual(0, counterSuccess);
            Assert.AreEqual(6, counterFailureFactory);
            CheckResultFail(result, failureMessage, customErrorObjectFactory);
        }

        #endregion
    }
}