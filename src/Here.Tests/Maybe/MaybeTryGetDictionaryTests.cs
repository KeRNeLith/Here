using NUnit.Framework;
using System.Collections.Generic;
using Here.Maybes;
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
        public void TryGetDictionariesTKeyTValue()
        {
            // Dictionary
            var dictionaryStringString = new Dictionary<string, string>
            {
                ["2"] = "string 2",
                ["12"] = "string 12"
            };

            var maybeString = dictionaryStringString.TryGetValue("2");
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 2", maybeString.Value);

            maybeString = dictionaryStringString.TryGetValue("42");
            Assert.IsFalse(maybeString.HasValue);

            // Try get with a null key always return None result
            maybeString = dictionaryStringString.TryGetValue(null);
            Assert.IsFalse(maybeString.HasValue);

            // Readonly Dictionary
            IReadOnlyDictionary<string, string> readonlyDictionaryStringString = new Dictionary<string, string>
            {
                ["42"] = "string 42",
                ["88"] = "string 88"
            };
            
            maybeString = readonlyDictionaryStringString.TryGetReadonlyValue("42");
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 42", maybeString.Value);

            maybeString = readonlyDictionaryStringString.TryGetReadonlyValue("12");
            Assert.IsFalse(maybeString.HasValue);

            // Try get with a null key always return None result
            maybeString = readonlyDictionaryStringString.TryGetReadonlyValue(null);
            Assert.IsFalse(maybeString.HasValue);
        }

        [Test]
        public void TryGetDictionariesTKeyObject()
        {
            // Dictionary
            var dictionaryStringObject = new Dictionary<string, object>
            {
                ["2"] = "string 2",
                ["12"] = new TestClass { TestInt = 12 }
            };

            var maybeString = dictionaryStringObject.TryGetValue<string, string>("2");
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 2", maybeString.Value);

            var maybeClass = dictionaryStringObject.TryGetValue<string, TestClass>("2");
            Assert.IsFalse(maybeClass.HasValue);

            maybeString = dictionaryStringObject.TryGetValue<string, string>("42");
            Assert.IsFalse(maybeString.HasValue);

            // Try get with a null key always return None result
            maybeString = dictionaryStringObject.TryGetValue<string, string>(null);
            Assert.IsFalse(maybeString.HasValue);

            // Readonly Dictionary
            IReadOnlyDictionary<string, object> readonlyDictionaryStringObject = new Dictionary<string, object>
            {
                ["42"] = "string 42",
                ["88"] = new TestClass { TestInt = 88 }
            };

            maybeString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>("42");
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("string 42", maybeString.Value);

            maybeClass = readonlyDictionaryStringObject.TryGetReadonlyValue<string, TestClass>("42");
            Assert.IsFalse(maybeClass.HasValue);

            maybeString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>("12");
            Assert.IsFalse(maybeString.HasValue);

            // Try get with a null key always return None result
            maybeString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>(null);
            Assert.IsFalse(maybeString.HasValue);
        }
    }
}
