#if SUPPORTS_ASYNC
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T}"/> asynchronous extensions (on failure).
    /// </summary>
    [TestFixture]
    internal class ValueResultOnFailureAsyncExtensionsTests : ResultTestsBase
    {
        [Test]
        public async Task ValueResultOnFailureAsync()
        {
            #region Local functions

            async Task CheckOnFailure<T>(Result<T> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                await result.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.Run(() => { /* Something */ });
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
            }

            async Task CheckOnFailureResult<T>(Result<T> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                string asyncRes = await result.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.FromResult("Test Async Failure 1");
                    },
                    "Default value 1",
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 1" : "Default value 1", asyncRes);

                asyncRes = await result.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.FromResult("Test Async Failure 2");
                    },
                    () => "Default value 2",
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 2 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 2" : "Default value 2", asyncRes);

                asyncRes = await result.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.FromResult("Test Async Failure 3");
                    },
                    () => Task.FromResult("Default value 3"),
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 3 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 3" : "Default value 3", asyncRes);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);
            await CheckOnFailureResult(ok, false, false);
            await CheckOnFailureResult(ok, true, false);

            // Warning result
            var warning = Result.Warn(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);
            await CheckOnFailureResult(warning, false, false);
            await CheckOnFailureResult(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);
            await CheckOnFailureResult(failure, false, true);
            await CheckOnFailureResult(failure, true, true);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(null, "Default"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(null, () => "Default"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(r => Task.FromResult("Never returned"), (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync((Func<Result<int>, Task<string>>)null, () => Task.FromResult("Default")));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(r => Task.FromResult("Never returned"), (Func<Task<string>>)null));
            // ReSharper disable once RedundantCast
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync((Func<Result<int>, Task<string>>)null, (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync((Func<Result<int>, Task<string>>)null, (Func<Task<string>>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public async Task TaskValueResultOnFailureAsyncTask()
        {
            #region Local function

            async Task CheckOnFailure<T>(Result<T> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                var taskRes = Task.FromResult(result);

                await taskRes.OnFailureAsync(
                    () => { ++counterFailure; },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);

                await taskRes.OnFailureAsync(
                    r => { ++counterFailure; },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 2 : 0, counterFailure);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);

            var taskResult = Task.FromResult(ok);
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(() => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Action<Result<int>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => { }));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public async Task TaskValueResultOnFailureAsyncValue()
        {
            #region Local functions

            async Task CheckOnFailure<T>(Result<T> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                var taskRes = Task.FromResult(result);
                string asyncRes = await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return "Test Async Failure 1";
                    },
                    "Default value 1",
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 1" : "Default value 1", asyncRes);

                asyncRes = await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return "Test Async Failure 2";
                    },
                    () => "Default value 2",
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 2 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 2" : "Default value 2", asyncRes);

                asyncRes = await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return "Test Async Failure 3";
                    },
                    () => Task.FromResult("Default value 3"),
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 3 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 3" : "Default value 3", asyncRes);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);

            var taskResult = Task.FromResult(ok);
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, string>)null, "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => "Never returned", "Never returned"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, string>)null, () => "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(r => "Never returned", (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, string>)null, (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => "Never returned", () => "Never returned"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, string>)null, () => Task.FromResult("Default value")));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(r => "Never returned", (Func<Task<string>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, string>)null, (Func<Task<string>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => "Never returned", () => Task.FromResult("Never returned")));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public async Task TaskValueResultOnFailureAsyncTaskValue()
        {
            #region Local functions

            async Task CheckOnFailure<T>(Result<T> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                var taskRes = Task.FromResult(result);
                await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.Run(() => { /* Something */ });
                    },
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
            }

            async Task CheckOnFailureResult<T>(Result<T> result, bool treatWarningAsError, bool expectFailure)
            {
                int counterFailure = 0;
                var taskRes = Task.FromResult(result);
                string asyncRes = await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.FromResult("Test Async Failure 1");
                    },
                    "Default value 1",
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 1 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 1" : "Default value 1", asyncRes);

                asyncRes = await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.FromResult("Test Async Failure 2");
                    },
                    () => "Default value 2",
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 2 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 2" : "Default value 2", asyncRes);

                asyncRes = await taskRes.OnFailureAsync(
                    r =>
                    {
                        ++counterFailure;
                        return Task.FromResult("Test Async Failure 3");
                    },
                    () => Task.FromResult("Default value 3"),
                    treatWarningAsError);
                Assert.AreEqual(expectFailure ? 3 : 0, counterFailure);
                Assert.AreEqual(expectFailure ? "Test Async Failure 3" : "Default value 3", asyncRes);
            }

            #endregion

            // Ok result
            var ok = Result.Ok(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);
            await CheckOnFailureResult(ok, false, false);
            await CheckOnFailureResult(ok, true, false);

            // Warning result
            var warning = Result.Warn(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);
            await CheckOnFailureResult(warning, false, false);
            await CheckOnFailureResult(warning, true, true);

            // Failure result
            var failure = Result.Fail<int>("My failure");
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);
            await CheckOnFailureResult(failure, false, true);
            await CheckOnFailureResult(failure, true, true);

            var taskResult = Task.FromResult(ok);
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => Task.Run(() => { })));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, Task<string>>)null, "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => Task.FromResult("Never returned"), "Never returned"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, Task<string>>)null, () => "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(r => Task.FromResult("Never returned"), (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, Task<string>>)null, (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => Task.FromResult("Never returned"), () => "Never returned"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, Task<string>>)null, () => Task.FromResult("Default value")));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(r => Task.FromResult("Never returned"), (Func<Task<string>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int>, Task<string>>)null, (Func<Task<string>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).OnFailureAsync(r => Task.FromResult("Never returned"), () => Task.FromResult("Never returned")));
            // ReSharper restore AssignNullToNotNullAttribute
        }
    }
}

#endif