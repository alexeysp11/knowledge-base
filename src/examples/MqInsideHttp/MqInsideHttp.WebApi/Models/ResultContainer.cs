namespace MqInsideHttp.WebApi.Models
{
    internal static class ResultContainer
    {
        private static List<string> _requestIdList;
        private static Dictionary<string, MqInsideHttpResponse> _responses;

        static ResultContainer()
        {
            _requestIdList = new List<string>();
            _responses = new Dictionary<string, MqInsideHttpResponse>();
        }

        internal static void AddRequestId(string requestId)
        {
            lock (_requestIdList)
            {
                if (!_requestIdList.Contains(requestId))
                {
                    _requestIdList.Add(requestId);
                }
            }
        }

        internal static MqInsideHttpResponse? GetResponseByRequestId(string requestId)
        {
            lock (_responses)
            {
                if (_responses.ContainsKey(requestId))
                {
                    return _responses[requestId];
                }
            }
            return null;
        }

        internal static List<string> GetRequestIdList()
        {
            lock (_requestIdList)
            {
                return new List<string>(_requestIdList);
            }
        }

        internal static void AddResponse(string requestId, MqInsideHttpResponse response)
        {
            lock (_responses)
            {
                if (!_responses.ContainsKey(requestId))
                {
                    _responses.Add(requestId, response);
                }
            }
        }
    }
}
