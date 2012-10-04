using Personal.Lib.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace Personal.Lib.Test
{


    [TestClass()]
    public class StreamLibTest
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


        [TestMethod()]
        public void AsyncCopyTest()
        {
            Random r = new Random(Environment.TickCount);
            var source = new byte[1 << 10];
            r.NextBytes(source);

            var dest = new MemoryStream();
            var start = DateTime.Now;
            var msSource = new MemoryStream(source);
            WaitHandle wh = msSource.AsyncCopy(dest);
            wh.WaitOne();

            //check
            int c = 0;
            for (int i = 0; i < source.Length; i++)
            {
                Assert.IsTrue(source[i] == (c = dest.ReadByte()));
            }
        }
    }
}
