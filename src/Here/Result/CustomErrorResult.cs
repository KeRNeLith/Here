using System;
using JetBrains.Annotations;
using Here.Maybes;

namespace Here.Results
{
    /// <summary>
    /// <see cref="CustomResult{TError}"/> is an object that represents the result/state of a treatment with a custom error object.
    /// </summary>
    [PublicAPI]
    public partial struct CustomResult<TError> : IResultError<TError>
    {
        /// <summary>
        /// A success <see cref="CustomResult{TError}"/>.
        /// </summary>
        internal static readonly CustomResult<TError> ResultOk = new CustomResult<TError>(new ResultLogic<TError>());

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

        /// <inheritdoc />
        public TError Error => _logic.Error;

        [NotNull]
        internal readonly ResultLogic<TError> _logic;

        /// <summary>
        /// <see cref="CustomResult{TError}"/> constructor.
        /// </summary>
        /// <param name="logic">Result logic.</param>
        internal CustomResult([NotNull] ResultLogic<TError> logic)
        {
            _logic = logic;
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
            _logic = new ResultLogic<TError>(isWarning, message, error, exception);
        }

        /// <summary>
        /// Convert this <see cref="CustomResult{TError}"/> to a <see cref="Maybe{Boolean}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{Boolean}"/>.</returns>
        public Maybe<bool> ToMaybe()
        {
            return Maybe<bool>.Some(_logic.IsSuccess);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }
    }

    /// <summary>
    /// <see cref="Result{T, TError}"/> is an object that represents the result/state of a treatment.
    /// This <see cref="Result{T, TError}"/> embed a <see cref="Value"/> resulting of the treatment
    /// or a custom error if failed.
    /// </summary>
    [PublicAPI]
    public partial struct Result<T, TError> : IResult<T>, IResultError<TError>
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

        /// <inheritdoc />
        public TError Error => _logic.Error;

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
        internal readonly ResultLogic<TError> _logic;

        /// <summary>
        /// <see cref="Result{T, TError}"/> "ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] T value)
        {
            _logic = new ResultLogic<TError>();
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
            _logic = new ResultLogic<TError>(true, message, default(TError), exception);
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
            _logic = new ResultLogic<TError>(false, message, error, exception);
            _value = default(T);
        }

        /// <summary>
        /// Convert this <see cref="Result{T, TError}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <returns>The corresponding <see cref="Maybe{T}"/>.</returns>
        public Maybe<T> ToMaybe()
        {
            if (_logic.IsSuccess)
                return Value;
            return Maybe<T>.None;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _logic.ToString();
        }
    }
}