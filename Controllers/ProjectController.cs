using FYPM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Ionic.Zip;
using FYPM.Models.ViewModel;

namespace FYPM.Controllers
{
    public class ProjectController : Controller                         
    {
        FYP_MSEntities1 dbContext = new FYP_MSEntities1();
       
        public ActionResult UploadProject()
        {
            return View();
        }
   
        [HttpPost]
        public JsonResult InsertProject(ProjectDetail projectDetail, List<HttpPostedFileBase> UploadDocuments)
        {
            // Check if projectDetail is valid
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            try
            {
                projectDetail.SupervisorID = Convert.ToInt32(Session["UserID"]);
                projectDetail.CreatedDate = DateTime.Now;

                var user = dbContext.Users.FirstOrDefault(x => x.UserId == projectDetail.SupervisorID);
                projectDetail.User = user;

                dbContext.ProjectDetails.Add(projectDetail);
                dbContext.SaveChanges();

                if (UploadDocuments != null && UploadDocuments.Any())
                {
                    foreach (var file in UploadDocuments)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            // Validate file size and type
                            if (file.ContentLength > MaxFileSizeInBytes || !IsFileTypeAllowed(file.FileName))
                            {
                                // Handle invalid file (return an error response or perform necessary actions)
                                continue; // Skip processing this file
                            }

                            // Generate a unique file name to prevent overwriting
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                            string documentPath = Path.Combine(Server.MapPath("~/Uploads/"), uniqueFileName);

                            file.SaveAs(documentPath);

                            var projectDocument = new ProjectDocument
                            {
                                ProjectId = projectDetail.ProjectId,
                                DocumentName = uniqueFileName, // Save the unique filename in the database
                                DocumentPath = documentPath
                            };

                            dbContext.ProjectDocuments.Add(projectDocument);
                            dbContext.SaveChanges();
                        }
                    }
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Handle and return an appropriate error response
                return Json(new { success = false, error = "An error occurred while processing the request." });
            }
        }

        // Validation helper methods (can be placed in the same controller or a separate utility class)
        private bool IsFileTypeAllowed(string fileName)
        {
            string[] allowedExtensions = { ".pdf", ".doc", ".docx" }; // Example allowed file extensions
            string fileExtension = Path.GetExtension(fileName);
            return allowedExtensions.Contains(fileExtension.ToLower());
        }

        private const int MaxFileSizeInBytes = 10 * 1024 * 1024; // Example maximum file size (10MB)

        [HttpGet]
        public ActionResult EditProject(int id)
        {
            ProjectDetail projectDetail = dbContext.ProjectDetails.FirstOrDefault(x => x.ProjectId == id);

            if (projectDetail == null)
            {
                return HttpNotFound();
            }

            return View(projectDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject(ProjectDetail projectDetail, List<HttpPostedFileBase> UploadDocuments)
        {
            if (ModelState.IsValid)
            {
                ProjectDetail existingProject = dbContext.ProjectDetails.Find(projectDetail.ProjectId);

                if (existingProject == null)
                {
                    return HttpNotFound();
                }

                existingProject.Title = projectDetail.Title;
                existingProject.Description = projectDetail.Description;
                existingProject.Skills = projectDetail.Skills;
                existingProject.Domain = projectDetail.Domain;
                existingProject.Tools = projectDetail.Tools;
                existingProject.StudentsAllowed = projectDetail.StudentsAllowed;

                if (UploadDocuments != null && UploadDocuments.Any())
                {
                    foreach (var file in UploadDocuments)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            string documentPath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);

                            file.SaveAs(documentPath);

                            var projectDocument = new ProjectDocument
                            {
                                ProjectId = projectDetail.ProjectId,
                                DocumentName = fileName,
                                DocumentPath = documentPath
                            };

                            dbContext.ProjectDocuments.Add(projectDocument);
                        }
                    }
                }
                dbContext.SaveChanges();

                return RedirectToAction("ListAllProjects");
            }
            return View(projectDetail);
        }
        public ActionResult ListAllProjects()
        {
            var role = Convert.ToString(Session["RoleName"]);
            List<ProjectDetail> projectList = new List<ProjectDetail>();
            if (role == "Supervisor")
            {
                var userId = Convert.ToInt32(Session["UserID"]);
                projectList = dbContext.ProjectDetails.Where(x => x.SupervisorID == userId).ToList();
            }
            return View("ProjectGrid", projectList);
        }
        public ActionResult DeleteDocument(int documentId, int projectId)
        {
            var document = dbContext.ProjectDocuments.FirstOrDefault(x => x.DocumentId == documentId);
            if (document != null)
            {
                dbContext.ProjectDocuments.Remove(document);
                dbContext.SaveChanges();
            }
            return RedirectToAction("EditProject", new { id = projectId });
        }


        public ActionResult DownloadExistingDocument(string documentFileName)
        {
            string documentPath = Server.MapPath("~/Uploads/" + documentFileName);
            if (!System.IO.File.Exists(documentPath))
            {
                return HttpNotFound();
            }
            string contentType = MimeMapping.GetMimeMapping(documentPath);
            return File(documentPath, contentType, documentFileName);
        }


        #region student
        public ActionResult ListAllStudentProjects()
        {
            return View("StudentProjectGrid", dbContext.ProjectDetails.ToList());
        }

        public ActionResult ShowAllTasks()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            List<Task> tasks = dbContext.Tasks.Where(x => x.AssignedTo == userId).ToList();
            return View("StudentTask", tasks);
        }

        public ActionResult DownloadDocuments(int projectId)
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

                    string zipFileName = "task_documents.zip";

                    return File(zipStream.ToArray(), "application/zip", zipFileName);
                }
            }

            return HttpNotFound();
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

        public ActionResult ProjectRequest(int projectId)
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            var project = dbContext.ProjectDetails.FirstOrDefault(p => p.ProjectId == projectId);

            var message = new Message
            {
                MessageText = user.FirstName + " " + user.LastName + " requested for the " + project.Title + " project.",
                SenderId = user.UserId,
                ReceiverId = project.SupervisorID,
                MessageDate = DateTime.Now
            };
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();
            return Json(1);
        }

        #endregion

        //Requests approvals 

        public ActionResult ProjectRequests()
        {
            

            return View();
        }

       
    }



}