using System;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Eithers
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> conversions to <see cref="Either{TLeft,TRight}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultConversionsTests : EitherTestsBase
    {
        #region Test class

        private class CustomErrorTest
        {
        }

        #endregion

        [Test]
        public void ValueResultToEither()
        {
            Result<int> resultOk = Result.Ok(42);
            Either<string, int> eitherStringInt = resultOk.ToEither();
            CheckRightEither(eitherStringInt, 42);

            Result<int> resultWarn = Result.Warn(12, "Warning");
            eitherStringInt = resultWarn.ToEither();
            CheckRightEither(eitherStringInt, 12);

            Result<Person> resultPersonOk = Result.Ok<Person>(null);
            Either<string, Person> eitherStringPerson = resultPersonOk.ToEither();
            CheckLeftEither(eitherStringPerson, ResultConstants.ValueResultToEitherNullValue);

            Result<Person> resultPersonWarn = Result.Warn<Person>(null, "Warning");
            eitherStringPerson = resultPersonWarn.ToEither();
            CheckLeftEither(eitherStringPerson, ResultConstants.ValueResultToEitherNullValue);

            Result<int> resultFail = Result.Fail<int>("Failure");
            eitherStringInt = resultFail.ToEither();
            CheckLeftEither(eitherStringInt, "Failure");
        }

        [Test]
        public void ValueCustomResultToMessageEither()
        {
            Result<int, CustomErrorTest> resultOk = Result.Ok<int, CustomErrorTest>(42);
            Either<string, int> eitherStringInt = resultOk.ToMessageEither();
            CheckRightEither(eitherStringInt, 42);

            Result<int, CustomErrorTest> resultWarn = Result.Warn<int, CustomErrorTest>(12, "Warning");
            eitherStringInt = resultWarn.ToMessageEither();
            CheckRightEither(eitherStringInt, 12);

            Result<Person, CustomErrorTest> resultPersonOk = Result.Ok<Person, CustomErrorTest>(null);
            Either<string, Person> eitherStringPerson = resultPersonOk.ToMessageEither();
            CheckLeftEither(eitherStringPerson, ResultConstants.ValueCustomResultToEitherNullValue);

            Result<Person, CustomErrorTest> resultPersonWarn = Result.Warn<Person, CustomErrorTest>(null, "Warning");
            eitherStringPerson = resultPersonWarn.ToMessageEither();
            CheckLeftEither(eitherStringPerson, ResultConstants.ValueCustomResultToEitherNullValue);

            Result<int, CustomErrorTest> resultFail = Result.Fail<int, CustomErrorTest>("Failure", new CustomErrorTest());
            eitherStringInt = resultFail.ToMessageEither();
            CheckLeftEither(eitherStringInt, "Failure");
        }

        [Test]
        public void ValueCustomResultToEither()
        {
            Result<int, CustomErrorTest> resultOk = Result.Ok<int, CustomErrorTest>(42);
            Either<CustomErrorTest, int> eitherErrorInt = resultOk.ToEither();
            CheckRightEither(eitherErrorInt, 42);

            Result<int, CustomErrorTest> resultWarn = Result.Warn<int, CustomErrorTest>(12, "Warning");
            eitherErrorInt = resultWarn.ToEither();
            CheckRightEither(eitherErrorInt, 12);

            Result<Person, CustomErrorTest> resultPersonOk = Result.Ok<Person, CustomErrorTest>(null);
            Assert.Throws<InvalidOperationException>(() => resultPersonOk.ToEither());

            Result<Person, CustomErrorTest> resultPersonWarn = Result.Warn<Person, CustomErrorTest>(null, "Warning");
            Assert.Throws<InvalidOperationException>(() => resultPersonWarn.ToEither());

            var customErrorObject = new CustomErrorTest();
            Result<int, CustomErrorTest> resultFail = Result.Fail<int, CustomErrorTest>("Failure", customErrorObject);
            eitherErrorInt = resultFail.ToEither();
            CheckLeftEither(eitherErrorInt, customErrorObject);
        }
    }
}