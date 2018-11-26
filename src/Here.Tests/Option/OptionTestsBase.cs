using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Here.Tests.Options
{
    /// <summary>
    /// Base class for<see cref="Option{T}"/> tests.
    /// </summary>
    internal class OptionTestsBase : HereTestsBase
    {
        // Methods to check Option
        private static double Epslion = 0.00001;

        protected static void CheckOptionNearValue(Option<double> option, double value)
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            Assert.AreEqual(value, option.Value, Epslion);
    }

        protected static void CheckOptionValue<T>(Option<T> option, [NotNull] T value)
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            Assert.AreEqual(value, option.Value);
        }

        protected static void CheckOptionSameValue<T>(Option<T> option, [NotNull] T value)
            where T : class
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            Assert.AreSame(value, option.Value);
        }

        protected static void CheckOptionCollectionValue<T>(Option<T> option, [NotNull] IEnumerable value)
            where T : IEnumerable
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            CollectionAssert.AreEqual(value, option.Value);
        }

        protected static void CheckOptionDictionaryValue<T, TKey, TValue>(Option<T> option, [NotNull] IDictionary<TKey, TValue> value)
            where T : IEnumerable
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            CollectionAssert.AreEquivalent(value, option.Value);
        }

        protected static void CheckEmptyOption<T>(Option<T> option)
        {
            Assert.IsFalse(option.HasValue);
            Assert.IsTrue(option.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = option.Value; });
        }
    }
}