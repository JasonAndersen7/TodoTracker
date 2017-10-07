using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoTrackerBiz;
using System.Collections.Generic;

namespace TodoTracker.Tests.BizTierTests
{
    [TestClass]
    public class TodoBizTests
    {
        [TestMethod]
        public void TestGetAllActiveBizReposGreaterThanZero()
        {
            //Arrange
            ITodoTrackers testTodoBiz = new TodoTrackers();

            //Act
            var results = testTodoBiz.GetActiveTodos();

            //Assert
            //verify that there are records in the database
            Assert.IsTrue(results.Count > 0);
        }
    }
}
