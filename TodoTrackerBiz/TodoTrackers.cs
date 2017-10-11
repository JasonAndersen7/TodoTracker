using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrackerModels;
using TodoTrackerData;

namespace TodoTrackerBiz
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepo _todoRepo;

        public TodoService(ITodoRepo todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public List<Todo> GetActiveTodos()
        {
            List<Todo> todos = new List<Todo>();

            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                todos = todoRepo.GetAllTodos();

                //verify that there are some todos in the Database
                if (!todos.Any())
                {
                    //TODO :)
                    //Log that there are no todos in the database

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

        public Todo GetSingleTodo(int TodoID)
        {
            Todo retrievedTodo = new Todo();

            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                retrievedTodo = todoRepo.GetSingleTodo(TodoID);

            }
            catch (Exception)
            {

                throw;
            }


            return retrievedTodo;
        }

        public bool AddTodo(Todo t)
        {
            bool result = false;
            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                result = todoRepo.AddTodo(t);

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool Delete(int TodoID)
        {
            bool result = false;

            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                result = todoRepo.Delete(TodoID);

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool Update(Todo t)
        {
            bool result = false;

            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                result = todoRepo.Update(t);


            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool Complete(int TodoID)
        {

            bool result = false;

            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                result = todoRepo.Complete(TodoID);

            }
            catch (Exception)
            {

                throw;
            }

            return result;

        }
    }
}
