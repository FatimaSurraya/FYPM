using FYPM.Models;
using FYPM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYPM.Controllers
{
    public class ResultController : Controller
    {
        FYP_MSEntities1 dbContext = new FYP_MSEntities1();
        public ActionResult ListAllResults()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            List<Result> results = dbContext.Results.Where(x => x.SupervisorID == userId).ToList();
            return View("Result", results);
        }

        public ActionResult ListAllStudentResults()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            List<Result> results = dbContext.Results.Where(x => x.StudentID == userId).ToList();
            return View("StudentResult", results);
        }

        public ActionResult AddResult()
        {
            var assignedTo = GetAssignedToUsers()?.Select(x => new SelectListItem
            {
                Value = x.UserId.ToString(),
                Text = string.Concat(x.FirstName, " ", x.LastName)
            }).ToList();

            var viewModel = new ResultViewModel
            {
                AssignedTo = assignedTo
            };

            return View("AddResult", viewModel);
        }

        [HttpGet]
        public JsonResult GetTasks(int assignedToId)
        {
            var tasks = dbContext.Tasks.Where(x => x.AssignedTo == assignedToId).Select(x => new SelectListItem
            {
                Value = x.TaskId.ToString(),
                Text = x.TaskName.ToString()
            }).ToList();
            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveResult(ResultViewModel result)
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            if (ModelState.IsValid && result.SelectedAssignedTo != 0)
            {
                var resultDbo = new Result
                {
                    TaskId = result.SelectedTask,
                    SupervisorID = userId,
                    StudentID = result.SelectedAssignedTo,
                    Score = result.Score,
                };
                dbContext.Results.Add(resultDbo);
                SendResultUpdate(result.SelectedAssignedTo, dbContext.Tasks.FirstOrDefault(x => x.TaskId == result.SelectedTask).TaskName);
                dbContext.SaveChanges();
                return Json(1);
            }
            return Json(-1);
        }

        [HttpGet]
        public JsonResult GetTaskStatus(int taskId)
        {
            var taskStatus = dbContext.Tasks.FirstOrDefault(x => x.TaskId == taskId)?.TaskStatus;
            return Json(new { TaskStatus = taskStatus }, JsonRequestBehavior.AllowGet);
        }

        private List<User> GetAssignedToUsers()
        {
            return dbContext.Users.Where(x => x.UserType.TypeName == "Student").ToList();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = dbContext.Results.FirstOrDefault(t => t.ResultId == id);

            if (result != null)
            {
                dbContext.Results.Remove(result);
                dbContext.SaveChanges();
            }
            return RedirectToAction("ListAllResults");
        }

        public void SendResultUpdate(int studentId, string msg)
        {
            var userId = Convert.ToInt32(Session["UserID"]);

            var message = new Message
            {
                MessageText = "Your result for " + msg + " has been uploaded, please check your result dashboard.",
                SenderId = userId,
                ReceiverId = studentId,
                MessageDate = DateTime.Now
            };
            dbContext.Messages.Add(message);
        }

    }
}