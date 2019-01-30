#if SUPPORTS_ASYNC
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result"/> asynchronous extensions.
    /// </summary>
    [TestFixture]
    internal class ResultOnAnyAsyncExtensionsTests : ResultTestsBase
    {
        private static IEnumerable<TestCaseData> CreateOnAnyTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(Result.Ok());
                yield return new TestCaseData(Result.Warn("My warning"));
                yield return new TestCaseData(Result.Fail("My failure"));
            }
        }

        [TestCaseSource(nameof(CreateOnAnyTestCases))]
        public async Task ResultOnAnyAsync(Result result)
        {
            int counter = 0;

            await result.OnAnyAsync(
                res =>
                {
                    ++counter;
                    return Task.Run(() => { /* Something */ });
                });
            Assert.AreEqual(1, counter);

            string asyncRes = await result.OnAnyAsync(
                res =>
                {
                    ++counter;
                    return Task.FromResult("Test Async 0");
                });
            Assert.AreEqual(2, counter);
            Assert.AreEqual("Test Async 0", asyncRes);

            var taskResult = Task.FromResult(result);

            await taskResult.OnAnyAsync(
                () =>
                {
                    ++counter;
                });
            Assert.AreEqual(3, counter);

            await taskResult.OnAnyAsync(
                res =>
                {
                    ++counter;
                });
            Assert.AreEqual(4, counter);

            asyncRes = await taskResult.OnAnyAsync(
                res =>
                {
                    ++counter;
                    return "Test Async 1";
                });
            Assert.AreEqual(5, counter);
            Assert.AreEqual("Test Async 1", asyncRes);

            await taskResult.OnAnyAsync(
                res =>
                {
                    ++counter;
                    return Task.Run(() => { /* Something */ });
                });
            Assert.AreEqual(6, counter);

            asyncRes = await taskResult.OnAnyAsync(
                res =>
                {
                    ++counter;
                    return Task.FromResult("Test Async 2");
                });
            Assert.AreEqual(7, counter);
            Assert.AreEqual("Test Async 2", asyncRes);

            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync((Func<Result, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action<Result>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result, int>)null));
        }
    }
}

#endif