using System;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> conversions to results.
    /// </summary>
    [TestFixture]
    internal class OptionConversionsTests : ResultTestsBase
    {
        [Test]
        public void OptionToResult()
        {
            // Option has value
            // Explicit conversion
            var optionInt = Option<int>.Some(42);
            Result result = optionInt.ToResult();
            CheckResultOk(result);

            result = optionInt.ToResult("Empty optionInt");
            CheckResultOk(result);

            // Implicit conversion
            result = optionInt;
            CheckResultOk(result);

            // Empty Option
            // Explicit conversion
            var emptyOptionInt = Option<int>.None;
            result = emptyOptionInt.ToResult();
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)));

            result = emptyOptionInt.ToResult("Empty emptyOptionInt");
            CheckResultFail(result, "Empty emptyOptionInt");

            // Implicit conversion
            result = emptyOptionInt;
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)));
        }

        [Test]
        public void OptionToValueResult()
        {
            // Option has value
            // Explicit conversion
            var optionInt = Option<int>.Some(42);
            Result<int> result = optionInt.ToValueResult();
            CheckResultOk(result, 42);

            result = optionInt.ToValueResult("Empty optionInt");
            CheckResultOk(result, 42);

            // Implicit conversion
            result = optionInt;
            CheckResultOk(result, 42);

            // Empty Option
            // Explicit conversion
            var emptyOptionInt = Option<int>.None;
            result = emptyOptionInt.ToValueResult();
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)));

            result = emptyOptionInt.ToValueResult("Empty emptyOptionInt");
            CheckResultFail(result, "Empty emptyOptionInt");

            // Implicit conversion
            result = emptyOptionInt;
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)));
        }

        [Test]
        public void OptionToCustomResult()
        {
            // Option has value
            var customErrorObject = new CustomErrorTest { ErrorCode = -32 };
            var optionInt = Option<int>.Some(42);
            CustomResult<CustomErrorTest> result = optionInt.ToCustomResult(customErrorObject);
            CheckResultOk(result);

            result = optionInt.ToCustomResult(customErrorObject, "Empty optionInt");
            CheckResultOk(result);

            result = optionInt.ToCustomResult(() => customErrorObject);
            CheckResultOk(result);

            result = optionInt.ToCustomResult(() => customErrorObject, "Empty optionInt");
            CheckResultOk(result);

            // Empty Option
            var emptyOptionInt = Option<int>.None;
            result = emptyOptionInt.ToCustomResult(customErrorObject);
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyOptionInt.ToCustomResult(customErrorObject, "Empty emptyOptionInt");
            CheckResultFail(result, "Empty emptyOptionInt", customErrorObject);

            result = emptyOptionInt.ToCustomResult(() => customErrorObject);
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyOptionInt.ToCustomResult(() => customErrorObject, "Empty emptyOptionInt");
            CheckResultFail(result, "Empty emptyOptionInt", customErrorObject);
            
            Assert.Throws<ArgumentNullException>(() => optionInt.ToCustomResult((CustomErrorTest)null, "Useless"));
            Assert.Throws<ArgumentNullException>(() => optionInt.ToCustomResult((Func<CustomErrorTest>)null, "Useless"));
        }

        [Test]
        public void OptionToValueCustomResult()
        {
            // Option has value
            var customErrorObject = new CustomErrorTest { ErrorCode = -32 };
            var optionInt = Option<int>.Some(42);
            Result<int, CustomErrorTest> result = optionInt.ToValueCustomResult(customErrorObject);
            CheckResultOk(result, 42);

            result = optionInt.ToValueCustomResult(customErrorObject, "Empty optionInt");
            CheckResultOk(result, 42);

            result = optionInt.ToValueCustomResult(() => customErrorObject);
            CheckResultOk(result, 42);

            result = optionInt.ToValueCustomResult(() => customErrorObject, "Empty optionInt");
            CheckResultOk(result, 42);

            // Empty Option
            var emptyOptionInt = Option<int>.None;
            result = emptyOptionInt.ToValueCustomResult(customErrorObject);
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyOptionInt.ToValueCustomResult(customErrorObject, "Empty emptyOptionInt");
            CheckResultFail(result, "Empty emptyOptionInt", customErrorObject);

            result = emptyOptionInt.ToValueCustomResult(() => customErrorObject);
            CheckResultFail(result, string.Format(Option.FailedToResultMessage, typeof(int)), customErrorObject);

            result = emptyOptionInt.ToValueCustomResult(() => customErrorObject, "Empty emptyOptionInt");
            CheckResultFail(result, "Empty emptyOptionInt", customErrorObject);

            Assert.Throws<ArgumentNullException>(() => optionInt.ToValueCustomResult((CustomErrorTest)null, "Useless"));
            Assert.Throws<ArgumentNullException>(() => optionInt.ToValueCustomResult((Func<CustomErrorTest>)null, "Useless"));
        }
    }
}