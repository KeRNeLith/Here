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
    /// Tests for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> asynchronous extensions (on any).
    /// </summary>
    [TestFixture]
    internal class ResultOnAnyAsyncExtensionsTests : ResultTestsBase
    {
        private static IEnumerable<TestCaseData> CreateResultOnAnyTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(Result.Ok());
                yield return new TestCaseData(Result.Warn("My warning"));
                yield return new TestCaseData(Result.Fail("My failure"));
            }
        }

        [TestCaseSource(nameof(CreateResultOnAnyTestCases))]
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result>)null).OnAnyAsync(r => Task.Run(() => {})));
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync((Func<Result, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result>)null).OnAnyAsync(() => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action<Result>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result>)null).OnAnyAsync(r => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result, int>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result>)null).OnAnyAsync(r => 12));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result>)null).OnAnyAsync(r => Task.FromResult(42)));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        private static IEnumerable<TestCaseData> CreateValueResultOnAnyTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(Result.Ok(12));
                yield return new TestCaseData(Result.Warn(42, "My warning"));
                yield return new TestCaseData(Result.Fail<int>("My failure"));
            }
        }

        [TestCaseSource(nameof(CreateValueResultOnAnyTestCases))]
        public async Task ValueResultOnAnyAsync<T>(Result<T> result)
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T>>)null).OnAnyAsync(r => Task.Run(() => { })));
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync((Func<Result<T>, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T>>)null).OnAnyAsync(() => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action<Result<T>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T>>)null).OnAnyAsync(r => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result<T>, int>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T>>)null).OnAnyAsync(r => 12));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result<T>, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T>>)null).OnAnyAsync(r => Task.FromResult(42)));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        private static IEnumerable<TestCaseData> CreateCustomResultOnAnyTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(Result.CustomOk<CustomErrorTest>());
                yield return new TestCaseData(Result.CustomWarn<CustomErrorTest>("My warning"));
                yield return new TestCaseData(Result.CustomFail("My failure", new CustomErrorTest()));
            }
        }

        [TestCaseSource(nameof(CreateCustomResultOnAnyTestCases))]
        public async Task CustomResultOnAnyAsync<TError>(CustomResult<TError> result)
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<CustomResult<TError>>)null).OnAnyAsync(r => Task.Run(() => { })));
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync((Func<CustomResult<TError>, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<CustomResult<TError>>)null).OnAnyAsync(() => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action<CustomResult<TError>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<CustomResult<TError>>)null).OnAnyAsync(r => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<CustomResult<TError>, int>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<CustomResult<TError>>)null).OnAnyAsync(r => 12));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<CustomResult<TError>, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<CustomResult<TError>>)null).OnAnyAsync(r => Task.FromResult(42)));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        private static IEnumerable<TestCaseData> CreateValueCustomResultOnAnyTestCases
        {
            [UsedImplicitly]
            get
            {
                yield return new TestCaseData(Result.Ok<int, CustomErrorTest>(12));
                yield return new TestCaseData(Result.Warn<int, CustomErrorTest>(42, "My warning"));
                yield return new TestCaseData(Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest()));
            }
        }

        [TestCaseSource(nameof(CreateValueCustomResultOnAnyTestCases))]
        public async Task ValueCustomResultOnAnyAsync<T, TError>(Result<T, TError> result)
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

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T, TError>>)null).OnAnyAsync(r => Task.Run(() => { })));
            Assert.ThrowsAsync<ArgumentNullException>(() => result.OnAnyAsync((Func<Result<T, TError>, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T, TError>>)null).OnAnyAsync(() => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Action<Result<T, TError>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T, TError>>)null).OnAnyAsync(r => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result<T, TError>, int>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T, TError>>)null).OnAnyAsync(r => 12));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnAnyAsync((Func<Result<T, TError>, Task<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<T, TError>>)null).OnAnyAsync(r => Task.FromResult(42)));
            // ReSharper restore AssignNullToNotNullAttribute
        }
    }
}

#endif