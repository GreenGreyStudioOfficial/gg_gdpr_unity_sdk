using GreenGrey.Analytics;
using UnityEngine;

namespace GreenGrey.GDPR
{
    [DefaultExecutionOrder(-10)]
    public class GGGdprComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad (this.gameObject);
            
            var configuration = GetComponent<GGGdprConfiguration>();
            var logger = new GGGdprLogger(configuration);
            GGGdpr.Instance.Setup(configuration, logger, GGAnalytics.Instance);
        }
    }
}