﻿using System;
using System.Diagnostics;
using JetBrains.Annotations;

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
        /// <param name="exception">Result embeded exception.</param>
        private Result(bool isWarning, [NotNull] string message, [CanBeNull] Exception exception)
        {
            Logic = new ResultLogic(isWarning, message, exception);
        }

        #region Cast

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="value">Value.</param>
        /// <returns>A corresponding <see cref="Result{T}"/>.</returns>
        [Pure]
        public Result<T> Cast<T>([CanBeNull] T value)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(value, Logic);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Result{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <param name="valueFactory">Factory method that create a value.</param>
        /// <returns>A corresponding <see cref="Result{T}"/>.</returns>
        [Pure]
        public Result<T> Cast<T>([NotNull, InstantHandle] Func<T> valueFactory)
        {
            if (IsFailure)
                return ToFailValueResult<T>();
            return new Result<T>(valueFactory(), Logic);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="CustomResult{TError}"/>
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull] TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomResult(errorObject);
            if (IsWarning)
                return CustomWarn<TError>(Logic.Message, Logic.Exception);
            return CustomOk<TError>();
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="CustomResult{TError}"/>
        /// </summary>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="errorFactory">Factory method that create a custom error object.</param>
        /// <returns>A corresponding <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        public CustomResult<TError> CustomCast<TError>([NotNull, InstantHandle] Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomResult(errorFactory());
            if (IsWarning)
                return CustomWarn<TError>(Logic.Message, Logic.Exception);
            return CustomOk<TError>();
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        public Result<T, TError> Cast<T, TError>([CanBeNull] T value, [NotNull] TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorObject);
            if (IsWarning)
                return Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Ok<T, TError>(value);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="errorFactory">Factory method that create a custom error object.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        public Result<T, TError> Cast<T, TError>([CanBeNull] T value, [NotNull, InstantHandle] Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorFactory());
            if (IsWarning)
                return Warn<T, TError>(value, Logic.Message, Logic.Exception);
            return Ok<T, TError>(value);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="valueFactory">Factory method that create a value.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        public Result<T, TError> Cast<T, TError>([NotNull, InstantHandle] Func<T> valueFactory, [NotNull] TError errorObject)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorObject);
            if (IsWarning)
                return Warn<T, TError>(valueFactory(), Logic.Message, Logic.Exception);
            return Ok<T, TError>(valueFactory());
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a <see cref="Result{T, TError}"/>
        /// </summary>
        /// <typeparam name="T">Type of the output result value.</typeparam>
        /// <typeparam name="TError">Type of the output result error type.</typeparam>
        /// <param name="valueFactory">Factory method that create a value.</param>
        /// <param name="errorFactory">Factory method that create a custom error object.</param>
        /// <returns>A corresponding <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        public Result<T, TError> Cast<T, TError>([NotNull, InstantHandle] Func<T> valueFactory, [NotNull, InstantHandle] Func<TError> errorFactory)
        {
            if (IsFailure)
                return ToFailCustomValueResult<T, TError>(errorFactory());
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
        /// Convert this <see cref="Result"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="Result{T}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{T}"/>.</returns>
        [Pure]
        internal Result<T> ToFailValueResult<T>()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail<T>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result"/> to a failure <see cref="Result{T, TError}"/>.
        /// This <see cref="Result"/> should be a warning or a failure.
        /// </summary>
        /// <param name="errorObject">Error object to use.</param>
        /// <returns>A failed <see cref="Result{T, TError}"/>.</returns>
        [Pure]
        internal Result<T, TError> ToFailCustomValueResult<T, TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result to a Result<T, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Fail<T, TError>(Logic.Message, errorObject, Logic.Exception);
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
        public bool IsSuccess => Logic.IsSuccess;

        /// <inheritdoc />
        public bool IsWarning => Logic.IsWarning;

        /// <inheritdoc />
        public bool IsFailure => Logic.IsFailure;

        /// <inheritdoc />
        public string Message => Logic.Message;
        
        /// <inheritdoc />
        public Exception Exception => Logic.Exception;

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
        internal readonly ResultLogic Logic;

        /// <summary>
        /// <see cref="Result{T}"/> "ok" constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        internal Result([CanBeNull] T value)
        {
            Logic = new ResultLogic();
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
            Logic = new ResultLogic(isWarning, message, exception);
            _value = value;
        }

        /// <summary>
        /// <see cref="Result{T}"/> constructor.
        /// </summary>
        /// <param name="value">Result value.</param>
        /// <param name="logic">Result logic.</param>
        internal Result([CanBeNull] T value, [NotNull] ResultLogic logic)
        {
            Logic = logic;
            _value = value;
        }

        #region Internal helpers

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="Result"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result"/>.</returns>
        [Pure]
        internal Result ToFailResult()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<T> to a Result failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="Result{TOut}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut}"/>.</returns>
        [Pure]
        internal Result<TOut> ToFailValueResult<TOut>()
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<T> to a Result<T> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut>(Logic.Message, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="CustomResult{TError}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="CustomResult{TError}"/>.</returns>
        [Pure]
        internal CustomResult<TError> ToFailCustomResult<TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<T> to a CustomResult<TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.CustomFail(Logic.Message, errorObject, Logic.Exception);
        }

        /// <summary>
        /// Convert this <see cref="Result{T}"/> to a failure <see cref="Result{TOut, TError}"/>.
        /// This <see cref="Result{T}"/> should be a warning or a failure.
        /// </summary>
        /// <returns>A failed <see cref="Result{TOut, TError}"/>.</returns>
        [Pure]
        internal Result<TOut, TError> ToFailCustomValueResult<TOut, TError>([NotNull] TError errorObject)
        {
            Debug.Assert(ResultLogic.IsConvertableToFailure(Logic), "Cannot convert a success Result<T> to a Result<T, TError> failure.");
            // ReSharper disable once AssignNullToNotNullAttribute, Justification The message is always not null or empty when here.
            return Result.Fail<TOut, TError>(Logic.Message, errorObject, Logic.Exception);
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Logic.ToString();
        }
    }
}