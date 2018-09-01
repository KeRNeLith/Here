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
    internal class MaybeTryGetTests : MaybeTestsBase
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
            CheckMaybeValue(maybeString, "string 2");

            maybeString = dictionaryStringString.TryGetValue("42");
            CheckEmptyMaybe(maybeString);

            // Try get with a null key always return None result
            maybeString = dictionaryStringString.TryGetValue(null);
            CheckEmptyMaybe(maybeString);

#if NET45 && NET451 && NET452 && NET46 && NET461 && NET462 && NETSTANDARD1_1 && NETCOREAPP1_0 && NETCOREAPP2_0
            // Readonly Dictionary
            IReadOnlyDictionary<string, string> readonlyDictionaryStringString = new Dictionary<string, string>
            {
                ["42"] = "string 42",
                ["88"] = "string 88"
            };
            
            maybeString = readonlyDictionaryStringString.TryGetReadonlyValue("42");
            CheckMaybeValue(maybeString, "string 42");

            maybeString = readonlyDictionaryStringString.TryGetReadonlyValue("12");
            CheckEmptyMaybe(maybeString);

            // Try get with a null key always return None result
            maybeString = readonlyDictionaryStringString.TryGetReadonlyValue(null);
            CheckEmptyMaybe(maybeString);
#endif
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
            CheckMaybeValue(maybeString, "string 2");

            var maybeClass = dictionaryStringObject.TryGetValue<string, TestClass>("2");
            CheckEmptyMaybe(maybeClass);

            maybeString = dictionaryStringObject.TryGetValue<string, string>("42");
            CheckEmptyMaybe(maybeString);

            // Try get with a null key always return None result
            maybeString = dictionaryStringObject.TryGetValue<string, string>(null);
            CheckEmptyMaybe(maybeString);

#if NET45 && NET451 && NET452 && NET46 && NET461 && NET462 && NETSTANDARD1_1 && NETCOREAPP1_0 && NETCOREAPP2_0
            // Readonly Dictionary
            IReadOnlyDictionary<string, object> readonlyDictionaryStringObject = new Dictionary<string, object>
            {
                ["42"] = "string 42",
                ["88"] = new TestClass { TestInt = 88 }
            };

            maybeString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>("42");
            CheckMaybeValue(maybeString, "string 42");

            maybeClass = readonlyDictionaryStringObject.TryGetReadonlyValue<string, TestClass>("42");
            CheckEmptyMaybe(maybeClass);

            maybeString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>("12");
            CheckEmptyMaybe(maybeString);

            // Try get with a null key always return None result
            maybeString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>(null);
            CheckEmptyMaybe(maybeString);
#endif
        }
    }
}
