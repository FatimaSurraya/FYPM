using FYPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYPM.Controllers
{
    public class UsersController : Controller

    {
        FYP_MSEntities db1 = new FYP_MSEntities();
        // GET: Users
        // GET: Users
        // GET: Users
        public ActionResult UsersDetail()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            return View(db1.Users.Where(x => x.UserId != userId && x.UserType.Type!="Admin").ToList());
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = GetUserById(id);
            var meetings = db1.Meetings.Where(x => x.StudentID == id || x.SupervisorID == id);
            var messages = db1.Messages.Where(x => x.ReceiverId == id || x.SenderId == id);
            var timetables = db1.Timetables.Where(x => x.UserId == id);
            var results = db1.Results.Where(x => x.StudentID == id || x.SupervisorID == id);
            var projects = db1.ProjectDetails.Where(x => x.SupervisorID == id);
            db1.Results.RemoveRange(results);
            foreach (var project in projects)
            {
                var tasks = db1.Tasks.Where(x => x.ProjectId == project.ProjectId);
                db1.Tasks.RemoveRange(tasks);
            }
            db1.SaveChanges();
            db1.ProjectDetails.RemoveRange(projects);
            db1.SaveChanges();
            db1.Timetables.RemoveRange(timetables);
            db1.Messages.RemoveRange(messages);
            db1.Meetings.RemoveRange(meetings);
            db1.Users.Remove(user);
            db1.SaveChanges();
            return RedirectToAction("UsersDetail");
        }

        private User GetUserById(int id)
        {
            return db1.Users.FirstOrDefault(u => u.UserId == id);
        }

    }
}