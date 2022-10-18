using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace GreenGrey.GDPR
{
    public class NetworkSender
    {
        public struct RequestResponse
        {
            public bool success { get; set; }
            public string error { get; set; }
            public long responseCode { get; set; }
            public string response { get; set; }
            public int attemptsUsed { get; set; }
        }
        
        public async Task<RequestResponse> SendRequestAsync(string _url, string _json, int _timeoutInMilliseconds = 3000, int _attempts = 3)
        {
            return await TrySendRequestAsync(_url, _json, _timeoutInMilliseconds, _attempts);
        }
        
        private async Task<RequestResponse> TrySendRequestAsync(string _url, string _json, int _timeout, int _attempts)
        {
            var errorMessage = "";
            var attemptsUsed = 0;
            long responseCode = 0;
            for (var i = 0; i < _attempts; i++)
            {
                attemptsUsed++;
                
                var request = CreateRequest(_url, _json, _timeout);
                var result = await SendRequestInternalAsync(request);
                result.attemptsUsed = attemptsUsed;
                if(result.success) 
                    return result;
                
                errorMessage = result.error;
                responseCode = result.responseCode;
            }
            return new RequestResponse()
            {
                success = false,
                error = errorMessage,
                attemptsUsed = attemptsUsed,
                responseCode = responseCode
            };
        }

        private async Task<RequestResponse> SendRequestInternalAsync(UnityWebRequest _request)
        {
            var requestResult = _request.SendWebRequest();
            while (!requestResult.isDone)
            {
                await Task.Yield();
            }

            var result = new RequestResponse()
            {
                success = _request.result == UnityWebRequest.Result.Success,
                responseCode = _request.responseCode,
                response = _request.downloadHandler.text,
                error = _request.error
            };
            
            _request.Dispose();

            return result;
        }

        private UnityWebRequest CreateRequest(string _url, string _json, int _timeoutInMilliseconds)
        {
            var request = new UnityWebRequest(_url, "POST");
            var jsonToSend = new UTF8Encoding().GetBytes(_json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.timeout = _timeoutInMilliseconds;
            request.SetRequestHeader("Content-Type", "application/json");
            return request;
        }
    }
}