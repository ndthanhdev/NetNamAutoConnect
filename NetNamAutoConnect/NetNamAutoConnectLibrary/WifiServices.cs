using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
                await GetStatus();
                await Task.Yield();
                using (HttpClient client = new HttpClient())
                {
                    var httpContent = InnerGenerateHttpContent(username, password);
#if DEBUG
                    var debug = await client.PostAsync(LOG_IN_URI, httpContent);
                    var result = await debug.Content.ReadAsStringAsync();
#else
                    await client.PostAsync(LOG_IN_URI, httpContent);
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

        // TODO: implement it
        public static async Task<string> GetStatus()
        {
            await Task.Yield();
            try
            {
                await Task.Yield();
                using (var filter = new HttpBaseProtocolFilter())
                {
                    filter.AllowAutoRedirect = false;
                    using (HttpClient client = new HttpClient(filter))
                    {
                        var response = await client.GetAsync(STATUS_URI);
                        if (HttpStatusCode.Ok == response.StatusCode)
                        {
                            var responseContentRaw = await response.Content.ReadAsStringAsync();
                            var responseContent = System.Net.WebUtility.HtmlDecode(responseContentRaw);
                            //var doc = new HtmlDocument();
                            //doc.LoadHtml(responseContent);
                            

                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
    }
}