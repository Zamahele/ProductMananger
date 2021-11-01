using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Security
    {
        public static readonly HttpClient httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });

        static Security()
        {
            httpClient.BaseAddress = new Uri("http://localhost:4300/api/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static string UserToken { get; set; }

        public static UserSecretInfo getUserToken(string UserName, string Passoword)
        {
            var response = httpClient.PostAsJsonAsync("Authenticate/GetUserToken", new User { Username = UserName, Password = Passoword }).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
            var data = response.Content.ReadAsAsync<User>().Result;
            return new UserSecretInfo { Token = data.Token, Expiration = data.Expiration };
        }
    }
}
