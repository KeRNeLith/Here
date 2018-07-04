using System;
using System.Collections.Generic;
using System.Text;

namespace Here.Result
{
	// Implicit operators
    public partial struct Result<T>
    {
        /// <summary>
        /// Implicit convertion from <see cref="Maybe{U}"/> (where U is a <see cref="Maybe{T}"/>) to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="embeddedMaybe">A <see cref="Maybe{T}"/> that encapsulate another <see cref="Maybe{T}"/>.</param>
        /// <returns>The simple corresponding <see cref="Maybe{T}"/>.</returns>
        public static implicit operator Result(Result<T> result)
        {
            return new Result(result._logic);
        }
    }
}
