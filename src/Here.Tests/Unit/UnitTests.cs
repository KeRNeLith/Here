using System;
using NUnit.Framework;

namespace Here.Tests.Units
{
    /// <summary>
    /// Tests for <see cref="Unit"/>.
    /// </summary>
    [TestFixture]
    internal class UnitTests : HereTestsBase
    {
        [Test]
        public void UnitReturn()
        {
            Assert.AreEqual(12, Unit.Default.Return(12));
            Assert.AreEqual(null, Unit.Default.Return((TestClass)null));
            Assert.AreEqual(12.5f, Unit.Default.Return(() => 12.5f));
            Assert.AreEqual(null, Unit.Default.Return(() => (TestClass)null));
        }

        [Test]
        public void UnitEquality()
        {
            var unit1 = Unit.Default;
            var unit2 = Unit.Default;
            var unit3 = new Unit();

            Assert.IsTrue(unit1.Equals(unit1));

            Assert.IsTrue(unit1.Equals(unit2));
            Assert.IsTrue(unit1.Equals((object)unit2));
            Assert.IsTrue(unit2.Equals(unit1));
            Assert.IsTrue(unit2.Equals((object)unit1));
            Assert.IsTrue(unit1 == unit2);
            Assert.IsTrue(unit2 == unit1);
            Assert.IsFalse(unit1 != unit2);
            Assert.IsFalse(unit2 != unit1);

            Assert.IsTrue(unit1.Equals(unit3));
            Assert.IsTrue(unit1.Equals((object)unit3));
            Assert.IsTrue(unit3.Equals(unit1));
            Assert.IsTrue(unit3.Equals(unit1));
            Assert.IsTrue(unit3.Equals((object)unit1));
            Assert.IsTrue(unit1 == unit3);
            Assert.IsTrue(unit3 == unit1);
            Assert.IsFalse(unit1 != unit3);
            Assert.IsFalse(unit3 != unit1);

            // Mixed
            Assert.IsFalse(unit1.Equals(new TestClass()));
        }

        [Test]
        public void UnitCompare()
        {
            var unit1 = Unit.Default;
            var unit2 = Unit.Default;
            var unit3 = new Unit();

            Assert.AreEqual(0, unit1.CompareTo(unit1));
            Assert.AreEqual(0, unit1.CompareTo((object)unit1));

            Assert.AreEqual(0, unit1.CompareTo(unit2));
            Assert.AreEqual(0, unit1.CompareTo((object)unit2));
            Assert.AreEqual(0, unit2.CompareTo(unit1));
            Assert.AreEqual(0, unit2.CompareTo((object)unit2));
            Assert.IsFalse(unit1 < unit2);
            Assert.IsTrue(unit1 <= unit2);
            Assert.IsFalse(unit1 > unit2);
            Assert.IsTrue(unit1 >= unit2);

            Assert.AreEqual(0, unit1.CompareTo(unit2));
            Assert.AreEqual(0, unit1.CompareTo((object)unit3));
            Assert.AreEqual(0, unit3.CompareTo(unit1));
            Assert.AreEqual(0, unit3.CompareTo((object)unit3));
            Assert.IsFalse(unit1 < unit3);
            Assert.IsTrue(unit1 <= unit3);
            Assert.IsFalse(unit1 > unit3);
            Assert.IsTrue(unit1 >= unit3);

            // Mixed
            Assert.AreEqual(1, unit1.CompareTo(null));

            var testObject = new TestClass();
            Assert.Throws<ArgumentException>(() => { var _ = unit1.CompareTo(new TestClass()); });
        }

        [Test]
        public void UnitHashCode()
        {
            var unit1 = Unit.Default;
            var unit2 = Unit.Default;
            var unit3 = new Unit();

            Assert.AreNotSame(unit1, unit2);
            Assert.IsTrue(unit1.Equals(unit2));
            Assert.IsTrue(unit2.Equals(unit1));
            Assert.AreEqual(0, unit1.GetHashCode());
            Assert.IsTrue(unit1.GetHashCode() == unit2.GetHashCode());

            Assert.AreNotSame(unit1, unit3);
            Assert.IsTrue(unit1.Equals(unit3));
            Assert.IsTrue(unit3.Equals(unit1));
            Assert.IsTrue(unit1.GetHashCode() == unit3.GetHashCode());
        }

        [Test]
        public void UnitToString()
        {
            Assert.AreEqual("()", Unit.Default.ToString());
            Assert.AreEqual("()", new Unit().ToString());
        }
    }
}