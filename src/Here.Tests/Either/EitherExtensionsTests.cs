using System;
using Here.Extensions;
using NUnit.Framework;

namespace Here.Tests.Eithers
{
    /// <summary>
    /// Tests for <see cref="Either{TLeft,TRight}"/> extensions.
    /// </summary>
    [TestFixture]
    internal class EitherExtensionsTests : EitherTestsBase
    {
        #region Match

        [Test]
        public void EitherMatchUnit()
        {
            int counterRight = 0;
            int counterLeft = 0;
            int counterNone = 0;

            #region Local function

            void CheckCounters(int left, int right, int none)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
                Assert.AreEqual(none, counterNone);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            eitherLeft.Match(
                r =>
                {
                    ++counterRight;
                },
                l =>
                {
                    ++counterLeft;
                });
            CheckCounters(1, 0, 0);

            eitherLeft.Match(
                r =>
                {
                    ++counterRight;
                },
                l =>
                {
                    ++counterLeft;
                },
                () =>
                {
                    ++counterNone;
                });
            CheckCounters(2, 0, 0);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            eitherRight.Match(
                r =>
                {
                    ++counterRight;
                },
                l =>
                {
                    ++counterLeft;
                });
            CheckCounters(2, 1, 0);

            eitherRight.Match(
                r =>
                {
                    ++counterRight;
                },
                l =>
                {
                    ++counterLeft;
                },
                () =>
                {
                    ++counterNone;
                });
            CheckCounters(2, 2, 0);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.Match(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }));
            CheckCounters(2, 2, 0);

            eitherNone.Match(
                r =>
                {
                    ++counterRight;
                },
                l =>
                {
                    ++counterLeft;
                },
                () =>
                {
                    ++counterNone;
                });
            CheckCounters(2, 2, 1);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Match(r => { }, null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.Match(null, l => { }));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.Match(null, null));

            Assert.Throws<ArgumentNullException>(() => eitherRight.Match(r => { }, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Match(null, l => { }));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Match(null, null));

            Assert.Throws<ArgumentNullException>(() => eitherNone.Match(r => { }, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Match(null, l => { }));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Match(null, null));
        }

        [Test]
        public void EitherMatchNullable()
        {
            int counterRight = 0;
            int counterLeft = 0;
            int counterNone = 0;

            #region Local function

            void CheckCounters(int left, int right, int none)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
                Assert.AreEqual(none, counterNone);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            int result = eitherLeft.MatchNullable(
                r =>
                {
                    ++counterRight;
                    return 12;
                },
                l =>
                {
                    ++counterLeft;
                    return 42;
                });
            CheckCounters(1, 0, 0);
            Assert.AreEqual(42, result);

            result = eitherLeft.MatchNullable(
                r =>
                {
                    ++counterRight;
                    return 13;
                },
                l =>
                {
                    ++counterLeft;
                    return 43;
                },
                () =>
                {
                    ++counterNone;
                    return -3;
                });
            CheckCounters(2, 0, 0);
            Assert.AreEqual(43, result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.MatchNullable(
                r =>
                {
                    ++counterRight;
                    return 12;
                },
                l =>
                {
                    ++counterLeft;
                    return 42;
                });
            CheckCounters(2, 1, 0);
            Assert.AreEqual(12, result);

            result = eitherRight.MatchNullable(
                r =>
                {
                    ++counterRight;
                    return 13;
                },
                l =>
                {
                    ++counterLeft;
                    return 43;
                },
                () =>
                {
                    ++counterNone;
                    return -3;
                });
            CheckCounters(2, 2, 0);
            Assert.AreEqual(13, result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.MatchNullable(
                    r =>
                    {
                        ++counterRight;
                        return 12;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return 42;
                    }));
            CheckCounters(2, 2, 0);

            result = eitherNone.MatchNullable(
                r =>
                {
                    ++counterRight;
                    return 14;
                },
                l =>
                {
                    ++counterLeft;
                    return 44;
                },
                () =>
                {
                    ++counterNone;
                    return -4;
                });
            CheckCounters(2, 2, 1);
            Assert.AreEqual(-4, result);

            // Null return
            Assert.IsNull(Either.Left<string, int>("str").MatchNullable(
                r =>
                {
                    ++counterRight;
                    return "Never here";
                },
                l =>
                {
                    ++counterLeft;
                    return null;
                }));
            CheckCounters(3, 2, 1);

            Assert.IsNull(Either.Right<int, string>("str").MatchNullable(
                r =>
                {
                    ++counterRight;
                    return null;
                },
                l =>
                {
                    ++counterLeft;
                    return "Never here";
                }));
            CheckCounters(3, 3, 1);

            Assert.IsNull(Either<int, string>.None.MatchNullable(
                r =>
                {
                    ++counterRight;
                    return "Never here";
                },
                l =>
                {
                    ++counterLeft;
                    return "Never here";
                },
                () =>
                {
                    ++counterNone;
                    return null;
                }));
            CheckCounters(3, 3, 2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.MatchNullable(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.MatchNullable(null, l => 12.5f));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.MatchNullable((Func<int, float>)null, null));

            Assert.Throws<ArgumentNullException>(() => eitherRight.MatchNullable(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.MatchNullable(null, l => 12.5f));
            Assert.Throws<ArgumentNullException>(() => eitherRight.MatchNullable((Func<int, float>)null, null));

            Assert.Throws<ArgumentNullException>(() => eitherNone.MatchNullable(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.MatchNullable(null, l => 12.5f));
            Assert.Throws<ArgumentNullException>(() => eitherNone.MatchNullable((Func<int, float>)null, null));
        }

        [Test]
        public void EitherMatch()
        {
            int counterRight = 0;
            int counterLeft = 0;
            int counterNone = 0;

            #region Local function

            void CheckCounters(int left, int right, int none)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
                Assert.AreEqual(none, counterNone);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            int result = eitherLeft.Match(
                r =>
                {
                    ++counterRight;
                    return 12;
                },
                l =>
                {
                    ++counterLeft;
                    return 42;
                });
            CheckCounters(1, 0, 0);
            Assert.AreEqual(42, result);

            result = eitherLeft.Match(
                r =>
                {
                    ++counterRight;
                    return 13;
                },
                l =>
                {
                    ++counterLeft;
                    return 43;
                },
                () =>
                {
                    ++counterNone;
                    return -3;
                });
            CheckCounters(2, 0, 0);
            Assert.AreEqual(43, result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Match(
                r =>
                {
                    ++counterRight;
                    return 12;
                },
                l =>
                {
                    ++counterLeft;
                    return 42;
                });
            CheckCounters(2, 1, 0);
            Assert.AreEqual(12, result);

            result = eitherRight.Match(
                r =>
                {
                    ++counterRight;
                    return 13;
                },
                l =>
                {
                    ++counterLeft;
                    return 43;
                },
                () =>
                {
                    ++counterNone;
                    return -3;
                });
            CheckCounters(2, 2, 0);
            Assert.AreEqual(13, result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.Match(
                    r =>
                    {
                        ++counterRight;
                        return 12;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return 42;
                    }));
            CheckCounters(2, 2, 0);

            result = eitherNone.Match(
                r =>
                {
                    ++counterRight;
                    return 14;
                },
                l =>
                {
                    ++counterLeft;
                    return 44;
                },
                () =>
                {
                    ++counterNone;
                    return -4;
                });
            CheckCounters(2, 2, 1);
            Assert.AreEqual(-4, result);

            // Null return
            Assert.Throws<NullResultException>(
                () => Either.Left<string, int>("str").Match(
                    r =>
                    {
                        ++counterRight;
                        return "Never here";
                    },
                    l =>
                    {
                        ++counterLeft;
                        return null;
                    }));
            CheckCounters(3, 2, 1);

            Assert.Throws<NullResultException>(
                () => Either.Right<int, string>("str").Match(
                    r =>
                    {
                        ++counterRight;
                        return null;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return "Never here";
                    }));
            CheckCounters(3, 3, 1);

            Assert.Throws<NullResultException>(
                () => Either<int, string>.None.Match(
                    r =>
                    {
                        ++counterRight;
                        return "Never here";
                    },
                    l =>
                    {
                        ++counterLeft;
                        return "Never here";
                    },
                    () =>
                    {
                        ++counterNone;
                        return null;
                    }));
            CheckCounters(3, 3, 2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Match(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.Match(null, l => 12.5f));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.Match((Func<int, float>)null, null));
            Assert.Throws<NullResultException>(() => eitherLeft.Match(r => "never executed", l => null));

            Assert.Throws<ArgumentNullException>(() => eitherRight.Match(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Match(null, l => 12.5f));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Match((Func<int, float>)null, null));
            Assert.Throws<NullResultException>(() => eitherRight.Match(r => null, l => "never executed"));

            Assert.Throws<ArgumentNullException>(() => eitherNone.Match(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Match(null, l => 12.5f));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Match((Func<int, float>)null, null));
            Assert.Throws<NullResultException>(() => eitherNone.Match(r => "never executed", l => "never executed", () => null));
        }

        #endregion

        #region Map/BiMap

        [Test]
        public void EitherMapLeft()
        {
            int counterLeft = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<float, int> result = eitherLeft.Map(
                (string l) =>
                {
                    ++counterLeft;
                    return 42.5f;
                });
            Assert.AreEqual(1, counterLeft);
            CheckLeftEither(result, 42.5f);
            
            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Map(
                (string l) =>
                {
                    ++counterLeft;
                    return 47.5f;
                });
            Assert.AreEqual(1, counterLeft);
            CheckRightEither(result, 42);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.Map(
                (string l) =>
                {
                    ++counterLeft;
                    return 50.5f;
                });
            Assert.AreEqual(1, counterLeft);
            CheckNoneEither(result);

            // Null return
            Assert.Throws<NullResultException>(
                () => Either.Left<string, int>("str").Map(
                    (string l) =>
                    {
                        ++counterLeft;
                        return (TestClass)null;
                    }));
            Assert.AreEqual(2, counterLeft);

            Either<TestClass, int> result2 = Either.Right<string, int>(42).Map(
                (string l) =>
                {
                    ++counterLeft;
                    return (TestClass)null;
                });
            Assert.AreEqual(2, counterLeft);
            CheckRightEither(result2, 42);

            result2 = eitherNone.Map(
                (string l) =>
                {
                    ++counterLeft;
                    return (TestClass)null;
                });
            Assert.AreEqual(2, counterLeft);
            CheckNoneEither(result2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Map((Func<string, float>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Map((Func<string, float>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Map((Func<string, float>)null));
        }

        [Test]
        public void EitherMapRight()
        {
            int counterRight = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<string, float> result = eitherLeft.Map(
                (int r) =>
                {
                    ++counterRight;
                    return 42.5f;
                });
            Assert.AreEqual(0, counterRight);
            CheckLeftEither(result, "A string");

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Map(
                (int r) =>
                {
                    ++counterRight;
                    return 47.5f;
                });
            Assert.AreEqual(1, counterRight);
            CheckRightEither(result, 47.5f);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.Map(
                (int r) =>
                {
                    ++counterRight;
                    return 50.5f;
                });
            Assert.AreEqual(1, counterRight);
            CheckNoneEither(result);

            // Null return
            Either<string, TestClass> result2 = Either.Left<string, int>("str").Map(
                    (int r) =>
                    {
                        ++counterRight;
                        return (TestClass)null;
                    });
            Assert.AreEqual(1, counterRight);
            CheckLeftEither(result2, "str");

            Assert.Throws<NullResultException>(() => Either.Right<string, int>(42).Map(
                (int r) =>
                {
                    ++counterRight;
                    return (TestClass)null;
                }));
            Assert.AreEqual(2, counterRight);

            result2 = eitherNone.Map(
                (int r) =>
                {
                    ++counterRight;
                    return (TestClass)null;
                });
            Assert.AreEqual(2, counterRight);
            CheckNoneEither(result2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.Map((Func<int, float>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.Map((Func<int, float>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.Map((Func<int, float>)null));
        }

        [Test]
        public void EitherBiMapToEitherRightOut()
        {
            int counterLeft = 0;
            int counterRight = 0;

            #region Local function

            void CheckCounters(int left, int right)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<string, float> result = eitherLeft.BiMap<string, int, float>(
                r =>
                {
                    ++counterRight;
                    return 42.5f;
                },
                l =>
                {
                    ++counterLeft;
                    return 43.5f;
                });
            CheckCounters(1, 0);
            CheckRightEither(result, 43.5f);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.BiMap<string, int, float>(
                r =>
                {
                    ++counterRight;
                    return 42.5f;
                },
                l =>
                {
                    ++counterLeft;
                    return 43.5f;
                });
            CheckCounters(1, 1);
            CheckRightEither(result, 42.5f);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.BiMap<string, int, float>(
                r =>
                {
                    ++counterRight;
                    return 42.5f;
                },
                l =>
                {
                    ++counterLeft;
                    return 43.5f;
                });
            CheckCounters(1, 1);
            CheckNoneEither(result);

            // Null return
            Assert.Throws<NullResultException>(
                () => Either.Left<string, int>("str").BiMap<string, int, TestClass>(
                    r =>
                    {
                        ++counterRight;
                        return null;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return null;
                    }));
            CheckCounters(2, 1);

            Assert.Throws<NullResultException>(
                () => Either.Right<string, int>(42).BiMap<string, int, TestClass>(
                    r =>
                    {
                        ++counterRight;
                        return null;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return null;
                    }));
            CheckCounters(2, 2);

            Either<string, TestClass> result2 = eitherNone.BiMap<string, int, TestClass>(
                r =>
                {
                    ++counterRight;
                    return null;
                },
                l =>
                {
                    ++counterLeft;
                    return null;
                });
            CheckCounters(2, 2);
            CheckNoneEither(result2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.BiMap(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.BiMap(null, l => 15.5f));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.BiMap<string, int, float>(null, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.BiMap(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.BiMap(null, l => 15.5f));
            Assert.Throws<ArgumentNullException>(() => eitherRight.BiMap<string, int, float>(null, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.BiMap(r => 12.5f, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.BiMap(null, l => 15.5f));
            Assert.Throws<ArgumentNullException>(() => eitherNone.BiMap<string, int, float>(null, null));
        }

        [Test]
        public void EitherBiMapToEither()
        {
            int counterLeft = 0;
            int counterRight = 0;
            var testObject = new TestClass();

            #region Local function

            void CheckCounters(int left, int right)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<TestClass, string> result = eitherLeft.BiMap(
                r =>
                {
                    ++counterRight;
                    return "success";
                },
                l =>
                {
                    ++counterLeft;
                    return testObject;
                });
            CheckCounters(1, 0);
            CheckLeftEither(result, testObject);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.BiMap(
                r =>
                {
                    ++counterRight;
                    return "success";
                },
                l =>
                {
                    ++counterLeft;
                    return testObject;
                });
            CheckCounters(1, 1);
            CheckRightEither(result, "success");

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.BiMap(
                r =>
                {
                    ++counterRight;
                    return "success";
                },
                l =>
                {
                    ++counterLeft;
                    return testObject;
                });
            CheckCounters(1, 1);
            CheckNoneEither(result);

            // Null return
            Assert.Throws<NullResultException>(
                () => Either.Left<string, int>("str").BiMap(
                    r =>
                    {
                        ++counterRight;
                        return (string)null;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return (TestClass)null;
                    }));
            CheckCounters(2, 1);

            Assert.Throws<NullResultException>(
                () => Either.Right<string, int>(42).BiMap(
                    r =>
                    {
                        ++counterRight;
                        return (string)null;
                    },
                    l =>
                    {
                        ++counterLeft;
                        return (TestClass)null;
                    }));
            CheckCounters(2, 2);

            Either<TestClass, string> result2 = eitherNone.BiMap(
                r =>
                {
                    ++counterRight;
                    return (string)null;
                },
                l =>
                {
                    ++counterLeft;
                    return (TestClass)null;
                });
            CheckCounters(2, 2);
            CheckNoneEither(result2);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.BiMap(r => "success", (Func<string, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.BiMap((Func<int, string>)null, l => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.BiMap((Func<int, string>)null, (Func<string, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.BiMap(r => "success", (Func<string, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.BiMap((Func<int, string>)null, l => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRight.BiMap((Func<int, string>)null, (Func<string, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.BiMap(r => "success", (Func<string, TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.BiMap((Func<int, string>)null, l => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherNone.BiMap((Func<int, string>)null, (Func<string, TestClass>)null));
        }

        #endregion

        #region IfLeft/IfFailure

        [Test]
        public void EitherIfLeftUnit()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            eitherLeft.IfLeft(
                l =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            eitherRight.IfLeft(
                l =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            eitherNone.IfLeft(
                l =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.IfLeft((Action<string>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.IfLeft((Action<string>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.IfLeft((Action<string>)null));
        }

        [Test]
        public void EitherIfFailure()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            eitherLeft.IfFailure(
                l =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            eitherRight.IfFailure(
                l =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            eitherNone.IfFailure(
                l =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.IfFailure(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.IfFailure(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.IfFailure(null));
        }

        [Test]
        public void EitherIfLeftToRight()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            int result = eitherLeft.IfLeft(22);
            Assert.AreEqual(22, result);

            result = eitherLeft.IfLeft(
                () =>
                {
                    ++counter;
                    return 32;
                });
            Assert.AreEqual(1, counter);
            Assert.AreEqual(32, result);

            result = eitherLeft.IfLeft(
                l =>
                {
                    ++counter;
                    return 62;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(62, result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.IfLeft(12);
            Assert.AreEqual(42, result);

            result = eitherRight.IfLeft(
                () =>
                {
                    ++counter;
                    return 12;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(42, result);

            result = eitherRight.IfLeft(
                l =>
                {
                    ++counter;
                    return 12;
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual(42, result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfLeft(55));

            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfLeft(
                    () =>
                    {
                        ++counter;
                        return 55;
                    }));
            Assert.AreEqual(2, counter);

            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfLeft(
                    l =>
                    {
                        ++counter;
                        return 55;
                    }));
            Assert.AreEqual(2, counter);


            var eitherLeftStringPerson = Either.Left<string, Person>("Error");
            var eitherRightStringPerson = Either.Right<string, Person>(new Person("Test"));
            var eitherNoneStringPerson = Either.Left<string, Person>("Error");

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft((Person)null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft((Person)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft((Person)null));

            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft((Func<Person>)null));
            Assert.Throws<NullResultException>(() => eitherLeftStringPerson.IfLeft(() => null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft((Func<Person>)null));
            Assert.AreEqual(new Person("Test"),  eitherRightStringPerson.IfLeft(() => null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft((Func<Person>)null));

            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft((Func<string, Person>)null));
            Assert.Throws<NullResultException>(() => eitherLeftStringPerson.IfLeft(s => null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft((Func<string, Person>)null));
            Assert.AreEqual(new Person("Test"), eitherRightStringPerson.IfLeft(s => null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft((Func<string, Person>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void EitherIfLeftToOut()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            float result = eitherLeft.IfLeft(
                l =>
                {
                    ++counter;
                    return 32.5f;
                },
                49.5f);
            Assert.AreEqual(1, counter);
            Assert.AreEqual(32.5f, result);

            result = eitherLeft.IfLeft(
                l =>
                {
                    ++counter;
                    return 62.5f;
                },
                () => 49.5f);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(62.5f, result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.IfLeft(
                l =>
                {
                    ++counter;
                    return 12.5f;
                },
                66.5f);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(66.5f, result);

            result = eitherRight.IfLeft(
                l =>
                {
                    ++counter;
                    return 12.5f;
                },
                () => 24.7f);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(24.7f, result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfLeft(
                    l =>
                    {
                        ++counter;
                        return 55.5f;
                    },
                    99.5f));
            Assert.AreEqual(2, counter);

            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfLeft(
                    l =>
                    {
                        ++counter;
                        return 55.5f;
                    },
                    () => 99.5f));
            Assert.AreEqual(2, counter);


            var eitherLeftStringPerson = Either.Left<string, Person>("Error");
            var eitherRightStringPerson = Either.Right<string, Person>(new Person("Test"));
            var eitherNoneStringPerson = Either.Left<string, Person>("Error");
            var testObject = new TestClass();

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft(r => testObject, (TestClass)null));
            Assert.Throws<NullResultException>(() => eitherLeftStringPerson.IfLeft(r => null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft(null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft(null, (TestClass)null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft(r => testObject, (TestClass)null));
            Assert.AreSame(testObject, eitherRightStringPerson.IfLeft(r => null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft(null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft(null, (TestClass)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft(r => testObject, (TestClass)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft(null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft(null, (TestClass)null));

            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft(r => testObject, (Func<TestClass>)null));
            Assert.Throws<NullResultException>(() => eitherLeftStringPerson.IfLeft(r => null, () => testObject));
            Assert.AreSame(testObject, eitherLeftStringPerson.IfLeft(l => testObject, () => null));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft(null, () => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfLeft((Func<string, TestClass>)null, (Func<TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft(r => testObject, (Func<TestClass>)null));
            Assert.AreSame(testObject, eitherRightStringPerson.IfLeft(r => null, () => testObject));
            Assert.Throws<NullResultException>(() => eitherRightStringPerson.IfLeft(r => testObject, () => null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft(null, () => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfLeft((Func<string, TestClass>)null, (Func<TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft(r => testObject, (Func<TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft(null, () => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfLeft((Func<string, TestClass>)null, (Func<TestClass>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion

        #region IfRight/IfSuccess

        [Test]
        public void EitherIfRightUnit()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            eitherLeft.IfRight(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(0, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            eitherRight.IfRight(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            eitherNone.IfRight(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.IfRight((Action<int>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.IfRight((Action<int>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.IfRight((Action<int>)null));
        }

        [Test]
        public void EitherIfSuccess()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            eitherLeft.IfSuccess(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(0, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            eitherRight.IfSuccess(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            eitherNone.IfSuccess(
                r =>
                {
                    ++counter;
                });
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.IfSuccess(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.IfSuccess(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.IfSuccess(null));
        }

        [Test]
        public void EitherIfRightToLeft()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            string result = eitherLeft.IfRight("Error 1");
            Assert.AreEqual("A string", result);

            result = eitherLeft.IfRight(
                () =>
                {
                    ++counter;
                    return "Error 2";
                });
            Assert.AreEqual(0, counter);
            Assert.AreEqual("A string", result);

            result = eitherLeft.IfRight(
                r =>
                {
                    ++counter;
                    return "Error 3";
                });
            Assert.AreEqual(0, counter);
            Assert.AreEqual("A string", result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.IfRight("Error 4");
            Assert.AreEqual("Error 4", result);

            result = eitherRight.IfRight(
                () =>
                {
                    ++counter;
                    return "Error 5";
                });
            Assert.AreEqual(1, counter);
            Assert.AreEqual("Error 5", result);

            result = eitherRight.IfRight(
                r =>
                {
                    ++counter;
                    return "Error 6";
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual("Error 6", result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfRight("Error 7"));

            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfRight(
                    () =>
                    {
                        ++counter;
                        return "Error 8";
                    }));
            Assert.AreEqual(2, counter);

            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfRight(
                    r =>
                    {
                        ++counter;
                        return "Error 9";
                    }));
            Assert.AreEqual(2, counter);

            var eitherLeftStringPerson = Either.Left<string, Person>("Error");
            var eitherRightStringPerson = Either.Right<string, Person>(new Person("Test"));
            var eitherNoneStringPerson = Either.Left<string, Person>("Error");

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight((string)null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight((string)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight((string)null));

            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight((Func<string>)null));
            Assert.AreEqual("Error", eitherLeftStringPerson.IfRight(() => null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight((Func<string>)null));
            Assert.Throws<NullResultException>(() => eitherRightStringPerson.IfRight(() => null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight((Func<string>)null));

            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight((Func<Person, string>)null));
            Assert.AreEqual("Error", eitherLeftStringPerson.IfRight(p => null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight((Func<Person, string>)null));
            Assert.Throws<NullResultException>(() => eitherRightStringPerson.IfRight(s => null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight((Func<Person, string>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void EitherIfRightToOut()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            float result = eitherLeft.IfRight(
                r =>
                {
                    ++counter;
                    return 32.5f;
                },
                49.5f);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(49.5f, result);

            result = eitherLeft.IfRight(
                r =>
                {
                    ++counter;
                    return 62.5f;
                },
                () => 51.5f);
            Assert.AreEqual(0, counter);
            Assert.AreEqual(51.5f, result);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.IfRight(
                r =>
                {
                    ++counter;
                    return 12.5f;
                },
                66.5f);
            Assert.AreEqual(1, counter);
            Assert.AreEqual(12.5f, result);

            result = eitherRight.IfRight(
                r =>
                {
                    ++counter;
                    return 13.5f;
                },
                () => 24.7f);
            Assert.AreEqual(2, counter);
            Assert.AreEqual(13.5f, result);

            // Either none
            var eitherNone = Either<string, int>.None;
            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfRight(
                    r =>
                    {
                        ++counter;
                        return 55.5f;
                    },
                    99.5f));
            Assert.AreEqual(2, counter);

            Assert.Throws<InvalidOperationException>(
                () => eitherNone.IfRight(
                    r =>
                    {
                        ++counter;
                        return 55.5f;
                    },
                    () => 99.5f));
            Assert.AreEqual(2, counter);


            var eitherLeftStringPerson = Either.Left<string, Person>("Error");
            var eitherRightStringPerson = Either.Right<string, Person>(new Person("Test"));
            var eitherNoneStringPerson = Either.Left<string, Person>("Error");
            var testObject = new TestClass();

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight(l => testObject, (TestClass)null));
            Assert.AreSame(testObject, eitherLeftStringPerson.IfRight(l => null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight(null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight(null, (TestClass)null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight(l => testObject, (TestClass)null));
            Assert.Throws<NullResultException>(() => eitherRightStringPerson.IfRight(l => null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight(null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight(null, (TestClass)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight(l => testObject, (TestClass)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight(null, testObject));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight(null, (TestClass)null));

            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight(l => testObject, (Func<TestClass>)null));
            Assert.AreSame(testObject, eitherLeftStringPerson.IfRight(l => null, () => testObject));
            Assert.Throws<NullResultException>(() => eitherLeftStringPerson.IfRight(l => testObject, () => null));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight(null, () => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherLeftStringPerson.IfRight((Func<Person, TestClass>)null, (Func<TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight(l => testObject, (Func<TestClass>)null));
            Assert.Throws<NullResultException>(() => eitherRightStringPerson.IfRight(l => null, () => testObject));
            Assert.AreSame(testObject, eitherRightStringPerson.IfRight(l => testObject, () => null));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight(null, () => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherRightStringPerson.IfRight((Func<Person, TestClass>)null, (Func<TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight(l => testObject, (Func<TestClass>)null));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight(null, () => testObject));
            Assert.Throws<ArgumentNullException>(() => eitherNoneStringPerson.IfRight((Func<Person, TestClass>)null, (Func<TestClass>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion

        #region Unwrapping

        [Test]
        public void LeftOrDefault()
        {
            // Either left
            Either<string, int> eitherLeft = Either.Left("Error");
            Assert.AreEqual("Error", eitherLeft.LeftOrDefault());

            // Either right
            Either<string, int> eitherRight = Either.Right(12);
            Assert.AreEqual(default(string), eitherRight.LeftOrDefault());

            // Either none
            Either<string, int> eitherNone = Either<string, int>.None;
            Assert.AreEqual(default(string), eitherNone.LeftOrDefault());
        }

        [Test]
        public void LeftOr()
        {
            // Either left
            Either<string, int> eitherLeft = Either.Left("Error");
            Assert.AreEqual("Error", eitherLeft.LeftOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.LeftOr((string)null));
            Assert.AreEqual("Error", eitherLeft.LeftOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.LeftOr((Func<string>)null));
            Assert.AreEqual("Error", eitherLeft.LeftOr(() => null));

            // Either right
            Either<string, int> eitherRight = Either.Right(12);
            Assert.AreEqual("Default", eitherRight.LeftOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.LeftOr((string)null));
            Assert.AreEqual("Default", eitherRight.LeftOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.LeftOr((Func<string>)null));
            Assert.Throws<NullResultException>(() => eitherRight.LeftOr(() => null));

            // Either none
            Either<string, int> eitherNone = Either<string, int>.None;
            Assert.AreEqual("Default", eitherNone.LeftOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.LeftOr((string)null));
            Assert.AreEqual("Default", eitherNone.LeftOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.LeftOr((Func<string>)null));
            Assert.Throws<NullResultException>(() => eitherNone.LeftOr(() => null));
        }

        [Test]
        public void RightOrDefault()
        {
            // Either left
            Either<string, int> eitherLeft = Either.Left("Error");
            Assert.AreEqual(default(int), eitherLeft.RightOrDefault());

            // Either right
            Either<string, int> eitherRight = Either.Right(12);
            Assert.AreEqual(12, eitherRight.RightOrDefault());

            // Either none
            Either<string, int> eitherNone = Either<string, int>.None;
            Assert.AreEqual(default(int), eitherNone.RightOrDefault());
        }

        [Test]
        public void RightOr()
        {
            // Either left
            Either<int, string> eitherLeft = Either.Left(12);
            Assert.AreEqual("Default", eitherLeft.RightOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.RightOr((string)null));
            Assert.AreEqual("Default", eitherLeft.RightOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.RightOr((Func<string>)null));
            Assert.Throws<NullResultException>(() => eitherLeft.RightOr(() => null));

            // Either right
            Either<int, string> eitherRight = Either.Right("Value");
            Assert.AreEqual("Value", eitherRight.RightOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.RightOr((string)null));
            Assert.AreEqual("Value", eitherRight.RightOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.RightOr((Func<string>)null));
            Assert.AreEqual("Value", eitherRight.RightOr(() => null));

            // Either none
            Either<int, string> eitherNone = Either<int, string>.None;
            Assert.AreEqual("Default", eitherNone.RightOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.RightOr((string)null));
            Assert.AreEqual("Default", eitherNone.RightOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.RightOr((Func<string>)null));
            Assert.Throws<NullResultException>(() => eitherNone.RightOr(() => null));
        }

        #endregion
    }
}