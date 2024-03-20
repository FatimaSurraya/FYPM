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

        // POST: Group/SendJoinRequest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendJoinRequest(Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                db1.Invitations.Add(invitation); // Add the newly created group to the context
                db1.SaveChanges();
                // For demonstration purposes, let's just return a success message
                TempData["Message"] = "Join request sent successfully!";
                return RedirectToAction("Index", "Home"); // Redirect to a different action after successful submission
            }

            // If ModelState is not valid, return to the form view to display validation errors
            return View(invitation);
        }
        public ActionResult AddMembers(int groupId)
        {
            var group = db1.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group == null)
            {
                return HttpNotFound(); // Return 404 if group not found
            }

            // Fetch available users to add to the group
            var availableUsers = db1.Users.ToList(); // Assuming you have a Users table

            // Provide group details and list of available users to add to the group
            var viewModel = new group_users
            {
                Group = group,
                     
            };

            return View(viewModel);
        }

        // POST: Group/AddMembers
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMembers(int groupId, List<int> userIds)
        {
            var group = db1.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group == null)
            {
                return HttpNotFound(); // Return 404 if group not found
            }

            if (userIds != null)
            {
                // Add selected users to the group
                foreach (var userId in userIds)
                {
                    var groupUser = new group_users
                    {
                        group_id = groupId, // Assign the group_id, not the group object
                        user_id = userId,
                        role = "Member", // Assuming role is hard-coded to "Member"
                        isApproved = true, // Assuming users are automatically approved
                    };
                    db1.group_users.Add(groupUser);
                }
                db1.SaveChanges();

                TempData["Message"] = "Members added successfully!";
            }
            else
            {
                TempData["Error"] = "No members selected!";
            }

            return RedirectToAction("AddMembers", new { groupId = groupId });
        }




        public ActionResult ApproveJoinRequest()
        {
           
            return View();
        }
       
}
    }




















