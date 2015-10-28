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
            List<Problem> alltasks = new List<Problem>();
            alltasks = repository.GetTasks().ToList();
            List<Problem> roottasks = new List<Problem>();
            roottasks = alltasks.FindAll(i => i.ParentTasks.Count == 0);
            return View(roottasks);
        }
        [HttpGet]
        public ActionResult CreateTask()
        {
            string[] statuses = { "Назначена", "Выполняется", "Приостановлена", "Завершена" };
            SelectList Statuses = new SelectList(statuses);
            ViewBag.Statuses = Statuses;
            return PartialView("PartialCreateTask");
        }
        [HttpPost]
        public ActionResult CreateTask(Problem _Task, string Status)
        {
            _Task.Status = Status;
            _Task.StartDate = DateTime.Now;

            repository.CreateTask(_Task);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateChildTask(int? Id )
        {
            string[] statuses = { "Назначена", "Выполняется", "Приостановлена", "Завершена" };
            SelectList Statuses = new SelectList(statuses);
            ViewBag.Statuses = Statuses;
            ViewBag.ParentTaskId = Id;
            return PartialView("PartialCreateChildTask");
        }
        [HttpPost]
        public ActionResult CreateChildTask(Problem _Task, string Status, string ParentTaskId)
        {
            Problem ParentTask = repository.FindTask(Convert.ToInt32(ParentTaskId));

            _Task.Status = Status;
            _Task.StartDate = DateTime.Now;
            _Task.ParentTasks.Add(ParentTask);

            repository.CreateTask(_Task);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditTask(int? Id)
        {
            Problem Task = repository.FindTask(Id);
            Task.EndDate = Convert.ToDateTime(Task.EndDate.ToString("d"));
            string[] vstatuses = {"Назначена","Выполняется", "Приостановлена", "Завершена" };
            SelectList Statuses = new SelectList(vstatuses);;
            if (Task.Status == "Назначена")
            {
                string[] statuses = { "Выполняется", "Приостановлена"};
                Statuses = new SelectList(statuses);
            }
            if (Task.Status == "Выполняется")
            {
                string[] statuses = { "Приостановлена", "Завершена" };
                Statuses = new SelectList(statuses);
            }
            if (Task.Status == "Приостановлена")
            {
                string[] statuses = { "Выполняется" ,"Завершена" };
                Statuses = new SelectList(statuses);
            }
            if (Task.Status == "Завершена")
            {
                string[] statuses = { "Выполняется"  };
                Statuses = new SelectList(statuses);
            }
            ViewBag.Statuses = Statuses;
            return PartialView("PartialEditTask", Task);
        }
        [HttpPost]
        public ActionResult EditTask(Problem _Task)
        {
            repository.EditTask(_Task);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteTask(int? id)
        {
            if (id == null)
                   return HttpNotFound();
   
            Problem  Task = repository.FindTask(id);
            if (Task == null)
                   return HttpNotFound();

            return PartialView("PartialDeleteTask", Task);
        }
        
        [HttpPost, ActionName("DeleteTask")]
        public ActionResult DeleteTaskConfirmed(int? id)
        {
            if (id == null) return HttpNotFound();

           Problem Task = repository.FindTask(id);

           if (Task == null) return HttpNotFound();

           repository.DeleteTask(Task.Id);

            return RedirectToAction("Index");
        }


    }
}
