using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Data.Entity;

using DataManager.Abstract;
using DataManager.Model;
using DataManager.Concrete.EFContexts;

namespace DataManager.Concrete
{
    public class ImplRepository : IRepository
    {
        private CFDBContex dbcontex = new CFDBContex();
        private bool disposing = false;

        public virtual void Dispose(bool disposing) {
            if (!this.disposing)
            {
                if (disposing) 
                {
                    this.dbcontex.Dispose();
                }
            }
            this.disposing = true;

        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //IRepository
        public IQueryable<Problem> GetTasks()
        {
             return dbcontex.Problems.Include(i=>i.ChildTasks).Include(i => i.ParentTasks);
        }

        public Problem GetTask(int Id)
        {
            return null;
        }
        public void CreateTask(Problem _Task) {
            dbcontex.Problems.Add(_Task);
            dbcontex.SaveChanges();
        }
        public void EditTask(Problem _Task) {
            Problem Task = FindTask(_Task.Id);

            Task.Title = _Task.Title;
            Task.Description = _Task.Description;
            Task.Laboriousness = _Task.Laboriousness;
            Task.ActualExecutiontime = _Task.ActualExecutiontime;
            Task.EndDate = _Task.EndDate;
            Task.Status = _Task.Status;
            Task.Performers = _Task.Performers;

            dbcontex.Entry(Task).State = EntityState.Modified;
            dbcontex.SaveChanges();
        }
        public Problem FindTask(int? Id) {
            if (dbcontex.Problems.Find(Id) != null)
            {
                return dbcontex.Problems.Where(i=>i.Id==Id).Include(i => i.ChildTasks).Include(i=>i.ParentTasks).Select(i=>i).First();
            }
            else return null;
        }
        public void DeleteTask(int Id) {
            Problem Task = FindTask(Id);

            List<int> cht = new List<int>();

            AddCht(cht, Task);
            //List<int> pcht = new List<int>();
            //cht.Distinct
            
            for (int i = 0; i < cht.Count; i++) {
                Problem vTask = FindTask(cht[i]);
                dbcontex.Problems.Remove(vTask);
                dbcontex.SaveChanges(); 
            }
            
            dbcontex.Problems.Remove(Task);
            dbcontex.SaveChanges();
        }

        private void AddCht(List<int> Cht, Problem task) {
            //Cht.Add(task.Id);
            if (task.ChildTasks.Count!=0)
                foreach (Problem Childtask in task.ChildTasks)
                { 
                    Cht.Add(Childtask.Id);
                    Problem v = FindTask(Childtask.Id);
                    AddCht(Cht, v);
                }
        }
    }
}
