using System;

using Moq;
using TodoTrackerData;
using TodoTrackerBiz;
using System.Collections.Generic;
using TodoTrackerModels;
using NUnit.Framework;

namespace TodoTracker.Tests
{
    [TestFixture]
    public class MoreBizTests
    {

        private ITodoService _todoBiz;
        private Mock<ITodoRepo> _mockRepo;

        [SetUp]
        public void Init()
        {
                 _mockRepo  = new Mock<ITodoRepo>(MockBehavior.Strict);
        }
        
        
        //[TestCase]
        //public void TestGetAllActiveTodos()
        //{
        //    List<Todo> mockedUpTodos = new List<Todo>();

        //    //spawn off some Todos 
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Todo t = new Todo()
        //        {
        //            Assignee = "Tom" + i,
        //            IsCompleted = 0,
        //            DueDate = DateTime.Now.ToShortDateString(),
        //            Requester = "Alex" + i,
        //            TodoDesc = i.ToString()
        //        };
        //        mockedUpTodos.Add(t);
        //    }

        //    _mockRepo.Setup(mock => mock.GetAllTodos()).Returns( (List<Todos> m) => mockedUpTodos);
        //    _mockRepo.CallBase = false;
        //    _todoBiz = new TodoService(_mockRepo.Object);
        //    var result = _todoBiz.GetActiveTodos();

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.Count == 5);

        //    //mockRepo.VerifyAll();

        //}

        //[TestCase]
        //public void TestGetNoActiveTodos()
        //{
        //    List<Todo> mockedUpTodos = new List<Todo>();

        //    //spawn off some Todos 
        //    for (int i = 0; i < 5; i++)
        //    {
        //        Todo t = new Todo()
        //        {
        //            Assignee = "Tom" + i,
        //            IsCompleted = 1,
        //            DueDate = DateTime.Now.ToShortDateString(),
        //            Requester = "Alex" + i,
        //            TodoDesc = i.ToString()
        //        };
        //        mockedUpTodos.Add(t);
        //    }

        //    _mockRepo.Setup(mock => mock.GetAllTodos()).Returns((List<Todos> m) => mockedUpTodos);
        //    _mockRepo.CallBase = false;
        //    _todoBiz = new TodoService(_mockRepo.Object);
        //    var result = _todoBiz.GetActiveTodos();

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.Count == 0);

        //    //mockRepo.VerifyAll();

        //}



        [TestCase(2, 2)]
        public void TestGetSingleTodo(int todoId, int expectedResult)
        {
            Todo t = new Todo()
            {
                Assignee = "Tom",
                IsCompleted = 1,
                DueDate = DateTime.Now.ToShortDateString(),
                Requester = "Alex",
                TodoDesc = "stuff"
                , TodoID = 1
                };
         

            _mockRepo.Setup(mock => mock.GetSingleTodo(todoId)).Returns(t);
            _mockRepo.CallBase = false;
            _todoBiz = new TodoService(_mockRepo.Object);
            var result = _todoBiz.GetSingleTodo(todoId);

            Assert.That(result, Is.EqualTo(t));
            Assert.IsTrue(result.TodoID  == 2);

            //mockRepo.VerifyAll();

        }
    }
}
