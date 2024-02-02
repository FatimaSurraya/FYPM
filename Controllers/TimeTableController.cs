using FYPM.Models;
using FYPM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYPM.Controllers
{
    public class TimeTableController : Controller
    {
        FYP_MSEntities1 dbContext = new FYP_MSEntities1();

        public ActionResult ListAllTimetables()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            var role = Convert.ToString(Session["RoleName"]);
            List<Timetable> timetableDbos = dbContext.Timetables.Where(x => x.UserId == userId).ToList();

            if (role == "Student")
            {
                var projectId = dbContext.StudentProjectRequests.FirstOrDefault(x => x.UserId == userId && (bool)x.IsApproved)?.ProjectId;
                var supervisorId = dbContext.ProjectDetails.FirstOrDefault(x => x.ProjectId == projectId)?.SupervisorID;
                var supevisorTimetables = dbContext.Timetables.Where(x => x.UserId == supervisorId).ToList();
                foreach (var tt in supevisorTimetables)
                {
                    timetableDbos.Add(tt);
                }
            }
            var timetables = new List<TimetableViewModel>();
            if (timetableDbos != null && timetableDbos.Any())
            {
                foreach (var timetable in timetableDbos)
                {
                    var parts = timetable.Timetable1.Split('|');
                    var timetableViewModel = new TimetableViewModel
                    {
                        Date = DateTime.ParseExact(parts[0], "MM/dd/yyyy", CultureInfo.InvariantCulture),
                        DayOfWeek = parts[1],
                        Time = TimeSpan.Parse(parts[2]),
                        Event = parts[3],
                        Room = parts[4],
                        TimetableId = timetable.TimetableId
                    };
                    timetables.Add(timetableViewModel);
                }
            }
            return View("Timetable", timetables);
        }

        public ActionResult AddTimetable()
        {
            return View("AddTimetable");
        }

        [HttpPost]
        public JsonResult SaveTimetable(TimetableViewModel timetable)
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            if (ModelState.IsValid)
            {
                var timetableDbo = new Timetable
                {
                    UserId = userId,
                    Timetable1 = $"{timetable.Date.ToString("MM/dd/yyyy")}|{timetable.DayOfWeek}|{timetable.Time.ToString()}|{timetable.Event}|{timetable.Room}"
                };
                dbContext.Timetables.Add(timetableDbo);
                dbContext.SaveChanges();
                return Json(1);
            }
            return Json(-1);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var timetable = dbContext.Timetables.FirstOrDefault(t => t.TimetableId == id);

            if (timetable != null)
            {
                dbContext.Timetables.Remove(timetable);
                dbContext.SaveChanges();
            }
            return RedirectToAction("ListAllTimetables");
        }
    }
}