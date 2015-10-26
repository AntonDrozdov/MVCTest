using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Model
{
    public class Problem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Performers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Laboriousness { get; set; }
        public int ActualExecutiontime { get; set; }
        public ICollection<Problem> ChildTasks { get; set; }
        public ICollection<Problem> ParentTasks { get; set; }


        public int? StatusId { get; set; }
        public Status Status { get; set; }

        public Problem()
        { 
            ChildTasks = new List<Problem>();
            ParentTasks = new List<Problem>();
        }
    }
}
