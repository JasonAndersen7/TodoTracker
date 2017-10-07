using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrackerModels;
using TodoTrackerData;


namespace TodoTrackerBiz
{
    public class TodoTrackers : ITodoTrackers
    {


        public List<Todo> GetActiveTodos()
        {
            List<Todo> todos = new List<Todo>();

            try
            {
                ITodoRepo todoRepo = new TodoRepo();

                todos = todoRepo.GetActiveTodos();

                //verify that there are some todos in the Database
                if (!todos.Any())
                {
                    //TODO :)
                    //Log that there are no todos in the database

                }
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

            return true;
        }

        public bool Delete(int TodoID)
        {

            return true;
        }

        public bool Update(Todo t)
        {

            return true;
        }

        public bool Complete(int TodoID)
        {


            return true;

        }
    }
}
