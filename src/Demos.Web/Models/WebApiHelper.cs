using System;
using System.Net;
using System.Net.Http;
using Demos.Common;

namespace Demos.Web.Models
{
    public class WebApiHelper
    {
        public static TResult PostAsJson<TInput, TResult>(string uri, TInput input)
        {
            var result = default(TResult);
            var client = Create();
            var response = client.PostAsJsonAsync(uri, input).Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<TResult>().Result;
            }
            LogResult(uri, response.StatusCode, result);
            return result;
        }
        
        public static TResult GetAsJson<TResult>(string uri)
        {
            var result = default(TResult);
            var client = Create();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("abc");
            var response = client.GetAsync(uri).Result;
            //var test = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<TResult>().Result;
            }
            LogResult(uri, response.StatusCode, result);
            return result;
        }

        public static HttpClient Create(bool https = true)
        {
            var client = new HttpClient();
            //When you set HttpWebRequest.KeepAlive = true the header set is Connection: keep-alive
            //When you set HttpWebRequest.KeepAlive = false the header set is Connection: close
            //So you will need
            //client.DefaultRequestHeaders.Add("Connection", "close");
            client.DefaultRequestHeaders.ConnectionClose = true;

            if (https)
            {
                if (_oldProtocolType == null)
                {
                    _oldProtocolType = ServicePointManager.SecurityProtocol;
                }
                ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            }
            return client;
        }

        public string GetCurrentUriParams(string name, string currentUrl)
        {
            if (currentUrl != null)
            {
                var uri = new Uri(currentUrl);
                var nameValueCollection = uri.ParseQueryString();
                var value = nameValueCollection.Get(name);
                return value;
            }
            return null;
        }

        private static SecurityProtocolType? _oldProtocolType = null;
        private static void LogResult<T>(string uri, HttpStatusCode statusCode, T result)
        {
            object resultLog = string.Empty;
            if (!result.IsDefault())
            {
                resultLog = result;
            }
            var trace = string.Format("{0} -> {1} -> {2}", uri, statusCode, resultLog);
            Console.WriteLine(trace);
        }
    }
}
