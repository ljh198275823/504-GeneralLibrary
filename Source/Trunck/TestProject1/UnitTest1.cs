using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LJH.GeneralLibrary;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DecimalTrimTest()
        {
            decimal t = -9.10m;
            Assert.IsTrue(t.Trim().ToString() == "-9.1");
            t = 0.010m;
            Assert.IsTrue(t.Trim().ToString() == "0.01");
            t = 0.00m;
            Assert.IsTrue(t.Trim().ToString() == "0");
            t = 9100m;
            Assert.IsTrue(t.Trim().ToString() == "9100");
            t = -9010m;
            Assert.IsTrue(t.Trim().ToString() == "-9010");
            t = 9.000m;
            Assert.IsTrue(t.Trim().ToString() == "9");
        }
    }
}
