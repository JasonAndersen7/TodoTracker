using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoTrackerModels;

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
        //public JsonResult Get_AllTodos()
        //{

        //    //    return Json(, JsonRequestBehavior.AllowGet);
        //    return Json()

        //}

    }


}