using System;
using JetBrains.Annotations;

namespace Here.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/>.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Indicates if this <see cref="Result"/> is a success without warning.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <returns>True if the result is a success without warning, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool IsOnlySuccess(in this Result result)
        {
            return result.IsSuccess && !result.IsWarning;
        }

        /// <summary>
        /// Throws this <see cref="Result"/> exception if it has one, otherwise do nothing.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        [PublicAPI]
        public static void Throws(in this Result result)
        {
            if (result.Exception is null)
                return;
            throw result.Exception;
        }

        /// <summary>
        /// Ensures that this <see cref="Result"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="result"><see cref="Result"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result Ensure(
            in this Result result,
            [NotNull, InstantHandle] in Func<bool> predicate,
            [NotNull] in string errorMessage)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage));

            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.Fail(errorMessage);

            return result;
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Indicates if this <see cref="Result{T}"/> is a success without warning.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <returns>True if the result is a success without warning, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool IsOnlySuccess<T>(in this Result<T> result)
        {
            return result.IsSuccess && !result.IsWarning;
        }

        /// <summary>
        /// Throws this <see cref="Result{T}"/> exception if it has one, otherwise do nothing.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        [PublicAPI]
        public static void Throws<T>(in this Result<T> result)
        {
            if (result.Exception is null)
                return;
            throw result.Exception;
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T}"/> value if it is a success, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to unwrap value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T}"/> if it has a value, otherwise the default value.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T>(in this Result<T> result, [CanBeNull] in T defaultValue = default)
        {
            if (result.IsSuccess)
                return result._value;
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T}"/> value if it is a success, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to unwrap value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public static T Unwrap<T>(in this Result<T> result, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            if (orFunc is null)
                throw new ArgumentNullException(nameof(orFunc));

            if (result.IsSuccess)
                return result._value;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T}"/> value if it is a success, 
        /// uses the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Result{T}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>(
            in this Result<T> result,
            [NotNull, InstantHandle] in Func<T, TOut> converter,
            [CanBeNull] in TOut defaultValue = default)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            if (result.IsSuccess)
                return converter(result._value);
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T}"/> value if it is a success, 
        /// uses the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Result{T}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TOut>(
            in this Result<T> result,
            [NotNull, InstantHandle] in Func<T, TOut> converter,
            [NotNull, InstantHandle] in Func<TOut> orFunc)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));
            if (orFunc is null)
                throw new ArgumentNullException(nameof(orFunc));

            if (result.IsSuccess)
                return converter(result._value);
            return orFunc();
        }

        /// <summary>
        /// Ensures that this <see cref="Result{T}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result<T> Ensure<T>(
            in this Result<T> result,
            [NotNull, InstantHandle] in Predicate<T> predicate,
            [NotNull] in string errorMessage)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage));

            if (result.IsFailure)
                return result;

            if (!predicate(result._value))
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
                Exception chosenException = embeddedResult._value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult._value.IsFailure)
                {
                    return embeddedResult._value.ToFailResult(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult._value.IsWarning)
                {
                    return embeddedResult._value.ToWarnResult(
                        $"{embeddedResult._value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult._value.ToWarnResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult._value;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
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
                Exception chosenException = embeddedResult._value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult._value.IsFailure)
                {
                    return embeddedResult._value.ToFailValueResult<T>(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult._value.IsWarning)
                {
                    return embeddedResult._value.ToWarnValueResult(
                        $"{embeddedResult._value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult._value.ToWarnValueResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult._value;
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Indicates if this <see cref="CustomResult{TError}"/> is a success without warning.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <returns>True if the result is a success without warning, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool IsOnlySuccess<TError>(in this CustomResult<TError> result)
        {
            return result.IsSuccess && !result.IsWarning;
        }

        /// <summary>
        /// Throws this <see cref="CustomResult{TError}"/> exception if it has one, otherwise do nothing.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        [PublicAPI]
        public static void Throws<TError>(in this CustomResult<TError> result)
        {
            if (result.Exception is null)
                return;
            throw result.Exception;
        }

        /// <summary>
        /// Ensures that this <see cref="CustomResult{TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorObject"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static CustomResult<TError> Ensure<TError>(
            in this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Func<bool> predicate, 
            [NotNull] in string errorMessage, 
            [NotNull] in TError errorObject)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage));
            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));

            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.CustomFail(errorMessage, errorObject);

            return result;
        }

        /// <summary>
        /// Ensures that this <see cref="CustomResult{TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/>.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorFactory"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static CustomResult<TError> Ensure<TError>(
            in this CustomResult<TError> result,
            [NotNull, InstantHandle] in Func<bool> predicate,
            [NotNull] in string errorMessage,
            [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage));
            if (errorFactory is null)
                throw new ArgumentNullException(nameof(errorFactory));

            if (result.IsFailure)
                return result;

            if (!predicate())
                return Result.CustomFail(errorMessage, errorFactory());

            return result;
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Indicates if this <see cref="Result{T, TError}"/> is a success without warning.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <returns>True if the result is a success without warning, otherwise false.</returns>
        [PublicAPI, Pure]
        public static bool IsOnlySuccess<T, TError>(in this Result<T, TError> result)
        {
            return result.IsSuccess && !result.IsWarning;
        }

        /// <summary>
        /// Throws this <see cref="Result{T, TError}"/> exception if it has one, otherwise do nothing.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        [PublicAPI]
        public static void Throws<T, TError>(in this Result<T, TError> result)
        {
            if (result.Exception is null)
                return;
            throw result.Exception;
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T, TError}"/> value if it is a success, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to unwrap value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T, TError}"/> if it has a value, otherwise the default value.</returns>
        [PublicAPI, Pure]
        public static T Unwrap<T, TError>(in this Result<T, TError> result, [CanBeNull] in T defaultValue = default)
        {
            if (result.IsSuccess)
                return result._value;
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T, TError}"/> value if it is a success, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to unwrap value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T, TError}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public static T Unwrap<T, TError>(in this Result<T, TError> result, [NotNull, InstantHandle] in Func<T> orFunc)
        {
            if (orFunc is null)
                throw new ArgumentNullException(nameof(orFunc));

            if (result.IsSuccess)
                return result._value;
            return orFunc();
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T, TError}"/> value if it is a success, 
        /// uses the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Result{T, TError}"/> value.</param>
        /// <param name="defaultValue">Default value to use.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T, TError}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TError, TOut>(
            in this Result<T, TError> result,
            [NotNull, InstantHandle] in Func<T, TOut> converter,
            [CanBeNull] in TOut defaultValue = default)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            if (result.IsSuccess)
                return converter(result._value);
            return defaultValue;
        }

        /// <summary>
        /// Unwraps this <see cref="Result{T, TError}"/> value if it is a success, 
        /// uses the <paramref name="converter"/> to convert the value, 
        /// otherwise returns the result from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Result{T, TError}"/> value.</param>
        /// <param name="orFunc">Default value factory method.</param>
        /// <returns>The unwrapped value from this <see cref="Result{T}"/> if it has a value, otherwise the default value.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="converter"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="orFunc"/> is null.</exception>
        [PublicAPI, Pure]
        public static TOut Unwrap<T, TError, TOut>(
            in this Result<T, TError> result,
            [NotNull, InstantHandle] in Func<T, TOut> converter,
            [NotNull, InstantHandle] in Func<TOut> orFunc)
        {
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));
            if (orFunc is null)
                throw new ArgumentNullException(nameof(orFunc));

            if (result.IsSuccess)
                return converter(result._value);
            return orFunc();
        }

        /// <summary>
        /// Ensures that this <see cref="Result{T, TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorObject"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result<T, TError> Ensure<T, TError>(
            in this Result<T, TError> result,
            [NotNull, InstantHandle] in Predicate<T> predicate,
            [NotNull] in string errorMessage,
            [NotNull] in TError errorObject)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage));
            if (errorObject == null)
                throw new ArgumentNullException(nameof(errorObject));

            if (result.IsFailure)
                return result;

            if (!predicate(result._value))
                return Result.Fail<T, TError>(errorMessage, errorObject);

            return result;
        }

        /// <summary>
        /// Ensures that this <see cref="Result{T, TError}"/> fulfill the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="predicate">Predicate to match.</param>
        /// <param name="errorMessage">The error message to use if the predicate is not fulfilled.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="predicate"/> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorMessage"/> is null or empty.</exception>
        /// <exception cref="T:System.ArgumentNullException">If the <paramref name="errorFactory"/> is null or empty.</exception>
        [PublicAPI, Pure]
        public static Result<T, TError> Ensure<T, TError>(
            in this Result<T, TError> result,
            [NotNull, InstantHandle] in Predicate<T> predicate,
            [NotNull] in string errorMessage,
            [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));
            if (errorMessage is null)
                throw new ArgumentNullException(nameof(errorMessage));
            if (errorFactory is null)
                throw new ArgumentNullException(nameof(errorFactory));

            if (result.IsFailure)
                return result;

            if (!predicate(result._value))
                return Result.Fail<T, TError>(errorMessage, errorFactory());

            return result;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result, TError}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
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
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
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
        /// <typeparam name="TError">Type of the result error object.</typeparam>
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
                Exception chosenException = embeddedResult._value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult._value.IsFailure)
                {
                    return embeddedResult._value.ToFailCustomResult(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult._value.IsWarning)
                {
                    return embeddedResult._value.ToWarnValueCustomResult(
                        $"{embeddedResult._value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult._value.ToWarnValueCustomResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult._value;
        }

        /// <summary>
        /// Flattens this <see cref="Result{Result, TError}"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the result value.</typeparam>
        /// <typeparam name="TError">Type of the result error object.</typeparam>
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
                Exception chosenException = embeddedResult._value.Exception ?? embeddedResult.Exception; // Keep deepest exception

                if (embeddedResult._value.IsFailure)
                {
                    return embeddedResult._value.ToFailValueCustomResult<T>(
                        $"{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                if (embeddedResult._value.IsWarning)
                {
                    return embeddedResult._value.ToWarnValueCustomResult(
                        $"{embeddedResult._value.Message}{Environment.NewLine}Resulting in: {embeddedResult.Message}",
                        chosenException);
                }

                return embeddedResult._value.ToWarnValueCustomResult(embeddedResult.Message, chosenException);
            }

            return embeddedResult._value;
        }

        #endregion
    }
}
