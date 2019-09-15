using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Here.Tests.Options
{
    /// <summary>
    /// Helpers to test <see cref="Option{T}"/>.
    /// </summary>
    internal class OptionTestHelpers
    {
        // Methods to check Option
        private const double Epsilon = 0.00001;

        public static void CheckOptionNearValue(Option<double> option, double value)
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            Assert.AreEqual(value, option.Value, Epsilon);
        }

        public static void CheckOptionValue<T>(Option<T> option, [NotNull] T value)
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            Assert.AreEqual(value, option.Value);
        }

        public static void CheckOptionSameValue<T>(Option<T> option, [NotNull] T value)
            where T : class
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            Assert.AreSame(value, option.Value);
        }

        public static void CheckOptionCollectionValue<T>(Option<T> option, [NotNull] IEnumerable value)
            where T : IEnumerable
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            CollectionAssert.AreEqual(value, option.Value);
        }

        public static void CheckOptionDictionaryValue<T, TKey, TValue>(Option<T> option, [NotNull] IDictionary<TKey, TValue> value)
            where T : IEnumerable
        {
            Assert.IsTrue(option.HasValue);
            Assert.IsFalse(option.HasNoValue);
            CollectionAssert.AreEquivalent(value, option.Value);
        }

        public static void CheckEmptyOption<T>(Option<T> option)
        {
            Assert.IsFalse(option.HasValue);
            Assert.IsTrue(option.HasNoValue);
            Assert.Throws<InvalidOperationException>(() => { var unused = option.Value; });
        }
    }
}