using System;
using Newtonsoft.Json;

namespace GreenGrey.GDPR
{
    [Serializable]
    public struct GGGdprMetaServiceData
    {
        [JsonProperty("mainToken")] private string m_mainToken;
        [JsonProperty("accessToken")] private string m_accessToken;

        public GGGdprMetaServiceData(string _mainToken, string _accessToken)
        {
            m_mainToken = _mainToken;
            m_accessToken = _accessToken;
        }
    }
}