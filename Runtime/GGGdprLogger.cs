using UnityEngine;

namespace GreenGrey.GDPR
{
    public class GGGdprLogger : IGGGdprLogger
    {
        private const string LOG_PREFIX = "[GDPR]:";
        
        private IGGGdprConfiguration m_gdprConfiguration;

        public GGGdprLogger(IGGGdprConfiguration _gdprConfiguration)
        {
            m_gdprConfiguration = _gdprConfiguration;
        }

        public void Log(string _text)
        {
            if (!m_gdprConfiguration.withLogs)
                return;
            
            Debug.Log($"{LOG_PREFIX} {_text}");
        }

        public void Warning(string _text)
        {
            if (!m_gdprConfiguration.withLogs)
                return;
            
            Debug.LogWarning($"{LOG_PREFIX} {_text}");
        }

        public void Error(string _text)
        {
            if (!m_gdprConfiguration.withLogs)
                return;
            
            Debug.LogError($"{LOG_PREFIX} {_text}");
        }
    }
}