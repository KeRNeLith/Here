﻿using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region IResult

        /// <summary>
        /// Indicates if this <see cref="IResult"/> is a success without warning.
        /// </summary>
        /// <param name="result"><see cref="IResult"/> to check.</param>
        /// <returns>True if the result is a success without warning, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool IsOnlySuccess([NotNull] this IResult result)
        {
            return result.IsSuccess && !result.IsWarning;
        }

        #endregion

        #region IResult<T>

        /// <summary>
        /// Unwraps this <see cref="IResult{T}"/> value if it is a success, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has a value, otherwise the default value.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T>([NotNull]this IResult<T> result, [CanBeNull] in T defaultValue = default)
        {
            if (result.IsSuccess)
                return result.Value;
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="IResult{T}"/> value if it is a success, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public static T Unwrap<T>([NotNull] this IResult<T> result, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            Throw.IfArgumentNull(orFunc, nameof(orFunc));

            if (result.IsSuccess)
                return result.Value;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="IResult{T}"/> value if it is a success, 
        /// uses the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="IResult{T}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>([NotNull] this IResult<T> result,
            [NotNull, InstantHandle] in Func<T, TOut> converter,
            [CanBeNull] in TOut defaultValue = default)
        {
            Throw.IfArgumentNull(converter, nameof(converter));

            if (result.IsSuccess)
                return converter(result.Value);
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="IResult{T}"/> value if it is a success, 
        /// uses the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="IResult{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="IResult{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="IResult{T}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="IResult{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>([NotNull] this IResult<T> result,
            [NotNull, InstantHandle] in Func<T, TOut> converter,
            [NotNull, InstantHandle] in Func<TOut> orFunc)
        {
            Throw.IfArgumentNull(converter, nameof(converter));
            Throw.IfArgumentNull(orFunc, nameof(orFunc));

            if (result.IsSuccess)
                return converter(result.Value);
            return orFunc();
        }

        #endregion

        #region Result

        /// <summary>
        /// Ensures that this <see cref="Result"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result Ensure(in this Result result, [NotNull, InstantHandle] in Func<bool> predicate, [NotNull] in string errorMessage)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(errorMessage, nameof(errorMessage));

            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.Fail(errorMessage);

            return result;
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Ensures that this <see cref="Result{T}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result<T> Ensure<T>(in this Result<T> result, [NotNull, InstantHandle] in Predicate<T> predicate, [NotNull] in string errorMessage)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(errorMessage, nameof(errorMessage));

            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T>(errorMessage);

            return result;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="embeddedResult">A <see cref="Result{Result}"/>.</param>
        /// <returns>Flattened <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result Flatten(in this Result<Result> embeddedResult)
        {
            if (embeddedResult.IsFailure)
            {
                return embeddedResult.ToFailResult();
            }

            if (embeddedResult.IsWarning)
            {
                Exception chosenException = embeddedResult.Value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult.Value.IsFailure)
                {
                    return embeddedResult.Value.ToFailResult(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult.Value.IsWarning)
                {
                    return embeddedResult.Value.ToWarnResult(
                        $"{embeddedResult.Value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult.Value.ToWarnResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult.Value;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="embeddedResult">A <see cref="Result{Result}"/>.</param>
        /// <returns>Flattened <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> Flatten<T>(in this Result<Result<T>> embeddedResult)
        {
            if (embeddedResult.IsFailure)
            {
                return embeddedResult.ToFailValueResult<T>();
            }

            if (embeddedResult.IsWarning)
            {
                Exception chosenException = embeddedResult.Value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult.Value.IsFailure)
                {
                    return embeddedResult.Value.ToFailValueResult<T>(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult.Value.IsWarning)
                {
                    return embeddedResult.Value.ToWarnValueResult(
                        $"{embeddedResult.Value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult.Value.ToWarnValueResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult.Value;
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Ensures that this <see cref="CustomResult{TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static CustomResult<TError> Ensure<TError>(in this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Func<bool> predicate, 
            [NotNull] in string errorMessage, 
            [NotNull] in TError errorObject)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(errorMessage, nameof(errorMessage));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.CustomFail(errorMessage, errorObject);

            return result;
        }

        /// <summary>
        /// Ensures that this <see cref="CustomResult{TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static CustomResult<TError> Ensure<TError>(in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<bool> predicate,
            [NotNull] in string errorMessage,
            [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(errorMessage, nameof(errorMessage));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.CustomFail(errorMessage, errorFactory());

            return result;
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Ensures that this <see cref="Result{T, TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result<T, TError> Ensure<T, TError>(in this Result<T, TError> result,
            [NotNull, InstantHandle] in Predicate<T> predicate,
            [NotNull] in string errorMessage,
            [NotNull] in TError errorObject)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(errorMessage, nameof(errorMessage));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T, TError>(errorMessage, errorObject);

            return result;
        }

        /// <summary>
        /// Ensures that this <see cref="Result{T, TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Error type of the result.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result<T, TError> Ensure<T, TError>(in this Result<T, TError> result,
            [NotNull, InstantHandle] in Predicate<T> predicate,
            [NotNull] in string errorMessage,
            [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(predicate, nameof(predicate));
            Throw.IfArgumentNull(errorMessage, nameof(errorMessage));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result.Fail<T, TError>(errorMessage, errorFactory());

            return result;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result, TError}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="embeddedResult">A <see cref="Result{Result, TError}"/>.</param>
        /// <returns>Flattened <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result Flatten<TError>(in this Result<Result, TError> embeddedResult)
        {
            Result<Result> tmpResult = embeddedResult;
            return tmpResult.Flatten();
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result, TError}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="embeddedResult">A <see cref="Result{Result, TError}"/>.</param>
        /// <returns>Flattened <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> Flatten<T, TError>(in this Result<Result<T>, TError> embeddedResult)
        {
            Result<Result<T>> tmpResult = embeddedResult;
            return tmpResult.Flatten();
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result, TError}"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="embeddedResult">A <see cref="Result{Result, TError}"/>.</param>
        /// <returns>Flattened <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public static CustomResult<TError> Flatten<TError>(in this Result<CustomResult<TError>, TError> embeddedResult)
        {
            if (embeddedResult.IsFailure)
            {
                return embeddedResult.ToFailCustomResult();
            }

            if (embeddedResult.IsWarning)
            {
                Exception chosenException = embeddedResult.Value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult.Value.IsFailure)
                {
                    return embeddedResult.Value.ToFailCustomResult(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult.Value.IsWarning)
                {
                    return embeddedResult.Value.ToWarnValueCustomResult(
                        $"{embeddedResult.Value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult.Value.ToWarnValueCustomResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult.Value;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result, TError}"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="embeddedResult">A <see cref="Result{Result, TError}"/>.</param>
        /// <returns>Flattened <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T, TError> Flatten<T, TError>(in this Result<Result<T, TError>, TError> embeddedResult)
        {
            if (embeddedResult.IsFailure)
            {
                return embeddedResult.ToFailValueCustomResult<T>();
            }

            if (embeddedResult.IsWarning)
            {
                Exception chosenException = embeddedResult.Value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult.Value.IsFailure)
                {
                    return embeddedResult.Value.ToFailValueCustomResult<T>(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult.Value.IsWarning)
                {
                    return embeddedResult.Value.ToWarnValueCustomResult(
                        $"{embeddedResult.Value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult.Value.ToWarnValueCustomResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult.Value;
        }

        #endregion
    }
}
