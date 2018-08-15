﻿using System;
using JetBrains.Annotations;

namespace Here.Results.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On failure).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result.IsFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] Action onFailure, bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailResult();
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result.IsFailure"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is <see cref="Result.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] Action<Result> onFailure, bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailResult();
            }

            return result;
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result{T}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is <see cref="Result{T}.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] Action onFailure, bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailValueResult<T>();
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result{T}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is <see cref="Result{T}.IsFailure"/>.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] Action<Result<T>> onFailure, bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailValueResult<T>();
            }

            return result;
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsFailure"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action onFailure,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsFailure"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] Action onFailure,
            [NotNull, InstantHandle] Func<TError> errorFactory, 
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorFactory());
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsFailure"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] Action<CustomResult<TError>> onFailure,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="CustomResult{TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is <see cref="CustomResult{TError}.IsFailure"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] Action<CustomResult<TError>> onFailure,
            [NotNull, InstantHandle] Func<TError> errorFactory, 
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorFactory());
            }

            return result;
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result{T, TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsFailure"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result, 
            [NotNull, InstantHandle] Action onFailure,
            [NotNull] TError errorObject, 
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result{T, TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsFailure"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action onFailure,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorFactory());
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result{T, TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsFailure"/>.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result, 
            [NotNull, InstantHandle] Action<Result<T, TError>> onFailure,
            [NotNull] TError errorObject,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Call the <paramref name="onFailure"/> action when the <paramref name="result"/> is <see cref="Result{T, TError}.IsFailure"/>.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is <see cref="Result{T, TError}.IsFailure"/>.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/> corresponding to this one.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] Action<Result<T, TError>> onFailure,
            [NotNull, InstantHandle] Func<TError> errorFactory,
            bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorFactory());
            }

            return result;
        }

        #endregion
    }
}