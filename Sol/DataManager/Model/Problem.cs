using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataManager.Model
{
    public class Problem
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
       
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Title { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Performers { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, 2000, ErrorMessage = "Недопустимое количество часов")]
        public int Laboriousness { get; set; }

        [Required]
        [Range(1, 2000, ErrorMessage = "Недопустимое количество часов")]
        public int ActualExecutiontime { get; set; }

        public ICollection<Problem> ChildTasks { get; set; }
        public ICollection<Problem> ParentTasks { get; set; }

        public string Status { get; set; }

        public Problem()
        { 
            ChildTasks = new List<Problem>();
            ParentTasks = new List<Problem>();
        }
    }
}
