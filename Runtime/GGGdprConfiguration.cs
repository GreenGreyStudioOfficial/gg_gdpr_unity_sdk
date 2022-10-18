using UnityEngine;

namespace GreenGrey.GDPR
{
    public class GGGdprConfiguration : MonoBehaviour, IGGGdprConfiguration
    {
        [SerializeField] private string m_serviceUrl;
        [SerializeField] private int m_requestTimeout;
        [SerializeField] private int m_requestAttempts;
        [SerializeField] private bool m_withLogs = true;

        public string serviceUrl => m_serviceUrl;
        public int requestTimeout => m_requestTimeout;
        public int requestAttempts => m_requestAttempts;
        public bool withLogs => m_withLogs;
    }
}