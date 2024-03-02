using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FYPM
{
    public class TwilioSmsService
    {
        private readonly string AccountSid;
        private readonly string tokenNumber;
        private readonly string phoneNumber;
      
        public TwilioSmsService(string AccountSid, string tokenNumber, string phoneNumber)
        {
            AccountSid = "AC66d0c98f4daedd97410a92307386c62b";
            tokenNumber = "ebea45833792178bf2ae72c7f9aaed9e";
            phoneNumber = "+16815324812";
           
        }

        public void SendMessage(string to, string message)
        {
            TwilioClient.Init(AccountSid, tokenNumber);

            MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(phoneNumber),
                to: new Twilio.Types.PhoneNumber(to)
            );
        }
    }
}
