using FYPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYPM.Models.ViewModel;
using Ionic.Zip;
using System.IO;

namespace FYPM.Controllers
{
    public class TaskController : Controller
    {
        FYP_MSEntities1 dbContext = new FYP_MSEntities1();
        public ActionResult ListAllTasks()
        {
            List<Task> tasks = dbContext.Tasks.ToList();
            return View("Task", tasks);
        }

        public ActionResult AddTask()
        {
            var projects = GetProjects()
                .Select(x => new SelectListItem
                {
                    Value = x.ProjectId.ToString(),
                    Text = x.Title
                })
                .ToList();

            var assignedToOptions = GetAssignedToUsers()
                .Select(x => new SelectListItem
                {
                    Value = x.UserId.ToString(),
                    Text = string.Concat(x.FirstName, " ", x.LastName)
                })
                .ToList();

            var viewModel = new TaskViewModel
            {
                Projects = new SelectList(projects, "Value", "Text"),
                AssignedToOptions = assignedToOptions
            };

            return View("AddTask", viewModel);
        }

        [HttpPost]
        public JsonResult SaveTask(TaskViewModel task)
        {
            if (ModelState.IsValid && task.SelectedAssignedTo != null && task.SelectedAssignedTo.Count() > 0)
            {
                var project = dbContext.ProjectDetails.FirstOrDefault(x => x.ProjectId == task.SelectedProjectId);
                foreach (var item in task.SelectedAssignedTo)
                {
                    var taskDbo = new Task
                    {
                        TaskName = project.Title,
                        TaskDescription = project.Description,
                        TaskStatus = "Not Started",
                        DueDate = task.DueDate,
                        AssignedTo = item,
                        ProjectId = project.ProjectId
                    };
                    dbContext.Tasks.Add(taskDbo);
                }
                dbContext.SaveChanges();
                return Json(1);
            }
            return Json(-1);
        }

        public ActionResult EditTask(int id)
        {
            var task = dbContext.Tasks.FirstOrDefault(x => x.TaskId == id);
            var taskViewModel = new TaskViewModel
            {
                SelectedProjectId = task.ProjectId,
                SelectedAssignedTo = new List<int>() { task.AssignedTo },
                DueDate = (DateTime)task.DueDate
            };
            var projects = GetProjects()?.Select(x => new
            {
                ProjectId = x.ProjectId,
                Title = x.Title
            }).ToList();
            var assignedTo = GetAssignedToUsers()?.Select(x => new
            {
                UserId = x.UserId,
                FullName = string.Concat(x.FirstName, " ", x.LastName)
            }).ToList();
            taskViewModel.Projects = new SelectList(projects, "ProjectId", "Title");
            taskViewModel.AssignedTo = new MultiSelectList(assignedTo, "UserId", "FullName", taskViewModel.SelectedAssignedTo);
            return View("EditTask", taskViewModel);
        }

        private List<ProjectDetail> GetProjects()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            var projectList = dbContext.ProjectDetails.Where(x => x.SupervisorID == userId).ToList();
            return projectList;
        }

        private List<User> GetAssignedToUsers()
        {
            return dbContext.Users.Where(x => x.UserType.TypeName == "Student").ToList();
        }


        [HttpPost]
        public ActionResult DeleteTask(int id)
        {
            var task = dbContext.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task != null)
            {
                dbContext.Tasks.Remove(task);
                dbContext.SaveChanges();
            }
            return RedirectToAction("ListAllProjects");
        }

        public ActionResult ShowAllTasks()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            List<Task> tasks = dbContext.Tasks.Where(x => x.AssignedTo == userId).ToList();
            return View("StudentTask", tasks);
        }

        [HttpPost]
        public JsonResult UpdateTaskStatus(int taskId, int studentId, string newStatus)
        {
            var task = dbContext.Tasks.FirstOrDefault(t => t.TaskId == taskId && t.AssignedTo == studentId);
            if (task != null)
            {
                task.TaskStatus = newStatus;
                dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Task not found." });
        }


        public ActionResult DownloadDocuments(int projectId = 0)
        {
            var documents = dbContext.ProjectDocuments?.Where(x => x.ProjectId == projectId)?.Select(x => new
            {
                x.DocumentPath,
                x.DocumentName
            }).ToList();

            if (documents != null && documents.Count() > 0)
            {
                using (MemoryStream zipStream = new MemoryStream())
                {
                    using (ZipFile zipFile = new ZipFile())
                    {
                        foreach (var document in documents)
                        {
                            if (!string.IsNullOrEmpty(document.DocumentPath) && System.IO.File.Exists(document.DocumentPath))
                            {
                                byte[] fileBytes = System.IO.File.ReadAllBytes(document.DocumentPath);
                                string fileName = document.DocumentName;
                                zipFile.AddEntry(fileName, fileBytes);
                            }
                        }

                        zipFile.Save(zipStream);
                    }

                    string zipFileName = "taskdocuments.zip";

                    return File(zipStream.ToArray(), "application/zip", zipFileName);
                }
            }

            return HttpNotFound();
        }


    }
}