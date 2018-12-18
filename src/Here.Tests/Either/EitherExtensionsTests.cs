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

            // Either right
            Either<string, int> eitherRight = Either.Right(12);
            Assert.AreEqual("Default", eitherRight.LeftOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.LeftOr((string)null));
            Assert.AreEqual("Default", eitherRight.LeftOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.LeftOr((Func<string>)null));

            // Either none
            Either<string, int> eitherNone = Either<string, int>.None;
            Assert.AreEqual("Default", eitherNone.LeftOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.LeftOr((string)null));
            Assert.AreEqual("Default", eitherNone.LeftOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.LeftOr((Func<string>)null));
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

            // Either right
            Either<int, string> eitherRight = Either.Right("Error");
            Assert.AreEqual("Error", eitherRight.RightOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.RightOr((string)null));
            Assert.AreEqual("Error", eitherRight.RightOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherRight.RightOr((Func<string>)null));

            // Either none
            Either<int, string> eitherNone = Either<int, string>.None;
            Assert.AreEqual("Default", eitherNone.RightOr("Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.RightOr((string)null));
            Assert.AreEqual("Default", eitherNone.RightOr(() => "Default"));
            Assert.Throws<ArgumentNullException>(() => eitherNone.RightOr((Func<string>)null));
        }

        #endregion
    }
}