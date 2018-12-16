using NUnit.Framework;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> conversions to <see cref="Option{T}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultConversionsTests : OptionTestsBase
    {
        #region Test class

        private class CustomErrorTest
        {
        }

        #endregion

        [Test]
        public void ResultToOption()
        {
            // Explicit conversions
            Result resultOk = Result.Ok();
            Option<bool> optionBool = resultOk.ToOption();
            CheckOptionValue(optionBool, true);

            Result resultWarn = Result.Warn("Warning");
            optionBool = resultWarn.ToOption();
            CheckOptionValue(optionBool, true);

            Result resultFail = Result.Fail("Failure");
            optionBool = resultFail.ToOption();
            CheckOptionValue(optionBool, false);

            // Implicit conversions
            optionBool = resultOk;
            CheckOptionValue(optionBool, true);

            optionBool = resultWarn;
            CheckOptionValue(optionBool, true);

            optionBool = resultFail;
            CheckOptionValue(optionBool, false);
        }

        [Test]
        public void ValueResultToOption()
        {
            // Explicit conversions
            Result<int> resultOk = Result.Ok(42);
            Option<int> optionInt = resultOk.ToOption();
            CheckOptionValue(optionInt, 42);

            Result<int> resultWarn = Result.Warn(12, "Warning");
            optionInt = resultWarn.ToOption();
            CheckOptionValue(optionInt, 12);

            Result<int> resultFail = Result.Fail<int>("Failure");
            optionInt = resultFail.ToOption();
            CheckEmptyOption(optionInt);

            // Implicit conversions
            optionInt = resultOk;
            CheckOptionValue(optionInt, 42);

            optionInt = resultWarn;
            CheckOptionValue(optionInt, 12);

            optionInt = resultFail;
            CheckEmptyOption(optionInt);
        }

        [Test]
        public void CustomResultToOption()
        {
            // Explicit conversions
            CustomResult<CustomErrorTest> resultOk = Result.CustomOk<CustomErrorTest>();
            Option<bool> optionBool = resultOk.ToOption();
            CheckOptionValue(optionBool, true);

            CustomResult<CustomErrorTest> resultWarn = Result.CustomWarn<CustomErrorTest>("Warning");
            optionBool = resultWarn.ToOption();
            CheckOptionValue(optionBool, true);

            CustomResult<CustomErrorTest> resultFail = Result.CustomFail("Failure", new CustomErrorTest());
            optionBool = resultFail.ToOption();
            CheckOptionValue(optionBool, false);

            // Implicit conversions
            optionBool = resultOk;
            CheckOptionValue(optionBool, true);

            optionBool = resultWarn;
            CheckOptionValue(optionBool, true);

            optionBool = resultFail;
            CheckOptionValue(optionBool, false);
        }

        [Test]
        public void ValueCustomResultToOption()
        {
            // Explicit conversions
            Result<int, CustomErrorTest> resultOk = Result.Ok<int, CustomErrorTest>(42);
            Option<int> optionInt = resultOk.ToOption();
            CheckOptionValue(optionInt, 42);

            Result<int, CustomErrorTest> resultWarn = Result.Warn<int, CustomErrorTest>(12, "Warning");
            optionInt = resultWarn.ToOption();
            CheckOptionValue(optionInt, 12);

            Result<int, CustomErrorTest> resultFail = Result.Fail<int, CustomErrorTest>("Failure", new CustomErrorTest());
            optionInt = resultFail.ToOption();
            CheckEmptyOption(optionInt);

            // Implicit conversions
            optionInt = resultOk;
            CheckOptionValue(optionInt, 42);

            optionInt = resultWarn;
            CheckOptionValue(optionInt, 12);

            optionInt = resultFail;
            CheckEmptyOption(optionInt);
        }
    }
}