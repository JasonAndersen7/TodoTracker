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
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted, TodoDesc from TODO where IsCompleted Not IN (1)";

            var myTodos = (List<TodoTrackerModels.Todo>)sqlConn.Query<TodoTrackerModels.Todo>(SqlString);

            return myTodos;
        }

        public Todo GetSingleTodo(int TodoID)
        {
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted, TodoDesc from TODO where TODoID = " + TodoID;

            //Had to add First or Default as some of my tests came back with multiple results, 
            //Research into why, most likely bad data, or bad data modeling
            var myTodo = (Todo)sqlConn.Query<TodoTrackerModels.Todo>(SqlString).FirstOrDefault();

            return myTodo;
        }

        public bool AddTodo(Todo t)
        {
            try
            {
                //TODO Research Dapper commands for Inserts
                StringBuilder commandText = new StringBuilder();
                commandText.Append("INSERT INTO Todo ( Requester, Assignee, DueDate, TodoDesc, IsCompleted ) VALUES ('");
                commandText.Append(t.Requester + "', '");
                commandText.Append(t.Assignee + "', '");
                commandText.Append(t.DueDate.ToString() + "', '");
                commandText.Append(t.TodoDesc + "', '");
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
            //the command succeeded
            return true;
        }


        /// <summary>
        /// Used to update one record
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(Todo t)
        {
            try
            {
                //TODO Research Dapper commands for Updates
                StringBuilder commandText = new StringBuilder();
                commandText.Append("UPDATE Todo SET "); 
                  commandText.Append(string.Format("Requester = '{0}',", t.Requester));
                commandText.Append(string.Format(" Assignee = '{0}',", t.Assignee));
                commandText.Append(string.Format(" DueDate = '{0}',", t.DueDate.ToString()));
                commandText.Append(string.Format(" IsCompleted = '{0}',", t.IsCompleted));
                commandText.Append(string.Format(" TodoDesc = '{0}'", t.TodoDesc));
                commandText.Append("WHERE ");
                commandText.Append(" TODOID = " + t.TodoID);

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
            //the command succeeded
            return true;
        }


    }
}
