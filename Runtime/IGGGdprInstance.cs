using System.Threading.Tasks;
using GreenGrey.Analytics;
using GreenGrey.GDPR.Command;

namespace GreenGrey.GDPR
{
    public interface IGGGdprInstance
    {
        void Setup(IGGGdprConfiguration _configuration, IGGGdprLogger _logger, IGGAnalytics _analytics);
        Task<GGGdprResponseType> ExecuteCommand(BaseGdprCommand _gdprCommand);
    }
}