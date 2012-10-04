using Personal.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Personal.Lib.Test
{


    [TestClass()]
    public class EnumUtilsTest
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

        public enum Feeling
        {
            [System.ComponentModel.Description("Contente")]
            Happy = 1 << 0,
            [System.ComponentModel.Description("Triste")]
            Sad = 1 << 1,
            [System.ComponentModel.Description("Histérico")]
            Hysterical = 1 << 2,
            //[System.ComponentModel.Description("Carrancudo")]
            Sullen = 1 << 3
        }

        [TestMethod()]
        public void GetEnumDescriptionTest()
        {
            Assert.AreEqual("Contente", EnumUtils.GetEnumDescription(typeof(Feeling), 1));
            Assert.AreEqual("Triste", EnumUtils.GetEnumDescription(typeof(Feeling), 2));
            Assert.AreEqual("Histérico", EnumUtils.GetEnumDescription(typeof(Feeling), 4));
            Assert.AreEqual("Sullen", EnumUtils.GetEnumDescription(typeof(Feeling), 8));

        }

        [TestMethod()]
        public void GetEnumDescriptionTest1()
        {
            Assert.AreEqual("Contente", EnumUtils.GetEnumDescription(typeof(Feeling), Feeling.Happy));
            Assert.AreEqual("Triste", EnumUtils.GetEnumDescription(typeof(Feeling), Feeling.Sad));
            Assert.AreEqual("Histérico", EnumUtils.GetEnumDescription(typeof(Feeling), Feeling.Hysterical));
            Assert.AreEqual("Sullen", EnumUtils.GetEnumDescription(typeof(Feeling), Feeling.Sullen));
        }

        [TestMethod()]
        public void GetEnumDescriptionTest2()
        {
            Assert.AreEqual("Contente", EnumUtils.GetEnumDescription(typeof(Feeling), "Happy"));
            Assert.AreEqual("Triste", EnumUtils.GetEnumDescription(typeof(Feeling), "Sad"));
            Assert.AreEqual("Histérico", EnumUtils.GetEnumDescription(typeof(Feeling), "Hysterical"));
            Assert.AreEqual("Sullen", EnumUtils.GetEnumDescription(typeof(Feeling), "Sullen"));
        }
    }
}
