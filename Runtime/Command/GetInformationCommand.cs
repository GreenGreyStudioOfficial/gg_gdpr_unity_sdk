using System.Net.Mail;

namespace GreenGrey.GDPR.Command
{
    public class GetInformationCommand : BaseGdprCommand
    {
        public override string method => "get";

        public GetInformationCommand(string _emailToSendInformation)
        {
            requestParams["email"] = _emailToSendInformation;
        }

        public override string ToString() => "Get GDPR information";
        
        internal override GGGdprResponseType GetResponseType()
        {
            return statusCode == 400 ? GGGdprResponseType.UNKNOWN_PLATFORM : GetDefaultResponseType();
        }

        internal override bool IsValid()
        {
            var email = requestParams["email"].ToString();
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith(".")) {
                return false;
            }
            try {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch {
                return false;
            }
        }
    }
}