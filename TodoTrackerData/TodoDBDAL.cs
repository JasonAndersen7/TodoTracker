using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SQLite;

namespace TodoTrackerData
{
  public  class TodoDBDAL
    {
     
        public void WakeMeUp ()
        {
            IDbConnection db = new SQLiteConnection (ConfigurationManager.ConnectionStrings["ToDoDB"].ConnectionString);

            string SqlString = "Select Requester, Assignee, DueDate, IsCompleted from TODO";

            var myTodos = (List<TodoTrackerModels.Todo>)db.Query< TodoTrackerModels.Todo>(SqlString);

            foreach (var item in myTodos)
            {
                Console.WriteLine(item.Assignee);
            }

        }


    }
}
