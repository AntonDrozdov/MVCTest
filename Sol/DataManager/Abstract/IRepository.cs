using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.Model;

namespace DataManager.Abstract
{
    public interface IRepository
    {
        IQueryable<Problem> GetTasks();
        Problem GetTask(int Id);
        void CreateTask(Problem _Task);
        void EditTask(Problem _Task);
        void DeleteTask(int Id);
    }
}
