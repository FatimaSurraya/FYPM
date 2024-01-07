using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYPM.Models.ViewModel
{
    public class ResultViewModel
    {
        public int SelectedAssignedTo { get; set; }
        public int SelectedTask { get; set; }
        public int Score { get; set; }
        public string TaskStatus { get; set; }

        public List<SelectListItem> Task { get; set; }
        public List<SelectListItem> AssignedTo { get; set; }
    }
}