using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Either{TLeft,TRight}"/> conversions to <see cref="Option{T}"/>.
    /// </summary>
    [TestFixture]
    internal class EitherConversionsTests : OptionTestsBase
    {
        [Test]
        public void EitherToOption()
        {
            // Either left
            var eitherLeft = Either<string, int>.Left("Error");
            Option<int> optionInt = eitherLeft.ToOption();
            CheckEmptyOption(optionInt);

            // Either right
            var eitherRight = Either<string, int>.Right(42);
            optionInt = eitherRight.ToOption();
            CheckOptionValue(optionInt, 42);

            // Either none
            var eitherNone = Either<string, int>.None;
            optionInt = eitherNone.ToOption();
            CheckEmptyOption(optionInt);
        }
    }
}