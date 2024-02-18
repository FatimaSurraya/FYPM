using FYPM.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FYPM.Controllers
{
    public class SmsController : Controller
    {
        
        public ActionResult Sms()
        {
            string accountSid = ConfigurationManager.AppSettings["AC66d0c98f4daedd97410a92307386c62b"];
            string authToken = ConfigurationManager.AppSettings["7a5063806b1433de80234d635e7e25bd"];
            string TwilioPhoneNumber = ConfigurationManager.AppSettings["+16815324812"];
         
        TwilioClient.Init(accountSid, authToken);

            try
            {
                // Send an SMS message
                var message = MessageResource.Create(
                    body: "Test message from Twilio!",
                    from: new Twilio.Types.PhoneNumber(TwilioPhoneNumber),
                    to: new Twilio.Types.PhoneNumber("+14243533831")
                );

                // Optionally, you can handle the response here, log it, etc.
                // For example:
                ViewBag.MessageSid = message.Sid;
                ViewBag.MessageStatus = message.Status;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during message sending
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }
    }
}
