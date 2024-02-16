using FYPM.Models;
using FYPM.Models.ViewModel;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;
using Twilio.Types;


namespace FYPM
{
    public class TwilioSmsService : ISmsService
    {
        private readonly string accountSid = "AC66d0c98f4daedd97410a92307386c62b";
        private readonly string authToken = "[7a5063806b1433de80234d635e7e25bd]";
        private readonly string phNumber = "+16815324812";
        private readonly string serviceSid = "[AC66d0c98f4daedd97410a92307386c62b]";
        private readonly ILogger _logger;

        public TwilioSmsService(ILogger<MessageViewModel> logger)
        {
            _logger = logger;
        }

        public ResponseDTO<bool> SendSms(SendSmsRequestDTO model)
        {
            // Replace - from mobile number
            var newMobile = model.to.Replace("-", "");
            var newMessage = model.message;
            // Add country code
            var number = $"+1{newMobile}";

            try
            {
                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                    body: newMessage,
                    from: new PhoneNumber(phNumber),
                    to: new PhoneNumber(number)
                );

                // Use Responses.OK for success
                return Responses.OK("SMS sent successfully", true);
            }
            catch (ApiException ex)
            {
                _logger.LogError(exception: ex, "SMS Service Error: " + ex.Message);

                // Explicitly specify type argument for Error method
                return Responses.Error<bool>("Failed to send SMS");
            }
        }

    }
}
