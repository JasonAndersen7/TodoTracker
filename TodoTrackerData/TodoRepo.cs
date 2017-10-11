using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using TodoTrackerModels;

namespace TodoTrackerData
{
    /// <summary>
    /// Defines the <see cref="TodoRepo" />
    /// </summary>
    public class TodoRepo : ITodoRepo
    {
        /// <summary>
        /// Defines the logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Defines the sqlConn
        /// </summary>
        private IDbConnection sqlConn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ToDoDB"].ConnectionString);

        /// <summary>
        /// Used to get only all  Todos
        /// </summary>
        /// <returns></returns>
        public List<Todo> GetAllTodos()
        {
            //get all todos 
            string SqlString = "Select TodoID, Requester, Assignee, DueDate, IsCompleted, TodoDesc from TODO";

            logger.Info("Sql query created :{0}", SqlString);

            var myTodos = (List<TodoTrackerModels.Todo>)sqlConn.Query<TodoTrackerModels.Todo>(SqlString);

            logger.Info(" Todos retreived : {0}", string.Join(",", myTodos.Select(s => s.TodoID.ToString())));

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

            logger.Info("Sql query created :{0}", SqlString);

            //Had to add First or Default as some of my tests came back with multiple results, 
            //Research into why, most likely bad data, or bad data modeling
            var myTodo = (Todo)sqlConn.Query<TodoTrackerModels.Todo>(SqlString).FirstOrDefault();

            logger.Info(" Todo retreived : {0}", myTodo.TodoID);

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
                commandText.Append(@"INSERT INTO Todo ( Requester, Assignee, DueDate, TodoDesc, IsCompleted ) VALUES ");
                commandText.Append(@"(@Requester, @Assignee, @DueDate, @TodoDesc, @IsCompleted)");

                logger.Info(" SQL command text for Adding a Record:{0}", commandText.ToString());

                CommandDefinition sqlCommand = new CommandDefinition(commandText.ToString(),
                    new
                    {
                        Requester = newTodo.Requester,
                        Assignee = newTodo.Assignee,
                        DueDate = newTodo.DueDate,
                        TodoDesc = newTodo.TodoDesc,
                        isCompleted = 0
                    });



                int recordsAffected = sqlConn.Execute(sqlCommand);

                logger.Info("Inserting Record resulted in :{0}", recordsAffected);

                if (recordsAffected > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception happened while inseting a record", newTodo);
                throw;
            }
            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
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

                CommandDefinition sqlCommand = new CommandDefinition(@"DELETE FROM Todo WHERE TodoId = @TodoID", new { TodoID = TodoID });

                ValidateSQLConnection(sqlConn);

                int recordsAffected = this.sqlConn.Execute(sqlCommand);
                logger.Info("Deleting Record :{0} resulted in :{1}", TodoID, recordsAffected);

                if (recordsAffected > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception happened while deleting a record", TodoID);
                throw;
            }
            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
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

                StringBuilder commandText = new StringBuilder();
                commandText.Append("UPDATE Todo SET ");

                commandText.Append(string.Format("Requester = @Requester, "));
                commandText.Append(string.Format(" Assignee = @Assignee, "));
                commandText.Append(string.Format(" DueDate = @DueDate, "));
                commandText.Append(string.Format(" TodoDesc = @TodoDesc "));
                commandText.Append("WHERE TODOID = " + updateTodo.TodoID);

                ValidateSQLConnection(sqlConn);

                int recordsAffected = this.sqlConn.Execute(commandText.ToString(), updateTodo);
                logger.Info("Updating Record :{0} resulted in :{1}", updateTodo.TodoID, recordsAffected);

                if (recordsAffected > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception happened while updating a record", updateTodo);
                throw;
            }
            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
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

                CommandDefinition sqlCommand = new CommandDefinition(@"UPDATE Todo SET IsCompleted = 1 WHERE TodoId = @TodoID", new { TodoID = TodoID });

                logger.Info("Completing :{0} with sql command of :{1}", TodoID, sqlCommand.CommandText);


                ValidateSQLConnection(sqlConn);

                int recordsAffected = this.sqlConn.Execute(sqlCommand);
                logger.Info("Completing Todo :{0} resulted in :{1}", TodoID, recordsAffected);

                if (recordsAffected > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception happened while completing a record", TodoID);
                throw;
            }

            finally
            {
                //good practice to close your db connections
                sqlConn.Close();
            }
        }

        /// <summary>
        /// The DateTimeSQLite
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }

        /// <summary>
        /// The ValidateSQLConnection
        /// </summary>
        /// <param name="sqlConn">The <see cref="IDbConnection"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ValidateSQLConnection(IDbConnection sqlConn)
        {

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

            return true;
        }
    }
}
