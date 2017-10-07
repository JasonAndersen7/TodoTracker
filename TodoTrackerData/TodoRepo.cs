using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrackerModels;

namespace TodoTrackerData
{
    public  class TodoRepo : ITodoRepo
    {

        public  List<Todo> GetTodos()
        {
            throw new NotImplementedException();
        }

        public Todo GetSingleTodo(int TodoID)
        {
            throw new NotImplementedException();
        }

        public bool AddTodo(Todo t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int TodoID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Todo t)
        {
            throw new NotImplementedException();
        }

        public bool Complete(int TodoID)
        {
            throw new NotImplementedException();
        }


    }
}
