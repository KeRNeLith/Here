using System;
#if SUPPORTS_BINARY_EXCEPTION_SERIALIZATION
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
#endif
using NUnit.Framework;

namespace Here.Tests.Units
{
    /// <summary>
    /// Tests for common stuff. 
    /// </summary>
    [TestFixture]
    internal class CommonTests : HereTestsBase
    {
#if SUPPORTS_BINARY_EXCEPTION_SERIALIZATION
        [Test]
        public void NullResultExceptionSerialization()
        {
            Exception ex = new NullResultException();

            // Save the full ToString() value, including the exception message and stack trace.
            string exceptionToString = ex.ToString();

            // Round-trip the exception: Serialize and de-serialize with a BinaryFormatter
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                // "Save" object state
                bf.Serialize(ms, ex);

                // Re-use the same stream for de-serialization
                ms.Seek(0, 0);

                // Replace the original exception with de-serialized one
                ex = (NullResultException)bf.Deserialize(ms);
            }

            // Double-check that the exception message and stack trace (owned by the base Exception) are preserved
            Assert.AreEqual(exceptionToString, ex.ToString());
        }
#endif
    }
}