using CedroRestaurante.Services.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CedroRestaurante.API.Helpers
{
    public static class MessageHelper
    {
        public static HttpResponseMessage CreateMessage(HttpStatusCode code, string message)
        {
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(message, Encoding.UTF8, "text/plain")
            };
        }

        public static async Task<HttpResponseMessage> CreateMessageAsync(HttpStatusCode code, object obj)
        {
            string objString = await Task.Run(() => JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Ignore
            }
            ));
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(objString, Encoding.UTF8, "application/json")
            };
        }

        public static async Task<HttpResponseMessage> CreateMessageAsync()
        {
            string objString = await Task.Run(() => JsonConvert.SerializeObject(Notification.Messages, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Include,
                NullValueHandling = NullValueHandling.Ignore
            }
            ));
            return new HttpResponseMessage(Notification.HttpStatusCode)
            {
                Content = new StringContent(objString, Encoding.UTF8, "application/json")
            };
        }
    }
}
