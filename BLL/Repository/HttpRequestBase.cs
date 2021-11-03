using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class HttpRequestBase
    {
        public static string _url => GetAppSetting("Url");  
        public static readonly HttpClient httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
        static HttpRequestBase()
        {
            httpClient.BaseAddress = new Uri(_url);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Security.UserToken);
        }

        private static string GetAppSetting(string key)
        {
            try
            {
                var asmPath = Assembly.GetExecutingAssembly().Location;
                var config = ConfigurationManager.OpenExeConfiguration(asmPath);
                var setting = config.AppSettings.Settings[key];
                return setting.Value;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error reading configuration setting", e);
            }
        }
    }
}
