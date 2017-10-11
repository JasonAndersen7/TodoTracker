using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TodoTrackerModels;

namespace TodoTrackerData.Tests
{
    /// <summary>
    /// Defines the <see cref="TodoRepoTests" />
    /// </summary>
    [TestClass()]
    public class TodoRepoTests
    {
        /// <summary>
        /// Verify that there are more than 1 records in the database
        /// </summary>
        [TestMethod()]
        public void GetTodosTest()
        {
            //Arrange
            List<Todo> todoTest = new List<Todo>();
            ITodoRepo dbDal = new TodoRepo();

            //Act
            todoTest = dbDal.GetAllTodos();



            //Assert
            Assert.IsNotNull(todoTest);
            Assert.IsTrue(todoTest.Count > 1);
        }

        /// <summary>
        /// Verify that you can find at least one record
        /// </summary>
        [TestMethod()]
        public void GetSingleTodo()
        {
            //For now hard code the unit test to get a single result
            //Arrange
            Todo todoTest = new Todo();
            ITodoRepo dbDal = new TodoRepo();

            //Act
            todoTest = dbDal.GetSingleTodo(1);


            //Assert
            Assert.IsNotNull(todoTest);
        }

        /// <summary>
        /// Verify that you can add a record
        /// </summary>
        [TestMethod()]
        public void AddSingleTodo()
        {
            //For now hard code the variables to add
            //Arrange
            Todo todoTest = new Todo()
            {
                Assignee = "Alex Smith",
                DueDate = DateTime.Now.ToString(),
                IsCompleted = 0,
                TodoDesc = "Score a Touchdown",
                Requester = "Peyton Manning"

            };

            ITodoRepo dbDal = new TodoRepo();

            //Act
            bool result = dbDal.AddTodo(todoTest);

            //Assert
            Assert.IsNotNull(todoTest);
        }

        /// <summary>
        /// Verify that you can add a record
        /// </summary>
        [TestMethod()]
        public void DeleteTodo()
        {
            //For now hard code the variables to add
            //Arrange
            int todoId = 2;

            ITodoRepo dbDal = new TodoRepo();

            //Act
            bool result = dbDal.Delete(2);

            //Assert
            Assert.IsNotNull(todoId);
        }

        /// <summary>
        /// The UpdateSingleTodo
        /// </summary>
        [TestMethod()]
        public void UpdateSingleTodo()
        {

            //For now hard code the variables to add
            //Arrange
            Todo todoTest = new Todo()
            {
                Assignee = "Alex Smith",
                DueDate = DateTime.Now.ToString(),
                IsCompleted = 0,
                TodoDesc = "Score a Touchdown",
                Requester = "Peyton Manning",
                TodoID = 1

            };

            ITodoRepo dbDal = new TodoRepo();

            //Act
            bool result = dbDal.Update(todoTest);

            //Assert
            Assert.IsNotNull(todoTest);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// The CompleteSingleTodo
        /// </summary>
        [TestMethod()]
        public void CompleteSingleTodo()
        {

            //For now hard code the variables 
            //Arrange
            int TodoId = 1;

            ITodoRepo dbDal = new TodoRepo();

            //Act
            bool result = dbDal.Complete(TodoId);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
