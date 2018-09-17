using System;

namespace Here.Tests
{
    /// <summary>
    /// Base class for Tests.
    /// </summary>
    internal class HereTestsBase
    {
        #region Test classes

        protected class PersonNotEquatable
        {
            public string Name { get; }

            public PersonNotEquatable(string name)
            {
                Name = name;
            }
        }

        protected class Person
        {
            private readonly string _name;

            public Person(string name)
            {
                _name = name;
            }

            public override bool Equals(object obj)
            {
                return obj is Person otherPerson
                    && _name.Equals(otherPerson._name, StringComparison.Ordinal);
            }

            public override int GetHashCode()
            {
                return _name.GetHashCode();
            }
        }

        protected class TestClass : IEquatable<TestClass>
        {
            public int TestInt { get; set; }

            public bool Equals(TestClass other)
            {
                if (other == null)
                    return false;
                return TestInt == other.TestInt;
            }

            public override string ToString()
            {
                return $"TestClass: {TestInt}";
            }
        }

        protected class TestClassLeaf : TestClass, IEquatable<TestClassLeaf>
        {
            public bool Equals(TestClassLeaf other)
            {
                if (other == null)
                    return false;
                return Equals(other);
            }

            public override string ToString()
            {
                return $"TestLeaf: {TestInt}";
            }
        }

        protected struct TestStruct
        {
        }

        #endregion
    }
}
