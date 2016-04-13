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

namespace CNBlogs.Apply.ServiceAgent
{
    public class UserService
    {
        public static async Task<int> GetUserIdByDisplayName(string displayName)
        {
            return 1;
        }

        public static async Task<bool> IsHasBlog(int userId)
        {
            return true;
        }
    }
}
