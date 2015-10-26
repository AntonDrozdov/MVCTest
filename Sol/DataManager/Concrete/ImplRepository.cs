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
             return dbcontex.Problems.Include(i => i.Status).Include(i=>i.ChildTasks);
        }
        public Problem GetTask(int Id)
        {
            return null;
        }
        public void CreateTask(Problem _Task) { }
        public void EditTask(Problem _Task) { }
        public void DeleteTask(int Id) { }
    }
}
