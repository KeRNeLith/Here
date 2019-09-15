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
    }
}