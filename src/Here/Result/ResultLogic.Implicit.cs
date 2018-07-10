namespace Here.Results
{
	// Implicit operators
    internal partial class ResultLogic<TError>
    {
        /// <summary>
        /// Implicit convertion from <see cref="ResultLogic{TError}"/> to a <see cref="ResultLogic"/>.
        /// </summary>
        /// <param name="logic"><see cref="ResultLogic{TError}"/> to convert.</param>
        /// <returns>A corresponding <see cref="ResultLogic"/>.</returns>
        public static implicit operator ResultLogic(ResultLogic<TError> logic)
        {
            if (logic.IsSuccess && !logic.IsWarning)
                return new ResultLogic();
            return new ResultLogic(logic.IsWarning, logic.Message);
        }
    }
}
