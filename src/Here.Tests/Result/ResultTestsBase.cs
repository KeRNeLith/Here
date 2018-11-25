using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Here.Tests.Results
{
    /// <summary>
    /// Base class for <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> tests.
    /// </summary>
    internal class ResultTestsBase : HereTestsBase
    {
        #region Test classes

        /// <summary>
        /// Test class for a custom error.
        /// </summary>
        protected class CustomErrorTest
        {
            public int ErrorCode { get; set; }
        }

        #endregion

        // Methods to check results

        #region Check Result

        protected static void CheckResultOk(Result result)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.IsNull(result.Exception);
        }

        protected static void CheckResultWarn(Result result, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        protected static void CheckResultFail(Result result, [NotNull] string expectedError, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        #endregion

        #region Check Result<T>

        protected static void CheckResultOk<T>(Result<T> result, [CanBeNull] T expectedValue)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.IsNull(result.Exception);
        }

        protected static void CheckResultWarn<T>(Result<T> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        protected static void CheckResultFail<T>(Result<T> result, [NotNull] string expectedError, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        #endregion

        #region Check CustomResult<TError>

        protected static void CheckResultOk<TError>(CustomResult<TError> result)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
            Assert.IsNull(result.Exception);
        }

        protected static void CheckResultWarn<TError>(CustomResult<TError> result, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        protected static void CheckResultFail<TError>(CustomResult<TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.AreSame(expectedErrorObject, result.Error);
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        #endregion

        #region Check Result<T, TError>

        protected static void CheckResultOk<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
            Assert.IsNull(result.Exception);
        }

        protected static void CheckResultWarn<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        protected static void CheckResultFail<T, TError>(Result<T, TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject, [CanBeNull] Exception expectedException = null)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
            Assert.AreSame(expectedErrorObject, result.Error);
            if (expectedException == null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        #endregion
    }
}