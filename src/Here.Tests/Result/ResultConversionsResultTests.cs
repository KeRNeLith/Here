using System;
using NUnit.Framework;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> conversions.
    /// </summary>
    [TestFixture]
    internal class ResultConversionsTests : ResultTestsBase
    {
        #region Result Cast

        [Test]
        public void CastResultToValueResult()
        {
            int counterValueFactory = 0;

            // Result ok
            var ok = Result.Ok();

            var valueResult = ok.Cast(12);
            CheckResultOk(valueResult, 12);

            valueResult = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(valueResult, 45);

            // Result warn
            var warning = Result.Warn("My warning");

            valueResult = warning.Cast(12);
            CheckResultWarn(valueResult, 12, "My warning");

            valueResult = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultWarn(valueResult, 45, "My warning");

            // Result fail
            var failure = Result.Fail("My failure");

            valueResult = failure.Cast(12);
            CheckResultFail(valueResult, "My failure");

            valueResult = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultFail(valueResult, "My failure");

            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int>)null));
        }

        [Test]
        public void CastResultToCustomResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok();

            var customResult = ok.CustomCast(customErrorObjectFactory);
            CheckResultOk(customResult);

            customResult = ok.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(customResult);

            // Result warn
            var warning = Result.Warn("My warning");

            customResult = warning.CustomCast(customErrorObjectFactory);
            CheckResultWarn(customResult, "My warning");

            customResult = warning.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(customResult, "My warning");

            // Result fail
            var failure = Result.Fail("My failure");

            customResult = failure.CustomCast(customErrorObjectFactory);
            CheckResultFail(customResult, "My failure", customErrorObjectFactory);

            customResult = failure.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(customResult, "My failure", customErrorObjectFactory);

            Assert.Throws<ArgumentNullException>(() => ok.CustomCast((CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.CustomCast((Func<CustomErrorTest>)null));
        }

        [Test]
        public void CastResultToValueCustomResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterValueFactory = 0;
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok();

            var result = ok.Cast(22, customErrorObjectFactory);
            CheckResultOk(result, 22);

            result = ok.Cast(11,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 11);

            result = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                }, customErrorObjectFactory);
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(result, 33);

            result = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 44;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterValueFactory);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 44);

            // Result warn
            var warning = Result.Warn("My warning");

            result = warning.Cast(22, customErrorObjectFactory);
            CheckResultWarn(result, 22, "My warning");

            result = warning.Cast(11,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 11, "My warning");

            result = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                }, customErrorObjectFactory);
            Assert.AreEqual(3, counterValueFactory);
            CheckResultWarn(result, 33, "My warning");

            result = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 44;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterValueFactory);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 44, "My warning");

            // Result fail
            var failure = Result.Fail("My failure");

            result = failure.Cast(22, customErrorObjectFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(11,
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                }, 
                customErrorObjectFactory);
            Assert.AreEqual(4, counterValueFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 44;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterValueFactory);
            Assert.AreEqual(2, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            Assert.Throws<ArgumentNullException>(() => ok.Cast(12.5f, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast(12.5f, (Func<CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast(() => 12.5f, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast(() => 12.5f, (Func<CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<float>)null, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<float>)null, (Func<CustomErrorTest>)null));
        }

        #endregion

        #region Result<T> Cast

        [Test]
        public void CastValueResultTInToValueResultTOutWithAs()
        {
            var testObject = new TestClass();
            var testObjectLeaf = new TestClassLeaf();

            // Result ok
            var ok = Result.Ok<TestClass>(testObjectLeaf);
            var valueResult = ok.Cast<TestClassLeaf>();
            CheckResultOk(valueResult, testObjectLeaf);

            ok = Result.Ok(testObject);
            valueResult = ok.Cast<TestClassLeaf>();
            CheckResultOk(valueResult, null);

            // Result warn
            var warning = Result.Warn<TestClass>(testObjectLeaf, "My warning");
            valueResult = warning.Cast<TestClassLeaf>();
            CheckResultWarn(valueResult, testObjectLeaf, "My warning");

            warning = Result.Warn(testObject, "My warning");
            valueResult = warning.Cast<TestClassLeaf>();
            CheckResultWarn(valueResult, null, "My warning");

            // Result fail
            var failure = Result.Fail<int>("My failure");
            valueResult = failure.Cast<TestClassLeaf>();
            CheckResultFail(valueResult, "My failure");
        }

        [Test]
        public void CastValueResultTInToValueResultTOut()
        {
            int counterValueFactory = 0;

            // Result ok
            var ok = Result.Ok(12);

            var valueResult = ok.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                });
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(valueResult, 12.1f);

            // Result warn
            var warning = Result.Warn(13, "My warning");

            valueResult = warning.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultWarn(valueResult, 13.1f, "My warning");

            // Result fail
            var failure = Result.Fail<int>("My failure");
            
            valueResult = failure.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultFail(valueResult, "My failure");

            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int, float>)null));
        }

        [Test]
        public void CastValueResultToCustomResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok(25);

            var customResult = ok.CustomCast(customErrorObjectFactory);
            CheckResultOk(customResult);

            customResult = ok.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(customResult);

            // Result warn
            var warning = Result.Warn(24, "My warning");

            customResult = warning.CustomCast(customErrorObjectFactory);
            CheckResultWarn(customResult, "My warning");

            customResult = warning.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(customResult, "My warning");

            // Result fail
            var failure = Result.Fail<int>("My failure");

            customResult = failure.CustomCast(customErrorObjectFactory);
            CheckResultFail(customResult, "My failure", customErrorObjectFactory);

            customResult = failure.CustomCast(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(customResult, "My failure", customErrorObjectFactory);

            Assert.Throws<ArgumentNullException>(() => ok.CustomCast((CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.CustomCast((Func<CustomErrorTest>)null));
        }

        [Test]
        public void CastValueResultToValueCustomResultWithAs()
        {
            var testObject = new TestClass();
            var testObjectLeaf = new TestClassLeaf();

            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok<TestClass>(testObjectLeaf);
            var result = ok.Cast<TestClassLeaf, CustomErrorTest>(customErrorObjectFactory);
            CheckResultOk(result, testObjectLeaf);

            result = ok.Cast<TestClassLeaf, CustomErrorTest>(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, testObjectLeaf);

            ok = Result.Ok(testObject);
            result = ok.Cast<TestClassLeaf, CustomErrorTest>(customErrorObjectFactory);
            CheckResultOk(result, null);

            result = ok.Cast<TestClassLeaf, CustomErrorTest>(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, null);

            // Result warn
            var warning = Result.Warn<TestClass>(testObjectLeaf, "My warning");
            result = warning.Cast<TestClassLeaf, CustomErrorTest>(customErrorObjectFactory);
            CheckResultWarn(result, testObjectLeaf, "My warning");

            result = warning.Cast<TestClassLeaf, CustomErrorTest>(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, testObjectLeaf, "My warning");


            warning = Result.Warn(testObject, "My warning");
            result = warning.Cast<TestClassLeaf, CustomErrorTest>(customErrorObjectFactory);
            CheckResultWarn(result, null, "My warning");

            result = warning.Cast<TestClassLeaf, CustomErrorTest>(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, null, "My warning");

            // Result fail
            var failure = Result.Fail<TestClass>("My failure");
            result = failure.Cast<TestClassLeaf, CustomErrorTest>(customErrorObjectFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast<TestClassLeaf, CustomErrorTest>(
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            Assert.Throws<ArgumentNullException>(() => ok.Cast<TestClass, CustomErrorTest>((CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast<TestClass, CustomErrorTest>((Func<CustomErrorTest>)null));
        }

        [Test]
        public void CastValueResultToValueCustomResult()
        {
            var customErrorObjectFactory = new CustomErrorTest { ErrorCode = -8 };
            int counterValueFactory = 0;
            int counterErrorFactory = 0;

            // Result ok
            var ok = Result.Ok(65);

            var result = ok.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                }, customErrorObjectFactory);
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(result, 65.1f);

            result = ok.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.2f;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(2, counterValueFactory);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultOk(result, 65.2f);

            // Result warn
            var warning = Result.Warn(44, "My warning");

            result = warning.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                }, customErrorObjectFactory);
            Assert.AreEqual(3, counterValueFactory);
            CheckResultWarn(result, 44.1f, "My warning");

            result = warning.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.2f;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterValueFactory);
            Assert.AreEqual(0, counterErrorFactory);
            CheckResultWarn(result, 44.2f, "My warning");

            // Result fail
            var failure = Result.Fail<int>("My failure");

            result = failure.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                },
                customErrorObjectFactory);
            Assert.AreEqual(4, counterValueFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            result = failure.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.2f;
                },
                () =>
                {
                    ++counterErrorFactory;
                    return customErrorObjectFactory;
                });
            Assert.AreEqual(4, counterValueFactory);
            Assert.AreEqual(1, counterErrorFactory);
            CheckResultFail(result, "My failure", customErrorObjectFactory);

            Assert.Throws<ArgumentNullException>(() => ok.Cast(val => val + 0.1f, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast(val => val + 0.1f, (Func<CustomErrorTest>)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int, float>)null, counterErrorFactory));
            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int, float>)null, () => customErrorObjectFactory));
            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int, float>)null, (CustomErrorTest)null));
            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int, float>)null, (Func<CustomErrorTest>)null));
        }

        #endregion

        #region CustomResult<TError> Cast

        [Test]
        public void CastCustomResultToValueResult()
        {
            var customErrorObject = new CustomErrorTest { ErrorCode = -5 };
            int counterValueFactory = 0;

            // Result ok
            var ok = Result.CustomOk<CustomErrorTest>();

            var valueResult = ok.Cast(12);
            CheckResultOk(valueResult, 12);

            valueResult = ok.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(valueResult, 45);

            // Result warn
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            valueResult = warning.Cast(12);
            CheckResultWarn(valueResult, 12, "My warning");

            valueResult = warning.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultWarn(valueResult, 45, "My warning");

            // Result fail
            var failure = Result.CustomFail("My failure", customErrorObject);

            valueResult = failure.Cast(12);
            CheckResultFail(valueResult, "My failure");

            valueResult = failure.Cast(
                () =>
                {
                    ++counterValueFactory;
                    return 45;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultFail(valueResult, "My failure");

            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<float>)null));
        }

        [Test]
        public void CastCustomResultToValueCustomResult()
        {
            var customErrorObject = new CustomErrorTest { ErrorCode = -5 };
            int counterValueFactory = 0;

            // Result ok
            var ok = Result.CustomOk<CustomErrorTest>();

            var result = ok.CustomCast(22);
            CheckResultOk(result, 22);

            result = ok.CustomCast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                });
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(result, 33);

            // Result warn
            var warning = Result.CustomWarn<CustomErrorTest>("My warning");

            result = warning.CustomCast(22);
            CheckResultWarn(result, 22, "My warning");

            result = warning.CustomCast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultWarn(result, 33, "My warning");

            // Result fail
            var failure = Result.CustomFail("My failure", customErrorObject);

            result = failure.CustomCast(22);
            CheckResultFail(result, "My failure", customErrorObject);

            result = failure.CustomCast(
                () =>
                {
                    ++counterValueFactory;
                    return 33;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            Assert.Throws<ArgumentNullException>(() => ok.CustomCast((Func<float>)null));
        }

        #endregion

        #region Result<T> Cast

        [Test]
        public void CastValueCustomResultTInToValueCustomResultTOutWithAs()
        {
            var testObject = new TestClass();
            var testObjectLeaf = new TestClassLeaf();
            var customErrorObject = new CustomErrorTest { ErrorCode = -4 };

            // Result ok
            var ok = Result.Ok<TestClass, CustomErrorTest>(testObjectLeaf);
            var result = ok.Cast<TestClassLeaf>();
            CheckResultOk(result, testObjectLeaf);

            ok = Result.Ok<TestClass, CustomErrorTest>(testObject);
            result = ok.Cast<TestClassLeaf>();
            CheckResultOk(result, null);

            // Result warn
            var warning = Result.Warn<TestClass, CustomErrorTest>(testObjectLeaf, "My warning");
            result = warning.Cast<TestClassLeaf>();
            CheckResultWarn(result, testObjectLeaf, "My warning");

            warning = Result.Warn<TestClass, CustomErrorTest>(testObject, "My warning");
            result = warning.Cast<TestClassLeaf>();
            CheckResultWarn(result, null, "My warning");

            // Result fail
            var failure = Result.Fail<TestClass, CustomErrorTest>("My failure", customErrorObject);
            result = failure.Cast<TestClassLeaf>();
            CheckResultFail(result, "My failure", customErrorObject);
        }

        [Test]
        public void CastValueCustomResultTInToValueCustomResultTOut()
        {
            var customErrorObject = new CustomErrorTest { ErrorCode = -4 };
            int counterValueFactory = 0;

            // Result ok
            var ok = Result.Ok<int, CustomErrorTest>(65);

            var result = ok.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                });
            Assert.AreEqual(1, counterValueFactory);
            CheckResultOk(result, 65.1f);

            // Result warn
            var warning = Result.Warn<int, CustomErrorTest>(44, "My warning");

            result = warning.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultWarn(result, 44.1f, "My warning");

            // Result fail
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);

            result = failure.Cast(
                value =>
                {
                    ++counterValueFactory;
                    return value + 0.1f;
                });
            Assert.AreEqual(2, counterValueFactory);
            CheckResultFail(result, "My failure", customErrorObject);

            Assert.Throws<ArgumentNullException>(() => ok.Cast((Func<int, float>)null));
        }

        #endregion
    }
}