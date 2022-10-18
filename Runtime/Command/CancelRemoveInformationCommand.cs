namespace GreenGrey.GDPR.Command
{
    public class CancelRemoveInformationCommand : BaseGdprCommand
    {
        public override string method => "cancel";
        
        public override string ToString() => "Cancel remove GDPR information";
    }
}