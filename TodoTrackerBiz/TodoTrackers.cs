﻿using System;
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
