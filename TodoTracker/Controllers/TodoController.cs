using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TodoTrackerBiz;
using TodoTrackerModels;

namespace TodoTracker.Controllers
{
    /// <summary>
    /// Defines the <see cref="TodoController" />
    /// </summary>
    public class TodoController : Controller
    {
        /// <summary>
        /// Defines the _todoBiz
        /// </summary>
        private ITodoService _todoBiz;

        /// <summary>
        /// Defines the logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoController"/> class.
        /// </summary>
        /// <param name="todoBiz">The <see cref="ITodoService"/></param>
        public TodoController(ITodoService todoBiz)
        {
            _todoBiz = todoBiz;
        }

        // GET: Todo
        /// <summary>
        /// The Index
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
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

            List<Todo> activeTodos = new List<Todo>();

            try
            {
                activeTodos = _todoBiz.GetActiveTodos();

                logger.Debug("Get all Active Todo resulted in : {0}", activeTodos.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Get All Active Todos failed");
            }

            return Json(activeTodos, JsonRequestBehavior.AllowGet);
        }

    
        /// <summary>
        /// The Get an ActiveTodo
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpGet]
        public JsonResult GetActiveTodo(int id)
        {
            Todo todo = null;
            try
            {


                todo = _todoBiz.GetSingleTodo(id);
                logger.Debug("Get Active Todo resulted in ID: {0} Description: {1}", todo.TodoID, todo.TodoDesc);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Get Active Todo failed");
            }

            return Json(todo, JsonRequestBehavior.AllowGet);
        }

     
        /// <summary>
        /// Delete a Todo
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpDelete]
        public JsonResult DeleteTodo(int id)
        {
            bool result = false;
            try
            {

                result = _todoBiz.Delete(id);
                logger.Debug("Deleting Todo  ID: {0} resulted in: {1}", id, result.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Delete Todo failed");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

      
        /// <summary>
        /// The CompleteTodo
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpDelete]
        public JsonResult CompleteTodo(int id)
        {
            bool result = false;
            try
            {

                result = _todoBiz.Complete(id);
                logger.Debug("Completing Todo  ID: {0} resulted in: {1}", id, result.ToString());
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Completing an Active Todo failed");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Udpate to Completed
        /// <summary>
        /// The UpdateTodo
        /// </summary>
        /// <param name="todo">The <see cref="Todo"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpPost]
        public JsonResult UpdateTodo(Todo todo)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                try
                {

                    result = _todoBiz.Update(todo);
                    logger.Debug("Updating Todo  ID: {0} resulted in: {1} ", todo.TodoID, result.ToString());
                    return Json(new
                    {
                        success = result
                    });
                }
                catch
                { }
            }

            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();

            return Json(new
            {
                success = false,
                errors
            });
        }

        /// <summary>
        /// The UpdateTodo
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult UpdateTodo()
        {
            return View();
        }

        
        /// <summary>
        /// The AddTodo
        /// </summary>
        /// <param name="todo">The <see cref="Todo"/></param>
        /// <returns>The <see cref="JsonResult"/></returns>
        [HttpPost]
        public JsonResult AddTodo(Todo todo)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                try
                {


                    result = _todoBiz.AddTodo(todo);

                    logger.Debug("Adding Todo Description: {0} resulted in: {1}", todo.TodoDesc, result.ToString());

                    if (result)
                    {
                        return Json(new
                        {
                            success = result
                        });
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Adding an Active Todos failed");
                }

            }


            var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();
            return Json(new
            {
                success = false,
                errors
            });
        }

        /// <summary>
        /// The AddTodo
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult AddTodo()
        {
            return View();
        }
    }
}
