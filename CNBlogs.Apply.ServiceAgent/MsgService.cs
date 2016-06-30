using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CNBlogs.Apply.ServiceAgent
{
    public class MsgService
    {
        private static string _msgHost = "";
        private static bool _isNotify = false;

        public static async Task Send(string title, string body, int recipientId)
        {
            if (_isNotify)
            {
                using (var client = new HttpClient())
                {
                    HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(new
                    {
                        Title = title,
                        Content = body,
                        RecipientId = recipientId,
                    }));
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    await client.PostAsync(_msgHost + "/api/notifications", httpContent);
                }
            }
        }
    }
}
