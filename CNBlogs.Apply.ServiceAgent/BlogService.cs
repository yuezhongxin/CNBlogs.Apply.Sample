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
using System.Net;
using CNBlogs.Apply.Application.DTOs;
using CNBlogs.Apply.Domain.ValueObjects;

namespace CNBlogs.Apply.ServiceAgent
{
    public class BlogService
    {
        private static string _blogHost = "";

        public static async Task<bool> EnableJsPermission(string alias)
        {
            using (var httpCilent = new HttpClient())
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{_blogHost}/blogs/{alias}/enable-script")
                };
                var response = await httpCilent.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    //Logging.Logger.Default.Info("UserService.IsAdmin",
                    //    $"{response.StatusCode}: {response.Content.ReadAsStringAsync().Result}");
                }
                return false;
            }
        }

        public static async Task<bool> HaveJsPermission(string alias)
        {
            using (var httpCilent = new HttpClient())
            {
                var response = await httpCilent.GetAsync($"{_blogHost}/blogs/{alias}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var blog = await response.Content.ReadAsAsync<Blog>();
                    if (blog != null)
                    {
                        return blog.EnableScript;
                    }
                    //Logging.Logger.Default.Info("UserService.GetUserByLoginName", "获取博客信息失败！");
                }
                else
                {
                    //Logging.Logger.Default.Info("UserService.GetUserByLoginName",
                    //    $"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                }
                throw new Exception("获取博客信息失败！");
            }
        }

        public static async Task<bool> ExistBlogApp(string targetBlogApp)
        {
            using (var httpCilent = new HttpClient())
            {
                var response = await httpCilent.GetAsync($"{_blogHost}/blogs/{targetBlogApp}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else if (response.StatusCode != HttpStatusCode.NotFound)
                {
                    //Logging.Logger.Default.Info("UserService.ExistBlogApp",
                    //    $"{response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
                }
                return false;
            }
        }
    }

    public class Blog
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public int PostCount { get; set; }

        public int PageSize { get; set; }

        public bool EnableScript { get; set; }
    }
}
