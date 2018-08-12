using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Here.Maybes;

namespace Here.Results
{
    /// <summary>
    /// <see cref="Result"/> is an object that represents the result/state of a treatment.
    /// </summary>
    [PublicAPI]
    public partial struct Result : IResult
    {
        /// <summary>
        /// A success <see cref="Result"/>.
        /// </summary>
        private static readonly Result ResultOk = new Result(new ResultLogic());

        /// <inheritdoc />
        public bool IsSuccess => _logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning=> _logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => _logic.IsFailure;

        /// <inheritdoc />
        public string Message => _logic.Message;

        /// <inheritdoc />
        public Exception Exception => _logic.Exception;

        [NotNull]
        internal readonly ResultLogic _logic;

        /// <summary>
        /// <see cref="Result"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal Result([NotNull] ResultLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// <see cref="Result"/> "warning"/"failure" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        private Result(bool isWarning, [NotNull] string message, [CanBeNull] Exception exception)
        {
            _logic = new ResultLogic(isWarning, message, exception);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{Boolean}"/>.</returns>
        public Maybe<bool> ToMaybe()
        {
            return Maybe<bool>.Some(_logic.IsSuccess);
        }

        #region Internal helpers

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result to a Result failure.");
            return Fail(_logic.Message, _logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="Result{T}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T}"/>.</returns>
        internal Result<T> ToFailValueResult<T>()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result to a Result<T> failure.");
            return Fail<T>(_logic.Message, _logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result to a CustomResult<TError> failure.");
            return CustomFail(_logic.Message, errorObject, _logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        internal Result<T, TError> ToFailCustomValueResult<T, TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result to a Result<T, TError> failure.");
            return Fail<T, TError>(_logic.Message, errorObject, _logic.Exception);
        }

        #endregion

        #region Factory methods

        // Here to be easy to call

        #region Result without value

        /// <summary>
        /// Get a success <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static Result Ok()
        {
            return ResultOk;
        }

        /// <summary>
        /// Get a success <see cref="Result"/> with warning.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result Warn([NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new Result(true, message, exception);
        }

        /// <summary>
        /// Get a failure <see cref="Result"/>.
        /// </summary> 
        /// <param name="error">Result error message.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt")]
        public static Result Fail([NotNull] string error, [CanBeNull] Exception exception = null)
        {
            return new Result(false, error, exception);
        }

        #endregion

        #region Result with Value

        /// <summary>
        /// Get a success <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T> Ok<T>([CanBeNull] T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Get a success <see cref="Result{T}"/> with warning.
        /// </summary> 
        /// <param name="value">Result value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result<T> Warn<T>([CanBeNull] T value, [NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new Result<T>(true, value, message, exception);
        }

        /// <summary>
        /// Get a failure <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="error">Result error message.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("error:null => halt")]
        public static Result<T> Fail<T>([NotNull] string error, [CanBeNull] Exception exception = null)
        {
            return new Result<T>(false, default(T), error, exception);
        }

        #endregion

        #region Result with Custom error

        /// <summary>
        /// Get a success <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        public static CustomResult<TError> CustomOk<TError>()
        {
            return CustomResult<TError>.ResultOk;
        }

        /// <summary>
        /// Get a success <see cref="CustomResult{TError}"/> with warning.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static CustomResult<TError> CustomWarn<TError>([NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new CustomResult<TError>(true, message, default(TError), exception);
        }

        /// <summary>
        /// Get a failure <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt; error:null => halt")]
        public static CustomResult<TError> CustomFail<TError>([NotNull] string message, [NotNull] TError error, [CanBeNull] Exception exception = null)
        {
            return new CustomResult<TError>(false, message, error, exception);
        }

        #endregion

        #region Result with Value + Custom error

        /// <summary>
        /// Get a success <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        public static Result<T, TError> Ok<T, TError>([CanBeNull] T value)
        {
            return new Result<T, TError>(value);
        }

        /// <summary>
        /// Get a success <see cref="Result{T, TError}"/> with warning.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt")]
        public static Result<T, TError> Warn<T, TError>([CanBeNull] T value, [NotNull] string message, [CanBeNull] Exception exception = null)
        {
            return new Result<T, TError>(value, message, exception);
        }

        /// <summary>
        /// Get a failure <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <param name="exception">Result embeded exception.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI, Pure]
        [ContractAnnotation("message:null => halt; error:null => halt")]
        public static Result<T, TError> Fail<T, TError>([NotNull] string message, [NotNull] TError error, [CanBeNull] Exception exception = null)
        {
            return new Result<T, TError>(message, error, exception);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// <see cref="Result{T}"/> is an object that represents the result/state of a treatment.
    /// This <see cref="Result{T}"/> embed a <see cref="Value"/> resulting of the treatment.
    /// </summary>
    [PublicAPI]
    public partial struct Result<T> : IResult<T>
    {
        /// <inheritdoc />
        public bool IsSuccess => _logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning => _logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => _logic.IsFailure;

        /// <inheritdoc />
        public string Message => _logic.Message;
        
        /// <inheritdoc />
        public Exception Exception => _logic.Exception;

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
        internal readonly ResultLogic _logic;

        /// <summary>
        /// <see cref="Result{T}"/> "ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] T value)
        {
            _logic = new ResultLogic();
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T}"/> "warning"/"error" constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="value">Embedded value.</param>
        /// <param name="message">Result message.</param>
        /// <param name="exception">Result embeded exception.</param>
        internal Result(bool isWarning, [CanBeNull] T value, [NotNull] string message, [CanBeNull] Exception exception)
        {
            _logic = new ResultLogic(isWarning, message, exception);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T}"/> constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="logic">Result logic.</param>
        internal Result([CanBeNull] T value, [NotNull] ResultLogic logic)
        {
            _logic = logic;
            _value = value;
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        public Maybe<T> ToMaybe()
        {
            if (_logic.IsSuccess)
                return Value;
            return Maybe<T>.None;
        }

        #region Internal helpers

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result<T> to a Result failure.");
            return Result.Fail(_logic.Message, _logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        internal Result<TOut> ToFailValueResult<TOut>()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result<T> to a Result<T> failure.");
            return Result.Fail<TOut>(_logic.Message, _logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result<T> to a CustomResult<TError> failure.");
            return Result.CustomFail(_logic.Message, errorObject, _logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        internal Result<TOut, TError> ToFailCustomValueResult<TOut, TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(_logic), "Cannot convert a success Result<T> to a Result<T, TError> failure.");
            return Result.Fail<TOut, TError>(_logic.Message, errorObject, _logic.Exception);
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }
    }
}