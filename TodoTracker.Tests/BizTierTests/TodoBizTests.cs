using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoTrackerBiz;
using TodoTrackerData;
using System.Collections.Generic;

namespace TodoTracker.Tests.BizTierTests
{
    [TestClass]
    public class TodoBizTests
    {

        private  ITodoService _todoBiz;
        private  ITodoRepo _todoRepo;
        
        [TestMethod]
        public void TestGetAllActiveBizReposGreaterThanZero()
        {
            //Arrange
            _todoRepo = new TodoRepo();
            _todoBiz = new TodoService(_todoRepo);
            
            //Act
            var results = _todoBiz.GetActiveTodos();

            //Assert
            //verify that there are records in the database
            Assert.IsTrue(results.Count > 0);
        }
    }
}
