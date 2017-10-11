using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTrackerModels

{
    /// <summary>
    /// Class that contains a single todo item
    /// </summary>
    public class Todo
    {
        public int TodoID { get; set; }
        public string Requester { get; set; }
        public string Assignee { get; set; }
        //Store this as string since SQLite doesn't have a native date time
        //Fix this if you have time to make it a Date Time object as it should be
        //and that fixes the hard coded dependency that you have on SQL ite
        public string DueDate { get; set; }
        public int IsCompleted { get; set; }
        public string TodoDesc { get; set; }

    }
}
