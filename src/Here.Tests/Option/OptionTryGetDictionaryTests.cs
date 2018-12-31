using System;
using System.Collections.Generic;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Options
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> try get dictionary.
    /// </summary>
    [TestFixture]
    internal class OptionTryGetTests : OptionTestsBase
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

            var optionString = dictionaryStringString.TryGetValue("2");
            CheckOptionValue(optionString, "string 2");

            optionString = dictionaryStringString.TryGetValue("42");
            CheckEmptyOption(optionString);

            // Try get with a null key always return None result
            optionString = dictionaryStringString.TryGetValue(null);
            CheckEmptyOption(optionString);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((IDictionary<string, object>)null).TryGetValue("2"));

#if (!NET20 && !NET30 && !NET35 && !NET40)
            // Readonly Dictionary
            IReadOnlyDictionary<string, string> readonlyDictionaryStringString = new Dictionary<string, string>
            {
                ["42"] = "string 42",
                ["88"] = "string 88"
            };
            
            optionString = readonlyDictionaryStringString.TryGetReadonlyValue("42");
            CheckOptionValue(optionString, "string 42");

            optionString = readonlyDictionaryStringString.TryGetReadonlyValue("12");
            CheckEmptyOption(optionString);

            // Try get with a null key always return None result
            optionString = readonlyDictionaryStringString.TryGetReadonlyValue(null);
            CheckEmptyOption(optionString);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((IReadOnlyDictionary<string, object>)null).TryGetReadonlyValue("2"));
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

            var optionString = dictionaryStringObject.TryGetValue<string, string>("2");
            CheckOptionValue(optionString, "string 2");

            var optionClass = dictionaryStringObject.TryGetValue<string, TestClass>("2");
            CheckEmptyOption(optionClass);

            optionString = dictionaryStringObject.TryGetValue<string, string>("42");
            CheckEmptyOption(optionString);

            // Try get with a null key always return None result
            optionString = dictionaryStringObject.TryGetValue<string, string>(null);
            CheckEmptyOption(optionString);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((IDictionary<string, object>)null).TryGetValue<string, string>("2"));

#if (!NET20 && !NET30 && !NET35 && !NET40)
            // Readonly Dictionary
            IReadOnlyDictionary<string, object> readonlyDictionaryStringObject = new Dictionary<string, object>
            {
                ["42"] = "string 42",
                ["88"] = new TestClass { TestInt = 88 }
            };

            optionString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>("42");
            CheckOptionValue(optionString, "string 42");

            optionClass = readonlyDictionaryStringObject.TryGetReadonlyValue<string, TestClass>("42");
            CheckEmptyOption(optionClass);

            optionString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>("12");
            CheckEmptyOption(optionString);

            // Try get with a null key always return None result
            optionString = readonlyDictionaryStringObject.TryGetReadonlyValue<string, string>(null);
            CheckEmptyOption(optionString);

            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => ((IReadOnlyDictionary<string, object>)null).TryGetReadonlyValue<string, string>("2"));
#endif
        }
    }
}
