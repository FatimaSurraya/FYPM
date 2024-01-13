using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FYPM.Models.ViewModel
{
    public class MeetingViewModel
    {

        [Display(Name = "Assigned To")]
        public List<int> SelectedAssignedTo { get; set; }

        [Display(Name = "Scheduled Date")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Meeting Type")]
        public string MeetingType { get; set; }
        public List<SelectListItem> AssignedTo { get; set; }
    }
}