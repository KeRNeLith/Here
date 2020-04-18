#if SUPPORTS_SERIALIZATION
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using NUnit.Framework;
using static Here.Tests.Eithers.EitherTestHelpers;
using static Here.Tests.Options.OptionTestHelpers;
using static Here.Tests.Results.ResultTestHelpers;

namespace Here.Tests.Serialization
{
    /// <summary>
    /// Tests relative to the serialization of certain types.
    /// </summary>
    [TestFixture]
    internal class SerializationTests : HereTestsBase
    {
        #region Test helpers

        [Pure]
        [NotNull]
        private static T SerializeAndDeserialize<T>([NotNull] T @object)
        {
            // Round-trip the exception: Serialize and de-serialize with a BinaryFormatter
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                // "Save" object state
                bf.Serialize(ms, @object);

                // Re-use the same stream for de-serialization
                ms.Seek(0, 0);

                // Replace the original exception with de-serialized one
                return (T)bf.Deserialize(ms);
            }
        }

        #endregion

        [Test]
        public void NullResultExceptionSerialization()
        {
            Exception exception = new NullResultException();

            // Save the full ToString() value, including the exception message and stack trace.
            string exceptionToString = exception.ToString();

            Exception deserializedException = SerializeAndDeserialize(exception);

            // Double-check that the exception message and stack trace (owned by the base Exception) are preserved
            Assert.AreEqual(exceptionToString, deserializedException.ToString());
        }

        [Test]
        public void OptionSerialization()
        {
            Option<int> option = 12;
            Option<int> deserializedOption = SerializeAndDeserialize(option);
            CheckOptionValue(deserializedOption, 12);

            option = Option<int>.None;
            deserializedOption = SerializeAndDeserialize(option);
            CheckEmptyOption(deserializedOption);
        }

        [Test]
        public void ResultSerialization()
        {
            Result result = Result.Ok();
            Result deserializedResult = SerializeAndDeserialize(result);
            CheckResultOk(deserializedResult);

            result = Result.Warn("Warning message");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn(deserializedResult, "Warning message");

            var exception = new InvalidOperationException("Exception message");
            result = Result.Warn("Warning message", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn_ExceptionEqual(deserializedResult, "Warning message", exception);

            result = Result.Fail("Failure message");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail(deserializedResult, "Failure message");

            result = Result.Fail("Failure message", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail_ExceptionEqual(deserializedResult, "Failure message", exception);
        }

        [Test]
        public void ValueResultSerialization()
        {
            Result<int> result = Result.Ok(12);
            Result<int> deserializedResult = SerializeAndDeserialize(result);
            CheckResultOk(deserializedResult, 12);

            result = Result.Warn(42, "Warning message");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn(deserializedResult, 42, "Warning message");

            var exception = new InvalidOperationException("Exception message");
            result = Result.Warn(77, "Warning message", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn_ExceptionEqual(deserializedResult, 77, "Warning message", exception);

            result = Result.Fail<int>("Failure message");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail(deserializedResult, "Failure message");

            result = Result.Fail<int>("Failure message", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail_ExceptionEqual(deserializedResult, "Failure message", exception);
        }

        [Test]
        public void CustomResultSerialization()
        {
            CustomResult<int> result = Result.CustomOk<int>();
            CustomResult<int> deserializedResult = SerializeAndDeserialize(result);
            CheckResultOk(deserializedResult);

            result = Result.CustomWarn<int>("Warning message");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn(deserializedResult, "Warning message");

            var exception = new InvalidOperationException("Exception message");
            result = Result.CustomWarn<int>("Warning message", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn_ExceptionEqual(deserializedResult, "Warning message", exception);

            result = Result.CustomFail("Failure message", -12);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail_Equal(deserializedResult, "Failure message", -12);

            result = Result.CustomFail("Failure message", -42, exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail_Equal(deserializedResult, "Failure message", -42, exception);
        }

        [Test]
        public void ValueCustomResultSerialization()
        {
            Result<int, string> result = Result.Ok<int, string>(12);
            Result<int, string> deserializedResult = SerializeAndDeserialize(result);
            CheckResultOk(deserializedResult, 12);

            result = Result.Warn<int, string>(42, "Warning message");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn(deserializedResult, 42, "Warning message");

            var exception = new InvalidOperationException("Exception message");
            result = Result.Warn<int, string>(77, "Warning message", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultWarn_ExceptionEqual(deserializedResult, 77, "Warning message", exception);

            result = Result.Fail<int, string>("Failure message", "Custom error");
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail_Equal(deserializedResult, "Failure message", "Custom error");

            result = Result.Fail<int, string>("Failure message", "Custom error", exception);
            deserializedResult = SerializeAndDeserialize(result);
            CheckResultFail_Equal(deserializedResult, "Failure message", "Custom error", exception);
        }

        [Test]
        public void EitherSerialization()
        {
            Either<int, string> either = Either<int, string>.Left(8);
            Either<int, string> deserializedEither = SerializeAndDeserialize(either);
            CheckLeftEither(deserializedEither, 8);

            either = Either<int, string>.Right("Right value");
            deserializedEither = SerializeAndDeserialize(either);
            CheckRightEither(deserializedEither, "Right value");

            either = Either<int, string>.None;
            deserializedEither = SerializeAndDeserialize(either);
            CheckNoneEither(deserializedEither);
        }

        [Test]
        public void EitherLeftSerialization()
        {
            EitherLeft<int> either = new EitherLeft<int>(42);
            EitherLeft<int> deserializedEither = SerializeAndDeserialize(either);
            CheckEitherLeft(deserializedEither, 42);
        }

        [Test]
        public void EitherRightSerialization()
        {
            EitherRight<int> either = new EitherRight<int>(25);
            EitherRight<int> deserializedEither = SerializeAndDeserialize(either);
            CheckEitherRight(deserializedEither, 25);
        }
    }
}
#endif