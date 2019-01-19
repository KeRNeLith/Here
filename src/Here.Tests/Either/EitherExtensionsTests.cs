using System;
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
                () =>
                {
                    var _ = Either.Left<string, int>("str").Map(
                        (string l) =>
                        {
                            ++counterLeft;
                            return (TestClass) null;
                        });
                });
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Map((Func<string, float>) null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Map((Func<string, float>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Map((Func<string, float>)null); });
            // ReSharper restore AssignNullToNotNullAttribute
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

            Assert.Throws<NullResultException>(
                () =>
                {
                    var _ = Either.Right<string, int>(42).Map(
                        (int r) =>
                        {
                            ++counterRight;
                            return (TestClass) null;
                        });
                });
            Assert.AreEqual(2, counterRight);

            result2 = eitherNone.Map(
                (int r) =>
                {
                    ++counterRight;
                    return (TestClass)null;
                });
            Assert.AreEqual(2, counterRight);
            CheckNoneEither(result2);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Map((Func<int, float>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Map((Func<int, float>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Map((Func<int, float>)null); });
            // ReSharper restore AssignNullToNotNullAttribute
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
            Either<string, float> result = eitherLeft.BiMap<float>(
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
            result = eitherRight.BiMap<float>(
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
            result = eitherNone.BiMap<float>(
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
                () =>
                {
                    var _ = Either.Left<string, int>("str").BiMap<TestClass>(
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
                });
            CheckCounters(2, 1);

            Assert.Throws<NullResultException>(
                () =>
                {
                    var _ = Either.Right<string, int>(42).BiMap<TestClass>(
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
                });
            CheckCounters(2, 2);

            Either<string, TestClass> result2 = eitherNone.BiMap<TestClass>(
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiMap(r => 12.5f, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiMap(null, l => 15.5f); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiMap<float>(null, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiMap(r => 12.5f, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiMap(null, l => 15.5f); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiMap<float>(null, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiMap(r => 12.5f, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiMap(null, l => 15.5f); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiMap<float>(null, null); });
            // ReSharper restore AssignNullToNotNullAttribute
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
                () =>
                {
                    var _ = Either.Left<string, int>("str").BiMap(
                        r =>
                        {
                            ++counterRight;
                            return (string) null;
                        },
                        l =>
                        {
                            ++counterLeft;
                            return (TestClass) null;
                        });
                });
            CheckCounters(2, 1);

            Assert.Throws<NullResultException>(
                () =>
                {
                    var _ = Either.Right<string, int>(42).BiMap(
                        r =>
                        {
                            ++counterRight;
                            return (string) null;
                        },
                        l =>
                        {
                            ++counterLeft;
                            return (TestClass) null;
                        });
                });
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiMap(r => "success", (Func<string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiMap((Func<int, string>)null, l => testObject); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiMap((Func<int, string>)null, (Func<string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiMap(r => "success", (Func<string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiMap((Func<int, string>)null, l => testObject); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiMap((Func<int, string>)null, (Func<string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiMap(r => "success", (Func<string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiMap((Func<int, string>)null, l => testObject); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiMap((Func<int, string>)null, (Func<string, TestClass>)null); });
            // ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion

        #region Bind/BiBind

        [Test]
        public void EitherBindLeft()
        {
            int counterLeft = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<float, int> result = eitherLeft.Bind(
                l =>
                {
                    ++counterLeft;
                    return Either.Left<float, int>(12.5f);
                });
            Assert.AreEqual(1, counterLeft);
            CheckLeftEither(result, 12.5f);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Bind(
                l =>
                {
                    ++counterLeft;
                    return Either.Left<float, int>(12.5f);
                });
            Assert.AreEqual(1, counterLeft);
            CheckRightEither(result, 42);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.Bind(
                l =>
                {
                    ++counterLeft;
                    return Either.Left<float, int>(12.5f);
                });
            Assert.AreEqual(1, counterLeft);
            CheckNoneEither(result);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Bind((Func<string, Either<float, int>>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Bind((Func<string, Either<float, int>>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Bind((Func<string, Either<float, int>>)null); });
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void EitherBindRight()
        {
            int counterRight = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            Either<string, float> result = eitherLeft.Bind(
                r =>
                {
                    ++counterRight;
                    return Either.Right<string, float>(42.5f);
                });
            Assert.AreEqual(0, counterRight);
            CheckLeftEither(result, "A string");

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.Bind(
                r =>
                {
                    ++counterRight;
                    return Either.Right<string, float>(47.5f);
                });
            Assert.AreEqual(1, counterRight);
            CheckRightEither(result, 47.5f);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.Bind(
                r =>
                {
                    ++counterRight;
                    return Either.Right<string, float>(42.5f);
                });
            Assert.AreEqual(1, counterRight);
            CheckNoneEither(result);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Map((Func<int, Either<string, float>>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Map((Func<int, Either<string, float>>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Map((Func<int, Either<string, float>>)null); });
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void EitherBiBind()
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
            Either<string, float> result = eitherLeft.BiBind(
                r =>
                {
                    ++counterRight;
                    return Either.Right<string, float>(45.5f);
                },
                l =>
                {
                    ++counterLeft;
                    return Either.Right<string, float>(47.5f);
                });
            CheckCounters(1, 0);
            CheckRightEither(result, 47.5f);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            result = eitherRight.BiBind(
                r =>
                {
                    ++counterRight;
                    return Either.Right<string, float>(42.5f);
                },
                l =>
                {
                    ++counterLeft;
                    return Either.Left<string, float>("Error");
                });
            CheckCounters(1, 1);
            CheckRightEither(result, 42.5f);

            // Either none
            var eitherNone = Either<string, int>.None;
            result = eitherNone.BiBind(
                r =>
                {
                    ++counterRight;
                    return Either.Left<string, float>("Error");
                },
                l =>
                {
                    ++counterLeft;
                    return Either.Right<string, float>(42.5f);
                });
            CheckCounters(1, 1);
            CheckNoneEither(result);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiBind(r => Either.Right<string, float>(12.5f), null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiBind(null, l => Either.Right<string, float>(12.5f)); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiBind<float>(null, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiBind(r => Either.Right<string, float>(12.5f), null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiBind(null, l => Either.Right<string, float>(12.5f)); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiBind<float>(null, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiBind(r => Either.Right<string, float>(12.5f), null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiBind(null, l => Either.Right<string, float>(12.5f)); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiBind<float>(null, null); });
            // ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion

        #region Fold/BiFold

        [Test]
        public void EitherFoldLeft()
        {
            var testObject = new TestClass();

            #region Local function

            void CheckFold(Either<string, int> either, bool expectLeft, float expectedResult = 1.0f)
            {
                int counter = 0;
                float res = either.Fold(
                    1.0f,
                    (seed, l) =>
                    {
                        ++counter;
                        return seed + l.Length;
                    });
                Assert.AreEqual(expectLeft ? 1 : 0, counter);
                Assert.AreEqual(expectLeft ? expectedResult : 1.0f, res);
            }

            void CheckFoldNullReturn(Either<string, int> either, bool expectThrow)
            {
                int counter = 0;
                if (expectThrow)
                {
                    Assert.Throws<NullResultException>(
                        () =>
                        {
                            var _ = either.Fold(
                                testObject,
                                (TestClass seed, string l) =>
                                {
                                    ++counter;
                                    return null;
                                });
                        });
                    Assert.AreEqual(1, counter);
                }
                else
                {
                    TestClass result = either.Fold(
                        testObject,
                        (TestClass seed, string l) =>
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
            CheckFold(eitherLeft, true, 9.0f);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckFold(eitherRight, false);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckFold(eitherNone, false);

            // Null return
            CheckFoldNullReturn(eitherLeft, true);
            CheckFoldNullReturn(eitherRight, false);
            CheckFoldNullReturn(eitherNone, false);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Fold(null, (TestClass seed, string l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Fold(null, (TestClass seed, string l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Fold(null, (TestClass seed, string l) => seed); });

            // Cannot test this in NET20 and NET30 due to NUnit package
#if !NET20 && !NET30
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Fold(testObject, (Func<TestClass, string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Fold(null, (Func<TestClass, string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Fold(testObject, (Func<TestClass, string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Fold(null, (Func<TestClass, string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Fold(testObject, (Func<TestClass, string, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Fold(null, (Func<TestClass, string, TestClass>)null); });
#endif
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void EitherFoldRight()
        {
            var testObject = new TestClass();

            #region Local function

            void CheckFold(Either<string, int> either, bool expectRight, float expectedResult = 1.0f)
            {
                int counter = 0;
                float res = either.Fold(
                    1.0f,
                    (seed, r) =>
                    {
                        ++counter;
                        return seed + r;
                    });
                Assert.AreEqual(expectRight ? 1 : 0, counter);
                Assert.AreEqual(expectRight ? expectedResult : 1.0f, res);
            }

            void CheckFoldNullReturn(Either<string, int> either, bool expectThrow)
            {
                int counter = 0;
                if (expectThrow)
                {
                    Assert.Throws<NullResultException>(
                        () =>
                        {
                            var _ = either.Fold(
                                testObject,
                                (TestClass seed, int r) =>
                                {
                                    ++counter;
                                    return null;
                                });
                        });
                    Assert.AreEqual(1, counter);
                }
                else
                {
                    TestClass result = either.Fold(
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
            CheckFold(eitherLeft, false);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckFold(eitherRight, true, 43.0f);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckFold(eitherNone, false);

            // Null return
            CheckFoldNullReturn(eitherLeft, false);
            CheckFoldNullReturn(eitherRight, true);
            CheckFoldNullReturn(eitherNone, false);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Fold(null, (TestClass seed, int r) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Fold(null, (TestClass seed, int r) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Fold(null, (TestClass seed, int r) => seed); });

            // Cannot test this in NET20 and NET30 due to NUnit package
#if !NET20 && !NET30
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Fold(testObject, (Func<TestClass, int, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.Fold(null, (Func<TestClass, int, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Fold(testObject, (Func<TestClass, int, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.Fold(null, (Func<TestClass, int, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Fold(testObject, (Func<TestClass, int, TestClass>)null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.Fold(null, (Func<TestClass, int, TestClass>)null); });
#endif
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void EitherBiFold()
        {
            var testObject = new TestClass();

            #region Local function

            void CheckBiFold(Either<string, int> either, bool expectRight, bool expectLeft, float expectedResult = 1.0f)
            {
                int counterLeft = 0;
                int counterRight = 0;
                float res = either.BiFold(
                    1.0f, 
                    (seed, r) =>
                    {
                        ++counterRight;
                        return seed + r;
                    },
                    (seed, l) =>
                    {
                        ++counterLeft;
                        return seed + l.Length;
                    });
                Assert.AreEqual(expectLeft && !expectRight ? 1 : 0, counterLeft);
                Assert.AreEqual(expectRight && !expectLeft ? 1 : 0, counterRight);
                Assert.AreEqual(expectLeft || expectRight ? expectedResult : 1.0f, res);
            }

            void CheckBiFoldNullReturn(Either<string, int> either, bool expectThrow, bool expectRight)
            {
                int counterLeft = 0;
                int counterRight = 0;

                if (expectThrow)
                {
                    Assert.Throws<NullResultException>(
                        () =>
                        {
                            var _ = either.BiFold(
                                testObject,
                                (seed, r) =>
                                {
                                    ++counterRight;
                                    return null;
                                },
                                (seed, l) =>
                                {
                                    ++counterLeft;
                                    return null;
                                });
                        });
                    Assert.AreEqual(expectRight ? 1 : 0, counterRight);
                    Assert.AreEqual(!expectRight ? 1 : 0, counterLeft);
                }
                else
                {
                    TestClass result = either.BiFold(
                        testObject,
                        (seed, r) =>
                        {
                            ++counterRight;
                            return null;
                        },
                        (seed, l) =>
                        {
                            ++counterLeft;
                            return null;
                        });
                    Assert.AreEqual(0, counterRight);
                    Assert.AreEqual(0, counterLeft);
                    Assert.AreSame(testObject, result);
                }
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckBiFold(eitherLeft, false, true, 9.0f);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckBiFold(eitherRight, true, false, 43.0f);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckBiFold(eitherNone, false, false);

            // Null return
            CheckBiFoldNullReturn(eitherLeft, true, false);
            CheckBiFoldNullReturn(eitherRight, true, true);
            CheckBiFoldNullReturn(eitherNone, false, false);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiFold(null, (TestClass seed, int r) => seed, (seed, l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiFold(testObject, null, (seed, l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiFold(testObject, (seed, r) => seed, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiFold(null, null, (TestClass seed, string l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiFold(null, (TestClass seed, int r) => seed, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherLeft.BiFold((TestClass)null, null, null); });

            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiFold(null, (TestClass seed, int r) => seed, (seed, l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiFold(testObject, null, (seed, l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiFold(testObject, (seed, r) => seed, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiFold(null, null, (TestClass seed, string l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiFold(null, (TestClass seed, int r) => seed, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherRight.BiFold((TestClass)null, null, null); });

            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiFold(null, (TestClass seed, int r) => seed, (seed, l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiFold(testObject, null, (seed, l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiFold(testObject, (seed, r) => seed, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiFold(null, null, (TestClass seed, string l) => seed); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiFold(null, (TestClass seed, int r) => seed, null); });
            Assert.Throws<ArgumentNullException>(() => { var _ = eitherNone.BiFold((TestClass)null, null, null); });
            // ReSharper restore AssignNullToNotNullAttribute
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

        #region OnLeft/OnFailure

        [Test]
        public void EitherOnLeft()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckLeftEither(
                eitherLeft.OnLeft(
                    l =>
                    {
                        ++counter;
                    }),
                "A string");
            Assert.AreEqual(1, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckRightEither(
                eitherRight.OnLeft(
                    l =>
                    {
                        ++counter;
                    }),
                42);
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckNoneEither(
                eitherNone.OnLeft(
                    l =>
                    {
                        ++counter;
                    }));
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnLeft(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnLeft(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnLeft(null));
        }

        [Test]
        public void EitherOnFailure()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckLeftEither(
                eitherLeft.OnFailure(
                    l =>
                    {
                        ++counter;
                    }),
                "A string");
            Assert.AreEqual(1, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckRightEither(
                eitherRight.OnFailure(
                    l =>
                    {
                        ++counter;
                    }),
                42);
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckNoneEither(
                eitherNone.OnFailure(
                    l =>
                    {
                        ++counter;
                    }));
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnFailure(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnFailure(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnFailure(null));
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

        #region OnRight/OnSuccess

        [Test]
        public void EitherOnRight()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckLeftEither(
                eitherLeft.OnRight(
                    r =>
                    {
                        ++counter;
                    }),
                "A string");
            Assert.AreEqual(0, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckRightEither(
                eitherRight.OnRight(
                    r =>
                    {
                        ++counter;
                    }),
                42);
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckNoneEither(
                eitherNone.OnRight(
                    r =>
                    {
                        ++counter;
                    }));
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnRight(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnRight(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnRight(null));
        }

        [Test]
        public void EitherOnSuccess()
        {
            int counter = 0;

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckLeftEither(
                eitherLeft.OnSuccess(
                    r =>
                    {
                        ++counter;
                    }),
                "A string");
            Assert.AreEqual(0, counter);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckRightEither(
                eitherRight.OnSuccess(
                    r =>
                    {
                        ++counter;
                    }),
                42);
            Assert.AreEqual(1, counter);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckNoneEither(
                eitherNone.OnSuccess(
                    r =>
                    {
                        ++counter;
                    }));
            Assert.AreEqual(1, counter);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnRight(null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnRight(null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnRight(null));
        }

        #endregion

        #region OnRightOrLeft/OnSuccessOrFailure

        [Test]
        public void EitherOnRightOrLeft()
        {
            int counterRight = 0;
            int counterLeft = 0;

            #region Local function

            void CheckCounters(int left, int right)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckLeftEither(
                eitherLeft.OnRightOrLeft(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }),
                "A string");
            CheckCounters(1, 0);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckRightEither(
                eitherRight.OnRightOrLeft(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }),
                42);
            CheckCounters(1, 1);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckNoneEither(
                eitherNone.OnRightOrLeft(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }));
            CheckCounters(1, 1);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnRightOrLeft(r => ++counterRight, null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnRightOrLeft(null, l => ++counterLeft));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnRightOrLeft(null, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnRightOrLeft(r => ++counterRight, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnRightOrLeft(null, l => ++counterLeft));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnRightOrLeft(null, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnRightOrLeft(r => ++counterRight, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnRightOrLeft(null, l => ++counterLeft));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnRightOrLeft(null, null));
        }

        [Test]
        public void EitherOnSuccessOrFailure()
        {
            int counterRight = 0;
            int counterLeft = 0;

            #region Local function

            void CheckCounters(int left, int right)
            {
                Assert.AreEqual(left, counterLeft);
                Assert.AreEqual(right, counterRight);
            }

            #endregion

            // Either left
            var eitherLeft = Either.Left<string, int>("A string");
            CheckLeftEither(
                eitherLeft.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }),
                "A string");
            CheckCounters(1, 0);

            // Either right
            var eitherRight = Either.Right<string, int>(42);
            CheckRightEither(
                eitherRight.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }),
                42);
            CheckCounters(1, 1);

            // Either none
            var eitherNone = Either<string, int>.None;
            CheckNoneEither(
                eitherNone.OnSuccessOrFailure(
                    r =>
                    {
                        ++counterRight;
                    },
                    l =>
                    {
                        ++counterLeft;
                    }));
            CheckCounters(1, 1);

            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnSuccessOrFailure(r => ++counterRight, null));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnSuccessOrFailure(null, l => ++counterLeft));
            Assert.Throws<ArgumentNullException>(() => eitherLeft.OnSuccessOrFailure(null, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnSuccessOrFailure(r => ++counterRight, null));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnSuccessOrFailure(null, l => ++counterLeft));
            Assert.Throws<ArgumentNullException>(() => eitherRight.OnSuccessOrFailure(null, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnSuccessOrFailure(r => ++counterRight, null));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnSuccessOrFailure(null, l => ++counterLeft));
            Assert.Throws<ArgumentNullException>(() => eitherNone.OnSuccessOrFailure(null, null));
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