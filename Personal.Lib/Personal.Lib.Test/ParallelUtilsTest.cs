using Personal.Lib.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Personal.Lib.Test
{


    [TestClass()]
    public class ParallelUtilsTest
    {


        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        static int Dummy()
        {
            char[] c = new char[1 << 10];
            for (int i = 0; i < c.Length; i++)
            {
                while (c[i] < 'Z')
                {
                    c[i]++;
                }
            }
            int res = 0;
            for (int i = 0; i < c.Length; i++)
            {
                res += c[i];
            }
            return res;
        }

        [TestMethod()]
        public void ParallelExecuteAndJoinTest()
        {
            int functions = 20;
            Func<int>[] fn = new Func<int>[functions];
            for (int i = 0; i < fn.Length; ++i)
            {
                fn[i] = Dummy;
            }
            int res = ParallelUtils.ParallelExecuteAndJoin((a, b) => a + b, fn);

            int expected = Dummy() * functions;

            Assert.AreEqual(expected, res);
        }


        [TestMethod()]
        public void BeginExecuteAndAddTest()
        {
            int functions = 20;
            Func<int>[] fn = new Func<int>[functions];
            for (int i = 0; i < fn.Length; ++i)
            {
                fn[i] = Dummy;
            }
            int res = ParallelUtils.BeginExecuteAndAdd((a, b) => a + b, fn);

            int expected = Dummy() * functions;

            Assert.AreEqual(expected, res);
        }
    }
}
