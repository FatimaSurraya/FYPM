using FYPM.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Web.Mvc;

namespace FYPM.Controllers
{
    public class SmsController : Controller
    {
        private readonly ILogger<SmsController> _logger;
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService, ILogger<SmsController> logger)
        {
            _smsService = smsService;
            _logger = logger;
        }

        // Action for sending an SMS
        [HttpPost]
        public ActionResult SendSms(SendSmsRequestDTO model)
        {
            try
            {
                var response = _smsService.SendSms(model);
                if (response.Success)
                {
                    return Json(response);
                }
                else
                {
                    // Return a status code 400 (Bad Request) with the error message
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, response.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending SMS");
                // Return a status code 500 (Internal Server Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }
    }
}
