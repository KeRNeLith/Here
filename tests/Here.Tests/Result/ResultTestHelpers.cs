using System;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Here.Tests.Results
{
    /// <summary>
    /// Helpers to test <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    internal class ResultTestHelpers
    {
        // Methods to check results

        private static void CheckResult_Exception([NotNull] IResult result, [CanBeNull] Exception expectedException)
        {
            if (expectedException is null)
                Assert.IsNull(result.Exception);
            else
                Assert.AreSame(expectedException, result.Exception);
        }

        private static void CheckResult_ExceptionEqual([NotNull] IResult result, [CanBeNull] Exception expectedException)
        {
            if (expectedException is null)
            {
                Assert.IsNull(result.Exception);
            }
            else
            {
                Assert.IsInstanceOf(expectedException.GetType(), result.Exception);
                Assert.AreEqual(expectedException.Message, result.Exception?.Message);
            }
        }

        #region Check Result

        public static void CheckResultOk(Result result)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.IsNull(result.Exception);
        }

        private static void CheckResultWarnInternal(Result result, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        public static void CheckResultWarn(Result result, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedMessage);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultWarn_ExceptionEqual(Result result, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedMessage);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        private static void CheckResultFailInternal(Result result, [NotNull] string expectedError)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
        }

        public static void CheckResultFail(Result result, [NotNull] string expectedError, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultFail_ExceptionEqual(Result result, [NotNull] string expectedError, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        #endregion

        #region Check Result<T>

        public static void CheckResultOk<T>(Result<T> result, [CanBeNull] T expectedValue)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.IsNull(result.Exception);
        }

        private static void CheckResultWarnInternal<T>(Result<T> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedValue, result.Value);
        }

        public static void CheckResultWarn<T>(Result<T> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedValue, expectedMessage);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultWarn_ExceptionEqual<T>(Result<T> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedValue, expectedMessage);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        private static void CheckResultFailInternal<T>(Result<T> result, [NotNull] string expectedError)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
        }

        public static void CheckResultFail<T>(Result<T> result, [NotNull] string expectedError, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultFail_ExceptionEqual<T>(Result<T> result, [NotNull] string expectedError, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        #endregion

        #region Check CustomResult<TError>

        public static void CheckResultOk<TError>(CustomResult<TError> result)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
            Assert.IsNull(result.Exception);
        }

        private static void CheckResultWarnInternal<TError>(CustomResult<TError> result, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
        }

        public static void CheckResultWarn<TError>(CustomResult<TError> result, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedMessage);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultWarn_ExceptionEqual<TError>(CustomResult<TError> result, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedMessage);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        private static void CheckResultFailInternal<TError>(CustomResult<TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
        }

        public static void CheckResultFail<TError>(CustomResult<TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError, expectedErrorObject);
            Assert.AreSame(expectedErrorObject, result.Error);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultFail_Equal<TError>(CustomResult<TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError, expectedErrorObject);
            Assert.AreEqual(expectedErrorObject, result.Error);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        #endregion

        #region Check Result<T, TError>

        public static void CheckResultOk<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.IsNull(result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
            Assert.IsNull(result.Exception);
        }

        private static void CheckResultWarnInternal<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage)
        {
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.IsWarning);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedValue, result.Value);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Error; });
        }

        public static void CheckResultWarn<T, TError>(Result<T, TError> result, [CanBeNull] T expectedValue, [NotNull] string expectedMessage, [CanBeNull] Exception expectedException = null)
        {
            CheckResultWarnInternal(result, expectedValue, expectedMessage);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultFailInternal<T, TError>(Result<T, TError> result, [NotNull] string expectedError)
        {
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.IsWarning);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(expectedError, result.Message);
            Assert.Throws<InvalidOperationException>(() => { var _ = result.Value; });
        }

        public static void CheckResultFail<T, TError>(Result<T, TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError);
            Assert.AreSame(expectedErrorObject, result.Error);
            CheckResult_Exception(result, expectedException);
        }

        public static void CheckResultFail_Equal<T, TError>(Result<T, TError> result, [NotNull] string expectedError, [NotNull] TError expectedErrorObject, [CanBeNull] Exception expectedException = null)
        {
            CheckResultFailInternal(result, expectedError);
            Assert.AreEqual(expectedErrorObject, result.Error);
            CheckResult_ExceptionEqual(result, expectedException);
        }

        #endregion
    }
}