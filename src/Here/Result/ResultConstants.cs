﻿namespace Here.Results
{
    /// <summary>
    /// Constants used for results in the library.
    /// </summary>
    internal static class ResultConstants
    {
        public const string ResultBitwiseOrOperatorErrorMessage = "None of results is success, so the | operator is failure.";

        public const string ResultBitwiseAndOperatorErrorMessage = "At least one of results is failure, so the & operator is failure.";

        public const string ResultScopeErrorMessage = "An exception occurred while running a Result scope\n" 
                                                    + "Error is: {0}";

        public const string ValueResultScopeErrorMessage = "An exception occurred while running a Result<T> scope\n" 
                                                         + "Error is: {0}";

        public const string CustomResultScopeErrorMessage = "An exception occurred while running a CustomResult<TError> scope\n" 
                                                          + "Error is: {0}";

        public const string ValueCustomResultScopeErrorMessage = "An exception occurred while running a Result<T, TError> scope\n" 
                                                               + "Error is: {0}";
    }
}