using FYPM.Models;
using FYPM.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FYPM.Controllers
{
    public class MessagesController : Controller
    {
        FYP_MSEntities1 dbContext = new FYP_MSEntities1();
        // GET: Messages
        public ActionResult Message()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            var userRole = Session["RoleName"].ToString();

            var messages = dbContext.Messages.Where(x => x.ReceiverId == userId).ToList();
            foreach (var message in messages)
            {
                var user = dbContext.Users.FirstOrDefault(x => x.UserId == message.SenderId);
                message.SenderName = user.FirstName + " " + user.LastName + "  " + user.CustomId;
            }

            var viewModel = new MessageViewModel();
            if (userRole == "Supervisor")
            {
                var assignedTo = dbContext.Users.Where(x => x.UserId != userId && x.UserType.TypeName == "Student")?.Select(x => new SelectListItem
                {
                    Value = x.UserId.ToString(),
                    Text = string.Concat(x.FirstName, " ", x.LastName)
                })
                .ToList();

                viewModel = new MessageViewModel
                {
                    Messages = messages,
                    AssignedTo = assignedTo
                };
            }

            if (userRole == "Student")
            {
                var assignedTo = dbContext.Users.Where(x => x.UserId != userId && x.UserType.TypeName == "Supervisor")?.Select(x => new SelectListItem
                {
                    Value = x.UserId.ToString(),
                    Text = string.Concat(x.FirstName, " ", x.LastName)
                })
                .ToList();

                viewModel = new MessageViewModel
                {
                    Messages = messages,
                    AssignedTo = assignedTo
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SendMessage(List<int> SelectedAssignedTo, string Message)
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            if (SelectedAssignedTo != null && SelectedAssignedTo.Count() > 0)
            {
                foreach (var receiverId in SelectedAssignedTo)
                {
                    var message = new Message
                    {
                        MessageText = Message,
                        SenderId = userId,
                        ReceiverId = receiverId,
                        MessageDate = DateTime.Now
                    };
                    dbContext.Messages.Add(message);
                }
                dbContext.SaveChanges();
                return Json(1);
            }
            return Json(-1);
        }
    }
}