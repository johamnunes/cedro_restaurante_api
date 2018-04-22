using System.Collections.Generic;
using System.Net;

namespace CedroRestaurante.Services.Helpers
{
    public static class Notification
    {
        public static HttpStatusCode HttpStatusCode { get; set; }
        public static List<string> Messages = new List<string>();

        public static void CreateMessage(HttpStatusCode httpStatusCode, string message)
        {
            Messages = new List<string> { message };
            HttpStatusCode = httpStatusCode;
        }
        public static void CreateMessage(HttpStatusCode httpStatusCode, List<string> message)
        {
            Messages = message;
            HttpStatusCode = httpStatusCode;
        }
    }
}
