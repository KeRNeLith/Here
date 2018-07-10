using System;
using JetBrains.Annotations;

namespace Here.Results
{
    /// <summary>
    /// <see cref="Result"/> is an object that represents the result/state of a treatment.
    /// </summary>
    public struct Result : IResult
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

        [NotNull]
        private readonly ResultLogic _logic;

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
        private Result(bool isWarning, [NotNull] string message)
        {
            _logic = new ResultLogic(isWarning, message);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }

        #region Factory methods

        // Here to be easy to call

        #region Result without value

        /// <summary>
        /// Get a success <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/>.</returns>
        public static Result Ok()
        {
            return ResultOk;
        }

        /// <summary>
        /// Get a success <see cref="Result"/> with warning.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [ContractAnnotation("null => halt")]
        public static Result Warn([NotNull] string message)
        {
            return new Result(true, message);
        }

        /// <summary>
        /// Get a failure <see cref="Result"/>.
        /// </summary> 
        /// <param name="error">Result error message.</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [ContractAnnotation("null => halt")]
        public static Result Fail([NotNull] string error)
        {
            return new Result(false, error);
        }

        #endregion

        #region Result with Value

        /// <summary>
        /// Get a success <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        public static Result<T> Ok<T>([CanBeNull] T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Get a success <see cref="Result{T}"/> with warning.
        /// </summary> 
        /// <param name="value">Result value.</param>
        /// <param name="message">Result message.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [ContractAnnotation("message:null => halt")]
        public static Result<T> Warn<T>([CanBeNull] T value, [NotNull] string message)
        {
            return new Result<T>(true, value, message);
        }

        /// <summary>
        /// Get a failure <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="error">Result error message.</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [ContractAnnotation("null => halt")]
        public static Result<T> Fail<T>([NotNull] string error)
        {
            return new Result<T>(false, default(T), error);
        }

        #endregion

        #region Result with Custom error

        /// <summary>
        /// Get a success <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        public static CustomResult<TError> CustomOk<TError>()
        {
            return CustomResult<TError>.ResultOk;
        }

        /// <summary>
        /// Get a success <see cref="CustomResult{TError}"/> with warning.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [ContractAnnotation("message:null => halt")]
        public static CustomResult<TError> CustomWarn<TError>([NotNull] string message)
        {
            return new CustomResult<TError>(true, message, default(TError));
        }

        /// <summary>
        /// Get a failure <see cref="CustomResult{TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [ContractAnnotation("message:null => halt, error:null => halt")]
        public static CustomResult<TError> CustomFail<TError>([NotNull] string message, [NotNull] TError error)
        {
            return new CustomResult<TError>(false, message, error);
        }

        #endregion

        #region Result with Value + Custom error

        /// <summary>
        /// Get a success <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        public static Result<T, TError> Ok<T, TError>([CanBeNull] T value)
        {
            return new Result<T, TError>(value);
        }

        /// <summary>
        /// Get a success <see cref="Result{T, TError}"/> with warning.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="message">Result message.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [ContractAnnotation("message:null => halt")]
        public static Result<T, TError> Warn<T, TError>([CanBeNull] T value, [NotNull] string message)
        {
            return new Result<T, TError>(value, message);
        }

        /// <summary>
        /// Get a failure <see cref="Result{T, TError}"/>.
        /// </summary>
        /// <param name="message">Result message.</param>
        /// <param name="error">Result error object.</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [ContractAnnotation("message:null => halt, error:null => halt")]
        public static Result<T, TError> Fail<T, TError>([NotNull] string message, [NotNull] TError error)
        {
            return new Result<T, TError>(message, error);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// <see cref="Result{T}"/> is an object that represents the result/state of a treatment.
    /// This <see cref="Result{T}"/> embed a <see cref="Value"/> resulting of the treatment.
    /// </summary>
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
        private readonly ResultLogic _logic;

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
        internal Result(bool isWarning, [CanBeNull] T value, [NotNull] string message)
        {
            _logic = new ResultLogic(isWarning, message);
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

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }
    }
}