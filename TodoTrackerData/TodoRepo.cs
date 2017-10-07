using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrackerModels;

using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Configuration;
using Dapper;

namespace TodoTrackerData
{
    public  class TodoRepo : ITodoRepo
    {
        IDbConnection sqlConn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ToDoDB"].ConnectionString);

        public  List<Todo> GetActiveTodos()
        {
            //get all todos but don't get ones that are completed
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted from TODO where IsCompleted Not IN (1)";

            var myTodos = (List<TodoTrackerModels.Todo>)sqlConn.Query<TodoTrackerModels.Todo>(SqlString);

            return myTodos;
        }

        public Todo GetSingleTodo(int TodoID)
        {
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted from TODO where TODoID = " + TodoID;

            var myTodo = (Todo)sqlConn.Query<TodoTrackerModels.Todo>(SqlString);

            return myTodo;
        }

        public bool AddTodo(Todo t)
        {
            try
            {

                StringBuilder commandText = new StringBuilder();
                commandText.Append("INSERT INTO Todo ( Requester, Assignee, DueDate, IsCompleted ) VALUES ('");
                commandText.Append(t.Requester + "', '");
                commandText.Append(t.Assignee + "', '");
                commandText.Append(t.DueDate.ToString() + "', '");
                commandText.Append(t.IsCompleted + "')");
                    
                
                // Ensure we have a connection
            if (sqlConn == null)
                {
                    throw new NullReferenceException(
                        "Please provide a connection");
                }

                // Ensure that the connection state is Open
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }

                sqlConn.Execute(commandText.ToString());
            }
            catch (Exception)
            {

                throw;
            }
            //the command succeeded
            return true;
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
