using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataManager.Model;
using DataManager.Abstract;
using DataManager.Concrete;


namespace GUIMVC.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repository;
        public AdminController(IRepository _repository)
        {
            this.repository = _repository;
        }


        public ActionResult Index()
        {
            List<Problem> model = new List<Problem>();
            model = repository.GetTasks().ToList();
            return View(model);
        }
        public ActionResult CreateTask()
        {
            return PartialView("");
        }
        public ActionResult EditTask() {
            return PartialView("");
        }
        public ActionResult DeleteTask()
        {
            return PartialView("");
        }

    }
}
