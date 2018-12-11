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
        }
    }
}