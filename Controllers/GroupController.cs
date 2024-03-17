using FYPM.Models;
using RMS.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FYPM.Controllers
{
    public class GroupController : Controller
    {

        FYP_MSEntities1 db1 = new FYP_MSEntities1();
        // GET: CreateGroup
        public ActionResult Group(Group group)
        {
            List<Group> groups = db1.Groups.ToList(); // Fetch all groups from the database
            return View(groups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(int groupId, string groupName)
        {
            try
            {
                if (!string.IsNullOrEmpty(groupName))
                {
                    Group group = new Group
                    {
                        GroupId = groupId,
                        GroupName = groupName,
                    };

                    db1.Groups.Add(group); // Add the newly created group to the context
                    db1.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    // Return JSON with error message if group name is empty
                    return Json(new { success = false, errorMessage = "Group name cannot be empty" });
                }
            }
            catch (Exception ex)
            {
                // Return JSON with error message if an exception occurs
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }



        public ActionResult SendJoinRequest()
        {
            

            return View();
        }

        public ActionResult ApproveJoinRequest()
        {
           
            return View();
        }
       
}
    }




















