using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace NetNamAutoConnectLibrary
{
    public static class WifiServices
    {
        private static readonly Uri LOG_IN_URI = new Uri("http://logoutwifi.netnam.vn/login");
        private static readonly Uri LOG_OUT_URI = new Uri("http://logoutwifi.netnam.vn/logout");
        private static readonly Uri NETNAM_URI = new Uri("http://logoutwifi.netnam.vn/");
        private static readonly Uri STATUS_URI = new Uri("http://logoutwifi.netnam.vn/status");

        public static async Task<bool> IsAuthenticated()
        {
            try
            {
                await Task.Yield();
                using (var filter = new HttpBaseProtocolFilter())
                {
                    filter.AllowAutoRedirect = false;
                    using (HttpClient client = new HttpClient(filter))
                    {
                        var response = await client.GetAsync(STATUS_URI);
                        return HttpStatusCode.Ok == response.StatusCode;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> IsConnectWithWifiNetNam()
        {
            try
            {
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(NETNAM_URI);
                    return HttpStatusCode.Ok == response.StatusCode;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task Login(string username, string password)
        {
            try
            {
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    var httpContent = InnerGenerateHttpContent(username, password);
#if DEBUG
                    var debug = await client.PostAsync(LOG_IN_URI, httpContent);
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
                    await client.GetAsync(LOG_OUT_URI);
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