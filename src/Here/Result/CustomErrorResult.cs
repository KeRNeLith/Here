using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// <see cref="CustomResult{TError}"/> is an object that represents the result/state of a treatment.
    /// It provides a custom error object in case of failure.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(IsSuccess) + " ? \"IsSuccess\" + (" + nameof(IsWarning) + " ? \" with warning\" : System.String.Empty) : \"IsFailure\"}")]
    public partial struct CustomResult<TError> : IResultError<TError>, IEquatable<CustomResult<TError>>, IComparable, IComparable<CustomResult<TError>>
    {
        /// <summary>
        /// A success <see cref="CustomResult{TError}"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly ResultLogic<TError> Logic;

        /// <summary>
        /// <see cref="CustomResult{TError}"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal CustomResult([NotNull] in ResultLogic<TError> logic)
        {
            Logic = logic;
        }

        /// <summary>
        /// <see cref="Result"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error.</param>
        /// <param name="exception">Result embedded exception.</param>
        internal CustomResult(in bool isWarning, [NotNull] in string message, [CanBeNull] in TError error, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic<TError>(isWarning, message, error, exception);
        }

        #region Cast

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([CanBeNull] in T value)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(value, ResultLogic.ToResultLogic(Logic));
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([NotNull, InstantHandle] in Func<T> valueFactory)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(valueFactory(), ResultLogic.ToResultLogic(Logic));
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> CustomCast<T>([CanBeNull] in T value)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T>();
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Result.Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Result.Ok<T, TError>(value);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> CustomCast<T>([NotNull, InstantHandle] in Func<T> valueFactory)
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
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="Result"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T}"/>.</returns>
        [Pure]
        internal Result<T> ToFailValueResult<T>()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<T>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="additionalMessage">Message to add as suffix of this <see cref="CustomResult{TError}"/> message.</param>
        /// <param name="exception">Exception to set in the failure <see cref="CustomResult{TError}"/>.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult([NotNull] in string additionalMessage, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T, TError> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message + additionalMessage, Logic.Error, exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T, TError}"/>.
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
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailCustomValueResult<T>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a Result<T, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<T, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a warning <see cref="CustomResult{TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a Ok or warning.
        /// </summary>
        /// <param name="message">Message to set in the warning <see cref="CustomResult{TError}"/>.</param>
        /// <param name="exception">Exception to set in the warning <see cref="CustomResult{TError}"/>.</param>
        /// <returns>A warning <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToWarnCustomValueResult([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning CustomResult<TError> to a CustomResult<TError> warning.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomWarn<TError>(message, exception);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks that this <see cref="CustomResult{TError}"/> is equals to the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public bool SuccessEquals(in CustomResult<TError> other)
        {
            if (IsSuccess && other.IsSuccess)
                return Equals(other);
            return false;
        }

        /// <inheritdoc />
        public bool Equals(CustomResult<TError> other)
        {
            return Logic.Equals(other.Logic);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is CustomResult<TError> result && Equals(result);
        }

        /// <summary>
        /// Indicates whether both <see cref="CustomResult{TError}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in CustomResult<TError> result1, in CustomResult<TError> result2)
        {
            return result1.Equals(result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="CustomResult{TError}"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>True if both <see cref="CustomResult{TError}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in CustomResult<TError> result1, in CustomResult<TError> result2)
        {
            return !(result1 == result2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Logic.GetHashCode();
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is CustomResult<TError> other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(CustomResult<TError>)}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compare this <see cref="CustomResult{TError}"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        public int CompareTo(CustomResult<TError> other)
        {
            return Logic.CompareTo(other.Logic);
        }

        /// <summary>
        /// Determines if this <see cref="CustomResult{TError}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in CustomResult<TError> left, in CustomResult<TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="CustomResult{TError}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in CustomResult<TError> left, in CustomResult<TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="CustomResult{TError}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in CustomResult<TError> left, in CustomResult<TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="CustomResult{TError}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="CustomResult{TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="CustomResult{TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in CustomResult<TError> left, in CustomResult<TError> right)
        {
            return left.CompareTo(right) >= 0;
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
    /// or a custom error object in case of failed.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(IsSuccess) + " ? \"IsSuccess\" + (" + nameof(IsWarning) + " ? \" with warning\" : System.String.Empty) + \", Value = \" + " + nameof(Value) + " : \"IsFailure\"}")]
    public partial struct Result<T, TError> : IResult<T>, IResultError<TError>, IEquatable<Result<T, TError>>, IComparable, IComparable<Result<T, TError>>
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

        [CanBeNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly ResultLogic<TError> Logic;

        /// <summary>
        /// <see cref="Result{T, TError}"/> "Ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] in T value)
        {
            Logic = new ResultLogic<TError>();
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> "warning" constructor.
        /// </summary>
        /// <param name="value">Embedded value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        internal Result([CanBeNull] in T value, [NotNull] in string message, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic<TError>(true, message, default, exception);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> "failure" constructor.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        internal Result([NotNull] in string message, [NotNull] in TError error, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic<TError>(false, message, error, exception);
            _value = default;
        }

        /// <summary>
        /// <see cref="Result{T, TError}"/> constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="logic">Result logic.</param>
        internal Result([CanBeNull] in T value, [NotNull] in ResultLogic<TError> logic)
        {
            Logic = logic;
            _value = value;
        }

        #region Cast

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a <see cref="Result{TOut, TError}"/>
        /// </summary>
        /// <typeparam name="TOut">Type of the output result value.</typeparam>
        /// <param name="converter">Function that convert this result value from input type to output type.</param>
        /// <returns>A <see cref="Result{TOut, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<TOut, TError> Cast<TOut>([NotNull, InstantHandle] in Func<T, TOut> converter)
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
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T, TError> to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        [Pure]
        internal Result<TOut> ToFailValueResult<TOut>()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<TIn, TError> to a Result<TOut> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult()
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success Result<T, TError> to a CustomResult<TError> failure.");
            return this;
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T, TError> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
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
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailCustomValueResult<TOut>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<TIn, TError> to a Result<TOut, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a failure.
        /// </summary>
        /// <param name="additionalMessage">Message to add as suffix of this <see cref="Result{TOut, TError}"/> message.</param>
        /// <param name="exception">Exception to set in the failure <see cref="Result{TOut, TError}"/>.</param>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailCustomValueResult<TOut>([NotNull] in string additionalMessage, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success Result<TIn, TError> to a Result<TOut, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The error is always not null when here.
            return Result.Fail<TOut, TError>(Logic.Message + additionalMessage, Logic.Error, exception);
        }

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to a warning <see cref="Result{T, TError}"/>.
        /// This <see cref="Result{T, TError}"/> should be a Ok or warning.
        /// </summary>
        /// <param name="message">Message to set in the warning <see cref="Result{T, TError}"/>.</param>
        /// <param name="exception">Exception to set in the warning <see cref="Result{T, TError}"/>.</param>
        /// <returns>A warning <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToWarnCustomValueResult([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning Result<T, TError> to a Result<T, TError> warning.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Warn<T, TError>(Value, message, exception);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks that this <see cref="Result{T, TError}"/> is equals to the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public bool SuccessEquals(in Result<T, TError> other)
        {
            if (IsSuccess && other.IsSuccess)
                return Equals(other);
            return false;
        }

        /// <inheritdoc />
        public bool Equals(Result<T, TError> other)
        {
            return Logic.Equals(other.Logic)
                && EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is Result<T, TError> result && Equals(result);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T, TError}"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Result<T, TError> result1, in Result<T, TError> result2)
        {
            return result1.Equals(result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result{T, TError}"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>True if both <see cref="Result{T, TError}"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Result<T, TError> result1, in Result<T, TError> result2)
        {
            return !(result1 == result2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ Logic.GetHashCode();
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in Result<T, TError> result, in T value)
        {
            if (result.IsSuccess)
                return EqualityComparer<T>.Default.Equals(result.Value, value);
            return false;
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <param name="value">Value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in Result<T, TError> result, in T value)
        {
            return !(result == value);
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is equals to the given value, otherwise false.</returns>
        public static bool operator ==(in T value, in Result<T, TError> result)
        {
            return result == value;
        }

        /// <summary>
        /// Indicates whether <see cref="Result{T, TError}"/> value is not equals to the given value.
        /// </summary>
        /// <param name="value">Value to compare.</param>
        /// <param name="result"><see cref="Result{T, TError}"/> that may embed a value to compare.</param>
        /// <returns>True if the <see cref="Result{T, TError}"/> value is not equals to the given value, otherwise false.</returns>
        public static bool operator !=(in T value, in Result<T, TError> result)
        {
            return !(result == value);
        }

        #endregion

        #region IComparable / IComparable<T>

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj is null)
                return 1;
            if (obj is Result<T, TError> other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Result<T, TError>)}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compare this <see cref="Result{T, TError}"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        public int CompareTo(Result<T, TError> other)
        {
            int logicCompare = Logic.CompareTo(other.Logic);
            if (logicCompare == 0)
            {
                if (_value is IComparable comparable)
                    return comparable.CompareTo(other._value);
                else if (_value is IComparable<T> comparableT)
                    return comparableT.CompareTo(other._value);
            }

            return logicCompare;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result{T, TError}"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result{T, TError}"/> to compare.</param>
        /// <param name="right">The second <see cref="Result{T, TError}"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Result<T, TError> left, in Result<T, TError> right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }
    }
}