using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// <see cref="CustomResult{TError}"/> is an object that represents the result/state of a treatment with a custom error object.
    /// </summary>
    [PublicAPI]
    public partial struct CustomResult<TError> : IResultError<TError>, IEquatable<CustomResult<TError>>
    {
        /// <summary>
        /// A success <see cref="CustomResult{TError}"/>.
        /// </summary>
        internal static readonly CustomResult<TError> ResultOk = new CustomResult<TError>(new ResultLogic<TError>());

        /// <inheritdoc />
        public bool IsSuccess => Logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning => Logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => Logic.IsFailure;

        /// <inheritdoc />
        public string Message => Logic.Message;

        /// <inheritdoc />
        public Exception Exception => Logic.Exception;

        /// <inheritdoc />
        public TError Error => Logic.Error;

        [NotNull]
        internal readonly ResultLogic<TError> Logic;

        /// <summary>
        /// <see cref="CustomResult{TError}"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal CustomResult([NotNull] ResultLogic<TError> logic)
        {
            Logic = logic;
        }

        /// <summary>
        /// <see cref="Result"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error.</param>
        /// <param name="exception">Result embeded exception.</param>
        internal CustomResult(bool isWarning, [NotNull] string message, [CanBeNull] TError error, [CanBeNull] Exception exception)
        {
            Logic = new ResultLogic<TError>(isWarning, message, error, exception);
        }

        #region Cast

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        public Result<T> Cast<T>([CanBeNull] T value)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(value, ResultLogic.ToResultLogic(Logic));
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that create a value.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        public Result<T> Cast<T>([NotNull, InstantHandle] Func<T> valueFactory)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(valueFactory(), ResultLogic.ToResultLogic(Logic));
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        public Result<T, TError> CustomCast<T>([CanBeNull] T value)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T>();
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Result.Ok<T, TError>(value);
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that create a value.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        public Result<T, TError> CustomCast<T>([NotNull, InstantHandle] Func<T> valueFactory)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T>();
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.Warn<T, TError>(valueFactory(), Logic.Message, Logic.Exception);
            return Result.Ok<T, TError>(valueFactory());
        }

        #endregion

        #region Internal helpers

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a failure <see cref="Result"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success CustomResult<TError> to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T}"/>.</returns>
        [Pure]
        internal Result<T> ToFailValueResult<T>()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success CustomResult<TError> to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<T>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success CustomResult<TError> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailCustomValueResult<T>()
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success CustomResult<TError> to a Result<T, TError> failure.");
            // ReSharper disable AssignNullToNotNullAttribute, Justification The message and error is always not null or empty when here.
            return Result.Fail<T, TError>(Logic.Message, Logic.Error, Logic.Exception);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailCustomValueResult<T>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success CustomResult<TError> to a Result<T, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<T, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks that this <see cref="CustomResult{TError}"/> is equals tp the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are equals and successful.</returns>
        [Pure]
        public bool SuccessEquals(CustomResult<TError> other)
        {
            if (IsSuccess && other.IsSuccess)
                return Equals(other);
            return false;
        }

        public bool Equals(CustomResult<TError> other)
        {
            return Logic.Equals(other.Logic);
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            return other is CustomResult<TError> result && Equals(result);
        }

        public static bool operator ==(CustomResult<TError> result1, CustomResult<TError> result2)
        {
            return result1.Equals(result2);
        }

        public static bool operator !=(CustomResult<TError> result1, CustomResult<TError> result2)
        {
            return !(result1 == result2);
        }

        public override int GetHashCode()
        {
            return Logic.GetHashCode();
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }
    }

    /// <summary>
    /// <see cref="Result{T, TError}"/> is an object that represents the result/state of a treatment.
    /// This <see cref="Result{T, TError}"/> embed a <see cref="Value"/> resulting of the treatment
    /// or a custom error if failed.
    /// </summary>
    [PublicAPI]
    public partial struct Result<T, TError> : IResult<T>, IResultError<TError>, IEquatable<Result<T, TError>>
    {
        /// <inheritdoc />
        public bool IsSuccess => Logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning => Logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => Logic.IsFailure;

        /// <inheritdoc />
        public string Message => Logic.Message;

        /// <inheritdoc />
        public Exception Exception => Logic.Exception;

        /// <inheritdoc />
        public TError Error => Logic.Error;

        private readonly T _value;

        /// <inheritdoc />
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException("Cannot get the value of a failed Result.");

                return _value;
            }
        }

        [NotNull]
        internal readonly ResultLogic<TError> Logic;

        /// <summary>
        /// <see cref="Result{T, TError}"/> "ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] T value)
        {
            Logic = new ResultLogic<TError>();
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> "warning" constructor.
        /// </summary>
        /// <param name="value">Embedded value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        internal Result([CanBeNull] T value, [NotNull] string message, [CanBeNull] Exception exception)
        {
            Logic = new ResultLogic<TError>(true, message, default(TError), exception);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> "failure" constructor.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embeded exception.</param>
        internal Result([NotNull] string message, [NotNull] TError error, [CanBeNull] Exception exception)
        {
            Logic = new ResultLogic<TError>(false, message, error, exception);
            _value = default(T);
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="logic">Result logic.</param>
        internal Result([CanBeNull] T value, [NotNull] ResultLogic<TError> logic)
        {
            Logic = logic;
            _value = value;
        }

        #region Cast

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a <see cref="Result{TOut, TError}"/>
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="converter">Function that convert this result value from input type to output type.</param>
        /// <returns>A corresponding <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        public Result<TOut, TError> Cast<TOut>([NotNull, InstantHandle] Func<T, TOut> converter)
        {
            if (IsFailure)
                return ToFailCustomValueResult<TOut>();
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.Warn<TOut, TError>(converter(Value), Logic.Message, Logic.Exception);
            return Result.Ok<TOut, TError>(converter(Value));
        }

        #endregion

        #region Internal helpers

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<T, TError> to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        [Pure]
        internal Result<TOut> ToFailValueResult<TOut>()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<TIn, TError> to a Result<TOut> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut>(Logic.Message, Logic.Exception);
        }


        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<T, TError> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailCustomValueResult<TOut>()
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success Result<TIn, TError> to a Result<TOut, TError> failure.");
            // ReSharper disable AssignNullToNotNullAttribute, Justification The message and error is always not null or empty when here.
            return Result.Fail<TOut, TError>(Logic.Message, Logic.Error, Logic.Exception);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailCustomValueResult<TOut>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<TIn, TError> to a Result<TOut, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks that this <see cref="Result{T, TError}"/> is equals tp the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equals and successful.</returns>
        [Pure]
        public bool SuccessEquals(Result<T, TError> other)
        {
            if (IsSuccess && other.IsSuccess)
                return Equals(other);
            return false;
        }

        public bool Equals(Result<T, TError> other)
        {
            return Logic.Equals(other.Logic)
                && EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            return other is Result<T, TError> result && Equals(result);
        }

        public static bool operator ==(Result<T, TError> result1, Result<T, TError> result2)
        {
            return result1.Equals(result2);
        }

        public static bool operator !=(Result<T, TError> result1, Result<T, TError> result2)
        {
            return !(result1 == result2);
        }

        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ Logic.GetHashCode();
        }

        public static bool operator ==(Result<T, TError> result, T value)
        {
            if (result.IsSuccess)
                return EqualityComparer<T>.Default.Equals(result.Value, value);
            return false;
        }

        public static bool operator !=(Result<T, TError> result, T value)
        {
            return !(result == value);
        }

        public static bool operator ==(T value, Result<T, TError> result)
        {
            return result == value;
        }

        public static bool operator !=(T value, Result<T, TError> result)
        {
            return !(result == value);
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }
    }
}