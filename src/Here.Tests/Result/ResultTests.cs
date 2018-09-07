using System;
using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Basic tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultTests : ResultTestsBase
    {
        [Test]
        public void ResultConstruction()
        {
            // Ok result
            var ok = Result.Ok();
            CheckResultOk(ok);

            // Warning result
            var warning = Result.Warn("My warning");
            CheckResultWarn(warning, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.Warn("My warning", warnException);
            CheckResultWarn(warning, "My warning", warnException);

            // Failure result
            var failure = Result.Fail("My failure");
            CheckResultFail(failure, "My failure");

            var failException = new Exception("Failure exception");
            failure = Result.Fail("My failure", failException);
            CheckResultFail(failure, "My failure", failException);
        }

        [Test]
        public void CustomResultConstruction()
        {
            // Ok result
            var ok = Result.CustomOk<Exception>();
            CheckResultOk(ok);

            // Warning result
            var warning = Result.CustomWarn<Exception>("My warning");
            CheckResultWarn(warning, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.CustomWarn<Exception>("My warning", warnException);
            CheckResultWarn(warning, "My warning", warnException);

            // Failure result
            var errorObject = new CustomErrorTest { ErrorCode = -42 };
            var failure = Result.CustomFail("My failure", errorObject);
            CheckResultFail(failure, "My failure", errorObject);

            var failException = new Exception("Failure exception");
            failure = Result.CustomFail("My failure", errorObject, failException);
            CheckResultFail(failure, "My failure", errorObject, failException);
        }

        [Test]
        public void ResultConstructionWithValue()
        {
            // Ok result
            var ok = Result.Ok(42);
            CheckResultOk(ok, 42);

            // Warning result
            var warning = Result.Warn(12, "My warning");
            CheckResultWarn(warning, 12, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.Warn(12, "My warning", warnException);
            CheckResultWarn(warning, 12, "My warning", warnException);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Warn(12, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Warn(12, ""); });

            // Failure result
            var failure = Result.Fail<int>("My failure");
            CheckResultFail(failure, "My failure");

            var failException = new Exception("Failure exception");
            failure = Result.Fail<int>("My failure", failException);
            CheckResultFail(failure, "My failure", failException);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Fail<int>(null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Fail<int>(""); });
        }

        [Test]
        public void ResultConstructionWithValueCustomError()
        {
            // Ok result
            var ok = Result.Ok<int, Exception>(42);
            CheckResultOk(ok, 42);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(12, "My warning");
            CheckResultWarn(warning, 12, "My warning");

            var warnException = new Exception("Warning exception");
            warning = Result.Warn<int, CustomErrorTest>(42, "My warning", warnException);
            CheckResultWarn(warning, 42, "My warning", warnException);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Warn<int, Exception>(12, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Warn<int, Exception>(12, ""); });

            // Failure result
            var customErrorObject = new CustomErrorTest { ErrorCode = -5 };
            var failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            CheckResultFail(failure, "My failure", customErrorObject);

            var failException = new Exception("Failure exception");
            failure = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject, failException);
            CheckResultFail(failure, "My failure", customErrorObject, failException);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Fail<int, Exception>(null, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Fail<int, Exception>("", null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = Result.Fail<int, Exception>("My failure", null); });
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void ResultEquality()
        {
            var exception1 = new Exception("My exception");
            var exception2 = new Exception("My exception");

            // Results
            var resultOk1 = Result.Ok();
            var resultOk2 = Result.Ok();
            var resultWarn1 = Result.Warn("My Warning");
            var resultWarn2 = Result.Warn("My Warning");
            var resultWarn3 = Result.Warn("My Warning", exception1);
            var resultWarn4 = Result.Warn("My Warning", exception1);
            var resultWarn5 = Result.Warn("My Warning", exception2);
            var resultWarn6 = Result.Warn("My Warning 2");
            var resultFail1 = Result.Fail("My Failure");
            var resultFail2 = Result.Fail("My Failure");
            var resultFail3 = Result.Fail("My Failure", exception1);
            var resultFail4 = Result.Fail("My Failure", exception1);
            var resultFail5 = Result.Fail("My Failure", exception2);
            var resultFail6 = Result.Fail("My Failure 2");

            // Checks Ok
            Assert.AreEqual(resultOk1, resultOk1);
            Assert.AreEqual(resultOk1, resultOk2);
            Assert.AreEqual(resultOk2, resultOk1);

            // Checks Warn
            Assert.AreEqual(resultWarn1, resultWarn1);
            Assert.AreEqual(resultWarn1, resultWarn2);
            Assert.AreEqual(resultWarn2, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn1);
            Assert.AreEqual(resultWarn3, resultWarn4);
            Assert.AreEqual(resultWarn4, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn5);
            Assert.AreNotEqual(resultWarn5, resultWarn3);
            Assert.AreNotEqual(resultWarn1, resultWarn6);
            Assert.AreNotEqual(resultWarn6, resultWarn1);

            // Checks Fail
            Assert.AreEqual(resultFail1, resultFail1);
            Assert.AreEqual(resultFail1, resultFail2);
            Assert.AreEqual(resultFail2, resultFail1);
            Assert.AreNotEqual(resultFail1, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail1);
            Assert.AreEqual(resultFail3, resultFail4);
            Assert.AreEqual(resultFail4, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail5);
            Assert.AreNotEqual(resultFail5, resultFail3);
            Assert.AreNotEqual(resultFail1, resultFail6);
            Assert.AreNotEqual(resultFail6, resultFail1);

            // Checks Ok & Warn
            Assert.AreNotEqual(resultOk1, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultOk1);

            // Checks Ok & Fail
            Assert.AreNotEqual(resultOk1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultOk1);

            // Checks Warn & Fail
            Assert.AreNotEqual(resultWarn1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultWarn1);

            // Mixed
            Assert.AreNotEqual(resultOk1, null);
            Assert.AreNotEqual(null, resultOk1);
            Assert.AreNotEqual(resultWarn1, null);
            Assert.AreNotEqual(null, resultWarn1);
            Assert.AreNotEqual(resultFail1, null);
            Assert.AreNotEqual(null, resultFail1);

            var valueResult = Result.Ok(10);
            Assert.AreNotEqual(resultOk1, valueResult);
            Assert.AreNotEqual(valueResult, resultOk1);

            var testObject = new TestClass();
            Assert.AreNotEqual(resultOk1, testObject);
            Assert.AreNotEqual(testObject, resultOk1);
        }

        [Test]
        public void ValueResultEquality()
        {
            var person1 = new Person("Jack");
            var person2 = new Person("Jacky");
            var person3 = new Person("Jack");
            var person4 = new PersonNotEquatable("Jean");
            var person5 = new PersonNotEquatable("Jo");
            var person6 = new PersonNotEquatable("Jean");
            var exception1 = new Exception("My exception");
            var exception2 = new Exception("My exception");

            // Results
            var resultOk1 = Result.Ok(person1);
            var resultOk2 = Result.Ok(person1);
            var resultOk3 = Result.Ok(person2);
            var resultOk4 = Result.Ok(person3);
            var resultOk5 = Result.Ok(person3);
            var resultOk6 = Result.Ok(person4);
            var resultWarn1 = Result.Warn(person1, "My Warning");
            var resultWarn2 = Result.Warn(person1, "My Warning");
            var resultWarn3 = Result.Warn(person1, "My Warning", exception1);
            var resultWarn4 = Result.Warn(person1, "My Warning", exception1);
            var resultWarn5 = Result.Warn(person1, "My Warning", exception2);
            var resultWarn6 = Result.Warn(person1, "My Warning 2");
            var resultWarn7 = Result.Warn(person2, "My Warning");
            var resultWarn8 = Result.Warn(person3, "My Warning");
            var resultWarn9 = Result.Warn(person4, "My Warning");
            var resultWarn10 = Result.Warn(person4, "My Warning");
            var resultWarn11 = Result.Warn(person4, "My Warning", exception1);
            var resultWarn12 = Result.Warn(person4, "My Warning", exception1);
            var resultWarn13 = Result.Warn(person4, "My Warning", exception2);
            var resultWarn14 = Result.Warn(person4, "My Warning 2");
            var resultWarn15 = Result.Warn(person5, "My Warning");
            var resultWarn16 = Result.Warn(person6, "My Warning");
            var resultFail1 = Result.Fail<Person>("My Failure");
            var resultFail2 = Result.Fail<Person>("My Failure");
            var resultFail3 = Result.Fail<Person>("My Failure", exception1);
            var resultFail4 = Result.Fail<Person>("My Failure", exception1);
            var resultFail5 = Result.Fail<Person>("My Failure", exception2);
            var resultFail6 = Result.Fail<Person>("My Failure 2");
            var resultFail7 = Result.Fail<PersonNotEquatable>("My Failure");
            var resultFail8 = Result.Fail<PersonNotEquatable>("My Failure");
            var resultFail9 = Result.Fail<PersonNotEquatable>("My Failure", exception1);
            var resultFail10 = Result.Fail<PersonNotEquatable>("My Failure", exception1);
            var resultFail11 = Result.Fail<PersonNotEquatable>("My Failure", exception2);
            var resultFail12 = Result.Fail<PersonNotEquatable>("My Failure 2");

            // Checks Ok
            Assert.AreEqual(resultOk1, resultOk1);
            Assert.AreEqual(resultOk1, resultOk2);
            Assert.AreEqual(resultOk2, resultOk1);
            Assert.AreNotEqual(resultOk1, resultOk3);
            Assert.AreNotEqual(resultOk3, resultOk1);

            Assert.AreEqual(resultOk4, resultOk4);
            Assert.AreEqual(resultOk4, resultOk5);
            Assert.AreEqual(resultOk5, resultOk4);
            Assert.AreNotEqual(resultOk4, resultOk6);
            Assert.AreNotEqual(resultOk6, resultOk4);

            // Checks Warn
            Assert.AreEqual(resultWarn1, resultWarn1);
            Assert.AreEqual(resultWarn1, resultWarn2);
            Assert.AreEqual(resultWarn2, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn1);
            Assert.AreEqual(resultWarn3, resultWarn4);
            Assert.AreEqual(resultWarn4, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn5);
            Assert.AreNotEqual(resultWarn5, resultWarn3);
            Assert.AreNotEqual(resultWarn1, resultWarn6);
            Assert.AreNotEqual(resultWarn6, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultWarn7);
            Assert.AreNotEqual(resultWarn7, resultWarn1);
            Assert.AreEqual(resultWarn1, resultWarn8);
            Assert.AreEqual(resultWarn8, resultWarn1);

            Assert.AreEqual(resultWarn9, resultWarn9);
            Assert.AreEqual(resultWarn9, resultWarn10);
            Assert.AreEqual(resultWarn10, resultWarn9);
            Assert.AreNotEqual(resultWarn9, resultWarn11);
            Assert.AreNotEqual(resultWarn11, resultWarn9);
            Assert.AreEqual(resultWarn11, resultWarn12);
            Assert.AreEqual(resultWarn12, resultWarn11);
            Assert.AreNotEqual(resultWarn11, resultWarn13);
            Assert.AreNotEqual(resultWarn13, resultWarn11);
            Assert.AreNotEqual(resultWarn9, resultWarn14);
            Assert.AreNotEqual(resultWarn14, resultWarn9);
            Assert.AreNotEqual(resultWarn9, resultWarn15);
            Assert.AreNotEqual(resultWarn15, resultWarn9);
            Assert.AreNotEqual(resultWarn9, resultWarn16);
            Assert.AreNotEqual(resultWarn16, resultWarn9);

            // Checks Fail
            Assert.AreEqual(resultFail1, resultFail1);
            Assert.AreEqual(resultFail1, resultFail2);
            Assert.AreEqual(resultFail2, resultFail1);
            Assert.AreNotEqual(resultFail1, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail1);
            Assert.AreEqual(resultFail3, resultFail4);
            Assert.AreEqual(resultFail4, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail5);
            Assert.AreNotEqual(resultFail5, resultFail3);
            Assert.AreNotEqual(resultFail1, resultFail6);
            Assert.AreNotEqual(resultFail6, resultFail1);

            Assert.AreEqual(resultFail7, resultFail7);
            Assert.AreEqual(resultFail7, resultFail8);
            Assert.AreEqual(resultFail8, resultFail7);
            Assert.AreNotEqual(resultFail7, resultFail9);
            Assert.AreNotEqual(resultFail9, resultFail7);
            Assert.AreEqual(resultFail9, resultFail10);
            Assert.AreEqual(resultFail10, resultFail9);
            Assert.AreNotEqual(resultFail9, resultFail11);
            Assert.AreNotEqual(resultFail11, resultFail9);
            Assert.AreNotEqual(resultFail7, resultFail12);
            Assert.AreNotEqual(resultFail12, resultFail7);

            // Checks Ok & Warn
            Assert.AreNotEqual(resultOk1, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultOk1);

            // Checks Ok & Fail
            Assert.AreNotEqual(resultOk1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultOk1);

            // Checks Warn & Fail
            Assert.AreNotEqual(resultWarn1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultWarn1);

            // Mixed
            Assert.AreNotEqual(resultOk1, null);
            Assert.AreNotEqual(null, resultOk1);
            Assert.AreNotEqual(resultWarn1, null);
            Assert.AreNotEqual(null, resultWarn1);
            Assert.AreNotEqual(resultFail1, null);
            Assert.AreNotEqual(null, resultFail1);

            var valueResult = Result.Ok<int>(12);
            Assert.AreNotEqual(resultOk1, valueResult);
            Assert.AreNotEqual(valueResult, resultOk1);

            var customValueResult = Result.Ok<int, CustomErrorTest>(10);
            Assert.AreNotEqual(resultOk1, customValueResult);
            Assert.AreNotEqual(customValueResult, resultOk1);

            var testObject = new TestClass();
            Assert.AreNotEqual(resultOk1, testObject);
            Assert.AreNotEqual(testObject, resultOk1);
        }

        [Test]
        public void CustomResultEquality()
        {
            var customErrorObject1 = new CustomErrorTest { ErrorCode = -5 };
            var customErrorObject2 = new CustomErrorTest { ErrorCode = -6 };

            var exception1 = new Exception("My exception");
            var exception2 = new Exception("My exception");

            // Results
            var resultOk1 = Result.CustomOk<CustomErrorTest>();
            var resultOk2 = Result.CustomOk<CustomErrorTest>();
            var resultWarn1 = Result.CustomWarn<CustomErrorTest>("My Warning");
            var resultWarn2 = Result.CustomWarn<CustomErrorTest>("My Warning");
            var resultWarn3 = Result.CustomWarn<CustomErrorTest>("My Warning", exception1);
            var resultWarn4 = Result.CustomWarn<CustomErrorTest>("My Warning", exception1);
            var resultWarn5 = Result.CustomWarn<CustomErrorTest>("My Warning", exception2);
            var resultWarn6 = Result.CustomWarn<CustomErrorTest>("My Warning 2");
            var resultFail1 = Result.CustomFail("My Failure", customErrorObject1);
            var resultFail2 = Result.CustomFail("My Failure", customErrorObject1);
            var resultFail3 = Result.CustomFail("My Failure", customErrorObject1, exception1);
            var resultFail4 = Result.CustomFail("My Failure", customErrorObject1, exception1);
            var resultFail5 = Result.CustomFail("My Failure", customErrorObject1, exception2);
            var resultFail6 = Result.CustomFail("My Failure 2", customErrorObject1);
            var resultFail7 = Result.CustomFail("My Failure", customErrorObject2);

            // Checks Ok
            Assert.AreEqual(resultOk1, resultOk1);
            Assert.AreEqual(resultOk1, resultOk2);
            Assert.AreEqual(resultOk2, resultOk1);

            // Checks Warn
            Assert.AreEqual(resultWarn1, resultWarn1);
            Assert.AreEqual(resultWarn1, resultWarn2);
            Assert.AreEqual(resultWarn2, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn1);
            Assert.AreEqual(resultWarn3, resultWarn4);
            Assert.AreEqual(resultWarn4, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn5);
            Assert.AreNotEqual(resultWarn5, resultWarn3);
            Assert.AreNotEqual(resultWarn1, resultWarn6);
            Assert.AreNotEqual(resultWarn6, resultWarn1);

            // Checks Fail
            Assert.AreEqual(resultFail1, resultFail1);
            Assert.AreEqual(resultFail1, resultFail2);
            Assert.AreEqual(resultFail2, resultFail1);
            Assert.AreNotEqual(resultFail1, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail1);
            Assert.AreEqual(resultFail3, resultFail4);
            Assert.AreEqual(resultFail4, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail5);
            Assert.AreNotEqual(resultFail5, resultFail3);
            Assert.AreNotEqual(resultFail1, resultFail6);
            Assert.AreNotEqual(resultFail6, resultFail1);
            Assert.AreNotEqual(resultFail1, resultFail7);
            Assert.AreNotEqual(resultFail7, resultFail1);

            // Checks Ok & Warn
            Assert.AreNotEqual(resultOk1, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultOk1);

            // Checks Ok & Fail
            Assert.AreNotEqual(resultOk1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultOk1);

            // Checks Warn & Fail
            Assert.AreNotEqual(resultWarn1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultWarn1);

            // Mixed
            Assert.AreNotEqual(resultOk1, null);
            Assert.AreNotEqual(null, resultOk1);
            Assert.AreNotEqual(resultWarn1, null);
            Assert.AreNotEqual(null, resultWarn1);
            Assert.AreNotEqual(resultFail1, null);
            Assert.AreNotEqual(null, resultFail1);

            var valueResult = Result.Ok(10);
            Assert.AreNotEqual(resultOk1, valueResult);
            Assert.AreNotEqual(valueResult, resultOk1);

            var testObject = new TestClass();
            Assert.AreNotEqual(resultOk1, testObject);
            Assert.AreNotEqual(testObject, resultOk1);
        }

        [Test]
        public void CustomValueResultEquality()
        {
            var customErrorObject1 = new CustomErrorTest { ErrorCode = -5 };
            var customErrorObject2 = new CustomErrorTest { ErrorCode = -6 };

            var person1 = new Person("Jack");
            var person2 = new Person("Jacky");
            var person3 = new Person("Jack");
            var person4 = new PersonNotEquatable("Jean");
            var person5 = new PersonNotEquatable("Jo");
            var person6 = new PersonNotEquatable("Jean");
            var exception1 = new Exception("My exception");
            var exception2 = new Exception("My exception");

            // Results
            var resultOk1 = Result.Ok<Person, CustomErrorTest>(person1);
            var resultOk2 = Result.Ok<Person, CustomErrorTest>(person1);
            var resultOk3 = Result.Ok<Person, CustomErrorTest>(person2);
            var resultOk4 = Result.Ok<Person, CustomErrorTest>(person3);
            var resultOk5 = Result.Ok<Person, CustomErrorTest>(person3);
            var resultOk6 = Result.Ok<PersonNotEquatable, CustomErrorTest>(person4);
            var resultWarn1 = Result.Warn<Person, CustomErrorTest>(person1, "My Warning");
            var resultWarn2 = Result.Warn<Person, CustomErrorTest>(person1, "My Warning");
            var resultWarn3 = Result.Warn<Person, CustomErrorTest>(person1, "My Warning", exception1);
            var resultWarn4 = Result.Warn<Person, CustomErrorTest>(person1, "My Warning", exception1);
            var resultWarn5 = Result.Warn<Person, CustomErrorTest>(person1, "My Warning", exception2);
            var resultWarn6 = Result.Warn<Person, CustomErrorTest>(person1, "My Warning 2");
            var resultWarn7 = Result.Warn<Person, CustomErrorTest>(person2, "My Warning");
            var resultWarn8 = Result.Warn<Person, CustomErrorTest>(person3, "My Warning");
            var resultWarn9 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person4, "My Warning");
            var resultWarn10 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person4, "My Warning");
            var resultWarn11 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person4, "My Warning", exception1);
            var resultWarn12 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person4, "My Warning", exception1);
            var resultWarn13 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person4, "My Warning", exception2);
            var resultWarn14 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person4, "My Warning 2");
            var resultWarn15 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person5, "My Warning");
            var resultWarn16 = Result.Warn<PersonNotEquatable, CustomErrorTest>(person6, "My Warning");
            var resultFail1 = Result.Fail<Person, CustomErrorTest>("My Failure", customErrorObject1);
            var resultFail2 = Result.Fail<Person, CustomErrorTest>("My Failure", customErrorObject1);
            var resultFail3 = Result.Fail<Person, CustomErrorTest>("My Failure", customErrorObject1, exception1);
            var resultFail4 = Result.Fail<Person, CustomErrorTest>("My Failure", customErrorObject1, exception1);
            var resultFail5 = Result.Fail<Person, CustomErrorTest>("My Failure", customErrorObject1, exception2);
            var resultFail6 = Result.Fail<Person, CustomErrorTest>("My Failure 2", customErrorObject1);
            var resultFail7 = Result.Fail<Person, CustomErrorTest>("My Failure", customErrorObject2);
            var resultFail8 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure", customErrorObject1);
            var resultFail9 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure", customErrorObject1);
            var resultFail10 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure", customErrorObject1, exception1);
            var resultFail11 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure", customErrorObject1, exception1);
            var resultFail12 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure", customErrorObject1, exception2);
            var resultFail13 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure 2", customErrorObject1);
            var resultFail14 = Result.Fail<PersonNotEquatable, CustomErrorTest>("My Failure", customErrorObject2);

            // Checks Ok
            Assert.AreEqual(resultOk1, resultOk1);
            Assert.AreEqual(resultOk1, resultOk2);
            Assert.AreEqual(resultOk2, resultOk1);
            Assert.AreNotEqual(resultOk1, resultOk3);
            Assert.AreNotEqual(resultOk3, resultOk1);

            Assert.AreEqual(resultOk4, resultOk4);
            Assert.AreEqual(resultOk4, resultOk5);
            Assert.AreEqual(resultOk5, resultOk4);
            Assert.AreNotEqual(resultOk4, resultOk6);
            Assert.AreNotEqual(resultOk6, resultOk4);

            // Checks Warn
            Assert.AreEqual(resultWarn1, resultWarn1);
            Assert.AreEqual(resultWarn1, resultWarn2);
            Assert.AreEqual(resultWarn2, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn1);
            Assert.AreEqual(resultWarn3, resultWarn4);
            Assert.AreEqual(resultWarn4, resultWarn3);
            Assert.AreNotEqual(resultWarn3, resultWarn5);
            Assert.AreNotEqual(resultWarn5, resultWarn3);
            Assert.AreNotEqual(resultWarn1, resultWarn6);
            Assert.AreNotEqual(resultWarn6, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultWarn7);
            Assert.AreNotEqual(resultWarn7, resultWarn1);
            Assert.AreEqual(resultWarn1, resultWarn8);
            Assert.AreEqual(resultWarn8, resultWarn1);

            Assert.AreEqual(resultWarn9, resultWarn9);
            Assert.AreEqual(resultWarn9, resultWarn10);
            Assert.AreEqual(resultWarn10, resultWarn9);
            Assert.AreNotEqual(resultWarn9, resultWarn11);
            Assert.AreNotEqual(resultWarn11, resultWarn9);
            Assert.AreEqual(resultWarn11, resultWarn12);
            Assert.AreEqual(resultWarn12, resultWarn11);
            Assert.AreNotEqual(resultWarn11, resultWarn13);
            Assert.AreNotEqual(resultWarn13, resultWarn11);
            Assert.AreNotEqual(resultWarn9, resultWarn14);
            Assert.AreNotEqual(resultWarn14, resultWarn9);
            Assert.AreNotEqual(resultWarn9, resultWarn15);
            Assert.AreNotEqual(resultWarn15, resultWarn9);
            Assert.AreNotEqual(resultWarn9, resultWarn16);
            Assert.AreNotEqual(resultWarn16, resultWarn9);

            // Checks Fail
            Assert.AreEqual(resultFail1, resultFail1);
            Assert.AreEqual(resultFail1, resultFail2);
            Assert.AreEqual(resultFail2, resultFail1);
            Assert.AreNotEqual(resultFail1, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail1);
            Assert.AreEqual(resultFail3, resultFail4);
            Assert.AreEqual(resultFail4, resultFail3);
            Assert.AreNotEqual(resultFail3, resultFail5);
            Assert.AreNotEqual(resultFail5, resultFail3);
            Assert.AreNotEqual(resultFail1, resultFail6);
            Assert.AreNotEqual(resultFail6, resultFail1);
            Assert.AreNotEqual(resultFail1, resultFail7);
            Assert.AreNotEqual(resultFail7, resultFail1);

            Assert.AreEqual(resultFail8, resultFail8);
            Assert.AreEqual(resultFail8, resultFail9);
            Assert.AreEqual(resultFail9, resultFail8);
            Assert.AreNotEqual(resultFail8, resultFail10);
            Assert.AreNotEqual(resultFail10, resultFail8);
            Assert.AreEqual(resultFail10, resultFail11);
            Assert.AreEqual(resultFail11, resultFail10);
            Assert.AreNotEqual(resultFail10, resultFail12);
            Assert.AreNotEqual(resultFail12, resultFail10);
            Assert.AreNotEqual(resultFail8, resultFail13);
            Assert.AreNotEqual(resultFail13, resultFail8);
            Assert.AreNotEqual(resultFail8, resultFail14);
            Assert.AreNotEqual(resultFail14, resultFail8);

            // Checks Ok & Warn
            Assert.AreNotEqual(resultOk1, resultWarn1);
            Assert.AreNotEqual(resultWarn1, resultOk1);

            // Checks Ok & Fail
            Assert.AreNotEqual(resultOk1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultOk1);

            // Checks Warn & Fail
            Assert.AreNotEqual(resultWarn1, resultFail1);
            Assert.AreNotEqual(resultFail1, resultWarn1);

            // Mixed
            Assert.AreNotEqual(resultOk1, null);
            Assert.AreNotEqual(null, resultOk1);
            Assert.AreNotEqual(resultWarn1, null);
            Assert.AreNotEqual(null, resultWarn1);
            Assert.AreNotEqual(resultFail1, null);
            Assert.AreNotEqual(null, resultFail1);

            var valueResult = Result.Ok<int>(12);
            Assert.AreNotEqual(resultOk1, valueResult);
            Assert.AreNotEqual(valueResult, resultOk1);

            var testObject = new TestClass();
            Assert.AreNotEqual(resultOk1, testObject);
            Assert.AreNotEqual(testObject, resultOk1);
        }

        [Test]
        public void ResultHashCode()
        {
            // Result ok
            var ok1 = Result.Ok();
            var ok2 = Result.Ok();
            Assert.AreNotSame(ok1, ok2);
            Assert.IsTrue(ok1.Equals(ok2));
            Assert.IsTrue(ok2.Equals(ok1));
            Assert.IsTrue(ok1.GetHashCode() == ok2.GetHashCode());

            // Result warn
            var warn1 = Result.Warn("My warning");
            var warn2 = Result.Warn("My warning");
            Assert.AreNotSame(warn1, warn2);
            Assert.IsTrue(warn1.Equals(warn2));
            Assert.IsTrue(warn2.Equals(warn1));
            Assert.IsTrue(warn1.GetHashCode() == warn2.GetHashCode());

            var warn3 = Result.Warn("My warning 2");
            Assert.AreNotSame(warn1, warn3);
            Assert.IsFalse(warn1.Equals(warn3));
            Assert.IsFalse(warn3.Equals(warn1));
            Assert.IsFalse(warn1.GetHashCode() == warn3.GetHashCode());

            // Result fail
            var fail1 = Result.Fail("My failure");
            var fail2 = Result.Fail("My failure");
            Assert.AreNotSame(fail1, fail2);
            Assert.IsTrue(fail1.Equals(fail2));
            Assert.IsTrue(fail2.Equals(fail1));
            Assert.IsTrue(fail1.GetHashCode() == fail2.GetHashCode());

            var fail3 = Result.Fail("My failure 2");
            Assert.AreNotSame(fail1, fail3);
            Assert.IsFalse(fail1.Equals(fail3));
            Assert.IsFalse(fail3.Equals(fail1));
            Assert.IsFalse(fail1.GetHashCode() == fail3.GetHashCode());
        }

        [Test]
        public void ValueResultHashCode()
        {
            // Result ok
            var ok1 = Result.Ok(42);
            var ok2 = Result.Ok(42);
            Assert.AreNotSame(ok1, ok2);
            Assert.IsTrue(ok1.Equals(ok2));
            Assert.IsTrue(ok2.Equals(ok1));
            Assert.IsTrue(ok1.GetHashCode() == ok2.GetHashCode());

            var ok3 = Result.Ok(12);
            Assert.AreNotSame(ok1, ok3);
            Assert.IsFalse(ok1.Equals(ok3));
            Assert.IsFalse(ok3.Equals(ok1));
            Assert.IsFalse(ok1.GetHashCode() == ok3.GetHashCode());

            // Result warn
            var warn1 = Result.Warn(12, "My warning");
            var warn2 = Result.Warn(12, "My warning");
            Assert.AreNotSame(warn1, warn2);
            Assert.IsTrue(warn1.Equals(warn2));
            Assert.IsTrue(warn2.Equals(warn1));
            Assert.IsTrue(warn1.GetHashCode() == warn2.GetHashCode());

            var warn3 = Result.Warn(42, "My warning 2");
            Assert.AreNotSame(warn1, warn3);
            Assert.IsFalse(warn1.Equals(warn3));
            Assert.IsFalse(warn3.Equals(warn1));
            Assert.IsFalse(warn1.GetHashCode() == warn3.GetHashCode());

            // Result fail
            var fail1 = Result.Fail<int>("My failure");
            var fail2 = Result.Fail<int>("My failure");
            Assert.AreNotSame(fail1, fail2);
            Assert.IsTrue(fail1.Equals(fail2));
            Assert.IsTrue(fail2.Equals(fail1));
            Assert.IsTrue(fail1.GetHashCode() == fail2.GetHashCode());

            var fail3 = Result.Fail<int>("My failure 2");
            Assert.AreNotSame(fail1, fail3);
            Assert.IsFalse(fail1.Equals(fail3));
            Assert.IsFalse(fail3.Equals(fail1));
            Assert.IsFalse(fail1.GetHashCode() == fail3.GetHashCode());
        }

        [Test]
        public void CustomResultHashCode()
        {
            var customErrorObject = new CustomErrorTest { ErrorCode = -5 };

            // Result ok
            var ok1 = Result.CustomOk<CustomErrorTest>();
            var ok2 = Result.CustomOk<CustomErrorTest>();
            Assert.AreNotSame(ok1, ok2);
            Assert.IsTrue(ok1.Equals(ok2));
            Assert.IsTrue(ok2.Equals(ok1));
            Assert.IsTrue(ok1.GetHashCode() == ok2.GetHashCode());

            // Result warn
            var warn1 = Result.CustomWarn<CustomErrorTest>("My warning");
            var warn2 = Result.CustomWarn<CustomErrorTest>("My warning");
            Assert.AreNotSame(warn1, warn2);
            Assert.IsTrue(warn1.Equals(warn2));
            Assert.IsTrue(warn2.Equals(warn1));
            Assert.IsTrue(warn1.GetHashCode() == warn2.GetHashCode());

            var warn3 = Result.CustomWarn<CustomErrorTest>("My warning 2");
            Assert.AreNotSame(warn1, warn3);
            Assert.IsFalse(warn1.Equals(warn3));
            Assert.IsFalse(warn3.Equals(warn1));
            Assert.IsFalse(warn1.GetHashCode() == warn3.GetHashCode());

            // Result fail
            var fail1 = Result.CustomFail("My failure", customErrorObject);
            var fail2 = Result.CustomFail("My failure", customErrorObject);
            Assert.AreNotSame(fail1, fail2);
            Assert.IsTrue(fail1.Equals(fail2));
            Assert.IsTrue(fail2.Equals(fail1));
            Assert.IsTrue(fail1.GetHashCode() == fail2.GetHashCode());

            var fail3 = Result.CustomFail("My failure 2", customErrorObject);
            Assert.AreNotSame(fail1, fail3);
            Assert.IsFalse(fail1.Equals(fail3));
            Assert.IsFalse(fail3.Equals(fail1));
            Assert.IsFalse(fail1.GetHashCode() == fail3.GetHashCode());
        }

        [Test]
        public void CustomValueResultHashCode()
        {
            var customErrorObject = new CustomErrorTest { ErrorCode = -5 };

            // Result ok
            var ok1 = Result.Ok<int, CustomErrorTest>(42);
            var ok2 = Result.Ok<int, CustomErrorTest>(42);
            Assert.AreNotSame(ok1, ok2);
            Assert.IsTrue(ok1.Equals(ok2));
            Assert.IsTrue(ok2.Equals(ok1));
            Assert.IsTrue(ok1.GetHashCode() == ok2.GetHashCode());

            var ok3 = Result.Ok<int, CustomErrorTest>(12);
            Assert.AreNotSame(ok1, ok3);
            Assert.IsFalse(ok1.Equals(ok3));
            Assert.IsFalse(ok3.Equals(ok1));
            Assert.IsFalse(ok1.GetHashCode() == ok3.GetHashCode());

            // Result warn
            var warn1 = Result.Warn<int, CustomErrorTest>(12, "My warning");
            var warn2 = Result.Warn<int, CustomErrorTest>(12, "My warning");
            Assert.AreNotSame(warn1, warn2);
            Assert.IsTrue(warn1.Equals(warn2));
            Assert.IsTrue(warn2.Equals(warn1));
            Assert.IsTrue(warn1.GetHashCode() == warn2.GetHashCode());

            var warn3 = Result.Warn<int, CustomErrorTest>(42, "My warning 2");
            Assert.AreNotSame(warn1, warn3);
            Assert.IsFalse(warn1.Equals(warn3));
            Assert.IsFalse(warn3.Equals(warn1));
            Assert.IsFalse(warn1.GetHashCode() == warn3.GetHashCode());

            // Result fail
            var fail1 = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            var fail2 = Result.Fail<int, CustomErrorTest>("My failure", customErrorObject);
            Assert.AreNotSame(fail1, fail2);
            Assert.IsTrue(fail1.Equals(fail2));
            Assert.IsTrue(fail2.Equals(fail1));
            Assert.IsTrue(fail1.GetHashCode() == fail2.GetHashCode());

            var fail3 = Result.Fail<int, CustomErrorTest>("My failure 2", customErrorObject);
            Assert.AreNotSame(fail1, fail3);
            Assert.IsFalse(fail1.Equals(fail3));
            Assert.IsFalse(fail3.Equals(fail1));
            Assert.IsFalse(fail1.GetHashCode() == fail3.GetHashCode());
        }

        [Test]
        public void ValueResultEqualityValue()
        {
            // Result ok
            var ok = Result.Ok(42);
            Assert.IsTrue(ok == 42);
            Assert.IsTrue(42 == ok);
            Assert.IsFalse(ok == 12);
            Assert.IsTrue(ok != 12);
            Assert.IsTrue(25 != ok);

            // Result warn
            var person1 = new Person("Jack");
            var person2 = new Person("Jean");
            var warn = Result.Warn(person1, "My warning");
            Assert.IsTrue(warn == person1);
            Assert.IsTrue(new Person("Jack") == warn);
            Assert.IsFalse(warn == person2);
            Assert.IsTrue(warn != person2);
            Assert.IsFalse(person1 != warn);

            // Result fail
            var fail = Result.Fail<int>("My failure");
            Assert.IsFalse(fail == 2);
            Assert.IsFalse(4 == fail);
            Assert.IsTrue(fail != 6);
            Assert.IsTrue(8 != fail);
        }

        [Test]
        public void CustomValueResultEqualityValue()
        {
            // Result ok
            var ok = Result.Ok<int, CustomErrorTest>(42);
            Assert.IsTrue(ok == 42);
            Assert.IsTrue(42 == ok);
            Assert.IsFalse(ok == 12);
            Assert.IsTrue(ok != 12);
            Assert.IsTrue(25 != ok);

            // Result warn
            var person1 = new Person("Jack");
            var person2 = new Person("Jean");
            var warn = Result.Warn<Person, CustomErrorTest>(person1, "My warning");
            Assert.IsTrue(warn == person1);
            Assert.IsTrue(new Person("Jack") == warn);
            Assert.IsFalse(warn == person2);
            Assert.IsTrue(warn != person2);
            Assert.IsFalse(person1 != warn);

            // Result fail
            var fail = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest { ErrorCode = -3 });
            Assert.IsFalse(fail == 2);
            Assert.IsFalse(4 == fail);
            Assert.IsTrue(fail != 6);
            Assert.IsTrue(8 != fail);
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