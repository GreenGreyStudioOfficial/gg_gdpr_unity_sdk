#if GG_METASERVER
using GreenGrey.Net;
#endif

namespace GreenGrey.GDPR
{
    public class GGGdprMetaServerDataProvider
    {
#if GG_METASERVER
        private static INetworkManager m_networkManager;
#endif

        public string mainToken
        {
            get
            {
#if GG_METASERVER
                return m_networkManager.authenticationProfile.mainToken;
#endif
                return string.Empty;
            }
        }

        public string authToken
        {
            get
            {
#if GG_METASERVER
                return m_networkManager.authenticationProfile.accessToken;
#endif
                return string.Empty;
            }
        }

#if GG_METASERVER
        public static void InjectNetworkManager(INetworkManager _networkManager)
        {
            m_networkManager = _networkManager;
        }
#endif
    }
}