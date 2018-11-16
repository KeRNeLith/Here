using System;

namespace Here.Results
{
    /// <summary>
    /// Constants used for results in the library.
    /// </summary>
    internal static class ResultConstants
    {
        internal static readonly string ResultScopeErrorMessage = "An exception occurred while running a Result scope" 
                                                                  + Environment.NewLine + "Error is: {0}";

        internal static readonly string ValueResultScopeErrorMessage = "An exception occurred while running a Result<T> scope" 
                                                                       + Environment.NewLine + "Error is: {0}";

        internal static readonly string CustomResultScopeErrorMessage = "An exception occurred while running a CustomResult<TError> scope" 
                                                                        + Environment.NewLine + "Error is: {0}";

        internal static readonly string ValueCustomResultScopeErrorMessage = "An exception occurred while running a Result<T, TError> scope" 
                                                                             + Environment.NewLine + "Error is: {0}";
    }
}