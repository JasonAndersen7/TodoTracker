using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoTrackerModels;
using TodoTrackerBiz;


namespace TodoTracker.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>  
        ///   
        /// Get All Todos  
        /// </summary>  
        /// <returns></returns>  
        public JsonResult GetAllActive()
        {
            ITodoTrackersBiz todos = new TodoTrackersBiz();

            List<Todo> activeTodos = new List<Todo>();

            try
            {
                 activeTodos =todos.GetActiveTodos();
            }
            catch 
            {
                throw;
            }

            return Json(activeTodos, JsonRequestBehavior.AllowGet);  
            //    return Json(, JsonRequestBehavior.AllowGet);

        }

        // GET: Get Single Todo  
        [HttpGet]
        public JsonResult GetActiveTodo(int id)
        {
            object todo = null;
            try
            {
            
                ITodoTrackersBiz todoTracker = new TodoTrackersBiz();

                todo = todoTracker.GetSingleTodo(id);
            }
            catch
            { }

            return Json(todo, JsonRequestBehavior.AllowGet);
        }

        // Delete A Todo
        [HttpDelete]
        public JsonResult DeleteTodo(int id)
        {
            bool result = false;
            try
            {
                ITodoTrackersBiz todoTracker = new TodoTrackersBiz();

                result = todoTracker.Delete(id);
            }
            catch
            { }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Udpate to Completed
        [HttpPost]
        public JsonResult CompleteTodo(int id)
        {
            bool result = false;
            try
            {
                ITodoTrackersBiz todoTracker = new TodoTrackersBiz();

                result = todoTracker.Complete(id);
            }
            catch
            { }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }


}