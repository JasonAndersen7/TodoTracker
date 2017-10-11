using System;
using System.Collections.Generic;
using System.Linq;
using TodoTrackerData;
using TodoTrackerModels;

namespace TodoTrackerBiz
{
    /// <summary>
    /// Defines the <see cref="TodoService" />
    /// </summary>
    public class TodoService : ITodoService
    {
        /// <summary>
        /// Defines the _todoRepo
        /// </summary>
        private readonly ITodoRepo _todoRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoService"/> class.
        /// </summary>
        /// <param name="todoRepo">The <see cref="ITodoRepo"/></param>
        public TodoService(ITodoRepo todoRepo)
        {
            _todoRepo = todoRepo;
        }

        /// <summary>
        /// Only get the Active Todos
        /// </summary>
        /// <returns></returns>
        public List<Todo> GetActiveTodos()
        {
            List<Todo> todos = new List<Todo>();

            try
            {

                todos = _todoRepo.GetAllTodos();

                //verify that there are some todos in the Database
                if (!todos.Any())
                {
                    throw new Exception("No Active Records Found in the database");

                }

                //remove completed items from the list
                todos.RemoveAll(x => x.IsCompleted == 1);
            }
            catch (Exception)
            {

                throw;
            }


            return todos;
        }

        /// <summary>
        /// The GetSingleTodo
        /// </summary>
        /// <param name="TodoID">The <see cref="int"/></param>
        /// <returns>The <see cref="Todo"/></returns>
        public Todo GetSingleTodo(int TodoID)
        {
            Todo retrievedTodo = new Todo();

            try
            {
                retrievedTodo = _todoRepo.GetSingleTodo(TodoID);

            }
            catch (Exception)
            {

                throw;
            }


            return retrievedTodo;
        }

        /// <summary>
        /// The AddTodo
        /// </summary>
        /// <param name="t">The <see cref="Todo"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool AddTodo(Todo t)
        {
            bool result = false;
            try
            {

                result = _todoRepo.AddTodo(t);

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="TodoID">The <see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Delete(int TodoID)
        {
            bool result = false;

            try
            {

                result = _todoRepo.Delete(TodoID);

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="t">The <see cref="Todo"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Update(Todo t)
        {
            bool result = false;

            try
            {

                result = _todoRepo.Update(t);


            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// The Complete
        /// </summary>
        /// <param name="TodoID">The <see cref="int"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool Complete(int TodoID)
        {

            bool result = false;

            try
            {

                result = _todoRepo.Complete(TodoID);

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
