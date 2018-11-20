using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// <see cref="Result"/> is an object that represents the result/state of a treatment.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("{" + nameof(IsSuccess) + " ? \"IsSuccess\" + (" + nameof(IsWarning) + " ? \" with warning\" : System.String.Empty) : \"IsFailure\"}")]
    public partial struct Result : IResult, IEquatable<Result>, IComparable, IComparable<Result>
    {
        /// <summary>
        /// A success <see cref="Result"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Result ResultOk = new Result(new ResultLogic());

        /// <inheritdoc />
        public bool IsSuccess => Logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning=> Logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => Logic.IsFailure;

        /// <inheritdoc />
        public string Message => Logic.Message;

        /// <inheritdoc />
        public Exception Exception => Logic.Exception;

        [NotNull]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly ResultLogic Logic;

        /// <summary>
        /// <see cref="Result"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal Result([NotNull] in ResultLogic logic)
        {
            Logic = logic;
        }

        /// <summary>
        /// <see cref="Result"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        private Result(in bool isWarning, [NotNull] in string message, [CanBeNull] in Exception exception)
        {
            Logic = new ResultLogic(isWarning, message, exception);
        }

        #region Cast

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([CanBeNull] in T value)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(value, Logic);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([NotNull, InstantHandle] in Func<T> valueFactory)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(valueFactory(), Logic);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="CustomResult{TError}"/>
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull] in TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomResult(errorObject);
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return CustomWarn<TError>(Logic.Message, Logic.Exception);
            return CustomOk<TError>();
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="CustomResult{TError}"/>
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomResult(errorFactory());
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return CustomWarn<TError>(Logic.Message, Logic.Exception);
            return CustomOk<TError>();
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([CanBeNull] in T value, [NotNull] in TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorObject);
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Ok<T, TError>(value);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([CanBeNull] in T value, [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorFactory());
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Ok<T, TError>(value);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([NotNull, InstantHandle] in Func<T> valueFactory, [NotNull] in TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorObject);
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Warn<T, TError>(valueFactory(), Logic.Message, Logic.Exception);
            return Ok<T, TError>(valueFactory());
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([NotNull, InstantHandle] in Func<T> valueFactory, [NotNull, InstantHandle] in Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorFactory());
            if (IsWarning)
                // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
                return Warn<T, TError>(valueFactory(), Logic.Message, Logic.Exception);
            return Ok<T, TError>(valueFactory());
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }

        #region Internal helpers

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="additionalMessage">Message to add as suffix of this <see cref="Result"/> message.</param>
        /// <param name="exception">Exception to set in the failure <see cref="Result"/>.</param>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult([NotNull] in string additionalMessage, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail(Logic.Message + additionalMessage, exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="Result{T}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T}"/>.</returns>
        [Pure]
        internal Result<T> ToFailValueResult<T>()
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail<T>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailCustomValueResult<T, TError>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a Result<T, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail<T, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a warning <see cref="Result"/>.
        /// This <see cref="Result"/> should be a Ok or warning.
        /// </summary>
        /// <param name="message">Message to set in the warning <see cref="Result"/>.</param>
        /// <param name="exception">Exception to set in the warning <see cref="Result"/>.</param>
        /// <returns>A warning <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToWarnResult([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning Result<T> to a Result<T> warning.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Warn(message, exception);
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks that this <see cref="Result"/> is equals to the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public bool SuccessEquals(in Result other)
        {
            if (IsSuccess && other.IsSuccess)
                return Equals(other);
            return false;
        }

        /// <inheritdoc />
        public bool Equals(Result other)
        {
            return Logic.Equals(other.Logic);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is Result result && Equals(result);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result"/> are equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are equal, otherwise false.</returns>
        public static bool operator ==(in Result result1, in Result result2)
        {
            return result1.Equals(result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are not equal, otherwise false.</returns>
        public static bool operator !=(in Result result1, in Result result2)
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
            if (obj is Result other)
                return CompareTo(other);

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Result)}");
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares this <see cref="Result"/> with the given one.
        /// Order keeps failures first, then warnings and finally successes.
        /// </summary>
        public int CompareTo(Result other)
        {
            return Logic.CompareTo(other.Logic);
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "less" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <(in Result left, in Result right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(in Result left, in Result right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(in Result left, in Result right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(in Result left, in Result right)
        {
            return left.CompareTo(right) >= 0;
        }

        #endregion

        #region Factory methods

        // Here to be easy to call

        #region Result without value

        /// <summary>
        /// Gets a success <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result Ok()
        {
            return ResultOk;
        }

        /// <summary>
        /// Gets a success <see cref="Result"/> with warning.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result Warn([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            return new Result(true, message, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result"/>.
        /// </summary> 
        /// <param name="error">Result error message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt")]
        public static Result Fail([NotNull] in string error, [CanBeNull] in Exception exception = null)
        {
            return new Result(false, error, exception);
        }

        #endregion

        #region Result with Value

        /// <summary>
        /// Gets a success <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> Ok<T>([CanBeNull] in T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Gets a success <see cref="Result{T}"/> with warning.
        /// </summary> 
        /// <param name="value">Result value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result<T> Warn<T>([CanBeNull] in T value, [NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            return new Result<T>(true, value, message, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="error">Result error message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt")]
        public static Result<T> Fail<T>([NotNull] in string error, [CanBeNull] in Exception exception = null)
        {
            return new Result<T>(false, default, error, exception);
        }

        #endregion

        #region Result with Custom error

        /// <summary>
        /// Gets a success <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public static CustomResult<TError> CustomOk<TError>()
        {
            return CustomResult<TError>.ResultOk;
        }

        /// <summary>
        /// Gets a success <see cref="CustomResult{TError}"/> with warning.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static CustomResult<TError> CustomWarn<TError>([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            return new CustomResult<TError>(true, message, default, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt; error:null => halt")]
        public static CustomResult<TError> CustomFail<TError>([NotNull] in string message, [NotNull] in TError error, [CanBeNull] in Exception exception = null)
        {
            return new CustomResult<TError>(false, message, error, exception);
        }

        #endregion

        #region Result with Value + Custom error

        /// <summary>
        /// Gets a success <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T, TError> Ok<T, TError>([CanBeNull] in T value)
        {
            return new Result<T, TError>(value);
        }

        /// <summary>
        /// Gets a success <see cref="Result{T, TError}"/> with warning.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result<T, TError> Warn<T, TError>([CanBeNull] in T value, [NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            return new Result<T, TError>(value, message, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt; error:null => halt")]
        public static Result<T, TError> Fail<T, TError>([NotNull] in string message, [NotNull] in TError error, [CanBeNull] in Exception exception = null)
        {
            return new Result<T, TError>(message, error, exception);
        }

        #endregion

        #endregion
    }
}