using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using CNBlogs.Apply.Domain.ValueObjects;
using System.Net;

namespace CNBlogs.Apply.ServiceAgent
{
    public class UserService
    {
        private static string userHost = "";

        public static async Task<User> GetUserByLoginName(string loginName)
        {
            using (var httpCilent = new HttpClient())
            {
                httpCilent.BaseAddress = new System.Uri(userHost);
                var response = await httpCilent.GetAsync($"/users?loginName={Uri.EscapeDataString(loginName)}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsAsync<User>();
                }
                return null;
            }
        }
    }
}
