namespace GreenGrey.GDPR.Command
{
    public class RemoveInformationCommand : BaseGdprCommand
    {
        public override string method => "remove";
        
        public override string ToString() => "Remove GDPR information";
    }
}