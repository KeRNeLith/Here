using System;
using System.Diagnostics;
#if SUPPORTS_SERIALIZATION
using System.Runtime.Serialization;
#endif
using JetBrains.Annotations;

namespace Here
{
    /// <summary>
    /// <see cref="CustomResult{TError}"/> is an object that represents the result/state of a treatment.
    /// It provides a custom error object in case of failure.
    /// </summary>
    [PublicAPI]
#if SUPPORTS_SERIALIZATION
    [Serializable]
#endif
    [DebuggerDisplay("{" + nameof(IsSuccess) + " ? \"IsSuccess\" + (" + nameof(IsWarning) + " ? \" with warning\" : System.String.Empty) : \"IsFailure\"}")]
    public readonly partial struct CustomResult<TError>
        : IResultError<TError>
        , IEquatable<CustomResult<TError>>
        , IComparable
        , IComparable<CustomResult<TError>>
#if SUPPORTS_SERIALIZATION
        , ISerializable
#endif
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
        /// <exception cref="InvalidOperationException">If the result is not a failure.</exception>
        public TError Error => Logic.Error;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal readonly ResultLogic<TError> Logic;

        /// <summary>
        /// <see cref="CustomResult{TError}"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal CustomResult(in ResultLogic<TError> logic)
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
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T}"/>.
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
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([NotNull, InstantHandle] in Func<T> valueFactory)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(valueFactory(), ResultLogic.ToResultLogic(Logic));
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T, TError> CustomCast<T>([CanBeNull] in T value)
        {
            if (IsFailure)
                return ToFailValueCustomResult<T>();
            if (IsWarning)
                return Result.Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Result.Ok<T, TError>(value);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> CustomCast<T>([NotNull, InstantHandle] in Func<T> valueFactory)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsFailure)
                return ToFailValueCustomResult<T>();
            if (IsWarning)
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
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a failure Result.");
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
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a failure Result<T>.");
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
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a failure CustomResult<TError>.");
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
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result<T, TError> to a failure CustomResult<TError>.");
            return Result.CustomFail(Logic.Message + additionalMessage, Logic.Error, exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailValueCustomResult<T>()
        {
            Debug.Assert(Logic.IsFailure, "Cannot convert a success CustomResult<TError> to a failure Result<T, TError>.");
            return Result.Fail<T, TError>(Logic.Message, Logic.Error, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="CustomResult{TError}"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Custom error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailValueCustomResult<T>([NotNull] in TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success CustomResult<TError> to a failure Result<T, TError>.");
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
        internal CustomResult<TError> ToWarnValueCustomResult([NotNull] in string message, [CanBeNull] in Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning CustomResult<TError> to a warning CustomResult<TError>.");
            return Result.CustomWarn<TError>(message, exception);
        }

        #endregion

        #region Equality / IEquatable<T>

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

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(CustomResult<TError>)}.");
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

#if SUPPORTS_SERIALIZATION
        #region ISerializable

        private CustomResult(SerializationInfo info, StreamingContext context)
        {
            bool isSuccess = (bool)info.GetValue("IsSuccess", typeof(bool));
            if (isSuccess)
            {
                bool isWarning = (bool)info.GetValue("IsWarning", typeof(bool));
                Logic = isWarning
                    ? new ResultLogic<TError>(
                        true,
                        (string)info.GetValue("Message", typeof(string)),
                        default,
                        (Exception)info.GetValue("Exception", typeof(Exception)))
                    : new ResultLogic<TError>();
            }
            else
            {
                Logic = new ResultLogic<TError>(
                    false,
                    (string)info.GetValue("Message", typeof(string)),
                    (TError)info.GetValue("Error", typeof(TError)),
                    (Exception)info.GetValue("Exception", typeof(Exception)));
            }
        }

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (Logic.IsSuccess)
            {
                info.AddValue("IsSuccess", true);
                if (Logic.IsWarning)
                {
                    info.AddValue("IsWarning", true);
                    info.AddValue("Message", Logic.Message);
                    info.AddValue("Exception", Logic.Exception);
                }
                else
                {
                    info.AddValue("IsWarning", false);
                }
            }
            else
            {
                info.AddValue("IsSuccess", false);
                info.AddValue("Message", Logic.Message);
                info.AddValue("Exception", Logic.Exception);
                info.AddValue("Error", Logic.Error);
            }
        }

        #endregion
#endif

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }
    }
}