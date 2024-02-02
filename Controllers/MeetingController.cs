using FYPM.Models;
using FYPM.Models.ViewModel;
using FYPM.Services.Agora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYPM.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        FYP_MSEntities1 dbContext = new FYP_MSEntities1();

        public ActionResult ListAllMeetings()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            List<Meeting> meetings = dbContext.Meetings.Where(x => x.SupervisorID == userId).ToList();
            return View("Meetings", meetings);
        }

        public ActionResult ListAllStudentMeetings()
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            List<Meeting> meetings = dbContext.Meetings.Where(x => x.StudentID == userId).ToList();
            return View("Meetings", meetings);
        }
        public ActionResult StartMeeting(string channel) {
            AgoraTokenBuilderService agoraTokenBuilderService = new AgoraTokenBuilderService();
            var userId = Session["UserID"].ToString();
            var appId = "5c84b8b3638042ff9358081f81682120";
            var certificateId = "78159623a565417696a509234e9eb7c6";
            uint privilegeExpiredTs = (uint)agoraTokenBuilderService.ConvertToUnixTimestamp(DateTime.Now) + 86400;
            string token = agoraTokenBuilderService.buildRTCToken(appId,certificateId, channel, userId, privilegeExpiredTs);
            StartMeetingViewModel model = new StartMeetingViewModel
            {
                AppId = appId,
                Token = token,
                ChannelName = channel,
                Uid = userId
            };

            return View(model);
        }
        public ActionResult AddMeeting()
        {

            var assignedTo = GetAssignedToUsers()?.Select(x => new SelectListItem
            {
                Value = x.UserId.ToString(),
                Text = string.Concat(x.FirstName, " ", x.LastName)
            }).ToList();

            var viewModel = new MeetingViewModel
            {
                AssignedTo = assignedTo
            };

            return View("AddMeetings", viewModel);
        }


        [HttpPost]
        public JsonResult SaveMeeting(MeetingViewModel meeting)
        {
            if (ModelState.IsValid && meeting.SelectedAssignedTo != null && meeting.SelectedAssignedTo.Count() > 0)
            {
                foreach (var item in meeting.SelectedAssignedTo)
                {
                    var MeetingDbo = new Meeting
                    {
                        MeetingType = meeting.MeetingType,
                        StudentID = item,
                        SupervisorID = Convert.ToInt32(Session["UserID"]),
                        ScheduledDate = meeting.ScheduledDate,
                    };
                    dbContext.Meetings.Add(MeetingDbo);
                }
                dbContext.SaveChanges();
                return Json(1);
            }
            return Json(-1);
        }

        private List<User> GetAssignedToUsers()
        {
            return dbContext.Users.Where(x => x.UserType.TypeName == "Student").ToList();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var meeting = dbContext.Meetings.FirstOrDefault(t => t.MeetingId == id);

            if (meeting != null)
            {
                dbContext.Meetings.Remove(meeting);
                dbContext.SaveChanges();
            }
            return RedirectToAction("ListAllMeetngs");
        }

        public ActionResult SendMeetingUpdate(int supervisorId, string msg)
        {
            var userId = Convert.ToInt32(Session["UserID"]);
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == userId);

            var message = new Message
            {
                MessageText = user.FirstName + " " + user.LastName + " " + msg + " meeting.",
                SenderId = user.UserId,
                ReceiverId = supervisorId,
                MessageDate = DateTime.Now
            };
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();
            return Json(1);
        }
    }
}