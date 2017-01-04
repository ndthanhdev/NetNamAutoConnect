using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Windows.Web.Http;

namespace NetNamAutoConnectLibrary.Services
{
    public static class WifiServices
    {
        private static readonly Uri LOGIN_URI = new Uri("http://logoutwifi.netnam.vn/login");

        private static readonly Uri LOGOUT_URI = new Uri("http://logoutwifi.netnam.vn/logout");

        public static async Task Login(string username, string password)
        {
            try
            {
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    var httpContent = InnerGenerateHttpContent(username, password);
#if DEBUG
                    var debug = await client.PostAsync(LOGIN_URI, httpContent);
                    var result = await debug.Content.ReadAsStringAsync();
#else
                    await client.PostAsync(LOGIN_URI, httpContent);
#endif
                }
            }
            catch (Exception)
            {
            }
        }

        public static async Task Logout()
        {
            try
            {
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    await client.GetAsync(LOGOUT_URI);
                }
            }
            catch (Exception)
            {
            }
        }

        private static IHttpContent InnerGenerateHttpContent(string username, string password)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("username", username);
            formData.Add("password", password);
            return new HttpFormUrlEncodedContent(formData);
        }
    }
}