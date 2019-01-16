using System;
using Here.Extensions;
using NUnit.Framework;

namespace Here.Tests.Eithers
{
    /// <summary>
    /// Tests for <see cref="Either{TLeft,TRight}"/> Linq extensions.
    /// </summary>
    [TestFixture]
    internal class EitherLinqExtensionsTests : EitherTestsBase
    {
        [Test]
        public void EitherAny()
        {
            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Assert.IsFalse(eitherLeft.Any());
            Assert.IsFalse(eitherLeft.Any(r => r == 1));

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            Assert.IsTrue(eitherRight.Any());
            Assert.IsFalse(eitherRight.Any(r => r == 1));
            Assert.IsTrue(eitherRight.Any(r => r == 42));

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.IsFalse(eitherNone.Any());
            Assert.IsFalse(eitherNone.Any(r => r == 1));

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Any(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Any(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Any(null));
        }

        [Test]
        public void EitherAll()
        {
            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Assert.IsFalse(eitherLeft.All(r => r == 1));

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            Assert.IsFalse(eitherRight.All(r => r == 1));
            Assert.IsTrue(eitherRight.All(r => r == 42));

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.IsFalse(eitherNone.All(r => r == 1));

            Assert.Throws<ArgumentNullException>(() => eitherLeft.All(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.All(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.All(null));
        }

        [Test]
        public void EitherContains()
        {
            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Assert.IsFalse(eitherLeft.Contains(12));

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            Assert.IsFalse(eitherRight.Contains(12));
            Assert.IsTrue(eitherRight.Contains(42));

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.IsFalse(eitherNone.Contains(12));
        }

        [Test]
        public void EitherSelect()
        {
            int counterRight = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<string, float> result = eitherLeft.Select(
                r =>
                {
                    ++counterRight;
                    return 42.5f;
                });
            Assert.AreEqual(0, counterRight);
            CheckLeftEither(result, "A string");

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Select(
                r =>
                {
                    ++counterRight;
                    return 47.5f;
                });
            Assert.AreEqual(1, counterRight);
            CheckRightEither(result, 47.5f);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.Select(
                r =>
                {
                    ++counterRight;
                    return 50.5f;
                });
            Assert.AreEqual(1, counterRight);
            CheckNoneEither(result);

            // Null return
            Either<string, TestClass> result2 = Either.Left<string, int>("str").Select(
                    r =>
                    {
                        ++counterRight;
                        return (TestClass)null;
                    });
            Assert.AreEqual(1, counterRight);
            CheckLeftEither(result2, "str");

            Assert.Throws<NullResultException>(() => Either.Right<string, int>(42).Select(
                r =>
                {
                    ++counterRight;
                    return (TestClass)null;
                }));
            Assert.AreEqual(2, counterRight);

            result2 = eitherNone.Select(
                r =>
                {
                    ++counterRight;
                    return (TestClass)null;
                });
            Assert.AreEqual(2, counterRight);
            CheckNoneEither(result2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Select((Func<int, float>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Select((Func<int, float>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Select((Func<int, float>)null));
        }

        [Test]
        public void EitherWhere()
        {
            int counterRight = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<string, int> result = eitherLeft.Where(
                r =>
                {
                    ++counterRight;
                    return true;
                });
            Assert.AreEqual(0, counterRight);
            CheckNoneEither(result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Where(
                r =>
                {
                    ++counterRight;
                    return true;
                });
            Assert.AreEqual(1, counterRight);
            CheckRightEither(result, 42);

            result = eitherRight.Where(
                r =>
                {
                    ++counterRight;
                    return false;
                });
            Assert.AreEqual(2, counterRight);
            CheckNoneEither(result);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.Where(
                r =>
                {
                    ++counterRight;
                    return true;
                });
            Assert.AreEqual(2, counterRight);
            CheckNoneEither(result);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Where(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Where(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Where(null));
        }

        [Test]
        public void EitherForEach()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            eitherLeft.ForEach(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(0, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            eitherRight.ForEach(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            eitherNone.ForEach(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.ForEach(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.ForEach(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.ForEach(null));
        }

        [Test]
        public void EitherAggregate()
        {
            var testObject = new TestClass();

            #region Local function

            void CheckAggregate(Either<string, int> either, bool expectRight, float expectedResult = 1.0f)
            {
                int counter = 0;
                float res = either.Aggregate(
                    1.0f,
                    (float seed, int r) =>
                    {
                        ++counter;
                        return seed + r;
                    });
                Assert.AreEqual(expectRight ? 1 : 0, counter);
                Assert.AreEqual(expectRight ? expectedResult : 1.0f, res);
            }

            void CheckAggregateNullReturn(Either<string, int> either, bool expectThrow)
            {
                int counter = 0;
                if (expectThrow)
                {
                    Assert.Throws<NullResultException>(
                        () => either.Aggregate(
                            testObject,
                            (TestClass seed, int r) =>
                            {
                                ++counter;
                                return null;
                            }));
                    Assert.AreEqual(1, counter);
                }
                else
                {
                    TestClass result = either.Aggregate(
                        testObject,
                        (TestClass seed, int r) =>
                        {
                            ++counter;
                            return null;
                        });
                    Assert.AreEqual(0, counter);
                    Assert.AreSame(testObject, result);
                }
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckAggregate(eitherLeft, false);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckAggregate(eitherRight, true, 43.0f);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckAggregate(eitherNone, false);

            // Null return
            CheckAggregateNullReturn(eitherLeft, false);
            CheckAggregateNullReturn(eitherRight, true);
            CheckAggregateNullReturn(eitherNone, false);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Fold(null, (TestClass seed, int r) => seed));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Fold(null, (TestClass seed, int r) => seed));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Fold(null, (TestClass seed, int r) => seed));

            // Cannot test this in NET20 and NET30 due to NUnit package
#if !NET20 && !NET30
            Assert.Throws<ArgumentNullException>(() => eitherLeft.Aggregate(testObject, (Func<TestClass, int, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.Aggregate(null, (Func<TestClass, int, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Aggregate(testObject, (Func<TestClass, int, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Aggregate(null, (Func<TestClass, int, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Aggregate(testObject, (Func<TestClass, int, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Aggregate(null, (Func<TestClass, int, TestClass>)null));
#endif
        }
    }
}