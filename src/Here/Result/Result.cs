using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Here
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
        internal Result([NotNull] ResultLogic logic)
        {
            Logic = logic;
        }

        /// <summary>
        /// <see cref="Result"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embedded exception.</param>
        private Result(bool isWarning, [NotNull] string message, [CanBeNull] Exception exception)
        {
            Logic = new ResultLogic(isWarning, message, exception);
        }

        #region Cast

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([CanBeNull] T value)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(value, Logic);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T> Cast<T>([NotNull, InstantHandle] Func<T> valueFactory)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));

            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(valueFactory(), Logic);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull] TError errorObject)
        {
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsFailure)
                return ToFailCustomResult(errorObject);
            if (IsWarning)
                return CustomWarn<TError>(Logic.Message, Logic.Exception);
            return CustomOk<TError>();
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull, InstantHandle] Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsFailure)
                return ToFailCustomResult(errorFactory());
            if (IsWarning)
                return CustomWarn<TError>(Logic.Message, Logic.Exception);
            return CustomOk<TError>();
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([CanBeNull] T value, [NotNull] TError errorObject)
        {
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsFailure)
                return ToFailValueCustomResult<T, TError>(errorObject);
            if (IsWarning)
                return Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Ok<T, TError>(value);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([CanBeNull] T value, [NotNull, InstantHandle] Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsFailure)
                return ToFailValueCustomResult<T, TError>(errorFactory());
            if (IsWarning)
                return Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Ok<T, TError>(value);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorObject"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([NotNull, InstantHandle] Func<T> valueFactory, [NotNull] TError errorObject)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));
            Throw.IfArgumentNull(errorObject, nameof(errorObject));

            if (IsFailure)
                return ToFailValueCustomResult<T, TError>(errorObject);
            if (IsWarning)
                return Warn<T, TError>(valueFactory(), Logic.Message, Logic.Exception);
            return Ok<T, TError>(valueFactory());
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="valueFactory">Factory method that creates a value.</param>
        /// <param name="errorFactory">Factory method that creates a custom error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="valueFactory"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="errorFactory"/> is null.</exception>
        [PublicAPI, Pure]
        public Result<T, TError> Cast<T, TError>([NotNull, InstantHandle] Func<T> valueFactory, [NotNull, InstantHandle] Func<TError> errorFactory)
        {
            Throw.IfArgumentNull(valueFactory, nameof(valueFactory));
            Throw.IfArgumentNull(errorFactory, nameof(errorFactory));

            if (IsFailure)
                return ToFailValueCustomResult<T, TError>(errorFactory());
            if (IsWarning)
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
        internal Result ToFailResult([NotNull] string additionalMessage, [CanBeNull] Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a Result failure.");
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
            return Fail<T>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a CustomResult<TError> failure.");
            return CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Converts this <see cref="Result"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailValueCustomResult<T, TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertibleToFailure(Logic), "Cannot convert a success Result to a Result<T, TError> failure.");
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
        internal Result ToWarnResult([NotNull] string message, [CanBeNull] Exception exception = null)
        {
            Debug.Assert(ResultLogic.IsConvertibleToWarning(Logic), "Cannot convert a warning Result<T> to a Result<T> warning.");
            return Warn(message, exception);
        }

        #endregion

        #region Equality / IEquatable<T>

        /// <summary>
        /// Checks that this <see cref="Result"/> is equals to the given one and that they are successful.
        /// </summary>
        /// <param name="other"><see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are equal and successful, otherwise false.</returns>
        [PublicAPI, Pure]
        public bool SuccessEquals(Result other)
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
        public static bool operator ==(Result result1, Result result2)
        {
            return result1.Equals(result2);
        }

        /// <summary>
        /// Indicates whether both <see cref="Result"/> are not equal.
        /// </summary>
        /// <param name="result1">First <see cref="Result"/> to compare.</param>
        /// <param name="result2">Second <see cref="Result"/> to compare.</param>
        /// <returns>True if both <see cref="Result"/> are not equal, otherwise false.</returns>
        public static bool operator !=(Result result1, Result result2)
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

            throw new ArgumentException($"Cannot compare an object of type {obj.GetType()} with a {typeof(Result)}.");
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
        public static bool operator <(Result left, Result right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "less" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator <=(Result left, Result right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "greater" than the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >(Result left, Result right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines if this <see cref="Result"/> is "greater" than or equal to the other one.
        /// </summary>
        /// <param name="left">The first <see cref="Result"/> to compare.</param>
        /// <param name="right">The second <see cref="Result"/> to compare.</param>
        /// <returns>The comparison result.</returns>
        public static bool operator >=(Result left, Result right)
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
        /// <exception cref="ArgumentNullException">If the <paramref name="message"/> is null or empty.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result Warn([NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new Result(true, message, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result"/>.
        /// </summary> 
        /// <param name="error">Result error message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="error"/> is null or empty.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt")]
        public static Result Fail([NotNull] string error, [CanBeNull] Exception exception = null)
        {
            return new Result(false, error, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result"/>.
        /// </summary> 
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("exception:null => halt")]
        public static Result Fail([NotNull] Exception exception)
        {
            Throw.IfArgumentNull(exception, nameof(exception));

            return new Result(false, exception.Message, exception);
        }

        #endregion

        #region Result with Value

        /// <summary>
        /// Gets a success <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> Ok<T>([CanBeNull] T value)
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
        /// <exception cref="ArgumentNullException">If the <paramref name="message"/> is null or empty.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result<T> Warn<T>([CanBeNull] T value, [NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new Result<T>(true, value, message, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="error">Result error message.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="error"/> is null or empty.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt")]
        public static Result<T> Fail<T>([NotNull] string error, [CanBeNull] Exception exception = null)
        {
            return new Result<T>(false, default(T), error, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("exception:null => halt")]
        public static Result<T> Fail<T>([NotNull] Exception exception)
        {
            Throw.IfArgumentNull(exception, nameof(exception));

            return new Result<T>(false, default(T), exception.Message, exception);
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
        /// <exception cref="ArgumentNullException">If the <paramref name="message"/> is null or empty.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static CustomResult<TError> CustomWarn<TError>([NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new CustomResult<TError>(true, message, default(TError), exception);
        }

        /// <summary>
        /// Gets a failure <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="message"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="error"/> is null.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt; error:null => halt")]
        public static CustomResult<TError> CustomFail<TError>([NotNull] string message, [NotNull] TError error, [CanBeNull] Exception exception = null)
        {
            return new CustomResult<TError>(false, message, error, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="error"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt; exception:null => halt")]
        public static CustomResult<TError> CustomFail<TError>([NotNull] TError error, [NotNull] Exception exception)
        {
            Throw.IfArgumentNull(exception, nameof(exception));

            return new CustomResult<TError>(false, exception.Message, error, exception);
        }

        #endregion

        #region Result with Value + Custom error

        /// <summary>
        /// Gets a success <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T, TError> Ok<T, TError>([CanBeNull] T value)
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
        /// <exception cref="ArgumentNullException">If the <paramref name="message"/> is null or empty.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result<T, TError> Warn<T, TError>([CanBeNull] T value, [NotNull] string message, [CanBeNull] Exception exception = null)
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
        /// <exception cref="ArgumentNullException">If the <paramref name="message"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="error"/> is null.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt; error:null => halt")]
        public static Result<T, TError> Fail<T, TError>([NotNull] string message, [NotNull] TError error, [CanBeNull] Exception exception = null)
        {
            return new Result<T, TError>(message, error, exception);
        }

        /// <summary>
        /// Gets a failure <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embedded exception.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="error"/> is null.</exception>
        /// <exception cref="ArgumentNullException">If the <paramref name="exception"/> is null.</exception>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt; exception:null => halt")]
        public static Result<T, TError> Fail<T, TError>([NotNull] TError error, [NotNull] Exception exception)
        {
            Throw.IfArgumentNull(exception, nameof(exception));

            return new Result<T, TError>(exception.Message, error, exception);
        }

        #endregion

        #endregion
    }
}