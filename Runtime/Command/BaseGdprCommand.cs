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
        internal abstract GGGdprResponseType GetResponseType();
        internal virtual bool IsValid() => true;

        protected GGGdprResponseType GetDefaultResponseType()
        {
            switch (statusCode)
            {
                case 202:
                    return GGGdprResponseType.SUCCESS;
                case 401:
                    return GGGdprResponseType.META_SERVER_AUTH_ERROR;
                case 402:
                    return GGGdprResponseType.REQUEST_VALIDATION_ERROR;
            }
            
            if (statusCode > 200 && statusCode <= 299)
                return GGGdprResponseType.SUCCESS;

            if (statusCode >= 400 && statusCode <= 599)
                return GGGdprResponseType.EROR;

            return GGGdprResponseType.UNKNOWN_RESPONSE_TYPE;
        }
    }
}