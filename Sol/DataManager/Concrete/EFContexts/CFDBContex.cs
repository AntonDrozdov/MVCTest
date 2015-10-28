using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;

using DataManager.Model;

namespace DataManager.Concrete.EFContexts
{
    public class CFDBContex : DbContext
    {
        public DbSet<Problem> Problems { get; set; }
        //public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Problem>().HasMany(c => c.ChildTasks)
                .WithMany(s => s.ParentTasks)
                .Map(t => t.MapLeftKey("ParentTask")
                .MapRightKey("ChildTask")
                .ToTable("TaskHierarchy"));
        }
    }
}
