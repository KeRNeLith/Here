using System;
using NUnit.Framework;

namespace Here.Tests.Eithers
{
    /// <summary>
    /// Tests for <see cref="Either{TLeft,TRight}"/>.
    /// </summary>
    [TestFixture]
    internal class EitherTests : EitherTestsBase
    {
        #region Either

        [Test]
        public void EitherConstructionLeft()
        {
            Either<string, int> eitherLeft1 = Either.Left<string, int>("Error message");
            CheckLeftEither(eitherLeft1, "Error message");

            Either<double, int> eitherLeft2 = Either.Left<double, int>((double?)2.0);
            CheckLeftEither(eitherLeft2, 2.0);

            Assert.Throws<ArgumentNullException>(() => Either.Left<double, int>(null));

            Either<double, int> eitherLeft3 = Either.Left(6.0);
            CheckLeftEither(eitherLeft3, 6.0);

            Either<double, int> eitherLeft4 = Either.Left((double?)12.0);
            CheckLeftEither(eitherLeft4, 12.0);

            Assert.Throws<ArgumentNullException>(() => { Either<double, int> _ = Either.Left((double?)null); });

            Either<double, int> eitherLeft5 = 18.0.ToEither<double, int>();
            CheckLeftEither(eitherLeft5, 18.0);

            Either<double, int> eitherLeft6 = ((double?)22.0).ToEither<double, int>();
            CheckLeftEither(eitherLeft6, 22.0);

            Assert.Throws<ArgumentNullException>(() => { Either<Person, int> _ = Either.Left((Person)null); });
            Assert.Throws<ArgumentNullException>(() => { Either<Person, int> _ = Either.Left<Person, int>(null); });
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { Either<double, int> _ = ((double?)null).ToEither<double, int>(); });

            Either<int, int> eitherLeft7 = Either.Left(1);
            CheckLeftEither(eitherLeft7, 1);

            Either<int, int> eitherLeft8 = Either.Left((int?)2);
            CheckLeftEither(eitherLeft8, 2);

            Assert.Throws<ArgumentNullException>(() => { Either<int, int> _ = Either.Left((int?)null); });

            Either<int, int> eitherLeft9 = Either.Left<int, int>(3);
            CheckLeftEither(eitherLeft9, 3);

            Either<int, int> eitherLeft10 = Either.Left<int, int>((int?)4);
            CheckLeftEither(eitherLeft10, 4);

            Assert.Throws<ArgumentNullException>(() => { Either<int, int> _ = Either.Left<int, int>(null); });
        }

        [Test]
        public void EitherConstructionRight()
        {
            Either<string, int> eitherRight1 = Either.Right<string, int>(5);
            CheckRightEither(eitherRight1, 5);

            Either<double, int> eitherRight2 = Either.Right<double, int>((int?)6);
            CheckRightEither(eitherRight2, 6);

            Assert.Throws<ArgumentNullException>(() => Either.Right<double, int>(null));

            Either<double, int> eitherRight3 = Either.Right(7);
            CheckRightEither(eitherRight3, 7);

            Either<double, int> eitherRight4 = Either.Right((int?)8);
            CheckRightEither(eitherRight4, 8);

            Assert.Throws<ArgumentNullException>(() => { Either<double, int> _ = Either.Right((int?)null); });

            Either<double, int> eitherRight5 = 18.ToEither<double, int>();
            CheckRightEither(eitherRight5, 18);

            Either<double, int> eitherRight6 = ((int?)22).ToEither<double, int>();
            CheckRightEither(eitherRight6, 22);

            Assert.Throws<ArgumentNullException>(() => { Either<double, Person> _ = Either.Right((Person)null); });
            Assert.Throws<ArgumentNullException>(() => { Either<double, Person> _ = Either.Right<double, Person>(null); });
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => { Either<double, int> _ = ((double?)null).ToEither<double, int>(); });

            Either<int, int> eitherRight7 = Either.Right(1);
            CheckRightEither(eitherRight7, 1);

            Either<int, int> eitherRight8 = Either.Right((int?)2);
            CheckRightEither(eitherRight8, 2);

            Assert.Throws<ArgumentNullException>(() => { Either<int, int> _ = Either.Right((int?)null); });

            Either<int, int> eitherRight9 = Either.Right<int, int>(3);
            CheckRightEither(eitherRight9, 3);

            Either<int, int> eitherRight10 = Either.Right<int, int>((int?)4);
            CheckRightEither(eitherRight10, 4);

            Assert.Throws<ArgumentNullException>(() => { Either<int, int> _ = Either.Right<int, int>(null); });
        }

        [Test]
        public void EitherConstructionNone()
        {
            Either<string, int> eitherNone1 = new Either<string, int>();
            CheckNoneEither(eitherNone1);

            Either<string, int> eitherNone2 = Either<string, int>.None;
            CheckNoneEither(eitherNone2);
        }

        [Test]
        public void EitherToValue()
        {
            Either<string, int> eitherLeft = Either.Left<string, int>("Error message");
            Assert.AreEqual("Error message", (string)eitherLeft);

            Assert.Throws<InvalidCastException>(() => { var _ = (int)Either.Left<string, int>("Error message"); });

            Either<string, int> eitherRight = Either.Right<string, int>(12);
            Assert.AreEqual(12, (int)eitherRight);

            Assert.Throws<InvalidCastException>(() => { var _ = (string)Either.Right<string, int>(25); });

            Assert.Throws<InvalidCastException>(() => { var _ = (string)Either<string, int>.None; });
            Assert.Throws<InvalidCastException>(() => { var _ = (int)Either<string, int>.None; });
        }

        [Test]
        public void EitherEqualityLeft()
        {
            // Either value type
            var eitherLeftIntDouble1 = Either.Left<int, double>(12);
            var eitherLeftIntDouble2 = Either.Left<int, double>(12);
            var eitherLeftIntDouble3 = Either.Left<int, double>(42);
            var eitherNoneIntDouble1 = Either<int, double>.None;
            var eitherNoneIntDouble2 = new Either<int, double>();

            Assert.IsTrue(eitherLeftIntDouble1.Equals(eitherLeftIntDouble2));
            Assert.IsTrue(eitherLeftIntDouble2.Equals(eitherLeftIntDouble1));
            Assert.IsTrue(eitherLeftIntDouble1 == eitherLeftIntDouble2);
            Assert.IsTrue(eitherLeftIntDouble2 == eitherLeftIntDouble1);
            Assert.IsFalse(eitherLeftIntDouble1 != eitherLeftIntDouble2);
            Assert.IsFalse(eitherLeftIntDouble2 != eitherLeftIntDouble1);

            Assert.IsFalse(eitherLeftIntDouble1.Equals(eitherLeftIntDouble3));
            Assert.IsFalse(eitherLeftIntDouble3.Equals(eitherLeftIntDouble1));
            Assert.IsFalse(eitherLeftIntDouble1 == eitherLeftIntDouble3);
            Assert.IsFalse(eitherLeftIntDouble3 == eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftIntDouble1 != eitherLeftIntDouble3);
            Assert.IsTrue(eitherLeftIntDouble3 != eitherLeftIntDouble1);

            Assert.IsFalse(eitherLeftIntDouble1.Equals(eitherNoneIntDouble1));
            Assert.IsFalse(eitherNoneIntDouble1.Equals(eitherLeftIntDouble1));
            Assert.IsFalse(eitherLeftIntDouble1 == eitherNoneIntDouble1);
            Assert.IsFalse(eitherNoneIntDouble1 == eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftIntDouble1 != eitherNoneIntDouble1);
            Assert.IsTrue(eitherNoneIntDouble1 != eitherLeftIntDouble1);

            Assert.IsTrue(eitherNoneIntDouble1.Equals(eitherNoneIntDouble2));
            Assert.IsTrue(eitherNoneIntDouble2.Equals(eitherNoneIntDouble1));
            Assert.IsTrue(eitherNoneIntDouble1 == eitherNoneIntDouble2);
            Assert.IsTrue(eitherNoneIntDouble2 == eitherNoneIntDouble1);
            Assert.IsFalse(eitherNoneIntDouble1 != eitherNoneIntDouble2);
            Assert.IsFalse(eitherNoneIntDouble2 != eitherNoneIntDouble1);

            // Either reference type
            var person1 = new Person("Test");
            var eitherLeftPersonInt1 = Either.Left<Person, int>(person1);
            var eitherLeftPersonInt2 = Either.Left<Person, int>(person1);
            var eitherLeftPersonInt3 = Either.Left<Person, int>(new Person("Test"));
            var eitherLeftPersonInt4 = Either.Left<Person, int>(new Person("Test2"));
            var eitherNonePersonInt1 = Either<Person, int>.None;
            var eitherNonePersonInt2 = new Either<Person, int>();

            Assert.IsTrue(eitherLeftPersonInt1.Equals(eitherLeftPersonInt2));
            Assert.IsTrue(eitherLeftPersonInt2.Equals(eitherLeftPersonInt1));
            Assert.IsTrue(eitherLeftPersonInt1 == eitherLeftPersonInt2);
            Assert.IsTrue(eitherLeftPersonInt2 == eitherLeftPersonInt1);
            Assert.IsFalse(eitherLeftPersonInt1 != eitherLeftPersonInt2);
            Assert.IsFalse(eitherLeftPersonInt2 != eitherLeftPersonInt1);

            Assert.IsTrue(eitherLeftPersonInt1.Equals(eitherLeftPersonInt3));
            Assert.IsTrue(eitherLeftPersonInt3.Equals(eitherLeftPersonInt1));
            Assert.IsTrue(eitherLeftPersonInt1 == eitherLeftPersonInt3);
            Assert.IsTrue(eitherLeftPersonInt3 == eitherLeftPersonInt1);
            Assert.IsFalse(eitherLeftPersonInt1 != eitherLeftPersonInt3);
            Assert.IsFalse(eitherLeftPersonInt3 != eitherLeftPersonInt1);

            Assert.IsFalse(eitherLeftPersonInt1.Equals(eitherLeftPersonInt4));
            Assert.IsFalse(eitherLeftPersonInt4.Equals(eitherLeftPersonInt1));
            Assert.IsFalse(eitherLeftPersonInt1 == eitherLeftPersonInt4);
            Assert.IsFalse(eitherLeftPersonInt4 == eitherLeftPersonInt1);
            Assert.IsTrue(eitherLeftPersonInt1 != eitherLeftPersonInt4);
            Assert.IsTrue(eitherLeftPersonInt4 != eitherLeftPersonInt1);

            Assert.IsFalse(eitherLeftPersonInt1.Equals(eitherNonePersonInt1));
            Assert.IsFalse(eitherNonePersonInt1.Equals(eitherLeftPersonInt1));
            Assert.IsFalse(eitherLeftPersonInt1 == eitherNonePersonInt1);
            Assert.IsFalse(eitherNonePersonInt1 == eitherLeftPersonInt1);
            Assert.IsTrue(eitherLeftPersonInt1 != eitherNonePersonInt1);
            Assert.IsTrue(eitherNonePersonInt1 != eitherLeftPersonInt1);

            Assert.IsTrue(eitherNonePersonInt1.Equals(eitherNonePersonInt2));
            Assert.IsTrue(eitherNonePersonInt2.Equals(eitherNonePersonInt1));
            Assert.IsTrue(eitherNonePersonInt1 == eitherNonePersonInt2);
            Assert.IsTrue(eitherNonePersonInt2 == eitherNonePersonInt1);
            Assert.IsFalse(eitherNonePersonInt1 != eitherNonePersonInt2);
            Assert.IsFalse(eitherNonePersonInt2 != eitherNonePersonInt1);

            var person2 = new PersonNotEquatable("Test");
            var eitherLeftPersonInt5 = Either.Left<PersonNotEquatable, int>(person2);
            var eitherLeftPersonInt6 = Either.Left<PersonNotEquatable, int>(person2);
            var eitherLeftPersonInt7 = Either.Left<PersonNotEquatable, int>(new PersonNotEquatable("Test"));
            var eitherNonePersonInt3 = Either<PersonNotEquatable, int>.None;
            var eitherNonePersonInt4 = new Either<PersonNotEquatable, int>();

            Assert.IsTrue(eitherLeftPersonInt5.Equals(eitherLeftPersonInt6));
            Assert.IsTrue(eitherLeftPersonInt6.Equals(eitherLeftPersonInt5));
            Assert.IsTrue(eitherLeftPersonInt5 == eitherLeftPersonInt6);
            Assert.IsTrue(eitherLeftPersonInt6 == eitherLeftPersonInt5);
            Assert.IsFalse(eitherLeftPersonInt5 != eitherLeftPersonInt6);
            Assert.IsFalse(eitherLeftPersonInt6 != eitherLeftPersonInt5);

            Assert.IsFalse(eitherLeftPersonInt5.Equals(eitherLeftPersonInt7));
            Assert.IsFalse(eitherLeftPersonInt7.Equals(eitherLeftPersonInt5));
            Assert.IsFalse(eitherLeftPersonInt5 == eitherLeftPersonInt7);
            Assert.IsFalse(eitherLeftPersonInt7 == eitherLeftPersonInt5);
            Assert.IsTrue(eitherLeftPersonInt5 != eitherLeftPersonInt7);
            Assert.IsTrue(eitherLeftPersonInt7 != eitherLeftPersonInt5);

            Assert.IsFalse(eitherLeftPersonInt5.Equals(eitherNonePersonInt3));
            Assert.IsFalse(eitherNonePersonInt3.Equals(eitherLeftPersonInt5));
            Assert.IsFalse(eitherLeftPersonInt5 == eitherNonePersonInt3);
            Assert.IsFalse(eitherNonePersonInt3 == eitherLeftPersonInt5);
            Assert.IsTrue(eitherLeftPersonInt5 != eitherNonePersonInt3);
            Assert.IsTrue(eitherNonePersonInt3 != eitherLeftPersonInt5);

            Assert.IsTrue(eitherNonePersonInt3.Equals(eitherNonePersonInt4));
            Assert.IsTrue(eitherNonePersonInt4.Equals(eitherNonePersonInt3));
            Assert.IsTrue(eitherNonePersonInt3 == eitherNonePersonInt4);
            Assert.IsTrue(eitherNonePersonInt4 == eitherNonePersonInt3);
            Assert.IsFalse(eitherNonePersonInt3 != eitherNonePersonInt4);
            Assert.IsFalse(eitherNonePersonInt4 != eitherNonePersonInt3);

            // Mixed
            Assert.IsTrue(eitherLeftIntDouble1.Equals(12));
            Assert.IsFalse(eitherLeftIntDouble1.Equals(42));
            Assert.IsFalse(eitherLeftIntDouble1.Equals(12.0));

            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(42);
            Assert.IsTrue(eitherLeftIntDouble1.Equals(eitherLeftInt1));
            Assert.IsTrue(eitherLeftIntDouble1 == eitherLeftInt1);
            Assert.IsTrue(eitherLeftInt1 == eitherLeftIntDouble1);
            Assert.IsFalse(eitherLeftIntDouble1 != eitherLeftInt1);
            Assert.IsFalse(eitherLeftInt1 != eitherLeftIntDouble1);

            Assert.IsFalse(eitherLeftIntDouble1.Equals(eitherLeftInt2));
            Assert.IsFalse(eitherLeftIntDouble1 == eitherLeftInt2);
            Assert.IsFalse(eitherLeftInt2 == eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftIntDouble1 != eitherLeftInt2);
            Assert.IsTrue(eitherLeftInt2 != eitherLeftIntDouble1);


            var eitherRightInt1 = Either.Right(12);
            var eitherRightInt2 = Either.Right(42);
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherLeftIntDouble1.Equals(eitherRightInt1));
            Assert.IsFalse(eitherLeftIntDouble1.Equals(eitherRightInt2));
            // ReSharper restore SuspiciousTypeConversion.Global

            var eitherRightDouble = Either.Right(12.5);
            Assert.IsFalse(eitherLeftIntDouble1 == eitherRightDouble);
            Assert.IsFalse(eitherRightDouble == eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftIntDouble1 != eitherRightDouble);
            Assert.IsTrue(eitherRightDouble != eitherLeftIntDouble1);

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherLeftPersonInt1.Equals(12));
            Assert.IsFalse(eitherLeftIntDouble1.Equals(eitherLeftPersonInt1));
            Assert.IsFalse(eitherLeftPersonInt1.Equals(eitherLeftIntDouble1));
            // ReSharper restore SuspiciousTypeConversion.Global

            // Equals with an object value
            Assert.IsFalse(eitherLeftIntDouble1.Equals(null));
            Assert.IsTrue(eitherLeftIntDouble1.Equals((object)eitherLeftIntDouble1));

            // Either same left and right types
            var eitherLeftIntInt1 = Either.Left<int, int>(12);
            var eitherLeftIntInt2 = Either.Left<int, int>(12);
            var eitherLeftIntInt3 = Either.Left<int, int>(42);
            var eitherNoneIntInt1 = Either<int, int>.None;
            var eitherNoneIntInt2 = new Either<int, int>();

            Assert.IsTrue(eitherLeftIntInt1.Equals(eitherLeftIntInt2));
            Assert.IsTrue(eitherLeftIntInt2.Equals(eitherLeftIntInt1));
            Assert.IsTrue(eitherLeftIntInt1 == eitherLeftIntInt2);
            Assert.IsTrue(eitherLeftIntInt2 == eitherLeftIntInt1);
            Assert.IsFalse(eitherLeftIntInt1 != eitherLeftIntInt2);
            Assert.IsFalse(eitherLeftIntInt2 != eitherLeftIntInt1);

            Assert.IsFalse(eitherLeftIntInt1.Equals(eitherLeftIntInt3));
            Assert.IsFalse(eitherLeftIntInt3.Equals(eitherLeftIntInt1));
            Assert.IsFalse(eitherLeftIntInt1 == eitherLeftIntInt3);
            Assert.IsFalse(eitherLeftIntInt3 == eitherLeftIntInt1);
            Assert.IsTrue(eitherLeftIntInt1 != eitherLeftIntInt3);
            Assert.IsTrue(eitherLeftIntInt3 != eitherLeftIntInt1);

            Assert.IsFalse(eitherLeftIntInt1.Equals(eitherNoneIntInt1));
            Assert.IsFalse(eitherNoneIntInt1.Equals(eitherLeftIntInt1));
            Assert.IsFalse(eitherLeftIntInt1 == eitherNoneIntInt1);
            Assert.IsFalse(eitherNoneIntInt1 == eitherLeftIntInt1);
            Assert.IsTrue(eitherLeftIntInt1 != eitherNoneIntInt1);
            Assert.IsTrue(eitherNoneIntInt1 != eitherLeftIntInt1);

            Assert.IsTrue(eitherNoneIntInt1.Equals(eitherNoneIntInt2));
            Assert.IsTrue(eitherNoneIntInt2.Equals(eitherNoneIntInt1));
            Assert.IsTrue(eitherNoneIntInt1 == eitherNoneIntInt2);
            Assert.IsTrue(eitherNoneIntInt2 == eitherNoneIntInt1);
            Assert.IsFalse(eitherNoneIntInt1 != eitherNoneIntInt2);
            Assert.IsFalse(eitherNoneIntInt2 != eitherNoneIntInt1);


            Assert.IsTrue(eitherLeftIntInt1.Equals(eitherLeftInt1));
            Assert.IsTrue(eitherLeftIntInt1 == eitherLeftInt1);
            Assert.IsTrue(eitherLeftInt1 == eitherLeftIntInt1);
            Assert.IsFalse(eitherLeftIntInt1 != eitherLeftInt1);
            Assert.IsFalse(eitherLeftInt1 != eitherLeftIntInt1);

            Assert.IsFalse(eitherLeftIntInt1.Equals(eitherRightInt1));
            Assert.IsFalse(eitherLeftIntInt1 == eitherRightInt1);
            Assert.IsFalse(eitherRightInt1 == eitherLeftIntInt1);
            Assert.IsTrue(eitherLeftIntInt1 != eitherRightInt1);
            Assert.IsTrue(eitherRightInt1 != eitherLeftIntInt1);
        }

        [Test]
        public void EitherEqualityRight()
        {
            // Either value type
            var eitherRightDoubleInt1 = Either.Right<double, int>(12);
            var eitherRightDoubleInt2 = Either.Right<double, int>(12);
            var eitherRightDoubleInt3 = Either.Right<double, int>(42);
            var eitherNoneDoubleInt1 = Either<double, int>.None;
            var eitherNoneDoubleInt2 = new Either<double, int>();

            Assert.IsTrue(eitherRightDoubleInt1.Equals(eitherRightDoubleInt2));
            Assert.IsTrue(eitherRightDoubleInt2.Equals(eitherRightDoubleInt1));
            Assert.IsTrue(eitherRightDoubleInt1 == eitherRightDoubleInt2);
            Assert.IsTrue(eitherRightDoubleInt2 == eitherRightDoubleInt1);
            Assert.IsFalse(eitherRightDoubleInt1 != eitherRightDoubleInt2);
            Assert.IsFalse(eitherRightDoubleInt2 != eitherRightDoubleInt1);

            Assert.IsFalse(eitherRightDoubleInt1.Equals(eitherRightDoubleInt3));
            Assert.IsFalse(eitherRightDoubleInt3.Equals(eitherRightDoubleInt1));
            Assert.IsFalse(eitherRightDoubleInt1 == eitherRightDoubleInt3);
            Assert.IsFalse(eitherRightDoubleInt3 == eitherRightDoubleInt1);
            Assert.IsTrue(eitherRightDoubleInt1 != eitherRightDoubleInt3);
            Assert.IsTrue(eitherRightDoubleInt3 != eitherRightDoubleInt1);

            Assert.IsFalse(eitherRightDoubleInt1.Equals(eitherNoneDoubleInt1));
            Assert.IsFalse(eitherNoneDoubleInt1.Equals(eitherRightDoubleInt1));
            Assert.IsFalse(eitherRightDoubleInt1 == eitherNoneDoubleInt1);
            Assert.IsFalse(eitherNoneDoubleInt1 == eitherRightDoubleInt1);
            Assert.IsTrue(eitherRightDoubleInt1 != eitherNoneDoubleInt1);
            Assert.IsTrue(eitherNoneDoubleInt1 != eitherRightDoubleInt1);

            Assert.IsTrue(eitherNoneDoubleInt1.Equals(eitherNoneDoubleInt2));
            Assert.IsTrue(eitherNoneDoubleInt2.Equals(eitherNoneDoubleInt1));
            Assert.IsTrue(eitherNoneDoubleInt1 == eitherNoneDoubleInt2);
            Assert.IsTrue(eitherNoneDoubleInt2 == eitherNoneDoubleInt1);
            Assert.IsFalse(eitherNoneDoubleInt1 != eitherNoneDoubleInt2);
            Assert.IsFalse(eitherNoneDoubleInt2 != eitherNoneDoubleInt1);

            // Either reference type
            var person1 = new Person("Test");
            var eitherRightIntPerson1 = Either.Right<int, Person>(person1);
            var eitherRightIntPerson2 = Either.Right<int, Person>(person1);
            var eitherRightIntPerson3 = Either.Right<int, Person>(new Person("Test"));
            var eitherRightIntPerson4 = Either.Right<int, Person>(new Person("Test2"));
            var eitherNoneIntPerson1 = Either<int, Person>.None;
            var eitherNoneIntPerson2 = new Either<int, Person>();

            Assert.IsTrue(eitherRightIntPerson1.Equals(eitherRightIntPerson2));
            Assert.IsTrue(eitherRightIntPerson2.Equals(eitherRightIntPerson1));
            Assert.IsTrue(eitherRightIntPerson1 == eitherRightIntPerson2);
            Assert.IsTrue(eitherRightIntPerson2 == eitherRightIntPerson1);
            Assert.IsFalse(eitherRightIntPerson1 != eitherRightIntPerson2);
            Assert.IsFalse(eitherRightIntPerson2 != eitherRightIntPerson1);

            Assert.IsTrue(eitherRightIntPerson1.Equals(eitherRightIntPerson3));
            Assert.IsTrue(eitherRightIntPerson3.Equals(eitherRightIntPerson1));
            Assert.IsTrue(eitherRightIntPerson1 == eitherRightIntPerson3);
            Assert.IsTrue(eitherRightIntPerson3 == eitherRightIntPerson1);
            Assert.IsFalse(eitherRightIntPerson1 != eitherRightIntPerson3);
            Assert.IsFalse(eitherRightIntPerson3 != eitherRightIntPerson1);

            Assert.IsFalse(eitherRightIntPerson1.Equals(eitherRightIntPerson4));
            Assert.IsFalse(eitherRightIntPerson4.Equals(eitherRightIntPerson1));
            Assert.IsFalse(eitherRightIntPerson1 == eitherRightIntPerson4);
            Assert.IsFalse(eitherRightIntPerson4 == eitherRightIntPerson1);
            Assert.IsTrue(eitherRightIntPerson1 != eitherRightIntPerson4);
            Assert.IsTrue(eitherRightIntPerson4 != eitherRightIntPerson1);

            Assert.IsFalse(eitherRightIntPerson1.Equals(eitherNoneIntPerson1));
            Assert.IsFalse(eitherNoneIntPerson1.Equals(eitherRightIntPerson1));
            Assert.IsFalse(eitherRightIntPerson1 == eitherNoneIntPerson1);
            Assert.IsFalse(eitherNoneIntPerson1 == eitherRightIntPerson1);
            Assert.IsTrue(eitherRightIntPerson1 != eitherNoneIntPerson1);
            Assert.IsTrue(eitherNoneIntPerson1 != eitherRightIntPerson1);

            Assert.IsTrue(eitherNoneIntPerson1.Equals(eitherNoneIntPerson2));
            Assert.IsTrue(eitherNoneIntPerson2.Equals(eitherNoneIntPerson1));
            Assert.IsTrue(eitherNoneIntPerson1 == eitherNoneIntPerson2);
            Assert.IsTrue(eitherNoneIntPerson2 == eitherNoneIntPerson1);
            Assert.IsFalse(eitherNoneIntPerson1 != eitherNoneIntPerson2);
            Assert.IsFalse(eitherNoneIntPerson2 != eitherNoneIntPerson1);

            var person2 = new PersonNotEquatable("Test");
            var eitherRightIntPerson5 = Either.Right<int, PersonNotEquatable>(person2);
            var eitherRightIntPerson6 = Either.Right<int, PersonNotEquatable>(person2);
            var eitherRightIntPerson7 = Either.Right<int, PersonNotEquatable>(new PersonNotEquatable("Test"));
            var eitherNoneIntPerson3 = Either<int, PersonNotEquatable>.None;
            var eitherNoneIntPerson4 = new Either<int, PersonNotEquatable>();

            Assert.IsTrue(eitherRightIntPerson5.Equals(eitherRightIntPerson6));
            Assert.IsTrue(eitherRightIntPerson6.Equals(eitherRightIntPerson5));
            Assert.IsTrue(eitherRightIntPerson5 == eitherRightIntPerson6);
            Assert.IsTrue(eitherRightIntPerson6 == eitherRightIntPerson5);
            Assert.IsFalse(eitherRightIntPerson5 != eitherRightIntPerson6);
            Assert.IsFalse(eitherRightIntPerson6 != eitherRightIntPerson5);

            Assert.IsFalse(eitherRightIntPerson5.Equals(eitherRightIntPerson7));
            Assert.IsFalse(eitherRightIntPerson7.Equals(eitherRightIntPerson5));
            Assert.IsFalse(eitherRightIntPerson5 == eitherRightIntPerson7);
            Assert.IsFalse(eitherRightIntPerson7 == eitherRightIntPerson5);
            Assert.IsTrue(eitherRightIntPerson5 != eitherRightIntPerson7);
            Assert.IsTrue(eitherRightIntPerson7 != eitherRightIntPerson5);

            Assert.IsFalse(eitherRightIntPerson5.Equals(eitherNoneIntPerson3));
            Assert.IsFalse(eitherNoneIntPerson3.Equals(eitherRightIntPerson5));
            Assert.IsFalse(eitherRightIntPerson5 == eitherNoneIntPerson3);
            Assert.IsFalse(eitherNoneIntPerson3 == eitherRightIntPerson5);
            Assert.IsTrue(eitherRightIntPerson5 != eitherNoneIntPerson3);
            Assert.IsTrue(eitherNoneIntPerson3 != eitherRightIntPerson5);

            Assert.IsTrue(eitherNoneIntPerson3.Equals(eitherNoneIntPerson4));
            Assert.IsTrue(eitherNoneIntPerson4.Equals(eitherNoneIntPerson3));
            Assert.IsTrue(eitherNoneIntPerson3 == eitherNoneIntPerson4);
            Assert.IsTrue(eitherNoneIntPerson4 == eitherNoneIntPerson3);
            Assert.IsFalse(eitherNoneIntPerson3 != eitherNoneIntPerson4);
            Assert.IsFalse(eitherNoneIntPerson4 != eitherNoneIntPerson3);

            // Mixed
            Assert.IsTrue(eitherRightDoubleInt1.Equals(12));
            Assert.IsFalse(eitherRightDoubleInt1.Equals(42));
            Assert.IsFalse(eitherRightDoubleInt1.Equals(12.0));

            var eitherRightInt1 = Either.Right(12);
            var eitherRightInt2 = Either.Right(42);
            Assert.IsTrue(eitherRightDoubleInt1.Equals(eitherRightInt1));
            Assert.IsTrue(eitherRightDoubleInt1 == eitherRightInt1);
            Assert.IsTrue(eitherRightInt1 == eitherRightDoubleInt1);
            Assert.IsFalse(eitherRightDoubleInt1 != eitherRightInt1);
            Assert.IsFalse(eitherRightInt1 != eitherRightDoubleInt1);

            Assert.IsFalse(eitherRightDoubleInt1.Equals(eitherRightInt2));
            Assert.IsFalse(eitherRightDoubleInt1 == eitherRightInt2);
            Assert.IsFalse(eitherRightInt2 == eitherRightDoubleInt1);
            Assert.IsTrue(eitherRightDoubleInt1 != eitherRightInt2);
            Assert.IsTrue(eitherRightInt2 != eitherRightDoubleInt1);


            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(42);
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherRightDoubleInt1.Equals(eitherLeftInt1));
            Assert.IsFalse(eitherRightDoubleInt1.Equals(eitherLeftInt2));
            // ReSharper restore SuspiciousTypeConversion.Global

            var eitherLeftDouble = Either.Left(12.5);
            Assert.IsFalse(eitherRightDoubleInt1 == eitherLeftDouble);
            Assert.IsFalse(eitherLeftDouble == eitherRightDoubleInt1);
            Assert.IsTrue(eitherRightDoubleInt1 != eitherLeftDouble);
            Assert.IsTrue(eitherLeftDouble != eitherRightDoubleInt1);

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherRightIntPerson1.Equals(12));
            Assert.IsFalse(eitherRightDoubleInt1.Equals(eitherRightIntPerson1));
            Assert.IsFalse(eitherRightIntPerson1.Equals(eitherRightDoubleInt1));
            // ReSharper restore SuspiciousTypeConversion.Global

            // Equals with an object value
            Assert.IsFalse(eitherRightDoubleInt1.Equals(null));
            Assert.IsTrue(eitherRightDoubleInt1.Equals((object)eitherRightDoubleInt1));

            // Either same left and right types
            var eitherRightIntInt1 = Either.Right<int, int>(12);
            var eitherRightIntInt2 = Either.Right<int, int>(12);
            var eitherRightIntInt3 = Either.Right<int, int>(42);
            var eitherNoneIntInt1 = Either<int, int>.None;
            var eitherNoneIntInt2 = new Either<int, int>();

            Assert.IsTrue(eitherRightIntInt1.Equals(eitherRightIntInt2));
            Assert.IsTrue(eitherRightIntInt2.Equals(eitherRightIntInt1));
            Assert.IsTrue(eitherRightIntInt1 == eitherRightIntInt2);
            Assert.IsTrue(eitherRightIntInt2 == eitherRightIntInt1);
            Assert.IsFalse(eitherRightIntInt1 != eitherRightIntInt2);
            Assert.IsFalse(eitherRightIntInt2 != eitherRightIntInt1);

            Assert.IsFalse(eitherRightIntInt1.Equals(eitherRightIntInt3));
            Assert.IsFalse(eitherRightIntInt3.Equals(eitherRightIntInt1));
            Assert.IsFalse(eitherRightIntInt1 == eitherRightIntInt3);
            Assert.IsFalse(eitherRightIntInt3 == eitherRightIntInt1);
            Assert.IsTrue(eitherRightIntInt1 != eitherRightIntInt3);
            Assert.IsTrue(eitherRightIntInt3 != eitherRightIntInt1);

            Assert.IsFalse(eitherRightIntInt1.Equals(eitherNoneIntInt1));
            Assert.IsFalse(eitherNoneIntInt1.Equals(eitherRightIntInt1));
            Assert.IsFalse(eitherRightIntInt1 == eitherNoneIntInt1);
            Assert.IsFalse(eitherNoneIntInt1 == eitherRightIntInt1);
            Assert.IsTrue(eitherRightIntInt1 != eitherNoneIntInt1);
            Assert.IsTrue(eitherNoneIntInt1 != eitherRightIntInt1);

            Assert.IsTrue(eitherNoneIntInt1.Equals(eitherNoneIntInt2));
            Assert.IsTrue(eitherNoneIntInt2.Equals(eitherNoneIntInt1));
            Assert.IsTrue(eitherNoneIntInt1 == eitherNoneIntInt2);
            Assert.IsTrue(eitherNoneIntInt2 == eitherNoneIntInt1);
            Assert.IsFalse(eitherNoneIntInt1 != eitherNoneIntInt2);
            Assert.IsFalse(eitherNoneIntInt2 != eitherNoneIntInt1);


            Assert.IsTrue(eitherRightIntInt1.Equals(eitherRightInt1));
            Assert.IsTrue(eitherRightIntInt1 == eitherRightInt1);
            Assert.IsTrue(eitherRightInt1 == eitherRightIntInt1);
            Assert.IsFalse(eitherRightIntInt1 != eitherRightInt1);
            Assert.IsFalse(eitherRightInt1 != eitherRightIntInt1);

            Assert.IsFalse(eitherRightIntInt1.Equals(eitherLeftInt1));
            Assert.IsFalse(eitherRightIntInt1 == eitherLeftInt1);
            Assert.IsFalse(eitherLeftInt1 == eitherRightIntInt1);
            Assert.IsTrue(eitherRightIntInt1 != eitherLeftInt1);
            Assert.IsTrue(eitherLeftInt1 != eitherRightIntInt1);
        }

        [Test]
        public void EitherEqualityLeftAndRight()
        {
            var eitherLeftDoubleInt = Either.Left<double, int>(12.5);
            var eitherLeftIntDouble = Either.Left<int, double>(12);
            var eitherRightDoubleInt = Either.Right<double, int>(12);

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherLeftDoubleInt.Equals(eitherLeftIntDouble));
            Assert.IsFalse(eitherLeftIntDouble.Equals(eitherLeftDoubleInt));
            // ReSharper restore SuspiciousTypeConversion.Global

            Assert.IsFalse(eitherLeftDoubleInt.Equals(eitherRightDoubleInt));
            Assert.IsFalse(eitherRightDoubleInt.Equals(eitherLeftDoubleInt));
            Assert.IsFalse(eitherLeftDoubleInt == eitherRightDoubleInt);
            Assert.IsFalse(eitherRightDoubleInt == eitherLeftDoubleInt);
            Assert.IsTrue(eitherLeftDoubleInt != eitherRightDoubleInt);
            Assert.IsTrue(eitherRightDoubleInt != eitherLeftDoubleInt);

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherLeftIntDouble.Equals(eitherRightDoubleInt));
            Assert.IsFalse(eitherRightDoubleInt.Equals(eitherLeftIntDouble));
            // ReSharper restore SuspiciousTypeConversion.Global

            // Same left and right type
            var eitherLeftIntInt = Either.Left<int, int>(12);
            var eitherRightIntInt = Either.Right<int, int>(12);

            Assert.IsFalse(eitherLeftIntInt.Equals(eitherRightIntInt));
            Assert.IsFalse(eitherRightIntInt.Equals(eitherLeftIntInt));
            Assert.IsFalse(eitherLeftIntInt == eitherRightIntInt);
            Assert.IsFalse(eitherRightIntInt == eitherLeftIntInt);
            Assert.IsTrue(eitherLeftIntInt != eitherRightIntInt);
            Assert.IsTrue(eitherRightIntInt != eitherLeftIntInt);
        }

        [Test]
        public void EitherEqualityHelper()
        {
            // Left Vs Left
            var eitherLeftIntDouble1 = Either.Left<int, double>(12);
            var eitherLeftIntDouble2 = Either.Left<int, double>(12);
            var eitherLeftIntDouble3 = Either.Left<int, double>(42);
            var eitherNoneIntDouble1 = new Either<int, double>();
            var eitherNoneIntDouble2 = Either<int, double>.None;

            Assert.IsTrue(EitherHelpers.AreEqual(eitherLeftIntDouble1, eitherLeftIntDouble1));

            Assert.IsTrue(EitherHelpers.AreEqual(eitherLeftIntDouble1, eitherLeftIntDouble2));
            Assert.IsTrue(EitherHelpers.AreEqual(eitherLeftIntDouble2, eitherLeftIntDouble1));

            Assert.IsFalse(EitherHelpers.AreEqual(eitherLeftIntDouble1, eitherLeftIntDouble3));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherLeftIntDouble3, eitherLeftIntDouble1));

            Assert.IsFalse(EitherHelpers.AreEqual(eitherLeftIntDouble1, eitherNoneIntDouble1));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherNoneIntDouble1, eitherLeftIntDouble1));

            Assert.IsTrue(EitherHelpers.AreEqual(eitherNoneIntDouble1, eitherNoneIntDouble2));
            Assert.IsTrue(EitherHelpers.AreEqual(eitherNoneIntDouble2, eitherNoneIntDouble1));

            // Right Vs Right
            var eitherRightIntDouble1 = Either.Right<int, double>(12.5);
            var eitherRightIntDouble2 = Either.Right<int, double>(12.5);
            var eitherRightIntDouble3 = Either.Right<int, double>(42.5);

            Assert.IsTrue(EitherHelpers.AreEqual(eitherRightIntDouble1, eitherRightIntDouble1));

            Assert.IsTrue(EitherHelpers.AreEqual(eitherRightIntDouble1, eitherRightIntDouble2));
            Assert.IsTrue(EitherHelpers.AreEqual(eitherRightIntDouble2, eitherRightIntDouble1));

            Assert.IsFalse(EitherHelpers.AreEqual(eitherRightIntDouble1, eitherRightIntDouble3));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherRightIntDouble3, eitherRightIntDouble1));

            Assert.IsFalse(EitherHelpers.AreEqual(eitherRightIntDouble1, eitherNoneIntDouble1));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherNoneIntDouble1, eitherRightIntDouble1));

            Assert.IsTrue(EitherHelpers.AreEqual(eitherNoneIntDouble1, eitherNoneIntDouble2));
            Assert.IsTrue(EitherHelpers.AreEqual(eitherNoneIntDouble2, eitherNoneIntDouble1));

            // Left Vs Right
            Assert.IsFalse(EitherHelpers.AreEqual(eitherLeftIntDouble1, eitherRightIntDouble1));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherRightIntDouble1, eitherLeftIntDouble1));
        }

        [Test]
        public void EitherCompareLeft()
        {
            var eitherLeftIntDouble1 = Either.Left<int, double>(12);
            var eitherLeftIntDouble2 = Either.Left<int, double>(12);
            var eitherLeftIntDouble3 = Either.Left<int, double>(42);
            var eitherNoneIntDouble1 = Either<int, double>.None;
            var eitherNoneIntDouble2 = new Either<int, double>();

            Assert.AreEqual(0, eitherLeftIntDouble1.CompareTo(eitherLeftIntDouble1));
            Assert.AreEqual(0, eitherLeftIntDouble1.CompareTo((object)eitherLeftIntDouble1));

            Assert.AreEqual(0, eitherLeftIntDouble1.CompareTo(eitherLeftIntDouble2));
            Assert.AreEqual(0, eitherLeftIntDouble1.CompareTo((object)eitherLeftIntDouble2));
            Assert.AreEqual(0, eitherLeftIntDouble2.CompareTo(eitherLeftIntDouble1));
            Assert.AreEqual(0, eitherLeftIntDouble2.CompareTo((object)eitherLeftIntDouble1));
            Assert.IsFalse(eitherLeftIntDouble1 < eitherLeftIntDouble2);
            Assert.IsTrue(eitherLeftIntDouble1 <= eitherLeftIntDouble2);
            Assert.IsFalse(eitherLeftIntDouble1 > eitherLeftIntDouble2);
            Assert.IsTrue(eitherLeftIntDouble1 >= eitherLeftIntDouble2);

            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(eitherLeftIntDouble3));
            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo((object)eitherLeftIntDouble3));
            Assert.AreEqual(1, eitherLeftIntDouble3.CompareTo(eitherLeftIntDouble1));
            Assert.AreEqual(1, eitherLeftIntDouble3.CompareTo((object)eitherLeftIntDouble1));
            Assert.IsTrue(eitherLeftIntDouble1 < eitherLeftIntDouble3);
            Assert.IsTrue(eitherLeftIntDouble1 <= eitherLeftIntDouble3);
            Assert.IsFalse(eitherLeftIntDouble1 > eitherLeftIntDouble3);
            Assert.IsFalse(eitherLeftIntDouble1 >= eitherLeftIntDouble3);

            Assert.AreEqual(1, eitherLeftIntDouble1.CompareTo(eitherNoneIntDouble1));
            Assert.AreEqual(1, eitherLeftIntDouble1.CompareTo((object)eitherNoneIntDouble1));
            Assert.AreEqual(-1, eitherNoneIntDouble1.CompareTo(eitherLeftIntDouble1));
            Assert.AreEqual(-1, eitherNoneIntDouble1.CompareTo((object)eitherLeftIntDouble1));
            Assert.IsFalse(eitherLeftIntDouble1 < eitherNoneIntDouble1);
            Assert.IsFalse(eitherLeftIntDouble1 <= eitherNoneIntDouble1);
            Assert.IsTrue(eitherLeftIntDouble1 > eitherNoneIntDouble1);
            Assert.IsTrue(eitherLeftIntDouble1 >= eitherNoneIntDouble1);

            Assert.AreEqual(0, eitherNoneIntDouble1.CompareTo(eitherNoneIntDouble2));
            Assert.AreEqual(0, eitherNoneIntDouble1.CompareTo((object)eitherNoneIntDouble2));
            Assert.AreEqual(0, eitherNoneIntDouble2.CompareTo(eitherNoneIntDouble1));
            Assert.AreEqual(0, eitherNoneIntDouble2.CompareTo((object)eitherNoneIntDouble1));
            Assert.IsFalse(eitherNoneIntDouble1 < eitherNoneIntDouble2);
            Assert.IsTrue(eitherNoneIntDouble1 <= eitherNoneIntDouble2);
            Assert.IsFalse(eitherNoneIntDouble1 > eitherNoneIntDouble2);
            Assert.IsTrue(eitherNoneIntDouble1 >= eitherNoneIntDouble2);

            // With null
            Assert.AreEqual(1, eitherLeftIntDouble1.CompareTo(null));
            Assert.AreEqual(1, eitherNoneIntDouble1.CompareTo(null));

            // With EitherLeft
            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(42);
            var eitherLeftInt3 = Either.Left(2);
            Assert.AreEqual(0, eitherLeftIntDouble1.CompareTo(eitherLeftInt1));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)eitherLeftInt1));
            Assert.IsFalse(eitherLeftIntDouble1 < eitherLeftInt1);
            Assert.IsTrue(eitherLeftIntDouble1 <= eitherLeftInt1);
            Assert.IsFalse(eitherLeftIntDouble1 > eitherLeftInt1);
            Assert.IsTrue(eitherLeftIntDouble1 >= eitherLeftInt1);
            Assert.IsFalse(eitherLeftInt1 < eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftInt1 <= eitherLeftIntDouble1);
            Assert.IsFalse(eitherLeftInt1 > eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftInt1 >= eitherLeftIntDouble1);

            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(eitherLeftInt2));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)eitherLeftInt2));
            Assert.IsTrue(eitherLeftIntDouble1 < eitherLeftInt2);
            Assert.IsTrue(eitherLeftIntDouble1 <= eitherLeftInt2);
            Assert.IsFalse(eitherLeftIntDouble1 > eitherLeftInt2);
            Assert.IsFalse(eitherLeftIntDouble1 >= eitherLeftInt2);
            Assert.IsFalse(eitherLeftInt2 < eitherLeftIntDouble1);
            Assert.IsFalse(eitherLeftInt2 <= eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftInt2 > eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftInt2 >= eitherLeftIntDouble1);

            Assert.AreEqual(1, eitherLeftIntDouble1.CompareTo(eitherLeftInt3));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)eitherLeftInt3));
            Assert.IsFalse(eitherLeftIntDouble1 < eitherLeftInt3);
            Assert.IsFalse(eitherLeftIntDouble1 <= eitherLeftInt3);
            Assert.IsTrue(eitherLeftIntDouble1 > eitherLeftInt3);
            Assert.IsTrue(eitherLeftIntDouble1 >= eitherLeftInt3);
            Assert.IsTrue(eitherLeftInt3 < eitherLeftIntDouble1);
            Assert.IsTrue(eitherLeftInt3 <= eitherLeftIntDouble1);
            Assert.IsFalse(eitherLeftInt3 > eitherLeftIntDouble1);
            Assert.IsFalse(eitherLeftInt3 >= eitherLeftIntDouble1);

            // With EitherRight
            var eitherRightDouble = Either.Right(12.5);
            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(eitherRightDouble));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)eitherRightDouble));
            Assert.IsTrue(eitherLeftIntDouble1 < eitherRightDouble);
            Assert.IsTrue(eitherLeftIntDouble1 <= eitherRightDouble);
            Assert.IsFalse(eitherLeftIntDouble1 > eitherRightDouble);
            Assert.IsFalse(eitherLeftIntDouble1 >= eitherRightDouble);
            Assert.IsFalse(eitherRightDouble < eitherLeftIntDouble1);
            Assert.IsFalse(eitherRightDouble <= eitherLeftIntDouble1);
            Assert.IsTrue(eitherRightDouble > eitherLeftIntDouble1);
            Assert.IsTrue(eitherRightDouble >= eitherLeftIntDouble1);

            // With left value
            Assert.AreEqual(0, eitherLeftIntDouble1.CompareTo(12));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)12));

            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(42));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)42));

            Assert.AreEqual(1, eitherLeftIntDouble1.CompareTo(2));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)2));

            // With right value
            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(12.5));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)12.5));

            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(42.5));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)42.5));

            Assert.AreEqual(-1, eitherLeftIntDouble1.CompareTo(2.5));
            Assert.Throws<ArgumentException>(() => eitherLeftIntDouble1.CompareTo((object)2.5));

            // Mixed
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.Throws<ArgumentException>(() => { var _ = eitherLeftIntDouble1.CompareTo(new Person("Test")); });
        }

        [Test]
        public void EitherCompareRight()
        {
            var eitherRightIntDouble1 = Either.Right<int, double>(12.5);
            var eitherRightIntDouble2 = Either.Right<int, double>(12.5);
            var eitherRightIntDouble3 = Either.Right<int, double>(42.5);
            var eitherNoneIntDouble1 = Either<int, double>.None;
            var eitherNoneIntDouble2 = new Either<int, double>();

            Assert.AreEqual(0, eitherRightIntDouble1.CompareTo(eitherRightIntDouble1));
            Assert.AreEqual(0, eitherRightIntDouble1.CompareTo((object)eitherRightIntDouble1));

            Assert.AreEqual(0, eitherRightIntDouble1.CompareTo(eitherRightIntDouble2));
            Assert.AreEqual(0, eitherRightIntDouble1.CompareTo((object)eitherRightIntDouble2));
            Assert.AreEqual(0, eitherRightIntDouble2.CompareTo(eitherRightIntDouble1));
            Assert.AreEqual(0, eitherRightIntDouble2.CompareTo((object)eitherRightIntDouble1));
            Assert.IsFalse(eitherRightIntDouble1 < eitherRightIntDouble2);
            Assert.IsTrue(eitherRightIntDouble1 <= eitherRightIntDouble2);
            Assert.IsFalse(eitherRightIntDouble1 > eitherRightIntDouble2);
            Assert.IsTrue(eitherRightIntDouble1 >= eitherRightIntDouble2);

            Assert.AreEqual(-1, eitherRightIntDouble1.CompareTo(eitherRightIntDouble3));
            Assert.AreEqual(-1, eitherRightIntDouble1.CompareTo((object)eitherRightIntDouble3));
            Assert.AreEqual(1, eitherRightIntDouble3.CompareTo(eitherRightIntDouble1));
            Assert.AreEqual(1, eitherRightIntDouble3.CompareTo((object)eitherRightIntDouble1));
            Assert.IsTrue(eitherRightIntDouble1 < eitherRightIntDouble3);
            Assert.IsTrue(eitherRightIntDouble1 <= eitherRightIntDouble3);
            Assert.IsFalse(eitherRightIntDouble1 > eitherRightIntDouble3);
            Assert.IsFalse(eitherRightIntDouble1 >= eitherRightIntDouble3);

            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(eitherNoneIntDouble1));
            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo((object)eitherNoneIntDouble1));
            Assert.AreEqual(-1, eitherNoneIntDouble1.CompareTo(eitherRightIntDouble1));
            Assert.AreEqual(-1, eitherNoneIntDouble1.CompareTo((object)eitherRightIntDouble1));
            Assert.IsFalse(eitherRightIntDouble1 < eitherNoneIntDouble1);
            Assert.IsFalse(eitherRightIntDouble1 <= eitherNoneIntDouble1);
            Assert.IsTrue(eitherRightIntDouble1 > eitherNoneIntDouble1);
            Assert.IsTrue(eitherRightIntDouble1 >= eitherNoneIntDouble1);

            Assert.AreEqual(0, eitherNoneIntDouble1.CompareTo(eitherNoneIntDouble2));
            Assert.AreEqual(0, eitherNoneIntDouble1.CompareTo((object)eitherNoneIntDouble2));
            Assert.AreEqual(0, eitherNoneIntDouble2.CompareTo(eitherNoneIntDouble1));
            Assert.AreEqual(0, eitherNoneIntDouble2.CompareTo((object)eitherNoneIntDouble1));
            Assert.IsFalse(eitherNoneIntDouble1 < eitherNoneIntDouble2);
            Assert.IsTrue(eitherNoneIntDouble1 <= eitherNoneIntDouble2);
            Assert.IsFalse(eitherNoneIntDouble1 > eitherNoneIntDouble2);
            Assert.IsTrue(eitherNoneIntDouble1 >= eitherNoneIntDouble2);

            // With null
            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(null));
            Assert.AreEqual(1, eitherNoneIntDouble1.CompareTo(null));

            // With EitherLeft
            var eitherLeftInt = Either.Left(12);
            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(eitherLeftInt));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)eitherLeftInt));
            Assert.IsFalse(eitherRightIntDouble1 < eitherLeftInt);
            Assert.IsFalse(eitherRightIntDouble1 <= eitherLeftInt);
            Assert.IsTrue(eitherRightIntDouble1 > eitherLeftInt);
            Assert.IsTrue(eitherRightIntDouble1 >= eitherLeftInt);
            Assert.IsTrue(eitherLeftInt < eitherRightIntDouble1);
            Assert.IsTrue(eitherLeftInt <= eitherRightIntDouble1);
            Assert.IsFalse(eitherLeftInt > eitherRightIntDouble1);
            Assert.IsFalse(eitherLeftInt >= eitherRightIntDouble1);

            // With EitherRight
            var eitherRightDouble1 = Either.Right(12.5);
            var eitherRightDouble2 = Either.Right(42.5);
            var eitherRightDouble3 = Either.Right(2.5);
            Assert.AreEqual(0, eitherRightIntDouble1.CompareTo(eitherRightDouble1));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)eitherRightDouble1));
            Assert.IsFalse(eitherRightIntDouble1 < eitherRightDouble1);
            Assert.IsTrue(eitherRightIntDouble1 <= eitherRightDouble1);
            Assert.IsFalse(eitherRightIntDouble1 > eitherRightDouble1);
            Assert.IsTrue(eitherRightIntDouble1 >= eitherRightDouble1);
            Assert.IsFalse(eitherRightDouble1 < eitherRightIntDouble1);
            Assert.IsTrue(eitherRightDouble1 <= eitherRightIntDouble1);
            Assert.IsFalse(eitherRightDouble1 > eitherRightIntDouble1);
            Assert.IsTrue(eitherRightDouble1 >= eitherRightIntDouble1);

            Assert.AreEqual(-1, eitherRightIntDouble1.CompareTo(eitherRightDouble2));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)eitherRightDouble2));
            Assert.IsTrue(eitherRightIntDouble1 < eitherRightDouble2);
            Assert.IsTrue(eitherRightIntDouble1 <= eitherRightDouble2);
            Assert.IsFalse(eitherRightIntDouble1 > eitherRightDouble2);
            Assert.IsFalse(eitherRightIntDouble1 >= eitherRightDouble2);
            Assert.IsFalse(eitherRightDouble2 < eitherRightIntDouble1);
            Assert.IsFalse(eitherRightDouble2 <= eitherRightIntDouble1);
            Assert.IsTrue(eitherRightDouble2 > eitherRightIntDouble1);
            Assert.IsTrue(eitherRightDouble2 >= eitherRightIntDouble1);

            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(eitherRightDouble3));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)eitherRightDouble3));
            Assert.IsFalse(eitherRightIntDouble1 < eitherRightDouble3);
            Assert.IsFalse(eitherRightIntDouble1 <= eitherRightDouble3);
            Assert.IsTrue(eitherRightIntDouble1 > eitherRightDouble3);
            Assert.IsTrue(eitherRightIntDouble1 >= eitherRightDouble3);
            Assert.IsTrue(eitherRightDouble3 < eitherRightIntDouble1);
            Assert.IsTrue(eitherRightDouble3 <= eitherRightIntDouble1);
            Assert.IsFalse(eitherRightDouble3 > eitherRightIntDouble1);
            Assert.IsFalse(eitherRightDouble3 >= eitherRightIntDouble1);

            // With left value
            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(12));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)12));

            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(42));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)42));

            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(2));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)2));

            // With right value
            Assert.AreEqual(0, eitherRightIntDouble1.CompareTo(12.5));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)12.5));

            Assert.AreEqual(-1, eitherRightIntDouble1.CompareTo(42.5));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)42.5));

            Assert.AreEqual(1, eitherRightIntDouble1.CompareTo(2.5));
            Assert.Throws<ArgumentException>(() => eitherRightIntDouble1.CompareTo((object)2.5));

            // Mixed
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.Throws<ArgumentException>(() => { var _ = eitherRightIntDouble1.CompareTo(new Person("Test")); });
        }

        [Test]
        public void EitherCompareHelper()
        {
            // Left Vs Left
            var eitherLeftIntDouble1 = Either.Left<int, double>(12);
            var eitherLeftIntDouble2 = Either.Left<int, double>(12);
            var eitherLeftIntDouble3 = Either.Left<int, double>(42);
            var eitherNoneIntDouble1 = new Either<int, double>();
            var eitherNoneIntDouble2 = Either<int, double>.None;

            Assert.AreEqual(0, EitherHelpers.Compare(eitherLeftIntDouble1, eitherLeftIntDouble1));

            Assert.AreEqual(0, EitherHelpers.Compare(eitherLeftIntDouble1, eitherLeftIntDouble2));
            Assert.AreEqual(0, EitherHelpers.Compare(eitherLeftIntDouble2, eitherLeftIntDouble1));

            Assert.AreEqual(-1, EitherHelpers.Compare(eitherLeftIntDouble1, eitherLeftIntDouble3));
            Assert.AreEqual(1, EitherHelpers.Compare(eitherLeftIntDouble3, eitherLeftIntDouble1));

            Assert.AreEqual(1, EitherHelpers.Compare(eitherLeftIntDouble1, eitherNoneIntDouble1));
            Assert.AreEqual(-1, EitherHelpers.Compare(eitherNoneIntDouble1, eitherLeftIntDouble1));

            Assert.AreEqual(0, EitherHelpers.Compare(eitherNoneIntDouble1, eitherNoneIntDouble2));
            Assert.AreEqual(0, EitherHelpers.Compare(eitherNoneIntDouble2, eitherNoneIntDouble1));

            // Right Vs Right
            var eitherRightIntDouble1 = Either.Right<int, double>(12.5);
            var eitherRightIntDouble2 = Either.Right<int, double>(12.5);
            var eitherRightIntDouble3 = Either.Right<int, double>(42.5);

            Assert.AreEqual(0, EitherHelpers.Compare(eitherRightIntDouble1, eitherRightIntDouble1));

            Assert.AreEqual(0, EitherHelpers.Compare(eitherRightIntDouble1, eitherRightIntDouble2));
            Assert.AreEqual(0, EitherHelpers.Compare(eitherRightIntDouble2, eitherRightIntDouble1));

            Assert.AreEqual(-1, EitherHelpers.Compare(eitherRightIntDouble1, eitherRightIntDouble3));
            Assert.AreEqual(1, EitherHelpers.Compare(eitherRightIntDouble3, eitherRightIntDouble1));

            Assert.AreEqual(1, EitherHelpers.Compare(eitherRightIntDouble1, eitherNoneIntDouble1));
            Assert.AreEqual(-1, EitherHelpers.Compare(eitherNoneIntDouble1, eitherRightIntDouble1));

            Assert.AreEqual(0, EitherHelpers.Compare(eitherNoneIntDouble1, eitherNoneIntDouble2));
            Assert.AreEqual(0, EitherHelpers.Compare(eitherNoneIntDouble2, eitherNoneIntDouble1));

            // Left Vs Right
            Assert.AreEqual(-1, EitherHelpers.Compare(eitherLeftIntDouble1, eitherRightIntDouble1));
            Assert.AreEqual(1, EitherHelpers.Compare(eitherRightIntDouble1, eitherLeftIntDouble1));
        }

        #endregion

        #region EitherLeft

        [Test]
        public void EitherLeftConstruction()
        {
            EitherLeft<int> eitherLeft1 = Either.Left(12);
            CheckEitherLeft(eitherLeft1, 12);

            EitherLeft<int> eitherLeft2 = Either.Left((int?)12);
            CheckEitherLeft(eitherLeft2, 12);

            Assert.Throws<ArgumentNullException>(() => Either.Left((int?)null));
            Assert.Throws<ArgumentNullException>(() => Either.Left((Person)null));
        }

        [Test]
        public void EitherLeftEquality()
        {
            // EitherLeft value type
            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(12);
            var eitherLeftInt3 = Either.Left(42);

            Assert.IsTrue(eitherLeftInt1.Equals(eitherLeftInt2));
            Assert.IsTrue(eitherLeftInt2.Equals(eitherLeftInt1));
            Assert.IsTrue(eitherLeftInt1 == eitherLeftInt2);
            Assert.IsTrue(eitherLeftInt2 == eitherLeftInt1);
            Assert.IsFalse(eitherLeftInt1 != eitherLeftInt2);
            Assert.IsFalse(eitherLeftInt2 != eitherLeftInt1);

            Assert.IsFalse(eitherLeftInt1.Equals(eitherLeftInt3));
            Assert.IsFalse(eitherLeftInt3.Equals(eitherLeftInt1));
            Assert.IsFalse(eitherLeftInt1 == eitherLeftInt3);
            Assert.IsFalse(eitherLeftInt3 == eitherLeftInt1);
            Assert.IsTrue(eitherLeftInt1 != eitherLeftInt3);
            Assert.IsTrue(eitherLeftInt3 != eitherLeftInt1);

            // EitherLeft reference type
            var person1 = new Person("Test");
            var eitherLeftPerson1 = Either.Left(person1);
            var eitherLeftPerson2 = Either.Left(person1);
            var eitherLeftPerson3 = Either.Left(new Person("Test"));
            var eitherLeftPerson4 = Either.Left(new Person("Test2"));

            Assert.IsTrue(eitherLeftPerson1.Equals(eitherLeftPerson2));
            Assert.IsTrue(eitherLeftPerson2.Equals(eitherLeftPerson1));
            Assert.IsTrue(eitherLeftPerson1 == eitherLeftPerson2);
            Assert.IsTrue(eitherLeftPerson2 == eitherLeftPerson1);
            Assert.IsFalse(eitherLeftPerson1 != eitherLeftPerson2);
            Assert.IsFalse(eitherLeftPerson2 != eitherLeftPerson1);

            Assert.IsTrue(eitherLeftPerson1.Equals(eitherLeftPerson3));
            Assert.IsTrue(eitherLeftPerson3.Equals(eitherLeftPerson1));
            Assert.IsTrue(eitherLeftPerson1 == eitherLeftPerson3);
            Assert.IsTrue(eitherLeftPerson3 == eitherLeftPerson1);
            Assert.IsFalse(eitherLeftPerson1 != eitherLeftPerson3);
            Assert.IsFalse(eitherLeftPerson3 != eitherLeftPerson1);

            Assert.IsFalse(eitherLeftPerson1.Equals(eitherLeftPerson4));
            Assert.IsFalse(eitherLeftPerson4.Equals(eitherLeftPerson1));
            Assert.IsFalse(eitherLeftPerson1 == eitherLeftPerson4);
            Assert.IsFalse(eitherLeftPerson4 == eitherLeftPerson1);
            Assert.IsTrue(eitherLeftPerson1 != eitherLeftPerson4);
            Assert.IsTrue(eitherLeftPerson4 != eitherLeftPerson1);

            var person2 = new PersonNotEquatable("Test");
            var eitherLeftPerson5 = Either.Left(person2);
            var eitherLeftPerson6 = Either.Left(person2);
            var eitherLeftPerson7 = Either.Left(new PersonNotEquatable("Test"));

            Assert.IsTrue(eitherLeftPerson5.Equals(eitherLeftPerson6));
            Assert.IsTrue(eitherLeftPerson6.Equals(eitherLeftPerson5));
            Assert.IsTrue(eitherLeftPerson5 == eitherLeftPerson6);
            Assert.IsTrue(eitherLeftPerson6 == eitherLeftPerson5);
            Assert.IsFalse(eitherLeftPerson5 != eitherLeftPerson6);
            Assert.IsFalse(eitherLeftPerson6 != eitherLeftPerson5);

            Assert.IsFalse(eitherLeftPerson5.Equals(eitherLeftPerson7));
            Assert.IsFalse(eitherLeftPerson7.Equals(eitherLeftPerson5));
            Assert.IsFalse(eitherLeftPerson5 == eitherLeftPerson7);
            Assert.IsFalse(eitherLeftPerson7 == eitherLeftPerson5);
            Assert.IsTrue(eitherLeftPerson5 != eitherLeftPerson7);
            Assert.IsTrue(eitherLeftPerson7 != eitherLeftPerson5);

            // Mixed
            Assert.IsTrue(eitherLeftInt1.Equals(12));
            Assert.IsFalse(eitherLeftInt1.Equals(42));
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherLeftPerson1.Equals(12));
            Assert.IsFalse(eitherLeftInt1.Equals(eitherLeftPerson1));
            Assert.IsFalse(eitherLeftPerson1.Equals(eitherLeftInt1));
            // ReSharper restore SuspiciousTypeConversion.Global

            // Equals with an object value
            Assert.IsFalse(eitherLeftInt1.Equals(null));
            Assert.IsTrue(eitherLeftInt1.Equals((object)eitherLeftInt1));
        }

        [Test]
        public void EitherLeftEqualityHelper()
        {
            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(12);
            var eitherLeftInt3 = Either.Left(42);

            Assert.IsTrue(EitherHelpers.AreEqual(eitherLeftInt1, eitherLeftInt1));

            Assert.IsTrue(EitherHelpers.AreEqual(eitherLeftInt1, eitherLeftInt2));
            Assert.IsTrue(EitherHelpers.AreEqual(eitherLeftInt2, eitherLeftInt1));

            Assert.IsFalse(EitherHelpers.AreEqual(eitherLeftInt1, eitherLeftInt3));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherLeftInt3, eitherLeftInt1));
        }

        [Test]
        public void EitherLeftCompare()
        {
            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(12);
            var eitherLeftInt3 = Either.Left(42);

            Assert.AreEqual(0, eitherLeftInt1.CompareTo(eitherLeftInt1));
            Assert.AreEqual(0, eitherLeftInt1.CompareTo((object)eitherLeftInt1));

            Assert.AreEqual(0, eitherLeftInt1.CompareTo(eitherLeftInt2));
            Assert.AreEqual(0, eitherLeftInt1.CompareTo((object)eitherLeftInt2));
            Assert.AreEqual(0, eitherLeftInt2.CompareTo(eitherLeftInt1));
            Assert.AreEqual(0, eitherLeftInt2.CompareTo((object)eitherLeftInt1));
            Assert.IsFalse(eitherLeftInt1 < eitherLeftInt2);
            Assert.IsTrue(eitherLeftInt1 <= eitherLeftInt2);
            Assert.IsFalse(eitherLeftInt1 > eitherLeftInt2);
            Assert.IsTrue(eitherLeftInt1 >= eitherLeftInt2);

            Assert.AreEqual(-1, eitherLeftInt1.CompareTo(eitherLeftInt3));
            Assert.AreEqual(-1, eitherLeftInt1.CompareTo((object)eitherLeftInt3));
            Assert.AreEqual(1, eitherLeftInt3.CompareTo(eitherLeftInt1));
            Assert.AreEqual(1, eitherLeftInt3.CompareTo((object)eitherLeftInt1));
            Assert.IsTrue(eitherLeftInt1 < eitherLeftInt3);
            Assert.IsTrue(eitherLeftInt1 <= eitherLeftInt3);
            Assert.IsFalse(eitherLeftInt1 > eitherLeftInt3);
            Assert.IsFalse(eitherLeftInt1 >= eitherLeftInt3);

            // Mixed
            Assert.AreEqual(0, eitherLeftInt1.CompareTo(12));
            Assert.AreEqual(-1, eitherLeftInt1.CompareTo(42));
            Assert.AreEqual(1, eitherLeftInt1.CompareTo(2));

            Assert.AreEqual(1, eitherLeftInt1.CompareTo(null));

            var eitherRightInt = Either.Right(12);
            var optionFloat = Option<float>.Some(12.1f);
            Assert.Throws<ArgumentException>(() => { var _ = eitherLeftInt1.CompareTo(optionFloat); });
            Assert.Throws<ArgumentException>(() => { var _ = eitherLeftInt1.CompareTo(eitherRightInt); });
        }

        [Test]
        public void EitherLeftCompareHelper()
        {
            var eitherLeftInt1 = Either.Left(12);
            var eitherLeftInt2 = Either.Left(12);
            var eitherLeftInt3 = Either.Left(42);

            Assert.AreEqual(0, EitherHelpers.Compare(eitherLeftInt1, eitherLeftInt1));

            Assert.AreEqual(0, EitherHelpers.Compare(eitherLeftInt1, eitherLeftInt2));
            Assert.AreEqual(0, EitherHelpers.Compare(eitherLeftInt2, eitherLeftInt1));

            Assert.AreEqual(-1, EitherHelpers.Compare(eitherLeftInt1, eitherLeftInt3));
            Assert.AreEqual(1, EitherHelpers.Compare(eitherLeftInt3, eitherLeftInt1));
        }

        #endregion

        #region EitherRight

        [Test]
        public void EitherRightConstruction()
        {
            EitherRight<int> eitherRight1 = Either.Right(12);
            CheckEitherRight(eitherRight1, 12);

            EitherRight<int> eitherLeft2 = Either.Right((int?)12);
            CheckEitherRight(eitherLeft2, 12);

            Assert.Throws<ArgumentNullException>(() => Either.Right((Person)null));
            Assert.Throws<ArgumentNullException>(() => Either.Right((int?)null));
        }

        [Test]
        public void EitherRightEquality()
        {
            // EitherRight value type
            var eitherRightInt1 = Either.Right(12);
            var eitherRightInt2 = Either.Right(12);
            var eitherRightInt3 = Either.Right(42);

            Assert.IsTrue(eitherRightInt1.Equals(eitherRightInt2));
            Assert.IsTrue(eitherRightInt2.Equals(eitherRightInt1));
            Assert.IsTrue(eitherRightInt1 == eitherRightInt2);
            Assert.IsTrue(eitherRightInt2 == eitherRightInt1);
            Assert.IsFalse(eitherRightInt1 != eitherRightInt2);
            Assert.IsFalse(eitherRightInt2 != eitherRightInt1);

            Assert.IsFalse(eitherRightInt1.Equals(eitherRightInt3));
            Assert.IsFalse(eitherRightInt3.Equals(eitherRightInt1));
            Assert.IsFalse(eitherRightInt1 == eitherRightInt3);
            Assert.IsFalse(eitherRightInt3 == eitherRightInt1);
            Assert.IsTrue(eitherRightInt1 != eitherRightInt3);
            Assert.IsTrue(eitherRightInt3 != eitherRightInt1);

            // EitherRight reference type
            var person1 = new Person("Test");
            var eitherRightPerson1 = Either.Right(person1);
            var eitherRightPerson2 = Either.Right(person1);
            var eitherRightPerson3 = Either.Right(new Person("Test"));
            var eitherRightPerson4 = Either.Right(new Person("Test2"));

            Assert.IsTrue(eitherRightPerson1.Equals(eitherRightPerson2));
            Assert.IsTrue(eitherRightPerson2.Equals(eitherRightPerson1));
            Assert.IsTrue(eitherRightPerson1 == eitherRightPerson2);
            Assert.IsTrue(eitherRightPerson2 == eitherRightPerson1);
            Assert.IsFalse(eitherRightPerson1 != eitherRightPerson2);
            Assert.IsFalse(eitherRightPerson2 != eitherRightPerson1);

            Assert.IsTrue(eitherRightPerson1.Equals(eitherRightPerson3));
            Assert.IsTrue(eitherRightPerson3.Equals(eitherRightPerson1));
            Assert.IsTrue(eitherRightPerson1 == eitherRightPerson3);
            Assert.IsTrue(eitherRightPerson3 == eitherRightPerson1);
            Assert.IsFalse(eitherRightPerson1 != eitherRightPerson3);
            Assert.IsFalse(eitherRightPerson3 != eitherRightPerson1);

            Assert.IsFalse(eitherRightPerson1.Equals(eitherRightPerson4));
            Assert.IsFalse(eitherRightPerson4.Equals(eitherRightPerson1));
            Assert.IsFalse(eitherRightPerson1 == eitherRightPerson4);
            Assert.IsFalse(eitherRightPerson4 == eitherRightPerson1);
            Assert.IsTrue(eitherRightPerson1 != eitherRightPerson4);
            Assert.IsTrue(eitherRightPerson4 != eitherRightPerson1);

            var person2 = new PersonNotEquatable("Test");
            var eitherRightPerson5 = Either.Right(person2);
            var eitherRightPerson6 = Either.Right(person2);
            var eitherRightPerson7 = Either.Right(new PersonNotEquatable("Test"));

            Assert.IsTrue(eitherRightPerson5.Equals(eitherRightPerson6));
            Assert.IsTrue(eitherRightPerson6.Equals(eitherRightPerson5));
            Assert.IsTrue(eitherRightPerson5 == eitherRightPerson6);
            Assert.IsTrue(eitherRightPerson6 == eitherRightPerson5);
            Assert.IsFalse(eitherRightPerson5 != eitherRightPerson6);
            Assert.IsFalse(eitherRightPerson6 != eitherRightPerson5);

            Assert.IsFalse(eitherRightPerson5.Equals(eitherRightPerson7));
            Assert.IsFalse(eitherRightPerson7.Equals(eitherRightPerson5));
            Assert.IsFalse(eitherRightPerson5 == eitherRightPerson7);
            Assert.IsFalse(eitherRightPerson7 == eitherRightPerson5);
            Assert.IsTrue(eitherRightPerson5 != eitherRightPerson7);
            Assert.IsTrue(eitherRightPerson7 != eitherRightPerson5);

            // Mixed
            Assert.IsTrue(eitherRightInt1.Equals(12));
            Assert.IsFalse(eitherRightInt1.Equals(42));
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(eitherRightPerson1.Equals(12));
            Assert.IsFalse(eitherRightInt1.Equals(eitherRightPerson1));
            Assert.IsFalse(eitherRightPerson1.Equals(eitherRightInt1));
            // ReSharper restore SuspiciousTypeConversion.Global

            // Equals with an object value
            Assert.IsFalse(eitherRightInt1.Equals(null));
            Assert.IsTrue(eitherRightInt1.Equals((object)eitherRightInt1));
        }

        [Test]
        public void EitherRightEqualityHelper()
        {
            var eitherRightInt1 = Either.Right(12);
            var eitherRightInt2 = Either.Right(12);
            var eitherRightInt3 = Either.Right(42);

            Assert.IsTrue(EitherHelpers.AreEqual(eitherRightInt1, eitherRightInt1));

            Assert.IsTrue(EitherHelpers.AreEqual(eitherRightInt1, eitherRightInt2));
            Assert.IsTrue(EitherHelpers.AreEqual(eitherRightInt2, eitherRightInt1));

            Assert.IsFalse(EitherHelpers.AreEqual(eitherRightInt1, eitherRightInt3));
            Assert.IsFalse(EitherHelpers.AreEqual(eitherRightInt3, eitherRightInt1));
        }

        [Test]
        public void EitherRightCompare()
        {
            var eitherRightInt1 = Either.Right(12);
            var eitherRightInt2 = Either.Right(12);
            var eitherRightInt3 = Either.Right(42);

            Assert.AreEqual(0, eitherRightInt1.CompareTo(eitherRightInt1));
            Assert.AreEqual(0, eitherRightInt1.CompareTo((object)eitherRightInt1));

            Assert.AreEqual(0, eitherRightInt1.CompareTo(eitherRightInt2));
            Assert.AreEqual(0, eitherRightInt1.CompareTo((object)eitherRightInt2));
            Assert.AreEqual(0, eitherRightInt2.CompareTo(eitherRightInt1));
            Assert.AreEqual(0, eitherRightInt2.CompareTo((object)eitherRightInt1));
            Assert.IsFalse(eitherRightInt1 < eitherRightInt2);
            Assert.IsTrue(eitherRightInt1 <= eitherRightInt2);
            Assert.IsFalse(eitherRightInt1 > eitherRightInt2);
            Assert.IsTrue(eitherRightInt1 >= eitherRightInt2);

            Assert.AreEqual(-1, eitherRightInt1.CompareTo(eitherRightInt3));
            Assert.AreEqual(-1, eitherRightInt1.CompareTo((object)eitherRightInt3));
            Assert.AreEqual(1, eitherRightInt3.CompareTo(eitherRightInt1));
            Assert.AreEqual(1, eitherRightInt3.CompareTo((object)eitherRightInt1));
            Assert.IsTrue(eitherRightInt1 < eitherRightInt3);
            Assert.IsTrue(eitherRightInt1 <= eitherRightInt3);
            Assert.IsFalse(eitherRightInt1 > eitherRightInt3);
            Assert.IsFalse(eitherRightInt1 >= eitherRightInt3);

            // Mixed
            Assert.AreEqual(0, eitherRightInt1.CompareTo(12));
            Assert.AreEqual(-1, eitherRightInt1.CompareTo(42));
            Assert.AreEqual(1, eitherRightInt1.CompareTo(2));

            Assert.AreEqual(1, eitherRightInt1.CompareTo(null));

            var eitherLeftInt = Either.Left(12);
            var optionFloat = Option<float>.Some(12.1f);
            Assert.Throws<ArgumentException>(() => { var _ = eitherRightInt1.CompareTo(optionFloat); });
            Assert.Throws<ArgumentException>(() => { var _ = eitherRightInt1.CompareTo(eitherLeftInt); });
        }

        [Test]
        public void EitherRightCompareHelper()
        {
            var eitherRightInt1 = Either.Right(12);
            var eitherRightInt2 = Either.Right(12);
            var eitherRightInt3 = Either.Right(42);

            Assert.AreEqual(0, EitherHelpers.Compare(eitherRightInt1, eitherRightInt1));

            Assert.AreEqual(0, EitherHelpers.Compare(eitherRightInt1, eitherRightInt2));
            Assert.AreEqual(0, EitherHelpers.Compare(eitherRightInt2, eitherRightInt1));

            Assert.AreEqual(-1, EitherHelpers.Compare(eitherRightInt1, eitherRightInt3));
            Assert.AreEqual(1, EitherHelpers.Compare(eitherRightInt3, eitherRightInt1));
        }

        #endregion

        [Test]
        public void EitherHashCode()
        {
            // Equal values
            // Either left
            Either<string, int> eitherLeftStringInt1 = Either.Left("Either error message");
            Either<string, int> eitherLeftStringInt2 = Either.Left("Either error message");
            Assert.AreNotSame(eitherLeftStringInt1, eitherLeftStringInt2);
            Assert.IsTrue(eitherLeftStringInt1.GetHashCode() == eitherLeftStringInt2.GetHashCode());

            EitherLeft<int> eitherLeftInt1 = Either.Left(12);
            EitherLeft<int> eitherLeftInt2 = Either.Left(12);
            Assert.AreNotSame(eitherLeftInt1, eitherLeftInt2);
            Assert.IsTrue(eitherLeftInt1.GetHashCode() == eitherLeftInt2.GetHashCode());

            // Either right
            Either<string, int> eitherRightStringInt1 = Either.Right(12);
            Either<string, int> eitherRightStringInt2 = Either.Right(12);
            Assert.AreNotSame(eitherRightStringInt1, eitherRightStringInt2);
            Assert.IsTrue(eitherRightStringInt1.GetHashCode() == eitherRightStringInt2.GetHashCode());

            EitherRight<int> eitherRightInt1 = Either.Right(32);
            EitherRight<int> eitherRightInt2 = Either.Right(32);
            Assert.AreNotSame(eitherRightInt1, eitherRightInt2);
            Assert.IsTrue(eitherRightInt1.GetHashCode() == eitherRightInt2.GetHashCode());

            // Either none
            Either<string, int> eitherNone1 = new Either<string, int>();
            Either<string, int> eitherNone2 = Either<string, int>.None;
            Assert.AreNotSame(eitherNone1, eitherNone2);
            Assert.AreEqual(0, eitherNone1.GetHashCode());
            Assert.IsTrue(eitherNone1.GetHashCode() == eitherNone2.GetHashCode());

            // Mixed
            EitherLeft<string> eitherLeftString1 = Either.Left("Either error message");
            Assert.AreNotSame(eitherLeftStringInt1, eitherLeftString1);
            Assert.IsTrue(eitherLeftStringInt1.GetHashCode() == eitherLeftString1.GetHashCode());

            EitherRight<int> eitherRightIntTmp = Either.Right(12);
            Assert.AreNotSame(eitherLeftInt1, eitherRightIntTmp);
            Assert.IsTrue(eitherLeftInt1.GetHashCode() == eitherRightIntTmp.GetHashCode());

            Assert.AreNotSame(eitherRightStringInt1, eitherRightIntTmp);
            Assert.IsTrue(eitherRightStringInt1.GetHashCode() == eitherRightIntTmp.GetHashCode());


            // Different values
            // Either left
            Either<string, int> eitherLeftStringInt3 = Either.Left("Either error message 2");
            Assert.AreNotSame(eitherLeftStringInt1, eitherLeftStringInt3);
            Assert.IsFalse(eitherLeftStringInt1.GetHashCode() == eitherLeftStringInt3.GetHashCode());

            EitherLeft<int> eitherLeftInt3 = Either.Left(22);
            Assert.AreNotSame(eitherLeftInt1, eitherLeftInt3);
            Assert.IsFalse(eitherLeftInt1.GetHashCode() == eitherLeftInt3.GetHashCode());

            // Either right
            Either<string, int> eitherRightStringInt3 = Either.Right(52);
            Assert.AreNotSame(eitherRightStringInt1, eitherRightStringInt3);
            Assert.IsFalse(eitherRightStringInt1.GetHashCode() == eitherRightStringInt3.GetHashCode());

            EitherRight<int> eitherRightInt3 = Either.Right(62);
            Assert.AreNotSame(eitherRightInt1, eitherRightInt3);
            Assert.IsFalse(eitherRightInt1.GetHashCode() == eitherRightInt3.GetHashCode());

            // Mixed
            EitherLeft<string> eitherLeftString2 = Either.Left("Either error message 2");
            Assert.AreNotSame(eitherLeftStringInt1, eitherLeftString2);
            Assert.IsFalse(eitherLeftStringInt1.GetHashCode() == eitherLeftString2.GetHashCode());
            
            Assert.AreNotSame(eitherLeftInt1, eitherRightInt1);
            Assert.IsFalse(eitherLeftInt1.GetHashCode() == eitherRightInt1.GetHashCode());

            Assert.AreNotSame(eitherRightStringInt1, eitherRightInt1);
            Assert.IsFalse(eitherRightStringInt1.GetHashCode() == eitherRightInt1.GetHashCode());
        }

        [Test]
        public void EitherToString()
        {
            // Either left
            Either<string, int> eitherStringInt = Either.Left("Either error message");
            Assert.AreEqual($"Left(Either error message)", eitherStringInt.ToString());
            EitherLeft<int> eitherLeftInt = Either.Left(12);
            Assert.AreEqual($"Left(12)", eitherLeftInt.ToString());

            // Either right
            eitherStringInt = Either.Right(42);
            Assert.AreEqual($"Right(42)", eitherStringInt.ToString());
            EitherRight<int> eitherRightInt = Either.Right(12);
            Assert.AreEqual($"Right(12)", eitherRightInt.ToString());

            // Either none
            Assert.AreEqual("None", new Either<string, int>().ToString());
            Assert.AreEqual("None", Either<string, int>.None.ToString());
        }
    }
}