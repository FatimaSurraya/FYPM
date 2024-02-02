using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYPM.Models.ViewModel
{
    public class TimetableViewModel
    {
        public int TimetableId { get; set; }
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan Time { get; set; }
        public string Event { get; set; }
        public string Room { get; set; }
    }
}