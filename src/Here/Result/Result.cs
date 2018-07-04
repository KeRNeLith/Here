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
        private static readonly Result ResultOk = new Result(false, false, null);

        /// <inheritdoc />
        public bool IsSuccess => _logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning=> _logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => _logic.IsFailure;

        /// <inheritdoc />
        public string Message => _logic.Message;

        [NotNull]
        private ResultLogic _logic;

        /// <summary>
        /// <see cref="Result"/> constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="isFailure">Result failure flag.</param>
        /// <param name="message">Result message.</param>
        private Result(bool isWarning, bool isFailure, [CanBeNull] string message)
        {
            _logic = new ResultLogic(isWarning, isFailure, message);
        }

        /// <summary>
        /// <see cref="Result"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal Result([NotNull] ResultLogic logic)
        {
            _logic = logic;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }

        #region Factory methods
        
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
        /// <returns>A <see cref="Result"/>.</returns>
        public static Result Warn([CanBeNull] string message)
        {
            return new Result(true, false, message);
        }

        /// <summary>
        /// Get a failure <see cref="Result"/>.
        /// </summary>
        /// <returns>A <see cref="Result"/>.</returns>
        public static Result Fail([CanBeNull] string error)
        {
            return new Result(false, true, error);
        }

        /// <summary>
        /// Get a success <see cref="Result{T}"/>.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        public static Result<T> Ok<T>([CanBeNull] T value)
        {
            return new Result<T>(false, value, null);
        }

        /// <summary>
        /// Get a success <see cref="Result{T}"/> with warning.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        public static Result<T> Warn<T>([CanBeNull] T value, [CanBeNull] string message)
        {
            return new Result<T>(true, value, message);
        }

        /// <summary>
        /// Get a failure <see cref="Result{T}"/>.
        /// </summary>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        public static Result<T> Fail<T>([CanBeNull] string error)
        {
            return new Result<T>(error);
        }

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
        private ResultLogic _logic;

        /// <summary>
        /// <see cref="Result{T}"/> success & warning constructor.
        /// </summary>
        /// <param name="isWarning">Result warning flag.</param>
        /// <param name="value">Embedded value.</param>
        /// <param name="message">Result message.</param>
        internal Result(bool isWarning, T value, [CanBeNull] string message)
        {
            _logic = new ResultLogic(isWarning, false, message);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T}"/> failure constructor.
        /// </summary>
        /// <param name="message">Result message.</param>
        internal Result([CanBeNull] string message)
        {
            _logic = new ResultLogic(false, true, message);
            _value = default(T);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }
    }
}