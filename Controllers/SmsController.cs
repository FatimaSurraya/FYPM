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
    {  public ActionResult GetSms()
        {
            return View();
        }
        
        public ActionResult Create()
        {
            string accountSid = "AC66d0c98f4daedd97410a92307386c62b";
            string authToken = "ebea45833792178bf2ae72c7f9aaed9e";
            string phone = "+16815324812";

            TwilioClient.Init(accountSid, authToken);

            try
            {
                // Send an SMS message
                var message = MessageResource.Create(
                    body: "Test message from Twilio!",
                    from: new PhoneNumber(phone),
                    to: new PhoneNumber("+14243533831") // Replace with your desired recipient phone number
                );

                // If the message was sent successfully
                ViewData["Success"] = message.Sid;
            }
            catch (Exception ex)
            {
                // If there was an error sending the message
                ViewData["Error"] = "Error sending SMS: " + ex.Message;
            }

            return View();
        }
    }
}
   