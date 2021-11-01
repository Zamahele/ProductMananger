using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMananger.Models
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCache cache;

        public TokenService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public void SetCacheToken(string token, DateTime expr)
        {
            cache.Set("TOKEN", token,expr);
        }

        public string FetchToken()
        {
            string token = string.Empty;

            // if cache doesn't contain 
            // an entry called TOKEN
            // error handling mechanism is mandatory
            if (!cache.TryGetValue("TOKEN", out token))
            {
              //  var tokenmodel = this.GetTokenFromApi();

                // keep the value within cache for 
                // given amount of time
                // if value is not accessed within the expiry time
                // delete the entry from the cache
                //var options = new MemoryCacheEntryOptions()
                //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(90));

                cache.Set("TOKEN", "");

                token = "";
            }

            return token;
        }
    }
}
