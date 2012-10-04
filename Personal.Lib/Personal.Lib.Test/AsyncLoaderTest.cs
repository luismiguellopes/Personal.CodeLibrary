using Personal.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Personal.Lib.Test
{


    /// <summary>
    ///This is a test class for AsyncLoaderTest and is intended
    ///to contain all AsyncLoaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AsyncLoaderTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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


        /// <summary>
        ///A test for AsyncLoader`1 Constructor
        ///</summary>
        public void AsyncLoaderConstructorTestHelper<T>()
        {
            Func<T> loadMethod = null; // TODO: Initialize to an appropriate value
            AsyncLoader<T> target = new AsyncLoader<T>(loadMethod);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void AsyncLoaderConstructorTest()
        {
            bool loaded = false;
            Func<string> m = () =>
            {
                Thread.Sleep(10);
                loaded = true;
                return "X";
            };
            var loader = new AsyncLoader<string>(m);
            
            //imediatamente após a criação o objecto ainda não deve estar carregado
            Assert.IsFalse(loaded);
            //Thread.Sleep(20);

            var res = loader.Value;

            Assert.IsTrue(loaded);
            Assert.AreEqual("X", res);
        }
    }
}
