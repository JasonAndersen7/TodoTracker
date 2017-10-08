using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrackerModels;


using System.Data;
using System.Data.SQLite;
using System.Configuration;
using Dapper;

namespace TodoTrackerData
{
    public  class TodoRepo : ITodoRepo
    {
        IDbConnection sqlConn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ToDoDB"].ConnectionString);


        /// <summary>
        /// Used to get only all Active Todos, those who do not have the IsCompleted Flag set to true
        /// </summary>
        /// <returns></returns>
        public  List<Todo> GetActiveTodos()
        {
            //get all todos but don't get ones that are completed
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted, TodoDesc from TODO where IsCompleted Not IN (1)";

            var myTodos = (List<TodoTrackerModels.Todo>)sqlConn.Query<TodoTrackerModels.Todo>(SqlString);

            return myTodos;
        }

        /// <summary>
        /// Get a Single todo Record
        /// </summary>
        /// <param name="TodoID">The unique id of the todo</param>
        /// <returns></returns>
        public Todo GetSingleTodo(int TodoID)
        {
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted, TodoDesc from TODO where TODoID = " + TodoID;

            //Had to add First or Default as some of my tests came back with multiple results, 
            //Research into why, most likely bad data, or bad data modeling
            var myTodo = (Todo)sqlConn.Query<TodoTrackerModels.Todo>(SqlString).FirstOrDefault();

            return myTodo;
        }

        /// <summary>
        /// Add a Todo 
        /// </summary>
        /// <param name="newTodo"></param>
        /// <returns></returns>
        public bool AddTodo(Todo newTodo)
        {
            try
            {
                //TODO Research Dapper commands for Inserts
                StringBuilder commandText = new StringBuilder();
                commandText.Append("INSERT INTO Todo ( Requester, Assignee, DueDate, TodoDesc, IsCompleted ) VALUES ('");
                commandText.Append(newTodo.Requester + "', '");
                commandText.Append(newTodo.Assignee + "', '");
                commandText.Append(newTodo.DueDate + "', '");
                commandText.Append(newTodo.TodoDesc + "', '");
                commandText.Append(newTodo.IsCompleted + "')");
                    
                
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
            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
            //the command succeeded
            return true;
        }


        /// <summary>
        /// Used to delete a specific record
        /// </summary>
        /// <param name="TodoID"></param>
        /// <returns></returns>
        public bool Delete(int TodoID)
        {
            try
            {
                //TODO Research Dapper commands for Inserts
                StringBuilder commandText = new StringBuilder();
                commandText.Append("DELETE FROM Todo WHERE TodoId = '" + TodoID + "'");

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
            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
            //the command succeeded
            return true;

            
        }


        /// <summary>
        /// Used to update one record
        /// </summary>
        /// <param name="updateTodo"></param>
        /// <returns></returns>
        public bool Update(Todo updateTodo)
        {
            try
            {
                //TODO Research Dapper commands for Updates
                StringBuilder commandText = new StringBuilder();
                commandText.Append("UPDATE Todo SET "); 
                  commandText.Append(string.Format("Requester = '{0}',", updateTodo.Requester));
                commandText.Append(string.Format(" Assignee = '{0}',", updateTodo.Assignee));
                commandText.Append(string.Format(" DueDate = '{0}',", updateTodo.DueDate.ToString()));
                commandText.Append(string.Format(" IsCompleted = '{0}',", updateTodo.IsCompleted));
                commandText.Append(string.Format(" TodoDesc = '{0}'", updateTodo.TodoDesc));
                commandText.Append("WHERE ");
                commandText.Append(" TODOID = " + updateTodo.TodoID);

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
            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
            //the command succeeded
            return true;
        }

        /// <summary>
        /// Set a Todo to complete 
        /// </summary>
        /// <param name="TodoID">The unique ID of the Todo</param>
        /// <returns></returns>
        public bool Complete(int TodoID)
        {
            try
            {
                //TODO Research Dapper commands for Updates
                StringBuilder commandText = new StringBuilder();
                commandText.Append("UPDATE Todo SET ");
                commandText.Append(string.Format(" IsCompleted = '{0}'",1));
             
                commandText.Append("WHERE ");
                commandText.Append(" TODOID = " + TodoID);

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

            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
            //the command succeeded
            return true;
        }



        private string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }

    }
}
