using System;
using JetBrains.Annotations;
using NUnit.Framework;
using Here.Results;

namespace Here.Tests.Results
{
    /// <summary>
    /// Base class for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> tests.
    /// </summary>
    internal class ResultTestsBase : HereTestsBase
    {
        // Methods to check results

        #region Check Result

        protected void CheckResultOK(Result result)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
        }

        protected void CheckResultWarn(Result result, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        protected void CheckResultFail(Result result, [NotNull] string expectedError)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
        }

        #endregion

        #region Check Result<T>

        protected void CheckResultOK<T>(Result<T> result, [CanBeNull] T expectedValue)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.AreEqual(expectedValue, result.Value);
        }

        protected void CheckResultWarn<T>(Result<T> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedValue, result.Value);
        }

        protected void CheckResultFail<T>(Result<T> result, [NotNull] string expectedError)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
        }

        #endregion

        #region Check CustomResult<TError>

        protected void CheckResultOK<TError>(CustomResult<TError> result)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
        }

        protected void CheckResultWarn<TError>(CustomResult<TError> result, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
        }

        protected void CheckResultFail<TError>(CustomResult<TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.AreSame(expectedErrorObject, result.Error);
        }

        #endregion

        #region Check Result<T, TError>

        protected void CheckResultOK<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
        }

        protected void CheckResultWarn<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
        }

        protected void CheckResultFail<T, TError>(Result<T, TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
            Assert.AreSame(expectedErrorObject, result.Error);
        }

        #endregion
    }
}