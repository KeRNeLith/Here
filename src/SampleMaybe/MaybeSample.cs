using Here.Maybes;

namespace Here.MaybeSample
{
    /// <summary>
    /// <see cref="Maybe{T}"/> sample.
    /// </summary>
    internal class MaybeSample
    {
        private static void Main()
        {
            var maybeInt = Maybe<int>.Some(12);
            var emptyMaybeInt = Maybe<int>.None;
            // Implicit conversion
            var emptyMaybeInt2 = Maybe.None;
            Maybe<int> emptyMaybeInt3 = Maybe.None;

            var maybeObject = Maybe<object>.Some(new object());
            var emptyMaybeObject = Maybe<object>.None;
            // Implicit conversion
            var emptyMaybeObject2 = Maybe.None;
            Maybe<object> emptyMaybeObject3 = Maybe.None;

            // Implicit conversion embedded maybe
            Maybe<Maybe<int>> embedMaybeInt = Maybe<Maybe<int>>.Some(Maybe<int>.Some(42));
            Maybe<int> maybeInt2 = embedMaybeInt;
        }
    }
}
