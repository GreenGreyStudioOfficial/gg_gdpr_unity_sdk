using System.Collections.Generic;

namespace GreenGrey.GDPR.Command
{
    public abstract class BaseGdprCommand
    {
        public bool success { get; internal set; }
        public string errorMessage { get; internal set; }
        public long statusCode { get; internal set; }
        public abstract string method { get; }

        public Dictionary<string, object> requestParams { get; } = new Dictionary<string, object>();

        public abstract override string ToString();
        internal virtual bool IsValid() => true;
    }
}