using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FYPM.Models.ViewModel
{
    public class MessageViewModel
    {
        [Display(Name = "Assigned To")]
        public List<int> SelectedAssignedTo { get; set; }
        public List<SelectListItem> AssignedTo { get; set; }
        public List<Message> Messages { get; set; }
      
    }
}