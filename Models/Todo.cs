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
        public DateTime DueDate { get; set; }
        public int IsCompleted { get; set; }


    }
}
