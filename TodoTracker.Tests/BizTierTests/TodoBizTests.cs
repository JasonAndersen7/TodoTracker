using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoTrackerBiz;
using System.Collections.Generic;

namespace TodoTracker.Tests.BizTierTests
{
    [TestClass]
    public class TodoBizTests
    {

        private readonly ITodoService _todoBiz;

        public TodoBizTests(ITodoService todoBiz)
        {
            _todoBiz = todoBiz;
        }


        
        [TestMethod]
        public void TestGetAllActiveBizReposGreaterThanZero()
        {
            //Arrange
            
            
            //Act
            var results = _todoBiz.GetActiveTodos();

            //Assert
            //verify that there are records in the database
            Assert.IsTrue(results.Count > 0);
        }
    }
}
