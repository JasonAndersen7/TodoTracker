using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoTrackerData;
using TodoTrackerBiz;
using System.Collections.Generic;
using TodoTrackerModels;


namespace TodoTracker.Tests
{
    [TestClass]
    public class MoreBizTests
    {

        private ITodoService _todoBiz;
        private Mock<ITodoRepo> _mockRepo;

        [TestInitialize]
        public void Init ()
        {
                 _mockRepo  = new Mock<ITodoRepo>(MockBehavior.Strict);
        }
        
        
        [TestMethod]
        public void TestMethod1()
        {
            List<Todo> mockedUpTodos = new List<Todo>();

            //spawn off some Todos 
            for (int i = 0; i < 5; i++)
            {
                Todo t = new Todo()
                {
                    Assignee = "Tom" + i,
                    IsCompleted = 0,
                    DueDate = DateTime.Now.ToShortDateString(),
                    Requester = "Alex" + i,
                    TodoDesc = i.ToString()
                };
                mockedUpTodos.Add(t);
            }

            _mockRepo.Setup(mock => mock.GetAllTodos()).Returns( (List<Todos> m) => mockedUpTodos);
            _mockRepo.CallBase = false;
            _todoBiz = new TodoService(_mockRepo.Object);
            var result = _todoBiz.GetActiveTodos();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);

            //mockRepo.VerifyAll();

        }
    }
}
