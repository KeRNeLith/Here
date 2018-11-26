namespace Here
{
    // Operators
    public partial struct Result
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<bool> ToOption()
        {
            return Option<bool>.Some(Logic.IsSuccess);
        }

        #endregion
    }

    public partial struct Result<T>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result{T}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<T> ToOption()
        {
            if (Logic.IsSuccess)
                return _value;
            return Option<T>.None;
        }

        #endregion
    }

    // ReSharper disable once UnusedTypeParameter
    public partial struct CustomResult<TError>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="CustomResult{TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<bool> ToOption()
        {
            return Option<bool>.Some(Logic.IsSuccess);
        }

        #endregion
    }

    // ReSharper disable once UnusedTypeParameter
    public partial struct Result<T, TError>
    {
        #region Gateway to Option

        /// <summary>
        /// Converts this <see cref="Result{T, TError}"/> to an <see cref="Option{T}"/>.
        /// </summary>
        /// <returns>An <see cref="Option{T}"/>.</returns>
        public Option<T> ToOption()
        {
            if (Logic.IsSuccess)
                return _value;
            return Option<T>.None;
        }

        #endregion
    }
}
