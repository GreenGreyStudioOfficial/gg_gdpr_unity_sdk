using System.Collections.Generic;
using System.Threading.Tasks;
using GreenGrey.Analytics;
using GreenGrey.Analytics.User;
using GreenGrey.GDPR.Command;
using Newtonsoft.Json;
using UnityEngine;

namespace GreenGrey.GDPR
{
    internal class GGGdprInstance : IGGGdprInstance
    {
        private readonly GGGdprMetaServerDataProvider m_serverDataProvider = new GGGdprMetaServerDataProvider();
        private readonly UserIdPrefsFileManager m_userIdPrefsManager = new UserIdPrefsFileManager();
        private readonly NetworkSender m_networkSender = new NetworkSender();

        private IGGGdprConfiguration m_configuration;
        private IGGGdprLogger m_logger;
        private IGGAnalytics m_ggAnalytics;
        
        public void Setup(IGGGdprConfiguration _configuration, IGGGdprLogger _logger, IGGAnalytics _analytics)
        {
            m_configuration = _configuration;
            m_logger = _logger;
            m_ggAnalytics = _analytics;
        }

        public async Task<GGGdprResponseType> ExecuteCommand(BaseGdprCommand _gdprCommand)
        {
            if (!_gdprCommand.IsValid())
            {
                _gdprCommand.errorMessage = "Not valid query";
                _gdprCommand.success = false;
                _gdprCommand.statusCode = 402;

                m_logger.Error($"{_gdprCommand} request is not valid.");
                return _gdprCommand.GetResponseType();
            }
            
            FillGlobalParameters(_gdprCommand.requestParams);

            var url = $"{m_configuration.serviceUrl}/{_gdprCommand.method}";
            var json = SerializeCommandParams(_gdprCommand.requestParams);
            
            m_logger.Log($"Send {_gdprCommand} request to:\nURL: {url}\nJSON: {json}");

            var requestResponse = await m_networkSender.SendRequestAsync(url, json, m_configuration.requestTimeout,
                m_configuration.requestAttempts);

            _gdprCommand.statusCode = requestResponse.responseCode;

            if (requestResponse.success)
            {
                _gdprCommand.success = true;
                
                m_logger.Log($"{_gdprCommand} request success. Code: {_gdprCommand.statusCode}");
                return _gdprCommand.GetResponseType();
            }
            
            _gdprCommand.errorMessage = requestResponse.error;
            _gdprCommand.success = false;
            
            m_logger.Error($"{_gdprCommand} request failed. Code: {_gdprCommand.statusCode}\nError message: {_gdprCommand.errorMessage}");
            return _gdprCommand.GetResponseType();
        }

        private void FillGlobalParameters(Dictionary<string, object> _parameters)
        {
            var deviceInfo = m_ggAnalytics.GetDeviceInfo();

            _parameters["userId"] = m_userIdPrefsManager.LoadUserId();
            _parameters["bundle_id"] = deviceInfo.AppPackageName.ToLower();
            _parameters["platform"] = GetApplicationPlatform();
            _parameters["serverData"] = new GGGdprMetaServiceData(m_serverDataProvider.mainToken, m_serverDataProvider.authToken);
        }

        private string SerializeCommandParams(Dictionary<string, object> _params)
        {
            return JsonConvert.SerializeObject(_params, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None
            });
        }

        private string GetApplicationPlatform()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    return "android";
                case RuntimePlatform.IPhonePlayer:
                    return "ios";
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                    return "editor";
            }

            m_logger.Error($"Not supported Application platform {Application.platform}");
            return string.Empty;
        }
    }
}