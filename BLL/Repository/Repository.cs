using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace BLL.Repository
{
    public class Repository<T>
    {
        public string requestUrl { get; set; }

        public IQueryable<T> FindAll()
        {
            var response = HttpRequestBase.httpClient.GetAsync(requestUrl).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
            var result = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
            return result.AsQueryable();
        }


        public T Find(int? id)
        {
            var response = HttpRequestBase.httpClient.GetAsync(requestUrl + "/" + id).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
            var result = response.Content.ReadAsAsync<T>().Result;
            return result;
        }


        public void Update(T target, int id)
        {
            var response = HttpRequestBase.httpClient.PutAsJsonAsync(requestUrl + "/" + id, target).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
        }

        public T Save(T target)
        {
            var response = HttpRequestBase.httpClient.PostAsJsonAsync(requestUrl, target).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
            return response.Content.ReadAsAsync<T>().Result;
        }

        public void Remove(int? id)
        {
            var response = HttpRequestBase.httpClient.DeleteAsync(requestUrl + "/" + id).Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ToString());
        }
    }
}
