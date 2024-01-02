using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FYPM.Models.ViewModel
{
    public class TaskViewModel
    {
        internal int TaskId;

        [Display(Name = "Project")]
        public int SelectedProjectId { get; set; }

        [Display(Name = "Assigned To")]
        public List<int> SelectedAssignedTo { get; set; }

        // Add this property to store the dynamic options
        public List<SelectListItem> AssignedToOptions { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Display(Name = "Task Status")]
        public string TaskStatus { get; set; }
        public SelectList Projects { get; set; }
        public MultiSelectList AssignedTo { get; set; }
    }
}