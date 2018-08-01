using NUnit.Framework;
using Here.Maybes;
using Here.Tests.Maybes;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> conversions.
    /// </summary>
    [TestFixture]
    internal class ResultConversionsTests : MaybeTestsBase
    {
        #region Test class

        private class CustomErrorTest
        {
        }

        #endregion

        [Test]
        public void ResultToMaybe()
        {
            // Explicit conversions
            Result resultOK = Result.Ok();
            Maybe<bool> maybeBool = resultOK.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            Result resultWarn = Result.Warn("Warning");
            maybeBool = resultWarn.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            Result resultFail = Result.Fail("Failure");
            maybeBool = resultFail.ToMaybe();
            CheckMaybeValue(maybeBool, false);

            // Implicit conversions
            maybeBool = resultOK;
            CheckMaybeValue(maybeBool, true);

            maybeBool = resultWarn;
            CheckMaybeValue(maybeBool, true);

            maybeBool = resultFail;
            CheckMaybeValue(maybeBool, false);
        }

        [Test]
        public void ValueResultToMaybe()
        {
            // Explicit conversions
            Result<int> resultOK = Result.Ok(42);
            Maybe<int> maybeInt = resultOK.ToMaybe();
            CheckMaybeValue(maybeInt, 42);

            Result<int> resultWarn = Result.Warn(12, "Warning");
            maybeInt = resultWarn.ToMaybe();
            CheckMaybeValue(maybeInt, 12);

            Result<int> resultFail = Result.Fail<int>("Failure");
            maybeInt = resultFail.ToMaybe();
            CheckEmptyMaybe(maybeInt);

            // Implicit conversions
            maybeInt = resultOK;
            CheckMaybeValue(maybeInt, 42);

            maybeInt = resultWarn;
            CheckMaybeValue(maybeInt, 12);

            maybeInt = resultFail;
            CheckEmptyMaybe(maybeInt);
        }

        [Test]
        public void CustomResultToMaybe()
        {
            // Explicit conversions
            CustomResult<CustomErrorTest> resultOK = Result.CustomOk<CustomErrorTest>();
            Maybe<bool> maybeBool = resultOK.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            CustomResult<CustomErrorTest> resultWarn = Result.CustomWarn<CustomErrorTest>("Warning");
            maybeBool = resultWarn.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            CustomResult<CustomErrorTest> resultFail = Result.CustomFail("Failure", new CustomErrorTest());
            maybeBool = resultFail.ToMaybe();
            CheckMaybeValue(maybeBool, false);

            // Implicit conversions
            maybeBool = resultOK;
            CheckMaybeValue(maybeBool, true);

            maybeBool = resultWarn;
            CheckMaybeValue(maybeBool, true);

            maybeBool = resultFail;
            CheckMaybeValue(maybeBool, false);
        }

        [Test]
        public void CustomValueResultToMaybe()
        {
            // Explicit conversions
            Result<int, CustomErrorTest> resultOK = Result.Ok<int, CustomErrorTest>(42);
            Maybe<int> maybeInt = resultOK.ToMaybe();
            CheckMaybeValue(maybeInt, 42);

            Result<int, CustomErrorTest> resultWarn = Result.Warn<int, CustomErrorTest>(12, "Warning");
            maybeInt = resultWarn.ToMaybe();
            CheckMaybeValue(maybeInt, 12);

            Result<int, CustomErrorTest> resultFail = Result.Fail<int, CustomErrorTest>("Failure", new CustomErrorTest());
            maybeInt = resultFail.ToMaybe();
            CheckEmptyMaybe(maybeInt);

            // Implicit conversions
            maybeInt = resultOK;
            CheckMaybeValue(maybeInt, 42);

            maybeInt = resultWarn;
            CheckMaybeValue(maybeInt, 12);

            maybeInt = resultFail;
            CheckEmptyMaybe(maybeInt);
        }
    }
}