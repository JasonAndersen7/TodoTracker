using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TodoTrackerBiz;
using TodoTrackerData;
using TodoTrackerModels;

namespace TodoTracker.Tests
{
    /// <summary>
    /// Defines the <see cref="MoreBizTests" />
    /// </summary>
    [TestFixture]
    public class MoreBizTests
    {
        /// <summary>
        /// Defines the _todoBiz
        /// </summary>
        private ITodoService _todoBiz;

        /// <summary>
        /// Defines the _mockRepo
        /// </summary>
        private Mock<ITodoRepo> _mockRepo;

        /// <summary>
        /// The Init
        /// </summary>
        [SetUp]
        public void Init()
        {
            _mockRepo = new Mock<ITodoRepo>(MockBehavior.Strict);
        }

        /// <summary>
        /// The TestGetAllActiveTodos
        /// </summary>
        [TestCase]
        public void TestGetAllActiveTodos()
        {
            List<Todo> mockedUpTodos = new List<Todo>();

            // spawn off some Todos
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

            _mockRepo.Setup(mock => mock.GetAllTodos()).Returns(mockedUpTodos);
            _mockRepo.CallBase = false;
            _todoBiz = new TodoService(_mockRepo.Object);
            var result = _todoBiz.GetActiveTodos();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 5);
        }

        /// <summary>
        /// The TestGetNoActiveTodos
        /// </summary>
        [TestCase]
        public void TestGetNoActiveTodos()
        {
            List<Todo> mockedUpTodos = new List<Todo>();

            //spawn off some Todos
            for (int i = 0; i < 6; i++)
            {
                Todo t = new Todo()
                {
                    Assignee = "Tom" + i,
                    IsCompleted = 1,
                    DueDate = DateTime.Now.ToShortDateString(),
                    Requester = "Alex" + i,
                    TodoDesc = i.ToString()
                };
                mockedUpTodos.Add(t);
            }

            _mockRepo.Setup(mock => mock.GetAllTodos()).Returns(mockedUpTodos);
            _mockRepo.CallBase = false;
            _todoBiz = new TodoService(_mockRepo.Object);
            var result = _todoBiz.GetActiveTodos();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        /// <summary>
        /// The TestGetSingleTodo
        /// </summary>
        /// <param name="todoId">The <see cref="int"/></param>
        /// <param name="expectedResult">The <see cref="int"/></param>
        [TestCase(2, 1)]
        public void TestGetSingleTodo(int todoId, int expectedResult)
        {
            Todo t = new Todo()
            {
                Assignee = "Tom",
                IsCompleted = 1,
                DueDate = DateTime.Now.ToShortDateString(),
                Requester = "Alex",
                TodoDesc = "stuff"
                ,
                TodoID = 1
            };


            _mockRepo.Setup(mock => mock.GetSingleTodo(todoId)).Returns(t);
            _mockRepo.CallBase = false;
            _todoBiz = new TodoService(_mockRepo.Object);
            var result = _todoBiz.GetSingleTodo(todoId);

            Assert.IsTrue(result.TodoID == expectedResult);
        }
    }
}
