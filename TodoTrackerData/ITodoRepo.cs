using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrackerModels;


namespace TodoTrackerData
{
   public interface ITodoRepo
    {
        List<Todo> GetActiveTodos();

        Todo GetSingleTodo(int TodoID);

        bool AddTodo(Todo t);

        bool Delete(int TodoID);

        bool Update(Todo t);

        bool Complete(int TodoID);


    }
}
