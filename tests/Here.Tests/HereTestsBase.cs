using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

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
            [UsedImplicitly]
            private string _name;

            public PersonNotEquatable(string name)
            {
                _name = name;
            }
        }

        protected class PersonComparable : IComparable<PersonComparable>
        {
            private readonly string _name;

            public PersonComparable(string name)
            {
                _name = name;
            }

            /// <inheritdoc />
            public int CompareTo(PersonComparable other)
            {
                return Comparer<string>.Default.Compare(_name, other._name);
            }
        }

        protected class Person
        {
            private readonly string _name;

            public Person(string name)
            {
                _name = name;
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                return obj is Person otherPerson
                    && _name.Equals(otherPerson._name, StringComparison.Ordinal);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return _name.GetHashCode();
            }
        }

        protected class TestClass : IEquatable<TestClass>
        {
            public int TestInt { get; set; }

            /// <inheritdoc />
            public bool Equals(TestClass other)
            {
                if (other is null)
                    return false;
                return TestInt == other.TestInt;
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                return Equals(obj as TestClass);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return RuntimeHelpers.GetHashCode(this);
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"TestClass: {TestInt}";
            }
        }

        protected class TestClassLeaf : TestClass, IEquatable<TestClassLeaf>
        {
            /// <inheritdoc />
            public bool Equals(TestClassLeaf other)
            {
                if (other is null)
                    return false;
                return base.Equals(other);
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                return Equals(obj as TestClassLeaf);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return RuntimeHelpers.GetHashCode(this);
            }

            /// <inheritdoc />
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