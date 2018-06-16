using System;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Base class for Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    internal class MaybeTestsBase
    {
        #region Test classes

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

        protected struct TestStruct
        {
        }

        #endregion
    }
}
