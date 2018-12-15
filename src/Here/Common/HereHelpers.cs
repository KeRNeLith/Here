namespace Here
{
    /// <summary>
    /// Helpers to be used inside the Here library.
    /// </summary>
    internal static class HereHelpers
    {
        /// <summary>
        /// Identity function.
        /// </summary>
        public static T Identity<T>(T param) => param;

        /// <summary>
        /// Do nothing action.
        /// </summary>
        public static void DoNothing() {}

        /// <summary>
        /// Do nothing action.
        /// </summary>
        public static void DoNothing<T>(T param) {}
    }
}