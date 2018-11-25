using NUnit.Framework;

namespace Here.Tests.Results
{
    /// <summary>
    /// Basic tests for <see cref="ResultLogic"/> and <see cref="ResultLogic{TError}"/>.
    /// </summary>
    [TestFixture]
    internal class ResultLogicTests : ResultTestsBase
    {
        [Test]
        public void ResultLogicIsConvertibleToFailure()
        {
            var logicOk = new ResultLogic();
            Assert.IsFalse(ResultLogic.IsConvertibleToFailure(logicOk));

            var logicWarn = new ResultLogic(true, "My warning", null);
            Assert.IsTrue(ResultLogic.IsConvertibleToFailure(logicWarn));

            var logicFail = new ResultLogic(false, "My failure", null);
            Assert.IsTrue(ResultLogic.IsConvertibleToFailure(logicFail));
        }

        [Test]
        public void ResultLogicIsConvertibleToWarning()
        {
            var logicOk = new ResultLogic();
            Assert.IsTrue(ResultLogic.IsConvertibleToWarning(logicOk));

            var logicWarn = new ResultLogic(true, "My warning", null);
            Assert.IsTrue(ResultLogic.IsConvertibleToWarning(logicWarn));

            var logicFail = new ResultLogic(false, "My failure", null);
            Assert.IsFalse(ResultLogic.IsConvertibleToWarning(logicFail));
        }

        [Test]
        public void ResultLogicErrorIsConvertibleToFailure()
        {
            var logicOk = new ResultLogic<CustomErrorTest>();
            Assert.IsFalse(ResultLogic.IsConvertibleToFailure(logicOk));

            var logicWarn = new ResultLogic<CustomErrorTest>(true, "My warning", null, null);
            Assert.IsTrue(ResultLogic.IsConvertibleToFailure(logicWarn));

            var logicFail = new ResultLogic<CustomErrorTest>(false, "My failure", new CustomErrorTest(), null);
            Assert.IsTrue(ResultLogic.IsConvertibleToFailure(logicFail));
        }

        [Test]
        public void ResultLogicErrorIsConvertibleToWarning()
        {
            var logicOk = new ResultLogic<CustomErrorTest>();
            Assert.IsTrue(ResultLogic.IsConvertibleToWarning(logicOk));

            var logicWarn = new ResultLogic<CustomErrorTest>(true, "My warning", null, null);
            Assert.IsTrue(ResultLogic.IsConvertibleToWarning(logicWarn));

            var logicFail = new ResultLogic<CustomErrorTest>(false, "My failure", new CustomErrorTest(), null);
            Assert.IsFalse(ResultLogic.IsConvertibleToWarning(logicFail));
        }

        [Test]
        public void ResultLogicEquality()
        {
            // Logics
            var logicOk1 = new ResultLogic();
            var logicOk2 = new ResultLogic();
            var logicOkTmp = logicOk1;
            var logicWarn1 = new ResultLogic(true, "My warning", null);
            var logicWarn2 = new ResultLogic(true, "My warning", null);
            var logicWarn3 = new ResultLogic(true, "My warning 2", null);
            var logicWarnTmp = logicWarn1;
            var logicFail1 = new ResultLogic(false, "My failure", null);
            var logicFail2 = new ResultLogic(false, "My failure", null);
            var logicFail3 = new ResultLogic(false, "My failure 2", null);
            var logicFailTmp = logicFail1;

            // Checks Ok
            Assert.AreEqual(logicOk1, logicOk1);
            Assert.IsTrue(logicOk1 == logicOkTmp);
            Assert.IsFalse(logicOk1 != logicOkTmp);

            Assert.AreEqual(logicOk1, logicOk2);
            Assert.AreEqual(logicOk2, logicOk1);
            Assert.IsTrue(logicOk1 == logicOk2);
            Assert.IsFalse(logicOk1 != logicOk2);

            // Checks Warn
            Assert.AreEqual(logicWarn1, logicWarn1);
            Assert.IsTrue(logicWarn1 == logicWarnTmp);
            Assert.IsFalse(logicWarn1 != logicWarnTmp);

            Assert.AreEqual(logicWarn1, logicWarn2);
            Assert.AreEqual(logicWarn2, logicWarn1);
            Assert.IsTrue(logicWarn1 == logicWarn2);
            Assert.IsFalse(logicWarn1 != logicWarn2);

            Assert.AreNotEqual(logicWarn1, logicWarn3);
            Assert.AreNotEqual(logicWarn3, logicWarn1);
            Assert.IsFalse(logicWarn1 == logicWarn3);
            Assert.IsTrue(logicWarn1 != logicWarn3);

            // Checks Fail
            Assert.AreEqual(logicFail1, logicFail1);
            Assert.IsTrue(logicFail1 == logicFailTmp);
            Assert.IsFalse(logicFail1 != logicFailTmp);

            Assert.AreEqual(logicFail1, logicFail2);
            Assert.AreEqual(logicFail2, logicFail1);
            Assert.IsTrue(logicFail1 == logicFail2);
            Assert.IsFalse(logicFail1 != logicFail2);

            Assert.AreNotEqual(logicFail1, logicFail3);
            Assert.AreNotEqual(logicFail3, logicFail1);
            Assert.IsFalse(logicFail1 == logicFail3);
            Assert.IsTrue(logicFail1 != logicFail3);

            // Mixed
            Assert.AreNotEqual(logicOk1, new TestClass());
            Assert.AreNotEqual(new TestClass(), logicOk1);

            // Checks with null
            Assert.AreNotEqual(logicOk1, null);
            Assert.AreNotEqual(null, logicOk1);
            Assert.IsFalse(logicOk1.Equals(null));
            Assert.IsFalse(logicOk1.Equals((object)null));
            Assert.IsFalse(logicOk1 == null);
            Assert.IsFalse(null == logicOk1);
            Assert.IsTrue(logicOk1 != null);
            Assert.IsTrue(null != logicOk1);
        }

        [Test]
        public void ResultLogicErrorEquality()
        {
            var customErrorObject1 = new CustomErrorTest { ErrorCode = -2 };
            var customErrorObject2 = new CustomErrorTest { ErrorCode = -3 };

            // Logics
            var logicOk1 = new ResultLogic<CustomErrorTest>();
            var logicOk2 = new ResultLogic<CustomErrorTest>();
            var logicOkTmp = logicOk1;
            var logicWarn1 = new ResultLogic<CustomErrorTest>(true, "My warning", null, null);
            var logicWarn2 = new ResultLogic<CustomErrorTest>(true, "My warning", null, null);
            var logicWarn3 = new ResultLogic<CustomErrorTest>(true, "My warning 2", null, null);
            var logicWarnTmp = logicWarn1;
            var logicFail1 = new ResultLogic<CustomErrorTest>(false, "My failure", customErrorObject1, null);
            var logicFail2 = new ResultLogic<CustomErrorTest>(false, "My failure", customErrorObject1, null);
            var logicFail3 = new ResultLogic<CustomErrorTest>(false, "My failure", customErrorObject2, null);
            var logicFail4 = new ResultLogic<CustomErrorTest>(false, "My failure 2", customErrorObject1, null);
            var logicFailTmp = logicFail1;

            // Checks Ok
            Assert.AreEqual(logicOk1, logicOk1);
            Assert.IsTrue(logicOk1 == logicOkTmp);
            Assert.IsFalse(logicOk1 != logicOkTmp);

            Assert.AreEqual(logicOk1, logicOk2);
            Assert.AreEqual(logicOk2, logicOk1);
            Assert.IsTrue(logicOk1 == logicOk2);
            Assert.IsFalse(logicOk1 != logicOk2);

            // Checks Warn
            Assert.AreEqual(logicWarn1, logicWarn1);
            Assert.IsTrue(logicWarn1 == logicWarnTmp);
            Assert.IsFalse(logicWarn1 != logicWarnTmp);

            Assert.AreEqual(logicWarn1, logicWarn2);
            Assert.AreEqual(logicWarn2, logicWarn1);
            Assert.IsTrue(logicWarn1 == logicWarn2);
            Assert.IsFalse(logicWarn1 != logicWarn2);

            Assert.AreNotEqual(logicWarn1, logicWarn3);
            Assert.AreNotEqual(logicWarn3, logicWarn1);
            Assert.IsFalse(logicWarn1 == logicWarn3);
            Assert.IsTrue(logicWarn1 != logicWarn3);

            // Checks Fail
            Assert.AreEqual(logicFail1, logicFail1);
            Assert.IsTrue(logicFail1 == logicFailTmp);
            Assert.IsFalse(logicFail1 != logicFailTmp);

            Assert.AreEqual(logicFail1, logicFail2);
            Assert.AreEqual(logicFail2, logicFail1);
            Assert.IsTrue(logicFail1 == logicFail2);
            Assert.IsFalse(logicFail1 != logicFail2);

            Assert.AreNotEqual(logicFail1, logicFail3);
            Assert.AreNotEqual(logicFail3, logicFail1);
            Assert.IsFalse(logicFail1 == logicFail3);
            Assert.IsTrue(logicFail1 != logicFail3);

            Assert.AreNotEqual(logicFail1, logicFail4);
            Assert.AreNotEqual(logicFail4, logicFail1);
            Assert.IsFalse(logicFail1 == logicFail4);
            Assert.IsTrue(logicFail1 != logicFail4);

            // Mixed
            Assert.AreNotEqual(logicOk1, new TestClass());
            Assert.AreNotEqual(new TestClass(), logicOk1);

            // Checks with null
            Assert.AreNotEqual(logicOk1, null);
            Assert.AreNotEqual(null, logicOk1);
            Assert.IsFalse(logicOk1.Equals(null));
            Assert.IsFalse(logicOk1.Equals((object)null));
            Assert.IsFalse(logicOk1 == null);
            Assert.IsFalse(null == logicOk1);
            Assert.IsTrue(logicOk1 != null);
            Assert.IsTrue(null != logicOk1);
        }

        [Test]
        public void ResultLogicCompare()
        {
            // Results
            var logicOk1 = new ResultLogic();
            var logicOk2 = new ResultLogic();
            var logicWarn1 = new ResultLogic(true, "My warning", null);
            var logicWarn2 = new ResultLogic(true, "My warning", null);
            var logicWarn3 = new ResultLogic(true, "My warning 2", null);
            var logicFail1 = new ResultLogic(false, "My failure", null);
            var logicFail2 = new ResultLogic(false, "My failure", null);
            var logicFail3 = new ResultLogic(false, "My failure 2", null);
            
            Assert.IsFalse(logicOk1 < logicOk2);
            Assert.IsTrue(logicOk1 <= logicOk2);
            Assert.IsFalse(logicOk1 > logicOk2);
            Assert.IsTrue(logicOk1 >= logicOk2);

            Assert.IsFalse(logicOk1 < logicWarn1);
            Assert.IsFalse(logicOk1 <= logicWarn1);
            Assert.IsTrue(logicOk1 > logicWarn1);
            Assert.IsTrue(logicOk1 >= logicWarn1);
            
            Assert.IsFalse(logicOk1 < logicFail1);
            Assert.IsFalse(logicOk1 <= logicFail1);
            Assert.IsTrue(logicOk1 > logicFail1);
            Assert.IsTrue(logicOk1 >= logicFail1);


            Assert.IsFalse(logicWarn1 < logicWarn2);
            Assert.IsTrue(logicWarn1 <= logicWarn2);
            Assert.IsFalse(logicWarn1 > logicWarn2);
            Assert.IsTrue(logicWarn1 >= logicWarn2);

            Assert.IsFalse(logicWarn1 < logicWarn3);
            Assert.IsTrue(logicWarn1 <= logicWarn3);
            Assert.IsFalse(logicWarn1 > logicWarn3);
            Assert.IsTrue(logicWarn1 >= logicWarn3);
            
            Assert.IsFalse(logicWarn1 < logicFail1);
            Assert.IsFalse(logicWarn1 <= logicFail1);
            Assert.IsTrue(logicWarn1 > logicFail1);
            Assert.IsTrue(logicWarn1 >= logicFail1);


            Assert.IsFalse(logicFail1 < logicFail2);
            Assert.IsTrue(logicFail1 <= logicFail2);
            Assert.IsFalse(logicFail1 > logicFail2);
            Assert.IsTrue(logicFail1 >= logicFail2);

            Assert.IsFalse(logicFail1 < logicFail3);
            Assert.IsTrue(logicFail1 <= logicFail3);
            Assert.IsFalse(logicFail1 > logicFail3);
            Assert.IsTrue(logicFail1 >= logicFail3);
        }

        [Test]
        public void ResultLogicErrorCompare()
        {
            var customErrorObject = new CustomErrorTest { ErrorCode = -1 };

            // Results
            var logicOk1 = new ResultLogic<CustomErrorTest>();
            var logicOk2 = new ResultLogic<CustomErrorTest>();
            var logicWarn1 = new ResultLogic<CustomErrorTest>(true, "My warning", null, null);
            var logicWarn2 = new ResultLogic<CustomErrorTest>(true, "My warning", null, null);
            var logicWarn3 = new ResultLogic<CustomErrorTest>(true, "My warning 2", null, null);
            var logicFail1 = new ResultLogic<CustomErrorTest>(false, "My failure", customErrorObject, null);
            var logicFail2 = new ResultLogic<CustomErrorTest>(false, "My failure", customErrorObject, null);
            var logicFail3 = new ResultLogic<CustomErrorTest>(false, "My failure 2", customErrorObject, null);

            Assert.IsFalse(logicOk1 < logicOk2);
            Assert.IsTrue(logicOk1 <= logicOk2);
            Assert.IsFalse(logicOk1 > logicOk2);
            Assert.IsTrue(logicOk1 >= logicOk2);
            
            Assert.IsFalse(logicOk1 < logicWarn1);
            Assert.IsFalse(logicOk1 <= logicWarn1);
            Assert.IsTrue(logicOk1 > logicWarn1);
            Assert.IsTrue(logicOk1 >= logicWarn1);
            
            Assert.IsFalse(logicOk1 < logicFail1);
            Assert.IsFalse(logicOk1 <= logicFail1);
            Assert.IsTrue(logicOk1 > logicFail1);
            Assert.IsTrue(logicOk1 >= logicFail1);


            Assert.IsFalse(logicWarn1 < logicWarn2);
            Assert.IsTrue(logicWarn1 <= logicWarn2);
            Assert.IsFalse(logicWarn1 > logicWarn2);
            Assert.IsTrue(logicWarn1 >= logicWarn2);
            
            Assert.IsFalse(logicWarn1 < logicWarn3);
            Assert.IsTrue(logicWarn1 <= logicWarn3);
            Assert.IsFalse(logicWarn1 > logicWarn3);
            Assert.IsTrue(logicWarn1 >= logicWarn3);
            
            Assert.IsFalse(logicWarn1 < logicFail1);
            Assert.IsFalse(logicWarn1 <= logicFail1);
            Assert.IsTrue(logicWarn1 > logicFail1);
            Assert.IsTrue(logicWarn1 >= logicFail1);
            

            Assert.IsFalse(logicFail1 < logicFail2);
            Assert.IsTrue(logicFail1 <= logicFail2);
            Assert.IsFalse(logicFail1 > logicFail2);
            Assert.IsTrue(logicFail1 >= logicFail2);
            
            Assert.IsFalse(logicFail1 < logicFail3);
            Assert.IsTrue(logicFail1 <= logicFail3);
            Assert.IsFalse(logicFail1 > logicFail3);
            Assert.IsTrue(logicFail1 >= logicFail3);
        }
    }
}