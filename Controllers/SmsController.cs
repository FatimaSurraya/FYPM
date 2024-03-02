using FYPM.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FYPM.Controllers
{
    public class SmsController : Controller
    {   public readonly TwilioSmsService smsService;
        public SmsController()
        {
            smsService = new TwilioSmsService(
                ConfigurationManager.AppSettings["TwilioAccountSid"],
                ConfigurationManager.AppSettings["TwilioAuthToken"],
                 ConfigurationManager.AppSettings["TwilioPhoneNumber"]
            );
        }
        [HttpGet]
        public ActionResult SendSms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendSms(string to, string message)
        {
           smsService.SendMessage(to, message);
            return Content("SMS sent successfully!");
        }
       
       
    }
}
   