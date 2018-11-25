using NUnit.Framework;
using Here.Tests.Maybes;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> conversions to <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultConversionsMaybeTests : MaybeTestsBase
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
            Result resultOk = Result.Ok();
            Maybe<bool> maybeBool = resultOk.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            Result resultWarn = Result.Warn("Warning");
            maybeBool = resultWarn.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            Result resultFail = Result.Fail("Failure");
            maybeBool = resultFail.ToMaybe();
            CheckMaybeValue(maybeBool, false);

            // Implicit conversions
            maybeBool = resultOk;
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
            Result<int> resultOk = Result.Ok(42);
            Maybe<int> maybeInt = resultOk.ToMaybe();
            CheckMaybeValue(maybeInt, 42);

            Result<int> resultWarn = Result.Warn(12, "Warning");
            maybeInt = resultWarn.ToMaybe();
            CheckMaybeValue(maybeInt, 12);

            Result<int> resultFail = Result.Fail<int>("Failure");
            maybeInt = resultFail.ToMaybe();
            CheckEmptyMaybe(maybeInt);

            // Implicit conversions
            maybeInt = resultOk;
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
            CustomResult<CustomErrorTest> resultOk = Result.CustomOk<CustomErrorTest>();
            Maybe<bool> maybeBool = resultOk.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            CustomResult<CustomErrorTest> resultWarn = Result.CustomWarn<CustomErrorTest>("Warning");
            maybeBool = resultWarn.ToMaybe();
            CheckMaybeValue(maybeBool, true);

            CustomResult<CustomErrorTest> resultFail = Result.CustomFail("Failure", new CustomErrorTest());
            maybeBool = resultFail.ToMaybe();
            CheckMaybeValue(maybeBool, false);

            // Implicit conversions
            maybeBool = resultOk;
            CheckMaybeValue(maybeBool, true);

            maybeBool = resultWarn;
            CheckMaybeValue(maybeBool, true);

            maybeBool = resultFail;
            CheckMaybeValue(maybeBool, false);
        }

        [Test]
        public void ValueCustomResultToMaybe()
        {
            // Explicit conversions
            Result<int, CustomErrorTest> resultOk = Result.Ok<int, CustomErrorTest>(42);
            Maybe<int> maybeInt = resultOk.ToMaybe();
            CheckMaybeValue(maybeInt, 42);

            Result<int, CustomErrorTest> resultWarn = Result.Warn<int, CustomErrorTest>(12, "Warning");
            maybeInt = resultWarn.ToMaybe();
            CheckMaybeValue(maybeInt, 12);

            Result<int, CustomErrorTest> resultFail = Result.Fail<int, CustomErrorTest>("Failure", new CustomErrorTest());
            maybeInt = resultFail.ToMaybe();
            CheckEmptyMaybe(maybeInt);

            // Implicit conversions
            maybeInt = resultOk;
            CheckMaybeValue(maybeInt, 42);

            maybeInt = resultWarn;
            CheckMaybeValue(maybeInt, 12);

            maybeInt = resultFail;
            CheckEmptyMaybe(maybeInt);
        }
    }
}