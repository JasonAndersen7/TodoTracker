using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoTrackerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTrackerData.Tests
{
    [TestClass()]
    public class TodoDBDALTests
    {
        [TestMethod()]
        public void WakeMeUpTest()
        {
            TodoDBDAL dbtest = new TodoDBDAL();

            dbtest.WakeMeUp();

            Assert.Fail();
        }
    }
}