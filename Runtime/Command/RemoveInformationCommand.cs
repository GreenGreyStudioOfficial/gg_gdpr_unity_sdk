namespace GreenGrey.GDPR.Command
{
    public class RemoveInformationCommand : BaseGdprCommand
    {
        public override string method => "remove";
        
        public override string ToString() => "Remove GDPR information";
        
        internal override GGGdprResponseType GetResponseType()
        {
            return statusCode == 202 ? 
                GGGdprResponseType.REMOVE_ALREADY_IN_PROGRESS :
                GetDefaultResponseType();
        }
    }
}