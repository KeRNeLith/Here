using NUnit.Framework;
using System.Collections.Generic;
using Here.Maybes.Extensions;

namespace Here.Tests.Maybes
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/> try get dictionary.
    /// </summary>
    [TestFixture]
    internal class MaybeTryGetTests : HereTestsBase
    {
        [Test]
        public void TryGetDictionaries()
        {
            // Dictionary
            var dictionaryIntString = new Dictionary<int, string>
            {
                [2] = "string 2",
                [12] = "string 12",
            };

            var maybeString = dictionaryIntString.TryGetValue(2);
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 2", maybeString.Value);

            maybeString = dictionaryIntString.TryGetValue(42);
            Assert.IsFalse(maybeString.HasValue);
            
            // Readonly Dictionary
            IReadOnlyDictionary<int, string> readonlyDictionaryIntString = new Dictionary<int, string>
            {
                [42] = "string 42",
                [88] = "string 88",
            };
            
            maybeString = readonlyDictionaryIntString.TryGetReadonlyValue(42);
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 42", maybeString.Value);

            maybeString = readonlyDictionaryIntString.TryGetReadonlyValue(12);
            Assert.IsFalse(maybeString.HasValue);
        }
    }
}
