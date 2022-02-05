#if SUPPORTS_ASYNC
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Here.Extensions;

namespace Here.Tests.Results
{
    /// <summary>
    /// Tests for <see cref="Result{T, TError}"/> asynchronous extensions (on failure).
    /// </summary>
    [TestFixture]
    internal class ValueCustomResultOnFailureAsyncExtensionsTests : ResultTestsBase
    {
        [Test]
        public async Task ValueCustomResultOnFailureAsync()
        {
            #region Local functions

            async Task CheckOnFailure<T, TError>(Result<T, TError> result, bool treatWarningAsError, bool expectFailure)
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

            async Task CheckOnFailureResult<T, TError>(Result<T, TError> result, bool treatWarningAsError, bool expectFailure)
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
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);
            await CheckOnFailureResult(ok, false, false);
            await CheckOnFailureResult(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);
            await CheckOnFailureResult(warning, false, false);
            await CheckOnFailureResult(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);
            await CheckOnFailureResult(failure, false, true);
            await CheckOnFailureResult(failure, true, true);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(null, "Default"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(null, () => "Default"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync(r => Task.FromResult("Never returned"), (Func<string>)null));
            // ReSharper disable once RedundantCast
            Assert.ThrowsAsync<ArgumentNullException>(() => ok.OnFailureAsync((Func<Result<int, CustomErrorTest>, Task<string>>)null, (Func<string>)null));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public async Task TaskValueCustomResultOnFailureAsyncTask()
        {
            #region Local function

            async Task CheckOnFailure<T, TError>(Result<T, TError> result, bool treatWarningAsError, bool expectFailure)
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
            var ok = Result.Ok<int, CustomErrorTest>(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);

            var taskResult = Task.FromResult(ok);
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Action)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(() => { }));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Action<Result<int, CustomErrorTest>>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(r => { }));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public async Task TaskValueCustomResultOnFailureAsyncValue()
        {
            #region Local functions

            async Task CheckOnFailure<T, TError>(Result<T, TError> result, bool treatWarningAsError, bool expectFailure)
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
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);

            var taskResult = Task.FromResult(ok);
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int, CustomErrorTest>, string>)null, "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(r => "Never returned", "Never returned"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int, CustomErrorTest>, string>)null, () => "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(r => "Never returned", (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int, CustomErrorTest>, string>)null, (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(r => "Never returned", () => "Never returned"));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public async Task TaskValueCustomResultOnFailureAsyncTaskValue()
        {
            #region Local functions

            async Task CheckOnFailure<T, TError>(Result<T, TError> result, bool treatWarningAsError, bool expectFailure)
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

            async Task CheckOnFailureResult<T, TError>(Result<T, TError> result, bool treatWarningAsError, bool expectFailure)
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
            }

            #endregion

            // Ok result
            var ok = Result.Ok<int, CustomErrorTest>(12);
            await CheckOnFailure(ok, false, false);
            await CheckOnFailure(ok, true, false);
            await CheckOnFailureResult(ok, false, false);
            await CheckOnFailureResult(ok, true, false);

            // Warning result
            var warning = Result.Warn<int, CustomErrorTest>(42, "My warning");
            await CheckOnFailure(warning, false, false);
            await CheckOnFailure(warning, true, true);
            await CheckOnFailureResult(warning, false, false);
            await CheckOnFailureResult(warning, true, true);

            // Failure result
            var failure = Result.Fail<int, CustomErrorTest>("My failure", new CustomErrorTest());
            await CheckOnFailure(failure, false, true);
            await CheckOnFailure(failure, true, true);
            await CheckOnFailureResult(failure, false, true);
            await CheckOnFailureResult(failure, true, true);

            var taskResult = Task.FromResult(ok);
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(r => Task.Run(() => { })));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int, CustomErrorTest>, Task<string>>)null, "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(r => Task.FromResult("Never returned"), "Never returned"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int, CustomErrorTest>, Task<string>>)null, () => "Default value"));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync(r => Task.FromResult("Never returned"), (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => taskResult.OnFailureAsync((Func<Result<int, CustomErrorTest>, Task<string>>)null, (Func<string>)null));
            Assert.ThrowsAsync<ArgumentNullException>(() => ((Task<Result<int, CustomErrorTest>>)null).OnFailureAsync(r => Task.FromResult("Never returned"), () => "Never returned"));
            // ReSharper restore AssignNullToNotNullAttribute
        }
    }
}

#endif